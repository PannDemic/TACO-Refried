using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;

namespace Taco.Classes
{
    public sealed class LogWatcher : Timer
    {
        private string _logPath;
        private string _systemChannelPrefix;
        private Encoding _fileEncoding;
        private LogFileType _logFileType;
        private List<LogEntry>  _previousLogEntries = new List<LogEntry>();
        private Dictionary<string, long> _fileSizes = new Dictionary<string, long>();
        private Dictionary<string, InterestingFile> _interestingFiles = new Dictionary<string, InterestingFile>();

        public event EventHandler<ProcessNewDataEventArgs> ProcessNewData;
        public event EventHandler<ProcessCombatEventArgs> ProcessCombat;

        public LogWatcher(string systemChannelPrefix, LogFileType logFileType, string logPath = null)
        {
            Interval = 250;
            Tick += OnTick;

            _systemChannelPrefix = systemChannelPrefix;
            _logFileType = logFileType;

            RootLogsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"EVE\logs");

            if (logPath != null)
                RootLogsPath = logPath;

            if (_logFileType == LogFileType.Game)
            {
                _fileEncoding = Encoding.ASCII;
                _logPath = RootLogsPath + @"\Gamelogs";
            }
            else
            {
                _fileEncoding = Encoding.Unicode;
                _logPath = RootLogsPath + @"\Chatlogs";
            }
        }

        public static string GetRootLogPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"EVE\logs");
        }
         
        public string SystemChannelPrefix
        {
            get
            {
                return _systemChannelPrefix;
            }
        }

        public string RootLogsPath { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ISynchronizeInvoke SynchronizingObject { get; set; }

        readonly Regex _chatLogRegex       = new Regex(@"\[\s\d{4}\.\d{2}\.\d{2}\s(?<time>\d{2}\:\d{2}\:\d{2})\s\]\s(?<name>\w.*)\s>\s(?<content>.*)", RegexOptions.Compiled);
        readonly Regex _gameLogRegex       = new Regex(@"\[\s\d{4}\.\d{2}\.\d{2}\s(?<time>\d{2}\:\d{2}\:\d{2})\s\]\s\(\w.*\)\s(?<content>.*)", RegexOptions.Compiled);
        readonly Regex _gameLogCombatRegex = new Regex(@"\[\s\d{4}\.\d{2}\.\d{2}\s\d{2}\:\d{2}\:\d{2}\s\]\s\(combat\)", RegexOptions.Compiled);

        private NewData ReadLogFile(string fileName, long startingPosition)
        {
            var newData = new NewData();

            using (var sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), _fileEncoding))
            {
                try
                {
                    sr.BaseStream.Seek(startingPosition, SeekOrigin.Begin);
                    newData.Data = CleanLineData(sr.ReadToEnd());
                    newData.FileLength = sr.BaseStream.Length;

                    return newData;
                }
                catch (Exception)
                {
                    newData.Data = string.Empty;
                    newData.FileLength = -1;
                    return newData;
                }
            }
        }

        private FileInfo InitLogFileInfo()
        {
            string fileName = (_logFileType != LogFileType.Game ? _systemChannelPrefix : "") + "*.txt";
            var dirfiles = new List<FileInfo>(new DirectoryInfo(_logPath).GetFiles(fileName)
                .Where(file => file.CreationTime > DateTime.Now.AddDays(-1))
                .OrderByDescending(file => file.LastWriteTime)
                .ThenByDescending(file => file.Name));

            bool isFirst = true;
            FileInfo tempFileInfo = null;

            foreach (var fileInfo in dirfiles)
            {
                var tempLength = GetFileLength(fileInfo.FullName);

                if (isFirst)
                {
                    tempFileInfo = fileInfo;
                    isFirst = false;
                }

                _fileSizes.Add(fileInfo.FullName, tempLength);
            }

            return tempFileInfo;
        }

        readonly Regex _localListener      = new Regex(@"Listener\:\s*(?<name>.*)", RegexOptions.Compiled);

        private string GetLogListener(string fileName)
        {
            var characterName = string.Empty;

            using (var sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), _fileEncoding))
            {
                try
                {
                    while (sr.Peek() >= 0)
                    {
                        var line = CleanLineData(sr.ReadLine());

                        var lineMatch = _localListener.Match(line ?? string.Empty);
                        if (lineMatch.Success)
                        {
                            characterName = lineMatch.Groups["name"].ToString();
                            break;
                        }

                    }
                }
                catch (Exception)
                {
                    return characterName;
                }
            }

            return characterName;
        }

        private List<FileInfo> GetChangedLogFiles()
        {
            var files = new List<FileInfo>();

            string fileName = (_logFileType != LogFileType.Game ? _systemChannelPrefix : "") + "*.txt";
            var dirfiles = new List<FileInfo>(new DirectoryInfo(_logPath).GetFiles(fileName)
                .Where(file => file.CreationTime > DateTime.Now.AddDays(-1))
                .OrderByDescending(file => file.LastWriteTime)
                .ThenByDescending(file => file.Name));

            foreach (var fileInfo in dirfiles)
            {
                var tempLength = GetFileLength(fileInfo.FullName);

                if (_fileSizes.ContainsKey(fileInfo.FullName))
                {
                    if (_fileSizes[fileInfo.FullName] != tempLength)
                    {
                        _fileSizes[fileInfo.FullName] = tempLength;
                        files.Add(fileInfo);
                    }
                }
                else
                {
                    _fileSizes.Add(fileInfo.FullName, tempLength);
                    files.Add(fileInfo);
                }
            }

            return files;
        }

        private string CleanLineData(string line)
        {
            return
                line.Replace(Convert.ToChar(65279).ToString(), "")
                    .Replace(Convert.ToChar(65534).ToString(), "")
                    .Replace("\r", "")
                    .Trim();
        }


        private void OnTick(object sender, EventArgs eventArgs)
        {
            Enabled = false;

            try
            {
                // Clean out expired previous log entries
                _previousLogEntries.RemoveAll(logEntry => logEntry.TimeAdded < DateTime.Now.AddSeconds(-5));

                // Process new files
                List<FileInfo> logFiles = GetChangedLogFiles();

                foreach (var fileInfo in logFiles)
                {
                    lock (this)
                    {
                        if (_interestingFiles.ContainsKey(fileInfo.FullName)) continue;

                        long tempLength = GetFileLength(fileInfo.FullName);
                        string tempCharName = GetLogListener(fileInfo.FullName);
                        _interestingFiles.Add(fileInfo.FullName,
                            new InterestingFile(fileInfo.FullName, tempLength, DateTime.Now, tempCharName));

                        LogEntry tempLogEntry = new LogEntry
                        {
                            FileName = fileInfo.Name,
                            EntryType =
                                _logFileType == LogFileType.Game
                                    ? LogEntryType.NewGameLogEvent
                                    : LogEntryType.NewChatLogEvent,
                            LineContent = tempLength.ToString(),
                            LogType = _logFileType,
                            CharacterName = tempCharName
                        };

                        OnProcessNewData(tempLogEntry);
                    }
                }

                // Raise events for game logs without an update in list 30 secs
                // and that have not already been triggered.
                if (_logFileType == LogFileType.Game)
                {
                    foreach (
                        var interestingFile in
                            _interestingFiles.Where(
                                interestingFile =>
                                    interestingFile.Value.InCombat &&
                                    !interestingFile.Value.TimeOutTriggered &&
                                    interestingFile.Value.LastCombat < DateTime.Now.AddSeconds(-30)))
                    {
                        interestingFile.Value.TimeOutTriggered = true;
                        interestingFile.Value.InCombat = false;
                        OnCombat(interestingFile.Value.FileName, interestingFile.Value.CharName, CombatEventType.Stop);
                    }
                }

                // Remove files with no update in last 120 mins from interesting files list
                List<string> uninterestingFiles = (from interestingFile in _interestingFiles
                    where interestingFile.Value.LastUpdate < DateTime.Now.AddMinutes(-120)
                    select interestingFile.Key).ToList();

                foreach (var uninterestingFile in uninterestingFiles)
                {
                    lock (this)
                    {
                        _interestingFiles.Remove(uninterestingFile);
                    }
                }

                // Get new content from all interesting files
                List<LogEntry> entries = new List<LogEntry>();

                lock (this)
                {
                    foreach (var interestingFile in _interestingFiles)
                    {
                        var tempFileInfo = new FileInfo(interestingFile.Key);

                        var tempFileLength = GetFileLength(tempFileInfo.FullName);

                        if (tempFileLength != interestingFile.Value.LastPosition)
                        {
                            NewData newData;
                            lock (this)
                            {
                                newData = ReadLogFile(interestingFile.Key, interestingFile.Value.LastPosition);
                                _interestingFiles[interestingFile.Key].LastPosition = newData.FileLength;
                                _interestingFiles[interestingFile.Key].LastUpdate = DateTime.Now;
                                
                            }

                            var lines = newData.Data.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                            lines =
                                lines.Select(
                                    line =>
                                        line.Replace(Convert.ToChar(65279).ToString(), "")
                                            .Replace(Convert.ToChar(65534).ToString(), "")
                                            .Replace("\r", "")
                                            .Trim()).Where(line => line.Length > 0).ToArray();

                            //lines = lines.Where(line => line.Trim().Length > 0).ToArray();

                            if (lines.Length == 0)
                                continue;

                            foreach (var line in lines)
                            {
                                var tempLogEntry = new LogEntry();
                                var lineMatch = _logFileType == LogFileType.Chat
                                    ? _chatLogRegex.Match(line)
                                    : _gameLogRegex.Match(line);

                                tempLogEntry.RawLine = line;
                                tempLogEntry.FileName = tempFileInfo.Name;
                                tempLogEntry.LogPrefix = _systemChannelPrefix;
                                tempLogEntry.TimeAdded = DateTime.Now;
                                tempLogEntry.LogType = _logFileType;
                                tempLogEntry.CharacterName = interestingFile.Value.CharName;

                                if (_logFileType == LogFileType.Game)
                                {
                                    if (_gameLogCombatRegex.IsMatch(line))
                                    {
                                        if (!_interestingFiles[interestingFile.Key].InCombat)
                                        {
                                            _interestingFiles[interestingFile.Key].InCombat = true;
                                            OnCombat(interestingFile.Value.FileName, interestingFile.Value.CharName,
                                                CombatEventType.Start);
                                        }

                                        _interestingFiles[interestingFile.Key].TimeOutTriggered = false;
                                        _interestingFiles[interestingFile.Key].LastCombat =
                                            _interestingFiles[interestingFile.Key].LastUpdate;
                                    }
                                }

                                if (lineMatch.Success)
                                {
                                    tempLogEntry.LogTime = lineMatch.Groups["time"].ToString();
                                    tempLogEntry.LineContent = lineMatch.Groups["content"].ToString();
                                    tempLogEntry.EntryType = LogEntryType.ChatEvent;
                                    tempLogEntry.ParseSuccess = true;

                                    if (_logFileType == LogFileType.Chat)
                                        tempLogEntry.PlayerName = lineMatch.Groups["name"].ToString();
                                }
                                else
                                {
                                    tempLogEntry.LineContent = line;
                                    tempLogEntry.ParseSuccess = false;
                                    tempLogEntry.EntryType = _logFileType == LogFileType.Chat
                                        ? LogEntryType.UnknownChatLogEvent
                                        : LogEntryType.UnknownGameLogEvent;
                                }

                                if (_previousLogEntries.All(logEntry => logEntry.LineContent != tempLogEntry.LineContent))
                                {
                                    _previousLogEntries.Add(tempLogEntry);
                                    entries.Add(tempLogEntry);
                                }
                            }
                        }
                    }
                }

                if (entries.Count > 0)
                {
                    foreach (var logEntry in entries)
                    {
                        OnProcessNewData(logEntry);
                    }
                }
            }
            finally
            {
                Enabled = true;
            }
        }

        public long GetFileLength(string fileName)
        {
            long tempLength = -1;
            using (var sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), _fileEncoding))
            {
                try
                {
                    tempLength = sr.BaseStream.Length;
                }
                catch (Exception)
                {
                    // Ignored
                }
            }

            return tempLength;
        }

        public bool StartWatch()
        {
            if (Enabled)
            {
                return true;
            }

            _interestingFiles.Clear();

            var logFile = InitLogFileInfo();

            if (logFile != null)
            {
                long tempLength = GetFileLength(logFile.FullName);
                string tempCharName = GetLogListener(logFile.FullName);
                _interestingFiles.Add(logFile.FullName, new InterestingFile(logFile.FullName, tempLength, DateTime.Now, tempCharName));

                var tempLogEntry = new LogEntry
                {
                    FileName = logFile.Name,
                    EntryType = _logFileType == LogFileType.Game ? LogEntryType.OpenGameLogEvent : LogEntryType.OpenChatLogEvent,
                    LineContent = tempLength.ToString(),
                    CharacterName = tempCharName
                };

                OnProcessNewData(tempLogEntry);
            }

            Enabled = true;
            return true;
        }

        public void StopWatch()
        {
            Enabled = false;
        }

        private void OnProcessNewData(LogEntry entry)
        {
            if (ProcessNewData != null)
                ProcessNewData.Invoke(this, new ProcessNewDataEventArgs(entry, _systemChannelPrefix));
        }

        private void OnCombat(string logFileName, string characterName, CombatEventType combatEvent)
        {
            if (ProcessCombat != null)
                ProcessCombat.Invoke(this, new ProcessCombatEventArgs(logFileName, characterName, combatEvent));
        }
    }

    public class ProcessCombatEventArgs : EventArgs
    {
        public ProcessCombatEventArgs(string logFileName, string characterName, CombatEventType combatEvent)
        {
            LogFileName = logFileName;
            CharacterName = characterName;
            CombatEvent = combatEvent;
        }

        public string LogFileName { get; set; }
        public string CharacterName { get; set; }
        public CombatEventType CombatEvent { get; set; }
    }

    public class ProcessNewDataEventArgs : EventArgs
    {
        public ProcessNewDataEventArgs(LogEntry logEntry, string logPrefix)
        {
            LogEntry = logEntry;
            LogPrefix = logPrefix;
        }

        public LogEntry LogEntry { get; set; }
        public string LogFileName { get; set; }
        public string LogPrefix { get; set; }
    }

    public class InterestingFile
    {
        public InterestingFile(string fileName, long lastPosition, DateTime lastUpdate, string charName = "")
        {
            FileName = fileName;
            LastPosition = lastPosition;
            LastUpdate = lastUpdate;
            CharName = charName;
            TimeOutTriggered = true;
        }

        public string FileName { get; set; }
        public long LastPosition { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime LastCombat { get; set; }
        public string CharName { get; set; }
        public bool TimeOutTriggered { get; set; }
        public bool InCombat { get; set; }
    }

    public class LogLine
    {
        public string FileName { get; set; }
        public string LineContent { get; set; }
    }

    public class NewData
    {
        public string Data { get; set; }
        public long FileLength { get; set; }
    }

    public class LogFileInfo
    {
        public long FileSize { get; set; }
        public bool IsNew { get; set; }
    }

    public class LogEntry
    {
        public string FileName { get; set; }
        public string LogTime { get; set; }
        public string PlayerName { get; set; }
        public string CharacterName { get; set; }
        public string LineContent { get; set; }
        public string LogPrefix { get; set; }
        public LogFileType LogType { get; set; }
        public LogEntryType EntryType { get; set; }
        public bool ParseSuccess { get; set; }
        public string RawLine { get; set; }
        public HashSet<int> MatchedIds { get; set; }
        public DateTime TimeAdded { get; set; }
    }

    public enum CombatEventType
    {
        Start,
        Stop
    }

    public enum LogEntryType
    {
        OpenChatLogEvent,
        NewChatLogEvent,
        OpenGameLogEvent,
        NewGameLogEvent,
        UnknownChatLogEvent,
        UnknownGameLogEvent,
        ExpiredChatLogEvent,
        ExpiredGameLogEvent,
        ChatEvent
    }

    public enum LogFileType
    {
        Game,
        Chat
    }
}

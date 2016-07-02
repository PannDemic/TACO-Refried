using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Taco.Classes
{
    public sealed class LocalWatcher : Timer
    {
        private string _logPath;
        private Encoding _fileEncoding;
        private Dictionary<string, long> _fileSizes = new Dictionary<string, long>();
        private Dictionary<string, InterestingFile> _interestingFiles = new Dictionary<string, InterestingFile>();

        public event EventHandler<ProcessSystemChangeEventArgs> SystemChange;

        public LocalWatcher(string logPath = null)
        {
            Interval = 250;
            Tick += OnTick;

            RootLogsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"EVE\logs");

            if (logPath != null)
                RootLogsPath = logPath;

            _fileEncoding = Encoding.Unicode;
            _logPath = RootLogsPath + @"\Chatlogs";
        }

        public static string GetRootLogPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"EVE\logs");
        }
         
        public string RootLogsPath { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ISynchronizeInvoke SynchronizingObject { get; set; }

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
            string fileName = "local*.txt";
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

        readonly Regex _localSystemChange = new Regex(@"EVE\sSystem\s>\sChannel\schanged\sto\sLocal\s:\s(?<systemname>.*)", RegexOptions.Compiled);
        readonly Regex _localListener = new Regex(@"Listener\:\s*(?<name>.*)", RegexOptions.Compiled);
        readonly Regex _localInitialSystem = new Regex(@"Channel\sID:\s*\(\(\'solarsystemid2\',\s(?<initialsystem>[0-9]{8})", RegexOptions.Compiled);

        //private string GetLogListener(string fileName)
        //{
        //    var characterName = string.Empty;

        //    using (var sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), _fileEncoding))
        //    {
        //        try
        //        {
        //            while (sr.Peek() >= 0)
        //            {
        //                var line = CleanLineData(sr.ReadLine());

        //                var lineMatch = _localListener.Match(line ?? string.Empty);
        //                if (lineMatch.Success)
        //                {
        //                    characterName = lineMatch.Groups["name"].ToString();
        //                    break;
        //                }

        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return characterName;
        //        }
        //    }

        //    return characterName;
        //}

        private LocalInfo InitLocal(string fileName)
        {
            LocalInfo tempInfo = new LocalInfo();

            using (var sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), _fileEncoding))
            {
                try
                {
                    while (sr.Peek() >= 0)
                    {
                        var line = CleanLineData(sr.ReadLine());

                        if (tempInfo.InitialSystem == -1)
                        {
                            var lineMatch = _localInitialSystem.Match(line ?? string.Empty);
                            if (lineMatch.Success)
                            {
                                tempInfo.InitialSystem = int.Parse(lineMatch.Groups["initialsystem"].ToString());
                                continue;
                            }
                        }

                        if (tempInfo.CharName == string.Empty)
                        {
                            var lineMatch = _localListener.Match(line ?? string.Empty);
                            if (lineMatch.Success)
                            {
                                tempInfo.CharName = lineMatch.Groups["name"].ToString();
                                if (tempInfo.CharName.Trim().Length <= 4)
                                {
                                    tempInfo.CharName = string.Empty;
                                }
                                continue;
                            }
                        }

                        var systemChangeMatch = _localSystemChange.Match(line ?? string.Empty);
                        if (systemChangeMatch.Success)
                        {
                            tempInfo.CurrentSystem = systemChangeMatch.Groups["systemname"].ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    return new LocalInfo();
                }
            }            

            return tempInfo;
        }

        private List<FileInfo> GetChangedLogFiles()
        {
            var files = new List<FileInfo>();

            string fileName = "local*.txt";
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
                // Process new files
                List<FileInfo> logFiles = GetChangedLogFiles();

                foreach (var fileInfo in logFiles)
                {
                    lock (this)
                    {
                        if (_interestingFiles.ContainsKey(fileInfo.FullName)) continue;

                        long tempLength = GetFileLength(fileInfo.FullName);

                        LocalInfo tempLocalInfo = InitLocal(fileInfo.FullName);

                        _interestingFiles.Add(fileInfo.FullName,
                            new InterestingFile(fileInfo.FullName, tempLength, DateTime.Now, tempLocalInfo.CharName));

                        OnSystemChange(
                            tempLocalInfo.CurrentSystem == string.Empty
                                ? tempLocalInfo.InitialSystem.ToString()
                                : tempLocalInfo.CurrentSystem,
                            tempLocalInfo.CharName);
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

                            lines = lines.Where(line => line.Length > 0).ToArray();

                            
                            //var lines = newData.Data.Split(new[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);

                            //lines = lines.Select(line => line.Replace(Convert.ToChar(65279).ToString(), "").Replace("\r", "").Trim()).Where(line => line.Length > 0).ToArray();
                            //lines = lines.Select(line => line.Trim()).Where(line => line.Length > 0).ToArray();

                            if (lines.Length == 0)
                                continue;

                            foreach (var line in lines)
                            {
                                var lineMatch = _localSystemChange.Match(line);
                                if (lineMatch.Success)
                                {
                                    OnSystemChange(lineMatch.Groups["systemname"].ToString(), interestingFile.Value.CharName);
                                }
                            }
                        }
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

                InitialLocalInfo = InitLocal(logFile.FullName);
                _interestingFiles.Add(logFile.FullName, new InterestingFile(logFile.FullName, tempLength, DateTime.Now, InitialLocalInfo.CharName));

                OnSystemChange(
                    InitialLocalInfo.CurrentSystem == String.Empty
                        ? InitialLocalInfo.InitialSystem.ToString()
                        : InitialLocalInfo.CurrentSystem,
                    InitialLocalInfo.CharName);
            }

            Enabled = true;
            return true;
        }

        public LocalInfo InitialLocalInfo { get; set; }

        public void StopWatch()
        {
            Enabled = false;
        }

        private void OnSystemChange(string systemName, string charName)
        {
            if (SystemChange != null)
                SystemChange.Invoke(this, new ProcessSystemChangeEventArgs(systemName, charName));
        }

        //private void InitializeComponent()
        //{
        //    this.dataGridView1 = new System.Windows.Forms.DataGridView();
        //    ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        //    // 
        //    // dataGridView1
        //    // 
        //    this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        //    this.dataGridView1.Location = new System.Drawing.Point(0, 0);
        //    this.dataGridView1.Name = "dataGridView1";
        //    this.dataGridView1.Size = new System.Drawing.Size(240, 150);
        //    this.dataGridView1.TabIndex = 0;
        //    ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();

        //}
    }

    public class ProcessSystemChangeEventArgs : EventArgs
    {
        public ProcessSystemChangeEventArgs(string systemName, string charName)
        {
            SystemName = systemName;
            CharName = charName;
        }

        public string SystemName { get; set; }
        public string CharName { get; set; }
    }

    public class LocalInfo
    {
        public LocalInfo()
        {
            CharName = string.Empty;
            InitialSystem = -1;
            CurrentSystem = string.Empty;
        }

        public string CharName { get; set; }
        public int InitialSystem { get; set; }
        public string CurrentSystem { get; set; }

    }
}

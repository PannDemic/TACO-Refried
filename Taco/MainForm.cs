using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ProtoBuf;
using QuickFont;
using Taco.Classes;
using PixelFormat = System.Drawing.Imaging.PixelFormat;


namespace Taco
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            _solarSystems = new SolarSystemManager();

            LoadSystemData();
            LoadSounds();
            SetupAutoComplete();
            PopulateSoundCombos();
            LoadConfig();

            glOut.MouseWheel += glOut_MouseWheel;
        }

        #region Variables

        TacoConfig _conf = new TacoConfig(Application.StartupPath);
        TacoCharacters _characters = new TacoCharacters(Application.StartupPath);

        private bool _glLoaded, _shadersLoaded, _isHighlighting, _zooming, _processLogs, _isFullScreen, _fontLoaded, _dragging;
        private bool _loadingAlerts, _hasRendered, _pickingFile, _configLoaded, _muteSound;
        private int _startX, _startY, _dY, _dX, _zoomTick, _highlightTick;
        private int _texSystem, _texGreenCh, _texRedCh, _texYellowCh, _texRedGreenCh, _texRedYellowCh, _texYellowGreenCh;
        private int _w, _h;

        private float _cameraDistance = 12000.0f;
        private float _pSize = 1;

        private int _currentHighlight = -1;
        private int _maxHighlighTick = 30;
        private int _maxZoomTick = 100;
        private int _zoomToSystemId = -1;

        private Shader _shader, _shaderConn, _shaderCrosshair;

        private Vector3 _eye, _lookAt, _zoomStart, _zoomEnd, _zoomDiff;

        private Matrix4 _matrixProjection, _matrixModelview, _dragModelView, _dragProjection;

        private Vector3 _vecY = new Vector3(0f, 1f, 0f);

        private Size _oldUiContainerSize, _oldSize, _oldGlOutSize;

        private Point _oldUiContainerPosition, _oldPosition, _oldGlOutPosition;

        private LogWatcher[] _logFiles = new LogWatcher[12];

        private Dictionary<string, SoundPlayer> _sounds = new Dictionary<string, SoundPlayer>();
        private SoundPlayer _customSound = new SoundPlayer();
        private SoundPlayer _anomalyWatcherSound = new SoundPlayer();

        private VboInfo _vbOs = new VboInfo();

        private Queue<LogEntry> _intelProcessingQueue = new Queue<LogEntry>();
        private Queue<LogEntry> _newIntelQueue = new Queue<LogEntry>();

        private SolarSystemManager _solarSystems;

        private List<AlertTrigger> _alertTriggers = new List<AlertTrigger>();

        private List<int> _ignoreSystems = new List<int>();
        private List<Regex> _ignoreStrings = new List<Regex>();
        private Dictionary<string, TabPage> _hiddenPages = new Dictionary<string, TabPage>();

        private HashSet<int> _triggerSystems = new HashSet<int>();
        private HashSet<int> _stickyHighlightSystems = new HashSet<int>();
        private LocalWatcher _localWatcher = new LocalWatcher();
        private Dictionary<string, int> _charLocations = new Dictionary<string, int>();
        #endregion Variables

        #region Configuration
        private void LoadConfig()
        {
            MonitorBranch.Checked = _conf.MonitorBranch;
            MonitorDeklein.Checked = _conf.MonitorDeklein;
            MonitorTenal.Checked = _conf.MonitorTenal;
            MonitorVenal.Checked = _conf.MonitorVenal;
            MonitorFade.Checked = _conf.MonitorFade;
            MonitorPureBlind.Checked = _conf.MonitorPureBlind;
            MonitorTribute.Checked = _conf.MonitorTribute;
            MonitorVale.Checked = _conf.MonitorVale;
            MonitorProvidence.Checked = _conf.MonitorProvidence;
            MonitorDelve.Checked = _conf.MonitorDelve;
            MonitorGameLog.Checked = _conf.MonitorGameLog;

            AlertBranch.Checked = _conf.AlertBranch;
            AlertDeklein.Checked = _conf.AlertDeklein;
            AlertTenal.Checked = _conf.AlertTenal;
            AlertVenal.Checked = _conf.AlertVenal;
            AlertFade.Checked = _conf.AlertFade;
            AlertPureBlind.Checked = _conf.AlertPureBlind;
            AlertTribute.Checked = _conf.AlertTribute;
            AlertVale.Checked = _conf.AlertVale;
            AlertProvidence.Checked = _conf.AlertProvidence;
            AlertDelve.Checked = _conf.AlertDelve;

            PreserveCameraDistance.Checked = _conf.PreserveCameraDistance;
            PreserveLookAt.Checked = _conf.PreserveLookAt;
            PreserveSelectedSystems.Checked = _conf.PreserveSelectedSystems;

            DisplayNewFileAlerts.Checked = _conf.DisplayNewFileAlerts;
            DisplayOpenFileAlerts.Checked = _conf.DisplayOpenFileAlerts;

            ShowCharacterLocations.Checked = _conf.ShowCharacterLocations;
            DisplayCharacterNames.Checked = _conf.DisplayCharacterNames;

            OverrideLogPath.Checked = _conf.OverrideLogPath;
            if (OverrideLogPath.Checked)
                LogPath.Text = _conf.LogPath;

            MonitorGameLog.Checked = _conf.MonitorGameLog;

            LoadAlertConfig();
            LoadIgnoreLists();
            PopulateCharacterNameCombos();

            CameraFollowCharacter.Checked = _conf.CameraFollowCharacter;
            CentreOnCharacter.SelectedIndex = _conf.CentreOnCharacter;
            MapRangeFrom.SelectedIndex = _conf.MapRangeFrom;

            ShowAlertAge.Checked = _conf.ShowAlertAge;
            ShowAlertAgeSecs.Checked = _conf.ShowAlertAgeSecs;
            ShowReportCount.Checked = _conf.ShowReportCount;
            MaxAlertAge.Value = _conf.MaxAlertAge;
            MaxAlerts.Value = _conf.MaxAlerts;

            if ((_conf.AnomalyMonitorSoundId == -1) && (_conf.AnomalyMonitorSoundPath == string.Empty))
            {
                AnomalyWatcherSound.SelectedIndex = -1;
            }
            else if ((_conf.AnomalyMonitorSoundId == -1) && (_conf.AnomalyMonitorSoundPath != string.Empty))
            {
                AnomalyWatcherSound.Items.RemoveAt(AnomalyWatcherSound.Items.Count - 1);
                AnomalyWatcherSound.Items.Add(_conf.AnomalyMonitorSoundPath);
                AnomalyWatcherSound.SelectedIndex = AnomalyWatcherSound.Items.Count - 1;
                _anomalyWatcherSound = LoadSoundFromFile(_conf.AnomalyMonitorSoundPath);
            }
            else if (_conf.AnomalyMonitorSoundId > -1)
            {
                AnomalyWatcherSound.SelectedIndex = _conf.AnomalyMonitorSoundId;
            }

            if (_conf.SelectedSystems != null)
            {
                int[] loadedSelectedSystems = _conf.SelectedSystems;

                foreach (var loadedSystem in loadedSelectedSystems)
                {
                    _stickyHighlightSystems.Add(loadedSystem);
                }
            }

            PreserveHomeSystem.Checked = _conf.PreserveHomeSystem;
            if (PreserveHomeSystem.Checked)
            {
                if (_conf.HomeSystemId != -1)
                    _solarSystems.SetCurrentHomeSystem(_conf.HomeSystemId);
            }

            PopulateCharacterList();

            _configLoaded = true;
        }

        private void PopulateCharacterList()
        {
            foreach (var name in _characters.Names)
            {
                CharacterList.Items.Add(name);
            }
        }

        private void SetWindowState()
        {
            var temp = new Rectangle(_conf.WindowPositionX, _conf.WindowPositionY, _conf.WindowSizeX, _conf.WindowSizeY);

            if (!SystemInformation.VirtualScreen.IntersectsWith(temp))
            {
                _conf.WindowPositionX = 50;
                _conf.WindowPositionY = 50;
                _conf.WindowSizeX = 1253;
                _conf.WindowSizeY = 815;
                _conf.IsFullScreen = false;
            }

            PreserveWindowPosition.Checked = _conf.PreserveWindowPosition;
            if (PreserveWindowPosition.Checked)
            {
                Left = _conf.WindowPositionX;
                Top = _conf.WindowPositionY;
            }

            PreserveWindowSize.Checked = _conf.PreserveWindowSize;
            if (PreserveWindowSize.Checked)
            {
                Width = _conf.WindowSizeX;
                Height = _conf.WindowSizeY;
            }

            PreserveFullScreenStatus.Checked = _conf.PreserveFullScreenStatus;
            if (PreserveFullScreenStatus.Checked)
                if (_conf.IsFullScreen)
                    ToggleFullScreen();
        }

        private void FinaliseConfig()
        {
            _conf.IsFullScreen = _isFullScreen;

            if (!_conf.IsFullScreen)
            {
                _conf.WindowPositionX = Left;
                _conf.WindowPositionY = Top;
                _conf.WindowSizeX = Width;
                _conf.WindowSizeY = Height;
            }
            else
            {
                _conf.WindowPositionX = _oldPosition.X;
                _conf.WindowPositionY = _oldPosition.Y;
                _conf.WindowSizeX = _oldSize.Width;
                _conf.WindowSizeY = _oldSize.Height;
            }

            if (_conf.PreserveHomeSystem)
                _conf.HomeSystemId = _solarSystems.HomeSystemId;

            if (_conf.PreserveCameraDistance)
                _conf.CameraDistance = _cameraDistance;

            if (_conf.PreserveLookAt)
            {
                _conf.LookAtX = _lookAt.X;
                _conf.LookAtY = _lookAt.Y;
            }

            if (_conf.OverrideLogPath)
            {
                _conf.LogPath = LogPath.Text;
            }
        }

        private void SaveStickySystems()
        {
            int[] temp = new int[_stickyHighlightSystems.Count];
            _stickyHighlightSystems.CopyTo(temp);
            _conf.SelectedSystems = temp;
        }

        private void WriteAlertConfig()
        {
            AlertTrigger[] temp = new AlertTrigger[_alertTriggers.Count];

            _alertTriggers.CopyTo(temp);
            _conf.AlertTriggers = temp;
        }

        private void LoadAlertConfig(bool clearAlertList = true)
        {
            _loadingAlerts = true;

            if (clearAlertList)
                AlertList.Items.Clear();

            _alertTriggers.Clear();
            _triggerSystems.Clear();

            if (_conf.AlertTriggers == null)
            {
                _loadingAlerts = false;
                return;
            }

            foreach (var temp in _conf.AlertTriggers)
            {
                if (temp.SystemId > -1 && temp.RangeTo != RangeAlertType.Character)
                {
                    temp.SystemName = _solarSystems.SolarSystems[temp.SystemId].Name;
                    _triggerSystems.Add(temp.SystemId);
                }

                _alertTriggers.Add(temp);

                if (clearAlertList)
                    AlertList.Items.Add(temp.ToString(), temp.Enabled);
            }

            _loadingAlerts = false;
        }

        private void LoadIgnoreLists()
        {
            if (_conf.IgnoreSystems != null)
            {
                _ignoreSystems = new List<int>(_conf.IgnoreSystems);

                foreach (var tempSystemId in _ignoreSystems)
                {
                    IgnoreSystemList.Items.Add(_solarSystems.SolarSystems[tempSystemId].Name);
                }
            }

            if (_conf.IgnoreStrings != null)
            {
                foreach (var tempString in _conf.IgnoreStrings)
                {
                    _ignoreStrings.Add(new Regex(@"\b" + tempString + @"\b", RegexOptions.Compiled | RegexOptions.IgnoreCase));
                    IgnoreTextList.Items.Add(tempString);
                }
            }
        }

        private void WriteIgnoreLists()
        {
            if (_ignoreSystems.Count > 0)
            {
                int[] tempIgnoreSystems = new int[_ignoreSystems.Count];
                _ignoreSystems.CopyTo(tempIgnoreSystems);
                _conf.IgnoreSystems = tempIgnoreSystems;
            }
            else
            {
                _conf.IgnoreSystems = null;
            }

            if (IgnoreTextList.Items.Count > 0)
            {
                string[] tempIgnoreStrings = new string[IgnoreTextList.Items.Count];
                IgnoreTextList.Items.CopyTo(tempIgnoreStrings, 0);
                _conf.IgnoreStrings = tempIgnoreStrings;
            }
            else
            {
                _conf.IgnoreStrings = null;
            }
        }

        private void PopulateCharacterNameCombos()
        {
            if (_conf.CharacterList.Count > 0)
            {
                RangeAlertCharacter.Items.AddRange(_conf.CharacterList.ToArray());
                RangeAlertCharacter.Invalidate();

                CentreOnCharacter.Items.AddRange(_conf.CharacterList.ToArray());
                CentreOnCharacter.Invalidate();

                MapRangeFrom.Items.AddRange(_conf.CharacterList.ToArray());
                MapRangeFrom.Invalidate();

                foreach (var character in _conf.CharacterList)
                {
                    followMenuItem.DropDownItems.Add(new ToolStripMenuItem(character));
                    mapRangeFromMenuItem.DropDownItems.Add(new ToolStripMenuItem(character));
                    anomalyMonitorMenuItem.DropDownItems.Add(new ToolStripMenuItem(character));
                }
            }            
        }
        #endregion Configuration

        #region Log Processing
        void logFile_ProcessNewData(object sender, ProcessNewDataEventArgs e)
        {
            if (!_hasRendered)
                return;

            LogEntry entry = e.LogEntry;

            if (entry == null || entry.PlayerName == "EVE System") return;


            if (entry.EntryType != LogEntryType.ChatEvent && entry.EntryType != LogEntryType.UnknownChatLogEvent && entry.EntryType != LogEntryType.UnknownGameLogEvent)
            {
                if (!_conf.CharacterList.Contains(entry.CharacterName))
                {
                    AddNewCharacter(entry.CharacterName);
                }
                
                switch (entry.EntryType)
                {
                    case LogEntryType.OpenChatLogEvent:
                        if (_conf.DisplayOpenFileAlerts)
                            WriteIntel(e.LogPrefix, " > Chat log opened: (" + entry.CharacterName + ") " + entry.FileName);
                        return;
                    case LogEntryType.NewChatLogEvent:
                        if (_conf.DisplayNewFileAlerts)
                            WriteIntel(e.LogPrefix, " > New chat log detected: (" + entry.CharacterName + ") " + entry.FileName);
                        return;
                    case LogEntryType.OpenGameLogEvent:
                        if (_conf.DisplayOpenFileAlerts)
                            WriteIntel(e.LogPrefix, " > Game log opened: (" + entry.CharacterName + ") " + entry.FileName);
                        return;
                    case LogEntryType.NewGameLogEvent:
                        if (_conf.DisplayNewFileAlerts)
                            WriteIntel(e.LogPrefix, " > New game log detected: (" + entry.CharacterName + ") " + entry.FileName);
                        return;
                }
                return;
            }

            if (!entry.ParseSuccess) return;

            var matchIds = new HashSet<int>();

            if (!_ignoreStrings.Any(ignoreString => ignoreString.IsMatch(entry.LineContent)))
            {
                if (entry.LogType == LogFileType.Chat)
                {
                    foreach (
                        var tempSystem in
                            _solarSystems.SolarSystems.Values.Where(
                                tempSystem => tempSystem.MatchNameRegex(entry.LineContent))
                                .Where(tempSystem => !_ignoreSystems.Contains(_solarSystems.Names[tempSystem.Name])))
                    {
                        matchIds.Add(_solarSystems.Names[tempSystem.Name]);
                    }

                    foreach (var matchId in matchIds)
                    {
                        // Home system path
                        var fromSystem = _solarSystems.HomeSystemId;
                        var toSystem = matchId;
                        var pathId = _solarSystems.GenerateUniquePathId(fromSystem, toSystem);

                        // Queue the path from home system for pathfinding if needed
                        if (!_solarSystems.PathCache.ContainsKey(pathId))
                            _solarSystems.FindAndCachePath(fromSystem, toSystem);

                        // Character paths
                        var tempLocations = BuildCharacterLocationIndex();

                        foreach (var locationId in tempLocations.Keys)
                        {
                            pathId = _solarSystems.GenerateUniquePathId(locationId, toSystem);

                            // Queue for pathfinding if needed
                            if (!_solarSystems.PathCache.ContainsKey(pathId))
                                _solarSystems.FindAndCachePath(locationId, toSystem);
                        }

                        foreach (
                            var tempTrigger in
                                _alertTriggers.Where(
                                    tempTrigger =>
                                        tempTrigger.Enabled &&
                                        (tempTrigger.RangeTo == RangeAlertType.System ||
                                         tempTrigger.RangeTo == RangeAlertType.Character)))
                        {
                            // Range from system alert
                            if (tempTrigger.RangeTo == RangeAlertType.System)
                            {
                                fromSystem = tempTrigger.SystemId;
                            }
                            else // Character alert
                            {
                                continue;
                            }


                            pathId = _solarSystems.GenerateUniquePathId(fromSystem, toSystem);

                            // Queue the path for pathfinding if needed
                            if (!_solarSystems.PathCache.ContainsKey(pathId))
                                _solarSystems.FindAndCachePath(fromSystem, toSystem);
                        }
                    }

                    // Process ZoomTo and log non-triggering intel if needed
                    if (matchIds.Count > 0)
                    {
                        var zoomToSystem = -1;

                        // Check for any systems in the log entry
                        foreach (var matchId in matchIds)
                        {
                            _solarSystems.AddAlert(matchId);

                            // Set the zoom to system to the final system
                            // in an entry if multiple systems are found
                            zoomToSystem = matchId;
                        }

                        // Check if we need to zoom to a found system based 
                        // on which intel channel the log entry comes from
                        if (CheckZoomTo(entry.LogPrefix))
                            ZoomTo(zoomToSystem);
                    }
                }
            }

            entry.MatchedIds = matchIds;
            _newIntelQueue.Enqueue(entry);
        }

        private bool CheckZoomTo(string logPrefix)
        {
            if (!_conf.CameraFollowCharacter)
            {
                switch (logPrefix)
                {
                    case "GOTG_Intel":
                        if (_conf.AlertGOTG)
                            return true;
                        break;
                    case "brn":
                        if (_conf.AlertBranch)
                            return true;
                        break;
                    case "dek":
                        if (_conf.AlertDeklein)
                            return true;
                        break;
                    case "tnl":
                        if (_conf.AlertTenal)
                            return true;
                        break;
                    case "vnl":
                        if (_conf.AlertVenal)
                            return true;
                        break;
                    case "fade":
                        if (_conf.AlertFade)
                            return true;
                        break;
                    case "pb":
                        if (_conf.AlertPureBlind)
                            return true;
                        break;
                    case "tri":
                        if (_conf.AlertTribute)
                            return true;
                        break;
                    case "vale":
                        if (_conf.AlertVale)
                            return true;
                        break;
                    case "provi":
                        if (_conf.AlertProvidence)
                            return true;
                        break;
                    case "delve":
                        if (_conf.AlertDelve)
                            return true;
                        break;
                    default:
                        return false;
                }
            }
            return false;
        }

        private void PlayTriggerSound(AlertTrigger tempTrigger)
        {
            if (_muteSound) return;

            // If the trigger sound is a custom sound
            if (tempTrigger.SoundId == -1)
            {
                // Load and play the sound
                SoundPlayer temp = LoadSoundFromFile(tempTrigger.SoundPath);
                temp.Play();
                temp.Dispose();
            }
            else // If the trigger sound is a built in sound
            {
                // Play the built in sound
                _sounds[tempTrigger.SoundPath].Play();
            }
        }

        public string ShortPrefix(string prefix)
        {
            switch (prefix)
            {
                case "fade":
                    return "fde";
                case "pb":
                    return "pbd";
                case "vale":
                    return "vle";
                case "provi":
                    return "prv";
                case "delve":
                    return "del";
                default:
                    return prefix;
            }
        }

        private int lineCountCombinesIntel = 0;

        public void WriteIntel(string prefix, string logLine, bool parseForSystemLinks = false, bool writingBuffer = false)
        {
            if (_bufferIntel && !writingBuffer)
            {
                var tempIntelBuffer = new IntelBuffer
                {
                    Prefix = prefix,
                    LogLine = logLine,
                    ParseForSystemLinks = parseForSystemLinks
                };

                _bufferedIntel.Enqueue(tempIntelBuffer);

                BufferingIndicator.Text = "Buffered Intel: " + _bufferedIntel.Count + " line";
                if (_bufferedIntel.Count > 1) BufferingIndicator.Text += "s";

                return;
            }

            const int logLineMaxLength = 100;

            // Trim the displayed line to the maximum line length if needed
            if (logLine.Length > logLineMaxLength)
                logLine = logLine.Substring(0, logLineMaxLength) + "...";

            var outputLine = ShortPrefix(prefix) + logLine + Environment.NewLine;

            // Write the line to the combined intel pane
            CombinedIntel.DeselectAll();
            CombinedIntel.SelectionStart = 0;
            CombinedIntel.SelectedText = outputLine;
            lineCountCombinesIntel++;

            // Link any systems in the new log line if needed
            if (parseForSystemLinks)
            {
                foreach (
                    var tempSystem in
                        _solarSystems.SolarSystems.Values.Where(
                            tempSystem => tempSystem.MatchNameRegex(logLine))
                            .Where(tempSystem => !_ignoreSystems.Contains(_solarSystems.Names[tempSystem.Name])))
                {
                    var linkPos = CombinedIntel.Find(tempSystem.Name);
                    CombinedIntel.InsertLink(tempSystem.Name, linkPos);
                }
            }

            // Link any names we know about
            foreach (var name in _characters.Names)
            {
                var nameStart = CombinedIntel.Text.IndexOf(name, 0, outputLine.Length - 1);
                if (nameStart >= 0)
                    CombinedIntel.InsertLink(name, nameStart);
            }

            // Write the line to the require channel pane
            switch (prefix)
            {
                case "GOTG_Intel":
                    GOTGIntel.Text = outputLine + GOTGIntel.Text;
                    break;
                case "brn":
                    BranchIntel.Text = outputLine + BranchIntel.Text;
                    break;
                case "dek":
                    DekleinIntel.Text = outputLine + DekleinIntel.Text;
                    break;
                case "tnl":
                    TenalIntel.Text = outputLine + TenalIntel.Text;
                    break;
                case "vnl":
                    VenalIntel.Text = outputLine + VenalIntel.Text;
                    break;
                case "fade":
                    FadeIntel.Text = outputLine + FadeIntel.Text;
                    break;
                case "pb":
                    PureBlindIntel.Text = outputLine + PureBlindIntel.Text;
                    break;
                case "tri":
                    TributeIntel.Text = outputLine + TributeIntel.Text;
                    break;
                case "vale":
                    ValeIntel.Text = outputLine + ValeIntel.Text;
                    break;
                case "provi":
                    ProvidenceIntel.Text = outputLine + ProvidenceIntel.Text;
                    break;
                case "delve":
                    DelveIntel.Text = outputLine + DelveIntel.Text;
                    break;
            }



            // Trim all intel panes to a maximum length
            const int maxTextBoxLength = 10000;
            const int maxIntelLines = 100;

            while (lineCountCombinesIntel > maxIntelLines)
            {
                var lastLineStart = CombinedIntel.Text.LastIndexOf("\n", CombinedIntel.Text.Length - 2) + 1;

                // Select the last line and delete it
                CombinedIntel.Select(lastLineStart, CombinedIntel.Text.Length - lastLineStart);
                CombinedIntel.ReadOnly = false;
                CombinedIntel.SelectedText = string.Empty;
                CombinedIntel.ReadOnly = true;
                CombinedIntel.SelectionStart = 0;
                lineCountCombinesIntel--;
            }
            if (GOTGIntel.Text.Length > maxTextBoxLength)
                GOTGIntel.Text = GOTGIntel.Text.Substring(0, maxTextBoxLength);

            if (BranchIntel.Text.Length > maxTextBoxLength)
                BranchIntel.Text = BranchIntel.Text.Substring(0, maxTextBoxLength);

            if (DekleinIntel.Text.Length > maxTextBoxLength)
                DekleinIntel.Text = DekleinIntel.Text.Substring(0, maxTextBoxLength);

            if (TenalIntel.Text.Length > maxTextBoxLength)
                TenalIntel.Text = TenalIntel.Text.Substring(0, maxTextBoxLength);

            if (VenalIntel.Text.Length > maxTextBoxLength)
                VenalIntel.Text = VenalIntel.Text.Substring(0, maxTextBoxLength);

            if (FadeIntel.Text.Length > maxTextBoxLength)
                FadeIntel.Text = FadeIntel.Text.Substring(0, maxTextBoxLength);

            if (PureBlindIntel.Text.Length > maxTextBoxLength)
                PureBlindIntel.Text = PureBlindIntel.Text.Substring(0, maxTextBoxLength);

            if (TributeIntel.Text.Length > maxTextBoxLength)
                TributeIntel.Text = TributeIntel.Text.Substring(0, maxTextBoxLength);

            if (ValeIntel.Text.Length > maxTextBoxLength)
                ValeIntel.Text = ValeIntel.Text.Substring(0, maxTextBoxLength);

            if (ProvidenceIntel.Text.Length > maxTextBoxLength)
                ProvidenceIntel.Text = ProvidenceIntel.Text.Substring(0, maxTextBoxLength);

            if (DelveIntel.Text.Length > maxTextBoxLength)
                DelveIntel.Text = DelveIntel.Text.Substring(0, maxTextBoxLength);

            CombinedIntel.SelectionStart = 0;
        }

        private void IntelUpdateTicker_Tick(object sender, EventArgs e)
        {
            // If there's nothing in the queue or we're waiting on paths to be found,
            // add any new log entries to the processing queue and stop further processing
            if ((_intelProcessingQueue.Count <= 0) || (_solarSystems.IsProcessingPaths))
            {
                while (_newIntelQueue.Count > 0)
                {
                    _intelProcessingQueue.Enqueue(_newIntelQueue.Dequeue());
                }

                return;
            }

            // Stop the update timer so it doesn't tick while we're processing
            IntelUpdateTicker.Enabled = false;

            while (_intelProcessingQueue.Count > 0)
            {
                // Dequeue the first item to process
                var entry = _intelProcessingQueue.Dequeue();

                // Write the intel to the text panels if chatlog entry
                if (entry.LogType == LogFileType.Chat)
                {
                    var jumpDisplay = "--";
                    var containsSystem = false;

                    // Get the jump range from home system to first matched system id
                    if (entry.MatchedIds.Count > 0)
                    {
                        var pathId = _solarSystems.GenerateUniquePathId(_solarSystems.HomeSystemId, entry.MatchedIds.First());
                        jumpDisplay = (_solarSystems.PathFindingCache[pathId].TotalJumps - 1).ToString().PadLeft(2, '0');
                        containsSystem = true;
                    }

                    // Write the intel to the text panels
                    WriteIntel(entry.LogPrefix, "-" + entry.LogTime + "|" + jumpDisplay + "| " + entry.PlayerName + " > " + entry.LineContent, containsSystem);
                }

                // Skip any further processing of the line if it contains an ignore string
                if (_ignoreStrings.Any(ignoreString => ignoreString.IsMatch(entry.LineContent))) continue;

                // Loop through each alert trigger
                foreach (var alertTrigger in _alertTriggers.Where(alertTrigger => alertTrigger.Enabled))
                {
                    var alertTriggered = false;

                    if (alertTrigger.Type == AlertType.Ranged)
                    {
                        // Loop through each matched system
                        foreach (var matchedId in entry.MatchedIds)
                        {
                            // Setup necessary variables for trigger processing
                            var fromSystem = new[] {-1};
                            var toSystem = matchedId;

                            if (alertTrigger.RangeTo == RangeAlertType.Home)
                            {
                                fromSystem = new[] { _solarSystems.HomeSystemId };
                            }
                            else if (alertTrigger.RangeTo == RangeAlertType.System)
                            {
                                fromSystem = new[] { alertTrigger.SystemId };
                            }
                            else if (alertTrigger.RangeTo == RangeAlertType.Character)
                            {
                                if (_followingChars && _charLocations.ContainsKey(alertTrigger.CharacterName))
                                {
                                    fromSystem = new[] { _charLocations[alertTrigger.CharacterName] };
                                }
                                else
                                {
                                    continue;
                                }                                
                            }
                            else if (alertTrigger.RangeTo == RangeAlertType.AnyCharacter && _followingChars)
                            {
                                fromSystem = _charLocations.Values.Distinct().ToArray();
                            }

                            foreach (var system in fromSystem)
                            {
                                var pathId = _solarSystems.GenerateUniquePathId(system, toSystem);
                                if (_solarSystems.PathFindingCache.ContainsKey(pathId))
                                {
                                    var jumpCount = _solarSystems.PathFindingCache[pathId].TotalJumps - 1;

                                    // Process the trigger and break if alert has been triggered
                                    alertTriggered = ProcessRangeAlertTrigger(alertTrigger, entry, jumpCount);
                                }
                                else
                                {
                                    WriteIntel("sys", "-" + "|!!| Path not found: " + pathId + " Alert: " + alertTrigger);
                                }

                                if (alertTriggered)
                                    break;
                            }
                        }

                        // Stop further alert processing if an alert was triggered
                        if (alertTriggered)
                            break;
                    }
                    else // Check custom text alerts
                    {
                        if (entry.LineContent.Contains(alertTrigger.Text) && (DateTime.Now - alertTrigger.TriggerTime).TotalSeconds > alertTrigger.RepeatInterval)
                        {
                            WriteIntel(ShortPrefix(entry.LogPrefix), "-" + entry.LogTime + "> Custom Alert Match: " + alertTrigger.Text);
                            PlayTriggerSound(alertTrigger);
                            alertTrigger.TriggerTime = DateTime.Now;
                            break;
                        }
                    }
                }
            }

            // Restart the update timer
            IntelUpdateTicker.Enabled = true;
        }

        private bool ProcessRangeAlertTrigger(AlertTrigger alertTrigger, LogEntry entry, int jumpCount)
        {
            if (alertTrigger.UpperLimitOperator == RangeAlertOperator.Equal)
            {
                if (alertTrigger.UpperRange != jumpCount) return false;

                PlayTriggerSound(alertTrigger);
                WriteIntel(entry.LogPrefix, "-" + entry.LogTime + "|♦♦| " + alertTrigger);
                return true;
            }

            if (jumpCount > alertTrigger.UpperRange) return false;

            if (alertTrigger.LowerLimitOperator == RangeAlertOperator.GreaterThanOrEqual)
            {
                if (jumpCount < alertTrigger.LowerRange) return false;

                PlayTriggerSound(alertTrigger);
                WriteIntel(entry.LogPrefix, "-" + entry.LogTime + "|♦♦| " + alertTrigger);
                return true;
            }

            if (jumpCount <= alertTrigger.LowerRange) return false;

            PlayTriggerSound(alertTrigger);
            WriteIntel(entry.LogPrefix, "-" + entry.LogTime + "|♦♦| " + alertTrigger);
            return true;
        }
        #endregion Log Processing

        #region VBO init and refresh
        private bool InitVboContent()
        {
            GL.GenBuffers(1, out _vbOs.SystemVbo);
            GL.GenBuffers(1, out _vbOs.SystemVao);
            GL.GenBuffers(1, out _vbOs.ColorVao);
            GL.GenBuffers(1, out _vbOs.ConnectionVbo);
            GL.GenBuffers(1, out _vbOs.ConnectionVao);
            GL.GenBuffers(1, out _vbOs.ConnectionColor);

            GL.GenBuffers(1, out _vbOs.TextQuadVbo);
            GL.GenBuffers(1, out _vbOs.TextQuadVao);
            GL.GenBuffers(1, out _vbOs.TextQuadColor);

            GL.GenBuffers(1, out _vbOs.TextLineVbo);
            GL.GenBuffers(1, out _vbOs.TextLineVao);
            GL.GenBuffers(1, out _vbOs.TextLineColor);

            _solarSystems.IsSystemVboDirty = !UploadSystemVbo(false);
            _solarSystems.IsSystemVaoDirty = !UploadSystemVao(false);
            _solarSystems.IsColorVaoDirty = !UploadColorVao(false);
            _solarSystems.IsConnectionVboDirty = !UploadConnectionVbo(false);
            _solarSystems.IsConnectionVaoDirty = !UploadConnectionVao(false);
            _solarSystems.IsConnectionColorVaoDirty = !UploadConnectionColorVao(false);

            return _vbOs.AllVbOsGenerated;
        }

        private bool RefreshVboContent()
        {
            if (_solarSystems.IsSystemVboDirty)
                _solarSystems.IsSystemVboDirty = !UploadSystemVbo();

            if (_solarSystems.IsSystemVaoDirty)
                _solarSystems.IsSystemVaoDirty = !UploadSystemVao();

            if (_solarSystems.IsColorVaoDirty)
                _solarSystems.IsColorVaoDirty = !UploadColorVao();

            if (_solarSystems.IsConnectionVboDirty)
                _solarSystems.IsConnectionVboDirty = !UploadConnectionVbo();

            if (_solarSystems.IsConnectionVaoDirty)
                _solarSystems.IsConnectionVaoDirty = !UploadConnectionVao();

            if (_solarSystems.IsConnectionColorVaoDirty)
                _solarSystems.IsConnectionColorVaoDirty = !UploadConnectionColorVao();

            return _solarSystems.AllVbOsClean;
        }
        #endregion VBO init and refresh

        #region VBO Uploaders

        #region Systems
        private bool UploadSystemVbo(bool refresh = true)
        {
            if (refresh)
            {
                GL.DeleteBuffer(_vbOs.SystemVbo);
                GL.GenBuffers(1, out _vbOs.SystemVbo);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.SystemVbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector3.SizeInBytes * _solarSystems.SystemCount), _solarSystems.SystemVboContent, BufferUsageHint.DynamicDraw);

            int bufferSize;

            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return _solarSystems.SystemCount * Vector3.SizeInBytes == bufferSize;
        }

        private bool UploadSystemVao(bool refresh = true)
        {
            if (refresh)
            {
                GL.DeleteBuffer(_vbOs.SystemVao);
                GL.GenBuffers(1, out _vbOs.SystemVao);
            }

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vbOs.SystemVao);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(int) * _solarSystems.SystemCount), _solarSystems.SystemElementVaoContent, BufferUsageHint.DynamicDraw);

            int bufferSize;

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return (sizeof(int) * _solarSystems.SystemCount) == bufferSize;
        }

        private bool UploadColorVao(bool refresh = true)
        {
            if (refresh)
            {
                GL.DeleteBuffer(_vbOs.ColorVao);
                GL.GenBuffers(1, out _vbOs.ColorVao);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.ColorVao);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(int) * _solarSystems.SystemCount), _solarSystems.SystemColorVaoContent, BufferUsageHint.DynamicDraw);

            int bufferSize;

            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return (sizeof(int) * _solarSystems.SystemCount) == bufferSize;

        }
        #endregion Systems

        #region Connections
        private bool UploadConnectionVbo(bool refresh = true)
        {
            if (refresh)
            {
                GL.DeleteBuffer(_vbOs.ConnectionVbo);
                GL.GenBuffers(1, out _vbOs.ConnectionVbo);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.ConnectionVbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector3.SizeInBytes * _solarSystems.ConnectionVertexCount), _solarSystems.ConnectionVboContent, BufferUsageHint.DynamicDraw);

            int bufferSize;

            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return _solarSystems.ConnectionVertexCount * Vector3.SizeInBytes == bufferSize;
        }

        private bool UploadConnectionVao(bool refresh = true)
        {
            if (refresh)
            {
                GL.DeleteBuffer(_vbOs.ConnectionVao);
                GL.GenBuffers(1, out _vbOs.ConnectionVao);
            }

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vbOs.ConnectionVao);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(int) * _solarSystems.ConnectionVertexCount), _solarSystems.ConnectionVaoContent, BufferUsageHint.DynamicDraw);

            int bufferSize;

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return (sizeof(int) * _solarSystems.ConnectionVertexCount) == bufferSize;
        }

        private bool UploadConnectionColorVao(bool refresh = true)
        {
            if (refresh)
            {
                GL.DeleteBuffer(_vbOs.ConnectionColor);
                GL.GenBuffers(1, out _vbOs.ConnectionColor);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.ConnectionColor);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector4.SizeInBytes * _solarSystems.ConnectionVertexCount), _solarSystems.ConnectionColorVaoContent, BufferUsageHint.DynamicDraw);

            int bufferSize;

            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return (Vector4.SizeInBytes * _solarSystems.ConnectionVertexCount) == bufferSize;
        }
        #endregion Connections

        #endregion VBO Uploaders

        #region Shaders
        public bool InitShaders()
        {
            string vs = "";
            string fs = "";
            Bitmap bmp = null;

            string vsc = "";
            string fsc = "";

            string vsch = "";
            string fsch = "";

            var assembly = Assembly.GetExecutingAssembly();

            var result = true;

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.Shaders.shader.vert"))
            {
                if (file != null)
                    using (var reader = new StreamReader(file))
                    {
                        try
                        {
                            vs = reader.ReadToEnd();
                        }
                        catch (Exception)
                        {
                            result = false;
                        }
                    }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.Shaders.shader.frag"))
            {
                if (file != null)
                    using (var reader = new StreamReader(file))
                    {
                        try
                        {
                            fs = reader.ReadToEnd();
                        }
                        catch (Exception)
                        {
                            result = false;
                        }
                    }
            }


            using (var file = assembly.GetManifestResourceStream("Taco.Resources.Shaders.connection.vert"))
            {
                if (file != null)
                    using (var reader = new StreamReader(file))
                    {
                        try
                        {
                            vsc = reader.ReadToEnd();
                        }
                        catch (Exception)
                        {
                            result = false;
                        }
                    }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.Shaders.connection.frag"))
            {
                if (file != null)
                    using (var reader = new StreamReader(file))
                    {
                        try
                        {
                            fsc = reader.ReadToEnd();
                        }
                        catch (Exception)
                        {
                            result = false;
                        }
                    }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.Shaders.crosshair.vert"))
            {
                if (file != null)
                    using (var reader = new StreamReader(file))
                    {
                        try
                        {
                            vsch = reader.ReadToEnd();
                        }
                        catch (Exception)
                        {
                            result = false;
                        }
                    }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.Shaders.crosshair.frag"))
            {
                if (file != null)
                    using (var reader = new StreamReader(file))
                    {
                        try
                        {
                            fsch = reader.ReadToEnd();
                        }
                        catch (Exception)
                        {
                            result = false;
                        }
                    }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.TexturesOther.system.png"))
            {
                try
                {
                    if (file != null) bmp = (Bitmap)Image.FromStream(file);
                    CreateTexture(out _texSystem, bmp);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.TexturesCrosshairs.green-crosshair.png"))
            {
                try
                {
                    if (file != null) bmp = (Bitmap)Image.FromStream(file);
                    CreateTexture(out _texGreenCh, bmp);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.TexturesCrosshairs.red-crosshair.png"))
            {
                try
                {
                    if (file != null) bmp = (Bitmap)Image.FromStream(file);
                    CreateTexture(out _texRedCh, bmp);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.TexturesCrosshairs.yellow-crosshair.png"))
            {
                try
                {
                    if (file != null) bmp = (Bitmap)Image.FromStream(file);
                    CreateTexture(out _texYellowCh, bmp);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.TexturesCrosshairs.redgreen-crosshair.png"))
            {
                try
                {
                    if (file != null) bmp = (Bitmap)Image.FromStream(file);
                    CreateTexture(out _texRedGreenCh, bmp);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.TexturesCrosshairs.redyellow-crosshair.png"))
            {
                try
                {
                    if (file != null) bmp = (Bitmap)Image.FromStream(file);
                    CreateTexture(out _texRedYellowCh, bmp);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.TexturesCrosshairs.yellowgreen-crosshair.png"))
            {
                try
                {
                    if (file != null) bmp = (Bitmap)Image.FromStream(file);
                    CreateTexture(out _texYellowGreenCh, bmp);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            _shader = new Shader(vs, fs);

            _shaderConn = new Shader(vsc, fsc);

            _shaderCrosshair = new Shader(vsch, fsch);

            _shadersLoaded = true;

            return result;
        }
        #endregion Shaders

        #region glOut Events and Setup
        private void glOut_Load(object sender, EventArgs e)
        {
            if (!CheckOpenGL())
            {
                MessageBox.Show("Looks like you're hardware or drivers don't support OpenGL 3.3+. Exiting.",
                    "OpenGL Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }

            _solarSystems.InitVboData();
            SetupVariables();
            SetupViewport();
            InitVboContent();
            InitShaders();
            SetupQFont();

            _glLoaded = true;
        }

        private void glOut_Paint(object sender, PaintEventArgs e)
        {
            if (!_glLoaded)
                return;

            BeginRender();
            RenderConnections();
            RenderSystems();
            RenderHud();
            EndRender();

            _hasRendered = true;
        }

        private void SetupViewport()
        {
            if (!_glLoaded)
                return;

            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.Blend);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


            GL.LoadIdentity();

            _w = glOut.Width;
            _h = glOut.Height;
            GL.Viewport(0, 0, _w, _h);
        }

        private void glOut_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        _dragging = true;

                        if (_zooming)
                            StopZoom();

                        _startX = e.X;
                        _startY = e.Y;

                        _dragProjection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)_w / _h, 1f, _cameraDistance);
                        _dragModelView = _matrixModelview;

                        var foundId = -1;
                        foreach (KeyValuePair<int, SolarSystem> tempSolarSystem in from tempSolarSystem in _solarSystems.SolarSystems let tempProjection = Project(tempSolarSystem.Value.Xyz, _matrixModelview, _matrixProjection, _w, _h) where IntersectsMouseCursor(tempProjection, e.X, e.Y, 5) select tempSolarSystem)
                        {
                            foundId = tempSolarSystem.Key;
                            break;
                        }

                        if (foundId < 0) return;
                        _solarSystems.AddHighlight(foundId);
                        _isHighlighting = true;
                        _currentHighlight = foundId;
                        if (!Ticker.Enabled)
                            Ticker.Enabled = true;

                        if (e.Clicks == 2 && _isHighlighting)
                        {
                            ZoomTo(_currentHighlight);
                        }

                        if (e.Clicks == 1 && _isHighlighting)
                        {
                            if (_stickyHighlightSystems.Contains(_currentHighlight))
                                _stickyHighlightSystems.Remove(_currentHighlight);
                            else
                                _stickyHighlightSystems.Add(_currentHighlight);

                            SaveStickySystems();
                        }
                    }
                    break;
                case MouseButtons.Right:
                    {
                        var foundId = -1;
                        foreach (KeyValuePair<int, SolarSystem> tempSolarSystem in from tempSolarSystem in _solarSystems.SolarSystems let tempProjection = Project(tempSolarSystem.Value.Xyz, _matrixModelview, _matrixProjection, _w, _h) where IntersectsMouseCursor(tempProjection, e.X, e.Y, 5) select tempSolarSystem)
                        {
                            foundId = tempSolarSystem.Key;
                            break;
                        }

                        if (foundId < 0)
                        {
                            MenuStrip.Show(glOut, new Point(e.X, e.Y));
                        }
                        else
                        {
                            if (_solarSystems.HomeSystemId != foundId)
                                _solarSystems.SetCurrentHomeSystem(foundId);
                            else if (_solarSystems.HomeSystemId == foundId)
                                _solarSystems.ClearCurrentSystem();
                        }

                        if (!Ticker.Enabled)
                            Ticker.Enabled = true;
                    }
                    break;
            }
        }

        private void glOut_MouseUp(object sender, MouseEventArgs e)
        {
            //if ((e.Button != MouseButtons.Right) || !_dragging) return;

            _dragging = false;

            _dX = 0;
            _dY = 0;
        }

        private bool IntersectsMouseCursor(Vector2 point, int mouseX, int mouseY, int radius)
        {
            //(x - center_x)^2 + (y - center_y)^2 < radius^2

            return Math.Pow(mouseX - point.X, 2) + Math.Pow(mouseY - point.Y, 2) <= Math.Pow(radius, 2);
        }

        private void glOut_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_hasRendered)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (_dragging)
                {
                    _dX = e.X - _startX;
                    _dY = e.Y - _startY;

                    var ray = new MouseRay((_w / 2) - _dX, (_h / 2) - _dY, _dragModelView, _dragProjection);

                    _lookAt.X = ray.End.X;
                    _lookAt.Y = ray.End.Y;

                    glOut.Invalidate();
                }
            }
            else
            {
                var foundId = -1;

                foreach (var tempSolarSystem in from tempSolarSystem in _solarSystems.SolarSystems let tempProjection = Project(tempSolarSystem.Value.Xyz, _matrixModelview, _matrixProjection, _w, _h) where IntersectsMouseCursor(tempProjection, e.X, e.Y, 5) select tempSolarSystem)
                {
                    foundId = tempSolarSystem.Key;
                    break;
                }

                if ((!_isHighlighting) && (_currentHighlight == foundId) && (foundId != -1))
                {
                    _isHighlighting = true;
                }
                else if ((_isHighlighting) && (_currentHighlight != foundId))
                {
                    _isHighlighting = false;
                    _highlightTick = 0;
                    _solarSystems.RemoveHighlight(_currentHighlight);
                }
                else if ((!_isHighlighting) && (foundId != -1))
                {
                    _currentHighlight = foundId;
                }
            }
        }

        void glOut_MouseWheel(object sender, MouseEventArgs e)
        {
            const int cameraDivisor = 1000;
            if (e.Delta > 0)
            {
                _cameraDistance += (-50 * (_cameraDistance / cameraDivisor));
            }
            else
            {
                _cameraDistance += (50 * (_cameraDistance / cameraDivisor));
            }

            if (_cameraDistance > 12100)
                _cameraDistance = 12100f;

            if (_cameraDistance < 50.0f)
                _cameraDistance = 50.0f;

            _pSize = CalcPointSize();

            glOut.Invalidate();
        }

        private void glOut_Resize(object sender, EventArgs e)
        {
            if (!_glLoaded)
                return;

            SetupViewport();
        }
        #endregion glOut Events and Setup

        #region GL Setup
        public string Analyze(GetPName pname, eType type)
        {
            bool result1b;
            int result1i;
            int[] result2i = new int[2];
            int[] result4i = new int[4];
            float result1f;
            Vector2 result2f;
            Vector4 result4f;
            string output;

            switch (type)
            {
                case eType.Boolean:
                    GL.GetBoolean(pname, out result1b);
                    output = pname + ": " + result1b;
                    break;
                case eType.Int:
                    GL.GetInteger(pname, out result1i);
                    output = pname + ": " + result1i;
                    break;
                case eType.IntEnum:
                    GL.GetInteger(pname, out result1i);
                    output = pname + ": " + (All)result1i;
                    break;
                case eType.IntArray2:
                    GL.GetInteger(pname, result2i);
                    output = pname + ": ( " + result2i[0] + ", " + result2i[1] + " )";
                    break;
                case eType.IntArray4:
                    GL.GetInteger(pname, result4i);
                    output = pname + ": ( " + result4i[0] + ", " + result4i[1] + " ) ( " + result4i[2] + ", " + result4i[3] + " )";
                    break;
                case eType.Float:
                    GL.GetFloat(pname, out result1f);
                    output = pname + ": " + result1f;
                    break;
                case eType.FloatArray2:
                    GL.GetFloat(pname, out result2f);
                    output = pname + ": ( " + result2f.X + ", " + result2f.Y + " )";
                    break;
                case eType.FloatArray4:
                    GL.GetFloat(pname, out result4f);
                    output = pname + ": ( " + result4f.X + ", " + result4f.Y + ", " + result4f.Z + ", " + result4f.W + " )";
                    break;
                default: throw new NotImplementedException();
            }

            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                return "Unsupported Token: " + pname;

            return output;
        }

        private bool CheckOpenGL()
        {
            if (File.Exists(Application.StartupPath + @"\taco-gldiag.txt"))
            {
                using (var diagWrite = new StreamWriter(Application.StartupPath + @"\taco-gldiag.txt"))
                {
                    var Renderer = GL.GetString(StringName.Renderer);
                    var GLSLang = GL.GetString(StringName.ShadingLanguageVersion);
                    var Vendor = GL.GetString(StringName.Vendor);
                    var Version = GL.GetString(StringName.Version);

                    var ExtensionsRaw = GL.GetString(StringName.Extensions);
                    var splitter = new string[] {" "};
                    var Extensions = ExtensionsRaw.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

                    diagWrite.WriteLine("===================================[ Main Info ]");
                    diagWrite.WriteLine("TACO Version: v0.6.0b");
                    diagWrite.WriteLine("Vendor: " + Vendor);
                    diagWrite.WriteLine("Renderer: " + Renderer);
                    diagWrite.WriteLine("GL Version: " + Version);
                    diagWrite.WriteLine("GLSL Version: " + GLSLang);
                    diagWrite.WriteLine(Environment.NewLine);

                    diagWrite.WriteLine("===================================[ Extensions ]");
                    foreach (var extension in Extensions)
                        diagWrite.WriteLine(extension);
                    diagWrite.WriteLine(Environment.NewLine);

                    diagWrite.WriteLine("===================================[ Framebuffer ]");
                    diagWrite.WriteLine(Analyze(GetPName.Doublebuffer, eType.Boolean));
                    diagWrite.WriteLine(Analyze(GetPName.MaxColorAttachments, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxDrawBuffers, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.AuxBuffers, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.DrawBuffer, eType.IntEnum));
                    diagWrite.WriteLine(Analyze(GetPName.MaxSamples, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxViewportDims, eType.IntArray2));
                    diagWrite.WriteLine(Analyze(GetPName.Viewport, eType.IntArray4));
                    diagWrite.WriteLine(Environment.NewLine);

                    diagWrite.WriteLine("===================================[ Framebuffer channels ]");
                    diagWrite.WriteLine(Analyze(GetPName.RedBits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.GreenBits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.BlueBits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.AlphaBits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.DepthBits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.StencilBits, eType.Int));

                    diagWrite.WriteLine(Analyze(GetPName.AccumRedBits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.AccumGreenBits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.AccumBlueBits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.AccumAlphaBits, eType.Int));
                    diagWrite.WriteLine(Environment.NewLine);

                    diagWrite.WriteLine("===================================[ Textures ]");
                    diagWrite.WriteLine(Analyze(GetPName.MaxCombinedTextureImageUnits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxVertexTextureImageUnits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxTextureImageUnits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxTextureUnits, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxTextureSize, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.Max3DTextureSize, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxCubeMapTextureSize, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxRenderbufferSize, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxTextureLodBias, eType.Int));
                    diagWrite.WriteLine(Environment.NewLine);

                    diagWrite.WriteLine("===================================[ Point&Line volumes ]");
                    diagWrite.WriteLine(Analyze(GetPName.AliasedPointSizeRange, eType.FloatArray2));
                    diagWrite.WriteLine(Analyze(GetPName.PointSizeMin, eType.Float));
                    diagWrite.WriteLine(Analyze(GetPName.PointSizeMax, eType.Float));
                    diagWrite.WriteLine(Analyze(GetPName.PointSizeGranularity, eType.Float));
                    diagWrite.WriteLine(Analyze(GetPName.PointSizeRange, eType.FloatArray2));

                    diagWrite.WriteLine(Analyze(GetPName.AliasedLineWidthRange, eType.FloatArray2));
                    diagWrite.WriteLine(Analyze(GetPName.LineWidthGranularity, eType.Float));
                    diagWrite.WriteLine(Analyze(GetPName.LineWidthRange, eType.FloatArray2));
                    diagWrite.WriteLine(Environment.NewLine);

                    diagWrite.WriteLine("===================================[ VBO ]");
                    diagWrite.WriteLine(Analyze(GetPName.MaxElementsIndices, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxElementsVertices, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxVertexAttribs, eType.Int));
                    diagWrite.WriteLine(Environment.NewLine);

                    diagWrite.WriteLine("===================================[ GLSL ]");
                    diagWrite.WriteLine(Analyze(GetPName.MaxCombinedFragmentUniformComponents, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxCombinedGeometryUniformComponents, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxCombinedVertexUniformComponents, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxFragmentUniformComponents, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxVertexUniformComponents, eType.Int));

                    diagWrite.WriteLine(Analyze(GetPName.MaxCombinedUniformBlocks, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxFragmentUniformBlocks, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxGeometryUniformBlocks, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxVertexUniformBlocks, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxUniformBlockSize, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxUniformBufferBindings, eType.Int));

                    diagWrite.WriteLine(Analyze(GetPName.MaxVaryingFloats, eType.Int));
                    diagWrite.WriteLine(Environment.NewLine);

                    diagWrite.WriteLine("===================================[ Transform Feedback ]");
                    diagWrite.WriteLine(Analyze(GetPName.MaxTransformFeedbackInterleavedComponents, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxTransformFeedbackSeparateAttribs, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxTransformFeedbackSeparateComponents, eType.Int));
                    diagWrite.WriteLine(Environment.NewLine);

                    diagWrite.WriteLine("===================================[ Fixed-Func Stacks ]");
                    diagWrite.WriteLine(Analyze(GetPName.MaxClientAttribStackDepth, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxAttribStackDepth, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxProjectionStackDepth, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxModelviewStackDepth, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxTextureStackDepth, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxNameStackDepth, eType.Int));
                    diagWrite.WriteLine(Environment.NewLine);

                    diagWrite.WriteLine("===================================[ Fixed-Func misc. stuff ]");
                    diagWrite.WriteLine(Analyze(GetPName.MaxEvalOrder, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxClipPlanes, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxArrayTextureLayers, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxListNesting, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxLights, eType.Int));
                    diagWrite.WriteLine(Analyze(GetPName.MaxTextureCoords, eType.Int));
                }
            }

            var majorVersion = GL.GetInteger(GetPName.MajorVersion);
            var minorVersion = GL.GetInteger(GetPName.MinorVersion);

            if (majorVersion > 3)
            {
                return true;
            }
            
            return majorVersion == 3 && minorVersion >= 3;
        }


        private void SetupVariables()
        {
            _w = glOut.Width;
            _h = glOut.Height;

            _cameraDistance =  _conf.CameraDistance;

            if (_cameraDistance < 50)
                _cameraDistance = 50.0f;

            if (_conf.PreserveLookAt)
            {
                var lookAtX = _conf.LookAtX;
                var lookAtY = _conf.LookAtY;

                _lookAt = new Vector3(lookAtX, lookAtY, 0f);
                _eye = new Vector3(_lookAt.X, _lookAt.Y, _cameraDistance);
            }
            else
            {
                _lookAt = new Vector3(0f, 0f, 0f);
                _eye = new Vector3(0f, 0f, _cameraDistance);
            }

            _pSize = CalcPointSize();

        }
        #endregion GL Setup

        #region Rendering
        private void RenderSystems()
        {
            if (_shadersLoaded)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.PointSprite);
                GL.Enable(EnableCap.ProgramPointSize);

                _shader.BindTexture(_texSystem, TextureUnit.Texture0, "tex");
                _shader.SetVariable("projection", _matrixProjection);
                _shader.SetVariable("modelView", _matrixModelview);
                _shader.SetVariable("hlpoints", _solarSystems.UniformSystems);
                _shader.SetVariable("hlsizes", _solarSystems.UniformSizes);
                _shader.SetVariable("hlcolors", _solarSystems.UniformColors);
                _shader.SetVariable("pointsize", _pSize);
                _shader.SetVariable("ncolor", SolarSystem.DefaultDrawColor);

                Shader.Bind(_shader);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.ColorVao);
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, sizeof(int), IntPtr.Zero);
            GL.EnableClientState(ArrayCap.ColorArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.SystemVbo);
            GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, IntPtr.Zero);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vbOs.SystemVao);

            GL.EnableClientState(ArrayCap.VertexArray);

            GL.DrawElements(PrimitiveType.Points, _solarSystems.SystemCount, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);

            if (_shadersLoaded)
            {
                Shader.Bind(null);

                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.Disable(EnableCap.ProgramPointSize);
                GL.Disable(EnableCap.PointSprite);
                GL.Disable(EnableCap.Texture2D);
            }
        }

        private void RenderConnections()
        {
            if (_shadersLoaded)
            {
                _shaderConn.SetVariable("projection", _matrixProjection);
                _shaderConn.SetVariable("modelView", _matrixModelview);
                GL.Enable(EnableCap.ColorMaterial);
                Shader.Bind(_shaderConn);
            }

            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.ConnectionVbo);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, IntPtr.Zero );

            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.ConnectionColor);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vbOs.ConnectionVao);

            GL.EnableClientState(ArrayCap.VertexArray);

            GL.DrawElements(PrimitiveType.Lines, _solarSystems.ConnectionVertexCount, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.DisableClientState(ArrayCap.VertexArray);

            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(0);

            if (_shadersLoaded)
            {
                Shader.Bind(null);
                GL.Disable(EnableCap.ColorMaterial);
            }
        }

        private void RenderHud()
        {
            ResetVbOs();
            _solarSystems.RemoveExpiredAlerts();
            DrawCrossHairLabels();
            DrawHoverLabels();
            DrawStickyLabels();

            RenderCrossHairs();
            RenderCrossHairLabels();
            RenderFont();
        }



        private void DrawHoverLabels()
        {
            Vector2 screenTemp;

            if (!_fontLoaded)
                return;

            if (!_isHighlighting)
                return;

            var hovering = _solarSystems.SolarSystems[_currentHighlight];


            if ((!_solarSystems.GreenCrossHairIDs.Contains(_currentHighlight)) && (!_solarSystems.RedCrossHairIDs.Contains(_currentHighlight)))
            {
                screenTemp = Project(_solarSystems.SolarSystems[_currentHighlight].Xyz, _matrixModelview, _matrixProjection, _w, _h);
                var pos = new Vector3((float)Math.Round(screenTemp.X + 10), _h - (float)Math.Round(screenTemp.Y - 10), 0.01f);

                var displayName = _solarSystems.SolarSystems[_currentHighlight].Name;

                var dp = new QFontDrawingPimitive(_mainText, _mainTextOptions ?? new QFontRenderOptions());
                dp.Print(displayName, pos, QFontAlignment.Left, Color.White);
                _drawing.DrawingPimitiveses.Add(dp);
            }

            var numSystems = hovering.ConnectedTo.Count;

            if (numSystems <= 0) return;
            foreach (SolarSystemConnection tempConnection in _solarSystems.SolarSystems[_currentHighlight].ConnectedTo.Where(tempConnection => (!_solarSystems.GreenCrossHairIDs.Contains(tempConnection.ToSystemId)) && (!_solarSystems.RedCrossHairIDs.Contains(tempConnection.ToSystemId))))
            {
                screenTemp = Project(_solarSystems.SolarSystems[tempConnection.ToSystemId].Xyz, _matrixModelview, _matrixProjection, _w, _h);
                Vector3 tempPos = new Vector3((float)Math.Round(screenTemp.X + 10), _h - (float)Math.Round(screenTemp.Y - 10), 0.01f);

                string displayName = _solarSystems.SolarSystems[tempConnection.ToSystemId].Name;

                var dp = new QFontDrawingPimitive(_mainText, _mainTextOptions ?? new QFontRenderOptions());
                dp.Print(displayName, tempPos, QFontAlignment.Left, Color.FromArgb(255, 160, 160, 160));
                _drawing.DrawingPimitiveses.Add(dp);
            }
        }

        private HashSet<int> ConnectedIds(int systemId)
        {
            var tempIds = new HashSet<int>();

            foreach (var tempConnection in _solarSystems.SolarSystems[systemId].ConnectedTo)
            {
                tempIds.Add(tempConnection.ToSystemId);
            }

            return tempIds;
        }

        private void DrawStickyLabels()
        {

            if (!_fontLoaded)
                return;

            foreach (var tempId in _stickyHighlightSystems)
            {
                if ((!_solarSystems.GreenCrossHairIDs.Contains(tempId)) && (!_solarSystems.RedCrossHairIDs.Contains(tempId)) )
                {
                    if (_isHighlighting && ((_currentHighlight == tempId) || ConnectedIds(_currentHighlight).Contains(tempId)))
                        continue;

                    var screenTemp = Project(_solarSystems.SolarSystems[tempId].Xyz, _matrixModelview, _matrixProjection, _w, _h);
                    var pos = new Vector3((float)Math.Round(screenTemp.X + 8), _h - (float)Math.Round(screenTemp.Y - 15), 0.01f);
                    var displayName = _solarSystems.SolarSystems[tempId].Name;

                    var dp = new QFontDrawingPimitive(_mainText, _mainTextOptions ?? new QFontRenderOptions());

                    dp.Print(displayName, pos, QFontAlignment.Left, Color.FromArgb(255, 110, 110, 110));
                    _drawing.DrawingPimitiveses.Add(dp);
                }
            }
        }

        private void RenderFont()
        {
            if (!_fontLoaded)
                return;

            _drawing.RefreshBuffers();
            _drawing.Draw();
        }

        private void ResetVbOs()
        {
            if (!_fontLoaded)
                return;

            _drawing.ProjectionMatrix = Matrix4.CreateOrthographicOffCenter(0, _w, 0, _h, -1, 0);
            _drawing.DrawingPimitiveses.Clear();
        }

        private void DrawCrossHairLabels()
        {
            if (!_fontLoaded)
                return;

            var queueLength = _solarSystems.RedCrossHairIDs.Count;
            queueLength += _solarSystems.GreenCrossHairIDs.Count;

            if (_conf.DisplayCharacterNames && _charLocations.Count > 0)
                queueLength += _charLocations.Values.Distinct().Count();

            if (queueLength <= 0) return;

            var sizes = new SizeF[queueLength];
            var pos = new Vector3[queueLength];

            Vector2 screenTemp;

            var i = 0;

            if (_conf.DisplayCharacterNames && _charLocations.Count > 0)
            {
                var tempLocations = BuildCharacterLocationIndex();

                foreach (var tempLocation in tempLocations)
                {
                    if (_fontLoaded)
                    {
                        var displayName = string.Empty;

                        foreach (var charName in tempLocation.Value)
                        {
                            displayName += charName;

                            if (charName != tempLocation.Value.Last())
                            {
                                displayName += Environment.NewLine;
                            }
                        }

                        screenTemp = Project(_solarSystems.SolarSystems[tempLocation.Key].Xyz, _matrixModelview, _matrixProjection, _w, _h);

                        var dp = new QFontDrawingPimitive(_mainText, _mainTextOptions ?? new QFontRenderOptions());

                        SizeF tempSize = dp.Measure(displayName, QFontAlignment.Right);

                        var tempAdjust = (int)Math.Round(tempSize.Height/2);

                        Vector3 tempPos = new Vector3((float)Math.Round(screenTemp.X - 20),
                            _h - (float)Math.Round(screenTemp.Y - tempAdjust), 0.01f);

                        sizes[i] = dp.Print(displayName, tempPos, QFontAlignment.Right, Color.White);
                        pos[i] = new Vector3(tempPos.X - sizes[i].Width, tempPos.Y, tempPos.Z);
                        _drawing.DrawingPimitiveses.Add(dp);
                    }
                    i++;
                }
            }

            foreach (var systemId in _solarSystems.RedCrossHairIDs)
            {
                if (_fontLoaded)
                {
                    var displayName = _solarSystems.SolarSystems[systemId].Name;
                    var tempPathId = -1;

                    // Home system
                    if (_conf.MapRangeFrom == 0)
                    {
                        if (_solarSystems.HomeSystemId != -1)
                            tempPathId = _solarSystems.GenerateUniquePathId(_solarSystems.HomeSystemId, systemId);
                    }
                    else // Character
                    {
                        if (_charLocations.ContainsKey(_conf.CharacterList[MapRangeFrom.SelectedIndex - 1]))
                        {
                            tempPathId =
                                _solarSystems.GenerateUniquePathId(
                                    _charLocations[_conf.CharacterList[MapRangeFrom.SelectedIndex - 1]], systemId);
                        }
                    }

                    if ((tempPathId != -1) && (_solarSystems.PathFindingCache.ContainsKey(tempPathId)))
                        if (_solarSystems.PathFindingCache[tempPathId].TotalJumps - 1 > 0)
                            displayName += " (" + (_solarSystems.PathFindingCache[tempPathId].TotalJumps - 1) + ")";

                    var tempStats = _solarSystems.GetSystemStats(systemId);

                    if (tempStats != null)
                    {
                        TimeSpan intelAge = DateTime.Now - tempStats.LastReport;
                        string mins = intelAge.Minutes.ToString();
                        string secs = intelAge.Seconds.ToString().PadLeft(2, '0');

                        if (_conf.ShowAlertAge && _conf.ShowAlertAgeSecs && _conf.ShowReportCount)
                            displayName += Environment.NewLine + mins + "m " + secs + "s | " + tempStats.ReportCount;
                        else if (_conf.ShowAlertAge && _conf.ShowAlertAgeSecs)
                            displayName = mins + ":" + secs + " | " + displayName; //displayName += Environment.NewLine + mins + "m " + secs + "s";
                        else if (_conf.ShowAlertAge && _conf.ShowReportCount)
                            displayName += Environment.NewLine + mins + "m | " + tempStats.ReportCount;
                        else if (_conf.ShowAlertAge)
                            displayName = mins + "m | " + displayName;
                        else if (_conf.ShowReportCount)
                            displayName = tempStats.ReportCount + " | " + displayName;
                    }

                    var dp = new QFontDrawingPimitive(_mainText, _mainTextOptions ?? new QFontRenderOptions());

                    screenTemp = Project(_solarSystems.SolarSystems[systemId].Xyz, _matrixModelview, _matrixProjection, _w, _h);

                    SizeF tempSize = dp.Measure(displayName);

                    var tempAdjust = (int)Math.Round(tempSize.Height / 2);

                    pos[i] = new Vector3((float)Math.Round(screenTemp.X + 23),
                        _h - (float)Math.Round(screenTemp.Y - tempAdjust), 0.01f);

                    sizes[i] = dp.Print(displayName, pos[i], QFontAlignment.Left, Color.White);
                    _drawing.DrawingPimitiveses.Add(dp);
                }
                i++;
            }

            foreach (var systemId in _solarSystems.GreenCrossHairIDs.Where(systemId => !_solarSystems.RedCrossHairIDs.Contains(systemId)))
            {
                if (_fontLoaded)
                {
                    var displayName = _solarSystems.SolarSystems[systemId].Name;

                    if (MapRangeFrom.SelectedIndex > 0)
                    {
                        var tempPathId = -1;

                        if (_solarSystems.HomeSystemId != -1)
                        {
                            if (_charLocations.ContainsKey(_conf.CharacterList[MapRangeFrom.SelectedIndex - 1]))
                            {
                                tempPathId =
                                    _solarSystems.GenerateUniquePathId(
                                        _charLocations[_conf.CharacterList[MapRangeFrom.SelectedIndex - 1]], _solarSystems.HomeSystemId);
                            }                            
                        }

                        if ((tempPathId != -1) && (_solarSystems.PathFindingCache.ContainsKey(tempPathId)))
                            if (_solarSystems.PathFindingCache[tempPathId].TotalJumps - 1 > 0)
                                displayName += " (" + (_solarSystems.PathFindingCache[tempPathId].TotalJumps - 1) + ")";
                    }

                    var dp = new QFontDrawingPimitive(_mainText, _mainTextOptions ?? new QFontRenderOptions());

                    screenTemp = Project(_solarSystems.SolarSystems[systemId].Xyz, _matrixModelview, _matrixProjection, _w, _h);

                    SizeF tempSize = dp.Measure(displayName);

                    var tempAdjust = (int)Math.Round(tempSize.Height / 2);

                    pos[i] = new Vector3((float)Math.Round(screenTemp.X + 23),
                        _h - (float)Math.Round(screenTemp.Y - tempAdjust), 0.01f);

                    sizes[i] = dp.Print(displayName, pos[i], QFontAlignment.Left, Color.White);
                    _drawing.DrawingPimitiveses.Add(dp);
                }
                i++;
            }

            // Draw plates

            var quads = new Vector4[queueLength * 4];
            var lines = new Vector4[queueLength * 4];

            for (var j = 0; j < queueLength; j++)
            {
                quads[(j * 4) + 0] = new Vector4(pos[j].X - 2f, pos[j].Y + 1f, 0, 1);
                quads[(j * 4) + 1] = new Vector4(pos[j].X + sizes[j].Width + 1f, pos[j].Y + 1f, 0, 1);
                quads[(j * 4) + 2] = new Vector4(pos[j].X + sizes[j].Width + 1f, pos[j].Y - sizes[j].Height - 1f, 0, 1);
                quads[(j * 4) + 3] = new Vector4(pos[j].X - 2f, pos[j].Y - sizes[j].Height - 1f, 0, 1);
                
                lines[(j * 4) + 0] = new Vector4(pos[j].X - 2.5f, pos[j].Y + 1.5f, 0, 1);
                lines[(j * 4) + 1] = new Vector4(pos[j].X + sizes[j].Width + 1.5f, pos[j].Y + 1.5f, 0, 1);
                lines[(j * 4) + 2] = new Vector4(pos[j].X + sizes[j].Width + 1.5f, pos[j].Y - sizes[j].Height - 1.5f, 0, 1);
                lines[(j*4) + 3] = new Vector4(pos[j].X - 2.5f, pos[j].Y - sizes[j].Height - 1.5f, 0, 1);
            }

            var quadVao = new int[queueLength*4];
            var lineVao = new int[queueLength*4];
            var quadColor = new Vector4[queueLength*4];
            var lineColor = new Vector4[queueLength*4];

            for (var k = 0; k < queueLength*4; k++)
            {
                //Element Arrays
                quadVao[k] = k;
                lineVao[k] = k;

                //Color Arrays
                quadColor[k] = new Vector4(0.5f, 0.5f, 0.5f, 0.25f);
                lineColor[k] = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
            }

            // Quads
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.TextQuadVbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr) (Vector4.SizeInBytes*queueLength*4), quads,
                BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vbOs.TextQuadVao);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr) (sizeof (int)*queueLength*4), quadVao,
                BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.TextQuadColor);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr) (Vector4.SizeInBytes*queueLength*4), quadColor,
                BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // Lines
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.TextLineVbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr) (Vector4.SizeInBytes*queueLength*4), lines,
                BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vbOs.TextLineVao);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr) (sizeof (int)*queueLength*4), lineVao,
                BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.TextLineColor);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr) (Vector4.SizeInBytes*queueLength*4), lineColor,
                BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private Dictionary<int, List<string>> BuildCharacterLocationIndex()
        {
            Dictionary<int, List<string>> tempLocations = new Dictionary<int, List<string>>();

            foreach (var charLocation in _charLocations)
            {
                if (!tempLocations.ContainsKey(charLocation.Value))
                {
                    tempLocations.Add(charLocation.Value, new List<string>());
                }

                tempLocations[charLocation.Value].Add(charLocation.Key);
            }
            return tempLocations;
        }

        private void RenderCrossHairLabels()
        {
            var queueLength = _solarSystems.RedCrossHairIDs.Count;
            queueLength += _solarSystems.GreenCrossHairIDs.Count;

            if (_conf.DisplayCharacterNames && _charLocations.Count > 0)
                queueLength += _charLocations.Values.Distinct().Count();

            if (_shadersLoaded)
            {
                _shaderConn.SetVariable("projection", Matrix4.CreateOrthographicOffCenter(0, _w, 0, _h, -1, 0));
                _shaderConn.SetVariable("modelView", Matrix4.Identity);
                GL.Enable(EnableCap.ColorMaterial);
                Shader.Bind(_shaderConn);
            }

            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.TextLineVbo);
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.TextLineColor);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vbOs.TextLineVao);

            GL.EnableClientState(ArrayCap.VertexArray);

            for (var i = 0; i < queueLength; i++)
            {
                var startElement = i * 4 * sizeof(uint);

                GL.DrawElements(PrimitiveType.LineLoop, 4, DrawElementsType.UnsignedInt, startElement);
            }

            GL.DisableClientState(ArrayCap.VertexArray);

            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(0);

            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.TextQuadVbo);
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbOs.TextQuadColor);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vbOs.TextQuadVao);

            GL.EnableClientState(ArrayCap.VertexArray);

            // TODO: GL.MultiDrawElements(PrimitiveType.Quads, queueLength, DrawElementsType.UnsignedInt, );

            for (var i = 0; i < queueLength; i++)
            {
                var startElement = i * 4 * sizeof(uint);

                GL.DrawElements(PrimitiveType.Quads, 4, DrawElementsType.UnsignedInt, startElement);
            }

            GL.DisableClientState(ArrayCap.VertexArray);

            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(0);

            if (_shadersLoaded)
            {
                Shader.Bind(null);
                GL.Disable(EnableCap.ColorMaterial);
            }
        }

        private void RenderCrossHairs()
        {
            if (!_shadersLoaded) return;

            var red = new List<Vector3>();
            var yellow = new List<Vector3>();

            var redGreen = new List<Vector3>();
            var redYellow = new List<Vector3>();
            var yellowGreen = new List<Vector3>();


            foreach (var systemId in _solarSystems.RedCrossHairIDs)
            {
                if (_charLocations.ContainsValue(systemId))
                {
                    // Char in system + alert
                    redYellow.Add(_solarSystems.SolarSystems[systemId].Xyz);
                }
                else if (_solarSystems.GreenCrossHairIDs.Contains(systemId))
                {
                    // Home system + alert
                    redGreen.Add(_solarSystems.SolarSystems[systemId].Xyz);
                }
                else
                {
                    // Plain red
                    red.Add(_solarSystems.SolarSystems[systemId].Xyz);
                }
            }

            foreach (var systemId in _charLocations.Values)
            {
                if (_solarSystems.GreenCrossHairIDs.Contains(systemId) && !_solarSystems.RedCrossHairIDs.Contains(systemId))
                {
                    // Char in home system + not alerting
                    yellowGreen.Add(_solarSystems.SolarSystems[systemId].Xyz);
                }
                else
                {
                    yellow.Add(_solarSystems.SolarSystems[systemId].Xyz);
                }
            }

            var green = (from systemId in _solarSystems.GreenCrossHairIDs
                where !_solarSystems.RedCrossHairIDs.Contains(systemId) && !_charLocations.ContainsValue(systemId)
                select _solarSystems.SolarSystems[systemId].Xyz).ToList();


            // ReSharper disable once JoinDeclarationAndInitializer
            bool renderRed, renderGreen, renderYellow, renderRedGreen, renderRedYellow, renderYellowGreen;
            // ReSharper disable once JoinDeclarationAndInitializer
            Vector3[] redCoords, yellowCoords, greenCoords, redGreenCoords, redYellowCoords, yellowGreenCoords;
            // ReSharper disable once JoinDeclarationAndInitializer
            int[] redVaoContent, yellowVaoContent, greenVaoContent, redGreenVaoContent, redYellowVaoContent, yellowGreenVaoContent;

            renderRed = renderYellow = renderGreen = renderRedGreen = renderRedYellow = renderYellowGreen = false;
            redCoords = yellowCoords = greenCoords = redGreenCoords = redYellowCoords = yellowGreenCoords =  new Vector3[0];
            redVaoContent = yellowVaoContent = greenVaoContent = redGreenVaoContent = redYellowVaoContent = yellowGreenVaoContent = new int[0];

            if (red.Count > 0)
            {
                renderRed = true;
                redCoords = new Vector3[red.Count];
                red.CopyTo(redCoords);
                redVaoContent = Enumerable.Range(0, red.Count).ToArray();
            }

            if (yellow.Count > 0)
            {
                renderYellow = true;
                yellowCoords = new Vector3[yellow.Count];
                yellow.CopyTo(yellowCoords);
                yellowVaoContent = Enumerable.Range(0, yellow.Count).ToArray();
            }

            if (green.Count > 0)
            {
                renderGreen = true;
                greenCoords = new Vector3[green.Count];
                green.CopyTo(greenCoords);
                greenVaoContent = Enumerable.Range(0, green.Count).ToArray(); 
            }

            if (redGreen.Count > 0)
            {
                renderRedGreen = true;
                redGreenCoords = new Vector3[redGreen.Count];
                redGreen.CopyTo(redGreenCoords);
                redGreenVaoContent = Enumerable.Range(0, redGreen.Count).ToArray();
            }

            if (redYellow.Count > 0)
            {
                renderRedYellow = true;
                redYellowCoords = new Vector3[redYellow.Count];
                redYellow.CopyTo(redYellowCoords);
                redYellowVaoContent = Enumerable.Range(0, redYellow.Count).ToArray();
            }

            if (yellowGreen.Count > 0)
            {
                renderYellowGreen = true;
                yellowGreenCoords = new Vector3[yellowGreen.Count];
                yellowGreen.CopyTo(yellowGreenCoords);
                yellowGreenVaoContent = Enumerable.Range(0, yellowGreen.Count).ToArray();
            }

            if (!renderRed && !renderYellow && !renderGreen && !renderRedGreen && !renderRedYellow && !renderYellowGreen) return;

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.PointSprite);
            GL.Enable(EnableCap.ProgramPointSize);

            _shaderCrosshair.SetVariable("projection", _matrixProjection);
            _shaderCrosshair.SetVariable("modelView", _matrixModelview);

            Shader.Bind(_shaderCrosshair);

            if (renderRed)
            {
                RenderColourCrossHairs(redCoords, redVaoContent, _texRedCh);
            }

            if (renderYellow)
            {
                RenderColourCrossHairs(yellowCoords, yellowVaoContent, _texYellowCh);
            }

            if (renderGreen)
            {
                RenderColourCrossHairs(greenCoords, greenVaoContent, _texGreenCh);
            }

            if (renderRedGreen)
            {
                RenderColourCrossHairs(redGreenCoords, redGreenVaoContent, _texRedGreenCh);
            }

            if (renderRedYellow)
            {
                RenderColourCrossHairs(redYellowCoords, redYellowVaoContent, _texRedYellowCh);
            }

            if (renderYellowGreen)
            {
                RenderColourCrossHairs(yellowGreenCoords, yellowGreenVaoContent, _texYellowGreenCh);
            }

            Shader.Bind(null);

            GL.Disable(EnableCap.ProgramPointSize);
            GL.Disable(EnableCap.PointSprite);
            GL.Disable(EnableCap.Texture2D);
        }

        private void RenderColourCrossHairs(Vector3[] coords, int[] vaoContent, int textureId)
        {
            int tempBuffer;
            int tempVaoBuffer;

            _shaderCrosshair.BindTexture(textureId, TextureUnit.Texture0, "tex");

            GL.GenBuffers(1, out tempBuffer);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, tempBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr) (Vector3.SizeInBytes*coords.Length), coords,
                BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            GL.GenBuffers(1, out tempVaoBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, tempVaoBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr) (sizeof (int)*vaoContent.Length), vaoContent,
                BufferUsageHint.DynamicDraw);

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.DrawElements(PrimitiveType.Points, coords.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            GL.DisableClientState(ArrayCap.VertexArray);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteBuffer(tempVaoBuffer);
            GL.DeleteBuffer(tempBuffer);
        }

        private QFontDrawing _drawing;
        private QFont _mainText;
        private QFontRenderOptions _mainTextOptions;

        private void SetupQFont()
        {
            _drawing = new QFontDrawing();

            var builderConfig = new QFontBuilderConfiguration(false)
            {
                TextGenerationRenderHint = TextGenerationRenderHint.SizeDependent
            };

            var assembly = Assembly.GetExecutingAssembly();
            Font tempFont = new Font(LoadFontFamilyFromResource("Taco.Resources.Fonts.Taco.ttf", ref assembly), 6);
            _mainText = new QFont(tempFont, builderConfig);

            _mainTextOptions = new QFontRenderOptions()
            {
                DropShadowActive = false, 
                Colour = Color.White, 
                WordSpacing = .5f, 
                LineSpacing = 1.3f
            };

            _fontLoaded = true;
        }

        private FontFamily LoadFontFamilyFromResource(string resourceName, ref Assembly assembly)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                try
                {
                    var buffer = new byte[stream.Length];

                    stream.Read(buffer, 0, buffer.Length);

                    var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    try
                    {
                        var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                        var fontCollection = new PrivateFontCollection();
                        fontCollection.AddMemoryFont(ptr, buffer.Length);
                        return fontCollection.Families[0];
                    }
                    finally
                    {
                        handle.Free();
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private Vector2 Project(Vector3 pos, Matrix4 viewMatrix, Matrix4 projectionMatrix, int screenWidth, int screenHeight)
        {
            pos = Vector3.Transform(pos, viewMatrix);
            pos = Vector3.Transform(pos, projectionMatrix);
            pos.X /= pos.Z;
            pos.Y /= pos.Z;
            pos.X = (pos.X + 1) * screenWidth / 2;
            pos.Y = (pos.Y + 1) * screenHeight / 2;

            return new Vector2(pos.X, screenHeight - pos.Y);
        }

        private void BeginRender()
        {
            if (!_solarSystems.AllVbOsClean || !_solarSystems.IsDataClean || !_solarSystems.AreUniformsClean)
            {
                if (!_solarSystems.IsDataClean)
                    _solarSystems.RefreshVboData();

                if (!_solarSystems.AllVbOsClean)
                    RefreshVboContent();

                if (!_solarSystems.AreUniformsClean)
                    _solarSystems.BuildUniforms();
            }

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _matrixProjection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)_w / _h, 0.1f, _cameraDistance + 10f);
            _eye = new Vector3(_lookAt.X, _lookAt.Y, _cameraDistance);
            _matrixModelview = Matrix4.Identity * Matrix4.LookAt(_eye, _lookAt, _vecY);
        }

        private void EndRender()
        {
            GL.Flush();
            glOut.SwapBuffers();
        }
        #endregion Rendering

        #region Utility
        private void SetupAutoComplete()
        {
            SearchSystem.AutoCompleteCustomSource = _solarSystems.NameStringCollection;
            SearchSystem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            SearchSystem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            SearchSystem.Invalidate();

            RangeAlertSystem.AutoCompleteCustomSource = _solarSystems.NameStringCollection;
            RangeAlertSystem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            RangeAlertSystem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            RangeAlertSystem.Invalidate();

            NewIgnoreSystem.AutoCompleteCustomSource = _solarSystems.NameStringCollection;
            NewIgnoreSystem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            NewIgnoreSystem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            NewIgnoreSystem.Invalidate();
        }

        private void LoadSystemData()
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var file = assembly.GetManifestResourceStream("Taco.Resources.Data.systemdata.bin"))
            {
                try
                {
                    var sa = Serializer.Deserialize<SolarSystemData[]>(file);
                    _solarSystems.LoadSystemData(sa);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void CreateTexture(out int texture, Bitmap bitmap)
        {
            // load texture 
            GL.GenTextures(1, out texture);

            //Still required else TexImage2D will be applyed on the last bound texture
            GL.BindTexture(TextureTarget.Texture2D, texture);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
            OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        public float CalcPointSize()
        {
            const float pMax = 10;
            const float pMin = 7;
            const float divisor = 40;

            var temp = ((pMin + pMax) - (_cameraDistance / divisor));

            if (temp > pMax)
                temp = pMax;

            if (temp < pMin)
                temp = pMin;

            return temp;
        }

        private void ZoomTo(int systemId)
        {
            if (_dragging) return;

            if (_zooming)
                StopZoom();

            _zoomStart = _lookAt;
            _zoomEnd = _solarSystems.SolarSystems[systemId].Xyz;

            _zoomDiff = _zoomEnd - _zoomStart;
            _zoomToSystemId = systemId;

            _zooming = true;
        }

        // TODO: Refactor this, needs work
        private LogWatcher OpenLogFile(CheckBox channel, string prefix, LogFileType logFileType)
        {
            if (!channel.Checked) return null;

            var tempWatcher = !OverrideLogPath.Checked ? new LogWatcher(prefix, logFileType) : new LogWatcher(prefix, logFileType, LogPath.Text);

            tempWatcher.ProcessNewData += logFile_ProcessNewData;
            tempWatcher.ProcessCombat += logFile_ProcessCombat;
            channel.Checked = tempWatcher.StartWatch();

            return tempWatcher;
        }

        void logFile_ProcessCombat(object sender, ProcessCombatEventArgs e)
        {
            if (e.CombatEvent == CombatEventType.Start)
            {
                foreach (
                    var dropDownItem in
                        anomalyMonitorMenuItem.DropDownItems.Cast<ToolStripMenuItem>()
                            .Where(dropDownItem => dropDownItem.Text == e.CharacterName && dropDownItem.Checked))
                {
                    WriteIntel("sys", " > Anomaly Monitor | Combat started: " + dropDownItem.Text);
                }
            }
            else if (e.CombatEvent == CombatEventType.Stop)
            {
                foreach (
                    var dropDownItem in
                        anomalyMonitorMenuItem.DropDownItems.Cast<ToolStripMenuItem>()
                            .Where(dropDownItem => dropDownItem.Text == e.CharacterName && dropDownItem.Checked))
                {
                    WriteIntel("sys", " > Anomaly Monitor | Combat finished: " + dropDownItem.Text);
                    PlayAnomalyWatcherSound();
                }
            }
        }

        bool _followingChars;

        private void ToggleFollowChars()
        {
            _followingChars = !_followingChars;

            if (_followingChars)
            {
                _localWatcher = new LocalWatcher();
                _localWatcher.SystemChange += _localWatcher_SystemChange;
                _localWatcher.StartWatch();
            }
            else
            {
                _localWatcher.SystemChange -= _localWatcher_SystemChange;
                _localWatcher.StopWatch();
                _localWatcher = null;
                _charLocations.Clear();
            }
        }

        // TODO: Convert _logFiles to List<T>
        private void ToggleLogWatch()
        {
            _processLogs = !_processLogs;

            if ((LogWatcher.GetRootLogPath().Length > 0) && (!OverrideLogPath.Checked))
            {
                LogPath.Text = LogWatcher.GetRootLogPath();
            }

            if (_processLogs)
            {
                if (!_conf.MonitorBranch && !_conf.MonitorDeklein && !_conf.MonitorTenal && !_conf.MonitorVenal && !_conf.MonitorGameLog)
                {
                    MessageBox.Show("Pick channels to monitor.", "Pick Channels", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _processLogs = !_processLogs;
                    return;
                }

                _logFiles[0] = OpenLogFile(MonitorBranch, "brn", LogFileType.Chat);
                _logFiles[1] = OpenLogFile(MonitorDeklein, "dek", LogFileType.Chat);
                _logFiles[2] = OpenLogFile(MonitorVenal, "vnl", LogFileType.Chat);
                _logFiles[3] = OpenLogFile(MonitorTenal, "tnl", LogFileType.Chat);
                _logFiles[4] = OpenLogFile(MonitorFade, "fade", LogFileType.Chat);
                _logFiles[5] = OpenLogFile(MonitorPureBlind, "pb", LogFileType.Chat);
                _logFiles[6] = OpenLogFile(MonitorTribute, "tri", LogFileType.Chat);
                _logFiles[7] = OpenLogFile(MonitorVale, "vale", LogFileType.Chat);
                _logFiles[8] = OpenLogFile(MonitorProvidence, "provi", LogFileType.Chat);
                _logFiles[9] = OpenLogFile(MonitorDelve, "delve", LogFileType.Chat);
                _logFiles[10] = OpenLogFile(MonitorGameLog, "gme", LogFileType.Game);
                _logFiles[11] = OpenLogFile(MonitorGOTG, "GOTG_Intel", LogFileType.Chat);

                LogWatchToggle.Text = "Stop Watching Logs";

                if (_conf.ShowCharacterLocations && !_followingChars)
                    ToggleFollowChars();
            }
            else
            {
                LogWatchToggle.Text = "Start Watching Logs";

                foreach (var tempLogWatcher in _logFiles.Where(tempLogWatcher => tempLogWatcher != null))
                {
                    tempLogWatcher.ProcessNewData -= logFile_ProcessNewData;
                    tempLogWatcher.ProcessCombat -= logFile_ProcessCombat;
                    tempLogWatcher.StopWatch();
                }

                _logFiles[0] = null;
                _logFiles[1] = null;
                _logFiles[2] = null;
                _logFiles[3] = null;
                _logFiles[4] = null;
                _logFiles[5] = null;
                _logFiles[6] = null;
                _logFiles[7] = null;
                _logFiles[8] = null;
                _logFiles[9] = null;
                _logFiles[10] = null;
                _logFiles[11] = null;

                if (_followingChars)
                    ToggleFollowChars();
            }
        }

        private void ToggleFullScreen()
        {
            if (!_isFullScreen)
            {
                _oldSize = Size;
                _oldPosition = new Point(Left, Top);

                _oldGlOutSize = glOut.Size;
                _oldGlOutPosition = new Point(glOut.Left, glOut.Top);

                _oldUiContainerSize = UIContainer.Size;
                _oldUiContainerPosition = new Point(UIContainer.Left, UIContainer.Top);

                FormBorderStyle = FormBorderStyle.None;

                Left = Screen.FromControl(this).Bounds.Left;
                Top = Screen.FromControl(this).Bounds.Top;
                Width = Screen.FromControl(this).Bounds.Width;
                Height = Screen.FromControl(this).Bounds.Height;

                glOut.Top = 0;
                glOut.Left = 0;
                glOut.Width = Width - 450;
                glOut.Height = Height;

                UIContainer.Top = 0;
                UIContainer.Left = Width - 450;
                UIContainer.SplitterDistance = 65;

                TopMost = true;

                FullscreenToggle.Text = "Windowed";
                _isFullScreen = true;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                Size = _oldSize;
                Left = _oldPosition.X;
                Top = _oldPosition.Y;

                glOut.Size = _oldGlOutSize;
                glOut.Left = _oldGlOutPosition.X;
                glOut.Top = _oldGlOutPosition.Y;

                UIContainer.Size = _oldUiContainerSize;
                UIContainer.Left = _oldUiContainerPosition.X;
                UIContainer.Top = _oldUiContainerPosition.Y;
                UIContainer.SplitterDistance = 65;

                TopMost = false;

                FullscreenToggle.Text = "Fullscreen";
                _isFullScreen = false;
            }
        }

        private void CrashMeRecursive()
        {
            CrashMeRecursive();
        }
        #endregion Utility

        #region Sound
        private SoundPlayer LoadSoundFromResource(string resourceName, ref Assembly assembly)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                try
                {
                    var soundPlayer = new SoundPlayer(stream);
                    soundPlayer.Load();

                    return soundPlayer;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private SoundPlayer LoadSoundFromFile(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                try
                {
                    var soundPlayer = new SoundPlayer(stream);
                    soundPlayer.Load();

                    return soundPlayer;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private void LoadSounds()
        {
            var assembly = Assembly.GetExecutingAssembly();

            _sounds.Add("1up1", LoadSoundFromResource("Taco.Resources.Sounds.1up1.wav", ref assembly));
            _sounds.Add("Boo2", LoadSoundFromResource("Taco.Resources.Sounds.Boo2.wav", ref assembly));
            _sounds.Add("KamekLaugh", LoadSoundFromResource("Taco.Resources.Sounds.KamekLaugh.wav", ref assembly));
            _sounds.Add("RedCoin2", LoadSoundFromResource("Taco.Resources.Sounds.RedCoin2.wav", ref assembly));
            _sounds.Add("RedCoin3", LoadSoundFromResource("Taco.Resources.Sounds.RedCoin3.wav", ref assembly));
            _sounds.Add("Coin", LoadSoundFromResource("Taco.Resources.Sounds.Coin.wav", ref assembly));
            _sounds.Add("Powerup", LoadSoundFromResource("Taco.Resources.Sounds.Powerup.wav", ref assembly));
            _sounds.Add("StarCoin", LoadSoundFromResource("Taco.Resources.Sounds.StarCoin.wav", ref assembly));
            _sounds.Add("SuitFly", LoadSoundFromResource("Taco.Resources.Sounds.SuitFly.wav", ref assembly));
            _sounds.Add("SuitSpin", LoadSoundFromResource("Taco.Resources.Sounds.SuitSpin.wav", ref assembly));
            _sounds.Add("Whistle", LoadSoundFromResource("Taco.Resources.Sounds.Whistle.wav", ref assembly));
        }

        private void PopulateSoundCombos()
        {
            foreach (string sound in _sounds.Keys)
            {
                RangeAlertSound.Items.Add(sound);
                CustomTextAlertSound.Items.Add(sound);
                AnomalyWatcherSound.Items.Add(sound);
            }

            RangeAlertSound.Items.Add("Custom...");
            CustomTextAlertSound.Items.Add("Custom...");
            AnomalyWatcherSound.Items.Add("Custom...");
        }
        #endregion Sound

        #region Other Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetWindowState();
        }

        private void Ticker_Tick(object sender, EventArgs e)
        {
            if (!_hasRendered)
                return;


            if (_isHighlighting)
                _highlightTick++;
            else
                _highlightTick = 0;

            if (_isHighlighting && (_highlightTick > _maxHighlighTick))
            {
                _solarSystems.AddHighlight(_currentHighlight);
            }

            if (_zooming)
            {
                _zoomTick++;

                if (_zoomTick <= _maxZoomTick)
                {
                    var tempLookat = new Vector3
                    {
                        X = (float) PennerDoubleAnimation.QuintEaseInOut(_zoomTick, _zoomStart.X, _zoomDiff.X, 100),
                        Y = (float) PennerDoubleAnimation.QuintEaseInOut(_zoomTick, _zoomStart.Y, _zoomDiff.Y, 100),
                        Z = 0.0f
                    };

                    _lookAt = tempLookat;
                    _eye = new Vector3(tempLookat.X, tempLookat.Y, _cameraDistance);
                }
                else
                {
                    _lookAt = _zoomEnd;
                    _eye = new Vector3(_lookAt.X, _lookAt.Y, _cameraDistance);
                    _solarSystems.AddHighlight(_zoomToSystemId, true);

                    if (_currentHighlight > 0)
                    {
                        _solarSystems.RemoveHighlight(_currentHighlight);
                    }

                    StopZoom();
                }
            }

            _solarSystems.IncomingTick();
            glOut.Invalidate();
        }

        private void StopZoom()
        {
            _currentHighlight = _zoomToSystemId;
            _isHighlighting = true;
            _zooming = false;
            _zoomTick = 0;
            _zoomToSystemId = -1;
        }

        private void LogWatchToggle_Click(object sender, EventArgs e)
        {
            ToggleLogWatch();
        }

        private void FullscreenToggle_Click(object sender, EventArgs e)
        {
            ToggleFullScreen();
        }

        private void QuitTaco_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ClearSelectedSystems_Click(object sender, EventArgs e)
        {
            _stickyHighlightSystems.Clear();
            SaveStickySystems();
        }

        private void TestFlood_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < 1; i++)
            {
                var randomNumber = random.Next(0, 5000);
                _solarSystems.AddAlert(randomNumber);
                ZoomTo(randomNumber);
            }

            _solarSystems.AddAlert(4322);
        }

        private bool InputFocused()
        {
            return SearchSystem.Focused ||
                   RangeAlertSystem.Focused ||
                   RangeAlertSound.Focused ||
                   RangeAlertCharacter.Focused ||
                   CentreOnCharacter.Focused ||
                   MapRangeFrom.Focused ||
                   NewCustomAlertText.Focused ||
                   CustomTextAlertSound.Focused ||
                   NewIgnoreText.Focused ||
                   NewIgnoreSystem.Focused;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Q):
                    if (!InputFocused())
                    {
                        Application.Exit();
                        return true;
                    }
                    break;
                case (Keys.Escape):
                    if (_isFullScreen && !AlertList.Focused)
                        ToggleFullScreen();
                    break;
                case (Keys.H):
                    if (_isFullScreen && !InputFocused())
                    {
                        if (glOut.Width == Width - 450)
                            glOut.Width = Width;
                        else
                            glOut.Width = Width - 450;
                        glOut.Height = Height;
                    }
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UIContainer_Panel1_Resize(object sender, EventArgs e)
        {
            UIContainer.SplitterDistance = 65;
        }

        private void FindSystem_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, SolarSystem> tempSolarSystem in _solarSystems.SolarSystems)
            {
                SolarSystem tempSystem = tempSolarSystem.Value;
                if (tempSystem.MatchNameRegex(SearchSystem.Text))
                {
                    ZoomTo(tempSolarSystem.Key);
                    return;
                }
            }
            
            MessageBox.Show("System \"" + SearchSystem.Text + "\" not found, try again.", "System Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void PathFindingTicker_Tick(object sender, EventArgs e)
        {
            _solarSystems.ProcessPathfindingQueue();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FinaliseConfig();

            // Cleanup hidden tab pages
            foreach (var hiddenPage in _hiddenPages)
            {
                hiddenPage.Value.Dispose();
            }
        }

        void _localWatcher_SystemChange(object sender, ProcessSystemChangeEventArgs e)
        {
            int nativeId;
            int systemId = -1;
            if (int.TryParse(e.SystemName, out nativeId))
            {
                foreach (KeyValuePair<int, SolarSystem> tempSolarSystem in from tempSolarSystem in _solarSystems.SolarSystems let tempSystem = tempSolarSystem.Value where tempSystem.NativeId == nativeId select tempSolarSystem)
                {
                    systemId = tempSolarSystem.Key;
                    break;
                }
            }
            else
            {
                foreach (KeyValuePair<int, SolarSystem> tempSolarSystem in from tempSolarSystem in _solarSystems.SolarSystems let tempSystem = tempSolarSystem.Value where tempSystem.MatchNameRegex(e.SystemName) select tempSolarSystem)
                {
                    systemId = tempSolarSystem.Key;
                    break;
                }
            }

            var charName = e.CharName.Trim();

            if (charName.Length > 0 && !_charLocations.ContainsKey(charName) && systemId != -1)
            {
                _charLocations.Add(charName, systemId);

                if (_conf.CameraFollowCharacter && CentreOnCharacter.Text == charName)
                    ZoomTo(systemId);

                if (!_conf.CharacterList.Contains(charName))
                {
                    AddNewCharacter(e.CharName);
                }
            }
            else if (systemId != -1 && charName.Length > 0)
            {
                _charLocations[charName] = systemId;

                if (CameraFollowCharacter.Checked && CentreOnCharacter.Text == charName)
                    ZoomTo(systemId);
            }

            var charDisplayName = charName.Length == 0 ? "Unknown" : charName;

            if (systemId != -1)
            {
                WriteIntel("sys", " > System Change: " + charDisplayName + " (" + _solarSystems.SolarSystems[systemId].Name + ")", true);

                // Update ranges if needed
                if (charDisplayName.Length > 0 && MapRangeFrom.SelectedIndex != 0)
                {
                    var charId = _conf.CharacterId(charDisplayName);

                    if (charId != -1 && (_conf.MapRangeFrom - 1) == charId)
                    {
                        _solarSystems.SetCharacterLocation(systemId);
                    }
                }
            }
        }

        private void AddNewCharacter(string characterName)
        {
            if (characterName.Trim().Length == 0) return;

            _conf.AddCharacter(characterName);

            RangeAlertCharacter.Items.Add(characterName);
            RangeAlertCharacter.Invalidate();

            CentreOnCharacter.Items.Add(characterName);
            CentreOnCharacter.Invalidate();

            MapRangeFrom.Items.Add(characterName);
            MapRangeFrom.Invalidate();

            followMenuItem.DropDownItems.Add(new ToolStripMenuItem(characterName));
            mapRangeFromMenuItem.DropDownItems.Add(new ToolStripMenuItem(characterName));
            anomalyMonitorMenuItem.DropDownItems.Add(new ToolStripMenuItem(characterName));

            WriteIntel("sys", " > New character found: " + characterName);
        }

        private void mapRangeFromMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Home System")
            {
                MapRangeFrom.SelectedIndex = 0;
            }
            else
            {
                MapRangeFrom.SelectedIndex = _conf.CharacterId(e.ClickedItem.Text) + 1;
            }
        }

        private void followMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "None")
            {
                CameraFollowCharacter.Checked = false;
            }
            else
            {
                CameraFollowCharacter.Checked = true;
                CentreOnCharacter.SelectedIndex = _conf.CharacterId(e.ClickedItem.Text);
            }
        }

        private void anomalyMonitorMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in anomalyMonitorMenuItem.DropDownItems)
            {
                if (dropDownItem.Text == e.ClickedItem.Text)
                {
                    dropDownItem.Checked = !dropDownItem.Checked;

                    if (dropDownItem.Checked)
                    {
                        if (AnomalyWatcherSound.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select an \"Anomaloy Monitor\" sound first. (Config -> Misc Settings)", "PEBKAC",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dropDownItem.Checked = false;
                            return;
                        }
                        WriteIntel("sys", " > Anomaly Monitor | Enabled: " + dropDownItem.Text);
                    }
                    else
                    {
                        WriteIntel("sys", " > Anomaly Monitor | Disabled: " + dropDownItem.Text);
                    }
                }
            }
        }
        #endregion Other Events

        #region Settings UI Events
        private void CameraFollowCharacter_CheckedChanged(object sender, EventArgs e)
        {
            CentreOnCharacter.Enabled = CameraFollowCharacter.Checked;

            if (!CameraFollowCharacter.Checked)
            {
                CentreOnCharacter.SelectedIndex = -1;
                CentreOnCharacter.Text = string.Empty;
            }
            else
            {
                if (_conf.CharacterList.Count > 0)
                {
                    CentreOnCharacter.SelectedIndex = 0;
                }
                else
                {
                    CameraFollowCharacter.Checked = false;
                    MessageBox.Show("No characters discovered yet!", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (_configLoaded)
                _conf.CameraFollowCharacter = CameraFollowCharacter.Checked;
        }

        private void DisplayCharacterNames_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.DisplayCharacterNames = DisplayCharacterNames.Checked;
        }

        private void ShowCharacterLocations_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.ShowCharacterLocations = ShowCharacterLocations.Checked;

            if (ShowCharacterLocations.Checked)
            {
                DisplayCharacterNames.Enabled = true;

                if (_processLogs && !_followingChars)
                    ToggleFollowChars();
            }
            else
            {
                DisplayCharacterNames.Enabled = false;
                DisplayCharacterNames.Checked = false;

                if (_followingChars)
                    ToggleFollowChars();
            }
        }

        private void CentreOnCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.CentreOnCharacter = CentreOnCharacter.SelectedIndex;

            foreach (ToolStripMenuItem dropDownItem in followMenuItem.DropDownItems)
            {
                if (CentreOnCharacter.SelectedIndex == -1 && dropDownItem.Text == "None")
                {
                    dropDownItem.Checked = true;
                }
                else if (dropDownItem.Text == CentreOnCharacter.Text)
                {
                    dropDownItem.Checked = true;
                }
                else
                {
                    dropDownItem.Checked = false;
                }
            }

            if (CameraFollowCharacter.Checked && _charLocations.ContainsKey(CentreOnCharacter.Text))
                ZoomTo(_charLocations[CentreOnCharacter.Text]);
        }

        private void MapRangeFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MapRangeFrom = MapRangeFrom.SelectedIndex;

            foreach (ToolStripMenuItem dropDownItem in mapRangeFromMenuItem.DropDownItems)
            {
                dropDownItem.Checked = dropDownItem.Text == MapRangeFrom.Text;
            }

            if (MapRangeFrom.SelectedIndex != 0)
            {
                if (_charLocations.ContainsKey(_conf.CharacterList[(MapRangeFrom.SelectedIndex - 1)]))
                {
                    _solarSystems.SetCharacterLocation(_charLocations[_conf.CharacterList[(MapRangeFrom.SelectedIndex - 1)]]);
                }
            }
        }

        private void ChooseLogPath_Click(object sender, EventArgs e)
        {
            DialogResult result = LogBrowser.ShowDialog();

            if (result != DialogResult.OK) return;

            if (Directory.Exists(LogBrowser.SelectedPath + @"\Chatlogs") && Directory.Exists(LogBrowser.SelectedPath + @"\Gamelogs"))
            {
                LogPath.Text = LogBrowser.SelectedPath;
                if (_configLoaded)
                    _conf.LogPath = LogBrowser.SelectedPath;
            }
            else
            {
                MessageBox.Show("Invalid Log Path.  Chatlogs and/or Gamelogs sub-directories missing.",
                    "Invalid Log Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewRangeAlert_Click(object sender, EventArgs e)
        {
            if (UpperLimitOperator.SelectedIndex == -1)
            {
                MessageBox.Show("Select an upper limit operator (the first drop down box).", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UpperLimitOperator.SelectedIndex > 0 && LowerLimitOperator.SelectedIndex == -1)
            {
                MessageBox.Show("Select a lower limit operator (the second drop down box).", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (NewRangeAlertType.SelectedIndex == -1)
            {
                MessageBox.Show("Select a range alert type.", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (RangeAlertSystem.Text.Trim().Length == 0 && NewRangeAlertType.SelectedIndex == 2)
            {
                MessageBox.Show("Select a system", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (RangeAlertCharacter.Text.Trim().Length == 0 && NewRangeAlertType.SelectedIndex == 3)
            {
                MessageBox.Show("Select a character", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (RangeAlertSound.SelectedIndex == -1)
            {
                MessageBox.Show("Select an alert sound", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var temp = new AlertTrigger { Type = AlertType.Ranged };


            switch ((string)UpperLimitOperator.SelectedItem)
            {
                case "<=":
                    temp.UpperLimitOperator = RangeAlertOperator.LessThanOrEqual;
                    break;
                case "=":
                    temp.UpperLimitOperator = RangeAlertOperator.Equal;
                    break;
                default:
                    temp.UpperLimitOperator = RangeAlertOperator.Equal;
                    break;
            }

            temp.UpperRange = Convert.ToInt32(UpperAlertRange.Value);

            switch ((string)LowerLimitOperator.SelectedItem)
            {
                case ">=":
                    temp.LowerLimitOperator = RangeAlertOperator.GreaterThanOrEqual;
                    break;
                case ">":
                    temp.LowerLimitOperator = RangeAlertOperator.GreaterThan;
                    break;
                default:
                    temp.LowerLimitOperator = RangeAlertOperator.GreaterThanOrEqual;
                    break;
            }

            temp.LowerRange = temp.UpperLimitOperator == RangeAlertOperator.Equal ? 0 : Convert.ToInt32(LowerAlertRange.Value);

            switch (NewRangeAlertType.SelectedIndex)
            {
                case 0:
                    temp.SystemId = -1;
                    temp.RangeTo = RangeAlertType.Home;
                    break;
                case 1:
                    temp.SystemId = -1;
                    temp.RangeTo = RangeAlertType.AnyCharacter;
                    break;
                case 2:
                    temp.SystemId = _solarSystems.Names[RangeAlertSystem.Text];
                    temp.RangeTo = RangeAlertType.System;
                    break;
                case 3:
                    temp.CharacterName = RangeAlertCharacter.Text.Trim();
                    temp.RangeTo = RangeAlertType.Character;
                    break;
            }

            temp.SystemName = RangeAlertSystem.Text;
            temp.SoundId = RangeAlertSound.SelectedIndex == RangeAlertSound.Items.Count - 1 ? -1 : RangeAlertSound.SelectedIndex;
            temp.SoundPath = (string)RangeAlertSound.SelectedItem;

            temp.Enabled = false;

            AlertList.Items.Add(temp.ToString());
            _alertTriggers.Add(temp);

            UpperLimitOperator.SelectedIndex = -1;
            UpperAlertRange.Value = 0;
            LowerLimitOperator.SelectedIndex = -1;
            LowerAlertRange.Value = 0;
            RangeAlertSystem.SelectedIndex = -1;
            RangeAlertSystem.Text = string.Empty;

            RangeAlertCharacter.Text = string.Empty;

            NewRangeAlertType.SelectedIndex = -1;

            RangeAlertSound.SelectedIndex = -1;
            RangeAlertSound.Items.RemoveAt(RangeAlertSound.Items.Count - 1);
            RangeAlertSound.Items.Add("Custom...");

            WriteAlertConfig();
            LoadAlertConfig();
        }

        private void AddNewCustomAlert_Click(object sender, EventArgs e)
        {
            if (NewCustomAlertText.Text.Trim().Length == 0)
            {
                MessageBox.Show("At least enter some text to alert on...", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NewCustomAlertText.Text = string.Empty;
                return;
            }

            if (CustomTextAlertSound.SelectedIndex == -1)
            {
                MessageBox.Show("Select an alert sound", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var temp = new AlertTrigger
            {
                Type = AlertType.Custom,
                Enabled = false,
                Text = NewCustomAlertText.Text,
                RepeatInterval = Convert.ToInt32(CustomAlertRepeatInterval.Value),
                SoundId =
                    CustomTextAlertSound.SelectedIndex == CustomTextAlertSound.Items.Count - 1
                        ? -1
                        : CustomTextAlertSound.SelectedIndex,
                SoundPath = (string)CustomTextAlertSound.SelectedItem
            };

            AlertList.Items.Add(temp.ToString());
            _alertTriggers.Add(temp);

            NewCustomAlertText.Text = string.Empty;
            CustomAlertRepeatInterval.Value = 0;

            CustomTextAlertSound.SelectedIndex = -1;
            CustomTextAlertSound.Items.RemoveAt(RangeAlertSound.Items.Count - 1);
            CustomTextAlertSound.Items.Add("Custom...");

            WriteAlertConfig();
            LoadAlertConfig();
        }

        private void RangeAlertSystem_Leave(object sender, EventArgs e)
        {
            if (RangeAlertSystem.Text.Trim().Length == 0) return;

            foreach (var tempSystem in _solarSystems.SolarSystems.Select(tempSolarSystem => tempSolarSystem.Value).Where(tempSystem => tempSystem.MatchNameRegex(RangeAlertSystem.Text)))
            {
                RangeAlertSystem.Text = tempSystem.Name;
                return;
            }

            MessageBox.Show("System \"" + RangeAlertSystem.Text + "\" not found, try again.", "System Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            RangeAlertSystem.Text = string.Empty;
        }

        private void RangeAlertSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_pickingFile || RangeAlertSound.SelectedIndex != RangeAlertSound.Items.Count - 1 || _loadingEdit) return;

            _pickingFile = true;

            if (CustomSoundPicker.ShowDialog() != DialogResult.OK) return;

            RangeAlertSound.Items.RemoveAt(RangeAlertSound.SelectedIndex);
            RangeAlertSound.Items.Add(CustomSoundPicker.FileName);
            RangeAlertSound.SelectedIndex = RangeAlertSound.Items.Count - 1;
            _customSound = LoadSoundFromFile(CustomSoundPicker.FileName);
            _pickingFile = false;
        }

        private void CustomTextAlertSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_pickingFile || CustomTextAlertSound.SelectedIndex != CustomTextAlertSound.Items.Count - 1) return;

            _pickingFile = true;

            if (CustomSoundPicker.ShowDialog() != DialogResult.OK) return;

            CustomTextAlertSound.Items.RemoveAt(CustomTextAlertSound.SelectedIndex);
            CustomTextAlertSound.Items.Add(CustomSoundPicker.FileName);
            CustomTextAlertSound.SelectedIndex = CustomTextAlertSound.Items.Count - 1;
            _customSound = LoadSoundFromFile(CustomSoundPicker.FileName);
            _pickingFile = false;
        }

        private void PlayAlertSound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_muteSound) return;

            if (_alertTriggers[AlertList.SelectedIndex].SoundId == -1)
            {
                var temp = LoadSoundFromFile(_alertTriggers[AlertList.SelectedIndex].SoundPath);
                temp.Play();
                temp.Dispose();
            }
            else if (RangeAlertSound.SelectedIndex < RangeAlertSound.Items.Count - 1)
            {
                _sounds[_alertTriggers[AlertList.SelectedIndex].SoundPath].Play();
            }
        }

        private void AddIgnoreText_Click(object sender, EventArgs e)
        {
            if (NewIgnoreText.Text.Length <= 0) return;

            _ignoreStrings.Add(new Regex(@"\b" + NewIgnoreText.Text + @"\b", RegexOptions.Compiled | RegexOptions.IgnoreCase));
            IgnoreTextList.Items.Add(NewIgnoreText.Text);
            WriteIgnoreLists();
        }

        private void RemoveIgnoreText_Click(object sender, EventArgs e)
        {
            if (IgnoreTextList.SelectedIndex < 0) return;

            IgnoreTextList.Items.RemoveAt(IgnoreTextList.SelectedIndex);

            _ignoreStrings.Clear();
            foreach (var tempString in IgnoreTextList.Items)
            {
                _ignoreStrings.Add(new Regex(@"\b" + tempString + @"\b", RegexOptions.Compiled | RegexOptions.IgnoreCase));
            }
            WriteIgnoreLists();
        }

        private void AddIgnoreSystem_Click(object sender, EventArgs e)
        {
            if (NewIgnoreSystem.Text.Length <= 0) return;

            IgnoreSystemList.Items.Add(NewIgnoreSystem.Text);
            _ignoreSystems.Add(_solarSystems.Names[NewIgnoreSystem.Text]);
            WriteIgnoreLists();
        }

        private void RemoveIgnoreSystem_Click(object sender, EventArgs e)
        {
            if (IgnoreSystemList.SelectedIndex < 0) return;

            _ignoreSystems.Remove(_solarSystems.Names[(string)IgnoreSystemList.SelectedItem]);
            IgnoreSystemList.Items.RemoveAt(IgnoreSystemList.SelectedIndex);
            WriteIgnoreLists();
        }

        private void NewIgnoreSystem_Leave(object sender, EventArgs e)
        {
            if (NewIgnoreSystem.Text.Length == 0) return;

            foreach (var tempSystem in _solarSystems.SolarSystems.Select(tempSolarSystem => tempSolarSystem.Value).Where(tempSystem => tempSystem.MatchNameRegex(NewIgnoreSystem.Text)))
            {
                NewIgnoreSystem.Text = tempSystem.Name;
                return;
            }

            MessageBox.Show("System \"" + NewIgnoreSystem.Text + "\" not found, try again.", "System Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            NewIgnoreSystem.Text = string.Empty;
        }

        private void MoveAlertUp_Click(object sender, EventArgs e)
        {
            if (AlertList.SelectedIndex == -1) return;

            var selectedIndex = AlertList.SelectedIndex;

            _alertTriggers.Move(AlertList.SelectedIndex, MoveDirection.Up);
            WriteAlertConfig();
            LoadAlertConfig();

            selectedIndex--;
            if (selectedIndex == -1)
                selectedIndex = 0;

            AlertList.SelectedIndex = selectedIndex;
        }

        private void MoveAlertDown_Click(object sender, EventArgs e)
        {
            if (AlertList.SelectedIndex == -1) return;

            var selectedIndex = AlertList.SelectedIndex;

            _alertTriggers.Move(AlertList.SelectedIndex, MoveDirection.Down);
            WriteAlertConfig();
            LoadAlertConfig();

            selectedIndex++;
            if (selectedIndex == AlertList.Items.Count)
                selectedIndex = AlertList.Items.Count - 1;

            AlertList.SelectedIndex = selectedIndex;
        }

        private void MonitorBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorBranch = MonitorBranch.Checked;

            if (MonitorBranch.Checked)
            {
                if (_hiddenPages.ContainsKey("BranchPage"))
                {
                    var insertionIndex = 1;

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["BranchPage"]);
                    _hiddenPages.Remove("BranchPage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("BranchPage");
                _hiddenPages.Add("BranchPage", BranchPage);
                SetChannelTabSize();
            }
        }

        private void MonitorDeklein_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorDeklein = MonitorDeklein.Checked;

            if (MonitorDeklein.Checked)
            {
                if (_hiddenPages.ContainsKey("DekleinPage"))
                {
                    var insertionIndex = UITabControl.TabPages.IndexOfKey("TenalPage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("VenalPage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("FadePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("PureBlindPage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("TributePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ValePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ProvidencePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("DelvePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ConfigPage");

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["DekleinPage"]);
                    _hiddenPages.Remove("DekleinPage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("DekleinPage");
                _hiddenPages.Add("DekleinPage", DekleinPage);
                SetChannelTabSize();
            }
        }

        private void MonitorTenal_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorTenal = MonitorTenal.Checked;

            if (MonitorTenal.Checked)
            {
                if (_hiddenPages.ContainsKey("TenalPage"))
                {
                    var insertionIndex = UITabControl.TabPages.IndexOfKey("VenalPage");
                    
                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("FadePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("PureBlindPage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("TributePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ValePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ProvidencePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("DelvePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ConfigPage");

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["TenalPage"]);
                    _hiddenPages.Remove("TenalPage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("TenalPage");
                _hiddenPages.Add("TenalPage", TenalPage);
                SetChannelTabSize();
            }
        }

        private void MonitorVenal_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorVenal = MonitorVenal.Checked;

            if (MonitorVenal.Checked)
            {
                if (_hiddenPages.ContainsKey("VenalPage"))
                {
                    var insertionIndex = UITabControl.TabPages.IndexOfKey("FadePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("PureBlindPage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("TributePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ValePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ProvidencePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("DelvePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ConfigPage");

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["VenalPage"]);
                    _hiddenPages.Remove("VenalPage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("VenalPage");
                _hiddenPages.Add("VenalPage", VenalPage);
                SetChannelTabSize();
            }
        }

        private void MonitorFade_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorFade = MonitorFade.Checked;

            if (MonitorFade.Checked)
            {
                if (_hiddenPages.ContainsKey("FadePage"))
                {
                    var insertionIndex = UITabControl.TabPages.IndexOfKey("PureBlindPage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("TributePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ValePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ProvidencePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("DelvePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ConfigPage");

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["FadePage"]);
                    _hiddenPages.Remove("FadePage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("FadePage");
                _hiddenPages.Add("FadePage", FadePage);
                SetChannelTabSize();
            }
        }

        private void MonitorPureBlind_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorPureBlind = MonitorPureBlind.Checked;

            if (MonitorPureBlind.Checked)
            {
                if (_hiddenPages.ContainsKey("PureBlindPage"))
                {
                    var insertionIndex = UITabControl.TabPages.IndexOfKey("TributePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ValePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ProvidencePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("DelvePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ConfigPage");

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["PureBlindPage"]);
                    _hiddenPages.Remove("PureBlindPage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("PureBlindPage");
                _hiddenPages.Add("PureBlindPage", PureBlindPage);
                SetChannelTabSize();
            }
        }

        private void MonitorTribute_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorTribute = MonitorTribute.Checked;

            if (MonitorTribute.Checked)
            {
                if (_hiddenPages.ContainsKey("TributePage"))
                {
                    var insertionIndex = UITabControl.TabPages.IndexOfKey("ValePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ProvidencePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("DelvePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ConfigPage");

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["TributePage"]);
                    _hiddenPages.Remove("TributePage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("TributePage");
                _hiddenPages.Add("TributePage", TributePage);
                SetChannelTabSize();
            }
        }

        private void MonitorVale_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorVale = MonitorVale.Checked;

            if (MonitorVale.Checked)
            {
                if (_hiddenPages.ContainsKey("ValePage"))
                {
                    var insertionIndex = UITabControl.TabPages.IndexOfKey("ProvidencePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("DelvePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ConfigPage");

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["ValePage"]);
                    _hiddenPages.Remove("ValePage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("ValePage");
                _hiddenPages.Add("ValePage", ValePage);
                SetChannelTabSize();
            }
        }

        private void MonitorProvidence_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorProvidence = MonitorProvidence.Checked;

            if (MonitorProvidence.Checked)
            {
                if (_hiddenPages.ContainsKey("ProvidencePage"))
                {
                    var insertionIndex = UITabControl.TabPages.IndexOfKey("DelvePage");

                    if (insertionIndex == -1)
                        insertionIndex = UITabControl.TabPages.IndexOfKey("ConfigPage");

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["ProvidencePage"]);
                    _hiddenPages.Remove("ProvidencePage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("ProvidencePage");
                _hiddenPages.Add("ProvidencePage", ProvidencePage);
                SetChannelTabSize();
            }
        }

        private void MonitorDelve_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorDelve = MonitorDelve.Checked;

            if (MonitorDelve.Checked)
            {
                if (_hiddenPages.ContainsKey("DelvePage"))
                {
                    var insertionIndex = UITabControl.TabPages.IndexOfKey("ConfigPage");

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["DelvePage"]);
                    _hiddenPages.Remove("DelvePage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("DelvePage");
                _hiddenPages.Add("DelvePage", DelvePage);
                SetChannelTabSize();
            }
        }

        private void MonitorGOTG_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorGOTG = MonitorGOTG.Checked;

            if (MonitorGOTG.Checked)
            {
                if (_hiddenPages.ContainsKey("GOTGPage"))
                {
                    var insertionIndex = UITabControl.TabPages.IndexOfKey("GOTGPage");
                    if (insertionIndex == -1)
                    {
                        insertionIndex = 1;
                    }

                    UITabControl.TabPages.Insert(insertionIndex, _hiddenPages["GOTGPage"]);
                    _hiddenPages.Remove("GOTGPage");
                    SetChannelTabSize();
                }
            }
            else
            {
                UITabControl.TabPages.RemoveByKey("GOTGPage");
                _hiddenPages.Add("GOTGPage", GOTGPage);
                SetChannelTabSize();
            }
        }

        private void SetChannelTabSize()
        {
            Size tempSize = UITabControl.ItemSize;

            tempSize.Width = (UITabControl.Width - 4) / UITabControl.TabCount;

            UITabControl.ItemSize = tempSize;
        }

        private void MonitorGameLog_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.MonitorGameLog = MonitorGameLog.Checked;
        }
        
        private void AlertBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertBranch = AlertBranch.Checked;
        }

        private void AlertDeklein_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertDeklein = AlertDeklein.Checked;
        }

        private void AlertTenal_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertTenal = AlertTenal.Checked;
        }

        private void AlertVenal_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertVenal = AlertVenal.Checked;
        }

        private void PreserveWindowSize_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.PreserveWindowSize = PreserveWindowSize.Checked;
        }

        private void PreserveWindowPosition_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.PreserveWindowPosition = PreserveWindowPosition.Checked;
        }

        private void PreserveFullScreenStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.PreserveFullScreenStatus = PreserveFullScreenStatus.Checked;
        }

        private void PreserveHomeSystem_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.PreserveHomeSystem = PreserveHomeSystem.Checked;
        }

        private void OverrideLogPath_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.OverrideLogPath = OverrideLogPath.Checked;

            ChooseLogPath.Enabled = OverrideLogPath.Checked;
            LogPath.Enabled = OverrideLogPath.Checked;
        }

        private void PreserveLookAt_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.PreserveLookAt = PreserveLookAt.Checked;
        }

        private void PreserveCameraDistance_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.PreserveCameraDistance = PreserveCameraDistance.Checked;
        }

        private void PreserveSelectedSystems_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.PreserveSelectedSystems = PreserveSelectedSystems.Checked;
        }

        private void DisplayNewFileAlerts_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.DisplayNewFileAlerts = DisplayNewFileAlerts.Checked;
        }

        private void DisplayOpenFileAlerts_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.DisplayOpenFileAlerts = DisplayOpenFileAlerts.Checked;
        }

        private void UpperLimitOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If not nothing and not >=
            if (UpperLimitOperator.SelectedIndex != -1 && UpperLimitOperator.SelectedIndex == 1)
            {
                LowerLimitOperator.Enabled = true;
                LowerAlertRange.Enabled = true;
            }
            else
            {
                LowerLimitOperator.Enabled = false;
                LowerAlertRange.Enabled = false;
                LowerLimitOperator.SelectedIndex = -1;
                LowerAlertRange.Value = 0;
            }
        }

        private void AlertFade_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertFade = AlertFade.Checked;
        }

        private void AlertPureBlind_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertPureBlind = AlertPureBlind.Checked;
        }

        private void AlertTribute_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertTribute = AlertTribute.Checked;
        }

        private void AlertVale_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertVale = AlertVale.Checked;
        }

        private void AlertProvidence_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertProvidence = AlertProvidence.Checked;
        }

        private void AlertDelve_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertDelve = AlertDelve.Checked;
        }

        private void AlertGOTG_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.AlertGOTG = AlertGOTG.Checked;
        }

        private void RemoveSelectedItem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _alertTriggers.RemoveAt(AlertList.SelectedIndex);
            AlertList.Items.RemoveAt(AlertList.SelectedIndex);

            WriteAlertConfig();
            LoadAlertConfig();
        }

        private void PlayRangeAlertSound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_muteSound) return;

            if (RangeAlertSound.SelectedIndex == -1)
                MessageBox.Show("Pick a sound first.", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (RangeAlertSound.SelectedIndex < RangeAlertSound.Items.Count - 1)
                _sounds[(string)RangeAlertSound.SelectedItem].Play();
            else if (RangeAlertSound.SelectedIndex == RangeAlertSound.Items.Count - 1)
                _customSound.Play();
        }

        private void PlayCustomTextAlertSound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_muteSound) return;

            if (CustomTextAlertSound.SelectedIndex == -1)
                MessageBox.Show("Pick a sound first.", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (CustomTextAlertSound.SelectedIndex < CustomTextAlertSound.Items.Count - 1)
                _sounds[(string)CustomTextAlertSound.SelectedItem].Play();
            else if (CustomTextAlertSound.SelectedIndex == CustomTextAlertSound.Items.Count - 1)
                _customSound.Play();
        }

        private void AlertList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AlertList.SelectedIndex >= 0)
            {
                RemoveSelectedItem.Enabled = true;
                PlayAlertSound.Enabled = true;
                EditSelectedItem.Enabled = true;
            }
            else
            {
                RemoveSelectedItem.Enabled = false;
                PlayAlertSound.Enabled = false;
                EditSelectedItem.Enabled = false;
            }
        }

        private void AlertList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_loadingAlerts) return;

            int selectedIndex = e.Index;

            _alertTriggers[e.Index].Enabled = !_alertTriggers[e.Index].Enabled;
            WriteAlertConfig();
            LoadAlertConfig();

            AlertList.SelectedIndex = selectedIndex;
        }

        private void NewRangeAlertType_TextChanged(object sender, EventArgs e)
        {
            if (NewRangeAlertType.SelectedIndex == 2)
            {
                RangeAlertSystem.Enabled = true;
                RangeAlertSystem.Visible = true;
                RangeAlertCharacter.Enabled = false;
                RangeAlertCharacter.Visible = false;
            }
            else if (NewRangeAlertType.SelectedIndex == 3)
            {
                RangeAlertCharacter.Enabled = true;
                RangeAlertCharacter.Visible = true;
                RangeAlertSystem.Enabled = false;
                RangeAlertSystem.Visible = false;
            }
            else
            {
                RangeAlertCharacter.Enabled = false;
                RangeAlertCharacter.Visible = false;
                RangeAlertSystem.Enabled = false;
                RangeAlertSystem.Visible = false;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clearSelectedSystemsMenuItem_Click(object sender, EventArgs e)
        {
            _stickyHighlightSystems.Clear();
            SaveStickySystems();
        }

        private void AnomalyWatcherSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_configLoaded) return;

            if (!_pickingFile && AnomalyWatcherSound.SelectedIndex == AnomalyWatcherSound.Items.Count - 1)
            {
                _pickingFile = true;

                if (CustomSoundPicker.ShowDialog() != DialogResult.OK) return;

                AnomalyWatcherSound.Items.RemoveAt(AnomalyWatcherSound.Items.Count - 1);
                AnomalyWatcherSound.Items.Add(CustomSoundPicker.FileName);
                AnomalyWatcherSound.SelectedIndex = AnomalyWatcherSound.Items.Count - 1;
                _anomalyWatcherSound = LoadSoundFromFile(CustomSoundPicker.FileName);
                _pickingFile = false;
            }

            if (_configLoaded)
            {
                _conf.AnomalyMonitorSoundId = AnomalyWatcherSound.SelectedIndex == AnomalyWatcherSound.Items.Count - 1
                    ? -1
                    : AnomalyWatcherSound.SelectedIndex;
                _conf.AnomalyMonitorSoundPath = AnomalyWatcherSound.Text;
            }
        }

        public void PlayAnomalyWatcherSound()
        {
            if (_muteSound) return;

            if (AnomalyWatcherSound.SelectedIndex == -1)
                MessageBox.Show("Pick a sound first.", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (AnomalyWatcherSound.SelectedIndex < AnomalyWatcherSound.Items.Count - 1)
                _sounds[(string)AnomalyWatcherSound.SelectedItem].Play();
            else if (AnomalyWatcherSound.SelectedIndex == AnomalyWatcherSound.Items.Count - 1)
                _anomalyWatcherSound.Play();
        }

        private void PlayAnomalyWatcherSoundPreview_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PlayAnomalyWatcherSound();
        }

        private void AnomalyWatcherSound_TextChanged(object sender, EventArgs e)
        {

        }

        private void MaxAlertAge_ValueChanged(object sender, EventArgs e)
        {
            _solarSystems.MaxAlertAge = decimal.ToInt32(MaxAlertAge.Value);

            if (_configLoaded)
                _conf.MaxAlertAge = decimal.ToInt32(MaxAlertAge.Value);
        }

        private void MaxAlerts_ValueChanged(object sender, EventArgs e)
        {
            _solarSystems.MaxAlerts = decimal.ToInt32(MaxAlerts.Value);

            if (_configLoaded)
                _conf.MaxAlerts = decimal.ToInt32(MaxAlerts.Value);
        }

        private void ShowAlertAge_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.ShowAlertAge = ShowAlertAge.Checked;
        }

        private void ShowAlertAgeSecs_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.ShowAlertAgeSecs = ShowAlertAgeSecs.Checked;
        }

        private void ShowReportCount_CheckedChanged(object sender, EventArgs e)
        {
            if (_configLoaded)
                _conf.ShowReportCount = ShowReportCount.Checked;
        }

        private void CrashRecursive_Click(object sender, EventArgs e)
        {
            CrashMeRecursive();
        }

        private void CrashException_Click(object sender, EventArgs e)
        {
            using (var testCrash = new StreamWriter(Application.StartupPath + @"\taco-test.txt"))
            {
                File.Delete(Application.StartupPath + @"\taco-test.txt");
            }
        }

        private void muteSoundMenuItem_Click(object sender, EventArgs e)
        {
            _muteSound = muteSoundMenuItem.Checked;
        }
        #endregion Settings UI Events

        /// <summary>
        /// New Code
        /// </summary>

        private bool _bufferIntel;

        private Queue<IntelBuffer> _bufferedIntel = new Queue<IntelBuffer>();

        private void CombinedIntel_Enter(object sender, EventArgs e)
        {
            _bufferIntel = true;
            BufferingIndicator.Text = "Buffering Intel";
            BufferingIndicator.Visible = true;
        }

        private void CombinedIntel_Leave(object sender, EventArgs e)
        {
            while (_bufferedIntel.Count > 0)
            {
                var tempIntelBuffer = _bufferedIntel.Dequeue();

                WriteIntel(tempIntelBuffer.Prefix, tempIntelBuffer.LogLine, tempIntelBuffer.ParseForSystemLinks, true);
            }

            BufferingIndicator.Visible = false;
            _bufferIntel = false;
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            glOut.Focus();
        }

        private void CombinedIntel_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (e.KeyCode == Keys.Z || e.KeyCode == Keys.Enter)
            {
                var selectedText = CombinedIntel.SelectedText.Trim();

                if (selectedText == string.Empty) return;

                if (!_characters.Names.Contains(selectedText))
                {
                    _characters.AddName(selectedText);
                    CharacterList.Items.Add(selectedText);
                }

                CombinedIntel.DeselectAll();

                var textEnd = false;
                var findStart = 0;

                while (!textEnd)
                {
                    var charNameStart = CombinedIntel.Text.IndexOf(selectedText, findStart);

                    if (charNameStart == -1)
                    {
                        textEnd = true;
                    }
                    else
                    {
                        // Insert the name as a link
                        CombinedIntel.InsertLink(selectedText, charNameStart);

                        // Set the start for the next search as the end of the currnet one
                        findStart = charNameStart + selectedText.Length;
                    }
                }

                if (selectedText.Length > 0)
                    Process.Start("http://zkillboard.com/search/" + selectedText);
            }
        }

        private void CombinedIntel_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (_characters.Names.Contains(e.LinkText))
                Process.Start("http://zkillboard.com/search/" + e.LinkText);
            else if (_solarSystems.Names.ContainsKey(e.LinkText))
                ZoomTo(_solarSystems.Names[e.LinkText]);
        }

        private bool _loadingEdit;

        private void EditSelectedItem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _loadingEdit = true;

            if (_alertTriggers[AlertList.SelectedIndex].Type == AlertType.Ranged)
            {
                AlertList.Enabled = false;
                RemoveSelectedItem.Enabled = false;
                PlayAlertSound.Enabled = false;
                MoveAlertUp.Enabled = false;
                MoveAlertDown.Enabled = false;
                AddCustomAlertGroup.Enabled = false;
                EditSelectedItem.Visible = false;
                CancelEditSelectedItem.Visible = true;
                AddNewRangeAlert.Visible = false;
                SaveRangeAlert.Visible = true;

                switch (_alertTriggers[AlertList.SelectedIndex].UpperLimitOperator)
                {
                    case RangeAlertOperator.LessThanOrEqual:
                        UpperLimitOperator.SelectedItem = "<=";
                        break;
                    case RangeAlertOperator.Equal:
                        UpperLimitOperator.SelectedItem = "=";
                        break;
                    default:
                        UpperLimitOperator.SelectedItem = "=";
                        break;
                }

                UpperAlertRange.Value = _alertTriggers[AlertList.SelectedIndex].UpperRange;

                switch (_alertTriggers[AlertList.SelectedIndex].LowerLimitOperator)
                {
                    case RangeAlertOperator.GreaterThanOrEqual:
                        LowerLimitOperator.SelectedItem = ">=";
                        break;
                    case RangeAlertOperator.GreaterThan:
                        LowerLimitOperator.SelectedItem = "=";
                        break;
                    default:
                        LowerLimitOperator.SelectedItem = "=";
                        break;
                }

                LowerAlertRange.Value = _alertTriggers[AlertList.SelectedIndex].UpperLimitOperator == RangeAlertOperator.Equal
                    ? 0
                    : _alertTriggers[AlertList.SelectedIndex].LowerRange;

                switch (_alertTriggers[AlertList.SelectedIndex].RangeTo)
                {
                    case RangeAlertType.Home:
                        NewRangeAlertType.SelectedIndex = 0;
                        break;
                    case RangeAlertType.AnyCharacter:
                        NewRangeAlertType.SelectedIndex = 1;
                        break;
                    case RangeAlertType.System:
                        NewRangeAlertType.SelectedIndex = 2;
                        RangeAlertSystem.Text =
                            _solarSystems.SolarSystems[_alertTriggers[AlertList.SelectedIndex].SystemId].Name;
                        break;
                    case RangeAlertType.Character:
                        NewRangeAlertType.SelectedIndex = 3;
                        RangeAlertCharacter.SelectedIndex =
                            _conf.CharacterId(_alertTriggers[AlertList.SelectedIndex].CharacterName);
                        break;
                }

                RangeAlertSystem.Text = _alertTriggers[AlertList.SelectedIndex].SystemName;

                if (_alertTriggers[AlertList.SelectedIndex].SoundId == -1)
                {
                    RangeAlertSound.Items.RemoveAt(RangeAlertSound.Items.Count - 1);
                    RangeAlertSound.Items.Add(_alertTriggers[AlertList.SelectedIndex].SoundPath);
                    RangeAlertSound.SelectedIndex = RangeAlertSound.Items.Count - 1;
                    _customSound = LoadSoundFromFile(_alertTriggers[AlertList.SelectedIndex].SoundPath);
                }
                else
                {
                    RangeAlertSound.SelectedIndex = _alertTriggers[AlertList.SelectedIndex].SoundId;
                }
            }
            else if (_alertTriggers[AlertList.SelectedIndex].Type == AlertType.Custom)
            {
                AlertList.Enabled = false;
                RemoveSelectedItem.Enabled = false;
                PlayAlertSound.Enabled = false;
                MoveAlertUp.Enabled = false;
                MoveAlertDown.Enabled = false;
                AddRangeAlertGroup.Enabled = false;
                EditSelectedItem.Visible = false;
                CancelEditSelectedItem.Visible = true;
                AddNewCustomAlert.Visible = false;
                SaveCustomAlert.Visible = true;

                NewCustomAlertText.Text = _alertTriggers[AlertList.SelectedIndex].Text;
                CustomAlertRepeatInterval.Value = _alertTriggers[AlertList.SelectedIndex].RepeatInterval;

                if (_alertTriggers[AlertList.SelectedIndex].SoundId == -1)
                {
                    CustomTextAlertSound.Items.RemoveAt(CustomTextAlertSound.Items.Count - 1);
                    CustomTextAlertSound.Items.Add(_alertTriggers[AlertList.SelectedIndex].SoundPath);
                    CustomTextAlertSound.SelectedIndex = RangeAlertSound.Items.Count - 1;
                    _customSound = LoadSoundFromFile(_alertTriggers[AlertList.SelectedIndex].SoundPath);
                }
                else
                {
                    CustomTextAlertSound.SelectedIndex = _alertTriggers[AlertList.SelectedIndex].SoundId;
                }
            }

            _loadingEdit = false;
        }

        private void CancelEditSelectedItem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_alertTriggers[AlertList.SelectedIndex].Type == AlertType.Ranged)
            {
                AlertList.Enabled = true;
                RemoveSelectedItem.Enabled = true;
                PlayAlertSound.Enabled = true;
                MoveAlertUp.Enabled = true;
                MoveAlertDown.Enabled = true;
                AddCustomAlertGroup.Enabled = true;
                EditSelectedItem.Visible = true;
                CancelEditSelectedItem.Visible = false;
                AddNewRangeAlert.Visible = true;
                SaveRangeAlert.Visible = false;

                UpperLimitOperator.SelectedIndex = -1;
                UpperAlertRange.Value = 0;
                LowerLimitOperator.SelectedIndex = -1;
                LowerAlertRange.Value = 0;
                RangeAlertSystem.SelectedIndex = -1;
                RangeAlertSystem.Text = string.Empty;

                RangeAlertCharacter.Text = string.Empty;

                NewRangeAlertType.SelectedIndex = -1;

                RangeAlertSound.SelectedIndex = -1;
                RangeAlertSound.Items.RemoveAt(RangeAlertSound.Items.Count - 1);
                RangeAlertSound.Items.Add("Custom...");
            }
            else if (_alertTriggers[AlertList.SelectedIndex].Type == AlertType.Custom)
            {
                AlertList.Enabled = true;
                RemoveSelectedItem.Enabled = true;
                PlayAlertSound.Enabled = true;
                MoveAlertUp.Enabled = true;
                MoveAlertDown.Enabled = true;
                AddRangeAlertGroup.Enabled = true;
                EditSelectedItem.Visible = true;
                CancelEditSelectedItem.Visible = false;
                AddNewCustomAlert.Visible = true;
                SaveCustomAlert.Visible = false;

                NewCustomAlertText.Text = string.Empty;
                CustomAlertRepeatInterval.Value = 0;

                CustomTextAlertSound.SelectedIndex = -1;
                CustomTextAlertSound.Items.RemoveAt(RangeAlertSound.Items.Count - 1);
                CustomTextAlertSound.Items.Add("Custom...");
            }
        }

        private void SaveRangeAlert_Click(object sender, EventArgs e)
        {
            if (UpperLimitOperator.SelectedIndex == -1)
            {
                MessageBox.Show("Select an upper limit operator (the first drop down box).", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UpperLimitOperator.SelectedIndex > 0 && LowerLimitOperator.SelectedIndex == -1)
            {
                MessageBox.Show("Select a lower limit operator (the second drop down box).", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (NewRangeAlertType.SelectedIndex == -1)
            {
                MessageBox.Show("Select a range alert type.", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (RangeAlertSystem.Text.Trim().Length == 0 && NewRangeAlertType.SelectedIndex == 2)
            {
                MessageBox.Show("Select a system", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (RangeAlertCharacter.Text.Trim().Length == 0 && NewRangeAlertType.SelectedIndex == 3)
            {
                MessageBox.Show("Select a character", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (RangeAlertSound.SelectedIndex == -1)
            {
                MessageBox.Show("Select an alert sound", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var tempIndex = AlertList.SelectedIndex;

            switch ((string)UpperLimitOperator.SelectedItem)
            {
                case "<=":
                    _alertTriggers[tempIndex].UpperLimitOperator = RangeAlertOperator.LessThanOrEqual;
                    break;
                case "=":
                    _alertTriggers[tempIndex].UpperLimitOperator = RangeAlertOperator.Equal;
                    break;
                default:
                    _alertTriggers[tempIndex].UpperLimitOperator = RangeAlertOperator.Equal;
                    break;
            }

            _alertTriggers[tempIndex].UpperRange = Convert.ToInt32(UpperAlertRange.Value);

            switch ((string)LowerLimitOperator.SelectedItem)
            {
                case ">=":
                    _alertTriggers[tempIndex].LowerLimitOperator = RangeAlertOperator.GreaterThanOrEqual;
                    break;
                case ">":
                    _alertTriggers[tempIndex].LowerLimitOperator = RangeAlertOperator.GreaterThan;
                    break;
                default:
                    _alertTriggers[tempIndex].LowerLimitOperator = RangeAlertOperator.GreaterThanOrEqual;
                    break;
            }

            _alertTriggers[tempIndex].LowerRange =
                _alertTriggers[tempIndex].UpperLimitOperator == RangeAlertOperator.Equal
                    ? 0
                    : Convert.ToInt32(LowerAlertRange.Value);

            switch (NewRangeAlertType.SelectedIndex)
            {
                case 0:
                    _alertTriggers[tempIndex].SystemId = -1;
                    _alertTriggers[tempIndex].RangeTo = RangeAlertType.Home;
                    break;
                case 1:
                    _alertTriggers[tempIndex].SystemId = -1;
                    _alertTriggers[tempIndex].RangeTo = RangeAlertType.AnyCharacter;
                    break;
                case 2:
                    _alertTriggers[tempIndex].SystemId = _solarSystems.Names[RangeAlertSystem.Text];
                    _alertTriggers[tempIndex].RangeTo = RangeAlertType.System;
                    break;
                case 3:
                    _alertTriggers[tempIndex].CharacterName = RangeAlertCharacter.Text.Trim();
                    _alertTriggers[tempIndex].RangeTo = RangeAlertType.Character;
                    break;
            }

            _alertTriggers[tempIndex].SystemName = RangeAlertSystem.Text;
            _alertTriggers[tempIndex].SoundId = RangeAlertSound.SelectedIndex ==
                                                              RangeAlertSound.Items.Count - 1
                ? -1
                : RangeAlertSound.SelectedIndex;
            _alertTriggers[tempIndex].SoundPath = (string)RangeAlertSound.SelectedItem;


            AlertList.Enabled = true;
            RemoveSelectedItem.Enabled = true;
            PlayAlertSound.Enabled = true;
            MoveAlertUp.Enabled = true;
            MoveAlertDown.Enabled = true;
            AddCustomAlertGroup.Enabled = true;
            EditSelectedItem.Visible = true;
            CancelEditSelectedItem.Visible = false;
            AddNewRangeAlert.Visible = true;
            SaveRangeAlert.Visible = false;

            UpperLimitOperator.SelectedIndex = -1;
            UpperAlertRange.Value = 0;
            LowerLimitOperator.SelectedIndex = -1;
            LowerAlertRange.Value = 0;
            RangeAlertSystem.SelectedIndex = -1;
            RangeAlertSystem.Text = string.Empty;

            RangeAlertCharacter.Text = string.Empty;

            NewRangeAlertType.SelectedIndex = -1;

            RangeAlertSound.SelectedIndex = -1;
            RangeAlertSound.Items.RemoveAt(RangeAlertSound.Items.Count - 1);
            RangeAlertSound.Items.Add("Custom...");

            WriteAlertConfig();
            LoadAlertConfig();

            AlertList.SelectedIndex = tempIndex;
        }

        private void SaveCustomAlert_Click(object sender, EventArgs e)
        {
            if (NewCustomAlertText.Text.Trim().Length == 0)
            {
                MessageBox.Show("At least enter some text to alert on...", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NewCustomAlertText.Text = string.Empty;
                return;
            }

            if (CustomTextAlertSound.SelectedIndex == -1)
            {
                MessageBox.Show("Select an alert sound", "PEBKAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var tempIndex = AlertList.SelectedIndex;

            _alertTriggers[tempIndex].Type = AlertType.Custom;
            _alertTriggers[tempIndex].Text = NewCustomAlertText.Text;
            _alertTriggers[tempIndex].RepeatInterval = Convert.ToInt32(CustomAlertRepeatInterval.Value);
            _alertTriggers[tempIndex].SoundId =
                CustomTextAlertSound.SelectedIndex == CustomTextAlertSound.Items.Count - 1
                    ? -1
                    : CustomTextAlertSound.SelectedIndex;
            _alertTriggers[tempIndex].SoundPath = (string)CustomTextAlertSound.SelectedItem;

            AlertList.Enabled = true;
            RemoveSelectedItem.Enabled = true;
            PlayAlertSound.Enabled = true;
            MoveAlertUp.Enabled = true;
            MoveAlertDown.Enabled = true;
            AddRangeAlertGroup.Enabled = true;
            EditSelectedItem.Visible = true;
            CancelEditSelectedItem.Visible = false;
            AddNewCustomAlert.Visible = true;
            SaveCustomAlert.Visible = false;

            NewCustomAlertText.Text = string.Empty;
            CustomAlertRepeatInterval.Value = 0;

            CustomTextAlertSound.SelectedIndex = -1;
            CustomTextAlertSound.Items.RemoveAt(RangeAlertSound.Items.Count - 1);
            CustomTextAlertSound.Items.Add("Custom...");

            WriteAlertConfig();
            LoadAlertConfig();

            AlertList.SelectedIndex = tempIndex;
        }

        private void AlertList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                AlertList.SelectedIndex = -1;
                glOut.Focus();
            }

        }

        private void AddLinkedCharacter_Click(object sender, EventArgs e)
        {
            if (NewLinkedCharacter.Text.Trim().Length <= 0) return;

            _characters.AddName(NewLinkedCharacter.Text.Trim());
            CharacterList.Items.Add(NewLinkedCharacter.Text.Trim());

            NewLinkedCharacter.Text = string.Empty;
        }

        private void RemoveLinkedCharacter_Click(object sender, EventArgs e)
        {
            if (CharacterList.SelectedIndex < 0) return;

            _characters.RemoveName(CharacterList.Items[CharacterList.SelectedIndex].ToString());
            CharacterList.Items.RemoveAt(CharacterList.SelectedIndex);
        }
      
    }
}

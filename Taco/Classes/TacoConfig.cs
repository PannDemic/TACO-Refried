using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ProtoBuf;

namespace Taco.Classes
{
    partial class TacoConfig
    {
        private ConfigVer5 _config;

        private readonly int _currentConfigVersion = 5;

        private string _configFile;

        public TacoConfig(string basePath)
        {
            _configFile = basePath + @"\taco.conf";
            if (!File.Exists(_configFile))
            {
                WriteDefaultConfig();
            }

            // ReSharper disable once JoinDeclarationAndInitializer
            bool configCorrupt, oldConfig, upgradeConfig;

            configCorrupt = oldConfig = upgradeConfig = false;

            using (var file = new FileStream(_configFile, FileMode.Open, FileAccess.Read))
            {
                var configVersion = Serializer.DeserializeWithLengthPrefix<ConfigVerion>(file, PrefixStyle.Base128, 1);

                if (configVersion.Version == 5)
                {
                    try
                    {
                        _config = Serializer.DeserializeWithLengthPrefix<ConfigVer5>(file, PrefixStyle.Base128, 2);
                    }
                    catch
                    {
                        configCorrupt = true;
                    }
                    
                }
                else if (configVersion.Version == 4)
                {
                    try
                    {
                        if (File.Exists(_configFile + ".v4.bak"))
                        {
                            File.Delete(_configFile + ".v4.bak");
                        }

                        File.Copy(_configFile, _configFile + ".v4.bak");
                        ConfigVer4 configVer4 = Serializer.DeserializeWithLengthPrefix<ConfigVer4>(file, PrefixStyle.Base128, 2);
                        _config = UpgradeV4ToV5(configVer4);
                        upgradeConfig = true;
                    }
                    catch
                    {
                        configCorrupt = true;
                    }
                    
                }
                else if (configVersion.Version == 3)
                {
                    try
                    {
                        if (File.Exists(_configFile + ".v3.bak"))
                        {
                            File.Delete(_configFile + ".v3.bak");
                        }

                        File.Copy(_configFile, _configFile + ".v3.bak");
                        ConfigVer3 configVer3 = Serializer.DeserializeWithLengthPrefix<ConfigVer3>(file, PrefixStyle.Base128, 2);
                        ConfigVer4 configVer4 = UpgradeV3ToV4(configVer3);
                        _config = UpgradeV4ToV5(configVer4);
                        upgradeConfig = true;
                    }
                    catch
                    {
                        configCorrupt = true;
                    }
                }
                else if (configVersion.Version == 2)
                {
                    try
                    {
                        if (File.Exists(_configFile + ".v2.bak"))
                        {
                            File.Delete(_configFile + ".v2.bak");
                        }

                        File.Copy(_configFile, _configFile + ".v2.bak");
                        ConfigVer2 configVer2 = Serializer.DeserializeWithLengthPrefix<ConfigVer2>(file, PrefixStyle.Base128, 2);
                        ConfigVer3 configVer3 = UpgradeV2ToV3(configVer2);
                        ConfigVer4 configVer4 = UpgradeV3ToV4(configVer3);
                        _config = UpgradeV4ToV5(configVer4);
                        upgradeConfig = true;
                    }
                    catch
                    {
                        configCorrupt = true;
                    }
                }
                else
                {
                    oldConfig = true;
                }
            }

            if (upgradeConfig)
            {
                var messageText = new StringBuilder();
                messageText.AppendLine("Your config file has been upgraded.");
                messageText.AppendLine("It has been backed up in your Taco directory.");
                messageText.AppendLine("You can delete it if you wish, or keep it, up to you really.");

                MessageBox.Show(messageText.ToString(), "Config Upgraded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteConfig();
            }

            if (configCorrupt)
            {
                var messageText = new StringBuilder();
                messageText.AppendLine("It looks like your config file is corrupt.");
                messageText.AppendLine("It will now be backed up, and a new default config created.");
                messageText.AppendLine("Please send taco.conf.corrupt to mcnubblet AT gmail.com so I can try and figure out why.");

                MessageBox.Show(messageText.ToString(), "Corrupt Config Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Move(_configFile, _configFile + ".corrupt");
                WriteDefaultConfig();

                using (var file = new FileStream(_configFile, FileMode.Open, FileAccess.Read))
                {
                    //configVersion = Serializer.DeserializeWithLengthPrefix<ConfigVerion>(file, PrefixStyle.Base128, 1);
                    _config = Serializer.DeserializeWithLengthPrefix<ConfigVer5>(file, PrefixStyle.Base128, 2);
                }
            }

            if (oldConfig)
            {
                var messageText = new StringBuilder();
                messageText.AppendLine("It looks like you have an older (v1) config.");
                messageText.AppendLine("Unfortunately, this can't be imported, sorry.");
                messageText.AppendLine("Your old config will now be backed up, and a new default config created.");

                MessageBox.Show(messageText.ToString(), "Old Config Detected", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                if (File.Exists(_configFile + ".v1.bak"))
                {
                    File.Delete(_configFile + ".v1.bak");
                }

                File.Move(_configFile, _configFile + ".v1.bak");
                WriteDefaultConfig();

                using (var file = new FileStream(_configFile, FileMode.Open, FileAccess.Read))
                {
                    //configVersion = Serializer.DeserializeWithLengthPrefix<ConfigVerion>(file, PrefixStyle.Base128, 1);
                    _config = Serializer.DeserializeWithLengthPrefix<ConfigVer5>(file, PrefixStyle.Base128, 2);
                }
            }
        }

        public void WriteDefaultConfig()
        {
            ConfigVer5 defaultConfig = new ConfigVer5(true);

            using (var file = new FileStream(_configFile, FileMode.Create, FileAccess.Write))
            {
                ConfigVerion temp = new ConfigVerion()
                {
                    Version = _currentConfigVersion
                };

                Serializer.SerializeWithLengthPrefix(file, temp, PrefixStyle.Base128, 1);
            }

            using (var file = File.Open(_configFile, FileMode.Append, FileAccess.Write))
            {
                Serializer.SerializeWithLengthPrefix(file, defaultConfig, PrefixStyle.Base128, 2);
            }            
        }

        public void WriteConfig()
        {
            using (var file = new FileStream(_configFile, FileMode.Truncate, FileAccess.Write))
            {
                ConfigVerion temp = new ConfigVerion()
                {
                    Version = _currentConfigVersion
                };

                Serializer.SerializeWithLengthPrefix(file, temp, PrefixStyle.Base128, 1);
            }

            using (var file = File.Open(_configFile, FileMode.Append, FileAccess.Write))
            {
                Serializer.SerializeWithLengthPrefix(file, _config, PrefixStyle.Base128, 2);
            }
        }

        public bool PreserveWindowPosition
        {
            get { return _config.PreserveWindowPosition; }
            set
            {
                _config.PreserveWindowPosition = value;
                WriteConfig();
            }
        }

        public bool PreserveWindowSize
        {
            get { return _config.PreserveWindowSize; }
            set
            {
                _config.PreserveWindowSize = value;
                WriteConfig();
            }
        }

        public int WindowPositionX
        {
            get { return _config.WindowPositionX; }
            set
            {
                _config.WindowPositionX = value;
                WriteConfig();
            }
        }

        public int WindowPositionY
        {
            get { return _config.WindowPositionY; }
            set
            {
                _config.WindowPositionY = value;
                WriteConfig();
            }
        }

        public int WindowSizeX
        {
            get { return _config.WindowSizeX; }
            set
            {
                _config.WindowSizeX = value;
                WriteConfig();
            }
        }

        public int WindowSizeY
        {
            get { return _config.WindowSizeY; }
            set
            {
                _config.WindowSizeY = value;
                WriteConfig();
            }
        }

        public bool PreserveFullScreenStatus
        {
            get { return _config.PreserveFullScreenStatus; }
            set
            {
                _config.PreserveFullScreenStatus = value;
                WriteConfig();
            }
        }

        public bool IsFullScreen
        {
            get { return _config.IsFullScreen; }
            set
            {
                _config.IsFullScreen = value;
                WriteConfig();
            }
        }

        public bool PreserveHomeSystem
        {
            get { return _config.PreserveHomeSystem; }
            set
            {
                _config.PreserveHomeSystem = value;
                WriteConfig();
            }
        }

        public int HomeSystemId
        {
            get { return _config.HomeSystemId; }
            set
            {
                _config.HomeSystemId = value;
                WriteConfig();
            }
        }

        public bool MonitorBranch
        {
            get { return _config.MonitorBranch; }
            set
            {
                _config.MonitorBranch = value;
                WriteConfig();
            }
        }

        public bool MonitorDeklein
        {
            get { return _config.MonitorDeklein; }
            set
            {
                _config.MonitorDeklein = value;
                WriteConfig();
            }
        }

        public bool MonitorTenal
        {
            get { return _config.MonitorTenal; }
            set
            {
                _config.MonitorTenal = value;
                WriteConfig();
            }
        }

        public bool MonitorVenal
        {
            get { return _config.MonitorVenal; }
            set
            {
                _config.MonitorVenal = value;
                WriteConfig();
            }
        }

        public bool MonitorFade
        {
            get { return _config.MonitorFade; }
            set
            {
                _config.MonitorFade = value;
                WriteConfig();
            }
        }

        public bool MonitorPureBlind
        {
            get { return _config.MonitorPureBlind; }
            set
            {
                _config.MonitorPureBlind = value;
                WriteConfig();
            }
        }

        public bool MonitorTribute
        {
            get { return _config.MonitorTribute; }
            set
            {
                _config.MonitorTribute = value;
                WriteConfig();
            }
        }

        public bool MonitorVale
        {
            get { return _config.MonitorVale; }
            set
            {
                _config.MonitorVale = value;
                WriteConfig();
            }
        }

        public bool MonitorProvidence
        {
            get { return _config.MonitorProvidence; }
            set
            {
                _config.MonitorProvidence = value;
                WriteConfig();
            }
        }

        public bool MonitorDelve
        {
            get { return _config.MonitorDelve; }
            set
            {
                _config.MonitorDelve = value;
                WriteConfig();
            }
        }

        public bool MonitorGameLog
        {
            get { return _config.MonitorGameLog; }
            set
            {
                _config.MonitorGameLog = value;
                WriteConfig();
            }
        }

        public bool AlertBranch
        {
            get { return _config.AlertBranch; }
            set
            {
                _config.AlertBranch = value;
                WriteConfig();
            }
        }

        public bool AlertDeklein
        {
            get { return _config.AlertDeklein; }
            set
            {
                _config.AlertDeklein = value;
                WriteConfig();
            }
        }

        public bool AlertTenal
        {
            get { return _config.AlertTenal; }
            set
            {
                _config.AlertTenal = value;
                WriteConfig();
            }
        }

        public bool AlertVenal
        {
            get { return _config.AlertVenal; }
            set
            {
                _config.AlertVenal = value;
                WriteConfig();
            }
        }

        public bool AlertFade
        {
            get { return _config.AlertFade; }
            set
            {
                _config.AlertFade = value;
                WriteConfig();
            }
        }

        public bool AlertPureBlind
        {
            get { return _config.AlertPureBlind; }
            set
            {
                _config.AlertPureBlind = value;
                WriteConfig();
            }
        }

        public bool AlertTribute
        {
            get { return _config.AlertTribute; }
            set
            {
                _config.AlertTribute = value;
                WriteConfig();
            }
        }

        public bool AlertVale
        {
            get { return _config.AlertVale; }
            set
            {
                _config.AlertVale = value;
                WriteConfig();
            }
        }

        public bool AlertProvidence
        {
            get { return _config.AlertProvidence; }
            set
            {
                _config.AlertProvidence = value;
                WriteConfig();
            }
        }

        public bool AlertDelve
        {
            get { return _config.AlertDelve; }
            set
            {
                _config.AlertDelve = value;
                WriteConfig();
            }
        }

        public bool PreserveCameraDistance
        {
            get { return _config.PreserveCameraDistance; }
            set
            {
                _config.PreserveCameraDistance = value;
                WriteConfig();
            }
        }

        public bool PreserveLookAt
        {
            get { return _config.PreserveLookAt; }
            set
            {
                _config.PreserveLookAt = value;
                WriteConfig();
            }
        }

        public float CameraDistance
        {
            get { return _config.CameraDistance; }
            set
            {
                _config.CameraDistance = value;
                WriteConfig();
            }
        }

        public float LookAtX
        {
            get { return _config.LookAtX; }
            set
            {
                _config.LookAtX = value;
                WriteConfig();
            }
        }

        public float LookAtY
        {
            get { return _config.LookAtY; }
            set
            {
                _config.LookAtY = value;
                WriteConfig();
            }
        }

        public bool OverrideLogPath
        {
            get { return _config.OverrideLogPath; }
            set
            {
                _config.OverrideLogPath = value;
                WriteConfig();
            }
        }

        public string LogPath
        {
            get { return _config.LogPath; }
            set
            {
                _config.LogPath = value;
                WriteConfig();
            }
        }

        public bool PreserveSelectedSystems
        {
            get { return _config.PreserveSelectedSystems; }
            set
            {
                _config.PreserveSelectedSystems = value;
                WriteConfig();
            }
        }

        public int[] SelectedSystems
        {
            get { return _config.SelectedSystems; }
            set
            {
                _config.SelectedSystems = value;
                WriteConfig();
            }
        }

        public AlertTrigger[] AlertTriggers
        {
            get { return _config.AlertTriggers; }
            set
            {
                _config.AlertTriggers = value;
                WriteConfig();
            }
        }

        public string[] IgnoreStrings
        {
            get { return _config.IgnoreStrings; }
            set
            {
                _config.IgnoreStrings = value;
                WriteConfig();
            }
        }

        public int[] IgnoreSystems
        {
            get { return _config.IgnoreSystems; }
            set
            {
                _config.IgnoreSystems = value;
                WriteConfig();
            }
        }

        public bool DisplayNewFileAlerts
        {
            get { return _config.DisplayNewFileAlerts; }
            set
            {
                _config.DisplayNewFileAlerts = value;
                WriteConfig();
            }
        }

        public bool DisplayOpenFileAlerts
        {
            get { return _config.DisplayOpenFileAlerts; }
            set
            {
                _config.DisplayOpenFileAlerts = value;
                WriteConfig();
            }
        }

        private List<string> _characterList = new List<string>();

        public void AddCharacter(string characterName)
        {
            if (characterName.Trim().Length <= 4) return;

            if (!_characterList.Contains(characterName.Trim()))
            {
                _characterList.Add(characterName.Trim());

                var tempCharacterList = new string[_characterList.Count];
                _characterList.CopyTo(tempCharacterList);
                _config.CharacterList = tempCharacterList;

                WriteConfig();
            }
        }

        public List<string> CharacterList
        {
            get
            {
                _characterList.Clear();
                if (_config.CharacterList != null)
                    _characterList.AddRange(_config.CharacterList);
                return _characterList;
            }
        }

        public int CharacterId(string characterName)
        {
            var i = 0;

            foreach (var character in _characterList)
            {
                if (character == characterName)
                    return i;

                i++;
            }

            return -1;
        }

        public bool DisplayCharacterNames
        {
            get { return _config.DisplayCharacterNames; }
            set
            {
                _config.DisplayCharacterNames = value;
                WriteConfig();
            }
        }

        public bool ShowCharacterLocations
        {
            get { return _config.ShowCharacterLocations; }
            set
            {
                _config.ShowCharacterLocations = value;
                WriteConfig();
            }
        }

        public bool CameraFollowCharacter
        {
            get { return _config.CameraFollowCharacter; }
            set
            {
                _config.CameraFollowCharacter = value;
                WriteConfig();
            }
        }

        public int CentreOnCharacter
        {
            get { return _config.CentreOnCharacter; }
            set
            {
                _config.CentreOnCharacter = value;
                WriteConfig();
            }
        }

        public int MapRangeFrom
        {
            get { return _config.MapRangeFrom; }
            set
            {
                _config.MapRangeFrom = value;
                WriteConfig();
            }
        }

        public int AnomalyMonitorSoundId
        {
            get { return _config.AnomalyMonitorSoundId; }
            set
            {
                _config.AnomalyMonitorSoundId = value;
                WriteConfig();
            }
        }

        public string AnomalyMonitorSoundPath
        {
            get { return _config.AnomalyMonitorSoundPath; }
            set
            {
                _config.AnomalyMonitorSoundPath = value;
                WriteConfig();
            }
        }

        public bool ShowAlertAge
        {
            get { return _config.ShowAlertAge; }
            set
            {
                _config.ShowAlertAge = value;
                WriteConfig();
            }
        }

        public bool ShowAlertAgeSecs
        { 
            get { return _config.ShowAlertAgeSecs; }
            set
            {
                _config.ShowAlertAgeSecs = value;
                WriteConfig();
            } 
        }

        public bool ShowReportCount
        {
            get { return _config.ShowReportCount; }
            set
            {
                _config.ShowReportCount = value;
                WriteConfig();
            }
        }

        public int MaxAlertAge
        {
            get { return _config.MaxAlertAge; }
            set
            {
                _config.MaxAlertAge = value;
                WriteConfig();
            }
        }

        public int MaxAlerts
        {
            get { return _config.MaxAlerts; }
            set
            {
                _config.MaxAlerts = value;
                WriteConfig();
            }
        }
    }
}

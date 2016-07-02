using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProtoBuf;

namespace Taco.Classes
{
    internal class TacoCharacters
    {
        private CharactersVer1 _characters;

        private readonly int _currentCharactersVersion = 1;

        private string _characterFile;

        private bool _isLoaded;

        public TacoCharacters(string basePath)
        {
            _characterFile = basePath + @"\characters.conf";
            if (!File.Exists(_characterFile))
            {
                WriteDefaultCharactersFile();
            }

            var fileCorrupt = false;

            using (var file = new FileStream(_characterFile, FileMode.Open, FileAccess.Read))
            {
                var characterFileVersion = Serializer.DeserializeWithLengthPrefix<CharactersVersion>(file, PrefixStyle.Base128, 1);

                if (characterFileVersion.Version == 1)
                {
                    try
                    {
                        _characters = Serializer.DeserializeWithLengthPrefix<CharactersVer1>(file, PrefixStyle.Base128, 2);
                        _isLoaded = true;
                    }
                    catch
                    {
                        fileCorrupt = true;
                    }
                }
            }

            if (fileCorrupt)
            {
                var messageText = new StringBuilder();
                messageText.AppendLine("It looks like your character file is corrupt.");
                messageText.AppendLine("It will now be backed up, and a new default one created.");
                messageText.AppendLine(
                    "Please send characters.conf.corrupt to mcnubblet AT gmail.com so I can try and figure out why.");

                MessageBox.Show(messageText.ToString(), "Corrupt File Detected", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                File.Move(_characterFile, _characterFile + ".corrupt");
                WriteDefaultCharactersFile();

                using (var file = new FileStream(_characterFile, FileMode.Open, FileAccess.Read))
                {
                    _characters = Serializer.DeserializeWithLengthPrefix<CharactersVer1>(file,
                        PrefixStyle.Base128, 2);
                    _isLoaded = true;
                }
            }

            if (_isLoaded)
            {
                if (_characters.Names != null)
                {
                    foreach (var name in _characters.Names)
                    {
                        _nameList.Add(name);
                    }
                }
            }

        }

        private void WriteDefaultCharactersFile()
        {
            CharactersVer1 defaultCharacters = new CharactersVer1(true);

            using (var file = new FileStream(_characterFile, FileMode.Create, FileAccess.Write))
            {
                CharactersVersion temp = new CharactersVersion()
                {
                    Version = _currentCharactersVersion
                };

                Serializer.SerializeWithLengthPrefix(file, temp, PrefixStyle.Base128, 1);
                Serializer.SerializeWithLengthPrefix(file, defaultCharacters, PrefixStyle.Base128, 2);
            }
        }

        private void WriteCharactersFile()
        {
            using (var file = new FileStream(_characterFile, FileMode.Truncate, FileAccess.Write))
            {
                CharactersVersion temp = new CharactersVersion()
                {
                    Version = _currentCharactersVersion
                };

                Serializer.SerializeWithLengthPrefix(file, temp, PrefixStyle.Base128, 1);
                Serializer.SerializeWithLengthPrefix(file, _characters, PrefixStyle.Base128, 2);
            }
        }

        private SortedSet<string> _nameList = new SortedSet<string>();

        public SortedSet<string> Names
        {
            get
            {
                return _nameList;
            }
        }

        public void AddName(string name)
        {
            var tempName = name.Trim();

            if (tempName == string.Empty || _nameList.Contains(tempName)) return;

            _nameList.Add(tempName);
            _characters.Names = _nameList.ToArray();
            WriteCharactersFile();
        }

        public void RemoveName(string name)
        {
            var tempName = name.Trim();

            if (tempName == string.Empty || !_nameList.Contains(tempName)) return;

            _nameList.Remove(tempName);
            _characters.Names = _nameList.ToArray();
            WriteCharactersFile();
        }
    }
}
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NMSSaveDataUtil.Classes
{
    public class FileSetting
    {
        private string _filename = "";
        private string _mode = "thru"; // thru, lock, ctrl
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        public string Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
    }

    public class Settings : INotifyPropertyChanged
    {
        private string _saveFolder;
        private string _backupFolder;
        private bool _enableCamera;
        private short _cameraMoveSpeed;
        private int _cameraRotateDelay;
        private short _cameraRotateSpeed;
        private int _cameraDuration;
        private bool _enablePortal;
        private List<FileSetting> _fileSettings;
        private List<string> _backupTargets;
        private Point _winLocation;
        private Size _winSize;
        private FormWindowState _winState;
        public Settings()
        {
            _saveFolder = String.Empty;
            _backupFolder = String.Empty;
            _cameraMoveSpeed = 32767;
            _cameraRotateDelay = 400;
            _cameraRotateSpeed = -22000;
            _cameraDuration = 21000;
            _enablePortal = false;
            _fileSettings = new List<FileSetting>();
            _backupTargets = new List<string>() { "accountdata.hg", "save.hg", "save2.hg" };
            _winLocation = new Point(0, 0);
            _winSize = new Size(0, 0);
            _winState = FormWindowState.Normal;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public string SaveFolder
        {
            get { return _saveFolder; }
            set
            {
                _saveFolder = value;
                OnPropertyChanged("SaveFolder");
            }
        }
        public string BackupFolder
        {
            get { return _backupFolder; }
            set
            {
                _backupFolder = value;
                OnPropertyChanged("BackupFolder");
            }
        }
        public bool EnableCamera
        {
            get { return _enableCamera; }
            set
            {
                _enableCamera = value;
                OnPropertyChanged("EnableCamera");
            }
        }
        public short CameraMoveSpeed
        {
            get { return _cameraMoveSpeed; }
            set
            {
                _cameraMoveSpeed = value;
                OnPropertyChanged("CameraMoveSpeed");
            }
        }
        public int CameraRotateDelay
        {
            get { return _cameraRotateDelay; }
            set
            {
                _cameraRotateDelay = value;
                OnPropertyChanged("CameraRotateDelay");
            }
        }
        public short CameraRotateSpeed
        {
            get { return _cameraRotateSpeed; }
            set
            {
                _cameraRotateSpeed = value;
                OnPropertyChanged("CameraRotateSpeed");
            }
        }
        public int CameraDuration
        {
            get { return _cameraDuration; }
            set
            {
                _cameraDuration = value;
                OnPropertyChanged("CameraDuration");
            }
        }
        public bool EnablePortal
        {
            get { return _enablePortal; }
            set
            {
                _enablePortal = value;
                OnPropertyChanged("EnablePortal");
            }
        }
        public string[] BackupTargets
        {
            get { return _backupTargets.ToArray(); }
            set
            {
                _backupTargets = value.ToList();
                OnPropertyChanged("BackupTargets");
            }
        }
        public List<FileSetting> FileSettings
        {
            get { return _fileSettings; }
            set
            {
                _fileSettings = value;
                OnPropertyChanged("FileSettings");
            }
        }
        public Point WinLocation
        {
            get { return _winLocation; }
            set
            {
                _winLocation = value;
                OnPropertyChanged("WinLocation");
            }
        }
        public Size WinSize
        {
            get { return _winSize; }
            set
            {
                _winSize = value;
                OnPropertyChanged("WinSize");
            }
        }
        public FormWindowState WinState
        {
            get { return _winState; }
            set
            {
                _winState = value;
                OnPropertyChanged("WinState");
            }
        }

        public string GetFileSetting(string filename)
        {
            FileSetting? saveSetting = _fileSettings.Find(ss => ss.Filename == filename);
            if (saveSetting == null) { return ""; }
            return saveSetting.Mode;
        }

        public void SetFileSetting(string filename, string mode)
        {
            int saveIndex = _fileSettings.FindIndex(ss => ss.Filename == filename);
            if (saveIndex == -1)
            {
                _fileSettings.Add(new FileSetting { Filename = filename, Mode = mode });
            }
            else
            {
                _fileSettings[saveIndex].Mode = mode;
            }
        }

        public static void Save(Settings settings)
        {
            CleanFileSettings(settings);

            XmlSerializer serializer = new(typeof(Settings));

            FileStream fs = new($@"{Directory.GetCurrentDirectory()}\Settings.xml", FileMode.Create);

            serializer.Serialize(fs, settings);
            fs.Close();
        }

        public static Settings Load()
        {
            string xmlPath = $@"{Directory.GetCurrentDirectory()}\Settings.xml";

            if (File.Exists(xmlPath))
            {
                XmlSerializer serializer = new(typeof(Settings));
                FileStream fs = new(xmlPath, FileMode.Open);

                Settings? settings = (Settings?)serializer.Deserialize(fs);
                fs.Close();

                if (settings != null)
                {
                    return settings;
                }
            }
            return new Settings();
        }

        private static void CleanFileSettings(Settings settings)
        {
            settings.FileSettings = settings.FileSettings.Where(ss =>
            {
                if (File.Exists($@"{settings.SaveFolder}\{ss.Filename}") && ss.Mode != "thru") return true;
                return false;
            }).ToList();
        }
    }
}

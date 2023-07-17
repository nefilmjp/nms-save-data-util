using System.ComponentModel;
using System.Xml.Serialization;

namespace NMSSaveDataUtil.Classes
{
    public class Settings : INotifyPropertyChanged
    {
        private string _saveFolder;
        private string _backupFolder;
        private bool _deleteAutosave;
        private bool _disableAutosave;
        private bool _enableCamera;
        private short _cameraMoveSpeed;
        private int _cameraRotateDelay;
        private short _cameraRotateSpeed;
        private int _cameraDuration;
        private List<string> _saveFiles;
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
            _saveFiles = new List<string>() { "accountdata.hg", "save.hg", "save2.hg" };
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
        public bool DeleteAutosave
        {
            get { return _deleteAutosave; }
            set
            {
                _deleteAutosave = value;
                OnPropertyChanged("DeleteAutosave");
            }
        }
        public bool DisableAutosave
        {
            get { return _disableAutosave; }
            set
            {
                _disableAutosave = value;
                OnPropertyChanged("DisableAutosave");
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
        public string[] SaveFiles
        {
            get { return _saveFiles.ToArray(); }
            set
            {
                _saveFiles = value.ToList();
                OnPropertyChanged("SaveFiles");
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

        public static void Save(Settings settings)
        {
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
    }
}

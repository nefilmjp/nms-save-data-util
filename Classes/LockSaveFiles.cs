using NMSSaveDataUtil.Properties;

namespace NMSSaveDataUtil.Classes
{
    internal class LockSaveFiles
    {
        public static bool IsReadonly(FileAttributes attributes)
        {
            if((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static FileAttributes AddAttribute(FileAttributes attributes, FileAttributes attributesToAdd)
        {
            return attributes | attributesToAdd;
        }

        public static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        public static void Lockfile(string fullpath)
        {
            if (!File.Exists(fullpath)) return;

            FileAttributes attr = File.GetAttributes(fullpath);

            if (!IsReadonly(attr))
            {
                FileAttributes newAttr = AddAttribute(attr, FileAttributes.ReadOnly);
                File.SetAttributes(fullpath, newAttr);
            }
        }

        public static void Unlockfile(string fullpath)
        {
            if (!File.Exists(fullpath)) return;

            FileAttributes attr = File.GetAttributes(fullpath);

            if (IsReadonly(attr))
            {
                FileAttributes newAttr = RemoveAttribute(attr, FileAttributes.ReadOnly);
                File.SetAttributes(fullpath, newAttr);
            }
        }

        public static void Init(Settings settings)
        {
            settings.FileSettings.ForEach(ss =>
            {
                if (ss.Mode == "lock")
                {
                    Lockfile(@$"{settings.SaveFolder}\{ss.Filename}");
                    Lockfile(@$"{settings.SaveFolder}\{ss.Filename}");
                }
            });
        }

        public static void Start(Settings settings)
        {
            string targetFolderPath = settings.SaveFolder;
            string[] files = ListFiles.GetChildren(targetFolderPath);

            foreach (string file in files)
            {
                if (!file.EndsWith(".hg")) continue;

                string filename = Path.GetFileName(file).Replace("mf_", "");
                if (settings.GetFileSetting(filename) != "ctrl") continue;

                Lockfile(file);
            }
        }

        public static void Stop(Settings settings)
        {
            string targetFolderPath = settings.SaveFolder;
            string[] files = ListFiles.GetChildren(targetFolderPath);

            foreach (string file in files)
            {
                if (file.EndsWith(".stream")) {
                    File.Delete(file);
                    continue;
                };

                if (!file.EndsWith(".hg")) continue;

                string filename = Path.GetFileName(file).Replace("mf_", "");
                if (settings.GetFileSetting(filename) != "ctrl") continue;

                Unlockfile(file);
            }
        }
    }
}

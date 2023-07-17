namespace NMSSaveDataUtil.Classes
{
    internal class LockSaveFiles
    {
        private static bool IsReadonly(FileAttributes attributes)
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

        private static FileAttributes AddAttribute(FileAttributes attributes, FileAttributes attributesToAdd)
        {
            return attributes | attributesToAdd;
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        public static void Start(string targetFolderPath)
        {
            string[] files = ListFiles.GetChildren(targetFolderPath);

            foreach (string file in files)
            {
                if (!file.EndsWith(".hg")) continue;

                FileAttributes attr = File.GetAttributes(file);

                if(!IsReadonly(attr)) {
                    FileAttributes newAttr = AddAttribute(attr, FileAttributes.ReadOnly);
                    File.SetAttributes(file, newAttr);
                }
            }
        }

        public static void Stop(string targetFolderPath)
        {
            string[] files = ListFiles.GetChildren(targetFolderPath);

            foreach (string file in files)
            {
                if (file.EndsWith(".stream")) {
                    File.Delete(file);
                    continue;
                };

                if (!file.EndsWith(".hg")) continue;

                FileAttributes attr = File.GetAttributes(file);

                if (IsReadonly(attr))
                {
                    FileAttributes newAttr = RemoveAttribute(attr, FileAttributes.ReadOnly);
                    File.SetAttributes(file, newAttr);
                }
            }
        }
    }
}

using System.Diagnostics;

namespace NMSSaveDataUtil.Classes
{
    internal class Archiver
    {
        public static void Compress(string targetFilePath, string[] sourceFiles)
        {
            string sources = $"\"{String.Join("\" \"", sourceFiles)}\"";

            ProcessStartInfo info = new()
            {
                FileName = "7za.exe",
                Arguments = $"a -t7z \"{targetFilePath}\" {sources}",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
            };

            Execute(info, "Backup");
        }

        public static void Decompress(string targetArchive, string targetFolder)
        {
            Debug.WriteLine("targetArchive", targetArchive);
            Debug.WriteLine("targetFolder", targetFolder);
            ProcessStartInfo info = new()
            {
                FileName = "7za.exe",
                Arguments = $"e \"{targetArchive}\" -aoa -o\"{targetFolder}\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
            };

            Execute(info, "Restore");
        }

        public static void Execute(ProcessStartInfo info, string type) {
            ThreadPool.QueueUserWorkItem(delegate {
                Process? p = Process.Start(info);

                if (p is null)
                {
                    MessageBox.Show(
                        $"{type} failed. (7-Zip can not initiate)",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                if (!p.HasExited)
                {
                    p.WaitForExit();
                }

                if (p.ExitCode != 0)
                {
                    MessageBox.Show(
                        $"{type} failed. (7-Zip returns error)",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSSaveDataUtil.Classes
{
    internal class Cache
    {
        /// <summary>
        /// バックアップ用に読み取り専用属性を外したセーブデータを作成
        /// </summary>
        /// <param name="paths">セーブデータのファイルパスの配列</param>
        /// <returns>キャッシュのファイルパスの配列</returns>
        public static string[] Create(string[] paths)
        {
            return paths.ToList().Select(sourcePath =>
            {
                string fileName = Path.GetFileName(sourcePath);
                string cachePath = Path.Join(GetTempDir(), fileName);

                File.Copy(sourcePath, cachePath, true);

                FileAttributes attr = File.GetAttributes(cachePath);
                if (LockSaveFiles.IsReadonly(attr))
                {
                    FileAttributes newAttr = LockSaveFiles.RemoveAttribute(attr, FileAttributes.ReadOnly);
                    File.SetAttributes(cachePath, newAttr);
                }

                return cachePath;
            }).ToArray();
        }

        public static string GetTempDir()
        {
            return Path.Join(Path.GetTempPath(), "NMSSaveDataUtil");
        }

        public static void CreateTempDir()
        {
            DeleteTempDir();
            Directory.CreateDirectory(GetTempDir());
        }

        public static void DeleteTempDir()
        {
            if (Directory.Exists(GetTempDir()))
            {
                Directory.Delete(GetTempDir(), true);
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;

namespace R5_Reloaded_Installer_Library.IO
{
    /// <summary>
    /// "class System.IO.Directory" expansion and other
    /// </summary>
    public static class DirectoryExpansion
    {
        public static string AppDataDirectoryPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string RunningDirectoryPath => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        public static void MoveOverwrite(string sourcePath, string destinationPath)
        {
            DirectoryCopy(sourcePath, destinationPath);
            DeleteAll(sourcePath);
        }

        public static void DeleteAll(string targetDirectoryPath)
        {
            if (!Directory.Exists(targetDirectoryPath)) return;

            string[] filePaths = Directory.GetFiles(targetDirectoryPath);
            foreach (string filePath in filePaths)
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }

            string[] directoryPaths = Directory.GetDirectories(targetDirectoryPath);
            foreach (string directoryPath in directoryPaths) DeleteAll(directoryPath);

            Directory.Delete(targetDirectoryPath, false);
        }

        private static void DirectoryCopy(string sourcePath, string destinationPath)
        {
            var sourceDirectory = new DirectoryInfo(sourcePath);
            var destinationDirectory = new DirectoryInfo(destinationPath);

            if (destinationDirectory.Exists == false)
            {
                destinationDirectory.Create();
                destinationDirectory.Attributes = sourceDirectory.Attributes;
            }

            foreach (var fileInfo in sourceDirectory.GetFiles())
                fileInfo.CopyTo(destinationDirectory.FullName + @"\" + fileInfo.Name, true);

            foreach (var directoryInfo in sourceDirectory.GetDirectories())
                DirectoryCopy(directoryInfo.FullName, destinationDirectory.FullName + @"\" + directoryInfo.Name);
        }
    }
}

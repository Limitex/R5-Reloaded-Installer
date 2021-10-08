using System.IO;

namespace R5_Reloaded_Installer.SharedClass
{
    public static class DirectoryExpansion
    {
		public static void MoveOverwrite(string sourcePath, string destinationPath)
		{
			DirectoryCopy(sourcePath, destinationPath);
			AllDelete(sourcePath);
		}
		public static void AllDelete(string targetDirectoryPath)
        {
            if (!Directory.Exists(targetDirectoryPath)) return;

            string[] filePaths = Directory.GetFiles(targetDirectoryPath);
            foreach (string filePath in filePaths)
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }

            string[] directoryPaths = Directory.GetDirectories(targetDirectoryPath);
            foreach (string directoryPath in directoryPaths) AllDelete(directoryPath);

            Directory.Delete(targetDirectoryPath, false);
        }


		private static void DirectoryCopy(string sourcePath, string destinationPath)
		{
			DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);
			DirectoryInfo destinationDirectory = new DirectoryInfo(destinationPath);

			if (destinationDirectory.Exists == false)
			{
				destinationDirectory.Create();
				destinationDirectory.Attributes = sourceDirectory.Attributes;
			}

			foreach (FileInfo fileInfo in sourceDirectory.GetFiles())
			{
				fileInfo.CopyTo(destinationDirectory.FullName + @"\" + fileInfo.Name, true);
			}

			foreach (DirectoryInfo directoryInfo in sourceDirectory.GetDirectories())
			{
				DirectoryCopy(directoryInfo.FullName, destinationDirectory.FullName + @"\" + directoryInfo.Name);
			}
		}
	}
}

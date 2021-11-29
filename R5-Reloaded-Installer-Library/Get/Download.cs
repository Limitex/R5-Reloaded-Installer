using R5_Reloaded_Installer_Library.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R5_Reloaded_Installer_Library.Get
{
    public class Download : IDisposable
    {
        private static string WorkingDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "R5-Reloaded-Installer");
        
        public Download()
        {
            DirectoryExpansion.CreateOverwrite(WorkingDirectoryPath);
        }

        public void Dispose()
        {
            DirectoryExpansion.DirectoryDelete(WorkingDirectoryPath);
        }
    }
}

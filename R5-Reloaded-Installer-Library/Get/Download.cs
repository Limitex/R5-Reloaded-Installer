using R5_Reloaded_Installer_Library.External;
using R5_Reloaded_Installer_Library.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R5_Reloaded_Installer_Library.Get
{
    public class Download : IDisposable
    {
        private static string WorkingDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "R5-Reloaded-Installer");
        
        private ResourceProcess aria2c;
        private ResourceProcess sevenZip;
        private ResourceProcess transmission;

        public Download()
        {
            DirectoryExpansion.CreateOverwrite(WorkingDirectoryPath);

            aria2c = new ResourceProcess(WorkingDirectoryPath, "aria2c");
            sevenZip = new ResourceProcess(WorkingDirectoryPath, "seven-za");
            transmission = new ResourceProcess(WorkingDirectoryPath, "transmission");

            aria2c.ResourceProcessReceives += new ResourceProcessEventHandler(Output);
            sevenZip.ResourceProcessReceives += new ResourceProcessEventHandler(Output);
            transmission.ResourceProcessReceives += new ResourceProcessEventHandler(Output);

            //aria2c.Run("","");
            //sevenZip.Run("","");
            //transmission.Run("","");
        }

        public void Dispose()
        {
            aria2c.Dispose();
            sevenZip.Dispose();
            transmission.Dispose();
            DirectoryExpansion.DirectoryDelete(WorkingDirectoryPath);
        }

        public void Output(object sender, DataReceivedEventArgs outLine)
        {
            Console.WriteLine(outLine.Data);
        }
    }
}

using R5_Reloaded_Installer_Library.External;
using R5_Reloaded_Installer_Library.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace R5_Reloaded_Installer_Library.Get
{
    public delegate void ProcessEventHandler(string outLine);

    public class Download : IDisposable
    {
        public event ProcessEventHandler? ProcessReceives = null;

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

            aria2c.ResourceProcessReceives += new ResourceProcessEventHandler(Aria2cProcess_EventHandler);
            sevenZip.ResourceProcessReceives += new ResourceProcessEventHandler(SevenZipProcess_EventHandler);
            transmission.ResourceProcessReceives += new ResourceProcessEventHandler(TransmissionProcess_EventHandler);
        }

        public void Dispose()
        {
            aria2c.Dispose();
            sevenZip.Dispose();
            transmission.Dispose();
            DirectoryExpansion.DirectoryDelete(WorkingDirectoryPath);
        }

        public void Aria2cProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (ProcessReceives == null) return;
            if (string.IsNullOrEmpty(outLine.Data)) return;
            var rawLine = Regex.Replace(outLine.Data, @"(\r|\n|(  )|\t|\x1b\[.*?m)", string.Empty);

            if (rawLine[0] == '[')
            {
                var nakedLine = Regex.Replace(rawLine, @"((#.{6}( ))|\[|\])", "");
                if (rawLine.Contains("FileAlloc"))
                    ProcessReceives(nakedLine.Substring(nakedLine.IndexOf("FileAlloc")));
                else
                    ProcessReceives(nakedLine);
            }
            else if (rawLine[0] == '(')
            {
                ProcessReceives(rawLine);
            }
            else if (rawLine.Contains("NOTICE"))
            {
                var nakedLine = Regex.Replace(rawLine, @"([0-9]{2}/[0-9]{2})( )([0-9]{2}:[0-9]{2}:[0-9]{2})( )", string.Empty);
                ProcessReceives(nakedLine);
            }
        }

        public void SevenZipProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (ProcessReceives == null) return;
            if (string.IsNullOrEmpty(outLine.Data)) return;
            var rawLine = Regex.Replace(outLine.Data, @"(\r|\n|(  )|\t|\x1b\[.*?m)", string.Empty);

            ProcessReceives(rawLine);
        }

        public void TransmissionProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (ProcessReceives == null) return;
            if (string.IsNullOrEmpty(outLine.Data)) return;
            var rawLine = Regex.Replace(outLine.Data, @"(\r|\n|(  )|\t|\x1b\[.*?m)", string.Empty);
            
            var nakedLine = Regex.Replace(rawLine, @"(\[([0-9]{4})-([0-9]{2})-([0-9]{2})( )([0-9]{2}):([0-9]{2}):([0-9]{2})\.(.*?)\])( )", string.Empty);
            ProcessReceives(Regex.Replace(nakedLine, @", ul to 0 \(0 kB/s\) \[(0\.00|None)\]", string.Empty));
            if (Regex.Match(nakedLine, "Progress").Success) Thread.Sleep(200);
        }
    }
}

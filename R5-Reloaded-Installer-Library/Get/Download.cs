using R5_Reloaded_Installer_Library.External;
using R5_Reloaded_Installer_Library.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        private string SaveingDirectoryPath;

        private ResourceProcess aria2c;
        private ResourceProcess sevenZip;
        private ResourceProcess transmission;

        public Download(string saveingDirectoryPath)
        {
            SaveingDirectoryPath = saveingDirectoryPath;
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

        public void Run(string address, string? name = null, string? path = null)
        {
            switch (Path.GetExtension(address).ToLower())
            {
                case ".zip":
                case ".7z":
                    break;
                case ".torrent":
                    break;
            }
        }

        private string Aria2c(string address, string? name = null, string? path = null)
        {
            var dirPath = path ?? SaveingDirectoryPath;
            var fileName = name ?? Path.GetFileName(address);
            var argument = " --dir=\"" + dirPath + "\" --out=\"" + fileName + "\" --seed-time=0 --allow-overwrite=true";
            aria2c.Run(address + argument, WorkingDirectoryPath);
            return Path.Combine(dirPath, fileName);
        }

        private string Transmission(string address, string? path = null)
        {
            var dirPath = path ?? SaveingDirectoryPath;
            var argument = " --download-dir \"" + dirPath + "\" --config-dir \"" + WorkingDirectoryPath + "\" -u 0";
            transmission.Run(address + argument, dirPath);
            return Path.Combine(dirPath, Path.GetFileNameWithoutExtension(address));
        }

        private string SevenZip(string address, string? path = null)
        {
            var dirPath = path ?? SaveingDirectoryPath;
            var argument = "-y x " + address;
            sevenZip.Run(argument, dirPath);
            return Path.Combine(dirPath, Path.GetFileNameWithoutExtension(address));
        }

        private void DirectoryFix(string sourceDirName)
        {
            var files = Directory.GetFiles(sourceDirName);
            var dirs = Directory.GetDirectories(sourceDirName);
            if (files.Length == 0 && dirs.Length == 1)
            {
                Directory.Move(dirs[0], sourceDirName + "_buffer");
                Directory.Delete(sourceDirName);
                Directory.Move(sourceDirName + "_buffer", sourceDirName);
            }
        }

        private string FormattingLine(string str) => Regex.Replace(str, @"(\r|\n|(  )|\t|\x1b\[.*?m)", string.Empty);

        private void Aria2cProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (ProcessReceives == null) return;
            if (string.IsNullOrEmpty(outLine.Data)) return;
            var rawLine = FormattingLine(outLine.Data);

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

        private void SevenZipProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (ProcessReceives == null) return;
            if (string.IsNullOrEmpty(outLine.Data)) return;
            var rawLine = FormattingLine(outLine.Data);

            ProcessReceives(rawLine);
        }

        private void TransmissionProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (ProcessReceives == null) return;
            if (string.IsNullOrEmpty(outLine.Data)) return;
            var rawLine = FormattingLine(outLine.Data);

            if (rawLine.Contains("Seeding"))
            {
                transmission.Kill();
                return;
            }

            var nakedLine = Regex.Replace(rawLine, @"(\[([0-9]{4})-([0-9]{2})-([0-9]{2})( )([0-9]{2}):([0-9]{2}):([0-9]{2})\.(.*?)\])( )", string.Empty);
            
            var dirName = Path.GetFileNameWithoutExtension(Regex.Match(((string[])sender)[0], "http.*?(?=( ))").ToString());
            if (!Regex.Match(nakedLine, dirName + ":").Success)
            {
                ProcessReceives(Regex.Replace(nakedLine, @", ul to 0 \(0 kB/s\) \[(0\.00|None)\]", string.Empty));
                Thread.Sleep(200);
            }
        }
    }
}

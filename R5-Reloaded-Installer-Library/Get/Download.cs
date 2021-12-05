﻿using R5_Reloaded_Installer_Library.External;
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

        public void Run(string address, string? name = null, string? path = null, int? appType = null)
        {
            switch (Path.GetExtension(address).ToLower())
            {
                case ".zip":
                case ".7z":
                    var filePath = Aria2c(address, name, path);
                    var dirPath = SevenZip(filePath, name, path);
                    DirectoryFix(dirPath);
                    Console.WriteLine(dirPath);
                    break;
                case ".torrent":
                    string? dir = null;
                    switch (appType) 
                    {
                        case 0:
                        case null:
                            dir = Aria2c(address, name, path);
                            break;
                        case 1:
                            dir = Transmission(address, name, path);
                            break;
                        default:
                            throw new Exception("Specify 0 or 1 for the app type.");
                    }
                    DirectoryFix(dir);
                    Console.WriteLine(dir);
                    break;
                default:
                    throw new Exception("The specified address cannot be downloaded with.");
            }
        }

        private string Aria2c(string address, string? name = null, string? path = null)
        {
            var dirPath = path ?? SaveingDirectoryPath;
            var fileName = name == null ? Path.GetFileName(address) : name + Path.GetExtension(address);
            var dirName = Path.GetFileNameWithoutExtension(fileName);
            var resurtPath = Path.Combine(dirPath, dirName);
            var argument = " --dir=\"" + resurtPath + "\" --out=\"" + fileName + "\" --seed-time=0 --allow-overwrite=true --follow-torrent=mem";
            DirectoryExpansion.CreateOverwrite(resurtPath);
            aria2c.Run(address + argument, dirPath);
            return resurtPath;
        }

        private string Transmission(string address, string? name = null, string? path = null)
        {
            var dirPath = path ?? SaveingDirectoryPath;
            var dirName = name ?? Path.GetFileNameWithoutExtension(address);
            var resurtPath = Path.Combine(dirPath, dirName);
            var argument = " --download-dir \"" + resurtPath + "\" --config-dir \"" + WorkingDirectoryPath + "\" -u 0";
            DirectoryExpansion.CreateOverwrite(resurtPath);
            transmission.Run(address + argument, dirPath);
            return resurtPath;
        }

        private string SevenZip(string address, string? name = null, string? path = null)
        {
            var dirPath = path ?? SaveingDirectoryPath;
            var dirName = name ?? Path.GetFileName(address).Replace(Path.GetExtension(address), string.Empty);
            var resurtPath = Path.Combine(dirPath, dirName);
            var argument = "x -y \"" + address + "\" -o\"" + resurtPath + "\"";
            DirectoryExpansion.CreateOverwrite(resurtPath);
            sevenZip.Run(argument, resurtPath);
            File.Delete(address);
            return resurtPath;
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

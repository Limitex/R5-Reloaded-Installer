using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using R5_Reloaded_Installer.SharedClass;

namespace R5_Reloaded_Installer_GUI
{
    partial class MainForm
    {
        private static bool CreateShortcutFlug;
        private static bool AddStartMenuFlug;
        private static Process aria2c = new Process();
        private static bool aria2ProcessFlug = false;

        private void StartProcessInitialize()
        {
            CreateShortcutFlug = CreateDesktopShortcutCheckBox.Checked;
            AddStartMenuFlug = AddToStartMenuCheckBox.Checked;
        }

        private void ExitProcess()
        {
            ExitFlug = true;
            if (aria2ProcessFlug)
            {
                aria2c.Kill();
                aria2c.Close();
            }
        }

        private void StartProcess()
        {
            new Thread(() => {
                if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), -1, -1, "Preparing...", "Waiting for download process");
                
                var link = WebGetLink.GetApexClientLink();
                var TorrentFile = Path.GetFileName(link);
                var TorrentPath = Path.Combine(InstallPath, TorrentFile.Replace(Path.GetExtension(TorrentFile), ""));

                string detoursR5FileName, scriptsR5FileName;
                using (new Download(InstallPath))
                {
                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), -1, -1, "Downloading detours_r5", null); else return;
                    detoursR5FileName = Download.RunZip(WebGetLink.GetDetoursR5Link(), InstallPath, "detours_r5");
                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), -1, -1, "Downloading scripts_r5", null); else return;
                    scriptsR5FileName = Download.RunZip(WebGetLink.GetScriptsR5Link(), InstallPath, "scripts_r5");

                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), 0, -1, "Preparing to download...", null); else return;

                    var OptionArguments = "";
                    if (ListenPortCheckBox.Checked)
                        OptionArguments += " --listen-port=" + ListenPortNumericUpDown.Value;
                    if (DhtListenPortCheckBox.Checked)
                        OptionArguments += " --dht-listen-port=" + DhtListenPortNumericUpDown.Value;
                    
                    aria2c.StartInfo = new ProcessStartInfo()
                    {
                        FileName = Download.Aria2Path,
                        Arguments = link + " " + Download.Argument + OptionArguments,
                        WorkingDirectory = InstallPath,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };
                    aria2c.EnableRaisingEvents = true;
                    aria2c.Exited += new EventHandler((EHsender, EHe) => { });
                    aria2c.ErrorDataReceived += new DataReceivedEventHandler(EventHandler_ConsoleOutputHandler);
                    aria2c.OutputDataReceived += new DataReceivedEventHandler(EventHandler_ConsoleOutputHandler);
                    aria2ProcessFlug = true;
                    aria2c.Start();
                    aria2c.BeginOutputReadLine();
                    aria2c.BeginErrorReadLine();
                    aria2c.WaitForExit();
                    aria2c.Close();
                }
                if (!ExitFlug)
                {
                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), 100, 0, "Complete.", "Moving the APEX Client.");
                    var BufferPath = Path.Combine(new DirectoryInfo(InstallPath).Parent.FullName, DirName + "_Buffer");
                    Directory.Move(TorrentPath, BufferPath);
                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), null, 20, null, "Moving the detours_r5.");
                    DirectoryExpansion.MoveOverwrite(detoursR5FileName, BufferPath);
                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), null, 40, null, "Moving the scripts_r5.");
                    Directory.Move(scriptsR5FileName, Path.Combine(BufferPath, ScriptsDirectoryPath));
                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), null, 60, null, "Moving the torrent file.");
                    File.Move(Path.Combine(InstallPath, TorrentFile), Path.Combine(BufferPath, TorrentFile));
                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), null, 70, null, "Returning from the buffer Directory.");
                    DirectoryExpansion.AllDelete(InstallPath);
                    Directory.Move(BufferPath, InstallPath);

                    var AppPath = Path.Combine(InstallPath, ExecutableFileName);
                    var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    var startMenuPath = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu))[0];
                    var startmenuShortcutPath = Path.Combine(startMenuPath, "R5-Reloaded");

                    if (CreateShortcutFlug)
                    {
                        if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), null, 80, null, "Add a shortcut to the desktop.");
                        CreateR5Shortcut(desktopPath, AppPath);
                    }
                        
                    if (AddStartMenuFlug)
                    {
                        if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), null, 90, null, "Add a shortcut to the Start menu.");
                        Directory.CreateDirectory(startmenuShortcutPath);
                        CreateR5Shortcut(startmenuShortcutPath, AppPath);
                    }
                }

                if (!ExitFlug) Invoke(new Delegate(() => CompleteProcess()));
            }).Start();
        }
        private void EventHandler_ConsoleOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                Invoke(new Delegate(() =>
                {
                    var rawLine = Regex.Replace(outLine.Data, @"(\r|\n|(  )|\t)", "");
                    if(LogForm.Visible) LogFormRichTexBox.AppendText(rawLine + "\n");

                    if (rawLine[0] == '[')
                    {
                        var nakedLine = Regex.Replace(rawLine, @"((#.*?( ))|\[|\])", "");
                        
                        if (nakedLine.Contains("ETA:"))
                        {
                            var DegPercent = int.Parse(Regex.Match(nakedLine, @"(?<=\().*?(?=%\))").Value);

                            var leftTimeRaw = Regex.Match(nakedLine, @"ETA:.*").Value;
                            var leftTimeVal = Regex.Match(nakedLine, @"(?<=ETA:).*").Value;

                            DownloadStatusLabel.Text = Regex.Replace(nakedLine, leftTimeRaw, "");
                            TimeLeftLabel.Text = leftTimeVal + " : Time left.";
                            SetStatus(DegPercent, -1, null, null);
                        }
                        else
                        {
                            DownloadStatusLabel.Text = nakedLine;
                            TimeLeftLabel.Text = "in preparation : Time Left.";
                        }
                    }
                }));
            }
        }

        private void CreateR5Shortcut(string path, string LinkDestination)
        {
            GetInstalledApps.CreateShortcut(path, "R5-Reloaded", LinkDestination, "");
            GetInstalledApps.CreateShortcut(path, "R5-Reloaded (Debug)", LinkDestination, "-debug");
            GetInstalledApps.CreateShortcut(path, "R5-Reloaded (Release)", LinkDestination, "-release");
            GetInstalledApps.CreateShortcut(path, "R5-Reloaded (Dedicated)", LinkDestination, "-dedicated");
            GetInstalledApps.CreateShortcut(path, "Scripts", Path.Combine(InstallPath, ScriptsDirectoryPath), "");
        }

        private delegate void SetStatusDelgete(int dpValue, int opValue, string dsText, string osText);
        private void SetStatus(int dpValue = -1, int opValue = -1, string dsText = null, string osText = null)
        {
            if (dpValue != -1) DownloadProgressBar.Value = dpValue;
            if (opValue != -1) OverallProgressBar.Value = opValue;
            if (dsText != null) DownloadStatusLabel.Text = dsText;
            if (osText != null) OverallStatusLabel.Text = osText;
        }
    }
}

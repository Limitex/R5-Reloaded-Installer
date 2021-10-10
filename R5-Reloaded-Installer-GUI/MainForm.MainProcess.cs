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
                    string vs = Regex.Replace(outLine.Data, @"(\r|\n|(  )|\t)", "");
                    if (vs[0] == '[')
                    {
                        var match = Regex.Match(vs, @"\[(.*?)\]").Value;

                        if (match.Contains("ETA:"))
                        {
                            var GB = Regex.Match(match, @"(?<=( )).*?(?=\()").Value;
                            var PA = Regex.Match(match, @"(?<=\().*?(?=%\))").Value;
                            var CN = Regex.Match(match, @"(?<=CN:).*?(?=( ))").Value;
                            var SD = Regex.Match(match, @"(?<=SD:).*?(?=( ))").Value;
                            var DL = Regex.Match(match, @"(?<=DL:).*?(?=( ))").Value;
                            var UL = Regex.Match(match, @"(?<=UL:).*?(?=( ))").Value;
                            var ET = Regex.Match(match, @"(?<=ETA:).*?(?=(\]))").Value;

                            TimeLeftLabel.Text = ET + " : Time left";
                            SetStatus(int.Parse(PA), -1, null, null);
                            DownloadStatusLabel.Text = "Size : " + GB + " , Speed : " + DL;
                        }
                        else
                        {
                            var mat = Regex.Match(vs.Remove(0, 1), @"(?<=(\[)).*?(?=\])").Value;
                            var exc = Regex.Match(mat, @"#(.*?)( )").Value;
                            if (mat.Contains(exc) && (exc.Length != 0))
                            {
                                DownloadStatusLabel.Text = mat.Replace(exc, " ");
                                TimeLeftLabel.Text = "in preparation : TimeLeft.";
                            }
                                
                            else if (!match.Contains("SEED"))
                            {
                                DownloadStatusLabel.Text = "Please wait for a while until you can connect to the tracker.";
                                TimeLeftLabel.Text = "Not connected to tracker.";
                            }
                                    
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

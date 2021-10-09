using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
                Invoke(new SetStatusDelgete(SetStatus), -1, -1, "Preparing...", "Waiting for download process");
                
                var link = WebGetLink.GetApexClientLink();
                var TorrentFile = Path.GetFileName(link);
                var TorrentPath = Path.Combine(InstallPath, TorrentFile.Replace(Path.GetExtension(TorrentFile), ""));

                string detoursR5FileName, scriptsR5FileName;
                using (new Download(InstallPath))
                {
                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), 1, -1, "Downloading detours_r5", null);
                    detoursR5FileName = Download.RunZip(WebGetLink.GetDetoursR5Link(), InstallPath, "detours_r5");
                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), 2, -1, "Downloading scripts_r5", null);
                    scriptsR5FileName = Download.RunZip(WebGetLink.GetScriptsR5Link(), InstallPath, "scripts_r5");

                    if (!ExitFlug) Invoke(new SetStatusDelgete(SetStatus), 3, -1, "Preparing to download...", null);

                    aria2c.StartInfo = new ProcessStartInfo()
                    {
                        FileName = Download.Aria2Path,
                        Arguments = link + " " + Download.Argument,
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
                var BufferPath = Path.Combine(new DirectoryInfo(InstallPath).Parent.FullName, DirName + "_Buffer"); // DirName
                Directory.Move(TorrentPath, BufferPath);
                DirectoryExpansion.MoveOverwrite(detoursR5FileName, BufferPath);
                Directory.Move(scriptsR5FileName, Path.Combine(BufferPath, ScriptsDirectoryPath));
                System.IO.File.Move(Path.Combine(InstallPath, TorrentFile), Path.Combine(BufferPath, TorrentFile));
                Directory.Delete(InstallPath);
                Directory.Move(BufferPath, InstallPath);

                var AppPath = Path.Combine(InstallPath, ExecutableFileName);
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var startMenuPath = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu))[0];
                var startmenuShortcutPath = Path.Combine(startMenuPath, "R5-Reloaded");

                if(CreateShortcutFlug)
                    CreateR5Shortcut(desktopPath, AppPath);

                if (AddStartMenuFlug)
                {
                    Directory.CreateDirectory(startmenuShortcutPath);
                    CreateR5Shortcut(startmenuShortcutPath, AppPath);
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
                    string vs = outLine.Data;
                    if (vs[0] == '[')
                    {
                        DownloadStatusLabel.Text = outLine.Data;
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

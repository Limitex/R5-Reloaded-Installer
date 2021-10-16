using R5_Reloaded_Installer_Library.Get;
using R5_Reloaded_Installer_Library.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R5_Reloaded_Installer_GUI
{
    public delegate void Delegate();

    public partial class MainForm : Form
    {
        public static string FinalDirectoryName = "R5-Reloaded";
        public static string ScriptsDirectoryPath = Path.Combine("platform", "scripts");
        private static string ExecutableFileName = "r5reloaded.exe";

        public static bool IsRunning = true;

        public static Download download;

        public MainForm()
        {
            InitializeComponent();
            new ButtonOperation(this, MainTask_StartInstall);
            new GetSizeAndPath(this);
            new LogWindow(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsRunning)
            {
                var dr = MessageBox.Show("Do you want to quit?", "Warning",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK) e.Cancel = true;
                else
                {
                    IsRunning = false;
                    if (download != null) download.ProcessKill();
                }
            }
        }

        private void MainTask_StartInstall(object sender, StartInstallEventArgs e)
        {
            new Thread(() => {
                string detoursR5FilePath, scriptsR5FilePath, apexClientFilePath;
                using (download = new Download(e.InstallationPath))
                {
                    download.WebClientReceives += new WebClientProcessEventHandler(WebClient_EventHandler);
                    download.Aria2ProcessReceives += new Aria2ProcessEventHandler(Aria2Process_EventHandler);
                    detoursR5FilePath = download.RunZip(e.Detours_R5URL, "detours_r5");
                    scriptsR5FilePath = download.RunZip(e.Scripts_R5URL, "scripts_r5");
                    apexClientFilePath = download.RunTorrent(e.ApexClientURL, "apex_client");
                }
                if (IsRunning)
                {
                    var TorrentFile = Path.GetFileName(e.ApexClientURL);
                    var BufferPath = Path.Combine(new DirectoryInfo(e.InstallationPath).Parent.FullName, FinalDirectoryName + "_Buffer");
                    Directory.Move(apexClientFilePath, BufferPath);
                    DirectoryExpansion.MoveOverwrite(detoursR5FilePath, BufferPath);
                    Directory.Move(scriptsR5FilePath, Path.Combine(BufferPath, ScriptsDirectoryPath));
                    File.Move(Path.Combine(e.InstallationPath, TorrentFile), Path.Combine(BufferPath, TorrentFile));
                    DirectoryExpansion.DeleteAll(e.InstallationPath);
                    Directory.Move(BufferPath, e.InstallationPath);

                    var AppPath = Path.Combine(e.InstallationPath, ExecutableFileName);
                    var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    var startMenuPath = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu))[0];
                    var startmenuShortcutPath = Path.Combine(startMenuPath, "R5-Reloaded");
                    var scriptsPath = Path.Combine(e.InstallationPath, ScriptsDirectoryPath);

                    if (e.CreateDesktopShortcut)
                    {
                        CreateR5Shortcut(desktopPath, AppPath, scriptsPath);
                    }
                    if (e.AddShortcutToStartMenu)
                    {
                        Directory.CreateDirectory(startmenuShortcutPath);
                        CreateR5Shortcut(startmenuShortcutPath, AppPath, scriptsPath);
                    }
                }
                if (IsRunning) Invoke(new Delegate(() => NextButton.Enabled = true));
            }).Start();
        }

        private static float LogTimer = 0;
        private void WebClient_EventHandler(object sender, DownloadProgressChangedEventArgs e)
        {
            if (IsRunning) Invoke(new Delegate(() =>
            {
                LogTimer += R5_Reloaded_Installer_Library.Other.Time.deltaTime;
                var parcentage = e.ProgressPercentage;
                if (LogTimer > 0.1f || parcentage == 100)
                {
                    LogTimer = 0;
                    var data = (string[])sender;
                    var fileName = Path.GetFileNameWithoutExtension(data[1]);
                    var fileExt = Path.GetExtension(data[1]).Replace(".", string.Empty).ToUpper();
                    var received = Math.Round(FileExpansion.ByteToKByte(e.BytesReceived) * 1000f) / 1000f;
                    var total = Math.Round(FileExpansion.ByteToKByte(e.TotalBytesToReceive) * 1000f) / 1000f;

                    if (parcentage == 100) Thread.Sleep(1);
                    LogWindow.WriteLine(
                        "Download " + fileName + " (" + fileExt + ") >> " +
                        string.Format("{0,8}", received.ToString("0.000")) +
                        "KB/" + string.Format("{0,8}", total.ToString("0.000")) +
                        "KB (" + string.Format("{0,3}", parcentage) + "%)");
                    if (parcentage == 100) LogWindow.WriteLine("(OK)"); ;
                }
            }));
        }

        private void Aria2Process_EventHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (IsRunning) Invoke(new Delegate(() =>
            {
                if (string.IsNullOrEmpty(outLine.Data)) return;

                var rawLine = Regex.Replace(outLine.Data, @"(\r|\n|(  )|\t)", string.Empty);

                if (rawLine[0] == '[')
                {
                    var nakedLine = Regex.Replace(rawLine, @"((#.{6}( ))|\[|\])", "");
                    if (rawLine.Contains("FileAlloc"))
                    {
                        LogWindow.WriteLine(nakedLine.Substring(nakedLine.IndexOf("FileAlloc")));
                    }
                    else
                    {
                        LogWindow.WriteLine(nakedLine);
                    }
                }
                else if (rawLine[0] == '(')
                {
                    LogWindow.WriteLine(rawLine);
                }
                else if (rawLine.Contains("NOTICE"))
                {
                    var nakedLine = Regex.Replace(rawLine, @"([0-9]{2}/[0-9]{2})( )([0-9]{2}:[0-9]{2}:[0-9]{2})( )", string.Empty);
                    LogWindow.WriteLine(nakedLine);
                }
            }));
        }

        private void CreateR5Shortcut(string path, string LinkDestination, string scriptsPath)
        {
            FileExpansion.CreateShortcut(path, "R5-Reloaded", LinkDestination, "");
            FileExpansion.CreateShortcut(path, "R5-Reloaded (Debug)", LinkDestination, "-debug");
            FileExpansion.CreateShortcut(path, "R5-Reloaded (Release)", LinkDestination, "-release");
            FileExpansion.CreateShortcut(path, "R5-Reloaded (Dedicated)", LinkDestination, "-dedicated");
            FileExpansion.CreateShortcut(path, "Scripts", scriptsPath, "");
        }
    }
}

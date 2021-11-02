using R5_Reloaded_Installer_Library.Get;
using R5_Reloaded_Installer_Library.IO;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace R5_Reloaded_Installer_GUI
{
    public delegate void Delegate();

    public partial class MainForm : Form
    {
        public static string FinalDirectoryName = "R5-Reloaded";
        public static string ScriptsDirectoryPath = Path.Combine("platform", "scripts");
        public static string ExecutableFileName = "r5reloaded.exe";

        public static bool IsRunning = true;

        public static Download download;

        public MainForm()
        {
            InitializeComponent();
            new ButtonOperation(this, MainTask_StartInstall);
            new GetSizeAndPath(this);
            new LogWindow(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var applicationList = InstalledApps.DisplayNameList();
            if (!(applicationList.Contains("Origin") && applicationList.Contains("Apex Legends")))
            {
                var dr = MessageBox.Show("\'Origin\' or \'Apex Legends\' is not installed.\n" +
                    "Do you want to continue?\n" +
                    "R5 Reloaded cannot be run without \'Origin\' and \'Apex Legends\' installed.",
                    "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Cancel)
                {
                    IsRunning = false;
                    Application.Exit();
                }
            }
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
            new Thread(() =>
            {
                string detoursR5FilePath, scriptsR5FilePath, apexClientFilePath;
                DownloadLogWrite("Preparing to download.", 0);
                using (download = new Download(DownloadProgramType.Transmission, e.InstallationPath))
                {
                    download.WebClientReceives += new WebClientProcessEventHandler(WebClient_EventHandler);
                    download.Aria2ProcessReceives += new Aria2ProcessEventHandler(Aria2Process_EventHandler);
                    download.TransmissionProcessReceives += new TransmissionProcessEventHandler(TransmissionProcess_EventHandler);
                    detoursR5FilePath = download.RunZip(e.Detours_R5URL, "detours_r5");
                    scriptsR5FilePath = download.RunZip(e.Scripts_R5URL, "scripts_r5");
                    DownloadLogWrite("Preparing to start torrent.", 0);
                    apexClientFilePath = download.RunTorrentOfTransmission(e.ApexClientURL, "apex_client");
                }
                DownloadLogWrite("Complete.", 100);
                if (IsRunning)
                {
                    OverallLogWrite("Moving the APEX Client.", 0);
                    var TorrentFile = Path.GetFileName(e.ApexClientURL);
                    var BufferPath = Path.Combine(new DirectoryInfo(e.InstallationPath).Parent.FullName, FinalDirectoryName + "_Buffer");
                    Directory.Move(apexClientFilePath, BufferPath);

                    OverallLogWrite("Moving the detours_r5.", 20);
                    DirectoryExpansion.MoveOverwrite(detoursR5FilePath, BufferPath);

                    OverallLogWrite("Moving the detours_r5.", 40);
                    Directory.Move(scriptsR5FilePath, Path.Combine(BufferPath, ScriptsDirectoryPath));

                    OverallLogWrite("Moving the torrent file.", 60);
                    File.Move(Path.Combine(e.InstallationPath, TorrentFile), Path.Combine(BufferPath, TorrentFile));

                    OverallLogWrite("Returning from the buffer Directory.", 80);
                    DirectoryExpansion.DeleteAll(e.InstallationPath);
                    Directory.Move(BufferPath, e.InstallationPath);

                    var AppPath = Path.Combine(e.InstallationPath, ExecutableFileName);
                    var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    var startMenuPath = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu))[0];
                    var startmenuShortcutPath = Path.Combine(startMenuPath, "R5-Reloaded");
                    var scriptsPath = Path.Combine(e.InstallationPath, ScriptsDirectoryPath);

                    if (e.CreateDesktopShortcut || e.AddShortcutToStartMenu)
                        OverallLogWrite("Creating a shortcut.", 90);
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
                if (IsRunning) Invoke(new Delegate(() =>
                {
                    DownloadProgressBar.Value = 100;
                    OverallProgressBar.Value = 100;
                    DownloadLogLabel.Text = "Complete.";
                    OverallLogLabel.Text = "Complete.";
                    NextButton.Enabled = true;
                }));
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

                    var logText = "Download " + fileName + " (" + fileExt + ") >> " +
                        string.Format("{0,8}", received.ToString("0.000")) +
                        "KB/" + string.Format("{0,8}", total.ToString("0.000")) +
                        "KB (" + string.Format("{0,3}", parcentage) + "%)";

                    if (fileExt != "TORRENT")
                        DownloadLogLabel.Text = logText;


                    if (parcentage == 100) Thread.Sleep(1);
                    LogWindow.WriteLine(logText);
                    if (parcentage == 100) LogWindow.WriteLine("(OK)"); ;
                }
            }));
        }

        private void Aria2Process_EventHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (IsRunning) Invoke(new Delegate(() =>
            {
                if (string.IsNullOrEmpty(outLine.Data)) return;
                var rawLine = Regex.Replace(outLine.Data, @"(\r|\n|(  )|\t|\x1b\[.*?m)", string.Empty);
                LogWindow.WriteLine(rawLine);

                if (rawLine[0] != '[') return;

                var nakedLine = Regex.Replace(rawLine, @"((#.{6}( ))|\[|\])", "");
                if (!nakedLine.Contains("FileAlloc"))
                {
                    if (nakedLine.Contains("ETA:"))
                    {
                        var DegPercent = int.Parse(Regex.Match(nakedLine, @"(?<=\().*?(?=%\))").Value);

                        var leftTimeRaw = Regex.Match(nakedLine, @"ETA:.*").Value;
                        var leftTimeVal = Regex.Match(nakedLine, @"(?<=ETA:).*").Value;

                        DownloadLogLabel.Text = Regex.Replace(nakedLine, leftTimeRaw, "");
                        TimeLeftLabel.Text = leftTimeVal + " : Time left.";
                        DownloadProgressBar.Value = DegPercent;
                    }
                    else
                    {
                        DownloadLogLabel.Text = nakedLine + " >> Looking for a seeder...";
                        TimeLeftLabel.Text = "in preparation : Time Left.";
                    }
                }
                else
                {
                    DownloadLogLabel.Text = nakedLine.Substring(nakedLine.IndexOf("FileAlloc"));
                }

            }));
        }

        private void TransmissionProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (string.IsNullOrEmpty(outLine.Data)) return;
                var TorrentFileName = ((string[])sender)[2];
            var rawLine = Regex.Replace(outLine.Data, @"(\r|\n|(  )|\t|\x1b\[.*?m)", string.Empty);
            var nakedLine = Regex.Replace(rawLine, @"(\[([0-9]{4})-([0-9]{2})-([0-9]{2})( )([0-9]{2}):([0-9]{2}):([0-9]{2})\.(.*?)\])( )", string.Empty);
                
            if (!Regex.Match(nakedLine, TorrentFileName + @":").Success)
            {
                if (IsRunning) Invoke(new Delegate(() =>
                {
                    nakedLine = Regex.Replace(nakedLine, @", ul to 0 \(0 kB/s\) \[(0\.00|None)\]", string.Empty);
                    LogWindow.WriteLine(nakedLine);
                    if (nakedLine.Contains("Progress:"))
                    {
                        float speed = 0;
                        switch (Regex.Match(nakedLine, @".(?=(B/s\)))").Value)
                        {
                            case "k":
                                speed = float.Parse(Regex.Match(nakedLine, @"(?<=( )\().*?(?=(kB/s\)))").Value) / 1000f;
                                break;
                            case "M":
                                speed = float.Parse(Regex.Match(nakedLine, @"(?<=( )\().*?(?=(MB/s\)))").Value);
                                break;
                            case "G":
                                speed = float.Parse(Regex.Match(nakedLine, @"(?<=( )\().*?(?=(GB/s\)))").Value) * 1000f;
                                break;
                            default:
                                speed = 0;
                                break;
                        }
                        var DegPercent = float.Parse(Regex.Match(nakedLine, @"(?<=Progress:).*?(?=%)").Value);

                        var filesize = float.Parse(((string[])sender)[3]);
                        var leftTime = new TimeSpan(0, 0, (int)(filesize / speed)).ToString(@"hh\:mm\:ss");
                        if (speed == 0) leftTime = "infinity";

                        DownloadLogLabel.Text = nakedLine;
                        TimeLeftLabel.Text = leftTime + " : Time left.";
                        DownloadProgressBar.Value = (int)DegPercent;
                    }
                    else
                    {
                        DownloadLogLabel.Text = nakedLine;
                    }
                }));
                if (IsRunning) Thread.Sleep(200);
            }
        }

        private void DownloadLogWrite(string text, int progressValue)
        {
            if (IsRunning) Invoke(new Delegate(() =>
            {
                DownloadLogLabel.Text = text;
                DownloadProgressBar.Value = progressValue;
            }));
        }

        private void OverallLogWrite(string text, int progressValue)
        {
            if (IsRunning) Invoke(new Delegate(() =>
            {
                OverallLogLabel.Text = text;
                OverallProgressBar.Value = progressValue;
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

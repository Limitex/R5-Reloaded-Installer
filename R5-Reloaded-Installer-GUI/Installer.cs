using R5_Reloaded_Installer_Library.Exclusive;
using R5_Reloaded_Installer_Library.Get;
using R5_Reloaded_Installer_Library.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace R5_Reloaded_Installer_GUI
{
    public class Installer
    {
        private MainForm mainForm;
        private const float FileSize_GB = 42f;
        public delegate void Delegate();
        public static readonly string DirName = "R5-Reloaded";
        private static readonly string ScriptsDirectoryPath = Path.Combine("platform", "scripts");
        private static readonly string WorldsEdgeAfterDarkPath = "package";
        private static readonly string ExecutableFileName = "r5reloaded.exe";

        public Installer(MainForm form)
        {
            mainForm = form;
            mainForm.InstallButton.Click += new EventHandler(InstallButton_Click);
        }

        private void ControlEnabled(bool status)
        {
            mainForm.BrowseButton.Enabled = status;
            mainForm.InstallButton.Enabled = status;
            mainForm.PathSelectTextBox.Enabled = status;
            mainForm.SelectFileDownloaderComboBox.Enabled = status;
            mainForm.SelectTorrentDownloaderComboBox.Enabled = status;
            mainForm.CreateDesktopShortcutCheckBox.Enabled = status;
            mainForm.AddToStartMenuShortcutCheckBox.Enabled = status;
        }

        private void CreateR5Shortcut(string path, string LinkDestination, string scriptsPath)
        {
            FileExpansion.CreateShortcut(path, "R5-Reloaded", LinkDestination, "");
            FileExpansion.CreateShortcut(path, "R5-Reloaded (Debug)", LinkDestination, "-debug");
            FileExpansion.CreateShortcut(path, "R5-Reloaded (Release)", LinkDestination, "-release");
            FileExpansion.CreateShortcut(path, "R5-Reloaded (Dedicated)", LinkDestination, "-dedicated");
            FileExpansion.CreateShortcut(path, "Scripts", scriptsPath, "");
        }

        private void InstallButton_Click(object? sender, EventArgs e)
        {
            ControlEnabled(false);

            var installPath = mainForm.PathSelectTextBox.Text ?? string.Empty;
            var downloadType_file = mainForm.SelectFileDownloaderComboBox.Text;
            var downloadType_torrent = mainForm.SelectTorrentDownloaderComboBox.Text;
            var shortcutCreate_desktop = mainForm.CreateDesktopShortcutCheckBox.Checked;
            var shortcutCreate_startmenu = mainForm.AddToStartMenuShortcutCheckBox.Checked;

            if (!Path.IsPathRooted(installPath))
            {
                MessageBox.Show("The file name is incorrect", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ControlEnabled(true);
                return;
            }

            var pathRoot = Path.GetPathRoot(installPath);
            if (pathRoot == null)
            {
                ControlEnabled(true);
                return;
            }
            var driveFreeSpace_GB = new DriveInfo(pathRoot).AvailableFreeSpace / 1024f / 1024f / 1024f;

            if (driveFreeSpace_GB < FileSize_GB)
            {
                var dr = MessageBox.Show("The disk size is less than " + FileSize_GB + "GB.\n" +
                    "Do you want to continue?", "Warning", 
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (dr == DialogResult.Cancel)
                {
                    ControlEnabled(true);
                    return;
                }
            }


            if (!Directory.Exists(installPath))
                Directory.CreateDirectory(installPath);
            else
            {
                var fileCount = Directory.GetFiles(installPath, "*", SearchOption.AllDirectories).Length;
                if (fileCount == 0)
                {
                    DirectoryExpansion.DirectoryDelete(installPath);
                    Directory.CreateDirectory(installPath);
                }
                else
                {
                    var dr = MessageBox.Show("The file already exists in the specified directory. " +
                        "\nDo you want to continue without deleting?", "Warning", 
                        MessageBoxButtons.OKCancel, 
                        MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                    {
                        MainForm.ProcessStart(installPath);
                        ControlEnabled(true);
                        return;
                    }
                }
            }

            var fileAppType = (downloadType_file) switch
            {
                "Aria2" => ApplicationType.Aria2c,
                "HttpClient" => ApplicationType.HttpClient,
                _ => ApplicationType.HttpClient
            };
            var torrentAppType = (downloadType_torrent) switch
            {
                "Aria2" => ApplicationType.Aria2c,
                "Transmission" => ApplicationType.Transmission,
                _ => ApplicationType.Transmission
            };

            MessageBox.Show("Start the installation.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MainForm.InstallVisitedFlug = true;
            mainForm.FullStatusLabel.Text = "in preparation...";
            mainForm.MonoProgressBar.Value = 0;
            mainForm.FullProgressBar.Value = 0;

            Task.Run(() => 
            {
                using (var download = new Download(installPath))
                {
                    download.ProcessReceives += new ProcessEventHandler((appType, outLine) =>
                        mainForm.Invoke(new Delegate(() =>
                            Download_ProcessEventHandler(appType, outLine))));
                    mainForm.Invoke(new Delegate(() => mainForm.FullProgressBar.Value = 10));

                    var worldsEdgeAfterDarkDirPath = download.Run(
                        WebGetLink.WorldsEdgeAfterDark(), "WorldsEdgeAfterDark", appType: fileAppType);
                    mainForm.Invoke(new Delegate(() => mainForm.FullProgressBar.Value = 20));

                    var detoursR5DirPath = download.Run(
                        WebGetLink.DetoursR5(), "detoursR5", appType: fileAppType);
                    mainForm.Invoke(new Delegate(() => mainForm.FullProgressBar.Value = 30));

                    var scriptsR5DirPath = download.Run(
                        WebGetLink.ScriptsR5(), "scriptsR5", appType: fileAppType);
                    mainForm.Invoke(new Delegate(() => mainForm.FullProgressBar.Value = 40));

                    var apexClientDirPath = download.Run(
                        WebGetLink.ApexClient_Torrent(), "ApexClient", appType: torrentAppType);
                    mainForm.Invoke(new Delegate(() => mainForm.FullProgressBar.Value = 80));

                    DirectoryExpansion.MoveOverwrite(detoursR5DirPath, apexClientDirPath);
                    Directory.Move(scriptsR5DirPath, Path.Combine(apexClientDirPath, ScriptsDirectoryPath));
                    DirectoryExpansion.MoveOverwrite(Path.Combine(worldsEdgeAfterDarkDirPath, WorldsEdgeAfterDarkPath), apexClientDirPath);
                    DirectoryExpansion.DirectoryDelete(worldsEdgeAfterDarkDirPath);
                    download.DirectoryFix(installPath);
                    mainForm.Invoke(new Delegate(() => mainForm.FullProgressBar.Value = 90));
                }

                var AppPath = Path.Combine(installPath, ExecutableFileName);
                var scriptsPath = Path.Combine(installPath, ScriptsDirectoryPath);

                if (shortcutCreate_desktop)
                {
                    var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    CreateR5Shortcut(desktopPath, AppPath, scriptsPath);
                }

                if (shortcutCreate_startmenu)
                {
                    var startMenuPath = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu))[0];
                    var startmenuShortcutPath = Path.Combine(startMenuPath, DirName);
                    Directory.CreateDirectory(startmenuShortcutPath);
                    CreateR5Shortcut(startmenuShortcutPath, AppPath, scriptsPath);
                }

                mainForm.Invoke(new Delegate(() => {
                    MainForm.InstallVisitedFlug = false;
                    mainForm.FullStatusLabel.Text = "Done.";
                    mainForm.MonoProgressBar.Value = 100;
                    mainForm.FullProgressBar.Value = 100;
                    MessageBox.Show("Complete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ControlEnabled(true);
                }));
            });
        }

        private void Download_ProcessEventHandler(ApplicationType appType, string outLine)
        {
            int progress = 0;
            mainForm.FullStatusLabel.Text = "[" + appType + "] ";
            switch (appType)
            {
                case ApplicationType.Aria2c:
                case ApplicationType.HttpClient:
                    int.TryParse(Regex.Match(outLine, @"(?<=\().*?(?=%\))").Value, out progress);
                    break;
                case ApplicationType.Transmission:
                    int.TryParse(Regex.Match(outLine, @"(?<=Progress:).*?(?=..%,)").Value, out progress);
                    break;
                case ApplicationType.SevenZip:
                    mainForm.FullStatusLabel.Text += "Uncompressing. ";
                    break;
            }
            mainForm.MonoProgressBar.Value = progress;
            mainForm.FullStatusLabel.Text += outLine;
        }
    }
}

using R5_Reloaded_Installer_Library.Exclusive;
using R5_Reloaded_Installer_Library.Get;
using R5_Reloaded_Installer_Library.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R5_Reloaded_Installer_GUI
{
    public class Installer
    {
        private MainForm mainForm;
        private const float FileSize_GB = 42f;
        public delegate void Delegate();

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
                        "\nDo you want to continue?", "Warning", 
                        MessageBoxButtons.OKCancel, 
                        MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                    {
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

            Task.Run(() => 
            {
                using (var download = new Download(installPath))
                {
                    download.ProcessReceives += new ProcessEventHandler((appType, outLine) => 
                        mainForm.Invoke(new Delegate(() => 
                            Download_ProcessEventHandler(appType, outLine))));

                    var worldsEdgeAfterDarkDirPath = download.Run(
                        WebGetLink.WorldsEdgeAfterDark(), "WorldsEdgeAfterDark", appType: fileAppType);
                    var detoursR5DirPath = download.Run(
                        WebGetLink.DetoursR5(), "detoursR5", appType: fileAppType);
                    var scriptsR5DirPath = download.Run(
                        WebGetLink.ScriptsR5(), "scriptsR5", appType: fileAppType);
                    var apexClientDirPath = download.Run(
                        WebGetLink.ApexClient(), "ApexClient", appType: torrentAppType);
                }
                mainForm.Invoke(new Delegate(() => ControlEnabled(true)));
            });
        }

        private void Download_ProcessEventHandler(ApplicationType appType, string outLine)
        {
            switch (appType)
            {
                case ApplicationType.Aria2c:
                    break;
                case ApplicationType.Transmission:
                    break;
                case ApplicationType.SevenZip:
                    break;
                case ApplicationType.HttpClient:
                    break;
            }
            mainForm.MonoStatusLabel.Text = "(" + appType + ") " + outLine;
        }
    }
}

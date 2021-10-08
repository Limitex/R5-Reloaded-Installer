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
    public partial class MainForm : Form
    {
        public static string DirName = "R5-Reloaded";
        public static string AppDataLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string InstallPath = Path.Combine(AppDataLocalPath, DirName);

        private static bool ExitFlug = false;
        private static bool ButtonSelectFlug = false;
        private static long FileSize = -1;
        private static long DriveSize = -1;
        delegate void Delegate();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetButtonEnebled();
            SetDriveAndFileSize();
            InstallLinkTextBox.Text = InstallPath;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ExitFlug)
            {
                var dr = MessageBox.Show("Do you want to quit?", "Warning",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK) ExitFlug = true;
                else e.Cancel = true;
            }
        }

        private void MainTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!ButtonSelectFlug)
            {
                e.Cancel = true;
                return;
            }
            else 
            { 
                ButtonSelectFlug = false; 
            }
            SetButtonEnebled();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            ButtonToTabNext(1);
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            ButtonToTabNext(-1);
        }
        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (!AgreeCheckBox.Checked)
            {
                MessageBox.Show("Check the checkbox on the Information tab to continue.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var dr = MessageBox.Show("Do you want to start the installation?", "Installer",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK && CheckValue())
            {
                ButtonToTabNext(1);
                StartProcessInitialize();
                StartProcess();
            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (ExitFlug)
            {
                Application.Exit();
                return;
            }
            var dr = MessageBox.Show("Would you like to cancel the installation?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                ExitFlug = true;
                Application.Exit();
            }
        }

        private void DiscordLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenSite(DiscordLinkLabel.Text);
        }

        private void WebsiteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenSite(WebsiteLinkLabel.Text);
        }

        private void AgreeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetButtonEnebled();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                InstallPath = Path.Combine(fbd.SelectedPath, DirName);
                InstallLinkTextBox.Text = InstallPath;
                DriveSize = GetFileSize.DriveFreeSpace(InstallPath);
                SetSizesText(FileSize, DriveSize);
                NextButton.Enabled = CheckSize();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R5_Reloaded_Installer_GUI
{
    public partial class MainForm : Form
    {
        private static bool ExitFlug = false;
        private static bool ButtonSelectFlug = false;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetButtonEnebled();
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
            if (dr == DialogResult.OK)
            {
                ButtonToTabNext(1);
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
            var dr = MessageBox.Show("Would you like to cancel?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes) Application.Exit();
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

        private void ButtonToTabNext(int i)
        {
            ButtonSelectFlug = true;
            if (0 < i) if (MainTabControl.SelectedIndex < MainTabControl.TabCount - 1)
                    MainTabControl.SelectedIndex += 1;
            if (i < 0) if (0 < MainTabControl.SelectedIndex)
                    MainTabControl.SelectedIndex -= 1;

        }

        private void SetButtonEnebled()
        {
            var nowTab = MainTabControl.SelectedTab.Name;

            if (nowTab == IntroductionTabPage.Name)
            {
                BackButton.Enabled = false;
                NextButton.Enabled = true;
                InstallButton.Enabled = false;
            }
            if (nowTab == InformationTabPage.Name)
            {
                BackButton.Enabled = true;
                NextButton.Enabled = AgreeCheckBox.Checked;
                InstallButton.Enabled = false;
            }
            if (nowTab == PlaceOfInstallationTabPage.Name)
            {
                BackButton.Enabled = true;
                NextButton.Enabled = true;
                InstallButton.Enabled = false;
            }
            if (nowTab == OptionTabPage.Name)
            {
                BackButton.Enabled = true;
                NextButton.Enabled = false;
                InstallButton.Enabled = true;
            }
            if (nowTab == ProcessTabPage.Name)
            {
                BackButton.Enabled = false;
                NextButton.Enabled = false;
                InstallButton.Enabled = false;
            }
            if (nowTab == CompletionTabPage.Name)
            {
                BackButton.Enabled = false;
                NextButton.Enabled = false;
                InstallButton.Enabled = false;
                CancelButton.Text = "Exit";
                ExitFlug = true;
            }
        }

        private void OpenSite(string url)
        {
            var info = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = url,
            };
            Process.Start(info);
        }

        private void CompleteProcess()
        {
            NextButton.Enabled = true;
        }

        private void StartProcess()
        {
            MessageBox.Show("Installing Process");
            CompleteProcess();
        }
    }
}

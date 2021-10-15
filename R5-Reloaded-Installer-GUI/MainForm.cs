using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R5_Reloaded_Installer_GUI
{
    public partial class MainForm : Form
    {
        public static string LatestDirectoryName = "R5-Reloaded";

        public static bool IsRunning = true;

        private static Form LogForm;
        private static RichTextBox LogFormRichTexBox;

        public MainForm()
        {
            InitializeComponent();
            LogFormInitialize();
            new ButtonOperation(this, MainTask_StartInstall);
            new GetSizeAndPath(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsRunning)
            {
                var dr = MessageBox.Show("Do you want to quit?", "Warning",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK) e.Cancel = true;
            }
        }

        private void MainTask_StartInstall(object sender, StartInstallEventArgs e)
        {
            MessageBox.Show(e.InstallationPath);
            MessageBox.Show(e.ApexClientURL);
            MessageBox.Show(e.Detours_R5URL);
            MessageBox.Show(e.Scripts_R5URL);
            MessageBox.Show(e.CreateDesktopShortcut.ToString());
            MessageBox.Show(e.AddShortcutToStartMenu.ToString());
            MessageBox.Show("MainTask");
            if (LogForm.Visible) LogFormRichTexBox.AppendText("LogText" + "\n");
            NextButton.Enabled = true;
        }

        private void DetailedInfoLinkLabe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!LogForm.Visible)
            {
                LogFormInitialize();
                LogForm.Show();
            }
        }

        private void LogFormInitialize()
        {
            LogForm = new Form() { Text = "Log Window", ShowIcon = false };
            LogFormRichTexBox = new RichTextBox()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Name = "logRichTextBox",
                Location = new Point(0, 0),
                Size = new Size(LogForm.Width - 16, LogForm.Height - 39),
                BackColor = Color.White,
                ReadOnly = true,
                HideSelection = false
            };
            LogForm.Controls.Add(LogFormRichTexBox);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace R5_Reloaded_Installer_GUI
{
    public class LogWindow
    {
        public static Form LogForm;
        public static RichTextBox LogRichTexBox;

        public LogWindow(MainForm form)
        {
            LogFormInitialize();
            form.DetailedInfoLinkLabe.LinkClicked += new LinkLabelLinkClickedEventHandler(DetailedInfoLinkLabe_LinkClicked);
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
            LogRichTexBox = new RichTextBox()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Name = "logRichTextBox",
                Location = new Point(0, 0),
                Size = new Size(LogForm.Width - 16, LogForm.Height - 39),
                BackColor = Color.White,
                ReadOnly = true,
                HideSelection = false
            };
            LogForm.Controls.Add(LogRichTexBox);
        }

        public static void WriteLine(string str)
        {
            if (LogForm.Visible) LogRichTexBox.AppendText(str + "\n");
        }
    }
}

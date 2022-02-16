namespace R5_Reloaded_Installer_GUI
{
    public partial class MainForm : Form
    {
        public static bool InstallVisitedFlug = false;
        public MainForm()
        {
            InitializeComponent();
            SelectFileDownloaderComboBox.SelectedIndex = 1;
            SelectTorrentDownloaderComboBox.SelectedIndex = 1;
            _ = new LinkLabelOpener(this);
            _ = new DirectorySelector(this);
            _ = new Installer(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (InstallVisitedFlug)
            {
                var dr = MessageBox.Show("Installation is in progress.\nDo you want to finish?", "Warning",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
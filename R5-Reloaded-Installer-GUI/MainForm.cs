namespace R5_Reloaded_Installer_GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SelectFileDownloaderComboBox.SelectedIndex = 1;
            SelectTorrentDownloaderComboBox.SelectedIndex = 1;
            new LinkLabelOpener(this);
        }
    }
}
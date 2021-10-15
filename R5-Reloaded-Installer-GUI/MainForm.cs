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

        public MainForm()
        {
            InitializeComponent();
            new ButtonOperation(this, (sender, e) => { });
            new GetSizeAndPath(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var dr = MessageBox.Show("Do you want to quit?", "Warning",
                 MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr != DialogResult.OK) e.Cancel = true;
        }
    }
}

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

        public Installer(MainForm form)
        {
            mainForm = form;
            mainForm.InstallButton.Click += new EventHandler(InstallButton_Click);
        }

        private void InstallButton_Click(object? sender, EventArgs e)
        {

        }
    }
}

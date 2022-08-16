using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogReader2
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();

            LoadSettings();
        }

        public string GetHelpFilePath() { return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Help.txt"); }

        private void LoadSettings()
        {
            string setting_file = GetHelpFilePath();
            if (File.Exists(setting_file))
            {
                using (StreamReader reader = File.OpenText(setting_file))
                {
                    tbHelpText.Text = reader.ReadToEnd();
                    tbHelpText.SelectionStart = 0;
                    tbHelpText.SelectionLength = 0;
                }
            }
        }
    }
}

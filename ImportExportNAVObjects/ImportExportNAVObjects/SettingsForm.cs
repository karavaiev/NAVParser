using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ImportExportNAVObjects
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["FinSQLFilePath"] = textBoxFinSQLFileLocation.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            try
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(textBoxFinSQLFileLocation.Text);
            }
            catch (Exception)
            {
            }
            
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFinSQLFileLocation.Text = openFileDialog1.FileName;
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBoxFinSQLFileLocation.Text = Properties.Settings.Default["FinSQLFilePath"].ToString();
        }
    }
}

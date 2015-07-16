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
using System.Diagnostics;

namespace ImportExportNAVObjects
{
    public partial class MainForm : Form
    {
        public const string ExportStatusSuccess = "Export successful";

        public MainForm()
        {
            InitializeComponent();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            statusText.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            try
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(textBoxFileName.Text);
            }
            catch (Exception)
            {
            }
            
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.CheckPathExists = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFileName.Text = openFileDialog1.FileName;
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NAVConnection navConnection = new NAVConnection(textBoxServerName.Text,textBoxDatabaseName.Text);

            if (!navConnection.TestConnection())
            {
                statusText.Text = navConnection.GetConnectionError();
            }
            else
            {
                NAVExport navExport = new NAVExport(Properties.Settings.Default["FinSQLFilePath"].ToString(), textBoxFileName.Text,
                    textBoxServerName.Text, textBoxDatabaseName.Text, filter: textBoxFilters.Text, ntauthentication: 1);

                navExport.Export();

                statusText.Text = ExportStatusSuccess;
            }
        }
    }
}

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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TaskMenager
{
    public partial class CommandLine : Form
    {
        private const string DataFilePath = "combobox_data.txt";
        public CommandLine()
        {
            InitializeComponent();
            LoadComboBoxData();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            string newText = comboBoxFIleName.Text;
            if (!string.IsNullOrEmpty(newText))
            {
                comboBoxFIleName.Items.Clear();
                comboBoxFIleName.Items.Add(newText);
                SaveComboBoxData();
            }
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(comboBoxFIleName.Text);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = startInfo;
            process.Start();
            this.Close();
        }
        private void SaveComboBoxData()
        {
            using (StreamWriter writer = new StreamWriter(DataFilePath))
            {
                foreach (string item in comboBoxFIleName.Items)
                {
                    writer.WriteLine(item);
                }
            }
        }
        private void LoadComboBoxData()
        {
            if (File.Exists(DataFilePath))
            {
                using (StreamReader reader = new StreamReader(DataFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        comboBoxFIleName.Items.Add(line);
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Все файлы (.)|.";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                comboBoxFIleName.Text = filePath;
            }
        }
    }
}

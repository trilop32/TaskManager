using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskMenager
{
    public partial class CommandLine : Form
    {
        public CommandLine()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(comboBoxFIleName.Text);
            System.Diagnostics.Process process=new System.Diagnostics.Process();
            process.StartInfo = startInfo;
            process.Start();
            this.Close();
        }
    }
}

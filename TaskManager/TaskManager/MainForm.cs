using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskMenager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            KDSHDJ();
            statusStrip1.Items.Add("");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadProcesses();
        }
        void KDSHDJ()
        {
            listViewProcesses.Columns.Add("PID");
            listViewProcesses.Columns.Add("Name");

        }
        void LoadProcesses()
        {
            listViewProcesses.Items.Clear();
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = processes[i].Id.ToString();
                item.SubItems.Add(processes[i].ProcessName);
                listViewProcesses.Items.Add(item);
            }
            statusStrip1.Items[0].Text=$"Количесвто процессов:{listViewProcesses.Items.Count}";
            
        }
    }
}
//System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();//все запущенные процессы
//for (int i = 0; i < processes.Length; i++)
//{
//    Console.WriteLine($"{processes[i].Id}\t{processes[i].MainModule.FileName}\t");
//    //Console.WriteLine($"Name: {processes[i].ProcessName}\t");
//    //Console.WriteLine($"PID: {processes[i].Id}\t");
//    //Console.WriteLine($"Path: {processes[i].MainModule.FileName}\t");
//    //Console.WriteLine();
//}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TaskMenager
{
    public partial class MainForm : Form
    {
        Dictionary<int, Process> d_processes;
        readonly int ramFactor = 1024;
        readonly string suuffix = "kB";
        public MainForm()
        {
            InitializeComponent();
            KDSHDJ();
            statusStrip1.Items.Add("");
            LoadProcesses();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //LoadProcesses();
            AddNewProcesses();
            RemoveOldProcesse();
            UpdateExistingProcesses();
            statusStrip1.Items[0].Text = $"Количесвто процессов:{listViewProcesses.Items.Count}";
        }
        void KDSHDJ()
        {
            listViewProcesses.Columns.Add("PID");//item
            listViewProcesses.Columns.Add("Name");//subitem 1
            listViewProcesses.Columns.Add("Working set");//subitem 2
            listViewProcesses.Columns.Add("Paek working set");//subitems 3
        }
        void LoadProcesses()
        {
            //listViewProcesses.Items.Clear();
            //Process[] processes = Process.GetProcesses();
            //for (int i = 0; i < processes.Length; i++)
            //{
            //    ListViewItem item = new ListViewItem();
            //    item.Text = processes[i].Id.ToString();
            //    item.SubItems.Add(processes[i].ProcessName);
            //    listViewProcesses.Items.Add(item);
            //}
            d_processes = Process.GetProcesses().ToDictionary(item => item.Id, item => item);
            foreach (KeyValuePair<int,Process> i in d_processes)
            {
                //ListViewItem item= new ListViewItem();
                //item.Text = i.Key.ToString();
                //item.SubItems.Add(i.Value.ProcessName);
                //listViewProcesses.Items.Add(item);
                AddProcessToListView(i.Value);
            }
            //statusStrip1.Items[0].Text = $"Количесвто процессов:{listViewProcesses.Items.Count}";
        }
        void AddNewProcesses()
        {
            Dictionary<int, Process> d_processes = Process.GetProcesses().ToDictionary(item => item.Id, item => item);
            foreach (KeyValuePair<int,Process> i in d_processes)
            {
                if (!this.d_processes.ContainsKey(i.Key))
                {
                    //this.d_processes.Add(i.Key, i.Value);
                    AddProcessToListView(i.Value);
                }
            }
            //statusStrip1.Items[0].Text = $"Количесвто процессов:{listViewProcesses.Items.Count}";
        }
        void RemoveOldProcesse()
        {
            this.d_processes = Process.GetProcesses().ToDictionary(item => item.Id, item => item);
            for(int i = 0; i < listViewProcesses.Items.Count;i++)
            {
                //string item_name = listViewProcesses.Items[i].Name;
                if (!d_processes.ContainsKey(Convert.ToInt32(listViewProcesses.Items[i].Text)))
                {
                    listViewProcesses.Items.RemoveAt(i);
                }
            }
        }
        void UpdateExistingProcesses()
        {
            for(int i=0;i<listViewProcesses.Items.Count;i++)
            {
                int id = Convert.ToInt32(listViewProcesses.Items[i].Text);
               // Process process = d_processes[id];
                listViewProcesses.Items[i].SubItems[2].Text = $"{d_processes[id].WorkingSet64/ramFactor} {suuffix}";
                listViewProcesses.Items[i].SubItems[3].Text = $"{d_processes[id].PeakWorkingSet64 / ramFactor} {suuffix}";
            }
        }
        void AddProcessToListView(Process process)
        {
            ListViewItem item = new ListViewItem();
            item.Text=process.Id.ToString();
            item.SubItems.Add(process.ProcessName);
            item.SubItems.Add($"{process.WorkingSet64/ramFactor} {suuffix}");
            item.SubItems.Add($"{process.PeakWorkingSet64 / ramFactor} {suuffix}");
            listViewProcesses.Items.Add(item);
        }
        void RomoveProcessFromListView(int pId)
        {
            listViewProcesses.Items.RemoveByKey(pId.ToString());
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandLine cmd = new CommandLine();
            cmd.ShowDialog();
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
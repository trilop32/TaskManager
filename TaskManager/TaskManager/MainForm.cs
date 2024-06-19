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
            statusStrip1.Items[0].Text = $"Количесвто процессов:{listViewProcesses.Items.Count}";
            this.d_processes = Process.GetProcesses().ToDictionary(item => item.Id, item => item);
        }
        void KDSHDJ()
        {
            listViewProcesses.Columns.Add("PID");
            listViewProcesses.Columns.Add("Name");
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
                ListViewItem item= new ListViewItem();
                item.Text = i.Key.ToString();
                item.SubItems.Add(i.Value.ProcessName);
                listViewProcesses.Items.Add(item);
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
            for(int i = 0; i < listViewProcesses.Items.Count;i++)
            {
                string item_name = listViewProcesses.Items[i].Name;
                if (!d_processes.ContainsKey(Convert.ToInt32(listViewProcesses.Items[i].Text)))
                {
                    listViewProcesses.Items.RemoveAt(i);
                }
            }
        }
        void AddProcessToListView(Process process)
        {
            ListViewItem item = new ListViewItem();
            item.Text=process.Id.ToString();
            item.SubItems.Add(process.ProcessName);
            listViewProcesses.Items.Add(item);
        }
        void RomoveProcessFromListView(int pId)
        {
            listViewProcesses.Items.RemoveByKey(pId.ToString());
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskMenager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KDSHDJ();
            
        }
        void KDSHDJ()
        {
            Process[] processes = Process.GetProcesses();
            
                for (int i = 0; i < processes.Length; i++)
                {
                    listViewProcesses.Items.Add($"{processes[i].Id} {processes[i].ProcessName}\n");
                }
            
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
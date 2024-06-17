#define SINGLE_PROCESS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;
using System.Xml.Linq;
namespace Process
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if SINGLE_PROCESS
            Console.WriteLine("Введите имя програмы");
            string process_name = Console.ReadLine();
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = process_name;
            process.Start();
            Console.WriteLine(process_name);
            Console.WriteLine(process.Id);
            Console.WriteLine(process.MainModule.FileName);
            IntPtr handle = IntPtr.Zero;
            OpenProcessToken(process.Handle, 8, out handle);//out обозначает изменение
            System.Security.Principal.WindowsIdentity wi = new System.Security.Principal.WindowsIdentity(handle);
            CloseHandle(handle);
            PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);

            while (true)
            {
                PerformanceCounter MB = new PerformanceCounter("Process", "Working Set - Private", process.ProcessName);
                double mbsize = Convert.ToDouble(Convert.ToString(Convert.ToDouble(MB.NextValue()) / 1024 / 1024).Substring(0, 4));
                double cpU = cpuCounter.NextValue();
                Console.WriteLine($"Username: {wi.Name}");
                Console.WriteLine($"SessionID: {process.SessionId}");
                Console.WriteLine($"Treads: {process.Threads}");
                Console.WriteLine($"Priority: {process.PriorityClass}");
                Console.WriteLine($"Занимаемая память: {mbsize} МБ");
                Console.WriteLine($"Загрузка ЦП:{cpU/3}");
                Thread.Sleep(1000);
                Console.Clear();
            }


#endif
        }
        //используем функции Windows которые в C# нет 
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAcsess, out IntPtr handle);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr handle);


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ClassLibrary1
{
    public class ProcessManager
    {
        public static Process[] GetProcesses()
        {
            return Process.GetProcesses();
        }

        public static void KillProcess(int processID)
        {
            var process = Process.GetProcessById(processID);
            process.Kill();
        }

        public static void SetProcessPriority(int processID, ProcessPriorityClass priority) 
        {
            var process = Process.GetProcessById(processID);
            process.PriorityClass = priority;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ClassLibrary1;

namespace ProcessManagerWindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadProcessInThread();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            if (ProcessTableDataGridView.Columns.Count == 0)
            {
                ProcessTableDataGridView.Columns.Add("ProcessId", "ID");
                ProcessTableDataGridView.Columns.Add("ProcessName", "Назва процесу");
                ProcessTableDataGridView.Columns.Add("MemoryUsage", "Пам’ять");
                ProcessTableDataGridView.Columns.Add("StartTime", "Час запуску");
                ProcessTableDataGridView.Columns.Add("Priority", "Пріоритет");
                ProcessTableDataGridView.Columns.Add("Threads", "Потоки");
            }
        }

        private void RunInBackground(ThreadStart action)
        {
            Thread thread = new Thread(action) { IsBackground = true };
            thread.Start();
        }

        private void UpdateUI(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private void LoadProcessInThread()
        {
            update_button.Enabled = false;
            ProcessTableDataGridView.Rows.Clear();

            RunInBackground(() =>
            {
                var processes = ProcessLoader.GetProcesses();

                foreach (var process in processes)
                {
                    try
                    {
                        if (!process.HasExited)
                        {
                            UpdateUI(() =>
                            {
                                ProcessTableDataGridView.Rows.Add(
                                    process.Id,
                                    process.ProcessName,
                                    Math.Round(process.WorkingSet64 / Math.Pow(1024, 2), 4) + " МБ",
                                    process.StartTime.ToString(),
                                    process.PriorityClass.ToString(),
                                    process.Threads.Count);
                            });
                        }
                    }
                    catch (Exception) { }
                }
                UpdateUI(() => update_button.Enabled = true);
            });
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            LoadProcessInThread();
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            if (int.TryParse(stop_textBox.Text, out int processID))
            {
                RunInBackground(() =>
                {
                    try
                    {
                        var process = Process.GetProcessById(processID);
                        process.Kill();
                        UpdateUI(() => LoadProcessInThread());
                    }
                    catch (ArgumentException)
                    {
                        UpdateUI(() => MessageBox.Show($"Процес за ID {processID} не знайдено!"));
                    }
                });
            }
        }

        private void change_priority_button_Click(object sender, EventArgs e)
        {
            if (int.TryParse(priority_textBox.Text, out int processID) && priority_box.SelectedItem != null)
            {
                var priority = (ProcessPriorityClass)Enum.Parse(typeof(ProcessPriorityClass), priority_box.SelectedItem.ToString());

                RunInBackground(() =>
                {
                    try
                    {
                        ProcessManager.SetProcessPriority(processID, priority);
                        UpdateUI(() => LoadProcessInThread());
                    }
                    catch (ArgumentException)
                    {
                        UpdateUI(() => MessageBox.Show($"Процес за ID {processID} не знайдено!"));
                    }
                });
            }
        }

        private void start_app_button_Click(object sender, EventArgs e)
        {
            string appPath = path.Text;
            if (!string.IsNullOrEmpty(appPath))
            {
                RunInBackground(() =>
                {
                    try
                    {
                        Process.Start(appPath);
                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => MessageBox.Show($"Помилка: {ex.Message}"));
                    }
                });
            }
        }
    }

    public class ProcessLoader
    {
        public static List<Process> GetProcesses() => ProcessManager.GetProcesses().ToList();
    }
}

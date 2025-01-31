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
            InitializeUI();
            RefreshProcessList();
        }

        private void InitializeUI()
        {
            if (ProcessTableDataGridView.Columns.Count == 0)
            {
                var columnNames = new (string Name, string Header)[]
                {
                    ("ProcessId", "ID"),
                    ("ProcessName", "Назва процесу"),
                    ("MemoryUsage", "Пам’ять"),
                    ("StartTime", "Час запуску"),
                    ("Priority", "Пріоритет"),
                    ("Threads", "Потоки")
                };

                foreach (var column in columnNames)
                {
                    ProcessTableDataGridView.Columns.Add(column.Name, column.Header);
                }
            }
        }

        private void RunInBackground(Action action)
        {
            Thread thread = new Thread(() =>
            {
                try { action(); }
                catch (Exception ex) { ShowError(ex.Message); }
            })
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void UpdateUI(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private void ShowError(string message)
        {
            UpdateUI(() => MessageBox.Show($"Помилка: {message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        private void RefreshProcessList()
        {
            update_button.Enabled = false;
            ProcessTableDataGridView.Rows.Clear();

            RunInBackground(() =>
            {
                var processes = ProcessLoader.GetProcesses();

                UpdateUI(() =>
                {
                    foreach (var process in processes)
                    {
                        try
                        {
                            if (!process.HasExited)
                            {
                                ProcessTableDataGridView.Rows.Add(
                                    process.Id,
                                    process.ProcessName,
                                    $"{Math.Round(process.WorkingSet64 / Math.Pow(1024, 2), 4)} МБ",
                                    process.StartTime,
                                    process.PriorityClass,
                                    process.Threads.Count
                                );
                            }
                        }
                        catch (Exception) { /* Пропускаємо */ }
                    }

                    update_button.Enabled = true;
                });
            });
        }

        private void update_button_Click(object sender, EventArgs e) => RefreshProcessList();

        private void stop_button_Click(object sender, EventArgs e)
        {
            if (int.TryParse(stop_textBox.Text, out int processID))
            {
                RunInBackground(() =>
                {
                    try
                    {
                        Process.GetProcessById(processID).Kill();
                        RefreshProcessList();
                    }
                    catch (ArgumentException)
                    {
                        ShowError($"Процес за ID {processID} не знайдено!");
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
                        RefreshProcessList();
                    }
                    catch (ArgumentException)
                    {
                        ShowError($"Процес за ID {processID} не знайдено!");
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
                    try { Process.Start(appPath); }
                    catch (Exception ex) { ShowError(ex.Message); }
                });
            }
        }
    }

    public class ProcessLoader
    {
        public static List<Process> GetProcesses() => ProcessManager.GetProcesses().ToList();
    }
}

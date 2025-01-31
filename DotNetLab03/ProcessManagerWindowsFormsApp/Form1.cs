using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            InitializeDataGridView();
            LoadProcessInThread();
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

        private void LoadProcessInThread()
        {
            update_button.Enabled = false;
            ProcessTableDataGridView.Rows.Clear();

            Thread processThread = new Thread(() =>
            {
                var processes = ProcessManager.GetProcesses();
                Invoke(new Action(() =>
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
                                    process.StartTime.ToString(),
                                    process.PriorityClass.ToString(),
                                    process.Threads.Count);
                            }
                        }
                        catch (Exception) { }
                    }
                    update_button.Enabled = true;
                }));
            })
            { IsBackground = true };
            processThread.Start();
        }

        private void update_button_Click(object sender, EventArgs e) => LoadProcessInThread();

        private void stop_button_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(stop_textBox.Text, out int processID))
            {
                MessageBox.Show("Введіть коректний числовий ID процеса.");
                return;
            }

            ExecuteInThread(() =>
            {
                try
                {
                    var process = Process.GetProcessById(processID);
                    process.Kill();
                    Invoke(new Action(LoadProcessInThread));
                }
                catch (ArgumentException)
                {
                    ShowMessage($"Процес за ID {processID} не знайдено!");
                }
                catch (Exception ex)
                {
                    ShowMessage($"Помилка: {ex.Message}!");
                }
            });
        }

        private void change_priority_button_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(priority_textBox.Text, out int processID))
            {
                MessageBox.Show("Введіть коректний числовий ID процеса.");
                return;
            }

            if (priority_box.SelectedItem == null)
            {
                MessageBox.Show("Виберіть пріоритет із випадаючого списку.");
                return;
            }

            if (!Enum.TryParse(priority_box.SelectedItem.ToString(), out ProcessPriorityClass priority))
            {
                MessageBox.Show("Некоректний пріоритет.");
                return;
            }

            ExecuteInThread(() =>
            {
                try
                {
                    ProcessManager.SetProcessPriority(processID, priority);
                    Invoke(new Action(LoadProcessInThread));
                }
                catch (ArgumentException)
                {
                    ShowMessage($"Процес за ID {processID} не знайдено!");
                }
                catch (InvalidOperationException)
                {
                    ShowMessage($"Процес за ID {processID} вже завершено!");
                }
                catch (Exception ex)
                {
                    ShowMessage($"Помилка: {ex.Message}!");
                }
            });
        }

        private void start_app_button_Click(object sender, EventArgs e)
        {
            string appPath = path.Text;
            if (string.IsNullOrWhiteSpace(appPath))
            {
                MessageBox.Show("Введіть шлях до програми.");
                return;
            }

            ExecuteInThread(() =>
            {
                try
                {
                    Process.Start(appPath);
                }
                catch (Exception ex)
                {
                    ShowMessage($"Помилка запуску: {ex.Message}");
                }
            });
        }

        private void ExecuteInThread(ThreadStart action)
        {
            Thread thread = new Thread(action) { IsBackground = true };
            thread.Start();
        }

        private void ShowMessage(string message)
        {
            Invoke(new Action(() => MessageBox.Show(message)));
        }
    }
}

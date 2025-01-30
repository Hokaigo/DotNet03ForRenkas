using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
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

        private void LoadProcessInThread()
        {
            update_button.Enabled = false;
            ProcessTableDataGridView.Rows.Clear();

            Thread processThread = new Thread(() =>
            {
                var processes = ProcessManager.GetProcesses();

                foreach (var process in processes)
                {
                    try
                    {
                        if (!process.HasExited) 
                        {
                            Invoke(new Action(() =>
                            {
                                ProcessTableDataGridView.Rows.Add(
                                    process.Id,
                                    process.ProcessName,
                                    Math.Round(process.WorkingSet64 / Math.Pow(1024,2),4) + " МБ",
                                    process.StartTime.ToString(),
                                    process.PriorityClass.ToString(),
                                    process.Threads.Count);
                            }));
                        }
                    }
                    catch (Exception ex) { }
                }
                Invoke(new Action(() => update_button.Enabled = true));
            });
            processThread.IsBackground = true;
            processThread.Start();
        }


        private void update_button_Click(object sender, EventArgs e)
        {
            LoadProcessInThread();
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            if (int.TryParse(stop_textBox.Text, out int processID))
            {
                Thread killThread = new Thread(() =>
                {
                    try
                    {
                        var process = Process.GetProcessById(processID);
                        process.Kill();

                        Invoke(new Action(() => LoadProcessInThread()));
                    }
                    catch (ArgumentException) 
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show($"Процес за ID {processID} не знайдено!");
                        }));
                    }
                    catch (Exception ex) 
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show($"Помилка: {ex.Message}!");
                        }));
                    }
                });

                killThread.IsBackground = true;
                killThread.Start();
            }
            else
            {
                MessageBox.Show("Введіть коректний числовий ID процеса.");
            }
        }


        private void change_priority_button_Click(object sender, EventArgs e)
        {
            if (int.TryParse(priority_textBox.Text, out int processID))
            {
                if (priority_box.SelectedItem != null)
                {
                    var priority = (ProcessPriorityClass)Enum.Parse(typeof(ProcessPriorityClass), priority_box.SelectedItem.ToString());

                    Thread priorityThread = new Thread(() =>
                    {
                        try
                        {
                            ProcessManager.SetProcessPriority(processID, priority);
                            Invoke(new Action(() => LoadProcessInThread()));
                        }
                        catch (ArgumentException)
                        {
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show($"Процес за ID {processID} не знайдено!");
                            }));
                        }
                        catch (InvalidOperationException)
                        {
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show($"Процес за ID {processID} вже завершено!");
                            }));
                        }
                        catch (Exception ex)
                        {
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show($"Помилка: {ex.Message}!");
                            }));
                        }
                    });

                    priorityThread.IsBackground = true;
                    priorityThread.Start();
                }
                else
                {
                    MessageBox.Show("Виберіть пріоритет із випадаючого списку.");
                }
            }
            else
            {
                MessageBox.Show("Введіть коректний числовий ID процеса.");
            }
        }


        private void start_app_button_Click(object sender, EventArgs e)
        {
            string appPath = path.Text;

            if(!string.IsNullOrEmpty(appPath)) 
            {
                Thread startAppThread = new Thread(() =>
                {
                    try
                    {
                        Process.Start(appPath);
                    }
                    catch(Exception ex) 
                    { 
                        MessageBox.Show($"Помилка: {ex}");
                    }
                });
                startAppThread.IsBackground = true;
                startAppThread.Start();
            }
        }
    }
}

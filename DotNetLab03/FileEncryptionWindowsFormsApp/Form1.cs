using ClassLibrary1;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FileEncryptionWindowsFormsApp
{
    public partial class Form1 : Form
    {
        private BackgroundWorker worker;
        private Stopwatch sw;
        private FileEncryptionService encryptionService;
        private string outputFolderPath;

        public Form1()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
        }

        private void InitializeBackgroundWorker()
        {
            worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(keyInputTextBox.Text))
            {
                MessageBox.Show("Здається, введений вами формат ключа не вірний.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(outputFolderPath))
            {
                MessageBox.Show("Будь ласка, оберіть місце для збереження файлу.");
                return false;
            }
            return true;
        }

        private string GetOutputFilePath(string inputFilePath)
        {
            if (!Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(outputFolderPath);
            }
            return Path.Combine(outputFolderPath,
                Path.GetExtension(inputFilePath) == ".enc"
                ? Path.GetFileNameWithoutExtension(inputFilePath)
                : Path.GetFileName(inputFilePath) + ".enc");
        }

        private void EncryptOrDecryptButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            using (OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Оберіть файл для шифрування або розшифрування",
                Filter = "Усі файли (*.*)|*.*",
                Multiselect = false
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string inputFilePath = openFileDialog.FileName;
                    string outputFilePath = GetOutputFilePath(inputFilePath);
                    bool isEncrypting = Path.GetExtension(inputFilePath) != ".enc";

                    progressBar.Value = 0;
                    sw = Stopwatch.StartNew();

                    try
                    {
                        encryptionService = new FileEncryptionService(keyInputTextBox.Text);
                        worker.RunWorkerAsync(new object[] { inputFilePath, outputFilePath, isEncrypting });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Сталася помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;
            string inputFilePath = (string)args[0];
            string outputFilePath = (string)args[1];
            bool isEncrypting = (bool)args[2];

            try
            {
                if (isEncrypting)
                    encryptionService.EncryptFile(inputFilePath, outputFilePath, worker.ReportProgress);
                else
                    encryptionService.DecryptFile(inputFilePath, outputFilePath, worker.ReportProgress);

                e.Result = new FileProcessingResult(outputFilePath, isEncrypting, new FileInfo(outputFilePath).Length);
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        private void UpdateProgress(int percentage)
        {
            progressBar.Value = percentage;
            progressLabel.Text = $"{percentage}%";
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateProgress(e.ProgressPercentage);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            sw.Stop();
            if (e.Result is Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            else if (e.Result is FileProcessingResult result)
            {
                MessageBox.Show($"Шифрування завершено!\nФайл: {Path.GetFileName(result.FilePath)}\nРозмір: {(result.FileSize / 1024.0):F4} КБ\nЧас: {sw.Elapsed}");
            }
            UpdateProgress(0);
        }

        private void generateRandomKeyButton_Click(object sender, EventArgs e)
        {
            randomKeyTextBox.Text = FileEncryptor.GenerateKey(16);
        }

        private void setOutputPathButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Оберіть місце для збереження файлу"
            })
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFolderPath = folderBrowserDialog.SelectedPath;
                    outputPathTextBox.Text = outputFolderPath;
                }
            }
        }
    }
}

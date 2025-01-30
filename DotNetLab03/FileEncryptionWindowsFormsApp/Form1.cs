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
        private FileEncryptor encryptor;
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

        private void EncryptOrDecryptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(keyInputTextBox.Text))
            {
                MessageBox.Show("Здається, введений вами формат ключа не вірний.");
                return;
            }

            if (string.IsNullOrWhiteSpace(outputFolderPath))
            {
                MessageBox.Show("Будь ласка, оберіть місце для збереження файлу.");
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Оберіть файл для шифрування або розшифрування";
                openFileDialog.Filter = "Усі файли (*.*)|*.*";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string inputFilePath = openFileDialog.FileName;

                    if (!Directory.Exists(outputFolderPath))
                    {
                        Directory.CreateDirectory(outputFolderPath);
                    }

                    string outputFilePath;
                    if (Path.GetExtension(inputFilePath) == ".enc") 
                    {
                        outputFilePath = Path.Combine(outputFolderPath, Path.GetFileNameWithoutExtension(inputFilePath));
                    }
                    else 
                    {
                        outputFilePath = Path.Combine(outputFolderPath, Path.GetFileName(inputFilePath) + ".enc");
                    }

                    bool isEncrypting = Path.GetExtension(inputFilePath) != ".enc";
                    progressBar.Value = 0;
                    sw = Stopwatch.StartNew();

                    try
                    {
                        encryptor = new FileEncryptor(keyInputTextBox.Text);
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
                    encryptor.EncryptFile(inputFilePath, outputFilePath, progress => worker.ReportProgress(progress));
                else
                    encryptor.DecryptFile(inputFilePath, outputFilePath, progress => worker.ReportProgress(progress));

                e.Result = new { FilePath = outputFilePath, IsEncrypting = isEncrypting, FileSize = new FileInfo(outputFilePath).Length };
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            progressLabel.Text = $"{e.ProgressPercentage}%";
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            sw.Stop();
            if (e.Result is Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            else
            {
                var result = (dynamic)e.Result;
                MessageBox.Show($"Шифрування завершено!\nФайл: {Path.GetFileName(result.FilePath)}\nРозмір: {(result.FileSize / 1024.0).ToString("F4")} КБ\n" +
                    $"Час: {sw.Elapsed}");
            }
            progressBar.Value = 0;
            progressLabel.Text = "0%";
        }

        private void generateRandomKeyButton_Click(object sender, EventArgs e)
        {
            randomKeyTextBox.Text = FileEncryptor.GenerateKey(16);
        }

        private void setOutputPathButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Оберіть місце для збереження файлу";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFolderPath = folderBrowserDialog.SelectedPath;
                    outputPathTextBox.Text = outputFolderPath;
                }
            }
        }
    }
}
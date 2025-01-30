using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ClassLibrary1
{
    public class FileEncryptor
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public FileEncryptor(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("Encryption key cannot be empty!");

            using (var sha256 = SHA256.Create())
            {
                this.key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            }

            using (var md5 = MD5.Create())
            {
                this.iv = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
        }

        public void EncryptFile(string inputFilePath, string outputFilePath, Action<int> progressCallback = null)
        {
            ProcessFile(inputFilePath, outputFilePath, true, progressCallback);
        }

        public void DecryptFile(string inputFilePath, string outputFilePath, Action<int> progressCallback = null)
        {
            ProcessFile(inputFilePath, outputFilePath, false, progressCallback);
        }

        private void ProcessFile(string inputFilePath, string outputFilePath, bool encrypt, Action<int> progressCallback)
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException("File not found!");

            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform transform = encrypt ? aes.CreateEncryptor() : aes.CreateDecryptor();

                using (FileStream inputFile = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream outputFile = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(outputFile, transform, CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    long totalBytesRead = 0;
                    long fileLength = inputFile.Length;

                    while ((bytesRead = inputFile.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;

                        progressCallback?.Invoke((int)((double)totalBytesRead / fileLength * 100));
                    }
                }
            }
        }

        public static string GenerateKey(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("Key length must be greater than zero!");

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}

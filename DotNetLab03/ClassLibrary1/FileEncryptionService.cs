using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class FileEncryptionService
    {
        private readonly FileEncryptor encryptor;

        public FileEncryptionService(string key)
        {
            encryptor = new FileEncryptor(key);
        }

        public void EncryptFile(string inputPath, string outputPath, Action<int> onProgress)
        {
            encryptor.EncryptFile(inputPath, outputPath, onProgress);
        }

        public void DecryptFile(string inputPath, string outputPath, Action<int> onProgress)
        {
            encryptor.DecryptFile(inputPath, outputPath, onProgress);
        }
    }

}

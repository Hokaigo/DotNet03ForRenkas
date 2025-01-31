using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class FileProcessingResult
    {
        public string FilePath { get; }
        public bool IsEncrypting { get; }
        public long FileSize { get; }

        public FileProcessingResult(string filePath, bool isEncrypting, long fileSize)
        {
            FilePath = filePath;
            IsEncrypting = isEncrypting;
            FileSize = fileSize;
        }
    }
}

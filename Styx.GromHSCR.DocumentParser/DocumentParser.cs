using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.DocumentParser
{
    public class DocumentParser
    {
        private string _folderPath;

        public DocumentParser(string folderPath)
        {
            _folderPath = folderPath;
        }

        public IEnumerable<IPrintInfo> ExecuteParsing()
        {
            var xlsFiles = Directory.GetFiles(_folderPath, "*.xls",SearchOption.AllDirectories);
            var xlsxFiles = Directory.GetFiles(_folderPath, "*.xlsx", SearchOption.AllDirectories);
            var pdfFiles = Directory.GetFiles(_folderPath, "*.pdf", SearchOption.AllDirectories);
            var txtFiles = Directory.GetFiles(_folderPath, "*.txt", SearchOption.AllDirectories);
            var docFiles = Directory.GetFiles(_folderPath, "*.doc", SearchOption.AllDirectories);
            var rtfFiles = Directory.GetFiles(_folderPath, "*.rtf", SearchOption.AllDirectories);

            var result = new List<IPrintInfo>();
            return result;
        }
    }
}

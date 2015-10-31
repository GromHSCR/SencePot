using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Styx.GromHSCR.FleaCatcher;
using PdfDocument = Spire.Pdf.PdfDocument;

namespace Styx.GromHSCR.SawMill
{
    public class SawMill
    {
        private static IEnumerable<string> _lumbers = new List<string> { };
        private string _outputFolder;
        private bool _isDesolateOriginal;
        private static IEnumerable<string> rtfLumber
        {
            get { return _lumbers.Where(p => p.ToLower().EndsWith(".rtf")); }
        }
        private static IEnumerable<string> pdfLumber
        {
            get { return _lumbers.Where(p => p.ToLower().EndsWith(".pdf")); }
        }

        public void GetLumber(IEnumerable<string> lumbers, string outputFolder, bool isDesolateOriginal = false)
        {
            _lumbers = lumbers;
            _outputFolder = outputFolder;
            _isDesolateOriginal = isDesolateOriginal;
        }

        public SawResult SawPdfWithResult()
        {
            var lumbersCount = pdfLumber.Count();
            var lumberCount = 0;
            var sawedCount = 0;
            var filesCount = 0;
            foreach (var lumber in pdfLumber)
            {
                try
                {
                    lumberCount++;
                    var helper = new AddressHelper();
                    var document = new Spire.Pdf.PdfDocument(File.OpenRead(lumber));
                    var endpage = document.Pages.Count;
                    if (endpage < 2) continue;
                    var filename = lumber.Split('\\').Last().Replace(".pdf", "");
                    for (int i = 0; i < endpage; i++)
                    {
                        PdfDocument pdf1 = new PdfDocument();
                        PdfPageBase page;
                        page = pdf1.Pages.Add(document.Pages[i].Size, new Spire.Pdf.Graphics.PdfMargins(0), PdfPageRotateAngle.RotateAngle270);
                        document.Pages[i].CreateTemplate().Draw(page, new System.Drawing.PointF(0, 0));
                        //var text = document.Pages[i].ExtractText();
                        //var address = helper.SearchAddress(text);
                        //var system = helper.SearchSystem(text);
                        //if (string.IsNullOrWhiteSpace(address)) continue;
                        pdf1.SaveToFile(_outputFolder + "/" + filename + (i + 1) + ".pdf");
                        filesCount++;
                    }
                    if (_isDesolateOriginal)
                        File.Delete(lumber);
                    document.Close();

                    sawedCount++;
                }
                catch (Exception e)
                {
                    return new SawResult(){ErrorMessage = e.Message};
                }
            }
            return new SawResult() { FilesCount = filesCount, LumberCount = lumberCount, SawedCount = sawedCount, IsOk = true };
        }

        public void SawPdf()
        {
            var lumbersCount = pdfLumber.Count();
            var lumberCount = 0;
            var sawedCount = 0;
            var filesCount = 0;
            foreach (var lumber in pdfLumber)
            {
                try
                {
                    lumberCount++;
                    var helper = new AddressHelper();
                    var document = new Spire.Pdf.PdfDocument(File.OpenRead(lumber));
                    var endpage = document.Pages.Count;
                    if (endpage < 2) continue;
                    var filename = lumber.Split('\\').Last().Replace(".pdf", "");
                    for (int i = 0; i < endpage; i++)
                    {
                        PdfDocument pdf1 = new PdfDocument();
                        PdfPageBase page;
                        page = pdf1.Pages.Add(document.Pages[i].Size, new Spire.Pdf.Graphics.PdfMargins(0), PdfPageRotateAngle.RotateAngle270);
                        document.Pages[i].CreateTemplate().Draw(page, new System.Drawing.PointF(0, 0));
                        //var text = document.Pages[i].ExtractText();
                        //var address = helper.SearchAddress(text);
                        //var system = helper.SearchSystem(text);
                        //if (string.IsNullOrWhiteSpace(address)) continue;
                        pdf1.SaveToFile(_outputFolder + "/" + filename + (i + 1) + ".pdf");
                        filesCount++;
                    }
                    if (_isDesolateOriginal)
                        File.Delete(lumber);
                    document.Close();

                    sawedCount++;
                }
                catch (Exception e)
                {

                }
            }
            Console.WriteLine("Распилено " + sawedCount + " из " + lumberCount + " pdf на " + filesCount + " файлов.");
        }

        public SawResult SawRtfWithResult()
        {
            var lumbersCount = rtfLumber.Count();
            var lumberCount = 0;
            var sawedCount = 0;
            var filesCount = 0;
            foreach (var lumber in rtfLumber)
            {
                try
                {
                    var helper = new AddressHelper();
                    var rtBox = new RichTextBox();
                    var woodstext = File.ReadAllText(lumber);
                    rtBox.Rtf = woodstext;
                    string plainText = rtBox.Text;
                    if (Regex.Matches(plainText, "адрес", RegexOptions.IgnoreCase).Count > 1)
                    {
                        lumberCount++;
                        var stringSeparators = new string[] { @"\pard\sect" };
                        var stringSeparators2 = new string[] { @"\pgwsxn11926" };
                        var doc = woodstext.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        var line = doc[0].Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);
                        var startLine = line[0];
                        var i = 0;
                        var k = doc.Count() - 1;
                        foreach (var d in doc)
                        {
                            var test1 = "";
                            if (i != 0)
                            {
                                test1 += startLine + @"\pgwsxn11926";
                            }
                            test1 += d + @"\pard\sect";
                            if (i != k)
                            {
                                test1 += "}";
                            }
                            var rtfBox = new RichTextBox();
                            rtfBox.Rtf = test1;
                            var rtfText = rtfBox.Text;
                            var address = helper.SearchAddress(rtfText);
                            var system = helper.SearchSystem(rtfText);
                            File.WriteAllText(_outputFolder + "/" + address + " " + system + ".rtf", test1);
                            i++;
                            filesCount++;
                        }
                        if (_isDesolateOriginal)
                            File.Delete(lumber);
                        sawedCount++;
                    }
                }
                catch (Exception e)
                {
                    return new SawResult(){ErrorMessage = e.Message};
                }
            }
            return new SawResult() { FilesCount = filesCount, LumberCount = lumberCount, SawedCount = sawedCount, IsOk = true };
        }

        public void SawRtf()
        {
            var lumbersCount = rtfLumber.Count();
            var lumberCount = 0;
            var sawedCount = 0;
            var filesCount = 0;
            foreach (var lumber in rtfLumber)
            {
                try
                {
                    var helper = new AddressHelper();
                    var rtBox = new RichTextBox();
                    var woodstext = File.ReadAllText(lumber);
                    rtBox.Rtf = woodstext;
                    string plainText = rtBox.Text;
                    if (Regex.Matches(plainText, "адрес", RegexOptions.IgnoreCase).Count > 1)
                    {
                        lumberCount++;
                        var stringSeparators = new string[] { @"\pard\sect" };
                        var stringSeparators2 = new string[] { @"\pgwsxn11926" };
                        var doc = woodstext.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        var line = doc[0].Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);
                        var startLine = line[0];
                        var i = 0;
                        var k = doc.Count() - 1;
                        foreach (var d in doc)
                        {
                            var test1 = "";
                            if (i != 0)
                            {
                                test1 += startLine + @"\pgwsxn11926";
                            }
                            test1 += d + @"\pard\sect";
                            if (i != k)
                            {
                                test1 += "}";
                            }
                            var rtfBox = new RichTextBox();
                            rtfBox.Rtf = test1;
                            var rtfText = rtfBox.Text;
                            var address = helper.SearchAddress(rtfText);
                            var system = helper.SearchSystem(rtfText);
                            File.WriteAllText(_outputFolder + "/" + address + " " + system + ".rtf", test1);
                            i++;
                            filesCount++;
                        }
                        if (_isDesolateOriginal)
                            File.Delete(lumber);
                        sawedCount++;
                    }
                }
                catch (Exception e)
                {

                }
            }
            Console.WriteLine("Распилено " + sawedCount + " из " + lumberCount + " rtf на " + filesCount + " файлов.");
        }
    }

    public class SawResult
    {
        public int LumberCount { get; set; }
        public int SawedCount { get; set; }
        public int FilesCount { get; set; }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
    }
}

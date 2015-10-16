using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Styx.GromHSCR;
using Styx.GromHSCR.DocumentBase.Documents;
using Styx.GromHSCR.Helpers;
using Styx.Messages;
using Styx.ViewModel;

namespace Styx.Documents
{
    public class PrintInfoListDocument : CollectionDocument<PrintInfoViewModel>
    {
        [Import(typeof(IPrintInfoService))]
        private IPrintInfoService _printInfoService;

        public ICommand EditCommand { get; set; }
        public ICommand ShowCommand { get; set; }
        public ICommand ExportToExcelCommand { get; set; }
        public ICommand CreateReportCommand { get; set; }
        public ICommand LoadPrintsCommand { get; set; }

        protected override void Initialization()
        {
            base.Initialization();
            Title = "Список распечаток";
            IconSource = Properties.Resources.List_Items_16.ToBitmapImage();
        }

        protected override void InitializationCommands()
        {
            base.InitializationCommands();

            ShowCommand = CreateCommand(ShowExecute, CanShowExecute);
            ExportToExcelCommand = CreateCommand(ExportToExcelExecute, CanExportToExcelExecute);
            LoadPrintsCommand = CreateCommand(LoadPrintsExecute, CanLoadPrintsExecute);
            CreateReportCommand = CreateCommand(CreateReportExecute, CanCreateReportExecute);
        }

        private void CreateReportExecute()
        {
            throw new NotImplementedException();
        }

        private void LoadPrintsExecute()
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var selectedPath = dialog.SelectedPath;
            ExecuteOperationAsync(() =>
            {
                var xlsFiles = Directory.GetFiles(selectedPath, "*.xls", SearchOption.AllDirectories);
                var xlsxFiles = Directory.GetFiles(selectedPath, "*.xlsx", SearchOption.AllDirectories);
                var pdfFiles = Directory.GetFiles(selectedPath, "*.pdf", SearchOption.AllDirectories);
                var txtFiles = Directory.GetFiles(selectedPath, "*.txt", SearchOption.AllDirectories);
                var docFiles = Directory.GetFiles(selectedPath, "*.doc", SearchOption.AllDirectories);
                var rtfFiles = Directory.GetFiles(selectedPath, "*.rtf", SearchOption.AllDirectories);
                var filesCount = xlsFiles.Count()
                                 + xlsxFiles.Count()
                                 + pdfFiles.Count()
                                 + txtFiles.Count()
                                 + docFiles.Count()
                                 + rtfFiles.Count();
                var countParsed = 0;
                MessengerInstance.Send(new ProgressMessage
                {
                    ProgressType = ProgressType.Indeterminate,
                    Text = "Обработано файлов " + countParsed + " из " + filesCount
                });
                foreach (var xlsFile in xlsFiles)
                {

                    countParsed++;
                    MessengerInstance.Send(new ProgressMessage
                    {
                        ProgressType = ProgressType.Indeterminate,
                        Text = "Обработано файлов " + countParsed + " из " + filesCount
                    });
                }
                foreach (var xlsxFile in xlsxFiles)
                {

                    countParsed++;
                    MessengerInstance.Send(new ProgressMessage
                    {
                        ProgressType = ProgressType.Indeterminate,
                        Text = "Обработано файлов " + countParsed + " из " + filesCount
                    });
                } foreach (var pdfFile in pdfFiles)
                {

                    countParsed++;
                    MessengerInstance.Send(new ProgressMessage
                    {
                        ProgressType = ProgressType.Indeterminate,
                        Text = "Обработано файлов " + countParsed + " из " + filesCount
                    });
                }
                foreach (var txtFile in txtFiles)
                {

                    countParsed++;
                    MessengerInstance.Send(new ProgressMessage
                    {
                        ProgressType = ProgressType.Indeterminate,
                        Text = "Обработано файлов " + countParsed + " из " + filesCount
                    });
                }
                foreach (var docFile in docFiles)
                {

                    countParsed++;
                    MessengerInstance.Send(new ProgressMessage
                    {
                        ProgressType = ProgressType.Indeterminate,
                        Text = "Обработано файлов " + countParsed + " из " + filesCount
                    });
                }
                foreach (var rtfFile in rtfFiles)
                {

                    countParsed++;
                    MessengerInstance.Send(new ProgressMessage
                    {
                        ProgressType = ProgressType.Indeterminate,
                        Text = "Обработано файлов " + countParsed + " из " + filesCount
                    });
                }
                MessengerInstance.Send(new ProgressMessage
                {
                    ProgressType = ProgressType.Stop,
                    Text = ""
                });
            }
                );
        }

        private void ExportToExcelExecute()
        {
            throw new NotImplementedException();
        }

        private bool CanCreateReportExecute()
        {
            return !IsBusy && SelectedItems.Any();
        }

        private bool CanLoadPrintsExecute()
        {
            return !IsBusy;
        }

        private bool CanExportToExcelExecute()
        {
            return !IsBusy && SelectedItems.Any();
        }

        private void ShowExecute()
        {
            throw new NotImplementedException();
        }

        private bool CanShowExecute()
        {
            return !IsBusy && SelectedItems.Any();
        }

        protected override void SaveChanges(Action callback)
        {
        }

        protected override void ChangeItems(IEnumerable<PrintInfoViewModel> itemsForChange)
        {
        }

        protected override void AddExecute()
        {
        }

        protected override void Refresh(Action<IEnumerable<PrintInfoViewModel>> onResult)
        {
            ExecuteOperationAsync(() => _printInfoService.Get500LatestPrintInfos(), result => onResult(result.Select(p => new PrintInfoViewModel(p))));
        }
    }
}

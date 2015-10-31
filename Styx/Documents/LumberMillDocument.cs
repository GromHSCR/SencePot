using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using Styx.GromHSCR.DocumentBase.Documents;
using Styx.GromHSCR.Helpers;
using Styx.GromHSCR.SawMill;
using Styx.Messages;
using Styx.ViewModel;

namespace Styx.Documents
{
    public class LumberMillDocument : Document<SawMillOptionsViewModel>
    {

        public ICommand SawCommand { get; set; }

        protected override void Initialization()
        {
            base.Initialization();
            Title = "Список распечаток";
            IconSource = Properties.Resources.Table_Link_16.ToBitmapImage();
        }

        protected override void InitializationCommands()
        {
            base.InitializationCommands();

            SawCommand = CreateCommand(SawExecute, CanSawExecute);
        }

        private void SawExecute()
        {
            ExecuteOperationAsync(() =>
            {
                var mill = new SawMill();
                var lumbers = new List<string>();

                if (ViewModel.IsSawRtf)
                {
                    var rtfs = Directory.GetFiles(ViewModel.InputFolder, "*.rtf", SearchOption.AllDirectories);
                    lumbers.AddRange(rtfs);
                }
                if (ViewModel.IsSawPdf)
                {
                    var pdfs = Directory.GetFiles(ViewModel.InputFolder, "*.pdf", SearchOption.AllDirectories);
                    lumbers.AddRange(pdfs);
                }
                mill.GetLumber(lumbers, ViewModel.OutputFolder, ViewModel.IsDesolateOriginal);
                var messagertf = "";
                if (ViewModel.IsSawRtf)
                {
                    MessengerInstance.Send(new ProgressMessage
                    {
                        ProgressType = ProgressType.Indeterminate,
                        Text = "Распил rtf..."
                    });
                    var resultRtf = mill.SawRtfWithResult();
                    messagertf = resultRtf.IsOk
                       ? ("Распилено " + resultRtf.SawedCount + " из " + resultRtf.LumberCount + " rtf на " +
                          resultRtf.FilesCount + " файлов.")
                       : ("Не удалось попилить rtf. Ошибка:" + resultRtf.ErrorMessage);
                }
                var messagepdf = "";
                if (ViewModel.IsSawPdf)
                {
                    MessengerInstance.Send(new ProgressMessage
                    {
                        ProgressType = ProgressType.Indeterminate,
                        Text = "Распил pdf..."
                    });
                    var resultPdf = mill.SawPdfWithResult();
                    messagepdf = resultPdf.IsOk
                        ? ("Распилено " + resultPdf.SawedCount + " из " + resultPdf.LumberCount + " pdf на " +
                           resultPdf.FilesCount + " файлов.")
                        : ("Не удалось попилить pdf. Ошибка:" + resultPdf.ErrorMessage);

                }
                return new { RTF = messagertf, PDF = messagepdf };
            }, result =>
            {
                if (!string.IsNullOrWhiteSpace(result.RTF))
                    MessageBox.Show(result.RTF);
                if (!string.IsNullOrWhiteSpace(result.PDF))
                    MessageBox.Show(result.PDF);
                MessengerInstance.Send(new ProgressMessage
                {
                    ProgressType = ProgressType.Stop,
                    Text = ""
                });
            });
        }

        private bool CanSawExecute()
        {
            return !IsBusy && !string.IsNullOrWhiteSpace(ViewModel.InputFolder) && !string.IsNullOrWhiteSpace(ViewModel.OutputFolder) && (ViewModel.IsSawPdf || ViewModel.IsSawRtf);
        }

        protected override void Add(SawMillOptionsViewModel viewModel, Action<SawMillOptionsViewModel> onResult)
        {
            throw new NotImplementedException();
        }

        protected override void Delete(SawMillOptionsViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        protected override void Refresh(Action<SawMillOptionsViewModel> onResult)
        {
            onResult(new SawMillOptionsViewModel());
        }

        protected override void Update(SawMillOptionsViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Styx.GromHSCR;
using Styx.GromHSCR.DocumentBase.Documents;
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
            ExecuteOperationAsync(() => _printInfoService.GetAllPrintInfos(), result => onResult(result.Select(p => new PrintInfoViewModel(p))));
        }
    }
}

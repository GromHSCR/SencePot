﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Styx.GromHSCR.DocumentBase.Documents;
using Styx.GromHSCR.Service.Api;
using Styx.ViewModel;

namespace Styx.Documents
{
    public class PrintInfoListDocument : CollectionDocument<PrintInfoViewModel>
    {
        [Import]
        private IPrintService _printService;

        public ICommand EditCommand { get; set; }
        public ICommand ShowCommand { get; set; }
        public ICommand ExportToExcelCommand { get; set; }
        public ICommand CreateReportCommand { get; set; }

        protected override void SaveChanges(Action callback)
        {
            throw new NotImplementedException();
        }

        protected override void ChangeItems(IEnumerable<PrintInfoViewModel> itemsForChange)
        {
            throw new NotImplementedException();
        }

        protected override void AddExecute()
        {
            throw new NotImplementedException();
        }

        protected override void Refresh(Action<IEnumerable<PrintInfoViewModel>> onResult)
        {
            ExecuteOperationAsync(() => _printService.GetAllPrintInfos(), result => onResult(result.Select(p => new PrintInfoViewModel(p))));
        }
    }
}
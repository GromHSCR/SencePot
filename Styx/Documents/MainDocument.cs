using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Styx.Actions;
using Styx.Base;
using Styx.GromHSCR.ActionBase.VisualActions;
using Styx.GromHSCR.DocumentBase.Documents;
using Styx.GromHSCR.DocumentBase.Managers;
using Styx.GromHSCR.MvvmBase.ViewModels;
using Styx.Messages;
using Styx.ViewModel;

namespace Styx.Documents
{
    public class MainDocument : ViewModelBase
    {
        // ReSharper disable once UnassignedField.Compiler
        [ImportMany]
        private IEnumerable<IVisualActionProvider> _actionProviders;

        [Import]
        public IDocumentManager DocumentManager { get; private set; }

        [Import]
        private IProgramInit _programInit;

        public IEnumerable<IVisualActionProvider> ActionProviders
        {
            get
            {
                return _actionProviders.OrderByDescending(p => p.Priority);
            }
        }

        public ICommand PrintInfoListCommand { get; set; }
        public ICommand LumberMillCommand { get; set; }

        public ProgressViewModel LoadingProgress { get; private set; }

        public void Init()
        {
			PrintInfoListCommand = DocumentManager.CreateDocumentCommand<PrintInfoListDocument>();
            LumberMillCommand = DocumentManager.CreateDocumentCommand<LumberMillDocument>();

            LoadingProgress = new ProgressViewModel();

            MessengerInstance.Register<ProgressMessage>(this, OnProgressMessageReceive);
        }
        
        private void OnProgressMessageReceive(ProgressMessage progressMessage)
        {
            LoadingProgress.ProcessProgressNotification(progressMessage);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SenceRep.Base;
using SenceRep.GromHSCR.ActionBase.VisualActions;
using SenceRep.GromHSCR.DocumentBase.Documents;
using SenceRep.GromHSCR.DocumentBase.Managers;
using SenceRep.GromHSCR.MvvmBase.ViewModels;
using SenceRep.Messages;
using SenceRep.ViewModel;

namespace SenceRep.Documents
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
        
        public ICommand ViewPrintsCommand { get; set; }

        public ProgressViewModel LoadingProgress { get; private set; }

        public void Init()
        {
            ViewPrintsCommand = DocumentManager.CreateDocumentCommand<PrintInfoListDocument>();

            LoadingProgress = new ProgressViewModel();

            MessengerInstance.Register<ProgressMessage>(this, OnProgressMessageReceive);
        }
        
        private void OnProgressMessageReceive(ProgressMessage progressMessage)
        {
            LoadingProgress.ProcessProgressNotification(progressMessage);
        }
    }
}

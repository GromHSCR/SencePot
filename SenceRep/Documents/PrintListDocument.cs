using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SenceRep.GromHSCR.DocumentBase.Documents;
using SenceRep.ViewModel;

namespace SenceRep.Documents
{
   public class PrintListDocument : CollectionDocument<PrintViewModel>
    {
       protected override void SaveChanges(Action callback)
       {
           throw new NotImplementedException();
       }

       protected override void ChangeItems(IEnumerable<PrintViewModel> itemsForChange)
       {
           throw new NotImplementedException();
       }

       protected override void AddExecute()
       {
           throw new NotImplementedException();
       }

       protected override void Refresh(Action<IEnumerable<PrintViewModel>> onResult)
       {
           throw new NotImplementedException();
       }
    }
}

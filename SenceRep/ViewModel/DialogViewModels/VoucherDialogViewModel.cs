using System.Collections.Generic;
using DevExpress.Mvvm;

namespace RedKassa.Promoter.ViewModel.DialogViewModels
{
	public class VoucherDialogViewModel : ViewModelBase
	{
		public VoucherDialogViewModel(string messageText, List<UnAcceptedSectorViewModel> sectors)
		{
			MessageText = messageText;
			UnAcceptedSectors = sectors;
		}

		public string MessageText { get; set; }
		
		public List<UnAcceptedSectorViewModel> UnAcceptedSectors { get; set; }
	}
}


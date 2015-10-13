using System.Collections.Generic;
using DevExpress.Mvvm;
using RedKassa.Promoter.ViewModel.Schemas;

namespace RedKassa.Promoter.ViewModel.DialogViewModels
{
	public class UnAcceptedSectorViewModel : ViewModelBase
	{
		public UnAcceptedSectorViewModel(string voucherSectorName, List<AgentSectorViewModel> sectors)
		{
			VoucherSectorName = voucherSectorName;
			Sectors = sectors;
		}

		public List<AgentSectorViewModel> Sectors { get; set; }

		private AgentSectorViewModel _selectedSector;

		public AgentSectorViewModel SelectedSector
		{
			get
			{
				return _selectedSector;
			}
			set
			{
				if (Equals(_selectedSector, value)) return;

				_selectedSector = value;
				RaisePropertyChanged("SelectedSector");
			}
		}

		public string VoucherSectorName { get; set; }
	}
}

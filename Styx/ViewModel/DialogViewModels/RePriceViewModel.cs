using System.Collections.Generic;
using RedKassa.Promoter.MvvmBase.ViewModels;

namespace RedKassa.Promoter.ViewModel.DialogViewModels
{
	public class RePriceViewModel : ViewModelBase
	{
		private List<RePriceOperationType> _operationTypes = new List<RePriceOperationType> { new RePriceOperationType { OperationName = Properties.Resources.Raise }, new RePriceOperationType { OperationName = Properties.Resources.Reduce } };
		public List<RePriceOperationType> OperationTypes
		{
			get
			{
				return _operationTypes;
			}
			set
			{
				if (_operationTypes == value) return;

				RaisePropertyChanging("OperationTypes");
				_operationTypes = value;
				RaisePropertyChanged("OperationTypes");
			}
		}

		private List<RePriceOperationCurrency> _operationCurrencys = new List<RePriceOperationCurrency> { new RePriceOperationCurrency { OperationName = Properties.Resources.Percent }, new RePriceOperationCurrency { OperationName = Properties.Resources.RePriceViewModel_currency } };
		public List<RePriceOperationCurrency> OperationCurrencys
		{
			get
			{
				return _operationCurrencys;
			}
			set
			{
				if (_operationCurrencys == value) return;

				RaisePropertyChanging("OperationCurrencys");
				_operationCurrencys = value;
				RaisePropertyChanged("OperationCurrencys");
			}
		}

		private RePriceOperationType _selectedOperationType;
		public RePriceOperationType SelectedOperationType
		{
			get
			{
				return _selectedOperationType;
			}
			set
			{
				if (_selectedOperationType == value) return;

				RaisePropertyChanging("SelectedOperationType");
				_selectedOperationType = value;
				RaisePropertyChanged("SelectedOperationType");
			}
		}

		private RePriceOperationCurrency _selectedOperationCurrency;
		public RePriceOperationCurrency SelectedOperationCurrency
		{
			get
			{
				return _selectedOperationCurrency;
			}
			set
			{
				if (_selectedOperationCurrency == value) return;

				RaisePropertyChanging("SelectedOperationCurrency");
				_selectedOperationCurrency = value;
				RaisePropertyChanged("SelectedOperationCurrency");
			}
		}

		private int _count;
		public int Count
		{
			get
			{
				return _count;
			}
			set
			{
				if (_count == value) return;

				RaisePropertyChanging("Count");
				_count = value;
				RaisePropertyChanged("Count");
			}
		}

		private bool _isRound;
		public bool IsRound
		{
			get
			{
				return _isRound;
			}
			set
			{
				if (_isRound == value) return;

				RaisePropertyChanging("IsRound");
				_isRound = value;
				RaisePropertyChanged("IsRound");
			}
		}

		private int _roundPrice;
		public int RoundPrice
		{
			get
			{
				return _roundPrice;
			}
			set
			{
				if (_roundPrice == value) return;

				RaisePropertyChanging("RoundPrice");
				_roundPrice = value;
				RaisePropertyChanged("RoundPrice");
			}
		}
	}

	public class RePriceOperationType
	{
		public string OperationName { get; set; }
	}

	public class RePriceOperationCurrency
	{
		public string OperationName { get; set; }
	}
}

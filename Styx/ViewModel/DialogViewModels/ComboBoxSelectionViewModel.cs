using System.Collections.Generic;
using DevExpress.Mvvm;
using RedKassa.Promoter.MvvmBase.ViewModels;

namespace RedKassa.Promoter.ViewModel.DialogViewModels
{
	public class ComboBoxSelecionViewModel : ViewModelBase<object>
	{
		public ComboBoxSelecionViewModel(string messageText, string labelText, List<object> objects)
		{
			MessageText = messageText;
			LabelText = labelText;
			ItemsSourceCollection = objects;
		}

		public string MessageText { get; set; }

		public string LabelText { get; set; }

		public List<object> ItemsSourceCollection { get; set; }

		public object SelectedItem { get; set; }
	}
}


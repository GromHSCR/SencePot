using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SenceRep.GromHSCR.MvvmBase.ViewModels;

namespace SenceRep.GromHSCR.DocumentBase.Documents
{
	public abstract class CollectionSelectableDocument<TListItem> : CollectionDocument<TListItem>
		where TListItem : ViewModelBase, ISelectableViewModel
	{
		private bool CanDeleteExecute()
		{
			return !IsBusy && SelectedItems.Any();
		}

		private void DeleteExecute()
		{
			DeleteItems(SelectedItems.ToList());
		}

		protected override void SaveChanges(Action callback)
		{
			throw new NotImplementedException();
		}

		protected override void InitializationCommands()
		{
			base.InitializationCommands();

			DeleteCommand = CreateCommand(DeleteExecute, CanDeleteExecute);
			ChangeCommand = CreateCommand(ChangeExecute, CanChangeExecute);
		}

		protected virtual bool CanChangeExecute()
		{
			return !IsBusy && ItemsSourceCollection.Any(p => p.IsSelected);
		}

		protected virtual void ChangeExecute()
		{
			ChangeItems(ItemsSourceCollection.Where(p => p.IsSelected).ToList());
		}

		protected override void SaveExecute()
		{
			throw new NotImplementedException();
		}

		protected abstract void DeleteItems(IEnumerable<TListItem> itemsForDelete);

		public ICommand ChangeCommand { get; protected set; }

		public ICommand DeleteCommand { get; protected set; }
	}
}

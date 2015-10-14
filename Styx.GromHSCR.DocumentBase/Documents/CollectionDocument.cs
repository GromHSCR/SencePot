using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Styx.GromHSCR.MvvmBase.Modifications;
using Styx.GromHSCR.MvvmBase.ViewModels;

namespace Styx.GromHSCR.DocumentBase.Documents
{
	public abstract class CollectionDocument<TListItem> : Document where TListItem : ViewModelBase
	{
		private ObservableCollection<TListItem> _itemsSourceCollection;
		private ICollectionView _itemsSource;
		private TListItem _currentItem;
		private ObservableCollection<TListItem> _selectedItems;

		private void ListItemIsModifiedChanged(object sender, EventArgs e)
		{
			var modification = sender as IModification;

			if (modification == null) return;

			IsModified = modification.IsModified;
		}

		protected override void InitializationCommands()
		{
			base.InitializationCommands();

			AddCommand = CreateCommand(AddExecute, CanAddExecute);
			ChangeCurrentItemCommand = CreateCommand(ChangeCurrentItemExecute, CanChangeItemOnDoubleClickExecute);
		}

		protected virtual bool CanChangeItemOnDoubleClickExecute()
		{
			return !IsBusy && CurrentItem != null;
		}

		protected virtual void ChangeCurrentItemExecute()
		{
			if (CurrentItem == null) return;

			ChangeItems(new List<TListItem> { CurrentItem });
		}

		protected virtual bool CanAddExecute()
		{
			return !IsBusy;
		}

		protected abstract void ChangeItems(IEnumerable<TListItem> itemsForChange);

		protected abstract void AddExecute();

		protected abstract void Refresh(Action<IEnumerable<TListItem>> onResult);

		protected override void RefreshExecute()
		{
			Action<IEnumerable<TListItem>> onResult =
				result =>
				{
					if (result == null)
					{
						ItemsSource = null;

						foreach (var listItem in ItemsSourceCollection)
						{
							listItem.IsModifiedChanged -= ListItemIsModifiedChanged;
						}

						ItemsSourceCollection.Clear();
					}
					else
					{
						ItemsSourceCollection = new ObservableCollection<TListItem>(result);

						foreach (var listItem in ItemsSourceCollection)
						{
							listItem.IsModifiedChanged += ListItemIsModifiedChanged;
						}
					}

					IsModified = ItemsSourceCollection.Any(p => p.IsModified);
				};

			ExecuteWithCheckForUnsaved(() => Refresh(onResult));
		}

		public ICommand AddCommand { get; protected set; }

		public ICommand ChangeCurrentItemCommand { get; protected set; }

		public override bool IsValid
		{
			get
			{
				return base.IsValid && ItemsSourceCollection.All(p => p.IsValid);
			}
		}

		public ObservableCollection<TListItem> SelectedItems
		{
			get
			{
				if (_selectedItems != null) return _selectedItems;
				_selectedItems = new ObservableCollection<TListItem>();
				_selectedItems.CollectionChanged += SelectedItemsChanged;
				return _selectedItems;
			}
		}

		// newly added items can be handled here
		protected virtual void SelectedItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			
		}

		public TListItem CurrentItem
		{
			get { return _currentItem; }
			set
			{
				if (_currentItem == value) return;

				RaisePropertyChanging("CurrentItem");
				_currentItem = value;
				_itemsSource.MoveCurrentTo(_currentItem);
				RaisePropertyChanged("CurrentItem");
			}
		}

		public ICollectionView ItemsSource
		{
			get { return _itemsSource; }
			protected set
			{
				if (value == _itemsSource) return;
				_itemsSource = value;
				RaisePropertyChanged("ItemsSource");
			}
		}

		public ObservableCollection<TListItem> ItemsSourceCollection
		{
			get
			{
				if (_itemsSourceCollection != null) return _itemsSourceCollection;

				_itemsSourceCollection = new ObservableCollection<TListItem>();
				ItemsSource = CollectionViewSource.GetDefaultView(_itemsSourceCollection);

				return _itemsSourceCollection;
			}
			protected set
			{
				if (value == _itemsSourceCollection) return;

				_itemsSourceCollection = value;
				RaisePropertyChanged("ItemsSourceCollection");
				ItemsSource = CollectionViewSource.GetDefaultView(_itemsSourceCollection);
			}
		}
	}
}

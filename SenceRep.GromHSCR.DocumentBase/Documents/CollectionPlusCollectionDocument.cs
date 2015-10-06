using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace SenceRep.GromHSCR.DocumentBase.Documents
{
	public abstract class CollectionPlusCollectionDocument<TListItem, TListThing> : Document
		where TListItem : ViewModelBase
		where TListThing : ViewModelBase
	{
		private ObservableCollection<TListItem> _itemsSourceCollection;
		private ObservableCollection<TListThing> _thingsSourceCollection;
		private ICollectionView _itemsSource;
		private TListItem _currentItem;
		private TListThing _currentThing;
		private ObservableCollection<TListItem> _selectedItems;
		private ObservableCollection<TListThing> _selectedThings;
		private ICollectionView _thingsSource;

		private void ListItemIsModifiedChanged(object sender, EventArgs e)
		{
			var modification = sender as IModification;

			if (modification == null) return;

			IsModified = modification.IsModified;
		}

		private void ListThingIsModifiedChanged(object sender, EventArgs e)
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
			ChangeCurrentThingCommand = CreateCommand(ChangeCurrentThingExecute, CanChangeThingOnDoubleClickExecute);
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

		protected virtual bool CanChangeThingOnDoubleClickExecute()
		{
			return !IsBusy && CurrentThing != null;
		}

		protected virtual void ChangeCurrentThingExecute()
		{
			if (CurrentThing == null) return;

			ChangeThings(new List<TListThing> { CurrentThing });
		}

		protected virtual bool CanAddExecute()
		{
			return !IsBusy;
		}

		protected abstract void ChangeItems(IEnumerable<TListItem> itemsForChange);

		protected abstract void ChangeThings(IEnumerable<TListThing> thingsForChange);

		protected abstract void AddExecute();

		protected abstract void Refresh(Action<IEnumerable<TListItem>> onResult, Action<IEnumerable<TListThing>> onResults);

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
			Action<IEnumerable<TListThing>> onResults =
				result =>
				{
					if (result == null)
					{
						ThingsSource = null;

						foreach (var listThing in ThingsSourceCollection)
						{
							listThing.IsModifiedChanged -= ListThingIsModifiedChanged;
						}

						ThingsSourceCollection.Clear();
					}
					else
					{
						ThingsSourceCollection = new ObservableCollection<TListThing>(result);

						foreach (var listThing in ThingsSourceCollection)
						{
							listThing.IsModifiedChanged += ListThingIsModifiedChanged;
						}
					}

					IsModified = ThingsSourceCollection.Any(p => p.IsModified);
				};
			ExecuteWithCheckForUnsaved(() => Refresh(onResult, onResults));
		}

		public ICommand AddCommand { get; protected set; }

		public ICommand ChangeCurrentItemCommand { get; protected set; }
		public ICommand ChangeCurrentThingCommand { get; protected set; }

		public override bool IsValid
		{
			get
			{
				return base.IsValid && ItemsSourceCollection.All(p => p.IsValid) && ThingsSourceCollection.All(p => p.IsValid);
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

		public ObservableCollection<TListThing> SelectedThings
		{
			get
			{
				if (_selectedItems != null) return _selectedThings;
				_selectedThings = new ObservableCollection<TListThing>();
				_selectedThings.CollectionChanged += SelectedItemsChanged;
				return _selectedThings;
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

		public TListThing CurrentThing
		{
			get { return _currentThing; }
			set
			{
				if (_currentThing == value) return;

				RaisePropertyChanging("CurrentThing");
				_currentThing = value;
				_thingsSource.MoveCurrentTo(_currentThing);
				RaisePropertyChanged("CurrentThing");
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

		public ICollectionView ThingsSource
		{
			get { return _thingsSource; }
			protected set
			{
				if (value == _thingsSource) return;
				_thingsSource = value;
				RaisePropertyChanged("ThingsSource");
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

		public ObservableCollection<TListThing> ThingsSourceCollection
		{
			get
			{
				if (_thingsSourceCollection != null) return _thingsSourceCollection;

				_thingsSourceCollection = new ObservableCollection<TListThing>();
				ItemsSource = CollectionViewSource.GetDefaultView(_thingsSourceCollection);

				return _thingsSourceCollection;
			}
			protected set
			{
				if (value == _thingsSourceCollection) return;

				_thingsSourceCollection = value;
				RaisePropertyChanged("ThingsSourceCollection");
				ThingsSource = CollectionViewSource.GetDefaultView(_thingsSourceCollection);
			}
		}
	}
}

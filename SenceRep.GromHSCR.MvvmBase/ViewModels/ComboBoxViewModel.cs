using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using SenceRep.GromHSCR.MvvmBase.Events.EventHandlerArgs;
using SenceRep.GromHSCR.MvvmBase.Events.EventHandlers;

namespace SenceRep.GromHSCR.MvvmBase.ViewModels
{
	public class ComboBoxViewModel<TViewModel> : IList<TViewModel>, INotifyCollectionChanged, INotifyPropertyChanged where TViewModel : ViewModelBase
	{
		private const string COUNT_STRING = "Count";
		private const string INDEXER_NAME = "Item[]";

		private ObservableCollection<TViewModel> _collection;
		private TViewModel _selectedItem;

		private void AddHandlersToCollection(ObservableCollection<TViewModel> collection)
		{
			if (collection == null) throw new ArgumentNullException("collection");

			collection.CollectionChanged += OnCollectionChanged;
			((INotifyPropertyChanged)collection).PropertyChanged += OnCollectionPropertyChanged;
		}

		private void OnCollectionPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChanged(e.PropertyName);
		}

		private void RemoveHandlersToCollection(ObservableCollection<TViewModel> collection)
		{
			if (collection == null) throw new ArgumentNullException("collection");

			collection.CollectionChanged -= OnCollectionChanged;
			((INotifyPropertyChanged)collection).PropertyChanged -= OnCollectionPropertyChanged;
		}

		private void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));

			if (propertyName != "SelectedItem") return;

			OnSelectedItemChanged(new SelectedItemEventHandlerArgs<TViewModel> { SelectedItem = SelectedItem });
		}

		private void OnSelectedItemChanged(SelectedItemEventHandlerArgs<TViewModel> args)
		{
			var handler = SelectedItemChanged;
			if (handler != null) handler(this, args);
		}

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			var handler = CollectionChanged;
			if (handler != null) handler(this, notifyCollectionChangedEventArgs);
		}

		protected ObservableCollection<TViewModel> Collection
		{
			get
			{
				if (_collection != null) return _collection;

				_collection = new ObservableCollection<TViewModel>();
				AddHandlersToCollection(_collection);

				return _collection;
			}
			set
			{
				if (value == _collection) return;

				if (_collection != null)
					RemoveHandlersToCollection(_collection);

				if (value != null)
					AddHandlersToCollection(value);

				_collection = value;
			}
		}

		public ComboBoxViewModel()
		{

		}

		public ComboBoxViewModel(IEnumerable<TViewModel> collection)
		{
			if (collection == null) throw new ArgumentNullException("collection");

			Collection = new ObservableCollection<TViewModel>(collection);
		}

		public ComboBoxViewModel(IEnumerable<TViewModel> collection, TViewModel selectedItem)
		{
			if (collection == null) throw new ArgumentNullException("collection");

			_selectedItem = selectedItem;
			Collection = new ObservableCollection<TViewModel>(collection);
		}

		public void AddRange(IEnumerable<TViewModel> enumerable)
		{
			if (enumerable == null) throw new ArgumentNullException("enumerable");

			if (!enumerable.Any()) return;

			var addCollection = new List<TViewModel>();

			if (Collection.Count > 0)
			{
				addCollection.AddRange(Collection);
			}

			addCollection.AddRange(enumerable);

			Collection = new ObservableCollection<TViewModel>(addCollection);

			OnPropertyChanged(COUNT_STRING);
			OnPropertyChanged(INDEXER_NAME);
			OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, enumerable.ToList()));
		}

		public IEnumerator<TViewModel> GetEnumerator()
		{
			return Collection.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(TViewModel item)
		{
			Collection.Add(item);
		}

		public void Clear()
		{
			Collection.Clear();
		}

		public bool Contains(TViewModel item)
		{
			return Collection.Contains(item);
		}

		public void CopyTo(TViewModel[] array, int arrayIndex)
		{
			Collection.CopyTo(array, arrayIndex);
		}

		public bool Remove(TViewModel item)
		{
			return Collection.Remove(item);
		}

		public int Count
		{
			get { return Collection.Count; }
		}

		bool ICollection<TViewModel>.IsReadOnly
		{
			get { return false; }
		}

		public int IndexOf(TViewModel item)
		{
			return Collection.IndexOf(item);
		}

		public void Insert(int index, TViewModel item)
		{
			Collection.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			Collection.RemoveAt(index);
		}

		public TViewModel this[int index]
		{
			get
			{
				return Collection == null ? null : Collection[index];
			}
			set
			{
				if (Collection == null)
					Collection = new ObservableCollection<TViewModel>();

				Collection[index] = value;
			}
		}

		public TViewModel SelectedItem
		{
			get
			{
				return _selectedItem;
			}

			set
			{
				if (_selectedItem == value) return;

				_selectedItem = value;
				OnPropertyChanged("SelectedItem");
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;
		public event PropertyChangedEventHandler PropertyChanged;
		public event SelectedItemEventHandler<TViewModel> SelectedItemChanged;
	}
}

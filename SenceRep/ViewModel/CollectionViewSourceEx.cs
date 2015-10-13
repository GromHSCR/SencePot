using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace SenceRep.ViewModel
{
	public static class CollectionViewSourceEx
	{
		public static ICollectionView GetDefaultView(object source, Predicate<object> filter)
		{
			var collectionView = CollectionViewSource.GetDefaultView(source);
			collectionView.Filter = filter;
			return collectionView;
		}

		public static void RefreshIfNotNull(this ICollectionView collectionView)
		{
			if (collectionView != null)
				collectionView.Refresh();
		}

        public static ListCollectionView GetDefaultListCollectionView<T>(List<T> list = null)
        {
	        var result = new ListCollectionView(list ?? new List<T>());
            return result;
        }
	}
}

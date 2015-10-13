using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedKassa.Promoter.MvvmBase.ViewModels;

namespace RedKassa.Promoter.ViewModel.DialogViewModels
{
	public class NotificationViewModel : ViewModelBase
	{

		public NotificationViewModel(string notificationMessage, bool isShowNotification)
		{
			if (notificationMessage == null) throw new ArgumentNullException("notificationMessage");
			_notificationMessage = notificationMessage;
			_isShowNotification = isShowNotification;
		}

		private string _notificationMessage;
		public string NotificationMessage
		{
			get { return _notificationMessage; }
			set
			{
				if (_notificationMessage == value) return;
				RaisePropertyChanging("NotificationMessage");
				_notificationMessage = value;
				RaisePropertyChanged("NotificationMessage");
			}
		}

		private bool _isShowNotification;
		public bool IsShowNotification
		{
			get { return _isShowNotification; }
			set
			{
				if (_isShowNotification == value) return;
				RaisePropertyChanging("IsShowNotification");
				_isShowNotification = value;
				RaisePropertyChanged("IsShowNotification");
			}
		}

	}
}

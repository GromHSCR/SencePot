using Styx.GromHSCR.MvvmBase.ViewModels;

namespace Styx.ViewModel
{
	public class ProgressViewModel : ViewModelBase
	{
		double _currentValue;
		double _minValue;
		double _maxValue;
		bool _isVisible;
		bool _isIndeterminate;
		string _progressText;

		public ProgressViewModel()
		{
			this.ResetAllValues();
		}

		public void ProcessProgressNotification(ProgressMessage message)
		{
			this.IsVisible = (message.ProgressType != ProgressType.Stop);

			this.MinValue = message.MinValue;
			this.MaxValue = message.MaxValue;
			this.CurrentValue = message.CurrentValue;
			this.ProgressText = message.Text;
			this.IsIndeterminate = message.ProgressType == ProgressType.Indeterminate;
		}

		public void ProcessValues(int current, int max)
		{
			this.IsIndeterminate = false;

			if (this.MinValue != 0)
				this.MinValue = 0;

			if (this.MaxValue != max)
				this.MaxValue = max;

			this.CurrentValue = current;
		}

		public void ResetAllValues()
		{
			this.MinValue = 0;
			this.MaxValue = 100;
			this.CurrentValue = 0;
			this.IsIndeterminate = true;
			this.IsVisible = false;
		}

		public double MinValue
		{
			get { return _minValue; }
			set
			{
				if (_minValue != value)
				{
					_minValue = value;
					RaisePropertyChanged("MinValue");
				}
			}
		}

		public double MaxValue
		{
			get { return _maxValue; }
			set
			{
				if (_maxValue != value)
				{
					_maxValue = value;
					RaisePropertyChanged("MaxValue");
				}
			}
		}

		public double CurrentValue
		{
			get { return _currentValue; }
			set
			{
				if (_currentValue != value)
				{
					_currentValue = value;
					RaisePropertyChanged("CurrentValue");
				}
			}
		}

		public bool IsVisible
		{
			get { return _isVisible; }
			set
			{
				if (_isVisible != value)
				{
					_isVisible = value;
					RaisePropertyChanged("IsVisible");
				}
			}
		}

		public string ProgressText
		{
			get { return _progressText; }
			set
			{
				if (_progressText != value)
				{
					_progressText = value;
					RaisePropertyChanged("ProgressText");
				}
			}
		}

		public bool IsIndeterminate
		{
			get { return _isIndeterminate; }
			set
			{
				if (_isIndeterminate != value)
				{
					_isIndeterminate = value;
					RaisePropertyChanged("IsIndeterminate");
				}
			}
		}
	}
}

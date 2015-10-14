using System;
using System.ComponentModel.Composition;
using System.Windows;
using SplashScreen = Styx.Base.DXSplashScreen.SplashScreen;

namespace Styx.Base
{
	public class InitEventArgs : EventArgs
	{
		public InitEventArgs(bool isInit)
		{
			IsInit = isInit;
		}

		public bool IsInit { get; private set; }
	}

	[Export(typeof(IProgramInit))]
	[PartCreationPolicy(CreationPolicy.Any)]
	public class ProgramInit : IProgramInit
	{
        //private static readonly ILog _log = LogManager.GetLogger(typeof(Program));
		private bool _isInit;
		private Action _action;

		public bool IsInit
		{
			get { return _isInit; }
			set
			{
				if (_isInit == value) return;

				_isInit = value;
				OnInitEvent(value);
			}
		}
		public ProgramInit()
		{
			Initialization();
		}

		private void Initialization()
		{
			IsInit = false;
		}

		public void OnInitExec(Action action)
		{
			_action = action;

			if (!IsInit)
			{
				_action.Invoke();
				_action = null;
			}
			else
			{
				//splash
				if (!DevExpress.Xpf.Core.DXSplashScreen.IsActive)
				{
					DevExpress.Xpf.Core.DXSplashScreen.Show<SplashScreen>();
				}
			}

		}

		private void OnInitEvent()
		{
			if (IsInit || _action == null) return;
			if (DevExpress.Xpf.Core.DXSplashScreen.IsActive)
			{
				DevExpress.Xpf.Core.DXSplashScreen.Close();
				Invoke();
			}
		}

		public void Invoke()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				_action.Invoke();
				_action = null;
			});
		}
		public event EventHandler<InitEventArgs> InitEvent;

		protected virtual void OnInitEvent(bool isInit)
		{
			var handler = InitEvent;
			OnInitEvent();
			if (handler != null) handler(this, new InitEventArgs(isInit));
		}
	}
}

﻿using System;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Core;

namespace SenceRep.Base.DXSplashScreen
{
	/// <summary>
	/// Interaction logic for SplashScreen.xaml
	/// </summary>
	public partial class SplashScreen : Window, ISplashScreen
	{
		public SplashScreen()
		{
			InitializeComponent();
			this.board.Completed += OnAnimationCompleted;
		}

		#region ISplashScreen
		public void Progress(double value)
		{
			progressBar.Value = value;
		}
		public void CloseSplashScreen()
		{
			this.board.Begin(this);
		}
		public void SetProgressState(bool isIndeterminate)
		{
			progressBar.IsIndeterminate = isIndeterminate;
		}
		#endregion

		#region Event Handlers
		void OnAnimationCompleted(object sender, EventArgs e)
		{
			this.board.Completed -= OnAnimationCompleted;
			this.Close();
		}
		#endregion

		private void SplashScreen_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}
	}
}

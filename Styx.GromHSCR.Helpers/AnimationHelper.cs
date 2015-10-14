using System;
using System.Windows;

namespace Styx.GromHSCR.Helpers
{
	public class AnimationHelper
	{

		public Guid Id
		{
			get { return Guid.NewGuid(); }
		}

		public static bool GetIsStartAnimation(DependencyObject obj)
		{
			return obj != null && (bool)obj.GetValue(IsStartAnimationProperty);
		}
		public static void SetIsStartAnimation(DependencyObject obj, bool value)
		{
			if (obj != null)
				obj.SetValue(IsStartAnimationProperty, value);
		}
		public static readonly DependencyProperty IsStartAnimationProperty =
			DependencyProperty.RegisterAttached("IsStartAnimation", typeof(bool), typeof(AnimationHelper), new PropertyMetadata(false));

		public static void RunAnimation(DependencyObject obj)
		{
			SetIsStartAnimation(obj, true);
			SetIsStartAnimation(obj, false);
		}
	}
}
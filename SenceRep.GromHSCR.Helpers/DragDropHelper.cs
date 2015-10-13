using System.Windows;

namespace SenceRep.GromHSCR.Helpers
{
	public class DragDropHelper
	{
		public static bool GetIsDragged(DependencyObject obj)
		{
			return obj != null && (bool)obj.GetValue(IsDraggedProperty);
		}
		public static void SetIsDragged(DependencyObject obj, bool value)
		{
			if (obj != null)
				obj.SetValue(IsDraggedProperty, value);
		}
		public static readonly DependencyProperty IsDraggedProperty =
			DependencyProperty.RegisterAttached("IsDragged", typeof(bool), typeof(AnimationHelper), new PropertyMetadata(false));
		public static void DragStart(DependencyObject obj)
		{
			SetIsDragged(obj, true);
		}
		public static void DragEnd(DependencyObject obj)
		{
			SetIsDragged(obj, false);
		}
	}
}

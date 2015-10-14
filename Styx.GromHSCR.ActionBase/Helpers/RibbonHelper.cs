using System.Windows;
using DevExpress.Xpf.Ribbon;

namespace Styx.GromHSCR.ActionBase.Helpers
{
	public static class RibbonHelper
	{
		static bool _isUpdating;

		public static bool GetIsSelected(RibbonPage obj)
		{
			return (bool)obj.GetValue(IsSelectedProperty);
		}

		public static void SetIsSelected(RibbonPage obj, bool value)
		{
			obj.SetValue(IsSelectedProperty, value);
		}

		public static readonly DependencyProperty IsSelectedProperty =
			DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(RibbonHelper),
			new FrameworkPropertyMetadata(false,
				null,
				(d, e) =>
				{
					if (!_isUpdating)
					{
						_isUpdating = true;

						var tab = d as RibbonPage;
						if (tab != null)
						{
							var binding = tab.GetBindingExpression(IsSelectedProperty);
							if (binding != null)
								binding.UpdateTarget();
							tab.IsSelected = GetIsSelected(tab);
						}

						_isUpdating = false;
					}
					return e;
				}));
	}
}

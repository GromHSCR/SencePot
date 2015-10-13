// ===================================
// <copyright>LogoUI Co.</copyright>
// <author>Vlad Spivak</author>
// <email>mailto:vlads@logoui.co.il</email>
// <created>16/00/11</created>
// <lastedit>16/00/11</lastedit>

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// ====================================================================================//


using System.Collections.Generic;
using System.Windows;
using System.Windows.Interactivity;
using TriggerBase = System.Windows.Interactivity.TriggerBase;

namespace SenceRep.GromHSCR.Helpers
{
	/// <summary>
	/// <see cref="FrameworkTemplate"/> for InteractivityElements instance
	/// <remarks>Subclassed for forward compatibility, perhaps one day <see cref="FrameworkTemplate"/> </remarks>
	/// <remarks>will not be partially internal</remarks>
	/// </summary>
	public class InteractivityTemplate : DataTemplate
	{

	}

	/// <summary>
	/// Holder for interactivity entries
	/// </summary>
	public class InteractivityItems : FrameworkElement
	{
		private List<Behavior> _behaviors;
		private List<TriggerBase> _triggers;

		/// <summary>
		/// Storage for triggers
		/// </summary>
		public new List<TriggerBase> Triggers
		{
			get { return _triggers ?? (_triggers = new List<TriggerBase>()); }
		}

		/// <summary>
		/// Storage for Behaviors
		/// </summary>
		public List<Behavior> Behaviors
		{
			get { return _behaviors ?? (_behaviors = new List<Behavior>()); }
		}

		#region Template attached property

		public static InteractivityTemplate GetTemplate(DependencyObject obj)
		{
			return (InteractivityTemplate)obj.GetValue(TemplateProperty);
		}

		public static void SetTemplate(DependencyObject obj, InteractivityTemplate value)
		{
			obj.SetValue(TemplateProperty, value);
		}

		public static readonly DependencyProperty TemplateProperty =
			DependencyProperty.RegisterAttached("Template",
			typeof(InteractivityTemplate),
			typeof(InteractivityItems),
			new PropertyMetadata(default(InteractivityTemplate), OnTemplateChanged));

		private static void OnTemplateChanged(
			DependencyObject d,
			DependencyPropertyChangedEventArgs e)
		{
			var dt = (InteractivityTemplate)e.NewValue;

			if (dt == null) return;
#if(!SILVERLIGHT)


			dt.Seal();
#endif
			var ih = (InteractivityItems)dt.LoadContent();
			BehaviorCollection bc = Interaction.GetBehaviors(d);
			var tc = Interaction.GetTriggers(d);

			foreach (Behavior behavior in ih.Behaviors)
				bc.Add(behavior);

			foreach (TriggerBase trigger in ih.Triggers)
				tc.Add(trigger);


		}

		#endregion
	}
}

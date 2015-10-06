using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using SenceRep.GromHSCR.ActionBase.Common;
using SenceRep.GromHSCR.ActionBase.Helpers;
using SenceRep.GromHSCR.ActionBase.VisualActions;

namespace SenceRep.GromHSCR.ActionBase.Adapters
{
	public class ActionsToRibbonAdapter
	{
		// #warning Hardcoded binding paths
		const string ACTIVE_DOCUMENT_PATH = "DocumentManager.ActiveDocument";
		const string DOCUMENT_MANAGER_PATH = "DocumentManager";

		public static IEnumerable<IVisualActionProvider> GetActionProviders(RibbonControl obj)
		{
			return (IEnumerable<IVisualActionProvider>)obj.GetValue(ActionProvidersProperty);
		}

		public static void SetActionProviders(RibbonControl obj, IEnumerable<IVisualActionProvider> value)
		{
			obj.SetValue(ActionProvidersProperty, value);
		}

		public static readonly DependencyProperty ActionProvidersProperty =
			DependencyProperty.RegisterAttached("ActionProviders", typeof(IEnumerable<IVisualActionProvider>), typeof(ActionsToRibbonAdapter),
			new UIPropertyMetadata(null,
				(d, e) =>
				{
					if (e.NewValue == null) return;
					var ribbon = d as RibbonControl;
					if (ribbon != null && !ribbon.IsLoaded)
						((RibbonControl)d).Loaded += RibbonLoaded;
				}));

		private static VisualActionGroupTab GetContextualGroupTab(DependencyObject obj)
		{
			return (VisualActionGroupTab)obj.GetValue(VisualActionGroupTabProperty);
		}

		private static void SetContextualGroupTab(DependencyObject obj, VisualActionGroupTab value)
		{
			obj.SetValue(VisualActionGroupTabProperty, value);
		}

		private static readonly DependencyProperty VisualActionGroupTabProperty = DependencyProperty.RegisterAttached("ContextualGroupTab", typeof(VisualActionGroupTab), typeof(ActionsToRibbonAdapter));

		private static readonly DependencyProperty IsBackstageOpenProperty =
			DependencyProperty.RegisterAttached("IsBackstageOpen", typeof(bool), typeof(ActionsToRibbonAdapter),
			new UIPropertyMetadata(
				(d, e) =>
				{
					var ribbon = d as RibbonControl;
					if (ribbon == null)
						return;
					if (e.NewValue.Equals(true))
						ClearAllLocalShortcuts(Window.GetWindow(ribbon));
					else
						RefreshLocalShortcuts(ribbon.Categories.SelectMany(p => p.Pages)
							.FirstOrDefault(tab => GetContextualGroupTab(tab) != null && tab.DataContext != null));
				}));

		private static void WindowPreviewKeyDown(object s, KeyEventArgs e)
		{
			var keyBinding = ((Window)s).InputBindings
					.OfType<KeyBinding>()
					.FirstOrDefault(p => p.Gesture != null && p.Gesture.Matches(null, e));

			if (keyBinding != null && keyBinding.Command != null && keyBinding.Command.CanExecute(keyBinding.CommandParameter))
			{
				keyBinding.Command.Execute(keyBinding.CommandParameter);
				e.Handled = true;
			}
		}

		private static void RibbonLoaded(object sender, EventArgs args)
		{
			var ribbon = (RibbonControl)sender;
			ribbon.Loaded -= RibbonLoaded;
			var wnd = Window.GetWindow(ribbon);
			if (wnd != null)
				wnd.PreviewKeyDown += WindowPreviewKeyDown;

			var backStage = ribbon.ApplicationMenu as BackstageViewControl;
			if (backStage != null)
				ribbon.SetBinding(IsBackstageOpenProperty, new Binding
				{
					Path = new PropertyPath(BackstageViewControl.IsOpenProperty),
					Source = backStage,
				});

			InitializeRibbonActions(ribbon);
		}

		private static void InitializeRibbonActions(RibbonControl ribbon)
		{
			var actionProviders = GetActionProviders(ribbon).ToList();
			if (actionProviders == null)
				return;

			foreach (var actionProvider in actionProviders)
			{
				FillContextualGroups(ribbon, actionProvider);
				FillTabs(ribbon, actionProvider);
				InitializeGlobalShortcuts(ribbon, actionProvider);
			}
		}

		private static void FillContextualGroups(RibbonControl ribbon, IVisualActionProvider actionProvider)
		{
			foreach (var contextualGroup in actionProvider.ContextualGroups)
			{
				var ribbonContextualGroup = new RibbonPageCategory
				{
					Caption = contextualGroup.Title,
					Name = contextualGroup.Name
				};

				ribbonContextualGroup.SetBinding(RibbonPageCategoryBase.IsVisibleProperty, new Binding(ACTIVE_DOCUMENT_PATH)
				{
					Mode = BindingMode.OneWay,
					Source = ribbon.DataContext,
					Converter = new TypeToBoolConverterEx(),
					ConverterParameter = contextualGroup.ContextualObjectType,
				});

				ribbon.Categories.Add(ribbonContextualGroup);

				if (String.IsNullOrWhiteSpace(contextualGroup.Name)) continue;
				if (ribbon.FindName(contextualGroup.Name) != null)
					throw new InvalidOperationException(String.Format("Specified context group is already registered. Group name [{0}]", contextualGroup.Name));
				ribbon.RegisterName(contextualGroup.Name, ribbonContextualGroup);
			}
		}

		private static void FillTabs(RibbonControl ribbon, IVisualActionProvider actionProvider)
		{
			foreach (var groupTab in actionProvider.GroupTabs)
			{
				var cat = ribbon.Categories.FirstOrDefault(p => p.IsDefault);
				var defaultPage = cat == null ? new RibbonDefaultPageCategory() : cat as RibbonDefaultPageCategory;

				var ribbonTab = new RibbonPage
				{
					Caption = groupTab.Header.ToString(),
					Name = groupTab.ContextualGroupName
				};

				ribbonTab.SetValue(RibbonPageGroup.KeyTipProperty, groupTab.KeyTip);
				ribbonTab.IsVisible = groupTab.IsVisible;

				if (groupTab.IsContextual)
				{
					var contextualGroup = actionProvider.ContextualGroups.FirstOrDefault(p => String.Equals(p.Name, groupTab.ContextualGroupName));
					if (contextualGroup == null)
						throw new InvalidOperationException(String.Format("Contextual group '{0}' not found)", groupTab.ContextualGroupName));

					var category = ribbon.Categories.FirstOrDefault(p => p.Name == groupTab.ContextualGroupName);

					ribbonTab.SetBinding(FrameworkContentElement.DataContextProperty, new Binding(ACTIVE_DOCUMENT_PATH)
					{
						Source = ribbon.DataContext,
						Converter = new TypeIsMatchConverter(),
						ConverterParameter = contextualGroup.ContextualObjectType
					});

					ribbonTab.SetBinding(RibbonHelper.IsSelectedProperty, new Binding
					{
						Converter = new TypeToBoolConverterEx(),
						ConverterParameter = contextualGroup.ContextualObjectType
					});

					SetContextualGroupTab(ribbonTab, groupTab);
					ribbonTab.DataContextChanged += (s, e) => RefreshLocalShortcuts(s as RibbonPage);

					if (category != null)
						category.Pages.Add(ribbonTab);
				}

				FillTabGroups(ribbonTab, groupTab.Groups);

				if (cat == null)
					ribbon.Categories.Add(defaultPage);
				if (ribbonTab.Groups.Count > 0 && defaultPage != null && !groupTab.IsContextual)
					defaultPage.Pages.Add(ribbonTab);
			}
		}

		private static void ClearAllLocalShortcuts(Window wnd)
		{
			if (wnd != null)
				wnd.InputBindings.RemoveRange(
					from binding in wnd.InputBindings.OfType<KeyBinding>()
					where GetContextualGroupTab(binding) != null
					select binding);
		}

		private static void RefreshLocalShortcuts(RibbonPage ribbonTab)
		{
			if (ribbonTab == null)
				return;

			var wnd = Window.GetWindow(ribbonTab);
			if (wnd == null)
				return;

			var groupTab = GetContextualGroupTab(ribbonTab);
			if (groupTab == null)
				return;

			wnd.InputBindings.RemoveRange(
				from binding in wnd.InputBindings.OfType<KeyBinding>()
				where ReferenceEquals(GetContextualGroupTab(binding), groupTab)
				select binding);

			if (ribbonTab.DataContext != null)
				InitializeLocalShortcuts(wnd, ribbonTab, groupTab.Groups.SelectMany(p => p.Actions));
		}

		private static void InitializeLocalShortcuts(Window window, RibbonPage ribbonTab, IEnumerable<VisualAction> actions)
		{
			foreach (var action in actions)
			{
				if (action.Shortcut != null)
				{
					var keyBinding = new KeyBinding { Gesture = action.Shortcut };
					SetContextualGroupTab(keyBinding, GetContextualGroupTab(ribbonTab));
					BindingOperations.SetBinding(keyBinding, InputBinding.CommandProperty,
						new Binding(action.CommandName)
						{
							Source = ribbonTab.DataContext
						});
					window.InputBindings.Add(keyBinding);
				}

				InitializeLocalShortcuts(window, ribbonTab, action.ChildActions);
			}
		}

		private static void InitializeGlobalShortcuts(RibbonControl ribbon, IVisualActionProvider actionProvider)
		{
			var wnd = Window.GetWindow(ribbon);
			if (wnd == null) return;
			foreach (var action in actionProvider.ActionsWithGlobalShortcuts)
			{
				if (String.IsNullOrEmpty(action.CommandName))
					continue;

				var keyBinding = new KeyBinding { Gesture = action.GlobalShortcut };
				BindingOperations.SetBinding(keyBinding, InputBinding.CommandProperty,
					new Binding(String.Format("{0}.{1}", DOCUMENT_MANAGER_PATH, action.CommandName)));
				wnd.InputBindings.Add(keyBinding);
			}
		}

		private static void FillTabGroups(RibbonPage tab, IEnumerable<VisualActionGroup> actionGroups)
		{
			foreach (var actionGroup in actionGroups)
			{
				var ribbonGroupBox = new RibbonPageGroup { Caption = actionGroup.Title };

				foreach (var action in actionGroup.Actions)
						ribbonGroupBox.ItemLinks.Add(CreateRibbonControl(action, tab));

				if (ribbonGroupBox.ItemLinks.Count > 0)
					tab.Groups.Add(ribbonGroupBox);

			}
		}



		static BarItemLink CreateRibbonControl(VisualAction action, RibbonPage ribbonTabItem)
		{
			var control = action.ChildActions.Count > 0 ? CreateDropDownControl(action) : CreateButtonControl(action);
			control.IsVisible = action.Visibility == Visibility.Visible;
			var link = control.CreateLink();

			ApplyScreenTip(link, control, action);

			control.SetBinding(FrameworkContentElement.DataContextProperty, new Binding(FrameworkElement.DataContextProperty.Name) { Source = ribbonTabItem });
			return link;
		}

		static void ApplyScreenTip(BarItemLink link, BarItem control, VisualAction action)
		{
			if (link == null) return;
			link.KeyTip = action.KeyTip;
			var screenTip = new ToolTipTitleItem { Text = action.ScreenTipText ?? action.Title };
			var shortcut = action.Shortcut ?? action.GlobalShortcut;
			if (shortcut != null)
				screenTip.Text += String.Format(" ({0})", shortcut.GetDisplayStringForCulture(CultureInfo.CurrentCulture));
			var tip = new SuperToolTip();
			tip.Items.Add(screenTip);
			tip.MaxWidth = 400;
			link.ToolTip = tip;
			control.Hint = tip;
		}

		static BarSplitButtonItem CreateDropDownControl(VisualAction action)
		{
			var control = new BarSplitButtonItem { LargeGlyph = action.Icon.ToBitmapImage() };
			if (!string.IsNullOrEmpty(action.CommandName))
				control.SetBinding(BarItem.CommandProperty, action.CommandName);
			if (action.CommandParameter != null)
				control.CommandParameter = action.CommandParameter;

			InitializeRibbonControl(control, action);
			var gallery = new Gallery
			{
				ColCount = 1,
				MinColCount = 1,
				IsGroupCaptionVisible = DefaultBoolean.False,
				AllowFilter = false,
				IsItemCaptionVisible = true,
				IsItemDescriptionVisible = true,
				ItemCheckMode = GalleryItemCheckMode.None
			};
			var galGroup = new GalleryItemGroup();
			gallery.Groups.Add(galGroup);

			foreach (var childAction in action.ChildActions)
			{
				var menuItem = new GalleryItem { Caption = childAction.TitleWrapping == TextWrapping.NoWrap ? (childAction.Title ?? string.Empty).Replace(' ', Convert.ToChar(160)) : childAction.Title };
				menuItem.SetBinding(GalleryItem.CommandProperty, childAction.CommandName);
				if (childAction.CommandParameter != null)
					menuItem.CommandParameter = childAction.CommandParameter;

				galGroup.Items.Add(menuItem);
			}
			control.PopupControl = new GalleryDropDownPopupMenu { InitialVisibleColCount = 1, InitialVisibleRowCount = 5, Gallery = gallery };

			return control;
		}

		static BarItem CreateButtonControl(VisualAction action)
		{
			var control = action.IsToggle
				? InitializeRibbonControl<BarCheckItem>(action, (c, a) => c.Glyph = a.Icon.ToBitmapImage())
				: InitializeRibbonControl<BarButtonItem>(action, (c, a) => c.LargeGlyph = a.Icon.ToBitmapImage());

			if (control is BarCheckItem)
			{
				if (action.CommandParameter == null)
					control.SetBinding(BarItem.CommandParameterProperty, new Binding(
						BarCheckItem.IsCheckedProperty.Name) { RelativeSource = RelativeSource.Self });
				if (!String.IsNullOrEmpty(action.TogglePropertyName))
					control.SetBinding(BarCheckItem.IsCheckedProperty, new Binding(action.TogglePropertyName));
			}

			if (!String.IsNullOrWhiteSpace(action.CommandName))
				control.SetBinding(BarItem.CommandProperty, action.CommandName);

			if (action.CommandParameter != null)
				control.CommandParameter = action.CommandParameter;

			return control;
		}

		private static TControl InitializeRibbonControl<TControl>(VisualAction visualAction,
			Action<TControl, VisualAction> initializer = null)
			where TControl : BarItem, new()
		{
			return InitializeRibbonControl(new TControl(), visualAction, initializer);
		}

		private static TControl InitializeRibbonControl<TControl>(TControl control, VisualAction visualAction,
			Action<TControl, VisualAction> initializer = null)
			where TControl : BarItem
		{
			#region Validate
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}

			if (visualAction == null)
			{
				throw new ArgumentNullException("visualAction");
			}
			#endregion

			control.Content = visualAction.TitleWrapping == TextWrapping.NoWrap
				? (visualAction.Title ?? string.Empty).Replace(' ', Convert.ToChar(160))
				: visualAction.Title;
			control.Glyph = visualAction.Icon.ToBitmapImage();
			control.RibbonStyle = visualAction.SizeDefinition == SizeDefinition.Large ? RibbonItemStyles.Large : RibbonItemStyles.SmallWithText;

			initializer.ExecuteIfNotNull(control, visualAction);

			return control;
		}
	}
}

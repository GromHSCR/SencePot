using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using DevExpress.Xpf.Layout.Core;
using SenceRep.GromHSCR.Common;
using SenceRep.GromHSCR.DocumentBase.Documents;

namespace SenceRep.Controls
{
	public class DockLayoutManagerEx : DockLayoutManager
	{
		public BindingBase DocumentHeaderBinding { get; set; }

		public BindingBase DocumentImageBinding { get; set; }

		public BindingBase DocumentHeaderTooltipBinding { get; set; }

		#region Dependency properties defenitions

		public IEnumerable DocumentsSource
		{
			get { return (IEnumerable)GetValue(DocumentsSourceProperty); }
			set { SetValue(DocumentsSourceProperty, value); }
		}

		public object SelectedDocument
		{
			get { return GetValue(SelectedDocumentProperty); }
			set { SetValue(SelectedDocumentProperty, value); }
		}

		private BaseLayoutItem ActiveItem
		{
			get { return (BaseLayoutItem)GetValue(ActiveItemProperty); }
		}

		public DataTemplate DocumentTemplate
		{
			get { return (DataTemplate)GetValue(DocumentTemplateProperty); }
			set { SetValue(DocumentTemplateProperty, value); }
		}

		#endregion

		public static readonly DependencyProperty SelectedDocumentProperty =
			DependencyProperty.Register("SelectedDocument", typeof(object), typeof(DockLayoutManagerEx),
			new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
				(d, e) =>
				{
					if (_isChanging)
						return;
					((DockLayoutManagerEx)d).ActivateSelectedDocumentPanel(e.NewValue);
				}));

		public static readonly DependencyProperty DocumentsSourceProperty =
			DependencyProperty.Register("DocumentsSource", typeof(IEnumerable), typeof(DockLayoutManagerEx),
			new UIPropertyMetadata(null,
			(d, e) =>
			{
				var dockManager = (DockLayoutManagerEx)d;

				if (e.OldValue != null)
				{
					var oldDocSource = e.OldValue as INotifyCollectionChanged;
					if (oldDocSource != null)
						oldDocSource.CollectionChanged -= dockManager.DocumentsSourceCollectionChanged;
				}

				var newDocSource = e.NewValue as INotifyCollectionChanged;
				if (newDocSource != null)
					newDocSource.CollectionChanged += dockManager.DocumentsSourceCollectionChanged;

				dockManager.InitializeDocumentPanels();
			}));

		private static readonly DependencyProperty ActiveItemProperty =
			DependencyProperty.Register("ActiveItem", typeof(BaseLayoutItem), typeof(DockLayoutManagerEx),
			new UIPropertyMetadata(null,
				(s, e) =>
				{
					var panel = e.NewValue as LayoutPanel;
					if (panel == null)
						return;

					using (Changing())
						s.SetValue(SelectedDocumentProperty, panel.DataContext);
				}));

		public static readonly DependencyProperty DocumentTemplateProperty =
			DependencyProperty.Register("DocumentTemplate", typeof(DataTemplate), typeof(DockLayoutManagerEx), new UIPropertyMetadata(null));

		static bool _isChanging;

		static IDisposable Changing()
		{
			return new LambdaDisposable(() => _isChanging = true, () => _isChanging = false);
		}

		IDisposable _subscriptionToken;
		Dictionary<string, IDocument> _restoredDocuments;



		protected override void OnUnloaded()
		{
			if (_subscriptionToken != null)
			{
				_subscriptionToken.Dispose();
				_subscriptionToken = null;
			}

			base.OnUnloaded();
		}

		BaseLayoutItem FindLayouItem(object dataContext)
		{
			var layoutGroups =
				Enumerable.Empty<LayoutGroup>()
				.Union(new[] { this.LayoutRoot })
				.Union(this.FloatGroups)
				.Union(this.AutoHideGroups);

			return
			(
				from g in layoutGroups
				select FindLayouItem(g, dataContext)
			)
			.FirstOrDefault(p => p != null);
		}

		BaseLayoutItem FindLayouItem(LayoutGroup layoutGroup, object dataContext)
		{
			BaseLayoutItem result = null;

			for (int i = 0; i < layoutGroup.Items.Count; i++)
			{
				var childLayoutGroup = layoutGroup[i] as LayoutGroup;
				if (childLayoutGroup != null)
					result = FindLayouItem(childLayoutGroup, dataContext);
				else
					if (ReferenceEquals(layoutGroup[i].DataContext, dataContext))
						result = layoutGroup[i];

				if (result != null)
					break;
			}

			return result;
		}

		DocumentGroup RootDocumentGroup
		{
			get
			{
				if (this.LayoutRoot == null)
					this.LayoutRoot = new LayoutGroup();

				var documentGroup = this.LayoutRoot.Items.OfType<DocumentGroup>().FirstOrDefault();
				if (documentGroup == null)
				{
					documentGroup = new DocumentGroup
					{
						ClosePageButtonShowMode = ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader,
						TabHeadersAutoFill = false,
						TabHeaderLayoutType = TabHeaderLayoutType.Scroll
					};
					this.LayoutRoot.Add(documentGroup);
				}
				return documentGroup;
			}
		}

		void DocumentsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (_isChanging)
				return;

			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					if (e.NewItems != null)
					{
						foreach (var item in e.NewItems)
							CreateDocumentPanel(item);
					}
					break;

				case NotifyCollectionChangedAction.Remove:
					if (e.OldItems != null)
					{
						foreach (var item in e.OldItems)
						{
							var layoutItem = FindLayouItem(item);
							if (layoutItem != null)
							{
								// I spent 5 hours to find this dumb solution using Reflector debug !!!
								this.Dispatcher.Invoke(new Action<BaseLayoutItem>(CloseDocumentPanel), layoutItem);
							}
						}
					}
					break;

				case NotifyCollectionChangedAction.Reset:
					throw new NotSupportedException();
			}
		}

		void CloseDocumentPanel(BaseLayoutItem layoutItem)
		{
			using (Changing())
			{
				if (this.DockController != null)
				{
					var documentsSource = this.DocumentsSource.Cast<object>();

					this.DockController.Close(layoutItem);

					if (!documentsSource.Any())
					{
						RootDocumentGroup.Clear();
						this.Visibility = Visibility.Collapsed;
					}

					this.Container.ActiveDockItem = null;

					ClearContainerForItem(layoutItem);

					this.RemoveVisualChild(layoutItem);
					this.RemoveLogicalChild(layoutItem);

					var layoutPanel = layoutItem as LayoutPanel;
					if (layoutPanel != null)
					{
						var content = layoutPanel.Content as ContentControl;
						if (content != null)
						{
							BindingOperations.ClearAllBindings(content);
							content.Content = null;
						}
					}
					layoutItem.DataContext = null;
				}
				GC.Collect();
				GC.WaitForPendingFinalizers();

				//activates first available document, if any
				if ((this.ActiveDockItem == null || ReferenceEquals(this.ActiveDockItem, layoutItem)) && this.DocumentsSource != null)
				{
					var availableDocument = this.DocumentsSource
						.OfType<IDocument>().FirstOrDefault();
					if (availableDocument != null)
					{
						var availableLayoutItem = FindLayouItem(availableDocument);
						if (availableLayoutItem != null)
						{
							this.BringToFront(availableLayoutItem);
							this.Activate(availableLayoutItem);
						}
					}
				}
			}
		}

		void CreateDocumentPanel(object dataContext)
		{
			this.Visibility = Visibility.Visible;

			var document = dataContext as IDocument;

			LayoutPanel layoutPanel = document == null ? new DocumentPanel { AllowHide = false } : new LayoutPanel { AllowHide = true };

			layoutPanel.AllowClose = true;
			layoutPanel.AllowDock = true;
			layoutPanel.AllowHide = false;
			layoutPanel.AllowContextMenu = true;
			layoutPanel.ShowCloseButton = true;

			if (document != null)
				RootDocumentGroup.Add(layoutPanel);

			ApplyDocumentDataContext(layoutPanel, dataContext);
		}

		void ApplyDocumentDataContext(LayoutPanel layoutPanel, object dataContext = null)
		{
			if (dataContext == null && !String.IsNullOrEmpty(layoutPanel.Name) && _restoredDocuments != null)
				dataContext = _restoredDocuments.ContainsKey(layoutPanel.Name) ? _restoredDocuments[layoutPanel.Name] : null;

			layoutPanel.DataContext = dataContext;
			layoutPanel.Content = new ContentControl
			{
				Content = dataContext,
				ContentTemplate = this.DocumentTemplate
			};

			if (this.DocumentHeaderBinding != null)
				BindingOperations.SetBinding(layoutPanel, BaseLayoutItem.CaptionProperty, this.DocumentHeaderBinding);

			if (this.DocumentImageBinding != null)
				BindingOperations.SetBinding(layoutPanel, BaseLayoutItem.CaptionImageProperty, this.DocumentImageBinding);

			if (this.DocumentHeaderTooltipBinding != null)
				BindingOperations.SetBinding(layoutPanel, BaseLayoutItem.ToolTipProperty, this.DocumentHeaderTooltipBinding);
		}

		void InitializeDocumentPanels()
		{
			this.LayoutRoot = new LayoutGroup();
			if (this.FloatGroups != null)
				this.FloatGroups.Clear();

			if (this.DocumentsSource == null)
				return;

			foreach (var doc in DocumentsSource)
				CreateDocumentPanel(doc);
		}

		void ActivateSelectedDocumentPanel(object selectedDocument)
		{
			if (selectedDocument != null)
			{
				var layoutItem = FindLayouItem(selectedDocument);

				if (layoutItem != null)
				{
					var index = layoutItem.Parent.Items.IndexOf(layoutItem);
					if (index >= 0)
						using (Changing())
						{
							this.ActiveDockItem = layoutItem;
							layoutItem.Parent.SelectedTabIndex = index;
							if (layoutItem.IsAutoHidden)
								this.DockController.Activate(layoutItem, true);
						}
				}
			}
		}

		void DockLayoutManagerEx_DockItemClosing(object sender, ItemCancelEventArgs e)
		{
			if (_isChanging)
				return;

			e.Cancel = true;

			var layoutGroup = e.Item as LayoutGroup;
			var layoutItems = layoutGroup != null ? layoutGroup.Items.ToList() : new List<BaseLayoutItem> { e.Item };

			foreach (var item in layoutItems)
			{
				var document = item.DataContext as IDocument;
				if (document != null)
					document.CloseCommand.Execute(null);
				else
				{
					e.Cancel = false;
					return;
				}
			}
		}

		void DockLayoutManagerExShowingMenu(object sender, ShowingMenuEventArgs e)
		{
			var menu = (e.Menu as ItemContextMenu);
			if (menu == null) return;

			e.ActionList.Clear();

			var actionCloseActiveItem = new BarButtonItem();
			BarItemLinkActionBase.SetItemLinkIndex(actionCloseActiveItem, 0);
			actionCloseActiveItem.Content = "Закрыть вкладку";
			actionCloseActiveItem.ItemClick += CloseActiveItemItemClick;

			var actionCloseAllButThisItem = new BarButtonItem();
			BarItemLinkActionBase.SetItemLinkIndex(actionCloseAllButThisItem, 0);
			actionCloseAllButThisItem.Content = "Закрыть все вкладки кроме этой";
			actionCloseAllButThisItem.ItemClick += CloseAllButThisItemClick;

			var actionCloseAllItems = new BarButtonItem();
			BarItemLinkActionBase.SetItemLinkIndex(actionCloseAllItems, 0);
			actionCloseAllItems.Content = "Закрыть все";
			actionCloseAllItems.ItemClick += CloseAllItemClick;

			// Action 2 - Insert a separator
			var actionAddSeparator = new InsertBarItemLinkAction
			{
				ItemLink = new BarItemLinkSeparator(),
				ItemLinkIndex = 3,
			};

			e.ActionList.Add(actionCloseAllItems);
			e.ActionList.Add(actionCloseAllButThisItem);
			e.ActionList.Add(actionCloseActiveItem);
			e.ActionList.Add(actionAddSeparator);
		}

		private void CloseAllItemClick(object sender, ItemClickEventArgs e)
		{
			var documents = this.DocumentsSource
						 .OfType<IDocument>().ToList();

			documents.ForEach(document =>
			{
				var currentDocuement = FindLayouItem(document);
				if (currentDocuement != null)
				{
					document.CloseCommand.Execute(null);
					this.CloseDocumentPanel(currentDocuement);
				}
			});
		}

		private void CloseActiveItemItemClick(object sender, ItemClickEventArgs e)
		{
			if (ActiveItem != null)
			{
				CloseDocumentPanel(ActiveItem);
			}
		}

		private void CloseAllButThisItemClick(object sender, ItemClickEventArgs e)
		{
			var documents = this.DocumentsSource
						.OfType<IDocument>().ToList();

			documents.ForEach(document =>
			{
				if (document != null && !document.IsActive)
				{
					var currentDocuement = FindLayouItem(document);
					if (currentDocuement != null && !currentDocuement.IsActive)
					{
						document.CloseCommand.Execute(null);
						this.CloseDocumentPanel(currentDocuement);
					}
				}
			});
		}

		public DockLayoutManagerEx()
		{
			this.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
			this.DockItemClosing += DockLayoutManagerEx_DockItemClosing;
			this.ShowingMenu += DockLayoutManagerExShowingMenu;
			this.SetBinding(ActiveItemProperty, new Binding(ActiveDockItemProperty.Name)
			{
				RelativeSource = RelativeSource.Self
			});

		}
	}
}

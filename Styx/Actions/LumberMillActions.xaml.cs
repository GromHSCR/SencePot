﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Styx.GromHSCR.ActionBase.VisualActions;

namespace Styx.Actions
{
	/// <summary>
    /// Логика взаимодействия для LumberMillActions.xaml
	/// </summary>
	[Export(typeof(IVisualActionProvider))]
	public partial class LumberMillActions
	{
        public LumberMillActions()
		{
			InitializeComponent();
		}
	}
}
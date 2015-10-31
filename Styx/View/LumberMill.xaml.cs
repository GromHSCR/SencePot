using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Styx.Documents;
using Styx.ViewModel;

namespace Styx.View
{
    /// <summary>
    /// Interaction logic for LumberMill.xaml
    /// </summary>
    public partial class LumberMill
    {
        private LumberMillDocument Document
        {
            get { return DataContext as LumberMillDocument; }
        }

        private SawMillOptionsViewModel ViewModel
        {
            get { return Document != null ? Document.ViewModel : null; }
        }

        public LumberMill()
        {
            InitializeComponent();
        }

        private void ButtonEdit_OnDefaultButtonClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel == null) return;
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            ViewModel.InputFolder = dialog.SelectedPath;
        }

        private void ButtonEdit_OnDefaultButtonClick2(object sender, RoutedEventArgs e)
        {
            if (ViewModel == null) return;
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            ViewModel.OutputFolder = dialog.SelectedPath;
        }
    }
}

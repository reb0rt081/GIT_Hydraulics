using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using ScienceAndMaths.Client.Modules.Picking.ViewModels;

using Unity;

namespace ScienceAndMaths.Client.Modules.Picking.Views
{
    /// <summary>
    /// Interaction logic for PickingView.xaml
    /// </summary>
    public partial class PickingView : UserControl
    {
        [Dependency]
        public IPickingViewModel PickingViewModel
        {
            get { return (IPickingViewModel) DataContext; }
            set
            {
                DataContext = value;
            }
        }

        public PickingView()
        {
            InitializeComponent();
        }

        private void ScienceAndMathsScannerControl_OnScanSubmitted(object sender, string e)
        {
            PickingViewModel.BarcodeEnteredCommand.Execute(e);
        }
    }
}

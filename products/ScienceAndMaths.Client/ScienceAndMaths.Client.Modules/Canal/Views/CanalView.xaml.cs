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

using ScienceAndMaths.Client.Modules.Canal.ViewModels;

using Unity;

namespace ScienceAndMaths.Client.Modules.Canal.Views
{
    /// <summary>
    /// Interaction logic for CanalView.xaml
    /// </summary>
    public partial class CanalView : UserControl
    {
        [Dependency]
        public ICanalViewModel canalViewModel
        {
            get { return (ICanalViewModel) DataContext; }
            set
            {
                DataContext = value;
            }
        }

        public CanalView()
        {
            InitializeComponent();
        }

        private void ScienceAndMathsScannerControl_OnScanSubmitted(object sender, string e)
        {
            canalViewModel.BarcodeEnteredCommand.Execute(e);
        }
    }
}

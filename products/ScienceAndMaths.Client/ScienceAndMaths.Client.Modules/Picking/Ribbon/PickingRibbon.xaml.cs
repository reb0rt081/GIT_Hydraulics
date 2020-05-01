using System;
using System.Collections.Generic;
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
using Prism.Regions;
using Unity;

namespace ScienceAndMaths.Client.Modules.Picking.Ribbon
{
    /// <summary>
    /// Interaction logic for PickingRibbon.xaml
    /// </summary>
    public partial class PickingRibbon : UserControl
    {
        public PickingRibbon()
        {
            InitializeComponent();
        }

        [Dependency]
        public PickingModule PickingModule
        {
            get { return (PickingModule) DataContext; }
            set
            {
                DataContext = value;
            }
        }
    }
}

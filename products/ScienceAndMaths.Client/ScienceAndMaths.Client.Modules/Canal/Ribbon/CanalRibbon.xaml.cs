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

namespace ScienceAndMaths.Client.Modules.Canal.Ribbon
{
    /// <summary>
    /// Interaction logic for CanalRibbon.xaml
    /// </summary>
    public partial class CanalRibbon : UserControl
    {
        public CanalRibbon()
        {
            InitializeComponent();
        }

        [Dependency]
        public CanalModule canalModule
        {
            get { return (CanalModule) DataContext; }
            set
            {
                DataContext = value;
            }
        }
    }
}

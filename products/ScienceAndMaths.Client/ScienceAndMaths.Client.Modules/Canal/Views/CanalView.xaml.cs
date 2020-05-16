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
using ScienceAndMaths.Shared.Canals;

using Unity;

namespace ScienceAndMaths.Client.Modules.Canal.Views
{
    /// <summary>
    /// Interaction logic for CanalView.xaml
    /// </summary>
    public partial class CanalView : UserControl
    {
        [Dependency]
        public ICanalViewModel CanalViewModel
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

        private void CanalCanvas_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            double previousX1 = canalCanvas.ActualWidth / 10;
            double previousY1 = canalCanvas.ActualHeight / 2;

            //  Drawing canal botton
            foreach (ICanalStretch canalStretch in CanalViewModel.Canal.CanalStretches)
            {
                Line canalLine = new Line();

                canalLine.Stroke = Brushes.Black;
                canalLine.X1 = previousX1;
                canalLine.Y1 = previousY1;

                canalLine.X2 = canalLine.X1 + canalStretch.Length;
                canalLine.Y2 = canalLine.Y1 + canalStretch.CanalSection.Slope * canalStretch.Length;

                canalLine.StrokeThickness = 1;

                canalCanvas.Children.Add(canalLine);

                previousX1 = canalLine.X2;
                previousY1 = canalLine.Y2;
            }

            previousX1 = canalCanvas.ActualWidth / 10;
            previousY1 = canalCanvas.ActualHeight / 2;

            if (CanalViewModel.CanalResult != null)
            {
                foreach (CanalPointResult pointResult in CanalViewModel.CanalResult.CanalPointResults)
                {
                    Line canalLine = new Line();

                    canalLine.Stroke = Brushes.Blue;
                    canalLine.X1 = previousX1;
                    canalLine.Y1 = previousY1;

                    canalLine.X2 = pointResult.X;
                    canalLine.Y2 = canalLine.Y1 + pointResult.WaterLevel;

                    canalLine.StrokeThickness = 1;

                    canalCanvas.Children.Add(canalLine);

                    previousX1 = canalLine.X2;
                    previousY1 = canalLine.Y2;
                }
            }
            
        }
    }
}

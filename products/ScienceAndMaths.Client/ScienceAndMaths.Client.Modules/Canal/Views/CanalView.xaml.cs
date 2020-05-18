using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            foreach (ICanalStretch canalStretch in CanalViewModel.CanalData.Canal.CanalStretches)
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

            if (CanalViewModel.CanalData?.CanalResult != null)
            {
                foreach (CanalPointResult pointResult in CanalViewModel.CanalData.CanalResult.CanalPointResults)
                {
                    Line canalLine = new Line();

                    canalLine.Stroke = Brushes.Blue;
                    canalLine.X1 = previousX1;
                    canalLine.Y1 = previousY1;

                    canalLine.X2 = canalLine.X1 + CanalViewModel.CanalData.CanalResult.CanalPointResults[1].X - CanalViewModel.CanalData.CanalResult.CanalPointResults[0].X;
                    canalLine.Y2 = canalLine.Y1 - pointResult.WaterLevel;

                    canalLine.StrokeThickness = 1;

                    canalCanvas.Children.Add(canalLine);

                    previousX1 = canalLine.X2;
                }
            }

        }
    }
}

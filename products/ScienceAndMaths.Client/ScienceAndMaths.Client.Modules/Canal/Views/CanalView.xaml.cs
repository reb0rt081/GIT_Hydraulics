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
        public double ScaleY { get; set; }

        [Dependency]
        public ICanalViewModel CanalViewModel
        {
            get { return (ICanalViewModel) DataContext; }
            set
            {
                DataContext = value;
            }
        }

        // Zoom.
        private double Zoom = 1;
        private void sliZoom_ValueChanged(object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            // Make sure the control's are all ready.
            if (!IsInitialized) return;

            // Display the zoom factor as a percentage.
            lblZoom.Content = sliZoom.Value + "%";

            // Get the scale factor as a fraction 0.25 - 2.00.
            double scale = (double)(sliZoom.Value / 100.0);

            // Scale the graph.
            canalCanvas.LayoutTransform = new ScaleTransform(scale, scale);
        }

        public CanalView()
        {
            InitializeComponent();

            ScaleY = 20;
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

                Label initialLabel = new Label();
                initialLabel.Content = canalStretch.CanalSection.Roughness;
                Canvas.SetLeft(initialLabel, previousX1);
                Canvas.SetTop(initialLabel, previousY1  + 10);

                canalLine.X2 = canalLine.X1 + canalStretch.Length;
                canalLine.Y2 = canalLine.Y1 + canalStretch.CanalSection.Slope * canalStretch.Length * ScaleY;

                canalLine.StrokeThickness = 1;

                canalCanvas.Children.Add(canalLine);
                canalCanvas.Children.Add(initialLabel);

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
                    canalLine.Y2 = canalLine.Y1 - pointResult.WaterLevel * ScaleY;

                    canalLine.StrokeThickness = 1;

                    canalCanvas.Children.Add(canalLine);

                    previousX1 = canalLine.X2;
                }
            }

        }
    }
}

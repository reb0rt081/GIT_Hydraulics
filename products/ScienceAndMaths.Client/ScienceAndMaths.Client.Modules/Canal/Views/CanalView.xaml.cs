using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using ScienceAndMaths.Client.Controls;
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

        public double ScaleX { get; set; }

        public double InitialCanalX { get; set; }

        public double InitialCanalY { get; set; }

        private readonly  List<UIElement> temporaryCanvasElements = new List<UIElement>();

        [Dependency]
        public ICanalViewModel CanalViewModel
        {
            get { return (ICanalViewModel) DataContext; }
            set
            {
                DataContext = value;
            }
        }

        private Line activeCanalLine;

        private void sliZoom_ValueChanged(object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            // Make sure the control's are all ready.
            if (!IsInitialized) return;

            // Display the zoom factor as a percentage.
            zoomLabel.Content = zoomSlider.Value + "%";

            // Get the scale factor as a fraction 0.25 - 2.00.
            double scale = (double)(zoomSlider.Value / 100.0);

            // Scale the graph.
            canalCanvas.LayoutTransform = new ScaleTransform(scale, scale);
        }

        private void CanalCanvas_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(e.Delta > 0)
            {
                double newValue = zoomSlider.Value + 25;

                if(newValue <= zoomSlider.Maximum)
                {
                    zoomSlider.Value = newValue;
                }
            }
            else if(e.Delta < 0)
            {
                double newValue = zoomSlider.Value - 25;

                if (newValue >= zoomSlider.Minimum)
                {
                    zoomSlider.Value = newValue;
                }
            }

            // Display the zoom factor as a percentage.
            zoomLabel.Content = zoomSlider.Value + "%";

            // Get the scale factor as a fraction 0.25 - 2.00.
            double scale = (double)(zoomSlider.Value / 100.0);

            // Scale the graph.
            canalCanvas.LayoutTransform = new ScaleTransform(scale, scale);
        }

        public CanalView()
        {
            InitializeComponent();

            ScaleY = 20;
        }

        private void CanalCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(canalCanvas);

            DisplayCanalResult(position);
        }

        private void CanalCanvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(canalCanvas);

            DisplayCanalResult(position);
        }

        private void CanalCanvas_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DrawCanal();
        }

        private void DisplayCanalResult(Point position)
        {
            if (CanalViewModel.CanalData != null)
            {
                CanalPointResult canalPointResult = CanalViewModel.CanalData.GetCanalPointResult((position.X - InitialCanalX) / ScaleX);

                if (canalPointResult != null)
                {
                    RemoveTemporaryCanvasElements();

                    //  Add cross
                    activeCanalLine = GetClosestCanalLine(position.X);
                    activeCanalLine.Stroke = Brushes.Red;

                    Label displayResultLabel = new Label();
                    displayResultLabel.Content = "X= " + canalPointResult.X + Environment.NewLine + "Y= " + canalPointResult.WaterLevel + " m";
                    Canvas.SetLeft(displayResultLabel, activeCanalLine.X1);
                    Canvas.SetTop(displayResultLabel, activeCanalLine.Y2 - 50);

                    temporaryCanvasElements.Add(displayResultLabel);

                    canalCanvas.Children.Add(displayResultLabel);
                }
            }
        }

        private void DrawCanal()
        {
            canalCanvas.Children.Clear();

            ResetCanalPointer(out double previousX1, out double previousY1);

            InitialCanalX = previousX1;
            InitialCanalY = previousY1;

            double finalX1 = canalCanvas.ActualWidth * 9 / 10;

            ScaleX = (finalX1 - previousX1) / CanalViewModel.CanalData.Canal.CanalStretches.Sum(cs => cs.Length);

            //  Drawing canal botton
            foreach (ICanalStretchModel canalStretch in CanalViewModel.CanalData.Canal.CanalStretches)
            {
                Line canalLine = new Line();

                canalLine.Stroke = Brushes.Black;
                canalLine.X1 = previousX1;
                canalLine.Y1 = previousY1;

                Label canalIdlabel = new Label();
                canalIdlabel.Content = CanalViewModel.CanalData.Canal.Id;
                Canvas.SetLeft(canalIdlabel, previousX1);
                Canvas.SetTop(canalIdlabel, previousY1 + 10);

                canalLine.X2 = canalLine.X1 + canalStretch.Length * ScaleX;
                canalLine.Y2 = canalLine.Y1 + canalStretch.CanalSection.Slope * canalStretch.Length * ScaleY;

                canalLine.StrokeThickness = ScaleX;

                canalCanvas.Children.Add(canalLine);
                canalCanvas.Children.Add(canalIdlabel);

                previousX1 = canalLine.X2;
                previousY1 = canalLine.Y2;
            }

            previousX1 = InitialCanalX;
            previousY1 = InitialCanalY;

            //  Drawing canal solution
            if (CanalViewModel.CanalData?.CanalResult != null)
            {
                var firstPoint = CanalViewModel.CanalData.CanalResult.CanalPointResults.FirstOrDefault();

                double increaseX = CanalViewModel.CanalData.CanalResult.CanalPointResults[1].X - CanalViewModel.CanalData.CanalResult.CanalPointResults[0].X;

                if (firstPoint != null)
                {
                    Label firstPointLabel = new Label();
                    firstPointLabel.Content = firstPoint.WaterLevel + " m";
                    Canvas.SetLeft(firstPointLabel, previousX1);
                    Canvas.SetTop(firstPointLabel, previousY1 - firstPoint.WaterLevel * ScaleY - 50);

                    canalCanvas.Children.Add(firstPointLabel);
                }

                foreach (CanalPointResult pointResult in CanalViewModel.CanalData.CanalResult.CanalPointResults)
                {
                    Line canalLine = new Line();
                    ICanalSection canalSection = CanalViewModel.CanalData.GetCanalSection(pointResult);
                    double actualIncreaseX = increaseX * Math.Cos(Math.Atan(canalSection.Slope));

                    canalLine.Stroke = Brushes.Blue;
                    canalLine.X1 = previousX1;
                    canalLine.Y1 = previousY1;

                    canalLine.X2 = canalLine.X1 + actualIncreaseX * ScaleX;
                    canalLine.Y2 = canalLine.Y1 - pointResult.WaterLevel * ScaleY;

                    canalLine.StrokeThickness = ScaleX;

                    canalCanvas.Children.Add(canalLine);

                    previousX1 = canalLine.X2;
                    previousY1 += actualIncreaseX * canalSection.Slope * ScaleY;
                }

                var lastPoint = CanalViewModel.CanalData.CanalResult.CanalPointResults.LastOrDefault();

                if (lastPoint != null)
                {
                    Label lastPointLabel = new Label();
                    lastPointLabel.Content = lastPoint.WaterLevel + " m";
                    Canvas.SetLeft(lastPointLabel, previousX1);
                    Canvas.SetTop(lastPointLabel, previousY1 - lastPoint.WaterLevel * ScaleY - 50);

                    canalCanvas.Children.Add(lastPointLabel);
                }
            }
        }

        private void ResetCanalPointer(out double initialX1, out double initialY1)
        {
            initialX1 = canalCanvas.ActualWidth / 10;
            initialY1 = canalCanvas.ActualHeight / 2;
        }

        private void RemoveTemporaryCanvasElements()
        {
            foreach (var uiElement in temporaryCanvasElements)
            {
                canalCanvas.Children.Remove(uiElement);
            }

            temporaryCanvasElements.Clear();

            if(activeCanalLine != null)
            {
                activeCanalLine.Stroke = Brushes.Blue;
            }
        }

        private Line GetClosestCanalLine(double x)
        {
            if(canalCanvas?.Children.Count > 0)
            {
                return canalCanvas.Children.OfType<Line>().OrderBy(ln => Math.Abs(ln.X1 - x)).FirstOrDefault();
            }

            return null;
        }
    }
}

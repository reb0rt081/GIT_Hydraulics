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

namespace ScienceAndMaths.Client.Controls
{
    /// <summary>
    /// Interaction logic for ScannerControl.xaml
    /// </summary>
    public partial class ScienceAndMathsScannerControl : UserControl
    {
        private Window window;
        public ScienceAndMathsScannerControl()
        {
            InitializeComponent();
        }

        // Using a DependencyProperty as the backing store for ClearTextOnSubmit.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ClearTextOnSubmitProperty = DependencyProperty.Register("ClearTextOnSubmit",
        //    typeof(bool),
        //    typeof(ScienceAndMathsScannerControl),
        //    new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public string LastSuccessfulScan { get; set; }

        public bool ClearTextOnSubmit { get; set; }

        /// <summary>
        ///     Submitted event
        /// </summary>
        public event EventHandler<string> ScanSubmitted;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            window = Window.GetWindow(barcodeScannedTextBox);

            if(window != null)
            {
                window.PreviewKeyDown += ParentWindowPreviewKeyDown;
                IsVisibleChanged += OnIsVisibleChanged;
            }
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                window.PreviewKeyDown += ParentWindowPreviewKeyDown;
            }
            else
            {
                window.PreviewKeyDown -= ParentWindowPreviewKeyDown;
            }
        }

        private void ParentWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            Keyboard.Focus(barcodeScannedTextBox);
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                ScanSubmitted?.Invoke(this, barcodeScannedTextBox.Text);

                LastSuccessfulScan = barcodeScannedTextBox.Text;

                if(ClearTextOnSubmit)
                {
                    barcodeScannedTextBox.Clear();
                }
                
            }
        }
    }
}

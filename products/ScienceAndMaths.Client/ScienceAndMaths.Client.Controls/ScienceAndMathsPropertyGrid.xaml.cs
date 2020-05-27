using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

using ScienceAndMaths.Client.Shared;

namespace ScienceAndMaths.Client.Controls
{
    /// <summary>
    /// Interaction logic for ScienceAandMathsPropertyGrid.xaml
    /// </summary>
    public partial class ScienceAndMathsPropertyGrid : UserControl
    {
        public ScienceAndMathsPropertyGrid()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
        }

        public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register(
            nameof(Properties),
            typeof(ObservableCollection<PropertyItem>),
            typeof(ScienceAndMathsPropertyGrid)
        );

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ObservableCollection<PropertyItem> oldPropertyList = Properties;
            Properties = new ObservableCollection<PropertyItem>();

            if (DataContext != null)
            {
                Type objectType = DataContext.GetType();

                var properties = objectType.GetProperties().ToList();

                foreach (var property in properties)
                {
                    Properties.Add(new PropertyItem { Name = property.Name, Value = property.GetValue(DataContext).ToString()});
                }
            }

            collectionProperties.ItemsSource = Properties;

            OnPropertyChanged(new DependencyPropertyChangedEventArgs(PropertiesProperty, oldPropertyList, Properties));
        }

        public ObservableCollection<PropertyItem> Properties {
            get { return (ObservableCollection<PropertyItem>)GetValue(PropertiesProperty); }
            set { SetValue(PropertiesProperty, value);  }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //foreach (KeyValuePair<string, object> property in Properties)
            //{
            //    // Here we should create and add the stuff
            //    Grid propertyGrid = new Grid();
            //    propertyGrid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength()});
            //    propertyPanel.Children.Add()
            //}
        }

        
    }
}

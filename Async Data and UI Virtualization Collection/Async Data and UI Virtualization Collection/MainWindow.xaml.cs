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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Async_Data_and_UI_Virtualization_Collection {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            DataContext = new AsyncExampleViewModel();
        }

        private void ProgressIndicator_Loaded(object sender, RoutedEventArgs e) {
            Border border = sender as Border;
            double width = border.ActualWidth;
            border.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            border.Width = 50;

            TranslateTransform trans = new TranslateTransform();

            border.RenderTransform = trans;

            var expand = new DoubleAnimation(-25, width + 25, TimeSpan.FromMilliseconds(2500));
            expand.RepeatBehavior = RepeatBehavior.Forever;

            trans.BeginAnimation(TranslateTransform.XProperty, expand);
        }
    }
}

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

namespace AnimatingPanel {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            ViewModel viewModel = new ViewModel();

            // create list of items
            for (int i = 0 ; i < 50 ; i++) {
                viewModel.Items.Add(new Item(i.ToString()));
            }


            // create stack
            viewModel.Stack.Add(new Item("Slika1"));
            viewModel.Stack.Add(new Item("Slika2"));
            viewModel.Stack.Add(new Item("Slika3"));
            viewModel.Stack.Add(new Item("Slika4"));

            DataContext = viewModel;
        }
    }

    public class ViewModel {
        public List<Item> Items { get; set; }
        public List<Item> Stack{ get; set; }

        public ViewModel() {
            Items = new List<Item>();
            Stack = new List<Item>();
        }
    }

    public class Item {
        public string Label { get; set; }

        public Item(string label) {
            Label = label;
        }
    }
}

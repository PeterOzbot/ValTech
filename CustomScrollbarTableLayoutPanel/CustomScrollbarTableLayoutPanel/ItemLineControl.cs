using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace CustomScrollbarTableLayoutPanel
{
    public partial class ItemLineControl : UserControl
    {
        private Item _item;

        public ItemLineControl()
        {
            InitializeComponent();
        }

        public ItemLineControl(Item item) : this()
        {
            _item = item;
            Margin = new Padding(0);
            SetUpBindings();
        }

        private void SetUpBindings()
        {
            QuantityLabel.DataBindings.Add("Text", _item, "Quantity", true, DataSourceUpdateMode.OnPropertyChanged, 0, "N0");
            NameLabel.DataBindings.Add("Text", _item, "Name", false, DataSourceUpdateMode.OnPropertyChanged, String.Empty);
            AmountLabel.DataBindings.Add("Text", _item, "Amount", true, DataSourceUpdateMode.OnPropertyChanged, 0, "C");
        }
    }
}

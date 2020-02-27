using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableLayoutPanel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();






            //// ItemTableLayout.AutoSize = true;
            // ItemTableLayout.ColumnCount = 1;
            // ItemTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // ItemTableLayout.RowCount = ItemTableLayout.RowCount + 1;
            // ItemTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            // ItemTableLayout.Controls.Add(new Label() { Text = "Street, City, State" }, 1, ItemTableLayout.RowCount - 1);

            // ItemTableLayout.RowCount = ItemTableLayout.RowCount + 1;
            // ItemTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            // ItemTableLayout.Controls.Add(new Label() { Text = "Street, City, State" }, 1, ItemTableLayout.RowCount - 1);

            // ItemTableLayout.RowCount = ItemTableLayout.RowCount + 1;
            // ItemTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            // ItemTableLayout.Controls.Add(new Label() { Text = "Street, City, State" }, 1, ItemTableLayout.RowCount - 1);

            // ItemTableLayout.RowCount = ItemTableLayout.RowCount + 1;
            // ItemTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            // ItemTableLayout.Controls.Add(new Label() { Text = "Street, City, State" }, 1, ItemTableLayout.RowCount - 1);
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //for (int i = 0; i < 10; i++)
            //{
            //    AddLine();
            //}
        }

        public void AddLine()
        {

            ItemListControl.AddLine(new CustomScrollbarTableLayoutPanel.Item
            {
                Amount = 100.45m,
                Name = "Monster energy drink",
                Quantity = 2
            });
            ItemListControl.AddLine(new CustomScrollbarTableLayoutPanel.Item
            {
                Amount = 4.5m,
                Name = "Radenska kraljevi vrelec",
                Quantity = 5
            });
            ItemListControl.AddLine(new CustomScrollbarTableLayoutPanel.Item
            {
                Amount = 10000.45m,
                Name = "Radenska kraljevi vrelec Radenska kraljevi vrelec",
                Quantity = 10000
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddLine();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {

                while (true)
                {
                    Task.Delay(50).Wait();

                    int move = ItemListControl.Location.Y > (this.Height-400) ? -20 : 20;

                    this.Invoke(new Action(() =>
                    {
                        ItemListControl.Location = new Point(ItemListControl.Location.X, ItemListControl.Location.Y + move);
                    }));
                }

            });
        }
    }
}

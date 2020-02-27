using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomScrollbarTableLayoutPanel
{
    public partial class ItemListControl : UserControl
    {
        private const int RowSize = 35;
        public ItemListControl()
        {
            InitializeComponent();

            ItemTableLayout.RowCount = 0;
            ItemTableLayout.RowStyles.Clear();

            ItemTableLayout.ColumnCount = 1;
            ItemTableLayout.ColumnStyles.Clear();
            ItemTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            ItemTableLayout.AutoScroll = true;
            ItemTableLayout.MouseWheel += ItemTableLayout_MouseWheel;

            SetUpScrollBar();
        }

        private void SetUpScrollBar()
        {
            ScrollbarPanel.VisibleSize = ItemTableLayout.Height;
            ScrollbarPanel.ValueChanged += ScrollbarPanel_ValueChanged;
        }

        private void ScrollbarPanel_ValueChanged(object sender, EventArgs e)
        {
            ItemTableLayout.AutoScrollPosition = new Point(0, ScrollbarPanel.Value);
        }

        private void ItemTableLayout_MouseWheel(object sender, MouseEventArgs e)
        {
            ScrollbarPanel.Value = Math.Abs(ItemTableLayout.AutoScrollPosition.Y);
        }

        public void AddLine(Item item)
        {   
            ItemLineControl itemLineControl = new ItemLineControl(item);

            ItemTableLayout.RowCount = ItemTableLayout.RowCount + 1;
            ItemTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, RowSize));
            ItemTableLayout.Controls.Add(itemLineControl, 0, ItemTableLayout.RowCount - 1);

            ScrollbarPanel.TotalSize = RowSize * ItemTableLayout.RowCount;
        }
    }
}

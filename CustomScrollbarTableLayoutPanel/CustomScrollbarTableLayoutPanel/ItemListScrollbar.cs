using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomScrollbarTableLayoutPanel
{
    public partial class ItemListScrollbar : UserControl
    {
        public ItemListScrollbar()
        {
            InitializeComponent();

            this.MouseDown += ItemListScrollbar_MouseDown;
            this.MouseMove += ItemListScrollbar_MouseMove;
            this.MouseUp += ItemListScrollbar_MouseUp;
        }

        private ItemListScrollbarHandle ScrollbarHandle { get; set; }
        private ItemListScrollbarHandle BackgroundScrollbarHandle { get; set; }

        private int _visibleSize;
        public int VisibleSize
        {
            get { return _visibleSize; }
            set
            {
                _visibleSize = value;
                UpdateHandle();
                Invalidate();
            }
        }

        private int _totalSize;
        public int TotalSize
        {
            get { return _totalSize; }
            set
            {
                _totalSize = value;
                UpdateHandle();
                Invalidate();
            }
        }

        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;

                int thumbHeight = CalculateThumbHeight();
                int pixelRange = this.Height - thumbHeight;

                if (_value != 0 && TotalSize > VisibleSize)
                {
                    float proportion = (float)_value / (float)(TotalSize - VisibleSize);
                    _thumbTop = (int)(proportion * (float)pixelRange);
                }
                else
                {
                    _thumbTop = 0;
                }


                Invalidate();
            }
        }

        public event EventHandler ValueChanged = null;
        private void OnValueChanged()
        {
            ValueChanged?.Invoke(this, new EventArgs());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // set the backgorund
            BackColor = Color.White;

            // move the handle to position
            ScrollbarHandle.Location = new Point(0, _thumbTop);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ScrollbarHandle = new ItemListScrollbarHandle();
            ScrollbarHandle.HandleColor = Color.Gray;
            this.Controls.Add(ScrollbarHandle);

            BackgroundScrollbarHandle = new ItemListScrollbarHandle();
            BackgroundScrollbarHandle.HandleHeight = BackgroundScrollbarHandle.Height = this.Height - 10;
            BackgroundScrollbarHandle.Location = new Point(0, 5);
            BackgroundScrollbarHandle.HandleColor = Color.LightGray;
            this.Controls.Add(BackgroundScrollbarHandle);

            UpdateHandle();
        }

        private void UpdateHandle()
        {
            if (ScrollbarHandle != null)
            {
                ScrollbarHandle.HandleHeight = CalculateThumbHeight();
                ScrollbarHandle.Invalidate();
            }
        }

        private int CalculateThumbHeight()
        {
            int thumbSize;
            if (TotalSize != 0)
            {
                float thumbSizePortion = (float)VisibleSize / (float)TotalSize;
                thumbSize = (int)(this.Height * thumbSizePortion);
            }
            else
            {
                thumbSize = this.Height;
            }

            return thumbSize;
        }


        private bool _thumbDown;
        private int _clickPoint;
        private int _thumbTop;

        private void ItemListScrollbar_MouseUp(object sender, MouseEventArgs e)
        {
            _thumbDown = false;
        }

        private void ItemListScrollbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_thumbDown == true)
            {
                MoveThumb(e.Y);
            }
        }

        private void MoveThumb(int y)
        {
            int thumbHeight = CalculateThumbHeight();
            int pixelRange = this.Height - thumbHeight;

            if (pixelRange > 0)
            {
                // get new value for handle top
                int newThumbTop = y - _clickPoint;

                // check if goes to negative  or out of range
                if (newThumbTop < 0)
                {
                    _thumbTop = 0;
                }
                else if (newThumbTop > pixelRange)
                {
                    _thumbTop = pixelRange;
                }
                else
                {
                    _thumbTop = newThumbTop;
                }

                // calculate new value
                float proportion = (float)_thumbTop / (float)pixelRange;
                _value = (int)(proportion * (TotalSize - VisibleSize));

                OnValueChanged();
                Invalidate();
            }
        }

        private void ItemListScrollbar_MouseDown(object sender, MouseEventArgs e)
        {
            Point ptPoint = this.PointToClient(Cursor.Position);
            int thumbHeight = CalculateThumbHeight();

            Rectangle thumbrect = new Rectangle(new Point(1, _thumbTop), new Size(20, thumbHeight));
            if (thumbrect.Contains(ptPoint))
            {
                // hit the thumb
                _clickPoint = (ptPoint.Y - _thumbTop);
                _thumbDown = true;
            }
        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomScrollbarTableLayoutPanel
{
    public partial class ItemListScrollbarHandle : Label
    {
        public ItemListScrollbarHandle()
        {
            InitializeComponent();
        }

        private int _handleHeight;
        public int HandleHeight
        {
            get { return _handleHeight; }
            set
            {
                _handleHeight = value;
                HandleSizes = new HandleSizes { HandleHeight = _handleHeight };
                Invalidate();
            }
        }

        public Color HandleColor { get; set; }

        public HandleSizes HandleSizes { get; private set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            this.BackColor = Color.Transparent;
            this.Height = HandleHeight;

            PaintHandle(e, HandleColor, HandleSizes);
        }

        /// <summary>
        /// This is used so the mouse events go throught handle to the scrollbar control
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = (-1);

            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public void PaintHandle(PaintEventArgs e, Color thumbColor, HandleSizes handleSizes)
        {
            //Set Graphics smoothing mode to Anit-Alias-- 
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw handle
            using (GraphicsPath graphPath = GetHandlePath(handleSizes))
            {
                //this.Region = new Region(graphPath);
                using (Pen pen = new Pen(thumbColor, 0.25f))
                {
                    pen.Alignment = PenAlignment.Inset;

                    e.Graphics.DrawPath(pen, graphPath);

                    // Fill path with color
                    using (Brush brush = new SolidBrush(thumbColor))
                    {
                        e.Graphics.FillPath(brush, graphPath);
                    }
                }
            }
        }

        private GraphicsPath GetHandlePath(HandleSizes handleSizes)
        {
            GraphicsPath path = new GraphicsPath();

            // create path
            path.AddArc(handleSizes.LeftMargin, handleSizes.TopMargin, handleSizes.HandleWidth, handleSizes.DomeHeight, 180, 180); // Top
            path.AddArc(handleSizes.RightMargin, handleSizes.MiddleHeight + handleSizes.DomeHeight - 1 - handleSizes.TopMargin, handleSizes.HandleWidth, handleSizes.DomeHeight, 360, 180); //Bottom 
            path.CloseAllFigures();

            return path;
        }
    }


    public class HandleSizes
    {
        public int HandleHeight { get; set; }
        public int LeftMargin { get; } = 8;
        public int RightMargin { get; } = 8;
        public int TopMargin { get; } = 0;
        public int BottomMargin { get; } = 5;
        public int DomeHeight { get; } = 5;
        public int MiddleHeight
        {
            get
            {
                return HandleHeight - (2 * DomeHeight);
            }
        }
        public int HandleWidth { get; } = 4;
    }
}

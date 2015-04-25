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

namespace FacebookBusyIndicator.WPF {
    /// <summary>
    /// Interaction logic for BusyIndicator.xaml
    /// </summary>
    public partial class BusyIndicator : UserControl {
        public BusyIndicator() {
            InitializeComponent();
        }

        /// <summary>
        /// Number of columns in indicator dependency property.
        /// </summary>
        public static readonly DependencyProperty ColumnNumberProperty =
                               DependencyProperty.Register("ColumnNumber", typeof(int), typeof(BusyIndicator));
        /// <summary>
        /// Number of columns in indicator.
        /// </summary>
        public int ColumnNumber {
            get { return (int) GetValue(ColumnNumberProperty); }
            set { SetValue(ColumnNumberProperty, value); }
        }

        /// <summary>
        /// TODO:
        /// </summary>
        public static readonly DependencyProperty ColumnFillProperty =
                               DependencyProperty.Register("ColumnFill", typeof(Brush), typeof(BusyIndicator));
        /// <summary>
        /// TODO:
        /// </summary>
        public Brush ColumnFill {
            get { return (Brush) GetValue(ColumnFillProperty); }
            set { SetValue(ColumnFillProperty, value); }
        }

        /// <summary>
        /// TODO:
        /// </summary>
        public static readonly DependencyProperty ColumnBorderProperty =
                               DependencyProperty.Register("ColumnBorder", typeof(Brush), typeof(BusyIndicator));
        /// <summary>
        /// TODO:
        /// </summary>
        public Brush ColumnBorder {
            get { return (Brush) GetValue(ColumnBorderProperty); }
            set { SetValue(ColumnBorderProperty, value); }
        }

        /// <summary>
        /// TODO:
        /// </summary>
        public static readonly DependencyProperty IsBusyProperty =
                               DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyIndicator), new PropertyMetadata() { PropertyChangedCallback = IsBusyChanged });
        /// <summary>
        /// TODO:
        /// </summary>
        public bool IsBusy {
            get { return (bool) GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }


        /// <summary>
        /// When IsBusy property changes stop/start animation.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void IsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {

            // get the busy indicator control as framework element
            FrameworkElement frameworkElement = d as FrameworkElement;

            // get the storyboard from control
            Storyboard storyboard = frameworkElement != null ? frameworkElement.Resources["storyboard"] as Storyboard : null;

            // if no story board then do nothing
            if (storyboard == null) { return; }

            // get the is busy value
            bool isBusy = Convert.ToBoolean(e.NewValue);

            // start if busy if not stop
            if (isBusy) {
                storyboard.Begin();
            }
            else {
                storyboard.Stop();
            }
        }

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);

            double columnWidth = this.Width / ColumnNumber;

            for (int i = 0 ; i < ColumnNumber ; i++) {
                Container.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(columnWidth) });
            }

            Storyboard storyboard = new Storyboard();
            storyboard.RepeatBehavior = RepeatBehavior.Forever;
            this.Resources.Add("storyboard", storyboard);

            int time = 150;

            for (int i = 0 ; i < ColumnNumber ; i++) {

                Border border = new Border();
                border.Name = "Indicator" + i;
                border.Margin = new Thickness(2, 0, 2, 0);
                border.SetValue(Grid.ColumnProperty, i);
                border.Background = ColumnFill;
                border.Opacity = 0.1;
                border.BorderBrush = ColumnBorder;
                border.BorderThickness = new Thickness(1);
                border.RenderTransform = new ScaleTransform() { CenterX = 0, CenterY = Height / 2, ScaleX = 1, ScaleY = 1 };
                border.SnapsToDevicePixels = true;

                this.RegisterName("Indicator" + i, border);

                DoubleAnimation anim = new DoubleAnimation(1, 2.5, TimeSpan.FromMilliseconds(time));
                anim.BeginTime = TimeSpan.FromMilliseconds(time * i);
                storyboard.Children.Add(anim);
                Storyboard.SetTargetProperty(anim, new PropertyPath("RenderTransform.ScaleY"));
                Storyboard.SetTarget(anim, border);

                DoubleAnimation anim2 = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(time));
                anim2.BeginTime = TimeSpan.FromMilliseconds(time * i);
                anim2.SetValue(Storyboard.TargetNameProperty, "Indicator" + i);
                anim2.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(BusyIndicator.OpacityProperty));
                storyboard.Children.Add(anim2);

                DoubleAnimation anim3 = new DoubleAnimation(1, 0.1, TimeSpan.FromMilliseconds(time * 3));
                anim3.BeginTime = TimeSpan.FromMilliseconds((time * i) + (time * 2) - (i * (time / ColumnNumber)));
                anim3.SetValue(Storyboard.TargetNameProperty, "Indicator" + i);
                anim3.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(BusyIndicator.OpacityProperty));
                storyboard.Children.Add(anim3);

                DoubleAnimation anim4 = new DoubleAnimation(2.5, 1, TimeSpan.FromMilliseconds(time * 3));
                anim4.BeginTime = TimeSpan.FromMilliseconds((time * i) + (time * 2) - (i * (time / ColumnNumber)));
                storyboard.Children.Add(anim4);
                Storyboard.SetTargetProperty(anim4, new PropertyPath("RenderTransform.ScaleY"));
                Storyboard.SetTarget(anim4, border);

                Container.Children.Add(border);
            }
        }
    }
}

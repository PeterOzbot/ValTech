using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AnimatingPanel {
    /// <summary>
    /// Panel to animate items in "stack" on mouse over
    /// </summary>
    public class AnimatingDisplayPanel : System.Windows.Controls.Panel {
        public AnimatingDisplayPanel() {
            this.Background = Brushes.Transparent;            // Make sure we get mouse events
            this.Background = Brushes.Red;                    // Good for debugging
            this.MouseEnter += new MouseEventHandler(FanPanel_MouseEnter);
            this.MouseLeave += new MouseEventHandler(FanPanel_MouseLeave);
        }

        private Size ourSize;
        private bool foundNewChildren = false;
        private double scaleFactor = 1;

        public int AnimationMilliseconds {
            get { return (int) GetValue(AnimationMillisecondsProperty); }
            set { SetValue(AnimationMillisecondsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationMilliseconds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationMillisecondsProperty =
            DependencyProperty.Register("AnimationMilliseconds", typeof(int), typeof(AnimatingDisplayPanel), new UIPropertyMetadata(1250));


        void FanPanel_MouseEnter(object sender, MouseEventArgs e) {
            this.InvalidateArrange();
        }

        void FanPanel_MouseLeave(object sender, MouseEventArgs e) {
            this.InvalidateArrange();
        }

        protected override Size MeasureOverride(Size availableSize) {
            // Allow children as much room as they want - then scale them
            Size size = new Size(Double.PositiveInfinity, Double.PositiveInfinity);
            Size desiredSize = new Size(0, 0);

            // 4 in row
            foreach (UIElement child in Children) {
                child.Measure(size);
                if (desiredSize.Width == 0) desiredSize.Width = 4 * child.DesiredSize.Width;
                desiredSize.Height += child.DesiredSize.Height;
            }
            desiredSize.Height = desiredSize.Height / 4;

            // EID calls us with infinity, but framework doesn't like us to return infinity
            if (double.IsInfinity(availableSize.Height) || double.IsInfinity(availableSize.Width)) {
                if (!double.IsInfinity(availableSize.Width))
                    return desiredSize;
                else
                    return new Size(100, 100);
            }
            else
                return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize) {
            //RaiseEvent(new RoutedEventArgs(AnimatingPanelControl.RefreshEvent, null));
            if (this.Children == null || this.Children.Count == 0)
                return finalSize;

            ourSize = finalSize;
            foundNewChildren = false;

            foreach (UIElement child in this.Children) {
                // If this is the first time we've seen this child, add our transforms
                if (child.RenderTransform as TransformGroup == null) {
                    foundNewChildren = true;
                    child.RenderTransformOrigin = new Point(-10, -10);
                    TransformGroup group = new TransformGroup();
                    child.RenderTransform = group;
                    group.Children.Add(new ScaleTransform());
                    group.Children.Add(new TranslateTransform());
                    group.Children.Add(new RotateTransform());
                }

                // Don't allow our children any clicks in icon form
                child.IsHitTestVisible = false;
                child.Arrange(new Rect(0, 0, child.DesiredSize.Width, child.DesiredSize.Height));

                // Scale the children so they fit in our size
                double sf = (Math.Min(ourSize.Width, ourSize.Height) * 0.4) / Math.Max(child.DesiredSize.Width, child.DesiredSize.Height);
                scaleFactor = Math.Min(scaleFactor, sf);
            }

            AnimateAll();

            return finalSize;
        }

        private void AnimateAll() {
            if (!this.IsMouseOver) {
                // Rotate all the children into a stack
                double r = 0;
                int sign = +1;
                foreach (UIElement child in this.Children) {
                    if (foundNewChildren)
                        child.SetValue(Panel.ZIndexProperty, 0);

                    AnimateTo(child, r, 0, 0, scaleFactor);
                    r += sign * 15;         // +-15 degree intervals
                    if (Math.Abs(r) > 90) {
                        r = 0;
                        sign = -sign;
                    }
                }
            }
            else {
                // On mouse over explode out the children and don't rotate them
                Random rand = new Random();
                foreach (UIElement child in this.Children) {
                    child.SetValue(Panel.ZIndexProperty, rand.Next(this.Children.Count));
                    double x = (rand.Next(32) - 8) * ourSize.Width / 32;
                    double y = (rand.Next(32) - 8) * ourSize.Height / 32;
                    AnimateTo(child, 0, x, y, scaleFactor);
                }
            }
        }

        private void AnimateTo(UIElement child, double r, double x, double y, double s) {
            TransformGroup group = (TransformGroup) child.RenderTransform;
            ScaleTransform scale = (ScaleTransform) group.Children[0];
            TranslateTransform trans = (TranslateTransform) group.Children[1];
            RotateTransform rot = (RotateTransform) group.Children[2];

            //rot.BeginAnimation(RotateTransform.AngleProperty, MakeAnimation(r, anim_Completed));
            trans.BeginAnimation(TranslateTransform.XProperty, MakeAnimation(x));
            trans.BeginAnimation(TranslateTransform.YProperty, MakeAnimation(y));
            //scale.BeginAnimation(ScaleTransform.ScaleXProperty, MakeAnimation(s));
            //scale.BeginAnimation(ScaleTransform.ScaleYProperty, MakeAnimation(s));
        }

        private DoubleAnimation MakeAnimation(double to) {
            return MakeAnimation(to, null);
        }

        private DoubleAnimation MakeAnimation(double to, EventHandler endEvent) {
            DoubleAnimation anim = new DoubleAnimation(to, TimeSpan.FromMilliseconds(AnimationMilliseconds));
            anim.AccelerationRatio = 0.2;
            anim.DecelerationRatio = 0.7;
            if (endEvent != null)
                anim.Completed += endEvent;
            return anim;
        }

    }
}

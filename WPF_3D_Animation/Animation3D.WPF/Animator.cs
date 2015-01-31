using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace Animation3D.WPF {
    /// <summary>
    /// Handles animations on given FrameworkElement
    /// </summary>
    public class Animator {
        private RotateTransform3D _rotateTransform3D = null;
        private AxisAngleRotation3D _frontRotation;
        private AxisAngleRotation3D _backRotation;


        /// <summary>
        /// Creates a new instance of the  <see cref="Animator"/> class.
        /// </summary>
        public Animator() { }



        /// <summary>
        /// Prepares FrameworkElement for animation.
        /// </summary>
        public void Initialize(FrameworkElement frameworkElement) {

            // assume its a button
            Button button = frameworkElement as Button;
            // get viewport
            Viewport3D viewport = button.Template.FindName("PART_Viewport", button) as Viewport3D;

            // set camera
            viewport.Camera = new PerspectiveCamera {
                Position = new Point3D(0, 0, 1),
                LookDirection = new Vector3D(0, 0, -1),
                FieldOfView = 90
            };

            // get transform
            Viewport2DVisual3D contentSurface = viewport.Children[1] as Viewport2DVisual3D;
            _rotateTransform3D = contentSurface.Transform as RotateTransform3D;

            // get rotation animation
            AxisAngleRotation3D rotation = _rotateTransform3D.Rotation as AxisAngleRotation3D;
            _frontRotation = _backRotation = rotation;
        }

        /// <summary>
        /// Animates FrameworkElement to original position
        /// </summary>
        public void Reset() {
            // Create the animations.
            DoubleAnimation frontAnimation, backAnimation;
            this.PrepareForRotation(out frontAnimation, out backAnimation, null);
            _backRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, backAnimation);
        }

        /// <summary>
        /// Performs animations
        /// </summary>
        public void Animate(FrameworkElement element, Point tiltTouchPoint) {
            // Create the animations.
            DoubleAnimation frontAnimation, backAnimation;

            CalculatorResult calculatorResult = Calculator.Calculate(element, tiltTouchPoint);
            _rotateTransform3D.CenterX = calculatorResult.Center[0];
            _rotateTransform3D.CenterY = calculatorResult.Center[1];
            _rotateTransform3D.CenterZ = calculatorResult.Center[2];

            this.PrepareForRotation(out frontAnimation, out backAnimation, calculatorResult);

            // Start the animations.
            _frontRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, frontAnimation);
        }

        /// <summary>
        /// Initializes animations
        /// </summary>
        private void PrepareForRotation(out DoubleAnimation frontAnimation, out DoubleAnimation backAnimation, CalculatorResult calculatorResult) {

            if (calculatorResult == null) {
                Vector3D axis = new Vector3D(1, 0, 0);
                double delta = 3;

                _frontRotation.Axis = axis;
                _backRotation.Axis = axis;



                //_RotateTransform3D.ce

                backAnimation = new EasingDoubleAnimation {
                    Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                    Equation = EasingEquation.CircEaseIn,
                    From = delta,
                    To = 0
                };

                frontAnimation = new EasingDoubleAnimation {
                    Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                    Equation = EasingEquation.CircEaseIn,
                    From = 0,
                    To = _frontRotation.Angle
                };

                return;
            }

            _frontRotation.Axis = calculatorResult.Axis;
            _backRotation.Axis = calculatorResult.Axis;

            frontAnimation = new EasingDoubleAnimation {
                Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                Equation = EasingEquation.CircEaseIn,
                From = _frontRotation.Angle,
                To = calculatorResult.Angle
            };

            backAnimation = new EasingDoubleAnimation {
                Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                Equation = EasingEquation.CircEaseIn,
                From = calculatorResult.Angle,
                To = 0
            };
        }
    }
}

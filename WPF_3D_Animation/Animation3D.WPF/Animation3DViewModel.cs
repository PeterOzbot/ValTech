using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Animation3D.WPF.Framework;

namespace Animation3D.WPF {
    /// <summary>
    /// View model for 3D animation
    /// </summary>
    public class Animation3DViewModel {
        private Animator _animator;

        /// <summary>
        /// Command used to initialize animatin elementen
        /// </summary>
        public ICommand InitializeCommand { get; private set; }
        /// <summary>
        /// Resets the values
        /// </summary>
        public ICommand ResetCommand { get; private set; }
        /// <summary>
        /// Command executed when mouse over element event is triggered
        /// </summary>
        public ICommand MouseMoveCommand { get; private set; }
        /// <summary>
        /// Path of image displayed
        /// </summary>
        public string ImagePath { get; private set; }
        /// <summary>
        /// Displays the calcualted values
        /// </summary>
        public Logger Logger { get; private set; }



        /// <summary>
        /// Creates a new instance of the  <see cref="Animation3DViewModel"/> class.
        /// </summary>
        public Animation3DViewModel() {
            InitImagePath();
            _animator = new Animator();
            InitializeCommand = new Command<FrameworkElement>(_animator.Initialize, null);
            ResetCommand = new Command(_animator.Reset, null);
            MouseMoveCommand = new Command<object>(OnMouseMove, null);
            Logger = Logger.Create();
        }



        /// <summary>
        /// Sets the iamge path
        /// </summary>
        private void InitImagePath() {
            string location = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // get directory path
            ImagePath = Path.Combine(location, "Image/image.png");
        }

        /// <summary>
        /// Mouse move command method
        /// </summary>
        private void OnMouseMove(dynamic value) {
            // get element
            FrameworkElement element = value.Sender as FrameworkElement;
            // Touch point relative to the element being tilted.
            Point tiltTouchPoint = value.Args.GetPosition(value.Sender as IInputElement);

            _animator.Animate(element, tiltTouchPoint);
        }
    }
}

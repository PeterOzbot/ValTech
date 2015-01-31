using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Animation3D.WPF.Framework {
    /// <summary>
    /// Behaviors for mouse events turned into commands.
    /// </summary>
    public class MouseCommandBehaviors {
        public static readonly DependencyProperty MouseMoveCommandProperty =
            DependencyProperty.RegisterAttached("MouseMoveCommand", typeof(ICommand),
            typeof(MouseCommandBehaviors), new FrameworkPropertyMetadata(
            new PropertyChangedCallback(MouseMoveCommandChanged)));


        private static void MouseMoveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            FrameworkElement element = (FrameworkElement) d;

            element.MouseMove += element_MouseMove;
        }

        static void element_MouseMove(object sender, MouseEventArgs e) {
            FrameworkElement element = (FrameworkElement) sender;

            ICommand command = GetMouseMoveCommand(element);

            command.Execute(new { Sender = sender, Args = e });
        }

        public static void SetMouseMoveCommand(UIElement element, ICommand value) {
            element.SetValue(MouseMoveCommandProperty, value);
        }

        public static ICommand GetMouseMoveCommand(UIElement element) {
            return (ICommand) element.GetValue(MouseMoveCommandProperty);
        }
    }
}

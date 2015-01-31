using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation3D.WPF {
    /// <summary>
    /// Used to display calculated values
    /// </summary>
    public class Logger : INotifyPropertyChanged {
        private string _text;
        /// <summary>
        /// Display text
        /// </summary>
        public string Text { get { return _text; } set { _text = value; OnPropertyChanged("Text"); } }


        /// <summary>
        /// PropertyChanged executing helper method.
        /// </summary>
        protected void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;




        private static Logger _instance;
        /// <summary>
        /// Creates Logger singleton
        /// </summary>
        public static Logger Create() {
            _instance = new Logger();
            return _instance;
        }

        /// <summary>
        /// Displays given text.
        /// </summary>
        public static void Write(string text) {
            if (_instance != null) {
                _instance.Text = text;
            }
        }
    }
}

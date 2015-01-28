using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AsyncVirtualization.Library;

namespace Async_Data_and_UI_Virtualization_Collection {
    /// <summary>
    /// Template selector used to select normal element template or loading template
    /// </summary>
    public class ElementTemplateSelector : DataTemplateSelector {
        public DataTemplate ElementTemplate { get; set; }
        public DataTemplate LoadingElementTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            LoadingElement element = item as LoadingElement;
            if (element != null) {
                return LoadingElementTemplate;
            }
            else {
                return ElementTemplate;
            }
        }
    }
}

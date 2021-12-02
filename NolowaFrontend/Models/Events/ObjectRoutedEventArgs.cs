using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NolowaFrontend.Models.Events
{
    public class ObjectRoutedEventArgs : RoutedEventArgs
    {
        private readonly object _parameter;

        public object Parameter => _parameter;

        public ObjectRoutedEventArgs(RoutedEvent routedEvent, object parameter) : base(routedEvent)
        {
            _parameter = parameter;
        }
    }
}

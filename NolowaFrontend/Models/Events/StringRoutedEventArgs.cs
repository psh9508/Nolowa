using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NolowaFrontend.Models.Events
{
    public class StringRoutedEventArgs : RoutedEventArgs
    {
        private readonly string _parameter;

        public string Parameter => _parameter;

        public StringRoutedEventArgs(RoutedEvent routedEvent, string parameter) : base(routedEvent)
        {
            _parameter = parameter;
        }
    }
}

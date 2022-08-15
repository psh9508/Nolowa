using NolowaFrontend.Extensions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NolowaFrontend.Extensions
{
    public class NolowaScrollCahngedEventArgs : EventArgs
    {
        public double VerticalChange { get; set; }
        public double VerticalOffset { get; set; }
        public double ScrollableHeight { get; set; }
    }

    public class ListboxExtension
    {
        public static readonly DependencyProperty ScrollChangedCommandProperty = DependencyProperty.RegisterAttached(
            "ScrollChangedCommand", typeof(ICommand), typeof(ListboxExtension),
            new PropertyMetadata(default(ICommand), OnScrollChangedCommandChanged));

        private static void OnScrollChangedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListBox listBox = d as ListBox;

            if (listBox == null)
                return;
            if (e.NewValue != null)
            {
                listBox.Loaded += ListBoxOnLoaded;
            }
            else if (e.OldValue != null)
            {
                listBox.Loaded -= ListBoxOnLoaded;
            }
        }

        private static void ListBoxOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ListBox listBox = sender as ListBox;
            if (listBox == null)
                return;

            ScrollViewer scrollViewer = UIHelper.FindChildren<ScrollViewer>(listBox).FirstOrDefault();
            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += ScrollViewerOnScrollChanged;
            }
        }

        private static void ScrollViewerOnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ListBox listBox = UIHelper.FindParent<ListBox>(sender as ScrollViewer);
            ScrollViewer scrollViewer = UIHelper.FindChildren<ScrollViewer>(listBox).FirstOrDefault();

            if (listBox != null)
            {
                ICommand command = GetScrollChangedCommand(listBox);
                command.Execute(new NolowaScrollCahngedEventArgs()
                {
                    VerticalChange = e.VerticalChange,
                    VerticalOffset = scrollViewer.VerticalOffset,
                    ScrollableHeight = scrollViewer.ScrollableHeight,
                });
            }
        }

        public static void SetScrollChangedCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(ScrollChangedCommandProperty, value);
        }

        public static ICommand GetScrollChangedCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(ScrollChangedCommandProperty);
        }
    }
}

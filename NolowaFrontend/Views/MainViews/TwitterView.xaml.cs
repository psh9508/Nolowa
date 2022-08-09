using NolowaFrontend.Models;
using NolowaFrontend.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NolowaFrontend.Views.MainViews
{
    /// <summary>
    /// TwitterView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TwitterView : UserControl
    {
        private bool _isScrollbarOnTop;
        private readonly Timer _timer;

        /// <summary>
        /// 프로필 클릭 이벤트를 라우티드이벤트로 만들어서 밖으로 버블링시킴
        /// </summary>
        public event RoutedEventHandler ClickedProfileImage
        {
            add { AddHandler(ClickedProfileImageEvent, value); }
            remove { RemoveHandler(ClickedProfileImageEvent, value); }
        }

        public static readonly RoutedEvent ClickedProfileImageEvent =
           EventManager.RegisterRoutedEvent("ClickedProfileImage", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TwitterView));

        public bool HideReloadCircle
        {
            get { return (bool)GetValue(HideReloadCircleProperty); }
            set { SetValue(HideReloadCircleProperty, value); }
        }

        public static readonly DependencyProperty HideReloadCircleProperty =
            DependencyProperty.Register("HideReloadCircle", typeof(bool), typeof(TwitterView), new PropertyMetadata(false));

        public double ReloadHeight
        {
            get { return (double)GetValue(ReloadHeightProperty); }
            set { SetValue(ReloadHeightProperty, value); }
        }

        public static readonly DependencyProperty ReloadHeightProperty =
            DependencyProperty.Register("ReloadHeight", typeof(double), typeof(TwitterView), new PropertyMetadata((s, e) =>
            {
                var sender = s as TwitterView;

                if (sender.ReloadHeight >= sender.ReloadGrid.MaxHeight)
                {
                    sender.ReloadHeight = sender.ReloadGrid.MaxHeight;
                }
            }));

        public double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(double), typeof(TwitterView), new PropertyMetadata((s, e) =>
            {
                const double maxCircleValue = 360.0d;

                var sender = s as TwitterView;

                if (sender.ProgressValue >= maxCircleValue)
                {
                    sender.ProgressValue = maxCircleValue;
                    sender.ReloadCommand?.Execute(null);
                }
            }));


        public ICommand ReloadCommand
        {
            get { return (ICommand)GetValue(ReloadCommandProperty); }
            set { SetValue(ReloadCommandProperty, value); }
        }

        public static readonly DependencyProperty ReloadCommandProperty =
            DependencyProperty.Register("ReloadCommand", typeof(ICommand), typeof(TwitterView), new PropertyMetadata(null));


        public TwitterView()
        {
            InitializeComponent();

            _timer = new Timer();
            _timer.AutoReset = false;
            _timer.Interval = 1000;
            _timer.Elapsed += (s, e) => {
                this.Dispatcher.Invoke(() => {
                    HideReloadCircle = true;
                });
            };
        }

        private void PostView_ClickedProfileImage(object sender, RoutedEventArgs e)
        {
            if (e is ObjectRoutedEventArgs args)
            {
                var newEventArgs = new ObjectRoutedEventArgs(ClickedProfileImageEvent, args.Parameter);
                RaiseEvent(newEventArgs);
            }
        }

        private void ListBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if(e.VerticalOffset <= 0)
            {
                _isScrollbarOnTop = true;
            }
            else
            {
                _isScrollbarOnTop = false;
            }
        }   

        private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (_isScrollbarOnTop)
            {
                if (e.Delta > 0)
                {
                    _timer.Start();

                    ReloadHeight += e.Delta / 4;
                    ProgressValue = ReloadHeight * 3.6;
                }               
            }

            if (e.Delta < 0)
            {
                _timer.Stop();
                ReloadHeight = 0;
            }
        }
    }
}

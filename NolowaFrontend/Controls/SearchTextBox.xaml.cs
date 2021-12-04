using NolowaFrontend.Extensions;
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

namespace NolowaFrontend.Controls
{
    /// <summary>
    /// SearchTextBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        private readonly Timer _timer;

        /// <summary>
        /// 타이머로 검색 될 때 발생하는 이벤트
        /// </summary>
        public static readonly RoutedEvent TimerSearchEvent = 
            EventManager.RegisterRoutedEvent("TimerSearchEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchTextBox));

        public event RoutedEventHandler TimerSearch
        {
            add => AddHandler(TimerSearchEvent, value);
            remove => RemoveHandler(TimerSearchEvent, value);
        }

        /// <summary>
        /// 엔터기를 이용해 검색이 될 때 발생하는 이벤트
        /// </summary>
        public static readonly RoutedEvent EnterSearchEvent =
           EventManager.RegisterRoutedEvent("EnterSearchEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchTextBox));

        public event RoutedEventHandler EnterSearch
        {
            add => AddHandler(EnterSearchEvent, value);
            remove => RemoveHandler(EnterSearchEvent, value);
        }

        public SearchTextBox()
        {
            InitializeComponent();
            
            _timer = new Timer();
            _timer.AutoReset = false;
            _timer.Interval = 700;
            _timer.Elapsed += (s, e) => {
                FireEvent(TimerSearchEvent);
            };
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (_timer.IsNull() || searchTextBox.Text.IsNotVaild())
            //    return;

            _timer.Stop();
            _timer.Start();
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _timer.Stop(); // 엔터를 누르자마자 timer 검색을 중지
                FireEvent(EnterSearchEvent);
            }
        }

        private void FireEvent(RoutedEvent routedEvent)
        {
            try
            {
                if (routedEvent.IsNull())
                    throw new NotImplementedException("eventArgs 설정 하지 않은채 검색을 시도 하였습니다.");

                Dispatcher.Invoke(() => {
                    var eventArgs = new StringRoutedEventArgs(routedEvent, searchTextBox.Text);
                    RaiseEvent(eventArgs);
                }); 
            }
            finally
            {
                _timer.Stop();
            }
        }
    }
}

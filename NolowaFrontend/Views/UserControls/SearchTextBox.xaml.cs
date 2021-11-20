using NolowaFrontend.Extensions;
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

namespace NolowaFrontend.Views.UserControls
{
    /// <summary>
    /// SearchTextBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        private readonly Timer _timer;

        //public event Action<string> GotTimerSearchTriggered;
        public RoutedEventHandler GotTimerSearchTriggered;
        public event Action<string> GotEnterSearchTriggered;


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
        /// 타이머를 이용한 검색을 수행할 명령어
        /// </summary>
        public ICommand TimerSearchCommand
        {
            get { return (ICommand)GetValue(TimerSearchCommandProperty); }
            set { SetValue(TimerSearchCommandProperty, value); }
        }

        public static readonly DependencyProperty TimerSearchCommandProperty =
            DependencyProperty.Register("TimerSearchCommand", typeof(ICommand), typeof(SearchTextBox));

        /// <summary>
        /// 엔터를 쳐서 검색을 할 때 수행할 명령어
        /// </summary>
        public ICommand EnterSearchCommand
        {
            get { return (ICommand)GetValue(EnterSearchCommandProperty); }
            set { SetValue(EnterSearchCommandProperty, value); }
        }

        public static readonly DependencyProperty EnterSearchCommandProperty =
            DependencyProperty.Register("EnterSearchCommand", typeof(ICommand), typeof(SearchTextBox));

        public SearchTextBox()
        {
            InitializeComponent();
            
            _timer = new Timer();
            _timer.AutoReset = false;
            _timer.Interval = 700;
            _timer.Elapsed += (s, e) => {
                Dispatcher.Invoke(() => {
                    var newEventArgs = new RoutedEventArgs(TimerSearchEvent, searchTextBox.Text);
                    RaiseEvent(newEventArgs);
                });
            };
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_timer.IsNull() || searchTextBox.Text.IsNotVaild())
                return;

            _timer.Stop();
            _timer.Start();
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _timer.Stop();
                SearchUser();
            }
        }

        private void SearchUser()
        {
            Dispatcher.Invoke(() => {
                try
                {
                    //if (searchTextBox.Text.IsValid())
                    //{
                        if (EnterSearchCommand.IsNull())
                            throw new NotImplementedException("EnterSearchCommand 설정 하지 않은채 검색을 시도 하였습니다.");

                        EnterSearchCommand.Execute(searchTextBox.Text);
                    //}
                }
                finally
                {
                    _timer.Stop();
                }
            });
        }
    }
}

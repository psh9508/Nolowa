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
                Search();
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

        private void Search()
        {
            Dispatcher.Invoke(() => {
                Search(TimerSearchCommand);
            });
        }

        private void SearchUser()
        {
            Dispatcher.Invoke(() => {
                Search(EnterSearchCommand);
            });
        }

        private void Search(ICommand command)
        {
            try
            {
                //if (searchTextBox.Text.IsValid())
                //{
                    if (command.IsNull())
                        throw new NotImplementedException("command가 설정 하지 않은채 검색을 시도 하였습니다.");

                    command.Execute(searchTextBox.Text);
                //}
            }
            finally
            {
                _timer.Stop();
            }
        }
    }
}

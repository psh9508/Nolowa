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
        /// 검색할 때 수행할 명령어
        /// </summary>
        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }

        public static readonly DependencyProperty SearchCommandProperty =
            DependencyProperty.Register("SearchCommand", typeof(ICommand), typeof(SearchTextBox));

        /// <summary>
        /// 엔터를 쳐서 유저를 검색할 때 수행할 명령어
        /// </summary>
        public ICommand SearchUserCommand
        {
            get { return (ICommand)GetValue(SearchUserCommandProperty); }
            set { SetValue(SearchUserCommandProperty, value); }
        }

        public static readonly DependencyProperty SearchUserCommandProperty =
            DependencyProperty.Register("SearchUserCommand", typeof(ICommand), typeof(SearchTextBox));


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
                Search(SearchCommand);
            });
        }

        private void SearchUser()
        {
            Dispatcher.Invoke(() => {
                Search(SearchUserCommand);
            });
        }

        private void Search(ICommand command)
        {
            try
            {
                if (searchTextBox.Text.IsValid())
                {
                    if (command.IsNull())
                        throw new NotImplementedException("command가 설정 하지 않은채 검색을 시도 하였습니다.");

                    command.Execute(searchTextBox.Text);
                }
            }
            finally
            {
                _timer.Stop();
            }
        }
    }
}

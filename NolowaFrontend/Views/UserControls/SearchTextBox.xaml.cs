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

        public SearchTextBox()
        {
            InitializeComponent();
            
            _timer = new Timer();
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
                Search();
            }
        }

        private void Search()
        {
            Dispatcher.Invoke(() =>
            {
                if (searchTextBox.Text.IsValid())
                {
                    if (SearchCommand.IsNull())
                        throw new NotImplementedException("SearchCommand이 설정 하지 않은채 검색을 시도 하였습니다.");

                    SearchCommand.Execute(searchTextBox.Text);
                }
            });
        }
    }
}

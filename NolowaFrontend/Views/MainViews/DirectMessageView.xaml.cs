using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// DirectMessageView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DirectMessageView : UserControl
    {
        public ICommand ClickedDirectMessageViewShowCommand
        {
            get { return (ICommand)GetValue(ClickedDirectMessageViewShowCommandProperty); }
            set { SetValue(ClickedDirectMessageViewShowCommandProperty, value); }
        }

        public static readonly DependencyProperty ClickedDirectMessageViewShowCommandProperty =
            DependencyProperty.Register("ClickedDirectMessageViewShowCommand", typeof(ICommand), typeof(DirectMessageView), new PropertyMetadata(null));

        public DirectMessageView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickedDirectMessageViewShowCommand?.Execute(null);
        }
    }
}

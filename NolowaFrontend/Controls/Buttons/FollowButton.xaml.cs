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

namespace NolowaFrontend.Controls.Buttons
{
    /// <summary>
    /// FollowButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FollowButton : UserControl
    {
        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(FollowButton), new PropertyMetadata(null));


        public FollowButton()
        {
            InitializeComponent();
        }
    }
}

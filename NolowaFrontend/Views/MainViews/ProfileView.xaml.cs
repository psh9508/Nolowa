using NolowaFrontend.Models;
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
    /// ProfileView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProfileView : UserControl
    {
        /// <summary>
        /// 프로필 클릭 이벤트를 라우티드이벤트로 만들어서 밖으로 버블링시킴
        /// </summary>
        public static readonly RoutedEvent ClickedProfileImageEvent =
            EventManager.RegisterRoutedEvent("ClickedProfileImage", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ProfileView));

        public event RoutedEventHandler ClickedProfileImage
        {
            add { AddHandler(ClickedProfileImageEvent, value); }
            remove { RemoveHandler(ClickedProfileImageEvent, value); }
        }


        //public User User
        //{
        //    get { return (User)GetValue(UserProperty); }
        //    set { SetValue(UserProperty, value); }
        //}

        //public static readonly DependencyProperty UserProperty =
        //    DependencyProperty.Register("User", typeof(User), typeof(ProfileView), new PropertyMetadata(null));


        public ProfileView()
        {
            InitializeComponent();
        }

        public static void Show(User user)
        {
            
        }
    }
}

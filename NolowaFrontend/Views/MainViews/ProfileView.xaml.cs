using NolowaFrontend.Models;
using NolowaFrontend.Servicies;
using NolowaFrontend.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly IPostService _postService;
        private readonly User _user;

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

        public ProfileView()
        {
            InitializeComponent();

            _postService = new PostService();
        }

        public ProfileView(User user) : this()
        {
            _user = user;
        }

        private async void ProfileView_Loaded(object sender, RoutedEventArgs e)
        {
            var postsResponse = await _postService.GetPostsAsync(_user.ID);

            if (postsResponse.IsSuccess)
                listPosts.ItemsSource = postsResponse.ResponseData;
        }
    }
}

using NolowaFrontend.Extensions;
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

        public ObservableCollection<Post> Posts
        {
            get { return (ObservableCollection<Post>)GetValue(PostsProperty); }
            set { SetValue(PostsProperty, value); }
        }

        public static readonly DependencyProperty PostsProperty =
            DependencyProperty.Register("Posts", typeof(ObservableCollection<Post>), typeof(ProfileView), new PropertyMetadata(new ObservableCollection<Post>()));

        public User User
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(User), typeof(ProfileView), new PropertyMetadata(null));


        public bool IsFollowButtonVisible
        {
            get { return (bool)GetValue(IsFollowButtonVisibleProperty); }
            set { SetValue(IsFollowButtonVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsFollowButtonVisibleProperty =
            DependencyProperty.Register("IsFollowButtonVisible", typeof(bool), typeof(ProfileView), new PropertyMetadata(true));


        public ProfileView()
        {
            InitializeComponent();

            _postService = new PostService();
        }

        public ProfileView(User user) : this()
        {
            User = user;
        }
        
        private async void ProfileView_Loaded(object sender, RoutedEventArgs e)
        {
            Posts = new ObservableCollection<Post>();

            if(User.IsNotNull())
            {
                var postsResponse = await _postService.GetMyPostsAsync(User.ID);

                if (postsResponse.IsSuccess)
                    Posts = postsResponse.ResponseData.ToObservableCollection();
            }
        }

        private void Background_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}

using NolowaFrontend.Core;
using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
using NolowaFrontend.Models.Events;
using NolowaFrontend.ViewModels;
using NolowaFrontend.ViewModels.Base;
using NolowaFrontend.Views.MainViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace NolowaFrontend.Views.UserControls
{
    /// <summary>
    /// PostView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PostView : UserControl
    {
        public string UserAccountID
        {
            get { return (string)GetValue(UserAccountIDProperty); }
            set { SetValue(UserAccountIDProperty, value); }
        }

        public static readonly DependencyProperty UserAccountIDProperty =
            DependencyProperty.Register("UserAccountID", typeof(string), typeof(PostView), new PropertyMetadata(""));

        public string UserID
        {
            get { return (string)GetValue(UserIDProperty); }
            set { SetValue(UserIDProperty, value); }
        }

        public static readonly DependencyProperty UserIDProperty =
            DependencyProperty.Register("UserID", typeof(string), typeof(PostView), new PropertyMetadata(""));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(PostView), new PropertyMetadata(""));

        public string ProfileImageSource
        {
            get { return (string)GetValue(ProfileImageSourceProperty); }
            set { SetValue(ProfileImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ProfileImageSourceProperty =
            DependencyProperty.Register("ProfileImageSource", typeof(string), typeof(PostView), new PropertyMetadata(""));

        public string ElapsedTime
        {
            get { return (string)GetValue(ElapsedTimeProperty); }
            set { SetValue(ElapsedTimeProperty, value); }
        }

        public static readonly DependencyProperty ElapsedTimeProperty =
            DependencyProperty.Register("ElapsedTime", typeof(string), typeof(PostView), new PropertyMetadata(""));

        public int SpeechBubbleCount
        {
            get { return (int)GetValue(SpeechBubbleCountProperty); }
            set { SetValue(SpeechBubbleCountProperty, value); }
        }

        public static readonly DependencyProperty SpeechBubbleCountProperty =
            DependencyProperty.Register("SpeechBubbleCount", typeof(int), typeof(PostView), new PropertyMetadata(1000));

        public int LikeCount
        {
            get { return (int)GetValue(LikeCountProperty); }
            set { SetValue(LikeCountProperty, value); }
        }

        public static readonly DependencyProperty LikeCountProperty =
            DependencyProperty.Register("LikeCount", typeof(int), typeof(PostView), new PropertyMetadata(0));

        // Client에서 Post를 고유하게 식별하는 값
        public Guid Guid { get; set; } = new Guid();

        public PostView()
        {
            InitializeComponent();
        }

        public PostView(Post post) : base()
        {
            UserAccountID = post.PostedUser.AccountID;
            UserID = post.PostedUser.ID.ToString();
            Message = post.Message;
            ElapsedTime = post.UploadedDateTime.ToElapsedTime();
            ProfileImageSource = post.PostedUser.GetProfileImageFile();
            Guid = post.Guid;
        }

        public void CompleteUpload()
        {
            this.IsEnabled = true;
        }

        private void SpeechBubbleButton_Click(object sender, RoutedEventArgs e)
        {
            SpeechBubbleCount++;
        }

        private void HeartButton_Click(object sender, RoutedEventArgs e)
        {
            LikeCount++;
        }
    }
}

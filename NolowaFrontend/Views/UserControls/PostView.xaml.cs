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
using System.Runtime.CompilerServices;
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
    public partial class PostView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 프로필 클릭 이벤트를 라우티드이벤트로 만들어서 밖으로 버블링시킴
        /// </summary>
        public static readonly RoutedEvent ClickedProfileImageEvent =
            EventManager.RegisterRoutedEvent("ClickedProfileImageRouted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PostView));
      
        public event RoutedEventHandler ClickedProfileImageRouted
        {
            add { AddHandler(ClickedProfileImageEvent, value); }
            remove { RemoveHandler(ClickedProfileImageEvent, value); }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(PostView), new PropertyMetadata(""));

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

        public User PostedUser
        {
            get { return (User)GetValue(PostedUserProperty); }
            set { SetValue(PostedUserProperty, value); }
        }

        public static readonly DependencyProperty PostedUserProperty =
            DependencyProperty.Register("PostedUser", typeof(User), typeof(PostView), new PropertyMetadata(null));


        public ICommand ReloadCommand
        {
            get { return (ICommand)GetValue(ReloadCommandProperty); }
            set { SetValue(ReloadCommandProperty, value); }
        }

        public static readonly DependencyProperty ReloadCommandProperty =
            DependencyProperty.Register("ReloadCommand", typeof(ICommand), typeof(PostView), new PropertyMetadata(null));

        private bool _isPostLoading;

        public bool IsPostLoading
        {
            get { return _isPostLoading; }
            set { _isPostLoading = value; OnPropertyChanged(); }
        }

        // Client에서 Post를 고유하게 식별하는 값
        public Guid Guid { get; set; } = new Guid();

        public PostView()
        {
            InitializeComponent();
        }

        public PostView(Post post) : base()
        {
            if(post.IsNull())
            {
                // post가 null이 들어오면 실제 데이터가 아니라 listbox 가장 마지막에 넣어줄
                // 로딩 표시가 들어가있는 빈 데이터를 생성한다.
            }
            else
            {
                Message = post.Message;
                ElapsedTime = post.UploadedDateTime.ToElapsedTime();
                PostedUser = post.PostedUser;

                Guid = post.Guid;
            }
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

        private void ProfileImageElipseView_ClickedProfileImage(object sender, RoutedEventArgs e)
        {
            //RaiseEvent(e);
            if(e is ObjectRoutedEventArgs args)
            {
                var newEventArgs = new ObjectRoutedEventArgs(ClickedProfileImageEvent, args.Parameter);
                RaiseEvent(newEventArgs);
            }
        }
    }
}

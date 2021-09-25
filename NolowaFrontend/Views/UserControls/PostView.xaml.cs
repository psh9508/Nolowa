using NolowaFrontend.ViewModels;
using NolowaFrontend.ViewModels.Base;
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

        public PostView()
        {
            InitializeComponent();
            ProfileImageSource = @"~\..\Resources\ProfilePicture.jpg";
        }
    }
}

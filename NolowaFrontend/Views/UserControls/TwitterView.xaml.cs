using NolowaFrontend.Models;
using NolowaFrontend.ViewModels;
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
    /// TweetView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TwitterView : UserControl
    {
        public event Action<Post> MadeNewTwitter;
        public event Action<Guid> FailedUploadTwitter;
        public event Action CompleteTwitter;

        private readonly TwitterVM _twitterVM;

        public TwitterView(User user)
        {
            InitializeComponent();

            _twitterVM = new TwitterVM(user);

            _twitterVM.MadeNewTwitter += newTweet => {
                MadeNewTwitter?.Invoke(newTweet);
            };

            _twitterVM.FailedUploadTwitter += guid => {
                FailedUploadTwitter?.Invoke(guid);
            };

            _twitterVM.UploadedTwitter += response => {
                CompleteTwitter?.Invoke();
            };

            this.DataContext = _twitterVM;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            _twitterVM.HideComplete = true;
        }
    }
}

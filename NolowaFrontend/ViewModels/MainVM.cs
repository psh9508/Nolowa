using NolowaFrontend.Models;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using NolowaFrontend.Servicies;
using System.Windows.Input;
using System.Collections.ObjectModel;
using NolowaFrontend.Views.UserControls;
using NolowaFrontend.Extensions;
using NolowaFrontend.Core;
using System.Net;
using NolowaFrontend.Views.MainViews;
using NolowaFrontend.Models.Events;
using System.Windows;
using NolowaFrontend.Views;
using System.Windows.Controls;

namespace NolowaFrontend.ViewModels
{
    public class MainVM : ViewModelBase
    {
        private readonly User _user;
        private readonly IPostService _service;
        private readonly ISearchService _searchService;
        private readonly SearchView _searchView;

        public string ProfileImageSource => _user.ProfileImageFile;
        public User User => _user;

        private ObservableCollection<PostView> _posts = new ObservableCollection<PostView>();

        public ObservableCollection<PostView> Posts
        {
            get { return _posts; }
            set { _posts = value; OnPropertyChanged(); }
        }

        private object _makeTwitterView;

        public object MakeTwitterView
        {
            get { return _makeTwitterView; }
            set { _makeTwitterView = value; OnPropertyChanged(); }
        }

        private object _mainView;

        public object MainView
        {
            get { return _mainView; }
            set { _mainView = value; OnPropertyChanged(); }
        }

        private object _userProfileView;

        public object UserProfileView
        {
            get { return _userProfileView; }
            set { _userProfileView = value; OnPropertyChanged(); }
        }


        private object _twitterResultView;

        public object TwitterResultView
        {
            get { return _twitterResultView; }
            set { _twitterResultView = value; OnPropertyChanged(); }
        }

        #region ICommands
        private ICommand _loadedEventCommand;

        public ICommand LoadedEventCommand
        {
            get
            {
                return GetRelayCommand(ref _loadedEventCommand, async _ =>
                {
                    await CachingProfileImageFileToLocal();

                    var posts = await _service.GetPostsAsync(_user.ID);

                    foreach (var post in posts.ResponseData)
                    {
                        //osts.Add(post);
                        Posts.Add(new PostView(post));
                    }

                    HomeViewCommand?.Execute(null);
                });
            }
        }

        private ICommand _twitterCommand;

        public ICommand TwitterCommand
        {
            get
            {
                return GetRelayCommand(ref _twitterCommand, _ =>
                {
                    var makeTwitterView = new MakeTwitterView(_user);
                    PostView postView = null;
                    //Post post = null;

                    makeTwitterView.MadeNewTwitter += newTwitter => {
                        postView = new PostView(newTwitter);
                        postView.IsEnabled = false;

                        Posts.Insert(0, postView);

                        //newTwitter.IsEnable = false;
                        //Posts.Insert(0, newTwitter);
                    };

                    makeTwitterView.FailedUploadTwitter += async guid => {
                        await Task.Delay(2000);

                        var uploadFailedTwitter = Posts.Where(x => x.Guid == guid).FirstOrDefault() ;

                        Posts.Remove(uploadFailedTwitter);
                    };

                    makeTwitterView.CompleteTwitter += () => {
                        TwitterResultView = new TwitterResultView();
                        postView?.CompleteUpload();
                        //post?.CompleteUpload();
                    };

                    MakeTwitterView = makeTwitterView;
                });
            }
        }

        private ICommand _searchViewCommand;

        public ICommand SearchViewCommand
        {
            get
            {
                return GetRelayCommand(ref _searchViewCommand, _ => {
                    //_searchView.ClickedProfileImag.Clear(); // 화면 바꿀 때 마다 이벤트 걸릴 것임
                    _searchView.ClickedProfileImage += (object sender, RoutedEventArgs e) =>
                    {
                        // 계속 이렇게 추가해야 하는건가?

                        if (e is ObjectRoutedEventArgs args)
                        {
                            var clickedUser = (User)args.Parameter;
                            var profileView = new ProfileView(clickedUser);

                            UserProfileView = profileView;
                        }
                    };
                    MainView = _searchView;
                });
            }
        }

        private ICommand _homeViewCommand;

        public ICommand HomeViewCommand
        {
            get
            {
                return GetRelayCommand(ref _homeViewCommand, _ =>
                {
                    var twitterView = new TwitterView();
                    twitterView.ClickedProfileImage += (object sender, RoutedEventArgs e) =>
                    {
                        // VM에서 이벤트를 연결하는게 좋은건가 xaml에서 이벤트를 연결하는게 좋은건가?

                        if (e is ObjectRoutedEventArgs args)
                        {
                            var clickedUser = (User)args.Parameter;
                            var profileView = new ProfileView(clickedUser);

                            UserProfileView = profileView;
                        }
                    };

                    MainView = twitterView;
                });
            }
        }

        private ICommand _testCommand;

        public ICommand TestCommand
        {
            get
            {
                return GetRelayCommand(ref _testCommand, text =>
                {
                });
            }
        }
        #endregion

        public MainVM(User user)
        {
            if (user.IsNull())
                throw new InvalidOperationException("로그인 된 user는 null일 수 없습니다.");

            AppConfiguration.LoginUser = user;

            _user = user;
            _service = new PostService();
            _searchService = new SearchService();
            _searchView = new SearchView();

            LoadedEventCommand.Execute(null);
        }

        public async void OnTimerSearch(object sender, StringRoutedEventArgs e)
        {
            await _searchView.TimerSearchAsync(e.Parameter);
        }

        public void OnEnterSearch(object sender, StringRoutedEventArgs e)
        {
        }

        private async Task CachingProfileImageFileToLocal()
        {
            await Task.Run(() =>
            {
                if (_user.ProfileImage.IsNull())
                    return;

                // No need to caching a file when it's a default profile image file.
                if (File.Exists(_user.ProfileImage.URL) == false)
                {
                    // LOG
                    return;
                }

                var profileImageCachingFullPath = Constant.PROFILE_IMAGE_ROOT_PATH + _user.ProfileImage.Hash + ".jpg";

                if (File.Exists(profileImageCachingFullPath) == false)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(_user.ProfileImage.URL, profileImageCachingFullPath);
                    }
                }
            });
        }
    }
}


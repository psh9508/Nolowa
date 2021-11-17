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

namespace NolowaFrontend.ViewModels
{
    public class MainVM : ViewModelBase
    {
        private readonly User _user;
        private readonly IPostService _service;
        private readonly ISearchService _searchService;

        public string ProfileImageSource => _user.GetProfileImageFile();

        private ObservableCollection<SearchedUser> _searchedUsers = new ObservableCollection<SearchedUser>();

        public ObservableCollection<SearchedUser> SearchedUsers
        {
            get { return _searchedUsers; }
            set { _searchedUsers = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PostView> _posts = new ObservableCollection<PostView>();

        public ObservableCollection<PostView> Posts
        {
            get { return _posts; }
            set { _posts = value; OnPropertyChanged(); }
        }

        private object _twitterView;

        public object TwitterView
        {
            get { return _twitterView; }
            set { _twitterView = value; OnPropertyChanged(); }
        }

        private object _mainView = new TwitterView();

        public object MainView
        {
            get { return _mainView; }
            set { _mainView = value; OnPropertyChanged(); }
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

                    var posts = await _service.GetPosts(_user.ID);

                    foreach (var post in posts.ResponseData)
                    {
                        Posts.Add(new PostView(post));
                    }
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
                    var twitterView = new MakeTwitterView(_user);
                    PostView postView = null;

                    twitterView.MadeNewTwitter += newTwitter => {
                        postView = new PostView(newTwitter);
                        postView.IsEnabled = false;

                        Posts.Insert(0, postView);
                    };

                    twitterView.FailedUploadTwitter += async guid => {
                        await Task.Delay(2000);

                        var uploadFailedTwitter = Posts.Where(x => x.Guid == guid).FirstOrDefault() ;

                        Posts.Remove(uploadFailedTwitter);
                    };

                    twitterView.CompleteTwitter += () => {
                        TwitterResultView = new TwitterResultView();
                        postView?.CompleteUpload();
                    };

                    TwitterView = twitterView;
                });
            }
        }

        private ICommand _searchViewCommand;

        public ICommand SearchViewCommand
        {
            get
            {
                return GetRelayCommand(ref _searchViewCommand, _ =>
                {
                    MainView = new SearchView();
                });
            }
        }

        private ICommand _searchCommand;

        public ICommand SearchCommand
        {
            get
            {
                return GetRelayCommand(ref _searchCommand, async searchText =>
                {
                    var test = await _searchService.Search(_user.ID, searchText.ToString());

                    var data = test.ResponseData.ToObservableCollection();

                    int breakPointStopsHere = 0;
                });
            }
        }

        private ICommand _userSearchCommand;

        public ICommand UserSearchCommand
        {
            get
            {
                return GetRelayCommand(ref _userSearchCommand, async searchText =>
                {
                    var response = await _searchService.SearchUser(searchText.ToString());

                    SearchedUsers = response.ResponseData.ToObservableCollection();
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
                    MainView = new TwitterView();
                });
            }
        }
        #endregion


        public MainVM(User user)
        {
            if (user.IsNull())
                throw new InvalidOperationException("로그인 된 user는 null일 수 없습니다.");

            _user = user;
            _service = new PostService();
            _searchService = new SearchService();

            LoadedEventCommand.Execute(null);
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


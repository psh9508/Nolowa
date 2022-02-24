using Microsoft.VisualBasic.ApplicationServices;
using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Servicies;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class ProfileVM :ViewModelBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        private Models.User _user;

        public Models.User User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Post> _posts;

        public ObservableCollection<Post> Posts
        {
            get { return _posts; }
            set { _posts = value; OnPropertyChanged(); }
        }

        private bool _isFollowButtonVisible;

        public bool IsFollowButtonVisible
        {
            get { return _isFollowButtonVisible; }
            set { _isFollowButtonVisible = value; OnPropertyChanged(); }
        }

        #region Commands
        private ICommand _followButtonClickCommand;

        public ICommand FollowButtonClickCommand
        {
            get
            {
                return GetRelayCommand(ref _followButtonClickCommand, async _ =>
                {
                    // _userService를 이용해 팔로우 API 호출
                });
            }
        }

        private ICommand _loadedCommand;

        public ICommand LoadedCommand
        {
            get
            {
                return GetRelayCommand(ref _loadedCommand, async _ =>
                {
                    Posts = new ObservableCollection<Post>();

                    if (User.IsNotNull())
                    {
                        var postsResponse = await _postService.GetMyPostsAsync(User.ID);

                        if (postsResponse.IsSuccess)
                            Posts = postsResponse.ResponseData.ToObservableCollection();
                    }
                });
            }
        }

        private ICommand _closeViewCommand;

        public ICommand CloseViewCommand
        {
            get
            {
                return GetRelayCommand(ref _closeViewCommand, async _ =>
                {
                    //Visibility = Visibility.Collapsed;
                });
            }
        }
        #endregion

        public ProfileVM()
        {
            _postService = new PostService();
            _userService = new UserService();
        }

        public ProfileVM(Models.User user) : this()
        {
            User = user;
        }
    }
}

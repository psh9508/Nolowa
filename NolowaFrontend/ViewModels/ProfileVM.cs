using Microsoft.VisualBasic.ApplicationServices;
using NolowaFrontend.Controls.Buttons;
using NolowaFrontend.Core;
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
        
        private eFollowButtonState _followButtonState = eFollowButtonState.Following;

        public eFollowButtonState FollowButtonState
        {
            get { return _followButtonState; }
            set { _followButtonState = value; OnPropertyChanged(); }
        }

        private bool _isVisible = true;

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; OnPropertyChanged(); }
        }

        private object _editView;

        public object EditView
        {
            get { return _editView; }
            set { _editView = value; OnPropertyChanged(); }
        }

        #region Commands
        private ICommand _followButtonClickCommand;

        public ICommand FollowButtonClickCommand
        {
            get
            {
                return GetRelayCommand(ref _followButtonClickCommand, async _ =>
                {
                    if(FollowButtonState == eFollowButtonState.Editable)
                    {
                        var editViewVM = new UpdateProfileVM(_user);
                        editViewVM.CompleteHide += () => {
                            EditView = null; // View를 다시 보여주기 위해서
                        };

                        EditView = editViewVM;
                    }
                    else
                    {
                        var nowState = FollowButtonState;
                        // _userService를 이용해 팔로우 API 호출
                        var response = await _userService.FollowAsync(long.Parse(AppConfiguration.LoginUser.USN), long.Parse(_user.USN));

                        if (response.IsNotNull())
                        {
                            ToggleFollowButtonState(response);

                            // Post 재로드
                        }
                        else
                        {
                            // 실패 처리
                            FollowButtonState = nowState;
                        }
                    }
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
                        var postsResponse = await _postService.GetMyPostsAsync(long.Parse(User.USN));

                        if (postsResponse.IsSuccess)
                            Posts = postsResponse.ResponseData.ToObservableCollection();
                    }
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

            SetFollowButtonState();
        }

        private void SetFollowButtonState()
        {
            if (User.USN == AppConfiguration.LoginUser.USN)
            {
                FollowButtonState = eFollowButtonState.Editable;
            }
            else if (AppConfiguration.LoginUser.Followers.Any(x => x.Id == long.Parse(User.USN)))
            {
                FollowButtonState = eFollowButtonState.Following;
            }
            else
            {
                FollowButtonState = eFollowButtonState.Followed;
            }
        }

        private void ToggleFollowButtonState(Follower changedFromServerData)
        {
            if (FollowButtonState == eFollowButtonState.Followed)
            {
                FollowButtonState = eFollowButtonState.Following;
                AppConfiguration.LoginUser.Followers.Add(changedFromServerData);
            }
            else if (FollowButtonState == eFollowButtonState.Following)
            {
                FollowButtonState = eFollowButtonState.Followed;
                AppConfiguration.LoginUser.Followers.Remove(changedFromServerData);
            }
        }
    }
}

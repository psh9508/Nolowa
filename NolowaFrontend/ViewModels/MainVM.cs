﻿using NolowaFrontend.Models;
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
using Microsoft.AspNetCore.SignalR.Client;

namespace NolowaFrontend.ViewModels
{
    public class MainVM : ViewModelBase
    {
        private readonly User _user;
        private readonly IPostService _service;
        private readonly ISearchService _searchService;
        private readonly IDirectMessageService _directMessageService;
        private readonly SearchView _searchView;
        private readonly TwitterVM _twitterVM;

        public string ProfileImageSource => _user.ProfileImageFile;
        public User User => _user;

        private int _unreadMessageCount;

        public int UnreadMessageCount
        {
            get { return _unreadMessageCount; }
            set { _unreadMessageCount = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PostView> _posts = new ObservableCollection<PostView>();

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

        private ProfileVM _profileViewModel;

        public ProfileVM ProfileViewModel
        {
            get { return _profileViewModel; }
            set { _profileViewModel = value; OnPropertyChanged(); }
        }

        private DirectMessageVM _directMessageViewModel;

        public DirectMessageVM DirectMessageViewModel
        {
            get { return _directMessageViewModel; }
            set { _directMessageViewModel = value; OnPropertyChanged(); }
        }

        private DirectMessageSendVM _directMessageSendViewModel;

        public DirectMessageSendVM DirectMessageSendViewModel
        {
            get { return _directMessageSendViewModel; }
            set { _directMessageSendViewModel = value; OnPropertyChanged(); }
        }

        private DirectMessageReceiverSelectVM _directMessageReceiverSelectViewModel;

        public DirectMessageReceiverSelectVM DirectMessageReceiverSelectViewModel
        {
            get { return _directMessageReceiverSelectViewModel; }
            set { _directMessageReceiverSelectViewModel = value; OnPropertyChanged(); }
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
                    //await LoadPostsAsync();
                    await SetUnreadMessageCount();

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

                    makeTwitterView.MadeNewTwitter += newTwitter =>
                    {
                        postView = new PostView(newTwitter);
                        postView.IsEnabled = false;

                        _twitterVM.InsertPost(postView);
                    };

                    makeTwitterView.FailedUploadTwitter += async guid =>
                    {
                        await Task.Delay(2000);

                        _twitterVM.RemovePost(guid);
                    };

                    makeTwitterView.CompleteTwitter += () =>
                    {
                        TwitterResultView = new TwitterResultView();
                        postView?.CompleteUpload();
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
                    #region 이전 로직
                    //var twitterView = new TwitterView();
                    //twitterView.ClickedProfileImage += (object sender, RoutedEventArgs e) =>
                    //{
                    //    // VM에서 이벤트를 연결하는게 좋은건가 xaml에서 이벤트를 연결하는게 좋은건가?

                    //    if (e is ObjectRoutedEventArgs args)
                    //    {
                    //        var clickedUser = (User)args.Parameter;
                    //        var profileVM = new ProfileVM(clickedUser);
                    //        profileVM.CompleteHide += () => {
                    //            ProfileViewModel = null;
                    //        };

                    //        ProfileViewModel = profileVM;
                    //    }
                    //};

                    //twitterView.ReloadCommand = new RelayCommand(async _ =>
                    //{
                    //    Posts.Clear();
                    //    await LoadPostsAsync();
                    //});

                    //MainView = twitterView; 
                    #endregion
                    MainView = _twitterVM;
                });
            }
        }

        private ICommand _directMessageCommand;

        public ICommand DirectMessageCommand
        {
            get
            {
                return GetRelayCommand(ref _directMessageCommand, _ => {

                    var directMessageVM = new DirectMessageVM();

                    directMessageVM.SelectDialog += user => {
                        var directMessageSendVM = new DirectMessageSendVM(user);

                        directMessageSendVM.CompleteHide += () => {
                            DirectMessageSendViewModel = null;
                        };

                        directMessageSendVM.ClickBackButton += async userId => {
                            directMessageVM.RemoveNewMessageCount(userId);
                            //UnreadMessageCount -= readMessageCount;
                            await SetUnreadMessageCount();
                        };

                        DirectMessageSendViewModel = directMessageSendVM;
                    };

                    MainView = directMessageVM;
                });
            }
        }

        private ICommand _directMessageSendViewCommand;

        public ICommand DirectMessageSendViewCommand
        {
            get
            {
                return GetRelayCommand(ref _directMessageSendViewCommand, text =>
                {
                    var directMessageReceiverSelectVM = new DirectMessageReceiverSelectVM();

                    directMessageReceiverSelectVM.CompleteHide += () => {
                        DirectMessageReceiverSelectViewModel = null;
                    };

                    directMessageReceiverSelectVM.ClickNextCommand += user => {
                        var directMessageSendVM = new DirectMessageSendVM(user);

                        directMessageSendVM.CompleteHide += () => {
                            DirectMessageSendViewModel = null;
                        };

                        DirectMessageSendViewModel = directMessageSendVM;
                    };

                    DirectMessageReceiverSelectViewModel = directMessageReceiverSelectVM;
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

            _ = NolowaHubConnection.Instance.InitializeAsync();

            NolowaHubConnection.Instance.OnReceiveDirectMessage += (long senderId, long receiveId, string message, string time) => {
                if (senderId != receiveId && receiveId == long.Parse(AppConfiguration.LoginUser.USN)) // 내게 보낸 DM이 아니고 받는 사람이 나일 때만 올려준다.
                    UnreadMessageCount += 1;
            };

            NolowaHubConnection.Instance.OnReadMessage += (int unreadMessageCount) => {
                UnreadMessageCount = unreadMessageCount;
            };

            _user = user;
            _service = new PostService();
            _searchService = new SearchService();
            _directMessageService = new DirectMessageService();

            _twitterVM = new TwitterVM(_user);

            _searchView = new SearchView();
            _searchView.ClickedProfileImage += (object sender, RoutedEventArgs e) =>
            {
                // 계속 이렇게 추가해야 하는건가?
                if (e is ObjectRoutedEventArgs args)
                {
                    var clickedUser = (User)args.Parameter;
                    var profileVM = new ProfileVM(clickedUser);
                    profileVM.CompleteHide += () => {
                        ProfileViewModel = null;
                    };

                    ProfileViewModel = profileVM;
                }
            };

            LoadedEventCommand.Execute(null);
        }

        public async void OnTimerSearch(object sender, StringRoutedEventArgs e)
        {
            await _searchView.TimerSearchAsync(e.Parameter);
        }

        public void OnEnterSearch(object sender, StringRoutedEventArgs e)
        {
        }

        public void ShowProfileView(object sender, RoutedEventArgs e)
        {
            if (e is ObjectRoutedEventArgs args)
            {
                var clickedUser = (User)args.Parameter;
                var profileVM = new ProfileVM(clickedUser);
                profileVM.CompleteHide += () => {
                    ProfileViewModel = null;
                };

                ProfileViewModel = profileVM;
            }
        }

        //private async Task LoadPostsAsync()
        //{
        //    var posts = await _service.GetPostsAsync(_user.Id);

        //    if (posts.ResponseData.Count() > 0)
        //        Posts.AddRange(posts.ResponseData.Select(x => new PostView(x)));
        //}

        private async Task SetUnreadMessageCount()
        {
            //UnreadMessageCount = await _directMessageService.GetUnreadMessageCount(AppConfiguration.LoginUser.Id);
        }

        private async Task CachingProfileImageFileToLocal()
        {
            await Task.Run(() =>
            {
                if (_user.ProfileInfo.ProfileImage.IsNull())
                    return;

                // No need to caching a file when it's a default profile image file.
                if (File.Exists(_user.ProfileInfo.ProfileImage.URL) == false)
                {
                    // LOG
                    return;
                }

                var profileImageCachingFullPath = Constant.PROFILE_IMAGE_ROOT_PATH + _user.ProfileInfo.ProfileImage.Hash + ".jpg";

                if (File.Exists(profileImageCachingFullPath) == false)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(_user.ProfileInfo.ProfileImage.URL, profileImageCachingFullPath);
                    }
                }
            });
        }
    }
}


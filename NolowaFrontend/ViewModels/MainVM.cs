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

namespace NolowaFrontend.ViewModels
{
    public class MainVM : ViewModelBase
    {
        private readonly User _user;
        private readonly IPostService _service;

        private ObservableCollection<PostView> _posts = new ObservableCollection<PostView>();

        public ObservableCollection<PostView> Posts
        {
            get { return _posts; }
            set { _posts = value; OnPropertyChanged(); }
        }

        private object _tweetView;

        public object TweetView
        {
            get { return _tweetView; }
            set { _tweetView = value; OnPropertyChanged(); }
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
                        Posts.Add(new PostView()
                        {
                            Name = post.Name,
                            UserAccountID = post.UserAccountId,
                            UserID = post.UserID.ToString(),
                            Message = post.Message,
                            ElapsedTime = GetElapsedTime(post.UploadedDate),
                            //ProfileImageSource = $"{Constant.PROFILE_IMAGE_ROOT_PATH}{post.PostedUser.ProfileImage?.Hash}.jpg",
                            ProfileImageSource = post.PostedUser.ProfileImage.IsNull() ? @"~\..\Resources\ProfilePicture.jpg" : $"{Constant.PROFILE_IMAGE_ROOT_PATH}{post.PostedUser.ProfileImage?.Hash}.jpg",
                        });
                    }
                });
            }
        }

        private ICommand _tweetCommand;

        public ICommand TweetCommand
        {
            get
            {
                return GetRelayCommand(ref _tweetCommand, _ =>
                {
                    // 미리 폼을 만들어 놓고 사용하는게 성능에 좋지 않을까?
                    TweetView = new TweetView();
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

            LoadedEventCommand.Execute(null);
        }

        private async Task CachingProfileImageFileToLocal()
        {
            await Task.Run(() =>
            {
                if (_user.ProfileImage.IsNull())
                    return;

                string profileImageCachingFullPath = _user.ProfileImage.URL;

                if (File.Exists(profileImageCachingFullPath) == false)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(_user.ProfileImage.URL, profileImageCachingFullPath);
                    }
                }
            });
        }

        private string GetElapsedTime(DateTime creadtedTime)
        {
            var timeSpan = DateTime.Now - creadtedTime;

            if(timeSpan.Days == 0 && timeSpan.Hours == 0 && timeSpan.Minutes == 0 && timeSpan.Seconds != 0)
            {
                return timeSpan.Seconds + "초";
            }
            else if(timeSpan.Days == 0 && timeSpan.Hours == 0 && timeSpan.Minutes != 0)
            {
                return timeSpan.Minutes + "분";
            }
            else if(timeSpan.Days == 0 && timeSpan.Hours != 0)
            {
                return timeSpan.Hours + "시";
            }
            else if(timeSpan.Days != 0)
            {
                if (timeSpan.Days >= 999)
                    return "999일";

                return timeSpan.Days + "일";
            }
            else
            {
                return "1초";
            }
        }
    }
}


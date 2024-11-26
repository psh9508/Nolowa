using NolowaFrontend.Core;
using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
using NolowaFrontend.Models.Events;
using NolowaFrontend.Servicies;
using NolowaFrontend.ViewModels.Base;
using NolowaFrontend.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class UploadFailException : Exception
    {
        
    }

    public class TwitterVM : ViewModelBase
    {
        public event Action<Post> MadeNewTwitter;
        public event Action<Guid> FailedUploadTwitter;
        public event Action<ResponseBaseEntity<Post>> UploadedTwitter;

        private readonly User _user;
        private readonly IPostService _postService;
        private PostView _listPostItemView;

        private int _nowPage = 1;
        private bool _isLoading;

        #region Props
        public User User => _user;

        public bool HideComplete { get; set; }

        private string _profileImageSource = string.Empty;

        public string ProfileImageSource
        {
            get { return _profileImageSource; }
            set { _profileImageSource = value; OnPropertyChanged(); }
        }

        private bool _isHide = false;

        public bool IsHide
        {
            get { return _isHide; }
            set { _isHide = value; OnPropertyChanged(); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PostView> _posts = new ObservableCollection<PostView>();

        public ObservableCollection<PostView> Posts
        {
            get { return _posts; }
            set { _posts = value; OnPropertyChanged(); }
        }

        private bool _isPostLoading;

        public bool IsPostLoading
        {
            get { return _isPostLoading; }
            set { _isPostLoading = value; OnPropertyChanged(); }
        }
        #endregion

        #region ICommands
        private ICommand _scrollCommand;
        public ICommand ScrollCommand
        {
            get { return _scrollCommand ?? (_scrollCommand = new RelayCommand(Scroll)); }
        }

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                return GetRelayCommand(ref _closeCommand, _ =>
                {
                    IsHide = true;
                });
            }
        }

        private ICommand _reloadCommand;

        public ICommand ReloadCommand
        {
            get
            {
                return GetRelayCommand(ref _loadedCommand, async _ =>
                {
                    await LoadPostsAsync();
                });
            }
        }


        private ICommand _makeTwitterCommand;

        public ICommand MakeTwitterCommand
        {
            get
            {
                return GetRelayCommand(ref _makeTwitterCommand, async _ =>  
                {
                    if (Message.IsNotVaild())
                        return;

                    IsHide = true;
                    var guid = Guid.NewGuid();

                    try
                    {
                        var newTwitter = new Post()
                        {
                            Message = Message,
                            Name = _user.AccountName,
                            PostedUser = _user,
                            Guid = guid,
                        };

                        MadeNewTwitter?.Invoke(newTwitter);

                        var response = await _postService.InsertPostAsync(newTwitter);

                        if (response.IsSuccess == false)
                            throw new UploadFailException();

                        await Task.Delay(3000);

                        var uploadResult = await WaitUntilHideCompleteAsync();

                        if (uploadResult == false)
                            throw new UploadFailException();

                        UploadedTwitter?.Invoke(response);
                    }
                    catch (Exception ex)
                    {
                        FailedUploadTwitter?.Invoke(guid);
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
                    await LoadPostsAsync();
                });
            }
        }
        #endregion

        public TwitterVM(User user)
        {
            _user = user;
            _postService = new PostService();
            _listPostItemView = new PostView(null);
        }

        public void InsertPost(PostView postView)
        {

        }

        private async Task<bool> WaitUntilHideCompleteAsync()
        {
            const int TIME_OUT = 5000;

            return await Task.Run(() =>
            {
                var timer = new Stopwatch();
                timer.Start();

                do
                {
                    if (timer.ElapsedMilliseconds >= TIME_OUT)
                        return false;

                    Task.Delay(100);

                } while (HideComplete != true);

                return true;
            });
        }

        private async Task LoadPostsAsync()
        {
            var reponsePosts = await _postService.GetHomePosts(long.Parse(_user.USN));

            AddPosts(reponsePosts.ResponseData);
        }

        private async void Scroll(object parameter)
        {
            var scrollChangedEventArgs = parameter as NolowaScrollCahngedEventArgs;
            if (scrollChangedEventArgs != null)
            {
                if (scrollChangedEventArgs.VerticalOffset <= 0)
                {
                    //_isScrollbarOnTop = true;
                }
                else
                {
                    if (scrollChangedEventArgs.VerticalChange > 0 && 
                        (scrollChangedEventArgs.VerticalOffset == scrollChangedEventArgs.ScrollableHeight))
                    {
                        if (_isLoading)
                            return;

                        try
                        {
                            _isLoading = true;

                            if (_listPostItemView.IsNotNull())
                                _listPostItemView.IsPostLoading = true;

                            var responsePosts = await _postService.GetPostsAsync(long.Parse(_user.USN), ++_nowPage);

                            AddPosts(responsePosts.ResponseData);
                        }
                        finally
                        {
                            _isLoading = false;
                        }
                    }
                }
            }
        }

        private void AddPosts(IEnumerable<Post> addedPosts)
        {
            if (addedPosts.IsNull())
            {
                if (_listPostItemView.IsNull())
                    return;

                if (Posts.Count > 0)
                    Posts.RemoveAt(Posts.Count() - 1); // 가장 마지막에 있는 로딩 이미지를 지우고

                _listPostItemView = null;
                // 모든 포스트를 다 가져왔습니다.
                return;
            }

            if (Posts.Count > 0)
                Posts.RemoveAt(Posts.Count() - 1); // 가장 마지막에 있는 로딩 이미지를 지우고

            Posts.AddRange(addedPosts.Select(x => new PostView(x)));

            if (_listPostItemView.IsNotNull())
                _listPostItemView.IsPostLoading = false;

            Posts.Add(_listPostItemView); // 가장 마지막에 로딩 이미지를 가지고 있는 빈 데이터를 넣어준다.
        }
    }
}

using NolowaFrontend.Core;
using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
using NolowaFrontend.Servicies;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class TwitterVM : ViewModelBase
    {
        public event Action<Post> MadeNewTwitter;
        public event Action<ResponseBaseEntity<Post>> UploadedTwitter;

        private readonly User _user;
        private readonly IPostService _postService;

        #region Props
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

        #endregion

        #region ICommands
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

                    var newTwitter = new Post()
                    {
                        Message = Message,
                        Name = _user.Name,
                        PostedUser = _user,
                    };

                    MadeNewTwitter?.Invoke(newTwitter);

                    var response = await _postService.InsertPost(newTwitter);

                    if (response.IsSuccess == false)
                    {

                    }

                    await Task.Delay(3000);

                    await WaitUntilHideCompleteAsync();

                    UploadedTwitter?.Invoke(response);

                });
            }
           
        }
        #endregion

        public TwitterVM(User user)
        {
            _user = user;
            _postService = new PostService();

            if (_user.ProfileImage?.Hash.IsValid() == true)
                ProfileImageSource = Constant.PROFILE_IMAGE_ROOT_PATH + _user.ProfileImage.Hash + ".jpg";
        }

        private async Task WaitUntilHideCompleteAsync()
        {
            const int TIME_OUT = 2000;

            await Task.Run(() =>
            {
                var timer = new Stopwatch();
                timer.Start();

                do
                {
                    if (timer.ElapsedMilliseconds >= TIME_OUT)
                        break;

                } while (HideComplete != true);
            });
        }
    }
}

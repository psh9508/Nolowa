using NolowaFrontend.Core;
using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class TweetVM : ViewModelBase
    {
        private readonly User _user;

        #region Props
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
        #endregion

        public TweetVM(User user)
        {
            _user = user;

            if(_user.ProfileImage?.Hash.IsValid() == true)
                ProfileImageSource = Constant.PROFILE_IMAGE_ROOT_PATH + _user.ProfileImage.Hash + ".jpg";
        }
    }
}

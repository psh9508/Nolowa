using NolowaFrontend.Models;
using NolowaFrontend.Servicies;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class UpdateProfileVM : ViewModelBase
    {
        public event Action CompleteHide;

        private readonly IUserService _userService;

        #region Props
        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        private bool _isHide = false;

        public bool IsHide
        {
            get { return _isHide; }
            set { _isHide = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
        private ICommand _backButtonCommand;

        public ICommand BackButtonCommand
        {
            get
            {
                return GetRelayCommand(ref _backButtonCommand, _ =>
                {
                    IsHide = true;
                });
            }
        }

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return GetRelayCommand(ref _saveCommand, async _ =>
                {
                    await _userService.ChangeProfileInfoAsync(_user.ProfileInfo);
                });
            }
        }

        private ICommand _completeHideAnimation;

        public ICommand CompleteHideAnimation
        {
            get
            {
                return GetRelayCommand(ref _completeHideAnimation, _ =>
                {
                    CompleteHide?.Invoke();
                });
            }
        }
        #endregion

        public UpdateProfileVM() 
        {
            _userService = new UserService();
        }

        public UpdateProfileVM(User user) : this()
        {
            User = user; // It should be copied
        }
    }
}

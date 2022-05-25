using NolowaFrontend.Models;
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
        #endregion


        public UpdateProfileVM() 
        { 
        }

        public UpdateProfileVM(User user)
        {
            User = user; // It should be copied
        }
    }
}

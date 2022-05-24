using NolowaFrontend.Models;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.ViewModels
{
    public class UpdateProfileVM : ViewModelBase
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        public UpdateProfileVM() 
        { 
        }

        public UpdateProfileVM(User user)
        {
            User = user; // It should be copied
        }
    }
}

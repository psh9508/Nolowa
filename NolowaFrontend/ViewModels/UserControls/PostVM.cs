using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.ViewModels.UserControls
{
    public class PostVM : ViewModelBase
    {
        private string _profileImageSource;

        public string ProfileImageSource
        {
            get { return _profileImageSource; }
            set
            {
                _profileImageSource = value;
                OnPropertyChanged();
            }
        }

        public PostVM()
        {
            ProfileImageSource = @"~\..\Resources\ProfilePicture.jpg";
        }
    }
}

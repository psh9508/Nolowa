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
    public class TweetVM : ViewModelBase
    {
        public event Action Close;

        private readonly User _user;

        #region Props
        private string _profileImageSource = string.Empty;

        public string ProfileImageSource
        {
            get { return _profileImageSource; }
            set { _profileImageSource = value; OnPropertyChanged(); }
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
                    Close?.Invoke();
                });
            }
        }
        #endregion

        public TweetVM()
        {
        }
    }
}

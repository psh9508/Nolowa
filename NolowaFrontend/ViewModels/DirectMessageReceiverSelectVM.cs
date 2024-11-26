using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Models.Events;
using NolowaFrontend.Servicies;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public  class DirectMessageReceiverSelectVM : ViewModelBase
    {
        private readonly ISearchService _searchService;

        public event Action<User> ClickNextCommand;

        #region Props
        private ObservableCollection<User> _searchedUsers;

        public ObservableCollection<User> SearchedUsers
        {
            get { return _searchedUsers; }
            set { _searchedUsers = value; OnPropertyChanged(); }
        }

        private bool _isHide = false;

        public bool IsHide
        {
            get { return _isHide; }
            set { _isHide = value; OnPropertyChanged(); }
        }

        private User _selectedUser;

        public User SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
        private ICommand _backButtonCommand;

        public ICommand BackButtonCommand
        {
            get
            {
                return GetRelayCommand(ref _backButtonCommand, async _ =>
                {
                    IsHide = true;
                });
            }
        }

        private ICommand _nextCommand;

        public ICommand NextCommand
        {
            get
            {
                return GetRelayCommand(ref _nextCommand, _ =>
                {
                    BackButtonCommand.Execute(null);
                    ClickNextCommand?.Invoke(SelectedUser);
                });
            }
        }
        #endregion

        public DirectMessageReceiverSelectVM()
        {
            _searchService = new SearchService();
        }

        public async void OnTimerSearch(object sender, StringRoutedEventArgs e)
        {
            if (e.Parameter.IsNotVaild())
            {
                SearchedUsers = new ObservableCollection<User>();
                return;
            }

            var response = await _searchService.SearchUser(e.Parameter);

            if (response.IsSuccess)
            {
                var convertedDatas = response.ResponseData.Select(x => new User()
                {
                    //Id = x.ID,
                    //AccountName = x.Name,
                    //UserId = x.UserId,
                    //ProfileInfo = new ProfileInfo()
                    //{
                    //    Message = x.ProfileInfo.Message,
                    //    BackgroundImage = x.ProfileInfo.BackgroundImage,
                    //    ProfileImage = x.ProfileInfo.ProfileImage,
                    //},
                });

                SearchedUsers = convertedDatas.ToObservableCollection();
            }
        }
    }
}

using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Servicies;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public  class DirectMessageReceiverSelectVM : ViewModelBase
    {
        private readonly ISearchService _searchService;

        #region Props
        private ObservableCollection<User> _searchedUsers;

        public ObservableCollection<User> SearchedUsers
        {
            get { return _searchedUsers; }
            set { _searchedUsers = value; OnPropertyChanged(); }
        }

        private string _inputText = string.Empty;

        public string InputText
        {
            get { return _inputText; }
            set { _inputText = value; OnPropertyChanged(); }
        }

        private bool _isHide = false;

        public bool IsHide
        {
            get { return _isHide; }
            set { _isHide = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
        private ICommand _inputTextChangedCommand;

        public ICommand InputTextChangedCommand
        {
            get
            {
                return GetRelayCommand(ref _inputTextChangedCommand, async _ =>
                {
                    if (InputText.IsNotVaild())
                    {
                        SearchedUsers = new ObservableCollection<User>();
                        return;
                    }

                    var response = await _searchService.SearchUser(InputText);

                    if (response.IsSuccess)
                    {
                        var convertedDatas = response.ResponseData.Select(x => new User()
                        {
                            Id = x.ID,
                            AccountName = x.Name,
                            UserId = x.UserId,
                            ProfileInfo = new ProfileInfo()
                            {
                                Message = x.ProfileInfo.Message,
                                BackgroundImage = x.ProfileInfo.BackgroundImage,
                                ProfileImage = x.ProfileInfo.ProfileImage,
                            },
                        });

                        SearchedUsers = convertedDatas.ToObservableCollection();
                    }
                });
            }
        }

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
        #endregion

        public DirectMessageReceiverSelectVM()
        {
            _searchService = new SearchService();
        }
    }
}

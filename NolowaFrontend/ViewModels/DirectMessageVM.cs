using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using NolowaFrontend.Core;
using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Servicies;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class PreviousDirectMessageDialogItem
    {
        [JsonPropertyName("account")]
        public User User { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int NewMessageCount { get; set; }
    }

    public class DirectMessageVM : ViewModelBase
    {
        public event Action<User, int> SelectDialog;
        
        private readonly IDirectMessageService _directMessageService;

        #region Props
        private DirectMessageSendVM _directMessageSendVM;

        public DirectMessageSendVM DirectMessageSendVM
        {
            get { return _directMessageSendVM; }
            set { _directMessageSendVM = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PreviousDirectMessageDialogItem> _previousDialogItems = new ObservableCollection<PreviousDirectMessageDialogItem>();

        public ObservableCollection<PreviousDirectMessageDialogItem> PreviousDialogItems
        {
            get { return _previousDialogItems; }
            set { _previousDialogItems = value; OnPropertyChanged(); }
        }

        private bool _isDataLoaded;

        public bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            set { _isDataLoaded = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands
        private ICommand _loadedCommand;

        public ICommand LoadedCommand
        {
            get
            {
                return GetRelayCommand(ref _loadedCommand, async _ => {
                    try
                    {
                        var response = await _directMessageService.GetPreviousDialogListAsync(AppConfiguration.LoginUser.Id);
                        PreviousDialogItems = response.ToObservableCollection();
                    }
                    finally
                    {
                        IsDataLoaded = true;
                    }
                });
            }
        }

        private ICommand _writeDirectMessageCommand;

        public ICommand WriteDirectMessageCommand
        {
            get
            {
                return GetRelayCommand(ref _writeDirectMessageCommand, _ => {
                    var dmSendVM = new DirectMessageSendVM();
                    dmSendVM.ClickBackButton += (receiverId, message) => {
                        PreviousDialogItems.Where(x => x.User.Id == receiverId)
                                           .Single()
                                           .NewMessageCount = 0;

                        PreviousDialogItems.Refresh();
                    };

                    DirectMessageSendVM = dmSendVM;
                });
            }
        }

        private ICommand _selectedItemChangedCommand;

        public ICommand SelectedItemChangedCommand
        {
            get
            {
                return GetRelayCommand(ref _selectedItemChangedCommand, selectedItem =>
                {
                    var viewItem = selectedItem as PreviousDirectMessageDialogItem;

                    if (viewItem.IsNull())
                        return;
                                         
                    if (SelectDialog.IsNotNull())
                    {
                        SelectDialog.Invoke(viewItem.User, viewItem.NewMessageCount);

                        viewItem.NewMessageCount = 0;

                        PreviousDialogItems.Refresh();
                    }
                });
            }
        }
        #endregion

        public DirectMessageVM()
        {
            _directMessageService = new DirectMessageService();

            NolowaHubConnection.Instance.OnReceiveDirectMessage += (long senderId, long receiveId, string message, string time) => 
            {
                var dialog = PreviousDialogItems.Where(x => (x.User.Id == receiveId || x.User.Id == senderId)
                                                          && x.User.Id != AppConfiguration.LoginUser.Id)
                                                .SingleOrDefault();
                if (dialog.IsNull())
                {
                    // 대화 추가
                }
                else
                {
                    dialog.Message = message;

                    if(senderId != AppConfiguration.LoginUser.Id)
                        dialog.NewMessageCount++;

                    PreviousDialogItems.Refresh();
                }
            };
        }

        public void Refresh(long receiverId, string message)
        {
            PreviousDialogItems.Where(x => x.User.Id == receiverId)
                               .Single()
                               .Message = message;

            PreviousDialogItems.Refresh();
        }
    }
}

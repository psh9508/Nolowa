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
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class PreviousDirectMessageDialogItem
    {
        [JsonPropertyName("account")]
        public User User { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class DirectMessageVM : ViewModelBase
    {
        private readonly ISignalRService _signalRService;

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

        #endregion

        #region Commands
        private ICommand _loadedCommand;

        public ICommand LoadedCommand
        {
            get
            {
                return GetRelayCommand(ref _loadedCommand, async _ => {
                    var response = await _signalRService.GetPreviousDialogListAsync(AppConfiguration.LoginUser.Id);
                    PreviousDialogItems = response.ToObservableCollection();
                });
            }
        }

        private ICommand _writeDirectMessageCommand;

        public ICommand WriteDirectMessageCommand
        {
            get
            {
                return GetRelayCommand(ref _writeDirectMessageCommand, _ => {
                    DirectMessageSendVM = new DirectMessageSendVM();
                });
            }
        }
        #endregion

        public DirectMessageVM()
        {
            _signalRService = new SignalRService();
        }
    }
}

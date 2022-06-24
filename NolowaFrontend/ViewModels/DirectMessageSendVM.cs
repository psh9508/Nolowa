using Microsoft.AspNetCore.SignalR.Client;
using NolowaFrontend.Core;
using NolowaFrontend.Extensions;
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
    public class DirectMessageSendVM : ViewModelBase
    {
        private readonly HubConnection _hubConnection;

        private ObservableCollection<string> _dialog = new ObservableCollection<string>();

        public ObservableCollection<string> Dialog
        {
            get { return _dialog; }
            set { _dialog = value; OnPropertyChanged(); }
        }

        private string _mseeage = string.Empty;

        public string Message
        {
            get { return _mseeage; }
            set { _mseeage = value; OnPropertyChanged();}
        }

        private bool _isHide = false;

        public bool IsHide
        {
            get { return _isHide; }
            set { _isHide = value; OnPropertyChanged(); }
        }

        private ICommand _sendDirectMessageCommand;

        public ICommand SendDirectMessageCommand
        {
            get
            {
                return GetRelayCommand(ref _sendDirectMessageCommand, async _ =>
                {
                    if (Message.IsNotVaild())
                        return;

                    if (_hubConnection.State != HubConnectionState.Connected)
                        await _hubConnection.StartAsync();

                    await _hubConnection.SendAsync("SendMessage", AppConfiguration.LoginUser.Id, 2, Message);
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

        public DirectMessageSendVM()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/DirectMessage")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On("ReceiveDirectMessage", (long userId, long receiveId, string message) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() => {
                    Dialog.Add($"{userId} : {receiveId}로 {message}");
                });
            });

            _hubConnection.StartAsync();
        }
    }
}

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

                    await _hubConnection.SendAsync("SendMessage", AppConfiguration.LoginUser.Id, Message);
                });
            }
        }

        public DirectMessageSendVM()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/DirectMessage")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On("ReceiveDirectMessage", (long userId, string message) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() => {
                    Dialog.Add($"{userId} : {message}");
                });
            });

            _hubConnection.StartAsync();
        }
    }
}

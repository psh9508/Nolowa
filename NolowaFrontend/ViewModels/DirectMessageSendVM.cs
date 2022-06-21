using Microsoft.AspNetCore.SignalR.Client;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class DirectMessageSendVM : ViewModelBase
    {
        private readonly HubConnection _hubConnection;

        private ICommand _sendDirectMessageCommand;

        public ICommand SendDirectMessageCommand
        {
            get
            {
                return GetRelayCommand(ref _sendDirectMessageCommand, async _ =>
                {
                    if (_hubConnection.State != HubConnectionState.Connected)
                        await _hubConnection.StartAsync();

                    await _hubConnection.SendAsync("SendMessage", "clientUser", "clientHello");
                });
            }
        }

        public DirectMessageSendVM()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/DirectMessage")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string, string>("ReceiveDirectMessage", (user, message) =>
            {

            });

            _hubConnection.StartAsync();
        }
    }
}

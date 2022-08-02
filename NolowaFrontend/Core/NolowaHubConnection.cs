using Microsoft.AspNetCore.SignalR.Client;
using NolowaFrontend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Core
{
    public class NolowaHubConnection 
    {
        private static readonly Lazy<NolowaHubConnection> _instance = new Lazy<NolowaHubConnection>(() => new NolowaHubConnection());
        public static NolowaHubConnection Instance { get { return _instance.Value; } }

        private event Action<long, long, string, string> _onReceiveDirectMessage;
        private event Action<int> _onReadMessage;

        private readonly HubConnection _hubConnection;
        private object _lockObject = new object();

        public event Action<long, long, string, string> OnReceiveDirectMessage
        {
            add
            {
                lock (_lockObject)
                {
                    _onReceiveDirectMessage -= value;
                    _onReceiveDirectMessage += value;
                }
            }
            remove
            {
                lock(_lockObject)
                {
                    _onReceiveDirectMessage -= value;
                }
            }
        }

        public event Action<int> OnReadMessage
        {
            add
            {
                lock (_lockObject)
                {
                    _onReadMessage -= value;
                    _onReadMessage += value;
                }
            }
            remove
            {
                lock (_lockObject)
                {
                    _onReadMessage -= value;
                }
            }
        }

        public HubConnectionState State
        {
            get { return _hubConnection.State; }
        }

        private NolowaHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/NolowaSocket")
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task InitializeAsync()
        {
            if (AppConfiguration.LoginUser.Id.IsNull())
                throw new InvalidOperationException("로그인 전에 호출 할 수 없습니다.");

            await _hubConnection.StartAsync();
            await _hubConnection.SendAsync("Login", AppConfiguration.LoginUser.Id);

            _hubConnection.On("ReceiveDirectMessage", (long senderId, long receiveId, string message, string time) => {
                _onReceiveDirectMessage.Invoke(senderId, receiveId, message, time);
            });

            _hubConnection.On("ReadMessage", (int unreadMessageCount) => {
                _onReadMessage.Invoke(unreadMessageCount);
            });
        }

        public async Task SendMessageAsync(long senderId, long receiverId, string message)
        {
            await _hubConnection.SendAsync("SendMessage", senderId, receiverId, message);
        }
    }
}

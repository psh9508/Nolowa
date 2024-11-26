using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using NolowaFrontend.Core;
using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Servicies;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class DirectMessageDialogItem
    {
        public long SenderId { get; set; }

        public long ReceiverId { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Time { get; set; } = string.Empty;

        public bool IsMine { get; set; }
    }

    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OthersTemplate { get; set; }
        public DataTemplate MineTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var message = (DirectMessageDialogItem)item;

            return message.IsMine ? MineTemplate : OthersTemplate;
        }
    }

    public class DirectMessageSendVM : ViewModelBase
    {
        public event Action<long> ClickBackButton;
        public event Action GetNewMessage;

        private readonly IDirectMessageService _directMessageService;

        public User Receiver { get; set; }

        private ObservableCollection<DirectMessageDialogItem> _dialog = new ObservableCollection<DirectMessageDialogItem>();

        public ObservableCollection<DirectMessageDialogItem> Dialog
        {
            get { return _dialog; }
            set { _dialog = value; OnPropertyChanged(); }
        }

        private string _mseeage = string.Empty;

        public string Message
        {
            get { return _mseeage; }
            set { _mseeage = value; OnPropertyChanged(); }
        }

        private bool _isHide = false;

        public bool IsHide
        {
            get { return _isHide; }
            set { _isHide = value; OnPropertyChanged(); }
        }

        #region Commands
        private ICommand _loadedCommand;

        public ICommand LoadedCommand
        {
            get
            {
                return GetRelayCommand(ref _loadedCommand, async _ =>
                {
                    Dialog.Clear();

                    var dialogResponse = await _directMessageService.GetDialog(long.Parse(AppConfiguration.LoginUser.USN), long.Parse(Receiver.USN));

                    if (dialogResponse.Count() > 0)
                    {
                        var dialogData = dialogResponse.Select(x => new DirectMessageDialogItem()
                        {
                            SenderId = x.SenderId,
                            ReceiverId = x.ReceiverId,
                            Message = x.Message,
                            Time = x.Time,
                            IsMine = x.SenderId == long.Parse(AppConfiguration.LoginUser.USN),
                        }).ToObservableCollection();

                        Dialog = dialogData;
                    }
                });
            }
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

                    string message = Message;
                    Message = string.Empty;

                    await NolowaHubConnection.Instance.SendMessageAsync(long.Parse(AppConfiguration.LoginUser.USN), long.Parse(Receiver.USN), message);
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
                    var readMessageCount = await _directMessageService.SetReadAllMessageAsync(long.Parse(AppConfiguration.LoginUser.USN), long.Parse(Receiver.USN));

                    ClickBackButton?.Invoke(long.Parse(Receiver.USN));

                    IsHide = true;
                });
            }
        }

        private ICommand _pressEnterKeyCommand;

        public ICommand PressEnterKeyCommand
        {
            get
            {
                return GetRelayCommand(ref _pressEnterKeyCommand, _ =>
                {
                    SendDirectMessageCommand.Execute(null);
                });
            }
        }
        #endregion

        public DirectMessageSendVM()
        {
            _directMessageService = new DirectMessageService();

            NolowaHubConnection.Instance.OnReceiveDirectMessage += (long senderId, long receiveId, string message, string time) => {
                Application.Current.Dispatcher.Invoke(() => {
                    Dialog.Add(new DirectMessageDialogItem()
                    {
                        SenderId = senderId,
                        ReceiverId = receiveId,
                        Message = message,
                        Time = time,
                        IsMine = senderId == long.Parse(AppConfiguration.LoginUser.USN),
                    });

                    GetNewMessage?.Invoke();
                });
            };
        }

        public DirectMessageSendVM(User receiver) : this()
        {
            Receiver = receiver;
        }
    }
}

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
        public event Action<long, string> ClickBackButton;

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

                    var dialogResponse = await _directMessageService.GetDialog(AppConfiguration.LoginUser.Id, Receiver.Id);

                    if (dialogResponse.Count() > 0)
                    {
                        var dialogData = dialogResponse.Select(x => new DirectMessageDialogItem()
                        {
                            SenderId = x.SenderId,
                            ReceiverId = x.ReceiverId,
                            Message = x.Message,
                            Time = x.Time,
                            IsMine = x.SenderId == AppConfiguration.LoginUser.Id,
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

                    await NolowaHubConnection.Instance.SendMessageAsync(AppConfiguration.LoginUser.Id, Receiver.Id, message);
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
                        IsMine = senderId == AppConfiguration.LoginUser.Id,
                    });
                });
            };
        }

        public DirectMessageSendVM(User receiver) : this()
        {
            Receiver = receiver;
        }
    }
}

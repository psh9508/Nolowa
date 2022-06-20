using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class DirectMessageVM : ViewModelBase
    {
        #region Props
        private DirectMessageSendVM _directMessageSendVM;

        public DirectMessageSendVM DirectMessageSendVM
        {
            get { return _directMessageSendVM; }
            set { _directMessageSendVM = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
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
    }
}

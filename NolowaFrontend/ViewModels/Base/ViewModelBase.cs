using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event Action CompleteHide;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        protected ICommand GetRelayCommand(ref ICommand prop, Action<object> action)
        {
            if (prop == null)
                prop = new RelayCommand(action);

            return prop;
        }

        private ICommand _completeHideAnimation;

        /// <summary>
        /// 폼이 사라지는 애니메이션이 끝났을 때 호출되는 event를 호출하는 Command
        /// </summary>
        public ICommand CompleteHideAnimation
        {
            get
            {
                return GetRelayCommand(ref _completeHideAnimation, _ =>
                {
                    CompleteHide?.Invoke();
                });
            }
        }
    }
}

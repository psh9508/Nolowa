using NolowaFrontend.Models;
using NolowaFrontend.Servicies;
using NolowaFrontend.ViewModels.Base;
using NolowaFrontend.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        public event Action<User> SuccessLogin;
        public event Action FailLogin;

        private readonly IAuthenticationService _service;

        #region Props
        private bool _isLogining;

        public bool IsLogining
        {
            get { return _isLogining; }
            set { _isLogining = value; OnPropertyChanged(); }
        }

        private bool _isLoginFailed;

        public bool IsLoginFailed
        {
            get { return _isLoginFailed; }
            set { _isLoginFailed = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands
        private ICommand loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                return GetRelayCommand(ref loginCommand, async x =>
                {
                    try
                    {
                        IsLogining = true;

                        var args = (object[])x;

                        var email = (string)args[0];
                        var password = (string)args[1];

                        var response = await _service.Login(email, password);

                        if (response?.IsSuccess == true)
                            SuccessLogin?.Invoke(response.ResponseData);
                        else
                        {
                            IsLoginFailed = true;
                            FailLogin?.Invoke();
                        }
                    }
                    finally
                    {
                        IsLogining = false;
                        IsLoginFailed = false;
                    }
                });
            }
        }         
        #endregion

        public LoginVM()
        {
            _service = new AuthenticationService();
        }
    }
}

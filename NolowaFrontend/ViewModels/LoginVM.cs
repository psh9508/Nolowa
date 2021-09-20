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


        private ICommand loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                return GetRelayCommand(ref loginCommand, async x => {
                    var args = (object[])x;

                    var email = (string)args[0];
                    var password = (string)args[1];

                    var response = await _service.Login(email, password);

                    if (response?.ResponseData != null)
                        SuccessLogin?.Invoke(response.ResponseData);
                    else
                        FailLogin?.Invoke();
                });
            }
        }        

        public LoginVM()
        {
            _service = new AuthenticationService();
        }
    }
}

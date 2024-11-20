using NolowaFrontend.Core;
using NolowaFrontend.Core.SNSLogin;
using NolowaFrontend.Models;
using NolowaFrontend.Models.IF;
using NolowaFrontend.Servicies;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Windows;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        public event Action<User> SuccessLogin;
        public event Action FailLogin;

        private readonly IAuthenticationService _service;
        private readonly IPostService _postService;

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

        private Visibility _signupViewVisibility = Visibility.Hidden;

        public Visibility SignupViewVisibility
        {
            get { return _signupViewVisibility; }
            set { _signupViewVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _loginViewVisibility = Visibility.Visible;

        public Visibility LoginViewVisibility
        {
            get { return _loginViewVisibility; }
            set { _loginViewVisibility = value; OnPropertyChanged(); }
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

                        // protobuf 사용 안함
                        //var loginReq = new LoginReq()
                        //{
                        //    Email = (string)args[0],
                        //    PlainPassword = (string)args[1],
                        //};

                        var loginReq = new LoginReq()
                        {
                            Id = (string)args[0],
                            Password = (string)args[1]
                        };

                        var response = await _service.Login(loginReq);

                        if (response?.IsSuccess == true)
                            SuccessLogin?.Invoke(response.ResponseData);
                        else
                        {
                            IsLoginFailed = true;
                            FailLogin?.Invoke();
                        }

                        //var message = new LoginMessage()
                        //{
                        //    Id = (string)args[0],
                        //    Password = (string)args[1],
                        //    Origin = $"frontend:{AppConfiguration.QueueName}",
                        //    Source = "frontend",
                        //    Target = MessageDestination.GATEWAY,
                        //    Destination = MessageDestination.SERVER,
                        //    Function = "LoginMessage",
                        //};

                        //_messageQueueService.SendMessage(message);
                    }
                    finally
                    {
                        IsLogining = false;
                        IsLoginFailed = false;
                    }
                });
            }
        }

        private ICommand _googleLoginCommand;

        public ICommand GoogleLoginCommand
        {
            get
            {
                return GetRelayCommand(ref _googleLoginCommand, _ =>
                {
                    var googleLoginProvider = new GoogleLoginProvider();
                    googleLoginProvider.ShowLoginPage();
                });
            }
        }

        private ICommand _kakaoLoginCommand;

        public ICommand KakaoLoginCommand
        {
            get
            {
                return GetRelayCommand(ref _kakaoLoginCommand, _ =>
                {
                    var googleLoginProvider = new KakaoLoginProvider();
                    googleLoginProvider.ShowLoginPage();
                });
            }
        }

        public ICommand _singupCommand;

        public ICommand SignupCommand
        {
            get
            {
                return GetRelayCommand(ref _singupCommand, _ =>
                {
                    ToggleSignupVisibility();
                    ToggleLoginVisibility();
                });
            }
        }
        #endregion

        public LoginVM()
        {
            AppConfiguration.QueueName = "Client_" + Guid.NewGuid().ToString();

            _service = new AuthenticationService();
            _postService = new PostService();
        }

        private void ToggleSignupVisibility()
        {
            if (SignupViewVisibility == Visibility.Hidden)
                SignupViewVisibility = Visibility.Visible;
            else
                SignupViewVisibility = Visibility.Hidden;
        }

        public void ToggleLoginVisibility()
        {
            if (LoginViewVisibility == Visibility.Hidden)
                LoginViewVisibility = Visibility.Visible;
            else
                LoginViewVisibility = Visibility.Hidden;
        }
    }
}

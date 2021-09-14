﻿using NolowaFrontend.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NolowaFrontend
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();

            loginView.SuccessLogin += user => {
                loginView.Hide();

                var main = new MainView(user);
                main.ShowDialog();
            };

            loginView.ShowDialog();
        }
    }
}

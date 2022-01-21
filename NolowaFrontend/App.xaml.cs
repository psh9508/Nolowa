using NolowaFrontend.Core;
using NolowaFrontend.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
            IocKernel.Initialize(new IocConfiguration());

            DownloadDefaultProfileImageFile();

            var loginView = new LoginView();

            loginView.SuccessLogin += user => {
                loginView.Hide();

                var main = new MainView(user);
                main.ShowDialog();
            };

            loginView.ShowDialog();
        }

        private void DownloadDefaultProfileImageFile()
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"NolowaFrontend.Resources.ProfilePicture.jpg"))
            using (var file = File.OpenWrite(Constant.DEFAULT_PROFILE_IMAGE_FULL_PATH))
            {
                var buffer = new byte[1024];
                int len;
                while ((len = resource.Read(buffer, 0, buffer.Length)) > 0)
                {
                    file.Write(buffer, 0, len);
                }
            }
        }
    }
}

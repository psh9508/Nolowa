using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NolowaFrontend.Controls
{
    /// <summary>
    /// SignUpControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignUpControl : UserControl
    {
        public event RoutedEventHandler ClickedCancel;
        public event RoutedEventHandler CompleteSignup;

        private const string CANCLE_ANIMATION = "CancleAnimation";
        private const string CONFIRM_ANIMATION = "ConfirmAnimation";

        public SignUpControl()
        {
            InitializeComponent();
        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard hideAnimation = (Storyboard)this.Resources[CANCLE_ANIMATION];
            hideAnimation.Completed += (sender, e) => {
                txtName.ClearText();
                txtEmail.ClearText();
                txtPassword.ClearText();
                txtPasswordValidation.ClearText();
                this.Visibility = Visibility.Hidden;
            };

            ClickedCancel?.Invoke(sender, e);
            hideAnimation.Begin();
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard hideAnimation = (Storyboard)this.Resources[CONFIRM_ANIMATION];
            hideAnimation.Completed += (_, _) => {
                this.Visibility = Visibility.Hidden;
            };

            CompleteSignup?.Invoke(sender, e);
            hideAnimation.Begin();
        }
    }
}

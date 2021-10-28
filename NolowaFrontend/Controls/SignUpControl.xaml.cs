using NolowaFrontend.Extensions;
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
            if (ValidateInputText() == false)
                return;

            if (ValidatePassword() == false)
            {
                // Message
                return;
            }

            Storyboard hideAnimation = (Storyboard)this.Resources[CONFIRM_ANIMATION];
            hideAnimation.Completed += (_, _) => {
                this.Visibility = Visibility.Hidden;
            };

            CompleteSignup?.Invoke(sender, e);
            hideAnimation.Begin();
        }

        private bool ValidateInputText()
        {
            if (txtName.InputText.IsNotVaild() 
                || txtEmail.InputText.IsNotVaild()
                || txtPassword.InputText.IsNotVaild()
                || txtPasswordValidation.InputText.IsNotVaild())
                return false;

            return true;
        }

        private bool ValidatePassword()
        {
            if (txtPassword.InputText != txtPasswordValidation.InputText)
                return false;

            return true;
        }

        private void ProfileImageButton_Click(object sender, RoutedEventArgs e)
        {
            using (var openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files(*.JPG)|*.JPG|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var filePath = openFileDialog.FileName;
                    SetProfileImage(filePath);

                    var fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
            }
        }

        private void SetProfileImage(string filePath)
        {          
            ImageBrush imgBrush = new ImageBrush();
            BitmapImage btpImg = new BitmapImage();

            btpImg.BeginInit();
            btpImg.UriSource = new Uri(filePath);
            btpImg.EndInit();
            imgBrush.ImageSource = btpImg;

            ProfileImage.Fill = imgBrush;
        }
    }
}

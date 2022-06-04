using Microsoft.AspNetCore.Http.Internal;
using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Models.IF;
using NolowaFrontend.Servicies;
using System;
using System.Collections.Generic;
using System.IO;
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

        private byte[] _profileImageByteArray;

        private readonly IUserService _userService;

        public SignUpControl()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        public SignUpControl(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard hideAnimation = (Storyboard)this.Resources[CANCLE_ANIMATION];
            hideAnimation.Completed += (sender, e) => {
                txtName.ClearText();
                txtEmail.ClearText();
                txtPassword.ClearText();
                txtPasswordValidation.ClearText();

                _profileImageByteArray = new byte[0];
                SetEmptyProfileImage();
                this.Visibility = Visibility.Hidden;
            };

            ClickedCancel?.Invoke(sender, e);
            hideAnimation.Begin();
        }

        private async void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputText() == false)
                return;

            if (ValidatePassword() == false)
            {
                // Message
                return;
            }

            var response = await Save(new IFSignUpUser()
            {
                AccountName = txtName.InputText,
                Email = txtEmail.InputText,
                Password = txtPassword.InputText,
                ProfileImage = _profileImageByteArray,
            });

            if (response.IsNull())
            {
                // Save Failed
                return;
            }

            Storyboard hideAnimation = (Storyboard)this.Resources[CONFIRM_ANIMATION];
            hideAnimation.Completed += (_, _) =>
            {
                this.Visibility = Visibility.Hidden;
            };

            CompleteSignup?.Invoke(sender, e);
            hideAnimation.Begin();
        }

        private async Task<byte[]> GetImageByteArray(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                var file = new FormFile(stream, 0, stream.Length, null, System.IO.Path.GetFileName(stream.Name));

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
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

        private async Task<User> Save(IFSignUpUser user)
        {
            try
            {
                return await _userService.SaveAsync(user);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool ValidateImageSize(Stream fileStream)
        {
            // is More than 2MB?
            if (fileStream.Length > 2097152)
                return false;

            return true;
        }

        private async void ProfileImageButton_Click(object sender, RoutedEventArgs e)
        {
            using (var openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files(*.JPG)|*.JPG|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (ValidateImageSize(openFileDialog.OpenFile()) == false)
                        return;

                    var filePath = openFileDialog.FileName;
                    
                    SetProfileImage(filePath);

                    _profileImageByteArray = await GetImageByteArray(filePath);
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

        private void SetEmptyProfileImage()
        {
            ProfileImage.Fill = null;
        }
    }
}

using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NolowaFrontend.ViewModels.UserControls
{
    public class PostVM : ViewModelBase
    {
        private string _profileImageSource;

        public string ProfileImageSource
        {
            get { return _profileImageSource; }
            set
            {
                _profileImageSource = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _profileImage;

        public BitmapImage ProfileImage
        {
            get { return _profileImage; }
            set 
            { 
                _profileImage = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _bitmap;

        public BitmapImage Bitmap
        {
            get { return _bitmap; }
            set
            {
                _bitmap = value;
                OnPropertyChanged();
            }
        }

        private byte[] _array;

        public byte[] array
        {
            get { return _array; }
            set { 
                _array = value;

                using (var ms = new System.IO.MemoryStream(array))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad; // here
                    image.StreamSource = ms;
                    image.EndInit();

                    ProfileImage = image;
                }
            }
        }

        private string _message = string.Empty;

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }


        public PostVM()
        {
            ProfileImageSource = @"~\..\Resources\ProfilePicture.jpg";
        }
    }
}

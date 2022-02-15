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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NolowaFrontend.Controls
{
    /// <summary>
    /// ToggleRadioImageButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ToggleRadioImageButton : RadioButton
    {
        public Uri DefaultImageUri
        {
            get { return (Uri)GetValue(DefaultImageUriProperty); }
            set { SetValue(DefaultImageUriProperty, value); }
        }

        public static readonly DependencyProperty DefaultImageUriProperty =
            DependencyProperty.Register("DefaultImageUri", typeof(Uri), typeof(ToggleRadioImageButton), new PropertyMetadata(null));

        public Uri ClickedImageUri
        {
            get { return (Uri)GetValue(ClickedImageUriProperty); }
            set { SetValue(ClickedImageUriProperty, value); }
        }

        public static readonly DependencyProperty ClickedImageUriProperty =
            DependencyProperty.Register("ClickedImageUri", typeof(Uri), typeof(ToggleRadioImageButton), new PropertyMetadata(null));

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(int), typeof(ToggleRadioImageButton), new PropertyMetadata(0));

        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(int), typeof(ToggleRadioImageButton), new PropertyMetadata(0));

        public ToggleRadioImageButton()
        {
            InitializeComponent();
        }
    }
}

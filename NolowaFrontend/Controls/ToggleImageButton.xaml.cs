using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// ToggleImageButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ToggleImageButton : ToggleButton
    {
        public Uri DefaultImageUri
        {
            get { return (Uri)GetValue(DefaultImageUriProperty); }
            set { SetValue(DefaultImageUriProperty, value); }
        }

        public static readonly DependencyProperty DefaultImageUriProperty =
            DependencyProperty.Register("DefaultImageUri", typeof(Uri), typeof(ToggleImageButton), new PropertyMetadata(null));

        public Uri ClickedImageUri
        {
            get { return (Uri)GetValue(ClickedImageUriProperty); }
            set { SetValue(ClickedImageUriProperty, value); }
        }

        public static readonly DependencyProperty ClickedImageUriProperty =
            DependencyProperty.Register("ClickedImageUri", typeof(Uri), typeof(ToggleImageButton), new PropertyMetadata(null));

        public int ImageWitdh
        {
            get { return (int)GetValue(ImageWitdhProperty); }
            set { SetValue(ImageWitdhProperty, value); }
        }

        public static readonly DependencyProperty ImageWitdhProperty =
            DependencyProperty.Register("ImageWitdh", typeof(int), typeof(ToggleImageButton), new PropertyMetadata(0));
        
        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(int), typeof(ToggleImageButton), new PropertyMetadata(0));

        public ToggleImageButton()
        {
            InitializeComponent();
        }
    }
}

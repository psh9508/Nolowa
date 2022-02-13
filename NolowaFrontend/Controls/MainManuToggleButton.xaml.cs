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
    /// MainManuToggleButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainManuToggleButton : UserControl
    {
        public Uri DefaultImageUri
        {
            get { return (Uri)GetValue(DefaultImageUriProperty); }
            set { SetValue(DefaultImageUriProperty, value); }
        }

        public static readonly DependencyProperty DefaultImageUriProperty =
            DependencyProperty.Register("DefaultImageUri", typeof(Uri), typeof(MainManuToggleButton), new PropertyMetadata(null));

        public Uri ClickedImageUri
        {
            get { return (Uri)GetValue(ClickedImageUriProperty); }
            set { SetValue(ClickedImageUriProperty, value); }
        }

        public static readonly DependencyProperty ClickedImageUriProperty =
            DependencyProperty.Register("ClickedImageUri", typeof(Uri), typeof(MainManuToggleButton), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MainManuToggleButton), new PropertyMetadata(null));

        public MainManuToggleButton()
        {
            InitializeComponent();
        }
    }
}

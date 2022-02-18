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

namespace NolowaFrontend.Controls.Buttons.Base
{
    /// <summary>
    /// RoundedToggleButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RoundedToggleButton : ToggleButton
    {
        public string CheckedContent
        {
            get { return (string)GetValue(CheckedContentProperty); }
            set { SetValue(CheckedContentProperty, value); }
        }

        public static readonly DependencyProperty CheckedContentProperty =
            DependencyProperty.Register("CheckedContent", typeof(string), typeof(RoundedToggleButton), new PropertyMetadata(string.Empty));

        public string UncheckedContent
        {
            get { return (string)GetValue(UncheckedContentProperty); }
            set { SetValue(UncheckedContentProperty, value); }
        }

        public static readonly DependencyProperty UncheckedContentProperty =
            DependencyProperty.Register("UncheckedContent", typeof(string), typeof(RoundedToggleButton), new PropertyMetadata(string.Empty));

        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(RoundedToggleButton), new PropertyMetadata(null));

        public Brush UncheckedBackground
        {
            get { return (Brush)GetValue(UncheckedBackgroundProperty); }
            set { SetValue(UncheckedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty UncheckedBackgroundProperty =
            DependencyProperty.Register("UncheckedBackground", typeof(Brush), typeof(RoundedToggleButton), new PropertyMetadata(null));

        public Brush CheckedContentForeground
        {
            get { return (Brush)GetValue(CheckedContentForegroundProperty); }
            set { SetValue(CheckedContentForegroundProperty, value); }
        }

        public static readonly DependencyProperty CheckedContentForegroundProperty =
            DependencyProperty.Register("CheckedContentForeground", typeof(Brush), typeof(RoundedToggleButton), new PropertyMetadata(null));

        public Brush UncheckedContentForeground
        {
            get { return (Brush)GetValue(UncheckedContentForegroundProperty); }
            set { SetValue(UncheckedContentForegroundProperty, value); }
        }

        public static readonly DependencyProperty UncheckedContentForegroundProperty =
            DependencyProperty.Register("UncheckedContentForeground", typeof(Brush), typeof(RoundedToggleButton), new PropertyMetadata(null));


        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(int), typeof(RoundedToggleButton), new PropertyMetadata(0));

        public RoundedToggleButton()
        {
            InitializeComponent();
        }
    }
}

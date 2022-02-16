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

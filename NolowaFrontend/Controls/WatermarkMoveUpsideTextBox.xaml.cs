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
    public enum TextBoxType
    {
        Normal,
        Password,
    }

    /// <summary>
    /// WatermarkMoveUpsideInputTextBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WatermarkMoveUpsideTextBox : UserControl
    {
        public string InputText
        {
            get
            {
                byte[] bytes = Encoding.UTF8.GetBytes((string)GetValue(InputTextProperty));
                return Encoding.Default.GetString(bytes);
            }
            set { SetValue(InputTextProperty, value); }
        }

        public static readonly DependencyProperty InputTextProperty =
            DependencyProperty.Register("InputText", typeof(string), typeof(WatermarkMoveUpsideTextBox), new PropertyMetadata(""));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(WatermarkMoveUpsideTextBox), new PropertyMetadata(""));


        public TextBoxType TextBoxType
        {
            get { return (TextBoxType)GetValue(TextBoxTypeProperty); }
            set { SetValue(TextBoxTypeProperty, value); }
        }

        public static readonly DependencyProperty TextBoxTypeProperty =
            DependencyProperty.
            Register("TextBoxType", typeof(TextBoxType), typeof(WatermarkMoveUpsideTextBox), new PropertyMetadata(TextBoxType.Normal));


        public WatermarkMoveUpsideTextBox()
        {
            InitializeComponent();
        }

        public void ClearText()
        {
            textBox.Clear();
            passwordBox.Clear();
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            InputText = ((PasswordBox)sender).Password;
        }
    }
}

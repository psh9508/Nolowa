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
    /// HeartButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class HeartButton : UserControl
    {
        public event RoutedEventHandler Click;

        public bool HasBeenLiked
        {
            get { return (bool)GetValue(HasBeenLikedProperty); }
            set { SetValue(HasBeenLikedProperty, value); }
        }

        public static readonly DependencyProperty HasBeenLikedProperty =
            DependencyProperty.Register("HasBeenLiked", typeof(bool), typeof(HeartButton), new PropertyMetadata(false));

        public HeartButton()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                HasBeenLiked = !HasBeenLiked;
                this.Click(this, e);
            }
        }
    }
}

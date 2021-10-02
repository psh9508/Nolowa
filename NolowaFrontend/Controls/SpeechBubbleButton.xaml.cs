using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// SpeechBubbleButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SpeechBubbleButton : UserControl
    {
        public event RoutedEventHandler Click;

        public int SpeechBubbleCount
        {
            get { return (int)GetValue(SpeechBubbleCountProperty); }
            set { SetValue(SpeechBubbleCountProperty, value); }
        }

        public static readonly DependencyProperty SpeechBubbleCountProperty =
            DependencyProperty.Register("SpeechBubbleCount",
                                        typeof(int),
                                        typeof(SpeechBubbleButton),
                                        new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                             (o, e) => {
                                                 (o as SpeechBubbleButton).OnSpeechBubbleCount(e);
                                             }
                                        ));

        public SpeechBubbleButton()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }

        private void OnSpeechBubbleCount(DependencyPropertyChangedEventArgs e)
        {
            int SamplePropertyNewValue = (int)e.NewValue;

            txtSpeechBubbleCount.Text = SamplePropertyNewValue.ToString("N2");
        }

    }
}

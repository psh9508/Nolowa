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

namespace NolowaFrontend.Controls.Buttons
{
    public enum eFollowButtonState
    {
        None,
        Editable,
        Followed,
        Following,
    }

    /// <summary>
    /// FollowButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FollowButton : UserControl
    {
        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(FollowButton), new PropertyMetadata(null));

        public eFollowButtonState ButtonState
        {
            get { return (eFollowButtonState)GetValue(ButtonStateProperty); }
            set { SetValue(ButtonStateProperty, value); }
        }

        public static readonly DependencyProperty ButtonStateProperty =
            DependencyProperty.Register("ButtonState", typeof(eFollowButtonState), typeof(FollowButton), new PropertyMetadata(eFollowButtonState.None
                , propertyChangedCallback: (s, e) =>
                {
                    var followButton = s as FollowButton;
                    
                    if (followButton != null)
                    {
                        switch (followButton.ButtonState)
                        {  
                            case eFollowButtonState.Editable:
                                followButton.body.Visibility = Visibility.Collapsed;
                                followButton.btnEdit.Visibility = Visibility.Visible;
                                break;
                            case eFollowButtonState.Followed:
                                followButton.body.IsChecked = false;
                                break;
                            case eFollowButtonState.Following:
                                followButton.body.IsChecked = true;
                                break;
                            default:
                                break;
                        }
                    }
                }));

        public FollowButton()
        {
            InitializeComponent();
        }
    }
}

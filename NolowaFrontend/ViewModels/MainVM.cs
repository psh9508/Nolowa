using NolowaFrontend.ViewModels.Base;
using NolowaFrontend.ViewModels.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NolowaFrontend.ViewModels
{
    public class MainVM : ViewModelBase
    {
        private List<PostVM> _posts = new List<PostVM>();

        public List<PostVM> Posts
        {
            get { return _posts; }
            set { 
                _posts = value;
                OnPropertyChanged();
            }
        }

        public MainVM()
        {
            Posts.AddRange(new List<PostVM>()
            {
                new PostVM(),
                new PostVM(),
                new PostVM(),
                new PostVM(),
                new PostVM(),
                new PostVM(),
            });
        }

    }
}

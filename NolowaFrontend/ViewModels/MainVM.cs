using NolowaFrontend.Models;
using NolowaFrontend.ViewModels.Base;
using NolowaFrontend.ViewModels.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;

namespace NolowaFrontend.ViewModels
{
    public class MainVM : ViewModelBase
    {
        private readonly User _user;

        private List<PostVM> _posts = new List<PostVM>();

        public List<PostVM> Posts
        {
            get { return _posts; }
            set { 
                _posts = value;
                OnPropertyChanged();
            }
        }

        //public Image byteArrayToImage(byte[] bytesArr)
        //{
        //    using (MemoryStream memstr = new MemoryStream(bytesArr))
        //    {
        //        Image img = Image.FromStream(memstr);
        //        return img;
        //    }
        //}

        public MainVM(User user)
        {
            _user = user;

            for (int i = 0; i < _user.FollowIds.Count; i++)
            {
                Posts.Add(new PostVM());
            }
        }

    }
}

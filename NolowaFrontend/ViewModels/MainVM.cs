using NolowaFrontend.Models;
using NolowaFrontend.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using NolowaFrontend.Servicies;
using System.Windows.Input;
using System.Collections.ObjectModel;
using NolowaFrontend.Views.UserControls;

namespace NolowaFrontend.ViewModels
{
    public class MainVM : ViewModelBase
    {
        private readonly User _user;
        private readonly IPostService _service;

        private ObservableCollection<PostView> _posts = new ObservableCollection<PostView>();

        public ObservableCollection<PostView> Posts
        {
            get { return _posts; }
            set { _posts = value; OnPropertyChanged(); }
        }

        public Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }

        public byte[] GetByteFrom()
        {
            Image img = Image.FromFile(@"C:\Users\psh02\source\repos\NolowaFrontend\NolowaFrontend\Resources\1.jpg");
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }

            return arr;
        }

        #region ICommands
        private ICommand _loadedEventCommand;

        public ICommand LoadedEventCommand
        {
            get
            {
                return GetRelayCommand(ref _loadedEventCommand, async x =>
                {
                    var posts = await _service.GetPosts(id: 5);

                    //foreach (var post in posts.ResponseData)
                    //{
                    //    Posts.Add(new PostView()
                    //    {
                    //        Name = post.Name,
                    //        UserAccountID = post.UserAccountId,
                    //        UserID = post.UserID.ToString(),
                    //        Message = post.Message,
                    //        ElapsedTime = GetElapsedTime(post.UploadedDate),
                    //    });
                    //}
                    Posts.Add(new PostView()
                    {
                        Name = "Name",
                        UserAccountID = "1",
                        UserID = "@ID",
                        Message = "자주 사용하는 유저의 프로필 사진 데이터를 필요할 때마다 서버에서 가져오는 것이 아니라 로그인 시 한번 다운로드 받아 로컬에 캐싱해둔다. 서버에선 로그인시 저장된 유저의 프로필 사진의 해시값을 이용해서 현재 로컬에 다운로드 되어있는 파일과 서버에 있는 파일이 같은지 확인하고 같지 않을 시만 로컬에 재다운로드를 한다.",
                        ElapsedTime = DateTime.Now.ToString(),
                        ProfileImageSource = @"C:\Users\psh02\OneDrive\사진\Nolowa\ProfileImages\844b2e56483db6f6f75611e891a84e9c38c727d02655f3d211a837f0aadfa31c.jpg",
                    });
                });
            }
        }
        #endregion


        public MainVM(User user)
        {
            _user = user;
            _service = new PostService();

            LoadedEventCommand.Execute(null);
        }


        private string GetElapsedTime(DateTime creadtedTime)
        {
            var timeSpan = DateTime.Now - creadtedTime;

            if(timeSpan.Days == 0 && timeSpan.Hours == 0 && timeSpan.Minutes == 0 && timeSpan.Seconds != 0)
            {
                return timeSpan.Seconds + "초";
            }
            else if(timeSpan.Days == 0 && timeSpan.Hours == 0 && timeSpan.Minutes != 0)
            {
                return timeSpan.Minutes + "분";
            }
            else if(timeSpan.Days == 0 && timeSpan.Hours != 0)
            {
                return timeSpan.Hours + "시";
            }
            else if(timeSpan.Days != 0)
            {
                if (timeSpan.Days >= 999)
                    return "999일";

                return timeSpan.Days + "일";
            }
            else
            {
                return "1초";
            }
        }
    }
}


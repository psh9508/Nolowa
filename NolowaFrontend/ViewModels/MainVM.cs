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
                    var posts = await _service.GetPosts(id: 1);

                    foreach (var post in posts.ResponseData)
                    {
                        Posts.Add(new PostView()
                        {
                            ID = post.ID.ToString(),
                            UserID = post.UserID,
                            Message = post.Message,
                            ElapsedTime = GetElapsedTime(post.UploadedDate),
                        });
                    }
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


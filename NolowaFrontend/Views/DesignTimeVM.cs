using NolowaFrontend.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Views
{
    public class TwitterViewDesignTimeVM
    {
        public ObservableCollection<PostView> Posts { get; set; } = new ObservableCollection<PostView>();

        public TwitterViewDesignTimeVM()
        {
            Posts.Add(new PostView()
            {
                Message = "디자인 타임에 나오는 데이터 입니다.",
            });

            Posts.Add(new PostView()
            {
                Message = "디자인 타임 데이터를 채우고 있습니다. 조금 긴 데이터를 넣어보도록 하겠습니다."
            });

            Posts.Add(new PostView()
            {
                Message = "AddRange()라는 함수가 있었다면 좋았을뻔 했습니다. It'd have been better, it had AddRange() method."
            });
        }
    }
}

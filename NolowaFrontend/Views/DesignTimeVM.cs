using NolowaFrontend.Models;
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
        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();

        public TwitterViewDesignTimeVM()
        {
            Posts.Add(new Post()
            {
                Message = "디자인 타임에 나오는 데이터 입니다.",
            });

            Posts.Add(new Post()
            {
                Message = "디자인 타임 데이터를 채우고 있습니다. 조금 긴 데이터를 넣어보도록 하겠습니다."
            });

            Posts.Add(new Post()
            {
                Message = "AddRange()라는 함수가 있었다면 좋았을뻔 했습니다. It'd have been better, it had AddRange() method in the ObservableCollection type."
            });
        }
    }

    public class SearchViewDesignTimeVM
    {
        public ObservableCollection<User> SearchedUsers { get; set; } = new ObservableCollection<User>();
        public ObservableCollection<string> SearchedKeywords { get; set; } = new ObservableCollection<string>();

        public SearchViewDesignTimeVM()
        {
            SearchedUsers.Add(new User()
            {
                UserId = "AccountID",
                AccountName = "Name",
                ProfileInfo = new ProfileInfo()
                {
                    ProfileImage = new Models.Images.ProfileImage()
                    {
                        Hash = "이승국",
                    }
                },
            });

            SearchedUsers.Add(new User()
            {
                UserId = "@Gold_Moon",
                AccountName = "마수리",
                ProfileInfo = new ProfileInfo()
                {
                    ProfileImage = new Models.Images.ProfileImage()
                    {
                        Hash = "844b2e56483db6f6f75611e891a84e9c38c727d02655f3d211a837f0aadfa31c",
                    }
                }
            });

            SearchedUsers.Add(new User()
            {
                UserId = "@xxxxbxxx",
                AccountName = "랜더스",
                ProfileInfo = new ProfileInfo()
                {
                    ProfileImage = new Models.Images.ProfileImage()
                    {
                        Hash = "aa",
                    }
                }
            });


            SearchedKeywords.Add("마우스");

            SearchedKeywords.Add("범죄와의 전쟁");

            SearchedKeywords.Add("행복한 가정");

            SearchedKeywords.Add("즐거운 하루");
        }
    }

}

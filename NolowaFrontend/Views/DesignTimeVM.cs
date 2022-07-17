using NolowaFrontend.Models;
using NolowaFrontend.ViewModels;
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

    public class DirectMessageSendViewDesignTimeVM
    {
        public ObservableCollection<DirectMessageDialogItem> Dialog { get; set; } = new ObservableCollection<DirectMessageDialogItem>();

        public DirectMessageSendViewDesignTimeVM()
        {
            Dialog.Add(new DirectMessageDialogItem()
            {
                Message = "가.",
                Time = "2022-02-04 11시 11분 11초",
                IsMine = true,
            });

            Dialog.Add(new DirectMessageDialogItem()
            {
                Message = "짧.",
                Time = "2022-02-04 11시 11분 11초",
                IsMine = false,
            });

            Dialog.Add(new DirectMessageDialogItem()
            {
                Message = "숲속에 웅크리고 있던 곰이 나왔다 언제까지 서성일지 모르겠지만 이것도 곧 끝나리라 생각한다.",
                Time = "2022-02-04 11시 11분 11초",
                IsMine = false,
            });

            Dialog.Add(new DirectMessageDialogItem()
            {
                Message = "핸드폰 액정이 깨졌습니다. 요즘엔 터치도 잘 안되는 것 같은데.. 하나 사야되나 고민이 되는 시기입니다.",
                Time = "2022-02-04 11시 11분 11초",
                IsMine = true,
            });

            Dialog.Add(new DirectMessageDialogItem()
            {
                Message = "생각보다 행복한 하루하루입니다. 그 누구도 나의 행복을 침해할 수 없습니다.",
                Time = "2022-02-04 11시 11분 11초",
                IsMine = true,
            });
        }
    }

    public class DirectMessageViewDesignTimeVM
    {
        public ObservableCollection<PreviousDirectMessageDialogItem> PreviousDialogItems { get; set; } = new ObservableCollection<PreviousDirectMessageDialogItem>();

        public DirectMessageViewDesignTimeVM()
        {
            PreviousDialogItems.Add(new PreviousDirectMessageDialogItem()
            {
                User = new User()
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
                },
                Message = "그 레이더는 실패했다.",
                Time = "1시간",
                NewMessageCount = 1,
            });

            PreviousDialogItems.Add(new PreviousDirectMessageDialogItem()
            {
                User = new User()
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
                },
                Message = "우리 마지막 대화는 이것입니다. 참고로 말씀드리지만 이 대화는 다시 시작 될 것입니다.",
                Time = "1시간",
            });

            PreviousDialogItems.Add(new PreviousDirectMessageDialogItem()
            {
                User = new User()
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
                },
                Message = "음악이야말로 세상에서 나에게 영감을 줄 수 있는 것 중 가장 강력한 것일 것이다. 나에게 음악을 허락하라!",
                Time = "1시간",
                NewMessageCount = 5,
            });
        }
    }

}

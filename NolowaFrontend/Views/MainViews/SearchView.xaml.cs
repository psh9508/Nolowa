using NolowaFrontend.Core;
using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Models.Events;
using NolowaFrontend.Servicies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NolowaFrontend.Views.MainViews
{
    /// <summary>
    /// SearchView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchView : UserControl
    {
        private readonly User _user;
        private readonly ISearchService _searchService;

        /// <summary>
        /// 프로필 클릭 이벤트를 라우티드이벤트로 만들어서 밖으로 버블링시킴
        /// </summary>
        public static readonly RoutedEvent ClickedProfileImageEvent =
            EventManager.RegisterRoutedEvent("ClickedProfileImage", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchView));

        public event RoutedEventHandler ClickedProfileImage
        {
            add { AddHandler(ClickedProfileImageEvent, value); }
            remove { RemoveHandler(ClickedProfileImageEvent, value); }
        }

        public ObservableCollection<User> SearchedUsers { get; set; } = new ObservableCollection<User>();

        public SearchView()
        {
            InitializeComponent();

            _user = AppConfiguration.LoginUser;
            _searchService = new SearchService();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await SetSearchedKeywordAsync();
        }

        public async Task TimerSearchAsync(string text)
        {
            txtSearchResultEmpty.Text = string.Empty;

            if (text.IsNotVaild())
            {
                listboxUsers.ItemsSource = new List<SearchedUser>();
                await SetSearchedKeywordAsync();
                return;
            }

            var response = await _searchService.SearchUser(text);

            if (response.IsSuccess)
            {
                await InsertSearchKeywordAsync(text);

                if (response.ResponseData.Count <= 0)
                {
                    listboxUsers.ItemsSource = new List<SearchedUser>();

                    txtSearchResultEmpty.Text = $"\"{text}\" 검색하기";
                    return;
                }

                var convertedDatas = response.ResponseData.Select(x => new User()
                {
                    ID = x.ID,
                    AccountName = x.Name,
                    UserId = x.UserId,
                    ProfileInfo = new ProfileInfo()
                    {
                        ProfileImage = x.ProfileInfo.ProfileImage,
                    },
                });

                listboxUsers.ItemsSource = convertedDatas;
            }
        }        

        private async Task SetSearchedKeywordAsync()
        {
            var response = await _searchService.GetSearchedKeywords(_user.ID);

            if (response.IsSuccess)
                listboxSearchedKeywords.ItemsSource = response.ResponseData;
        }

        private async Task InsertSearchKeywordAsync(string keyword)
        {
            await _searchService.Search(_user.ID, keyword);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _searchService.DeleteAllSearchedKeywords(_user.ID);

            await SetSearchedKeywordAsync();
        }

        private void ProfileImageElipseView_ClickedProfileImage(object sender, RoutedEventArgs e)
        {
            //RaiseEvent(e);
            if (e is ObjectRoutedEventArgs args)
            {
                var newEventArgs = new ObjectRoutedEventArgs(ClickedProfileImageEvent, args.Parameter);
                RaiseEvent(newEventArgs);
            }
        }
    }
}

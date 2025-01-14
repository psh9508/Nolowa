﻿using NolowaFrontend.Core;
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

        public ObservableCollection<ScoreInfo> SearchRankingKeywords { get; set; } = new();
        public ObservableCollection<User> SearchedUsers { get; set; } = new();

        public SearchView()
        {
            InitializeComponent();

            _user = AppConfiguration.LoginUser;
            _searchService = new SearchService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // 2개의 쓰레드에서 대기 없이 검색했던 목록과 검색어 랭킹을 가져온다
            _ = SetSearchedKeywordAsync();
            _ = GetSearchRankAsync();
        }

        private async Task GetSearchRankAsync()
        {
            var response = await _searchService.GetSearchkeywordRankAsync();

            if (response.IsSuccess)
            {
                var orderedRankData = response.ResponseData.OrderByDescending(x => x.Score).ThenBy(x => x.Key).ToList();

                for (int i = 0; i < response.ResponseData.Count; i++)
                    orderedRankData[i].Ranking = i + 1;

                listboxKeywordRanking.ItemsSource = orderedRankData.ToObservableCollection();
            }
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
                    //Id = x.ID,
                    //AccountName = x.Name,
                    //UserId = x.UserId,
                    //ProfileInfo = new ProfileInfo()
                    //{
                    //    Message = x.ProfileInfo.Message,
                    //    BackgroundImage = x.ProfileInfo.BackgroundImage,
                    //    ProfileImage = x.ProfileInfo.ProfileImage,
                    //},
                });

                listboxUsers.ItemsSource = convertedDatas;
            }
        }        

        private async Task SetSearchedKeywordAsync()
        {
            var response = await _searchService.GetSearchedKeywords(long.Parse(_user.USN));

            if (response.IsSuccess)
                listboxSearchedKeywords.ItemsSource = response.ResponseData;
        }

        private async Task InsertSearchKeywordAsync(string keyword)
        {
            await _searchService.Search(long.Parse(_user.USN), keyword);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _searchService.DeleteAllSearchedKeywords(long.Parse(_user.USN));

            await SetSearchedKeywordAsync();
        }

        private async void RefreshRankingButton_Click(object sender, RoutedEventArgs e)
        {
            await GetSearchRankAsync();
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

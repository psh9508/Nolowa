using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
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

        public SearchView(User user)
        {
            InitializeComponent();

            _user = user;
            _searchService = new SearchService();
        }

        public async Task TimerSearch(string text)
        {
            if (text.IsNotVaild())
            {
                listboxUsers.ItemsSource = new List<SearchedUser>();
                return;
            }

            var response = await _searchService.SearchUser(text);

            if(response.IsSuccess)
                listboxUsers.ItemsSource = response.ResponseData;
        }
    }
}

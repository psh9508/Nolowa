using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
using NolowaFrontend.Servicies.Base;
using NolowaFrontend.Views.MainViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Servicies
{
    public interface ISearchService
    {
        Task<ResponseBaseEntity<List<SearchedUser>>> Search(long userIdWhoSearches, string usernameBeSearched);
        Task<ResponseBaseEntity<List<SearchedUser>>> SearchUser(string usernameBeSearched);
        Task<ResponseBaseEntity<List<string>>> GetSearchedKeywords(long userId);
        Task DeleteAllSearchedKeywords(long userId);
        Task<ResponseBaseEntity<List<ScoreInfo>>> GetSearchkeywordRankAsync(int startIndex = 0, int endIndex = 5);
    }

    public class SearchService : ServiceBase, ISearchService
    {
        public override string ParentEndPoint => "Search";

        public async Task<ResponseBaseEntity<List<SearchedUser>>> Search(long userIdWhoSearches, string keyword)
        {
            return await DoGet<List<SearchedUser>>($"{keyword}?userId={userIdWhoSearches}");
        }

        public async Task<ResponseBaseEntity<List<SearchedUser>>> SearchUser(string usernameBeSearched)
        {
            return await DoGet<List<SearchedUser>>($"User/{usernameBeSearched}");
        }

        public async Task<ResponseBaseEntity<List<string>>> GetSearchedKeywords(long userId)
        {
            return await DoGet<List<string>>($"Keywords/{userId}");
        }

        public async Task DeleteAllSearchedKeywords(long userId)
        {
            await DoDelete($"DeleteAll/{userId}");
        }

        public async Task<ResponseBaseEntity<List<ScoreInfo>>> GetSearchkeywordRankAsync(int startIndex = 0, int endIndex = 5)
        {
            return await DoGet<List<ScoreInfo>>($"Keywords/Rank/{startIndex}/{endIndex}");
        }
    }
}

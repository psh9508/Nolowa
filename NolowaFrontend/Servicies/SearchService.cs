using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
using NolowaFrontend.Servicies.Base;
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
    }

    public class SearchService : ServiceBase, ISearchService
    {
        private const string parentEndPoint = "Search";

        public async Task<ResponseBaseEntity<List<SearchedUser>>> Search(long userIdWhoSearches, string keyword)
        {
            return await DoGet<List<SearchedUser>>($"{parentEndPoint}/{keyword}?userId={userIdWhoSearches}");
        }

        public async Task<ResponseBaseEntity<List<SearchedUser>>> SearchUser(string usernameBeSearched)
        {
            return await DoGet<List<SearchedUser>>($"{parentEndPoint}/User/{usernameBeSearched}");
        }

        public async Task<ResponseBaseEntity<List<string>>> GetSearchedKeywords(long userId)
        {
            return await DoGet<List<string>>($"{parentEndPoint}/Keywords/{userId}");
        }

        public async Task DeleteAllSearchedKeywords(long userId)
        {
            await DoDelete($"{parentEndPoint}/DeleteAll/{userId}");
        }
    }
}

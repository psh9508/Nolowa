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
    }

    public class SearchService : ServiceBase, ISearchService
    {
        private const string parentEndPoint = "Search";

        public async Task<ResponseBaseEntity<List<SearchedUser>>> Search(long userIdWhoSearches, string usernameBeSearched)
        {
            return await DoGet<List<SearchedUser>>($"{parentEndPoint}/{usernameBeSearched}?userId={userIdWhoSearches}");
        }
    }
}

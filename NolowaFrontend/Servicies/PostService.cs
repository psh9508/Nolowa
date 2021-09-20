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
    public interface IPostService
    {
        Task<ResponseBaseEntity<List<Post>>> GetPosts(long id);
    }

    public class PostService : ServiceBase, IPostService
    {
        private const string parentEndPoint = "Post";

        public async Task<ResponseBaseEntity<List<Post>>> GetPosts(long id)
        {
            return await DoGet<List<Post>>($"{parentEndPoint}/{id}/Followers");
        }
    }
}

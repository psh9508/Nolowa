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
        Task<ResponseBaseEntity<string>> IsAlive();
        Task<ResponseBaseEntity<List<Post>>> GetMyPostsAsync(long userId);
        Task<ResponseBaseEntity<List<Post>>> GetHomePosts(long id);
        Task<ResponseBaseEntity<List<Post>>> GetPostsAsync(long id, int pageNumber);
        Task<ResponseBaseEntity<Post>> InsertPostAsync(Post post);
    }

    public class PostService : ServiceBase, IPostService
    {
        //private const string parentEndPoint = "Post";
        //private const string parentEndPoint = "Posts";

        public override string ParentEndPoint => "Post";

        public async Task<ResponseBaseEntity<string>> IsAlive()
        {
            return await DoGet<string>("/Alive");
        }

        public async Task<ResponseBaseEntity<List<Post>>> GetMyPostsAsync(long userId)
        {
            return await DoGet<List<Post>>($"{userId}/Posts");
        }
        public async Task<ResponseBaseEntity<List<Post>>> GetHomePosts(long userId)
        {
            return await DoGet<List<Post>>($"{userId}/HomePosts");
            //return await DoGet<List<Post>>($"{userId}/Followers/1");
        }

        public async Task<ResponseBaseEntity<List<Post>>> GetPostsAsync(long id, int pageNumber)
        {
            return await DoGet<List<Post>>($"{id}/Followers/{pageNumber}");
        }

        public async Task<ResponseBaseEntity<Post>> InsertPostAsync(Post post)
        {
            return await DoPost<Post, Post>($"New", post);
        }
    }
}
    
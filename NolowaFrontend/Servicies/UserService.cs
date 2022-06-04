using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
using NolowaFrontend.Models.IF;
using NolowaFrontend.Servicies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Servicies
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(long id);
        Task<User> SaveAsync(IFSignUpUser user);
        Task<Follower> FollowAsync(long followerUserId, long followeeUserId);
    }

    public class UserService : ServiceBase, IUserService
    {
        //public override string ParentEndPoint => "User";
        public override string ParentEndPoint => "Accounts";

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await GetTFromService<List<User>>("/users");
        }

        public async Task<User> GetUserAsync(long id)
        {
            return await GetTFromService<User>($"users/{id}");
        }

        public async Task<User> SaveAsync(IFSignUpUser user)
        {
            string debug = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            var response = await DoPost<User, IFSignUpUser>($"Save", user);

            return response.IsSuccess ? response.ResponseData : null;
        }

        public async Task<Follower> FollowAsync(long followerUserId, long followeeUserId)
        {
            var response = await DoPost<Follower, IFFollowModel>($"Follow", new IFFollowModel()
            {
                SourceID = followerUserId,
                DestID = followeeUserId,
            });

            return response.ResponseData;
        }
    }
}

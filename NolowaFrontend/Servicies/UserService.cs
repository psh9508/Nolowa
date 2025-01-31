﻿using NolowaFrontend.Models;
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
        Task<bool> ChangeProfileInfoAsync(ProfileInfo data);
    }

    public class UserService : ServiceBase, IUserService
    {
        public override string ParentEndPoint => "User";

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

            var response = await DoPost<User, IFSignUpUser>($"v1/save", user);

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

        public async Task<bool> ChangeProfileInfoAsync(ProfileInfo data)
        {
            var response = await DoPut<bool, ProfileInfo>($"ProfileInfo/Change/{data.Id}", data);

            return response.ResponseData;
        }
    }
}

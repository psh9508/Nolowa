using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
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
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(long id);
        Task<User> Save(User user);
    }

    public class UserService : ServiceBase, IUserService
    {
        public async Task<List<User>> GetAllUsers()
        {
            return await GetTFromService<List<User>>("/users");
        }

        public async Task<User> GetUser(long id)
        {
            return await GetTFromService<User>($"users/{id}");
        }

        public async Task<User> Save(User user)
        {
            var response = await DoPost<User, User>("users/save", user);

            return response.IsSuccess ? response.ResponseData : null;
        }
    }
}

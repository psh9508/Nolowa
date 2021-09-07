using NolowaFrontend.Models;
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
        Task<User> Login(User user);
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

        public async Task<User> Login(User user)
        {
            return await DoPost<User, User>($"login", user);
        }
    }
}

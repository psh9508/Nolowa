using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
using NolowaFrontend.Servicies.Base;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Servicies
{
    public interface IAuthenticationService
    {
        Task<ResponseBaseEntity<User>> Login(string id, string password);
    }

    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private const string parentEndPoint = "Authentication";

        public async Task<ResponseBaseEntity<User>> Login(string id, string password)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                id = id,
                password = password,
            });

            var loginResponse = await DoPost<User, string>($"{parentEndPoint}/Login", json);

            if (loginResponse.IsSuccess)
                _jwtToken = loginResponse.ResponseData.JWTToken;

            return loginResponse;
        }
    }
}

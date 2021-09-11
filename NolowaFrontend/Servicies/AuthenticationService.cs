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
        Task<ResponseBaseEntity<bool>> Login(string id, string password);
    }

    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private const string parentEndPoint = "Authentication/";

        public async Task<ResponseBaseEntity<bool>> Login(string id, string password)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                id = id,
                password = password,
            });

            return await DoPost<bool, string>($"{parentEndPoint}/Login", json);
        }
    }
}

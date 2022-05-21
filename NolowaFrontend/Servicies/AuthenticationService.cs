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
        //private const string parentEndPoint = "Authentication";
        //private const string parentEndPoint = "Accounts";

        public override string ParentEndPoint => "Accounts";

        public async Task<ResponseBaseEntity<User>> Login(string id, string password)
        {
            string loginInfo = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                id = id,
                password = password,
            });

            try
            {
                var loginResponse = await DoPost<User, string>($"Login", loginInfo);

                if (loginResponse.IsSuccess)
                    _jwtToken = loginResponse.ResponseData.JWTToken;

                return loginResponse;
            }
            catch (Exception ex)
            {
                return new ResponseBaseEntity<User>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseData = null,
                };
            }
        }
    }
}

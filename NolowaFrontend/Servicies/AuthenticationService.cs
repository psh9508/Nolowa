using NolowaFrontend.Extensions;
using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
using NolowaFrontend.Models.Protos.Generated.prot;
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
        Task<ResponseBaseEntity<User>> Login(LoginReq request);
    }

    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        //private const string parentEndPoint = "Authentication";
        //private const string parentEndPoint = "Accounts";

        public override string ParentEndPoint => "Accounts";

        public async Task<ResponseBaseEntity<User>> Login(LoginReq request)
        {
            try
            {
                var loginResponse = await DoPost<User, LoginReq>($"Login", request);

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

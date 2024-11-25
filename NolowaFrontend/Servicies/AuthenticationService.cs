using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
using NolowaFrontend.Models.IF;
using NolowaFrontend.Servicies.Base;
using System;
using System.Threading.Tasks;

namespace NolowaFrontend.Servicies
{
    public interface IAuthenticationService
    {
        Task<ResponseBaseEntity<User>> Login(LoginReq request);
    }

    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        public override string ParentEndPoint => "Auth";

        public async Task<ResponseBaseEntity<User>> Login(LoginReq request)
        {
            try
            {
                var loginResponse = await DoPost<User, LoginReq>($"v1/login", request);

                if (loginResponse.IsSuccess)
                    _jwtToken = loginResponse.ResponseData.Jwt;

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

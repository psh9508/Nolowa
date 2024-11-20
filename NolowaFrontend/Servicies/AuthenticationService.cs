using NolowaFrontend.Core.MessageQueue.Messages;
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
        Task<ResponseBaseEntity<User>> Login(Core.MessageQueue.Messages.LoginReq request);
    }

    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        //private const string parentEndPoint = "Authentication";
        //private const string parentEndPoint = "Accounts";

        public override string ParentEndPoint => "Auth";

        public async Task<ResponseBaseEntity<User>> Login(Core.MessageQueue.Messages.LoginReq request)
        {
            try
            {
                var loginResponse = await DoPost<User, Core.MessageQueue.Messages.LoginReq>($"v1/login", request);

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

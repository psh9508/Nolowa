using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using NolowaFrontend.Configurations;
using System.Security.Cryptography;
using NolowaFrontend.Extensions;

namespace NolowaFrontend.Core.SNSLogin
{
    public class GoogleLoginProvider : SNSLoginBase<GoogleLoginConfiguration>
    {
        public GoogleLoginProvider() : base(configPath: "SNSLoginGroup/GoogleLogin")
        {
        }

        public override string GetAuthorizationRequestURI()
        {
            var authorizationRequestBuilder = new StringBuilder();

            authorizationRequestBuilder.Append(_configuration.AuthorizationEndpoint);
            authorizationRequestBuilder.Append("?");
            authorizationRequestBuilder.Append("response_type=code");
            authorizationRequestBuilder.Append("&");
            authorizationRequestBuilder.Append("access_type=offline");
            authorizationRequestBuilder.Append("&");
            //authorizationRequestBuilder.Append("scope=email%20profile");
            authorizationRequestBuilder.Append(@"scope=https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/plus.me+https://www.googleapis.com/auth/userinfo.profile");
            authorizationRequestBuilder.Append("&");
            authorizationRequestBuilder.Append($"redirect_uri={_configuration.RedirectURI}");
            authorizationRequestBuilder.Append("&");
            authorizationRequestBuilder.Append($"client_id={_configuration.GoogleClientID}");
            
            return authorizationRequestBuilder.ToString();
        }
    }
}

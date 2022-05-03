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
using NolowaFrontend.Models.Base;

namespace NolowaFrontend.Core.SNSLogin
{
    public class GoogleLoginProvider : SNSLoginBase<GoogleLoginConfiguration>
    {
        public GoogleLoginProvider() : base(configPath: "SNSLoginGroup/GoogleLogin")
        {
        }

        public override string ParentEndPoint => "Authentication";

        public async Task ShowLoginPage()
        {
            var authorizationRequestURIResponse = await DoGet<string>("Social/Google/AuthorizationRequestURI");

            if (authorizationRequestURIResponse.IsSuccess == false)
                return;

            Process.Start(new ProcessStartInfo(authorizationRequestURIResponse.ResponseData) { UseShellExecute = true });
        }
    }
}

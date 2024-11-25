using NolowaFrontend.Models;
using NolowaFrontend.Servicies.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NolowaFrontend.Core.SNSLogin
{
    public class KakaoLoginProvider : SNSLoginBase, ISNSLogin
    {
        public static event Action<Models.User> SuccessLogin;

        public override string ParentEndPoint => "Authentication";

        public async Task ShowLoginPage()
        {
            string code = await ShowAuthorizationPageAndGetCode("Social/Kakao/AuthorizationRequestURI");

            string queryString = GetQueryString("Social/Kakao/Login/", new Dictionary<string, string>()
            {
                ["code"] = code,
            });

            var loginedAccount = await DoGet<Models.User>(queryString);

            if (loginedAccount.IsSuccess == false)
            {
                // 로그인 실패
                return;
            }

            _jwtToken = loginedAccount.ResponseData.Jwt;

            SuccessLogin?.Invoke(loginedAccount.ResponseData);
        }
    }
}

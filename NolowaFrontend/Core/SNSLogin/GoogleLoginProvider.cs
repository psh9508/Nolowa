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
using System.Web;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualBasic.ApplicationServices;
using NolowaFrontend.Servicies;

namespace NolowaFrontend.Core.SNSLogin
{
    public class GoogleLoginProvider : SNSLoginBase<GoogleLoginConfiguration>
    {
        public static event Action<Models.User> SuccessLogin;

        private readonly IAuthenticationService _service;

        public GoogleLoginProvider() : base(configPath: "SNSLoginGroup/GoogleLogin")
        {
            _service = new AuthenticationService();
        }

        public override string ParentEndPoint => "Authentication";

        public async Task ShowLoginPage()
        {
            try
            {
                var authorizationRequestURIResponse = await DoGet<string>("Social/Google/AuthorizationRequestURI");

                if (authorizationRequestURIResponse.IsSuccess == false)
                    return;

                string redirectUri = HttpUtility.ParseQueryString(authorizationRequestURIResponse.ResponseData).Get("redirect_uri");
                var redirectionListener = GetGoogleRedirectionListener(redirectUri);

                Process.Start(new ProcessStartInfo(authorizationRequestURIResponse.ResponseData) { UseShellExecute = true });
                var context = await redirectionListener.GetContextAsync();

                var response = context.Response;
                string responseString = @"<html lang=""ko"">

<head>
    <title>login_complete</title>
    
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">

    <meta name=""keywords"" content="""">
    <meta name=""description"" content="""">

    
    <link rel=""shortcut icon"" href=""https://wstatic-cdn.plaync.com/common/plaync.ico"" type=""image/x-icon"">
    <link rel=""icon"" href=""https://wstatic-cdn.plaync.com/common/plaync.ico"" type=""image/x-icon"">
    <link rel=""apple-touch-icon"" href=""https://wstatic-cdn.plaync.com/common/plaync.png"" type=""image/png"">

    
    <script src=""https://wstatic-cdn.plaync.com/platform-common-util/js/platform.util.js?t=20220507054831""></script>

    
    <script src=""https://wstatic-cdn.plaync.com/common/js/lib/jquery-3.1.0.min.js""></script>
    <link rel=""stylesheet"" href=""https://wstatic-cdn.plaync.com/account/error/css/error.style.css?t=20220507054831"">
</head>
<body data-market=""NCS"" class=""pc"">

    <script>
      window.onload = function (e) {
        window.open('', '_self', '');
        window.close();
      }
    </script>
    
    <header class=""header"">
        <h1 class=""logo"">
            <a href=""https://wwww.naver..com"">
                <span>nolowa</span>
            </a>
        </h1>
    </header>

    <div id=""container"" class=""wrapper footer--small"">
        <div class=""wrap-contents"">
            <main class=""contents full"">
                <div class=""content-section"">
                    
                    <section class=""result-section"">
                        <h2 class=""title"">
                            <span class=""complete"">계정 인증 완료</span>
                        </h2>
                        <div class=""message"">
                            <p>P서비스 이용을 위한 로그인이 완료되었습니다.</p>
                            <p>플레이를 시작하세요!</p>
                        </div>
                    </section>
                </div>
            </main>
        </div>
        <div class=""wrap-footer"">
            <footer class=""footer"">
                <p>Copyright &copy; Nolowa Corporation. All Rights Reserved.</p>
            </footer>
        </div>
    </div>

</body>
</html>
";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                var responseOutput = response.OutputStream;
                await responseOutput.WriteAsync(buffer, 0, buffer.Length);

                var code = context.Request.QueryString.Get("code");

                responseOutput.Close();
                redirectionListener.Stop();
                redirectionListener.Close();

                string queryString = GetQueryString("Social/Google/Login/", new Dictionary<string, string>()
                {
                    ["code"] = code,
                });

                var loginedAccount = await DoGet<Models.User>(queryString);

                if(loginedAccount.IsSuccess == false)
                {
                    // 로그인 실패
                    return;
                }

                SuccessLogin?.Invoke(loginedAccount.ResponseData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private HttpListener GetGoogleRedirectionListener(string redirectionUri)
        {
            if ((redirectionUri[^1] == '/') == false)
                redirectionUri = redirectionUri + "/";

            var http = new HttpListener();

            try
            {
                http.Prefixes.Add(redirectionUri);
                http.Start();
            }
            catch (Exception ex)
            {
                http.Close();
                http.Start();
            }

            return http;
        }

        //private Object ByteArrayToObject(byte[] arrBytes)
        //{
        //    try
        //    {
        //        MemoryStream memStream = new MemoryStream();
        //        BinaryFormatter binForm = new BinaryFormatter();
        //        memStream.Write(arrBytes, 0, arrBytes.Length);
        //        memStream.Seek(0, SeekOrigin.Begin);
        //        Object obj = (Object)binForm.Deserialize(memStream);

        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}

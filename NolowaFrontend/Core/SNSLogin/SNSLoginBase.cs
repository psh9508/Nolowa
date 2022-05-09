using NolowaFrontend.Extensions;
using NolowaFrontend.Servicies;
using NolowaFrontend.Servicies.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NolowaFrontend.Core.SNSLogin
{
    abstract public class SNSLoginBase : ServiceBase
    {
        public SNSLoginBase()
        {
        }

        protected async Task<string> ShowAuthorizationPageAndGetCode(string authorizationURI)
        {
            HttpListener redirectionListener = null;

            try
            {
                var authorizationRequestURIResponse = await DoGet<string>(authorizationURI);

                if (authorizationRequestURIResponse.IsSuccess == false)
                {
                    return string.Empty;
                }

                string redirectUri = HttpUtility.ParseQueryString(authorizationRequestURIResponse.ResponseData).Get("redirect_uri");
                redirectionListener = GetRedirectionListener(redirectUri);

                Process.Start(new ProcessStartInfo(authorizationRequestURIResponse.ResponseData) { UseShellExecute = true });
                var context = await redirectionListener.GetContextAsync();

                var response = context.Response;

                string responseString = GetReponsePage();

                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                var responseOutput = response.OutputStream;
                await responseOutput.WriteAsync(buffer, 0, buffer.Length);

                responseOutput.Close();

                return context.Request.QueryString.Get("code") ?? string.Empty;
            }
            finally
            {
                redirectionListener?.Stop();
                redirectionListener?.Close();
            }
        }

        private HttpListener GetRedirectionListener(string redirectUri)
        {
            if ((redirectUri[^1] == '/') == false)
                redirectUri = redirectUri + "/";

            var http = new HttpListener();
           
            http.Prefixes.Add(redirectUri);
            http.Start();

            return http;
        }

        private string GetReponsePage()
        {
            return @"<html lang=""ko"">

<head>
    <title>login_complete</title>
    
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">

    <meta name=""keywords"" content="""">
    <meta name=""description"" content="""">
    
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
                            <p>서비스 이용을 위한 로그인이 완료되었습니다.</p>
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
        }
    }
}

using NolowaFrontend.Extensions;
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

namespace NolowaFrontend.Core.SNSLogin
{
    /// <typeparam name="TConfig"> 설정 파일과 맵핑될 모델 설정 파일과 맵핑될 모델</typeparam>
    abstract public class SNSLoginBase<TConfig> where TConfig : class
    {
        protected readonly TConfig _configuration;
        protected readonly HttpListener _httpListener;

        /// <summary>
        /// SNS 로그인에 필요한 설정 경로를 입력받아 설정을 T객체에 맵핑한다.
        /// </summary>
        /// <param name="configPath"> 
        ///     App.config에 있는 SNSLogin에 필요한 설정을 넣어 준다.
        ///     <example> 
        ///         sectionGroup/section 
        ///     </example>
        /// </param>
        public SNSLoginBase(string configPath)
        {
            _configuration = ConfigurationManager.GetSection(configPath) as TConfig;
            _httpListener = new HttpListener();
        }

        public async Task ShowLoginPage()
        {
            string authorizationRequestURI = GetAuthorizationRequestURI();

            SetHttpListener();

            Process.Start(new ProcessStartInfo(authorizationRequestURI) { UseShellExecute = true });

            var authorizationResponse = await _httpListener.GetContextAsync();

            var response = authorizationResponse.Response;

            string responseString = string.Format("<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>");
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                _httpListener.Stop();
            });
        }

        private void SetHttpListener()
        {
            string redirectURI = string.Format("http://{0}:{1}/", IPAddress.Loopback, GetRandomUnusedPort());
            _httpListener.Prefixes.Add(redirectURI);
            _httpListener.Start();
        }

        private int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        /// <summary>
        /// 인증 URI를 반환한다.
        /// </summary>
        /// <returns></returns>
        abstract public string GetAuthorizationRequestURI();
    }
}

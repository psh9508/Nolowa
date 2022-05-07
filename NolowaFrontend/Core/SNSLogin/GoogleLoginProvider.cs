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
                string responseString = "<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>";
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

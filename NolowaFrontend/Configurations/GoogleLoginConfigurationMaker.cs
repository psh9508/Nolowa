using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NolowaFrontend.Configurations
{
    public class GoogleLoginConfiguration
    {
        public string GoogleClientID { get; set; } = string.Empty;
        public string GoogleSecret { get; set; } = string.Empty;
        public string AuthorizationEndpoint { get; set; } = string.Empty;
        public string TokenEndpoint { get; set; } = string.Empty;
        public string UserInfoEndpoint { get; set; } = string.Empty;
        public string RedirectURI { get; set; } = string.Empty;
    }

    public class GoogleLoginConfigurationMaker : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            try
            {
                return new GoogleLoginConfiguration()
                {
                    GoogleClientID = section.SelectSingleNode("GoogleClientID").InnerText,
                    GoogleSecret = section.SelectSingleNode("GoogleSecret").InnerText,
                    AuthorizationEndpoint = section.SelectSingleNode("AuthorizationEndpoint").InnerText,
                    TokenEndpoint = section.SelectSingleNode("TokenEndpoint").InnerText,
                    UserInfoEndpoint = section.SelectSingleNode("UserInfoEndpoint").InnerText,
                    RedirectURI = section.SelectSingleNode("RedirectURI").InnerText,
                };
            }
            catch (Exception ex)
            {
                // 설정이 없는 경우 예외를 발생시켜줘서 설정을 만든 사람 외의 사람이 프로그램을 설정 없이 실행 시키는 것을 방지해야 한다.
                throw;
            }
        }
    }
}

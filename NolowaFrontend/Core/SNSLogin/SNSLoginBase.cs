using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Core.SNSLogin
{
    /// <typeparam name="TConfig"> 설정 파일과 맵핑될 모델 설정 파일과 맵핑될 모델</typeparam>
    abstract public class SNSLoginBase<TConfig> where TConfig : class
    {
        protected readonly TConfig _configuration;

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
        }

        abstract public void ShowLoginPage();
    }
}

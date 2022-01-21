using NolowaFrontend.Servicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Core
{
    public class ViewModelLocator
    {
        public UserService UserService
        {
            get { return IocKernel.Get<UserService>(); }
        }
    }
}

using Ninject;
using Ninject.Modules;
using NolowaFrontend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Core
{
    public static class IocKernel
    {
        private static StandardKernel _kernel;

        public static T Get<T>() => _kernel.Get<T>();

        public static void Initialize(params INinjectModule[] modules)
        {
            if (_kernel.IsNull())
                _kernel = new StandardKernel(modules);
        }

    }
}

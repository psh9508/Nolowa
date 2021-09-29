using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object src)
        {
            return src == null;
        }

        public static bool IsNotNull(this object src)
        {
            return !src.IsNull();
        }
    }
}

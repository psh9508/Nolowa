using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Extensions
{
    public static class StringExtension
    {
        public static bool IsValid(this string src)
        {
            return !String.IsNullOrEmpty(src) && !String.IsNullOrWhiteSpace(src);
        }

        public static bool IsNotVaild(this string src)
        {
            return !IsValid(src);
        }
    }
}

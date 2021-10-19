using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public static byte[] ToSha256(this string src)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(src);
            SHA256Managed sha256 = new SHA256Managed();

            return sha256.ComputeHash(bytes);
        }
    }
}

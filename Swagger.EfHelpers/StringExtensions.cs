using System;
using System.Collections.Generic;
using System.Text;

namespace SystemOut.Swagger.EfHelpers
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return str[0].ToString().ToLower() + str.Substring(1, str.Length - 1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Endpoints.Framework.Extensions
{
    public static class StringExtensions
    {
        private static Regex mJsonMarchOne = new Regex(@"\\[""\\\/bfnrtu]");
        private static Regex mJsonMarchTwo = new Regex(@"""[^""\\\n\r]*""|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?");
        private static Regex mJsonMarchThree = new Regex(@"(?:^|:|,)(?:\s*\[)+");

        public static string Replace(this string str, Regex expression, string newStr)
        {
            return expression.Replace(str, newStr);
        }

        public static string[] Match(this string str, string expression)
        {
            Regex reg = new Regex(expression);
            List<string> list = new List<string>();
            foreach (Match data in reg.Matches(str))
            {
                list.Add(data.Value);
            }
            return list.ToArray();
        }

        public static bool Test(this string expression, string str)
        {
            Regex reg = new Regex(expression);
            return reg.IsMatch(str);
        }

        public static bool IsJson(this string str)
        {
            return @"^[\],:{}\s]*$".Test(str.Replace(mJsonMarchOne, "@").Replace(mJsonMarchTwo, "]").Replace(mJsonMarchThree, ""));
        }
    }
}

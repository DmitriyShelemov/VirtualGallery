using System.Text.RegularExpressions;

namespace VirtualGallery.Web.Extensions
{
    public static class StringExtensions
    {
        public static string SplitByUpperCase(this string str, string divider = " ")
        {
            var replacePattern = "$1" + divider + "$2";
            return Regex.Replace(str, "([a-z])([A-Z])", replacePattern).ToLower();
        }

        public static string SplitByUpperCaseToLower(this string str, string divider = " ")
        {
            return str.SplitByUpperCase(divider).ToLower();
        }

        public static string FormatString(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
    }
}
using System;
using System.Text.RegularExpressions;

namespace Service.DataAccess.Utils
{
    public class SecurityConvert
    {
        public static string RemoveChars(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            str = Regex.Replace(str, "=", "");
            str = Regex.Replace(str, "'", "");
            str = Regex.Replace(str, ":", "");
            str = Regex.Replace(str, ",", "");
            str = Regex.Replace(str, "\"", "");
            str = Regex.Replace(str, @"\(", "");
            str = Regex.Replace(str, @"\)", "");
            str = Regex.Replace(str, @"\.", "");
            str = Regex.Replace(str, @"\?", "");
            str = Regex.Replace(str, @"\*", "");
            str = Regex.Replace(str, @"\[", "");
            str = Regex.Replace(str, @"\]", "");
            str = Regex.Replace(str, "$", "");
            str = Regex.Replace(str, "^", "");
            str = Regex.Replace(str, @"\\", "");
            str = Regex.Replace(str, "/", "");
            return str;
        }

        public static string SqlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            str = Regex.Replace(str, "=", "%3D");
            str = Regex.Replace(str, "'", "");
            str = Regex.Replace(str, "<", "");
            str = Regex.Replace(str, ">", "");
            str = Regex.Replace(str, " like ", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, " and ", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, " or ", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, " delete ", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, " update ", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, " insert ", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, " select ", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, " where ", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, " in ", "", RegexOptions.IgnoreCase);
            return str;
        }

        public static string HtmlAmpCode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            return Regex.Replace(str, "&", "&amp;");
        }

        public static string HtmlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            str = Regex.Replace(str, "&", "&amp;");
            str = Regex.Replace(str, "<", "&lt;");
            str = Regex.Replace(str, ">", "&gt;");
            str = Regex.Replace(str, "\"", "&quot;");
            str = Regex.Replace(str, " ", "&nbsp;");
            str = Regex.Replace(str, "\n", "<br />");
            str = str.Trim();

            return str;
        }

        public static string HtmlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            str = Regex.Replace(str, "&amp;", "&");
            str = Regex.Replace(str, "&lt;", "<");
            str = Regex.Replace(str, "&gt;", ">");
            str = Regex.Replace(str, "&quot;", "\"");
            str = Regex.Replace(str, "&nbsp;", " ");
            str = Regex.Replace(str, "<br />", "\n");

            return str;
        }
    }
}

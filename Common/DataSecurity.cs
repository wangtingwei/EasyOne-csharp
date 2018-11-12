namespace EasyOne.Common
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public abstract class DataSecurity
    {
        protected DataSecurity()
        {
        }

        public static string ConvertToJavaScript(string str)
        {
            str = str.Replace(@"\", @"\\");
            str = str.Replace("\n", @"\n");
            str = str.Replace("\r", @"\r");
            str = str.Replace("\"", "\\\"");
            str = str.Replace("'", @"\'");
            return str;
        }

        public static string FilterBadChar(string strchar)
        {
            string input = "";
            if (string.IsNullOrEmpty(strchar))
            {
                return "";
            }
            string str = strchar;
            string[] strArray = new string[] { 
                "+", "'", "%", "^", "&", "?", "(", ")", "<", ">", "[", "]", "{", "}", "/", "\"", 
                ";", ":", "Chr(34)", "Chr(0)", "--"
             };
            StringBuilder builder = new StringBuilder(str);
            for (int i = 0; i < strArray.Length; i++)
            {
                input = builder.Replace(strArray[i], "").ToString();
            }
            return Regex.Replace(input, "@+", "@");
        }

        public static string FilterSqlKeyword(string strchar)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(strchar))
            {
                return string.Empty;
            }
            strchar = strchar.ToLower();
            string[] strArray = new string[] { 
                "select", "update", "insert", "delete", "declare", "@", "exec", "dbcc", "alter", "drop", "create", "backup", "if", "else", "end", "and", 
                "or", "add", "set", "open", "close", "use", "begin", "retun", "as", "go", "exists", "kill"
             };
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strchar.Contains(strArray[i]))
                {
                    strchar = strchar.Replace(strArray[i], "");
                    flag = true;
                }
            }
            if (flag)
            {
                return FilterSqlKeyword(strchar);
            }
            return strchar;
        }

        public static string GetArrayValue(int index, Collection<string> field)
        {
            if ((index >= 0) && (index < field.Count))
            {
                return field[index];
            }
            return string.Empty;
        }

        public static string GetArrayValue(int index, string[] field)
        {
            if ((field != null) && ((index >= 0) && (index < field.Length)))
            {
                return field[index];
            }
            return string.Empty;
        }

        public static string HtmlDecode(object value)
        {
            if (value == null)
            {
                return null;
            }
            return HtmlDecode(value.ToString());
        }

        public static string HtmlDecode(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("<br>", "\n");
                value = value.Replace("&gt;", ">");
                value = value.Replace("&lt;", "<");
                value = value.Replace("&nbsp;", " ");
                value = value.Replace("&#39;", "'");
                value = value.Replace("&quot;", "\"");
            }
            return value;
        }

        public static string HtmlEncode(object value)
        {
            if (value == null)
            {
                return null;
            }
            return HtmlEncode(value.ToString());
        }

        public static string HtmlEncode(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace(" ", "&nbsp;");
                str = str.Replace("'", "&#39;");
                str = str.Replace("\"", "&quot;");
                str = str.Replace("\r\n", "<br>");
                str = str.Replace("\n", "<br>");
            }
            return str;
        }

        public static int Len(string str)
        {
            int num = 0;
            foreach (char ch in str)
            {
                if (ch > '\x007f')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
            }
            return num;
        }

        public static string MakeFileRndName()
        {
            return (DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + MakeRandomString("0123456789", 4));
        }

        public static string MakeFolderName()
        {
            return DateTime.Now.ToString("yyyyMM", CultureInfo.CurrentCulture);
        }

        public static string MakeRandomString(int pwdlen)
        {
            return MakeRandomString("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_*", pwdlen);
        }

        public static string MakeRandomString(string pwdchars, int pwdlen)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < pwdlen; i++)
            {
                int num = random.Next(pwdchars.Length);
                builder.Append(pwdchars[num]);
            }
            return builder.ToString();
        }

        public static string PELabelDecode(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("ζ#123;", "{");
                value = value.Replace("ζ#125;", "}");
            }
            return value;
        }

        public static string PELabelEncode(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("{", "ζ#123;");
                value = value.Replace("}", "ζ#125;");
                value = value.Replace("ζ#123;PE.SiteConfig.uploaddir/ζ#125;", "{PE.SiteConfig.uploaddir/}");
                value = value.Replace("ζ#123;PE.SiteConfig.ApplicationPath/ζ#125;", "{PE.SiteConfig.ApplicationPath/}");
            }
            return value;
        }

        public static string RandomNum()
        {
            return RandomNum(4);
        }

        public static string RandomNum(int intlong)
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < intlong; i++)
            {
                builder.Append(random.Next(10));
            }
            return builder.ToString();
        }

        public static string RngCspNum(int strLength)
        {
            if (strLength > 0)
            {
                strLength--;
            }
            else
            {
                strLength = 5;
            }
            byte[] data = new byte[strLength];
            new RNGCryptoServiceProvider().GetBytes(data);
            return BitConverter.ToInt32(data, 0).ToString(CultureInfo.CurrentCulture);
        }

        public static string Strings(string ichar, int length)
        {
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < length; i++)
            {
                builder.Append(ichar);
            }
            return builder.ToString();
        }

        public static string UrlEncode(object value)
        {
            if (value == null)
            {
                return null;
            }
            return UrlEncode(value.ToString());
        }

        public static string UrlEncode(string urlStr)
        {
            if (string.IsNullOrEmpty(urlStr))
            {
                return null;
            }
            return Regex.Replace(urlStr, @"[^a-zA-Z0-9,-_\.]+", new MatchEvaluator(DataSecurity.UrlEncodeMatch));
        }

        public static string UrlEncode(string weburl, bool isSEncode)
        {
            if (string.IsNullOrEmpty(weburl))
            {
                return null;
            }
            if (isSEncode)
            {
                return HttpUtility.UrlEncode(weburl);
            }
            return UrlEncode(weburl);
        }

        private static string UrlEncodeMatch(Match match)
        {
            string str = match.ToString();
            if (str.Length < 1)
            {
                return str;
            }
            StringBuilder builder = new StringBuilder();
            foreach (char ch in str)
            {
                if (ch > '\x007f')
                {
                    builder.AppendFormat("%u{0:X4}", (int) ch);
                }
                else
                {
                    builder.AppendFormat("%{0:X2}", (int) ch);
                }
            }
            return builder.ToString();
        }

        public static string XmlEncode(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("&", "&amp;");
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace("'", "&apos;");
                str = str.Replace("\"", "&quot;");
            }
            return str;
        }
    }
}


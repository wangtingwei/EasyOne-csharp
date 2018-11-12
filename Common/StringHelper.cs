namespace EasyOne.Common
{
    using Microsoft.VisualBasic;
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    /// <summary>
    /// 字符串处理帮助类
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 用逗号连接StringBuilder
        /// </summary>
        /// <param name="sb">the object of stringBuilder</param>
        /// <param name="append">the append string</param>
        public static void AppendString(StringBuilder sb, string append)
        {
            AppendString(sb, append, ",");
        }
        /// <summary>
        /// 用指定的符号将指定的String加入StringBuilder
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="append"></param>
        /// <param name="split"></param>
        public static void AppendString(StringBuilder sb, string append, string split)
        {
            if (sb.Length == 0)
            {
                sb.Append(append);
            }
            else
            {
                sb.Append(split);
                sb.Append(append);
            }
        }

        public static string Base64StringDecode(string input)
        {
            byte[] bytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string Base64StringEncode(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public static string CollectionFilter(string conStr, string tagName, int fType)
        {
            string input = conStr;
            switch (fType)
            {
                case 1:
                    return Regex.Replace(input, "<" + tagName + "([^>])*>", "", RegexOptions.IgnoreCase);

                case 2:
                    return Regex.Replace(input, "<" + tagName + "([^>])*>.*?</" + tagName + "([^>])*>", "", RegexOptions.IgnoreCase);

                case 3:
                    return Regex.Replace(Regex.Replace(input, "<" + tagName + "([^>])*>", "", RegexOptions.IgnoreCase), "</" + tagName + "([^>])*>", "", RegexOptions.IgnoreCase);
            }
            return input;
        }

        public static string CopyString(string returnStr)
        {
            if (returnStr.Contains("－复制"))
            {
                if (returnStr.Contains("－复制("))
                {
                    Regex regex = new Regex(@"^.*[/－]复制[/(](\d)+[/)]$");
                    if (regex.IsMatch(returnStr))
                    {
                        return (CopyStringNum(returnStr.Remove(returnStr.Length - 1)) + ")");
                    }
                    return (returnStr + "－复制");
                }
                return (returnStr + "(2)");
            }
            return (returnStr + "－复制");
        }

        public static string CopyStringNum(string returnStr)
        {
            int length = 0;
            foreach (char ch in returnStr)
            {
                if (char.IsDigit(ch))
                {
                    length++;
                }
                else
                {
                    length = 0;
                }
            }
            if (length == 0)
            {
                return (returnStr + "1");
            }
            int num2 = DataConverter.CLng(returnStr.Substring(returnStr.Length - length, length)) + 1;
            return (returnStr.Remove(returnStr.Length - length, length) + num2.ToString(CultureInfo.CurrentCulture));
        }

        public static string DecodeIP(long ip)
        {
            string[] strArray = new string[] { ((ip >> 0x18) & 0xffL).ToString(CultureInfo.CurrentCulture), ".", ((ip >> 0x10) & 0xffL).ToString(CultureInfo.CurrentCulture), ".", ((ip >> 8) & 0xffL).ToString(CultureInfo.CurrentCulture), ".", (ip & 0xffL).ToString(CultureInfo.CurrentCulture) };
            return string.Concat(strArray);
        }

        public static string DecodeLockIP(string lockIP)
        {
            StringBuilder builder = new StringBuilder(0x100);
            if (!string.IsNullOrEmpty(lockIP))
            {
                try
                {
                    string[] strArray = lockIP.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        string[] strArray2 = strArray[i].Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                        builder.Append(DecodeIP(Convert.ToInt64(strArray2[0], CultureInfo.CurrentCulture)) + "----" + DecodeIP(Convert.ToInt64(strArray2[1], CultureInfo.CurrentCulture)) + "\n");
                    }
                    return builder.ToString().TrimEnd(new char[] { '\n' });
                }
                catch (IndexOutOfRangeException)
                {
                    return builder.ToString();
                }
            }
            return builder.ToString();
        }

        public static double EncodeIP(string sip)
        {
            if (string.IsNullOrEmpty(sip))
            {
                return 0.0;
            }
            string[] strArray = sip.Split(new char[] { '.' });
            long num = 0L;
            foreach (string str in strArray)
            {
                byte num2;
                if (byte.TryParse(str, out num2))
                {
                    num = (num << 8) | num2;
                }
                else
                {
                    return 0.0;
                }
            }
            return (double) num;
        }

        public static string EncodeLockIP(string ipList)
        {
            StringBuilder builder = new StringBuilder(0x100);
            if (!string.IsNullOrEmpty(ipList.Trim()))
            {
                string[] strArray = ipList.Split(new char[] { '\n' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strArray[i]) && strArray[i].Contains("----"))
                    {
                        string[] strArray2 = strArray[i].Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                        if (strArray2.Length < 2)
                        {
                            throw new ArgumentException("请填写正确网站黑白名单中的IP地址！");
                        }
                        if (!DataValidator.IsIP(strArray2[0]) || !DataValidator.IsIP(strArray2[1]))
                        {
                            throw new ArgumentException("请填写正确网站黑白名单中的IP地址！");
                        }
                        if (i == 0)
                        {
                            builder.Append(EncodeIP(strArray2[0]) + "----" + EncodeIP(strArray2[1]));
                        }
                        else
                        {
                            builder.Append(string.Concat(new object[] { "$$$", EncodeIP(strArray2[0]), "----", EncodeIP(strArray2[1]) }));
                        }
                    }
                }
            }
            return builder.ToString();
        }

        public static string FilterScript(string conStr, string filterItem)
        {
            string str = conStr.Replace("\r", "{$Chr13}").Replace("\n", "{$Chr10}");
            foreach (string str2 in filterItem.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                switch (str2)
                {
                    case "Iframe":
                        str = CollectionFilter(str, str2, 2);
                        break;

                    case "Object":
                        str = CollectionFilter(str, str2, 2);
                        break;

                    case "Script":
                        str = CollectionFilter(str, str2, 2);
                        break;

                    case "Style":
                        str = CollectionFilter(str, str2, 2);
                        break;

                    case "Div":
                        str = CollectionFilter(str, str2, 3);
                        break;

                    case "Span":
                        str = CollectionFilter(str, str2, 3);
                        break;

                    case "Table":
                        str = CollectionFilter(CollectionFilter(CollectionFilter(CollectionFilter(CollectionFilter(str, str2, 3), "Tbody", 3), "Tr", 3), "Td", 3), "Th", 3);
                        break;

                    case "Img":
                        str = CollectionFilter(str, str2, 1);
                        break;

                    case "Font":
                        str = CollectionFilter(str, str2, 3);
                        break;

                    case "A":
                        str = CollectionFilter(str, str2, 3);
                        break;

                    case "Html":
                        str = StripTags(str);
                        goto Label_01FB;
                }
            }
        Label_01FB:
            return str.Replace("{$Chr13}", "\r").Replace("{$Chr10}", "\n");
        }

        public static bool FoundCharInArr(string checkStr, string findStr)
        {
            return FoundCharInArr(checkStr, findStr, ",");
        }

        public static bool FoundCharInArr(string checkStr, string findStr, string split)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(split))
            {
                split = ",";
            }
            if (!string.IsNullOrEmpty(checkStr))
            {
                if (string.IsNullOrEmpty(checkStr))
                {
                    return flag;
                }
                checkStr = split + checkStr + split;
                if (findStr.IndexOf(split, StringComparison.Ordinal) != -1)
                {
                    string[] strArray = findStr.Split(new char[] { Convert.ToChar(split, CultureInfo.CurrentCulture) });
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (checkStr.Contains(split + strArray[i] + split))
                        {
                            return true;
                        }
                    }
                    return flag;
                }
                if (checkStr.Contains(split + findStr + split))
                {
                    flag = true;
                }
            }
            return flag;
        }

        private static string GetGbkX(string input)
        {
            if (string.Compare(input, "吖", StringComparison.CurrentCulture) >= 0)
            {
                if (string.Compare(input, "八", StringComparison.CurrentCulture) < 0)
                {
                    return "A";
                }
                if (string.Compare(input, "嚓", StringComparison.CurrentCulture) < 0)
                {
                    return "B";
                }
                if (string.Compare(input, "咑", StringComparison.CurrentCulture) < 0)
                {
                    return "C";
                }
                if (string.Compare(input, "妸", StringComparison.CurrentCulture) < 0)
                {
                    return "D";
                }
                if (string.Compare(input, "发", StringComparison.CurrentCulture) < 0)
                {
                    return "E";
                }
                if (string.Compare(input, "旮", StringComparison.CurrentCulture) < 0)
                {
                    return "F";
                }
                if (string.Compare(input, "铪", StringComparison.CurrentCulture) < 0)
                {
                    return "G";
                }
                if (string.Compare(input, "讥", StringComparison.CurrentCulture) < 0)
                {
                    return "H";
                }
                if (string.Compare(input, "咔", StringComparison.CurrentCulture) < 0)
                {
                    return "J";
                }
                if (string.Compare(input, "垃", StringComparison.CurrentCulture) < 0)
                {
                    return "K";
                }
                if (string.Compare(input, "嘸", StringComparison.CurrentCulture) < 0)
                {
                    return "L";
                }
                if (string.Compare(input, "拏", StringComparison.CurrentCulture) < 0)
                {
                    return "M";
                }
                if (string.Compare(input, "噢", StringComparison.CurrentCulture) < 0)
                {
                    return "N";
                }
                if (string.Compare(input, "妑", StringComparison.CurrentCulture) < 0)
                {
                    return "O";
                }
                if (string.Compare(input, "七", StringComparison.CurrentCulture) < 0)
                {
                    return "P";
                }
                if (string.Compare(input, "亽", StringComparison.CurrentCulture) < 0)
                {
                    return "Q";
                }
                if (string.Compare(input, "仨", StringComparison.CurrentCulture) < 0)
                {
                    return "R";
                }
                if (string.Compare(input, "他", StringComparison.CurrentCulture) < 0)
                {
                    return "S";
                }
                if (string.Compare(input, "哇", StringComparison.CurrentCulture) < 0)
                {
                    return "T";
                }
                if (string.Compare(input, "夕", StringComparison.CurrentCulture) < 0)
                {
                    return "W";
                }
                if (string.Compare(input, "丫", StringComparison.CurrentCulture) < 0)
                {
                    return "X";
                }
                if (string.Compare(input, "帀", StringComparison.CurrentCulture) < 0)
                {
                    return "Y";
                }
                if (string.Compare(input, "咗", StringComparison.CurrentCulture) < 0)
                {
                    return "Z";
                }
            }
            return input;
        }

        public static string GetInitial(string str)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                builder.Append(GetOneIndex(str.Substring(i, 1)));
            }
            return builder.ToString();
        }

        private static string GetOneIndex(string input)
        {
            if ((Convert.ToChar(input, CultureInfo.CurrentCulture) >= '\0') && (Convert.ToChar(input, CultureInfo.CurrentCulture) < 'Ā'))
            {
                return input;
            }
            return GetGbkX(input);
        }

        public static bool IsIncludeChinese(string inputData)
        {
            Regex regex = new Regex("[一-龥]");
            return regex.Match(inputData).Success;
        }

        public static string MD5(string input)
        {
            using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
            {
                return BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "").ToLower(CultureInfo.CurrentCulture);
            }
        }

        public static int MD5D(string strText)
        {
            using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
            {
                byte[] bytes = Encoding.Default.GetBytes(strText);
                bytes = provider.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte num in bytes)
                {
                    builder.Append(num.ToString("D", CultureInfo.CurrentCulture).ToLower(CultureInfo.CurrentCulture));
                }
                string input = builder.ToString();
                if (input.Length >= 9)
                {
                    input = "9" + input.Substring(1, 8);
                }
                else
                {
                    input = "9" + input;
                }
                provider.Clear();
                return DataConverter.CLng(input);
            }
        }

        public static string MD5GB2312(string input)
        {
            using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
            {
                return BitConverter.ToString(provider.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(input))).Replace("-", "").ToLower(CultureInfo.CurrentCulture);
            }
        }

        public static string RemoveXss(string input)
        {
            string str;
            string str2;
            do
            {
                str = input;
                input = Regex.Replace(input, @"(&#*\w+)[\x00-\x20]+;", "$1;");
                input = Regex.Replace(input, "(&#x*[0-9A-F]+);*", "$1;", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, "&(amp|lt|gt|nbsp|quot);", "&amp;$1;");
                input = HttpUtility.HtmlDecode(input);
            }
            while (str != input);
            input = Regex.Replace(input, "(?<=(<[\\s\\S]*=\\s*\"[^\"]*))>(?=([^\"]*\"[\\s\\S]*>))", "&gt;", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"(?<=(<[\s\S]*=\s*'[^']*))>(?=([^']*'[\s\S]*>))", "&gt;", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"(?<=(<[\s\S]*=\s*`[^`]*))>(?=([^`]*`[\s\S]*>))", "&gt;", RegexOptions.IgnoreCase);
            do
            {
                str = input;
                input = Regex.Replace(input, @"(<[^>]+?style[\x00-\x20]*=[\x00-\x20]*[^>]*?)\\([^>]*>)", "$1/$2", RegexOptions.IgnoreCase);
            }
            while (str != input);
            input = Regex.Replace(input, @"[\x00-\x08\x0b-\x0c\x0e-\x19]", "");
            input = Regex.Replace(input, "(<[^>]+?[\\x00-\\x20\"'/])(on|xmlns)[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "([a-z]*)[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*)[\\x00-\\x20]*j[\\x00-\\x20]*a[\\x00-\\x20]*v[\\x00-\\x20]*a[\\x00-\\x20]*s[\\x00-\\x20]*c[\\x00-\\x20]*r[\\x00-\\x20]*i[\\x00-\\x20]*p[\\x00-\\x20]*t[\\x00-\\x20]*:", "$1=$2nojavascript...", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "([a-z]*)[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*)[\\x00-\\x20]*v[\\x00-\\x20]*b[\\x00-\\x20]*s[\\x00-\\x20]*c[\\x00-\\x20]*r[\\x00-\\x20]*i[\\x00-\\x20]*p[\\x00-\\x20]*t[\\x00-\\x20]*:", "$1=$2novbscript...", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"(<[^>]+style[\x00-\x20]*=[\x00-\x20]*[^>]*?)/\*[^>]*\*/([^>]*>)", "$1$2", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+?)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*?[eｅＥ][xｘＸ][pｐＰ][rｒＲ][eｅＥ][sｓＳ][sｓＳ][iｉＩ][oｏＯ][nｎＮ][\\x00-\\x20]*[\\(\\（][^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+?)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*?behaviour[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+?)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*?behavior[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+?)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*?position[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+?)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*?s[\\x00-\\x20]*c[\\x00-\\x20]*r[\\x00-\\x20]*i[\\x00-\\x20]*p[\\x00-\\x20]*t[\\x00-\\x20]*:*[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"</*\w+:\w[^>]*>", "　");
            do
            {
                str2 = input;
                input = Regex.Replace(input, "</*(applet|meta|xml|blink|link|style|script|embed|object|iframe|frame|frameset|ilayer|layer|bgsound|title|base)[^>]*>?", "　", RegexOptions.IgnoreCase);
            }
            while (str2 != input);
            return input;
        }

        public static string ReplaceChar(string source, char replaceChar, char newchar)
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(source))
            {
                return builder.ToString();
            }
            source = source.Trim();
            if (source.Contains(replaceChar.ToString()))
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] == replaceChar)
                    {
                        if ((i < (source.Length - 1)) && (source[i] != source[i + 1]))
                        {
                            builder.Append(newchar);
                        }
                    }
                    else
                    {
                        builder.Append(source[i]);
                    }
                }
            }
            else
            {
                builder.Append(source);
            }
            return builder.ToString().Trim();
        }

        public static string ReplaceIgnoreCase(string input, string oldValue, string newValue)
        {
            return Strings.Replace(input, oldValue, newValue, 1, -1, CompareMethod.Text);
        }

        public static string Sha1(string input)
        {
            using (SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider())
            {
                return BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "").ToLower(CultureInfo.CurrentCulture);
            }
        }

        public static string StripTags(string input)
        {
            Regex regex = new Regex("<([^<]|\n)+?>");
            return regex.Replace(input, "");
        }

        public static string SubString(string demand, int length, string substitute)
        {
            demand = DataSecurity.HtmlDecode(demand);
            if (Encoding.Default.GetBytes(demand).Length <= length)
            {
                return demand;
            }
            ASCIIEncoding encoding = new ASCIIEncoding();
            length -= Encoding.Default.GetBytes(substitute).Length;
            int num = 0;
            StringBuilder builder = new StringBuilder();
            byte[] bytes = encoding.GetBytes(demand);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num > length)
                {
                    break;
                }
                builder.Append(demand.Substring(i, 1));
            }
            builder.Append(substitute);
            return builder.ToString();
        }

        public static int SubStringLength(string demand)
        {
            if (string.IsNullOrEmpty(demand))
            {
                return 0;
            }
            ASCIIEncoding encoding = new ASCIIEncoding();
            int num = 0;
            byte[] bytes = encoding.GetBytes(demand);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3f)
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

        public static string Trim(string returnStr)
        {
            if (!string.IsNullOrEmpty(returnStr))
            {
                return returnStr.Trim();
            }
            return string.Empty;
        }

        public static bool ValidateMD5(string password, string md5Value)
        {
            if (string.Compare(password, md5Value, StringComparison.Ordinal) != 0)
            {
                return (string.Compare(password, md5Value.Substring(8, 0x10), StringComparison.Ordinal) == 0);
            }
            return true;
        }
    }
}


namespace EasyOne.Collection
{
    using EasyOne.Common;
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    public class CollectionCommon
    {
        private string m_CollectionMessage;

        public string ConvertToAbsluteUrl(string relativeAddress, string absoluteAddress)
        {
            string str = "";
            if (DataValidator.IsUrl(relativeAddress))
            {
                return relativeAddress;
            }
            if (DataValidator.IsUrl(absoluteAddress))
            {
                try
                {
                    Uri baseUri = new Uri(absoluteAddress);
                    str = new Uri(baseUri, relativeAddress).ToString();
                }
                catch (InvalidOperationException exception)
                {
                    this.m_CollectionMessage = exception.Message;
                }
            }
            return str;
        }

        public static string CreateKeyWord(string conStr, int num)
        {
            string str = "";
            if (string.IsNullOrEmpty(conStr))
            {
                return str;
            }
            if (num < 2)
            {
                num = 2;
            }
            conStr = conStr.Replace(Convert.ToChar(0x20).ToString(), "");
            conStr = conStr.Replace(Convert.ToChar(9).ToString(), "");
            conStr = conStr.Replace("&nbsp;".ToString(), "");
            conStr = conStr.Replace(" ".ToString(), "");
            conStr = conStr.Replace("(".ToString(), "");
            conStr = conStr.Replace(")".ToString(), "");
            conStr = conStr.Replace("<".ToString(), "");
            conStr = conStr.Replace(">".ToString(), "");
            if (num >= conStr.Length)
            {
                return ("|" + conStr.Substring(0, conStr.Length) + "|");
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < conStr.Length; i++)
            {
                if ((i + num) > conStr.Length)
                {
                    break;
                }
                builder.Append("|" + conStr.Substring(i, num));
            }
            str = builder.ToString();
            if (str.Length < 0xfe)
            {
                return (str + "|");
            }
            return (str.Substring(0, 0xfe) + "|");
        }

        public ArrayList GetArray(string code, string wordsBegin, string wordsEnd)
        {
            ArrayList list = new ArrayList();
            try
            {
                Regex regex = new Regex(wordsBegin + @"(?<title>[\s\S]+?)" + wordsEnd, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                for (Match match = regex.Match(code); match.Success; match = match.NextMatch())
                {
                    list.Add(match.Groups["title"].ToString());
                }
            }
            catch (ArgumentException exception)
            {
                this.m_CollectionMessage = exception.Message;
            }
            return list;
        }

        public string GetHttpPage(Uri url, string coding)
        {
            string str = "";
            if (string.IsNullOrEmpty(coding))
            {
                coding = "gb2312";
            }
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                str = new StreamReader(responseStream, Encoding.GetEncoding(coding)).ReadToEnd();
                responseStream.Close();
                response.Close();
            }
            catch (NotSupportedException exception)
            {
                this.m_CollectionMessage = exception.Message;
            }
            catch (InvalidOperationException exception2)
            {
                this.m_CollectionMessage = exception2.Message;
            }
            catch (IOException exception3)
            {
                this.m_CollectionMessage = exception3.Message;
            }
            return str;
        }

        public string GetInterceptionString(string conStr, string startStr, string overStr)
        {
            return this.GetInterceptionString(conStr, startStr, overStr, false, false);
        }

        public string GetInterceptionString(string conStr, string startStr, string overStr, bool incluL, bool incluR)
        {
            string str = "";
            if ((!string.IsNullOrEmpty(conStr) && !string.IsNullOrEmpty(startStr)) && !string.IsNullOrEmpty(overStr))
            {
                conStr = conStr.Replace("\r\n", "{$char(13)}").Replace("\n", "\r\n").Replace("{$char(13)}", "\r\n");
                try
                {
                    int startIndex = conStr.IndexOf(startStr, 1, StringComparison.Ordinal);
                    if (startIndex <= 0)
                    {
                        return str;
                    }
                    if (!incluL)
                    {
                        startIndex += startStr.Length;
                    }
                    int num2 = conStr.IndexOf(overStr, 1, StringComparison.Ordinal);
                    if (num2 <= 0)
                    {
                        return str;
                    }
                    if (incluR)
                    {
                        num2 += startStr.Length;
                    }
                    if ((startIndex > 0) && ((num2 - startIndex) > 0))
                    {
                        str = conStr.Substring(startIndex, num2 - startIndex);
                    }
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    this.m_CollectionMessage = exception.Message;
                }
            }
            return str;
        }

        public string GetPaing(string code, string wordsBegin, string wordsEnd)
        {
            string str = this.GetInterceptionString(code, wordsBegin, wordsEnd);
            if (string.IsNullOrEmpty(code))
            {
                code = code.Trim();
                code = code.Replace(" ", "%20");
                code = code.Replace(",", "");
                code = code.Replace("'", "");
                code = code.Replace("\"", "");
                code = code.Replace("<", "");
                code = code.Replace(">", "");
                code = code.Replace("&nbsp;", "");
            }
            return str;
        }

        public string CollectionMessage
        {
            get
            {
                return this.m_CollectionMessage;
            }
            set
            {
                this.m_CollectionMessage = value;
            }
        }
    }
}


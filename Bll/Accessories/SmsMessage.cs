namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public sealed class SmsMessage
    {
        private SmsMessage()
        {
        }

        public static string SendMessage(string sendNum, string sendContent, string sendType, string sendTime, string reserve)
        {
            SmsConfig smsConfig = SiteConfig.SmsConfig;
            string str = Guid.NewGuid().ToString();
            StringBuilder builder = new StringBuilder();
            builder.Append("ID=");
            builder.Append(str);
            builder.Append("&UserName=");
            builder.Append(smsConfig.UserName);
            builder.Append("&SendNum=");
            builder.Append(sendNum);
            builder.Append("&Content=");
            builder.Append(sendContent);
            builder.Append("&SendTiming=");
            builder.Append(sendType);
            builder.Append("&SendTime=");
            builder.Append(sendTime);
            builder.Append("&Reserve=");
            builder.Append(reserve);
            builder.Append("&MD5String=");
            builder.Append(StringHelper.MD5GB2312(str + smsConfig.UserName + smsConfig.MD5Key + sendNum + sendContent + sendType + sendTime));
            string s = builder.ToString();
            Uri requestUri = new Uri("http://sms.EasyOne.net/MessageGate2/MessageGate.aspx");
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUri);
            request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-silverlight, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, */*";
            request.Referer = "http://sms.EasyOne.net/MessageGate2/MessageGate.aspx";
            request.UserAgent = "Mozilla/4.0   (compatible;   MSIE   6.0;   Windows   NT   5.2;   SV1;   .NET   CLR   1.1.4322";
            request.Headers.Add("Accept-Language:   zh-cn");
            request.ServicePoint.Expect100Continue = false;
            byte[] bytes = Encoding.Default.GetBytes(s);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            Stream responseStream = ((HttpWebResponse) request.GetResponse()).GetResponseStream();
            Encoding encoding = Encoding.Default;
            StreamReader reader = new StreamReader(responseStream, encoding);
            string str3 = reader.ReadToEnd();
            reader.Close();
            return str3;
        }
    }
}


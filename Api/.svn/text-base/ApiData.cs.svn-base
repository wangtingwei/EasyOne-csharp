namespace EasyOne.Api
{
    using EasyOne.Common;
    using EasyOne.Components;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;

    public class ApiData
    {
        public const int Action = 1;
        public const int Address = 0x12;
        public const int Answer = 9;
        public const int Appid = 0;
        public const int Balance = 0x1a;
        public const int Birthday = 13;
        public const int City = 30;
        public const int Email = 7;
        public const int Experience = 0x17;
        public const int Gender = 12;
        public const int Homepage = 20;
        public const int Jointime = 0x16;
        private string m_API_Enable;
        private string m_API_Key;
        private string m_API_Timeout;
        private string m_API_Urls;
        private string m_ErrMsg = "";
        private bool m_FoundErr;
        private XmlDocument m_sMyXmlDoc = new XmlDocument();
        private string[,] m_sPE_Items = new string[0x20, 2];
        private string[] m_Urls;
        public const int Message = 4;
        public const int Mobile = 0x10;
        public const int Msn = 15;
        public const int Password = 6;
        public const int Posts = 0x1b;
        public const int Province = 0x1d;
        public const int QQ = 14;
        public const int Question = 8;
        private string RequestXml = (HttpContext.Current.Request.PhysicalApplicationPath + "/API/Request.xml");
        private string ResponseXml = (HttpContext.Current.Request.PhysicalApplicationPath + "/API/Response.xml");
        public const int Savecookie = 10;
        public const int Sex = 0x1f;
        public const int Status = 3;
        public const int Syskey = 2;
        public const int Telephone = 0x11;
        public const int Ticket = 0x18;
        public const int Truename = 11;
        public const int Userip = 0x15;
        public const int UserName = 5;
        public const int Userstatus = 0x1c;
        public const int Valuation = 0x19;
        public const int Zipcode = 0x13;

        public ApiData()
        {
            this.m_sPE_Items[0, 0] = "appid";
            this.m_sPE_Items[1, 0] = "action";
            this.m_sPE_Items[2, 0] = "syskey";
            this.m_sPE_Items[3, 0] = "status";
            this.m_sPE_Items[4, 0] = "message";
            this.m_sPE_Items[5, 0] = "username";
            this.m_sPE_Items[6, 0] = "password";
            this.m_sPE_Items[7, 0] = "email";
            this.m_sPE_Items[8, 0] = "question";
            this.m_sPE_Items[9, 0] = "answer";
            this.m_sPE_Items[10, 0] = "savecookie";
            this.m_sPE_Items[11, 0] = "truename";
            this.m_sPE_Items[12, 0] = "gender";
            this.m_sPE_Items[13, 0] = "birthday";
            this.m_sPE_Items[14, 0] = "qq";
            this.m_sPE_Items[15, 0] = "msn";
            this.m_sPE_Items[0x10, 0] = "mobile";
            this.m_sPE_Items[0x11, 0] = "telephone";
            this.m_sPE_Items[0x12, 0] = "address";
            this.m_sPE_Items[0x13, 0] = "zipcode";
            this.m_sPE_Items[20, 0] = "homepage";
            this.m_sPE_Items[0x15, 0] = "userip";
            this.m_sPE_Items[0x16, 0] = "jointime";
            this.m_sPE_Items[0x17, 0] = "experience";
            this.m_sPE_Items[0x18, 0] = "ticket";
            this.m_sPE_Items[0x19, 0] = "valuation";
            this.m_sPE_Items[0x1a, 0] = "balance";
            this.m_sPE_Items[0x1b, 0] = "posts";
            this.m_sPE_Items[0x1c, 0] = "userstatus";
            this.m_sPE_Items[0x1d, 0] = "province";
            this.m_sPE_Items[30, 0] = "city";
            this.m_sPE_Items[0x1f, 0] = "sex";
            this.m_sPE_Items[0, 1] = "EasyOne";
            this.m_sPE_Items[1, 1] = "";
            this.m_sPE_Items[2, 1] = "";
            this.m_sPE_Items[3, 1] = "0";
            this.m_sPE_Items[4, 1] = "操作已成功完成！";
            this.m_sPE_Items[5, 1] = "";
            this.m_sPE_Items[6, 1] = "";
            this.m_sPE_Items[7, 1] = "";
            this.m_sPE_Items[8, 1] = "";
            this.m_sPE_Items[9, 1] = "";
            this.m_sPE_Items[10, 1] = "";
            this.m_sPE_Items[11, 1] = "";
            this.m_sPE_Items[12, 1] = "";
            this.m_sPE_Items[13, 1] = "";
            this.m_sPE_Items[14, 1] = "";
            this.m_sPE_Items[15, 1] = "";
            this.m_sPE_Items[0x10, 1] = "";
            this.m_sPE_Items[0x11, 1] = "";
            this.m_sPE_Items[0x12, 1] = "";
            this.m_sPE_Items[0x13, 1] = "";
            this.m_sPE_Items[20, 1] = "";
            this.m_sPE_Items[0x15, 1] = "";
            this.m_sPE_Items[0x16, 1] = "";
            this.m_sPE_Items[0x17, 1] = "";
            this.m_sPE_Items[0x18, 1] = "";
            this.m_sPE_Items[0x19, 1] = "";
            this.m_sPE_Items[0x1a, 1] = "";
            this.m_sPE_Items[0x1b, 1] = "";
            this.m_sPE_Items[0x1c, 1] = "";
            this.m_sPE_Items[0x1d, 1] = "";
            this.m_sPE_Items[30, 1] = "";
            this.m_sPE_Items[0x1f, 1] = "";
            XmlNodeList childNodes = GetNode().ChildNodes;
            this.m_API_Enable = childNodes.Item(0).ChildNodes[1].InnerText.ToLower(CultureInfo.CurrentCulture);
            this.m_API_Key = childNodes.Item(1).ChildNodes[1].InnerText;
            this.m_API_Timeout = childNodes.Item(2).ChildNodes[1].InnerText;
            this.m_API_Urls = childNodes.Item(3).ChildNodes[1].InnerText;
            this.m_Urls = this.ApiUrls.Split(new char[] { '|' });
        }

        public static int ExchangeGender(string iSex)
        {
            if (!string.IsNullOrEmpty(iSex) && DataValidator.IsNumber(iSex))
            {
                if (iSex == "1")
                {
                    return 1;
                }
                if (iSex == "0")
                {
                    return 2;
                }
            }
            return 0;
        }

        public static int ExchangStatus(string status)
        {
            if (!string.IsNullOrEmpty(status) && DataValidator.IsNumber(status))
            {
                if (status == "1")
                {
                    return 1;
                }
                if (status == "2")
                {
                    return 2;
                }
                if (status == "4")
                {
                    return 4;
                }
                if (status == "8")
                {
                    return 8;
                }
            }
            return 0;
        }

        public static int GenderToDV(string sex)
        {
            switch (sex)
            {
                case "Female":
                    return 0;

                case "Male":
                    return 1;
            }
            return 1;
        }

        private static XmlNode GetNode()
        {
            XmlDocument document = SiteCache.Get("CK_System_XmlDocument_API_EnableConfig") as XmlDocument;
            if (document == null)
            {
                string str;
                document = new XmlDocument();
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    str = current.Server.MapPath("~/API/API.Config");
                }
                else
                {
                    str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/Security.config");
                }
                try
                {
                    document.Load(str);
                }
                catch (XmlException exception)
                {
                    HttpContext.Current.Items["ErrorMessage"] = "API.Config配置文件不符合XML规范，具体错误信息：" + exception.Message;
                    HttpContext.Current.Server.Transfer("~/Prompt/ShowError.aspx");
                }
                SiteCache.Insert("CK_System_XmlDocument_API_EnableConfig", document, new CacheDependency(str));
            }
            XmlNode node = document.SelectSingleNode("Config/APISettings");
            if (node == null)
            {
                HttpContext.Current.Items["ErrorMessage"] = "Security.config配置文件不存在checkPermissions根元素";
                HttpContext.Current.Server.Transfer("~/Prompt/ShowError.aspx");
            }
            return node;
        }

        public string GetNodeText(string strNodeName)
        {
            if (!string.IsNullOrEmpty(strNodeName) && this.IsNode(strNodeName))
            {
                return this.m_sMyXmlDoc.DocumentElement.SelectSingleNode(strNodeName).InnerText;
            }
            return "";
        }

        public static bool IsAPiEnable()
        {
            return (GetNode().ChildNodes.Item(0)["Value"].InnerText.ToLower(CultureInfo.CurrentCulture) == "true");
        }

        public bool IsNode(string strNodeName)
        {
            if (string.IsNullOrEmpty(strNodeName))
            {
                return false;
            }
            if (this.m_sMyXmlDoc.DocumentElement.SelectSingleNode(strNodeName) == null)
            {
                return false;
            }
            return true;
        }

        public void PrepareData(bool vIsQuest)
        {
            for (int i = 0; i < this.m_sPE_Items.GetLength(0); i++)
            {
                if (vIsQuest)
                {
                    if ((i != 3) || (i != 4))
                    {
                        this.m_sPE_Items[i, 1] = this.GetNodeText(this.m_sPE_Items[i, 0]);
                    }
                }
                else if (((i != 2) || (i != 5)) || (i != 6))
                {
                    this.m_sPE_Items[i, 1] = this.GetNodeText(this.m_sPE_Items[i, 0]);
                }
            }
        }

        public void PrepareXml(bool vIsQuest)
        {
            string requestXml;
            if (vIsQuest)
            {
                requestXml = this.RequestXml;
            }
            else
            {
                requestXml = this.ResponseXml;
            }
            try
            {
                this.m_sMyXmlDoc.Load(requestXml);
            }
            catch (XmlException exception)
            {
                this.m_FoundErr = true;
                this.m_ErrMsg = "加载XML模版文件出错！" + exception.Message.ToString(CultureInfo.CurrentCulture);
                return;
            }
            for (int i = 0; i < this.m_sPE_Items.GetLength(0); i++)
            {
                if (vIsQuest)
                {
                    if ((i != 3) || (i != 4))
                    {
                        this.SetNodeText(this.m_sPE_Items[i, 0], this.m_sPE_Items[i, 1]);
                    }
                }
                else if (((i != 1) || (i != 2)) || (i != 5))
                {
                    this.SetNodeText(this.m_sPE_Items[i, 0], this.m_sPE_Items[i, 1]);
                }
            }
        }

        public void RollbackUser(int index)
        {
            index--;
            while (index > 0)
            {
                this.SetNodeText("action", "delete");
                Uri requestUri = new Uri(this.m_Urls[index]);
                WebRequest request = WebRequest.Create(requestUri);
                request.Method = "POST";
                byte[] bytes = Encoding.Default.GetBytes(this.m_sMyXmlDoc.InnerXml);
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                index--;
            }
        }

        public void SendPost()
        {
            int index = 0;
            XmlDocument document = new XmlDocument();
            this.m_sPE_Items[5, 1] = this.GetNodeText(this.m_sPE_Items[5, 0]);
            this.m_sPE_Items[2, 1] = StringHelper.MD5GB2312(this.m_sPE_Items[5, 1] + this.ApiKey).Substring(8, 0x10);
            this.SetNodeText(this.m_sPE_Items[2, 0], this.m_sPE_Items[2, 1]);
            for (index = 0; index < this.m_Urls.GetLength(0); index++)
            {
                if (!string.IsNullOrEmpty(this.m_Urls[index]))
                {
                    Uri requestUri = new Uri(this.m_Urls[index]);
                    WebRequest request = WebRequest.Create(requestUri);
                    request.Method = "POST";
                    request.Credentials = CredentialCache.DefaultCredentials;
                    request.Timeout = int.Parse(this.m_API_Timeout, CultureInfo.CurrentCulture);
                    byte[] bytes = Encoding.Default.GetBytes(this.m_sMyXmlDoc.InnerXml);
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                    string xml = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("gb2312")).ReadToEnd();
                    try
                    {
                        document.LoadXml(xml);
                        string innerText = document.DocumentElement.SelectSingleNode("status").InnerText;
                        string str3 = document.DocumentElement.SelectSingleNode("body/message").InnerText;
                        if (innerText != "0")
                        {
                            this.m_FoundErr = true;
                            this.m_ErrMsg = str3;
                        }
                    }
                    catch (Exception exception)
                    {
                        this.m_FoundErr = true;
                        this.m_ErrMsg = "用户服务目前不可用" + exception.Message.ToString(CultureInfo.CurrentCulture);
                    }
                }
                if (this.m_FoundErr && (index > 0))
                {
                    this.RollbackUser(index);
                    return;
                }
            }
        }

        public void SetNodeText(string strNodeName, string strNodeText)
        {
            strNodeName = strNodeName.ToLower(CultureInfo.CurrentCulture);
            if ((!string.IsNullOrEmpty(strNodeName) && !string.IsNullOrEmpty(strNodeText)) && this.IsNode(strNodeName))
            {
                this.m_sMyXmlDoc.DocumentElement.SelectSingleNode(strNodeName).InnerText = strNodeText;
            }
        }

        public void WriteErrXml()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "gb2312";
            HttpContext.Current.Response.AddHeader("contenttype", "text/xml; charset=gb2312");
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            HttpContext.Current.Response.ContentType = "text/xml; charset=gb2312";
            HttpContext.Current.Response.Write("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
            HttpContext.Current.Response.Write("<root><appid>EasyOne</appid><status>1</status><body><message>" + this.m_ErrMsg + "</message></body></root>");
            HttpContext.Current.Response.End();
        }

        public void WriteXml()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "gb2312";
            HttpContext.Current.Response.AddHeader("contenttype", "text/xml; charset=gb2312");
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            HttpContext.Current.Response.ContentType = "text/xml; charset=gb2312";
            HttpContext.Current.Response.Write(this.m_sMyXmlDoc.InnerXml);
            HttpContext.Current.Response.End();
        }

        public string ApiEnable
        {
            get
            {
                return this.m_API_Enable;
            }
            set
            {
                this.m_API_Enable = value;
            }
        }

        public string ApiKey
        {
            get
            {
                return this.m_API_Key;
            }
            set
            {
                this.m_API_Key = value;
            }
        }

        public string ApiTimeout
        {
            get
            {
                return this.m_API_Timeout;
            }
            set
            {
                this.m_API_Timeout = value;
            }
        }

        public string ApiUrls
        {
            get
            {
                return this.m_API_Urls;
            }
            set
            {
                this.m_API_Urls = value;
            }
        }

        public string ErrMsg
        {
            get
            {
                return this.m_ErrMsg;
            }
            set
            {
                this.m_ErrMsg = value;
            }
        }

        public bool FoundErr
        {
            get
            {
                return this.m_FoundErr;
            }
            set
            {
                this.m_FoundErr = value;
            }
        }

        public XmlDocument MyXmlDoc
        {
            get
            {
                return this.m_sMyXmlDoc;
            }
            set
            {
                this.m_sMyXmlDoc = value;
            }
        }

        public string[,] SpeItems
        {
            get
            {
                return this.m_sPE_Items;
            }
            set
            {
                this.m_sPE_Items = value;
            }
        }

        public string[] Urls
        {
            get
            {
                return this.m_Urls;
            }
            set
            {
                this.m_Urls = value;
            }
        }
    }
}


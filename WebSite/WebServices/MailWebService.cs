namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Enumerations;
    using System;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Script.Services;
    using System.Web.Services;
    using System.Xml;

    [WebService(Namespace="http://tempuri.org/"), WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1), ScriptService]
    public class MailWebService : WebService
    {
        [WebMethod]
        public CommonMailServerConfig GetMailConfig(string txtMailFrom)
        {
            CommonMailServerConfig config = null;
            Regex regex = new Regex(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$");
            if (regex.IsMatch(txtMailFrom))
            {
                config = new CommonMailServerConfig();
                XmlDocument document = new XmlDocument();
                document.Load(HttpContext.Current.Server.MapPath("~/Config/CommonMailServer.config"));
                XmlNodeList list = document.SelectNodes("configuration/CommonMailServer/MailConfig");
                if (list == null)
                {
                    return config;
                }
                foreach (XmlNode node in list)
                {
                    if (!(node.FirstChild.InnerText == txtMailFrom.Substring(txtMailFrom.IndexOf("@", StringComparison.Ordinal))))
                    {
                        continue;
                    }
                    config.MailFrom = node.SelectSingleNode("MailFrom").InnerXml;
                    config.EnabledSsl = bool.Parse(node.SelectSingleNode("EnabledSsl").InnerXml);
                    config.Port = int.Parse(node.SelectSingleNode("Port").InnerXml);
                    config.MailServer = node.SelectSingleNode("MailServer").InnerXml;
                    string innerXml = node.SelectSingleNode("AuthenticationType").InnerXml;
                    if (innerXml == null)
                    {
                        goto Label_0140;
                    }
                    if (!(innerXml == "Basic"))
                    {
                        if (innerXml == "None")
                        {
                            goto Label_0137;
                        }
                        goto Label_0140;
                    }
                    config.AuthenticationType = AuthenticationType.Basic;
                    goto Label_0147;
                Label_0137:
                    config.AuthenticationType = AuthenticationType.None;
                    goto Label_0147;
                Label_0140:
                    config.AuthenticationType = AuthenticationType.Ntlm;
                Label_0147:
                    config.MailServerUserName = node.SelectSingleNode("MailServerUserName").InnerXml.Replace("{$UserName}", txtMailFrom.Substring(0, txtMailFrom.IndexOf("@", StringComparison.Ordinal)));
                    return config;
                }
            }
            return config;
        }
    }
}


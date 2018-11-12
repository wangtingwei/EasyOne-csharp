namespace EasyOne.Web
{
    using EasyOne.Components;
    using System;
    using System.Web;

    public class AdminMapProvider : XmlSiteMapProvider
    {
        private string m_AdminPath;

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            string str2;
            if (string.IsNullOrEmpty(rawUrl))
            {
                return null;
            }
            this.m_AdminPath = GetAdminPath(rawUrl);
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath == "/")
            {
                str2 = rawUrl.Replace(this.m_AdminPath, "/Admin/");
            }
            else
            {
                str2 = rawUrl.Replace(applicationPath + this.m_AdminPath, applicationPath + "/Admin/");
            }
            SiteMapNode node = base.FindSiteMapNode(str2);
            if (node == null)
            {
                string[] strArray = str2.Split(new char[] { '?' });
                if (strArray.Length >= 2)
                {
                    string str3 = strArray[0];
                    string[] strArray2 = strArray[1].Split(new char[] { '&' });
                    for (int i = 0; i < strArray2.Length; i++)
                    {
                        node = base.FindSiteMapNode(str3 + "?" + strArray2[i]);
                        if (node != null)
                        {
                            break;
                        }
                    }
                    if (node == null)
                    {
                        node = base.FindSiteMapNode(str3);
                    }
                }
            }
            if (node != null)
            {
                lock (node)
                {
                    node.ReadOnly = false;
                    node.Url = rawUrl;
                    node.Title = node.Title.Replace("点券", SiteConfig.UserConfig.PointName);
                    node.Description = node.Description.Replace("点券", SiteConfig.UserConfig.PointName);
                    node.ReadOnly = true;
                }
            }
            return node;
        }

        private static string GetAdminPath(string rawUrl)
        {
            string str2 = "/" + SiteConfig.SiteOption.ManageDir + "/";
            int index = rawUrl.IndexOf(str2, StringComparison.CurrentCultureIgnoreCase);
            if (index == -1)
            {
                return str2;
            }
            return rawUrl.Substring(index, str2.Length);
        }

        public override SiteMapNode GetParentNode(SiteMapNode node)
        {
            SiteMapNode parentNode = base.GetParentNode(node);
            if ((parentNode != null) && !string.IsNullOrEmpty(parentNode.Url))
            {
                parentNode.ReadOnly = false;
                parentNode.Url = parentNode.Url.Replace("/Admin/", this.m_AdminPath);
                parentNode.ReadOnly = true;
            }
            return parentNode;
        }
    }
}


namespace EasyOne.Web
{
    using EasyOne.Components;
    using System;
    using System.Web;

    public class UserMapProvider : XmlSiteMapProvider
    {
        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            SiteMapNode node = base.FindSiteMapNode(rawUrl);
            if (node == null)
            {
                string[] strArray = rawUrl.Split(new char[] { '?' });
                if (strArray.Length >= 2)
                {
                    string str = strArray[0];
                    string[] strArray2 = strArray[1].Split(new char[] { '&' });
                    for (int i = 0; i < strArray2.Length; i++)
                    {
                        node = base.FindSiteMapNode(str + "?" + strArray2[i]);
                        if (node != null)
                        {
                            return node;
                        }
                    }
                }
                return node;
            }
            node.ReadOnly = false;
            node.Url = rawUrl;
            node.Title = node.Title.Replace("点券", SiteConfig.UserConfig.PointName);
            node.Description = node.Description.Replace("点券", SiteConfig.UserConfig.PointName);
            node.ReadOnly = true;
            return node;
        }
    }
}


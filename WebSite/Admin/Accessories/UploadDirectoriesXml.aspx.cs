namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web.UI.HtmlControls;

    public partial class UploadDirectoriesXml : AdminPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            XTreeCollection trees = new XTreeCollection();
            string uploadDir = SiteConfig.SiteOption.UploadDir;
            string str2 = base.Request["Dir"];
            str2 = uploadDir + str2 + "/";
            DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + str2);
            foreach (DirectoryInfo info2 in info.GetDirectories())
            {
                string str3 = "fileManage.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"] + "/" + info2.Name);
                XTreeItem item = new XTreeItem();
                item.Text = info2.Name;
                item.ArrModelId = "";
                item.ArrModelName = "";
                item.Icon = "";
                item.NodeId = "";
                item.Target = "main_right";
                item.Expand = "0";
                item.Enable = "1";
                if (info2.GetDirectories().Length > 0)
                {
                    item.XmlSrc = "UploadDirectoriesXML.aspx?Dir=" + base.Server.UrlEncode(base.Request["Dir"] + "/" + info2.Name);
                }
                item.Action = str3;
                trees.Add(item);
            }
            base.Response.Write(trees.ToString());
            base.Response.End();
        }
    }
}


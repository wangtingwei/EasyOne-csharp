namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web.UI.HtmlControls;

    public partial class TemplateDirectoriesXml : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string str2;
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            XTreeCollection xTreeList = new XTreeCollection();
            if (((str2 = BasePage.RequestStringToLower("Action")) != null) && (str2 == "templateselect"))
            {
                this.TemplateSelect(xTreeList);
            }
            else
            {
                this.TemplateManage(xTreeList);
            }
            base.Response.Write(xTreeList.ToString());
            base.Response.End();
        }

        private void TemplateManage(XTreeCollection xTreeList)
        {
            string str = base.BasePath + SiteConfig.SiteOption.ManageDir;
            string templateDir = SiteConfig.SiteOption.TemplateDir;
            string str3 = base.Request["Dir"];
            str3 = templateDir + str3 + "/";
            DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + str3);
            foreach (DirectoryInfo info2 in info.GetDirectories())
            {
                if ((info2.Name != "标签库") && (info2.Name != "分页标签库"))
                {
                    string str4 = str + "/Template/TemplateManage.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"] + "/" + info2.Name);
                    XTreeItem item = new XTreeItem();
                    item.Text = info2.Name;
                    item.ArrModelId = "";
                    item.ArrModelName = "";
                    item.Icon = "";
                    item.NodeId = "";
                    item.Target = "main_right";
                    item.Expand = "0";
                    item.AnchorType = "1";
                    if (info2.GetDirectories().Length > 0)
                    {
                        item.XmlSrc = str + "/Template/TemplateDirectoriesXML.aspx?Dir=" + base.Server.UrlEncode(base.Request["Dir"] + "/" + info2.Name);
                    }
                    item.Action = str4;
                    xTreeList.Add(item);
                }
            }
        }

        private void TemplateSelect(XTreeCollection xTreeList)
        {
            string str = base.BasePath + SiteConfig.SiteOption.ManageDir;
            string templateDir = SiteConfig.SiteOption.TemplateDir;
            string str3 = base.Request["Dir"];
            str3 = templateDir + str3 + "/";
            DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + str3);
            foreach (DirectoryInfo info2 in info.GetDirectories())
            {
                if ((info2.Name != "标签库") && (info2.Name != "分页标签库"))
                {
                    string str4 = str + "/Accessories/ShowTemplates.aspx?FilesDir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"] + "/" + info2.Name);
                    XTreeItem item = new XTreeItem();
                    item.Text = info2.Name;
                    item.ArrModelId = "";
                    item.ArrModelName = "";
                    item.Icon = "";
                    item.NodeId = "";
                    item.Target = "main_right";
                    item.Expand = "0";
                    item.AnchorType = "1";
                    if (info2.GetDirectories().Length > 0)
                    {
                        item.XmlSrc = str + "/Template/TemplateDirectoriesXML.aspx?Action=TemplateSelect&Dir=" + base.Server.UrlEncode(base.Request["Dir"] + "/" + info2.Name);
                    }
                    item.Action = str4;
                    xTreeList.Add(item);
                }
            }
        }
    }
}


namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class NodeTree : DynamicPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.XLoadNodeTree.RootText = SiteConfig.SiteInfo.SiteName;
            this.XLoadNodeTree.RootAction = "ContentManage.aspx";
            this.XLoadNodeTree.XmlSrc = "NodeXml.aspx?Action=Content";
        }
    }
}


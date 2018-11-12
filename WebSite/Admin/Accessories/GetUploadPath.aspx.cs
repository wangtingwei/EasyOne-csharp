namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class GetUploadPath : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int nodeId = BasePage.RequestInt32("NodeId");
            this.Session["EasyOne:NodeId"] = nodeId;
            if (nodeId != 0)
            {
                string uploadFilePathRule = "";
                NodeInfo cacheNodeById = Nodes.GetCacheNodeById(nodeId);
                if (SiteConfig.SiteOption.EnableUploadFiles)
                {
                    uploadFilePathRule = "/" + SiteConfig.SiteOption.UploadFilePathRule;
                    uploadFilePathRule = Nodes.ResolveUploadDir(cacheNodeById, uploadFilePathRule);
                    this.Session["EasyOne:UserFilesPath"] = uploadFilePathRule;
                }
            }
        }
    }
}


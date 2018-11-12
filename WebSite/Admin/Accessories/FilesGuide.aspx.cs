namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using EasyOne.ModelControls;

    public partial class FilesGuide : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TrvUploadDir.FileDirectory = SiteConfig.SiteOption.UploadDir;
            string uploadDir = SiteConfig.SiteOption.UploadDir;
            int startIndex = uploadDir.LastIndexOf("/", StringComparison.Ordinal) + 1;
            string str2 = "";
            if (startIndex >= 0)
            {
                str2 = uploadDir.Substring(startIndex, uploadDir.Length - startIndex);
            }
            else
            {
                str2 = uploadDir;
            }
            this.TrvUploadDir.RootNodeName = str2;
        }
    }
}


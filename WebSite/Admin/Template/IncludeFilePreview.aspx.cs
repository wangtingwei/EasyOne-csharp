namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Templates;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class IncludeFilePreview : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            IncludeFileInfo includeFileInfoById = IncludeFile.GetIncludeFileInfoById(BasePage.RequestInt32("id"));
            if (!includeFileInfoById.IsNull)
            {
                this.LitName.Text = includeFileInfoById.Name;
                switch (includeFileInfoById.IncludeType)
                {
                    case IncludeType.None:
                        return;

                    case IncludeType.JSWriteHtml:
                    case IncludeType.JS:
                        this.LitPreview.Text = "<script type=\"text/javascript\" src=\"" + base.BasePath + SiteConfig.SiteOption.IncludeFilePath + "/" + includeFileInfoById.FileName + "\"></script>";
                        return;

                    case IncludeType.Html:
                        this.LitPreview.Text = "<iframe frameborder=\"0\" width=\"100%\" height=\"400px\" src=\"" + base.BasePath + SiteConfig.SiteOption.IncludeFilePath + "/" + includeFileInfoById.FileName + "\" scrolling=\"auto\" id=\"includeFile\"></iframe>";
                        return;
                }
            }
            else
            {
                AdminPage.WriteErrMsg("您修改的内嵌代码不存在！", "IncludeFileManage.aspx");
            }
        }
    }
}


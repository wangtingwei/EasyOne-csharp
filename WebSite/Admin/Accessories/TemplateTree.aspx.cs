namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class TemplateTree : AdminPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            this.TrvTemplateDir.FileDirectory = SiteConfig.SiteOption.TemplateDir;
            string templateDir = SiteConfig.SiteOption.TemplateDir;
            int startIndex = templateDir.LastIndexOf("/", StringComparison.Ordinal) + 1;
            string str2 = "";
            if (startIndex >= 0)
            {
                str2 = templateDir.Substring(startIndex, templateDir.Length - startIndex);
            }
            else
            {
                str2 = templateDir;
            }
            this.TrvTemplateDir.RootNodeName = str2;
            string str3 = base.BasePath + SiteConfig.SiteOption.ManageDir;
            this.TrvTemplateDir.DirectoriesXmlUrl = str3 + "/Template/TemplateDirectoriesXML.aspx?Action=TemplateSelect&Dir=";
        }
    }
}


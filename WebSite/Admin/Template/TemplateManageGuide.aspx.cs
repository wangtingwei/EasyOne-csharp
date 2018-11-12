namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web.UI.WebControls;

    public partial class TemplateManageGuide : AdminPage
    {

        private void DrpFilePath(ListControl dropName)
        {
            string templateDir = SiteConfig.SiteOption.TemplateDir;
            if (!string.IsNullOrEmpty(templateDir))
            {
                DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + templateDir);
                if (info.Exists)
                {
                    foreach (DirectoryInfo info2 in info.GetDirectories("*", SearchOption.AllDirectories))
                    {
                        if (!info2.FullName.Contains("标签库") && !info2.FullName.Contains("分页标签库"))
                        {
                            string text = (info2.FullName.Remove(0, info.FullName.Length) + "/").Replace(@"\", "/");
                            dropName.Items.Add(new ListItem(text, text));
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
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
                this.DrpFilePath(this.DropSearchFile);
            }
        }
    }
}


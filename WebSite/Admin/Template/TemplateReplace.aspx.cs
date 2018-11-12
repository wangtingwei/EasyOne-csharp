namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web.UI.WebControls;

    public partial class TemplateReplace : AdminPage
    {

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("TemplateManage.aspx?Dir=" + base.Server.UrlEncode(BasePage.RequestString("Dir")));
        }

        protected void BtnReplace_Click(object sender, EventArgs e)
        {
            string text = this.TxtOriginalContent.Text;
            string newContent = this.TxtNewContent.Text;
            string selectedValue = this.DropReplaceFile.SelectedValue;
            FileSystemObject.ReplaceFileContent(base.Request.PhysicalApplicationPath + (SiteConfig.SiteOption.TemplateDir + selectedValue + "/"), text, newContent);
            AdminPage.WriteSuccessMsg("在" + selectedValue + "目录范围内的模板文件，将文件内容中的“" + text + "”批量替换成 “" + newContent + "”执行完毕！", "TemplateManage.aspx?Dir=" + base.Server.UrlEncode(BasePage.RequestString("Dir")));
        }

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
                this.DrpFilePath(this.DropReplaceFile);
                string str = BasePage.RequestString("Dir");
                if (!string.IsNullOrEmpty(str))
                {
                    this.DropReplaceFile.SelectedValue = str + "/";
                }
            }
        }
    }
}


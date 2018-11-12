namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class TemplateUpload : AdminPage
    {

        protected void EBtnUpload_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid && this.FileUpload1.HasFile)
            {
                if (Path.GetExtension(this.FileUpload1.FileName) == ".html")
                {
                    string filename = BasePage.RequestString("Dir");
                    filename = SiteConfig.SiteOption.TemplateDir + filename;
                    filename = (base.Request.PhysicalApplicationPath + filename).Replace("/", @"\") + @"\" + this.FileUpload1.FileName;
                    this.FileUpload1.SaveAs(filename);
                    AdminPage.WriteSuccessMsg("上传成功", "javascript:opener.location.reload();window.close();");
                }
                else
                {
                    AdminPage.WriteErrMsg("请上传.html文件");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("Dir");
            this.LblCurrentDir.Text = str;
            if (string.IsNullOrEmpty(str))
            {
                this.LblCurrentDir.Text = "/" + str;
            }
        }
    }
}


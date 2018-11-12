namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class StyleSheetsUpload : AdminPage
    {

        protected void EBtnUpload_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid && this.FileUpload1.HasFile)
            {
                StringCollection strings = new StringCollection();
                strings.Add(".css");
                strings.Add(".gif");
                strings.Add(".jpg");
                strings.Add(".png");
                strings.Add(".bmp");
                string str = Path.GetExtension(this.FileUpload1.FileName).ToLower();
                if (strings.Contains(str))
                {
                    string filename = BasePage.RequestString("Dir");
                    filename = "/Skin" + filename;
                    filename = (base.Request.PhysicalApplicationPath + filename).Replace("/", @"\") + @"\" + this.FileUpload1.FileName;
                    this.FileUpload1.SaveAs(filename);
                    AdminPage.WriteSuccessMsg("上传成功", "javascript:opener.location.reload();window.close();");
                }
                else
                {
                    AdminPage.WriteErrMsg("请上传css|gif|jpg|png|bmp文件");
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


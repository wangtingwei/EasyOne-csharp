namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web.UI.WebControls;

    public partial class StyleSheets : AdminPage
    {
        protected string currentPath;
        private string m_action;
        protected string skinPath = "/Skin";

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                try
                {
                    if (this.m_action == "Modify")
                    {
                        FileSystemObject.WriteFile(this.currentPath, this.EditorContent.Text);
                        AdminPage.WriteSuccessMsg("<li>保存风格数据成功！</li>", this.ViewState["UrlReferrer"].ToString());
                    }
                    if (this.m_action == "Add")
                    {
                        this.currentPath = this.currentPath + @"\" + this.TxtFileName.Text;
                        FileInfo info = new FileInfo(this.currentPath);
                        if (info.Exists)
                        {
                            this.savefilename.Text = "文件已存在，请更改文件名";
                        }
                        else
                        {
                            FileSystemObject.WriteFile(this.currentPath, this.EditorContent.Text);
                            AdminPage.WriteSuccessMsg("<li>保存风格数据成功！</li>", this.ViewState["UrlReferrer"].ToString());
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    AdminPage.WriteErrMsg("文件未找到");
                }
                catch (UnauthorizedAccessException)
                {
                    AdminPage.WriteErrMsg("<li>访问文件失败！检查您的服务器是否给风格文件夹写入权限。</li>");
                }
            }
        }

        protected void InitPage()
        {
            this.m_action = BasePage.RequestString("Action", "Add");
            this.ValeTxtFileName.ValidationExpression = "^[^\\/ :*?\"<>|.]+\\.css$";
            string str = BasePage.RequestString("Dir").Replace("..", string.Empty);
            this.currentPath = base.Request.PhysicalApplicationPath + this.skinPath + str;
            this.currentPath = this.currentPath.Replace("/", @"\");
            if (!this.Page.IsPostBack)
            {
                string str2 = "StyleManage.aspx";
                if (!string.IsNullOrEmpty(str))
                {
                    if (str.IndexOf('.') > 0)
                    {
                        str2 = str2 + "?Dir=" + base.Server.UrlEncode(str.Substring(0, str.LastIndexOf('/')));
                    }
                    else
                    {
                        str2 = str2 + "?Dir=" + base.Server.UrlEncode(str);
                    }
                }
                this.ViewState["UrlReferrer"] = str2;
                if (this.m_action == "Modify")
                {
                    this.LblFileName.Text = str;
                    this.TxtFileName.Visible = false;
                    this.ValrTxtFileName.Visible = false;
                    this.ValeTxtFileName.Enabled = false;
                    try
                    {
                        this.EditorContent.Text = FileSystemObject.ReadFile(this.currentPath);
                    }
                    catch (FileNotFoundException)
                    {
                        AdminPage.WriteErrMsg("文件未找到", this.ViewState["UrlReferrer"].ToString());
                    }
                    catch (UnauthorizedAccessException)
                    {
                        AdminPage.WriteErrMsg("<li>读取文件失败！检查您的服务器是否给风格文件夹写入权限。</li>", this.ViewState["UrlReferrer"].ToString());
                    }
                }
                if (this.m_action == "Add")
                {
                    this.LblFileName.Text = str + "/";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitPage();
        }
    }
}


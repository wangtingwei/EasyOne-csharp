namespace EasyOne.WebSite.Admin.Template
{
    using AjaxControlToolkit;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class TemplateUI : AdminPage
    {
        protected string currentPath;
        private string m_action;

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            this.LblLabelList.Text = string.Empty;
            foreach (LabelManageInfo info in LabelManage.GetLabelList(1, 0, this.LabelSearch.Text, string.Empty))
            {
                string text = this.LblLabelList.Text;
                this.LblLabelList.Text = text + "<div onclick=\"cit()\" outype=\"1\" class=\"spanfixdiv\" alt=\"" + info.Intro + "\">" + info.Name + "</div>";
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                try
                {
                    if (this.m_action == "Modify")
                    {
                        FileSystemObject.WriteFile(this.currentPath, this.TxtTemplate.Text);
                        AdminPage.WriteSuccessMsg("<li>保存模板数据成功！</li>", this.ViewState["UrlReferrer"].ToString());
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
                            FileSystemObject.WriteFile(this.currentPath, this.TxtTemplate.Text);
                            AdminPage.WriteSuccessMsg("<li>保存模板数据成功！</li>", this.ViewState["UrlReferrer"].ToString());
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    AdminPage.WriteErrMsg("文件未找到");
                }
                catch (UnauthorizedAccessException)
                {
                    AdminPage.WriteErrMsg("<li>访问文件失败！检查您的服务器是否给模板文件夹写入权限。</li>");
                }
            }
        }

        protected void BuildLabelList(string typename)
        {
            this.LblLabelList.Text = string.Empty;
            foreach (LabelManageInfo info in LabelManage.GetLabelList(typename))
            {
                string text = this.LblLabelList.Text;
                this.LblLabelList.Text = text + "<div onclick=\"cit()\" outype=\"1\" class=\"spanfixdiv\" alt=\"" + info.Intro + "\">" + info.Name + "</div>";
            }
        }

        protected void BuildPagerList(string typename)
        {
            this.LblPageList.Text = string.Empty;
            foreach (PagerManageInfo info in PagerManage.GetPagerList(typename))
            {
                string text = this.LblPageList.Text;
                this.LblPageList.Text = text + "<div onclick=\"cit()\" outype=\"2\" class=\"spanfixdiv\" alt=\"" + info.Intro + "\">" + info.Name + "</div>";
            }
        }

        protected void DropLabelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BuildLabelList(this.DropLabelList.SelectedValue);
        }

        protected void DropPagerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BuildPagerList(this.DropPagerList.SelectedValue);
        }

        protected void InitPage()
        {
            this.m_action = BasePage.RequestString("Action", "Add");
            this.ValeTxtFileName.ValidationExpression = "^[^\\/ :*?\"<>|.]+\\.html$";
            string str = BasePage.RequestString("Dir").Replace("..", string.Empty);
            this.currentPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + str;
            this.currentPath = this.currentPath.Replace("/", @"\");
            if (!this.Page.IsPostBack)
            {
                string str2 = "TemplateManage.aspx";
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
                        this.TxtTemplate.Text = FileSystemObject.ReadFile(this.currentPath);
                    }
                    catch (FileNotFoundException)
                    {
                        AdminPage.WriteErrMsg("文件未找到", this.ViewState["UrlReferrer"].ToString());
                    }
                    catch (UnauthorizedAccessException)
                    {
                        AdminPage.WriteErrMsg("<li>读取文件失败！检查您的服务器是否给模板文件夹写入权限。</li>", this.ViewState["UrlReferrer"].ToString());
                    }
                }
                if (this.m_action == "Add")
                {
                    this.LblFileName.Text = str + "/";
                }
                ListItem item = new ListItem();
                item.Text = "全部分类";
                this.DropLabelList.DataSource = LabelManage.GetLabelTypeList();
                this.DropLabelList.DataTextField = "Name";
                this.DropLabelList.DataValueField = "Name";
                this.DropLabelList.DataBind();
                this.DropLabelList.Items.Insert(0, item);
                this.DropPagerList.DataSource = PagerManage.GetPagerTypeList();
                this.DropPagerList.DataTextField = "Name";
                this.DropPagerList.DataValueField = "Name";
                this.DropPagerList.DataBind();
                this.DropPagerList.Items.Insert(0, item);
                this.BuildLabelList(string.Empty);
                this.BuildPagerList(string.Empty);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitPage();
            this.TxtTemplate.Attributes.Add("onmouseup", "dragend(this)");
            this.TxtTemplate.Attributes.Add("onClick", "savePos(this)");
            this.TxtTemplate.Attributes.Add("onmousemove", "DragPos(this)");
        }
    }
}


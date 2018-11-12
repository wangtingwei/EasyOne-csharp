namespace EasyOne.WebSite.Admin.Template
{
    using AjaxControlToolkit;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Model.Templates;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class IncludeFileUI : AdminPage
    {

        private void BindDataToControls()
        {
            IncludeFileInfo includeFileInfoById = IncludeFile.GetIncludeFileInfoById(BasePage.RequestInt32("id"));
            if (!includeFileInfoById.IsNull)
            {
                this.TxtName.Text = includeFileInfoById.Name;
                this.ViewState["Name"] = includeFileInfoById.Name;
                this.TxtDescription.Text = includeFileInfoById.Description;
                this.TxtFileName.Text = includeFileInfoById.FileName;
                this.ViewState["FileName"] = includeFileInfoById.FileName;
                this.RadlIncludeType.SelectedValue = Enum.GetName(typeof(IncludeType), includeFileInfoById.IncludeType);
                this.RadlAssociateType.SelectedValue = Enum.GetName(typeof(AssociateType), includeFileInfoById.AssociateType);
                this.TxtTemplate.Text = includeFileInfoById.Template;
            }
            else
            {
                AdminPage.WriteErrMsg("您修改的内嵌代码不存在！", "IncludeFileManage.aspx");
            }
        }

        protected void BtnSaveIncludeFile_Click(object sender, EventArgs e)
        {
            IncludeFileInfo includeFileInfo = new IncludeFileInfo();
            includeFileInfo.Id = BasePage.RequestInt32("id");
            includeFileInfo.Name = this.TxtName.Text;
            includeFileInfo.FileName = this.TxtFileName.Text;
            includeFileInfo.Description = this.TxtDescription.Text;
            includeFileInfo.Template = this.TxtTemplate.Text;
            includeFileInfo.IncludeType = (IncludeType) Enum.Parse(typeof(IncludeType), this.RadlIncludeType.SelectedValue);
            includeFileInfo.AssociateType = (AssociateType) Enum.Parse(typeof(AssociateType), this.RadlAssociateType.SelectedValue);
            if (BasePage.RequestStringToLower("action") == "modify")
            {
                if ((includeFileInfo.Name != this.ViewState["Name"].ToString()) && IncludeFile.ExistsName(includeFileInfo.Name))
                {
                    AdminPage.WriteErrMsg("内嵌代码名称已经存在");
                }
                bool flag = string.Compare(includeFileInfo.FileName, this.ViewState["FileName"].ToString(), StringComparison.OrdinalIgnoreCase) != 0;
                if (flag && IncludeFile.ExistsFileName(includeFileInfo.FileName))
                {
                    AdminPage.WriteErrMsg("内嵌代码文件名已经存在");
                }
                if (IncludeFile.Update(includeFileInfo))
                {
                    if (flag)
                    {
                        string includeFilePath = SiteConfig.SiteOption.IncludeFilePath;
                        includeFilePath = "~/" + includeFilePath + "/" + this.ViewState["FileName"].ToString();
                        FileInfo info2 = new FileInfo(HttpContext.Current.Request.MapPath(includeFilePath));
                        if (info2.Exists)
                        {
                            info2.Delete();
                        }
                    }
                    AdminPage.WriteSuccessMsg("修改成功", "IncludeFileManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("修改失败", "IncludeFileManage.aspx");
                }
            }
            else
            {
                if (IncludeFile.ExistsName(includeFileInfo.Name))
                {
                    AdminPage.WriteErrMsg("内嵌代码名称已经存在");
                }
                if (IncludeFile.ExistsFileName(includeFileInfo.FileName))
                {
                    AdminPage.WriteErrMsg("内嵌代码文件名已经存在");
                }
                if (IncludeFile.Add(includeFileInfo))
                {
                    AdminPage.WriteSuccessMsg("添加成功", "IncludeFileManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("添加失败", "IncludeFileManage.aspx");
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

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestStringToLower("action");
            this.TxtTemplate.Attributes.Add("onmouseup", "dragend(this)");
            this.TxtTemplate.Attributes.Add("onClick", "savePos(this)");
            this.TxtTemplate.Attributes.Add("onmousemove", "DragPos(this)");
            this.RegTxtFileName.ValidationExpression = "^[^\\\\\\./:\\*\\?\\\"<>\\|]{1}[^\\\\/:\\*\\.\\?\\\"<>\\|]{0,254}\\.[^\\\\/:\\*\\.\\?\\\"<>\\|]{1,8}$";
            this.RegTxtFileName.ErrorMessage = "文件名格式不正确";
            if (!base.IsPostBack)
            {
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
            if ((str == "modify") && !base.IsPostBack)
            {
                this.BindDataToControls();
            }
        }
    }
}


namespace EasyOne.WebSite.Admin.Template
{
    using AjaxControlToolkit;
    using EasyOne.Controls;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Pager : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }
            if (this.ViewState["action"].ToString() == "add")
            {
                if (PagerManage.Exists(this.TxtLabelName.Text))
                {
                    AdminPage.WriteErrMsg("标签名重复");
                }
            }
            else if ((string.Compare(this.ViewState["oldlabelname"].ToString(), this.TxtLabelName.Text, StringComparison.OrdinalIgnoreCase) > 0) && PagerManage.Exists(this.TxtLabelName.Text))
            {
                AdminPage.WriteErrMsg("您不能修改成已存在的标签名");
            }
            PagerManageInfo ainfo = new PagerManageInfo();
            ainfo.Name = this.TxtLabelName.Text;
            ainfo.Type = this.TxtLabelType.Text;
            ainfo.Template = new StringBuilder(this.TxtLabelTemplate.Text);
            ainfo.Intro = this.TxtLabelIntro.Text;
            bool flag = false;
            string str = this.ViewState["action"].ToString();
            if (str != null)
            {
                if (!(str == "add"))
                {
                    if (str == "modify")
                    {
                        flag = PagerManage.Update(ainfo, this.ViewState["oldlabelname"].ToString());
                        goto Label_0148;
                    }
                }
                else
                {
                    flag = PagerManage.Add(ainfo);
                    goto Label_0148;
                }
            }
            flag = PagerManage.Add(ainfo);
        Label_0148:
            if (flag)
            {
                BasePage.ResponseRedirect("PagerManage.aspx");
            }
        }

        private void FillData()
        {
            this.ViewState["PagerName"] = BasePage.RequestString("Name");
            PagerManageInfo pagerByName = PagerManage.GetPagerByName(this.ViewState["PagerName"].ToString());
            this.TxtLabelName.Text = pagerByName.Name;
            this.ViewState["oldlabelname"] = pagerByName.Name;
            this.TxtLabelType.Text = pagerByName.Type;
            this.TxtLabelTemplate.Text = pagerByName.Template.ToString();
            this.TxtLabelIntro.Text = pagerByName.Intro;
        }

        private void InitDropLabelType()
        {
            this.DropLabelType.DataSource = PagerManage.GetPagerTypeList();
            this.DropLabelType.DataTextField = "Name";
            this.DropLabelType.DataValueField = "Name";
            this.DropLabelType.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部分类";
            item.Value = "全部分类";
            this.DropLabelType.Items.Insert(0, item);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.IsPostBack)
            {
                goto Label_00F1;
            }
            string str2 = BasePage.RequestStringToLower("Action", "add");
            if (str2 != null)
            {
                if (!(str2 == "add"))
                {
                    if (str2 == "modify")
                    {
                        this.ViewState["action"] = "modify";
                        this.LblPTitle.Text = "修改分页标签";
                        this.FillData();
                        goto Label_0095;
                    }
                }
                else
                {
                    this.ViewState["action"] = "add";
                    goto Label_0095;
                }
            }
            this.ViewState["action"] = "Add";
        Label_0095:
            this.InitDropLabelType();
            this.DropLabelType.Attributes.Add("onChange", "addclass('" + this.DropLabelType.ClientID + "','" + this.TxtLabelType.ClientID + "')");
        Label_00F1:
            base.Form.Attributes.Add("onmouseup", "dragclear()");
            base.Form.Attributes.Add("onmousemove", "dragmove()");
            this.TxtLabelTemplate.Attributes.Add("onmouseup", "dragend();");
            this.TxtLabelTemplate.Attributes.Add("onmousemove", "movePoint();;");
        }
    }
}


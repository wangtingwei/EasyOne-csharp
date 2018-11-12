namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.Contents;

    public partial class AddContentToSpecial : AdminPage
    {
        private string action;

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            if (this.action == "special")
            {
                BasePage.ResponseRedirect("SpecialInfosManage.aspx");
            }
            else
            {
                BasePage.ResponseRedirect("ContentManage.aspx");
            }
        }

        protected void EBtnBacthSet_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                bool flag;
                string idsFromListBox = this.GetIdsFromListBox(this.LstSpecial);
                string text = this.TxtGeneralId.Text;
                if (this.action == "special")
                {
                    flag = Special.AddContentToSpecialInfos(idsFromListBox, text);
                }
                else
                {
                    flag = Special.AddContentToSpecialInfoByGeneralId(idsFromListBox, text);
                }
                if (flag)
                {
                    AdminPage.WriteSuccessMsg("添加成功！", "SpecialInfosManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("添加失败！", "ContentManage.aspx");
                }
            }
        }

        private string GetIdsFromListBox(ListBox Lst)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListItem item in Lst.Items)
            {
                if (item.Selected)
                {
                    StringHelper.AppendString(sb, item.Value);
                }
            }
            return sb.ToString();
        }

        private void Initial()
        {
            string str = BasePage.RequestString("Id");
            if (!this.Page.IsPostBack)
            {
                IList<SpecialInfo> specialList = Special.GetSpecialList();
                this.LstSpecial.DataSource = specialList;
                this.LstSpecial.DataTextField = "SpecialName";
                this.LstSpecial.DataValueField = "SpecialId";
                this.LstSpecial.DataBind();
            }
            this.TxtGeneralId.Text = str;
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("function SelectAll(){");
            builder.Append("for(var i=0;i<document.getElementById('");
            builder.Append(this.LstSpecial.ClientID);
            builder.Append("').length;i++){");
            builder.Append("document.getElementById('");
            builder.Append(this.LstSpecial.ClientID);
            builder.Append("').options[i].selected=true;}}");
            builder.Append("function UnSelectAll(){");
            builder.Append("for(var i=0;i<document.getElementById('");
            builder.Append(this.LstSpecial.ClientID);
            builder.Append("').length;i++){");
            builder.Append("document.getElementById('");
            builder.Append(this.LstSpecial.ClientID);
            builder.Append("').options[i].selected=false;}}");
            builder.Append("</script>");
            base.ClientScript.RegisterClientScriptBlock(base.GetType(), "Select", builder.ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RolePermissions.BusinessAccessCheck(OperateCode.SpecialInfoManage);
            }
            catch (CustomException exception)
            {
                AdminPage.WriteErrMsg("<li>您没有此功能权限，错误原因：" + exception.Message + "！</li>");
            }
            this.action = BasePage.RequestStringToLower("Action", "special");
            this.Initial();
        }
    }
}


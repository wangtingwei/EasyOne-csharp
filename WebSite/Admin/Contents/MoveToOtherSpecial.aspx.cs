namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.Contents;

    public partial class MoveToOtherSpecial : AdminPage
    {

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("SpecialInfosManage.aspx");
        }

        protected void EBtnBacthSet_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int specialId = DataConverter.CLng(this.LstTargetSpecial.SelectedValue);
                if (specialId <= 0)
                {
                    AdminPage.WriteErrMsg("请先指定目标专题！", "SpecialInfosManage.aspx");
                }
                if (this.RadSpecialInfoId0.Checked)
                {
                    if (!DataValidator.IsValidId(this.TxtGeneralId.Text))
                    {
                        AdminPage.WriteErrMsg("指定的专题信息ID不正确！", "SpecialInfosManage.aspx");
                    }
                    else
                    {
                        int sourceSpecialId = DataConverter.CLng(this.HdnSpecialId.Value);
                        Special.UpdateSpecialIdByGeneralId(specialId, sourceSpecialId, this.TxtGeneralId.Text);
                    }
                }
                else if (this.RadSpecialInfoId1.Checked)
                {
                    string idsFromListBox = this.GetIdsFromListBox(this.LstSpecial);
                    if (!DataValidator.IsValidId(idsFromListBox))
                    {
                        AdminPage.WriteErrMsg("指定的专题ID不正确！", "SpecialInfosManage.aspx");
                    }
                    else
                    {
                        Special.MoveSpecialInfoBySpecialId(idsFromListBox, specialId);
                    }
                }
                AdminPage.WriteSuccessMsg("批量处理成功！", "SpecialInfosManage.aspx");
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
            this.TxtGeneralId.Text = BasePage.RequestString("Id");
            if (!this.Page.IsPostBack)
            {
                IList<SpecialInfo> specialList = Special.GetSpecialList();
                this.LstSpecial.DataSource = specialList;
                this.LstSpecial.DataTextField = "SpecialName";
                this.LstSpecial.DataValueField = "SpecialId";
                this.LstSpecial.DataBind();
                this.LstTargetSpecial.DataSource = specialList;
                this.LstTargetSpecial.DataTextField = "SpecialName";
                this.LstTargetSpecial.DataValueField = "SpecialId";
                this.LstTargetSpecial.DataBind();
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
                this.HdnSpecialId.Value = BasePage.RequestString("SpecialId");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.SpecialManage);
            this.Initial();
        }
    }
}


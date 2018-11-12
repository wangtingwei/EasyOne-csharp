namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.ModelControls;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class SpecialBatchSet : AdminPage
    {

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("SpecialManage.aspx");
        }

        protected void EBtnBacthSet_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ListItem item in this.LstSpecial.Items)
                {
                    if (item.Selected)
                    {
                        StringHelper.AppendString(sb, item.Value);
                    }
                }
                if (sb.Length <= 0)
                {
                    AdminPage.WriteErrMsg("请先指定要批量设置的专题！", "SpecialBatchSet.aspx");
                }
                if (Special.SpecialBatchSet(this.GetSpecialInfo(), sb.ToString(), this.GetCheckItem()))
                {
                    IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Special);
                    AdminPage.WriteSuccessMsg("专题批量设置成功！", "SpecialManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("专题批量设置失败！", "SpecialBatchSet.aspx");
                }
            }
        }

        private Dictionary<string, bool> GetCheckItem()
        {
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            dictionary.Add("OpenType", this.ChkOpenType.Checked);
            dictionary.Add("IsElite", this.ChkIsElite.Checked);
            dictionary.Add("SpecialTemplatePath", this.ChKSpecialTemplatePath.Checked);
            return dictionary;
        }

        private SpecialInfo GetSpecialInfo()
        {
            SpecialInfo info = new SpecialInfo();
            info.OpenType = DataConverter.CLng(this.RadlOpenType.SelectedValue);
            info.IsElite = DataConverter.CBoolean(this.RadlIsElite.SelectedValue);
            info.SpecialTemplatePath = this.FileCSpecialTemplatePath.Text;
            return info;
        }

        private void Initial()
        {
            if (!this.Page.IsPostBack)
            {
                IList<SpecialInfo> specialList = Special.GetSpecialList();
                this.LstSpecial.DataSource = specialList;
                this.LstSpecial.DataTextField = "SpecialName";
                this.LstSpecial.DataValueField = "SpecialId";
                this.LstSpecial.DataBind();
            }
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.SpecialManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Initial();
        }
    }
}


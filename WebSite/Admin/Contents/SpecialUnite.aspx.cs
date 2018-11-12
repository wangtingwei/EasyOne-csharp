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
    using System.Web.UI.WebControls;
    using EasyOne.Model.Contents;

    public partial class SpecialUnite : AdminPage
    {

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("SpecialManage.aspx");
        }

        protected void EBtnUnite_Click(object sender, EventArgs e)
        {
            int specialId = DataConverter.CLng(this.DropFromSpecial.SelectedValue);
            int targetSpecialId = DataConverter.CLng(this.DropToSpecial.SelectedValue);
            switch (Special.UniteSpecial(specialId, targetSpecialId))
            {
                case 0:
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("<li>专题合并成功！</li>", "SpecialManage.aspx");
                    return;

                case 1:
                    AdminPage.WriteErrMsg("<li>未指定要合并的专题！</li>", "SpecialManage.aspx");
                    return;

                case 2:
                    AdminPage.WriteErrMsg("<li>未指定目标专题！</li>", "SpecialManage.aspx");
                    return;

                case 3:
                    AdminPage.WriteErrMsg("<li>要合并的专题与目标专题相同！</li>", "SpecialManage.aspx");
                    return;

                case 4:
                    AdminPage.WriteErrMsg("<li>更新专题信息失败！</li>", "SpecialManage.aspx");
                    return;

                case 5:
                    AdminPage.WriteErrMsg("<li>删除被合并的专题失败！</li>", "SpecialManage.aspx");
                    return;
            }
        }

        private void Initial()
        {
            if (!this.Page.IsPostBack)
            {
                IList<SpecialInfo> specialList = Special.GetSpecialList();
                this.DropFromSpecial.DataSource = specialList;
                this.DropFromSpecial.DataTextField = "SpecialName";
                this.DropFromSpecial.DataValueField = "SpecialId";
                this.DropFromSpecial.DataBind();
                this.DropToSpecial.DataSource = specialList;
                this.DropToSpecial.DataTextField = "SpecialName";
                this.DropToSpecial.DataValueField = "SpecialId";
                this.DropToSpecial.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.SpecialManage);
            this.Initial();
        }
    }
}


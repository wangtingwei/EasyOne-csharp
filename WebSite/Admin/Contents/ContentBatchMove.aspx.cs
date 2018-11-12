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

    public partial class ContentBatchMove : AdminPage
    {

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentManage.aspx");
        }

        protected void EBtnBacthMove_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int nodeId = DataConverter.CLng(this.DropNode.SelectedValue);
                string text = this.TxtGeneralId.Text;
                if (!DataValidator.IsValidId(text))
                {
                    AdminPage.WriteSuccessMsg("指定的内容ID不正确，请重新指定！", "ContentManage.aspx");
                }
                if (ContentManage.BatchMove(text, nodeId))
                {
                    AdminPage.WriteSuccessMsg("批量移动成功！", "ContentManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("批量移动失败！", "ContentManage.aspx");
                }
            }
        }

        private void Initial()
        {
            string str = BasePage.RequestString("Id");
            if (!this.Page.IsPostBack)
            {
                IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
                this.DropNode.DataSource = nodeNameForContainerItems;
                this.DropNode.DataBind();
                this.TxtGeneralId.Text = str;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.ContentManage);
            this.Initial();
        }
    }
}


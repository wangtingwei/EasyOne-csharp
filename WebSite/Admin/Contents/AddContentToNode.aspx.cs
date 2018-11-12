namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class AddContentToNode : AdminPage
    {

        private static void AddVirtualContent(int nodeId, int generalId)
        {
            if (!ContentManage.Exists(generalId, nodeId))
            {
                foreach (CommonModelInfo info in ContentManage.GetInfoList(generalId))
                {
                    if (ContentManage.Exists(info.GeneralId, nodeId))
                    {
                        return;
                    }
                }
                ContentManage.AddVirtualContent(generalId, nodeId);
            }
        }

        private static void AddVirtualContent(string generalIds, int nodeId)
        {
            if (generalIds.Contains(","))
            {
                foreach (string str in generalIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    AddVirtualContent(nodeId, DataConverter.CLng(str));
                }
            }
            else
            {
                AddVirtualContent(nodeId, DataConverter.CLng(generalIds));
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentManage.aspx");
        }

        protected void EBtnBacthSet_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string idsFromListBox = this.GetIdsFromListBox(this.LstNode);
                string text = this.TxtGeneralId.Text;
                if (!DataValidator.IsValidId(text))
                {
                    AdminPage.WriteErrMsg("<li>指定的内容ID不正确，请重新指定！</li>", "ContentManage.aspx");
                }
                if (string.IsNullOrEmpty(idsFromListBox))
                {
                    AdminPage.WriteErrMsg("<li>没有选择栏目！</li>");
                }
                if (idsFromListBox.Contains(","))
                {
                    foreach (string str3 in idsFromListBox.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        AddVirtualContent(text, DataConverter.CLng(str3));
                    }
                }
                else
                {
                    AddVirtualContent(text, DataConverter.CLng(idsFromListBox));
                }
                AdminPage.WriteSuccessMsg("<li>添加到其他节点成功！</li>", "ContentManage.aspx");
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
                IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
                this.LstNode.DataSource = nodeNameForContainerItems;
                this.LstNode.DataTextField = "NodeName";
                this.LstNode.DataValueField = "NodeId";
                this.LstNode.DataBind();
            }
            this.TxtGeneralId.Text = str;
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("function SelectAll(){");
            builder.Append("for(var i=0;i<document.getElementById('");
            builder.Append(this.LstNode.ClientID);
            builder.Append("').length;i++){");
            builder.Append("document.getElementById('");
            builder.Append(this.LstNode.ClientID);
            builder.Append("').options[i].selected=true;}}");
            builder.Append("function UnSelectAll(){");
            builder.Append("for(var i=0;i<document.getElementById('");
            builder.Append(this.LstNode.ClientID);
            builder.Append("').length;i++){");
            builder.Append("document.getElementById('");
            builder.Append(this.LstNode.ClientID);
            builder.Append("').options[i].selected=false;}}");
            builder.Append("</script>");
            base.ClientScript.RegisterClientScriptBlock(base.GetType(), "Select", builder.ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RolePermissions.BusinessAccessCheck(OperateCode.CategoryInfoManage);
            }
            catch (CustomException exception)
            {
                AdminPage.WriteErrMsg("<li>您没有此功能权限，错误原因：" + exception.Message + "！</li>");
            }
            this.Initial();
        }
    }
}


namespace EasyOne.WebSite.Common
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Controls.Editor;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Model.Crm;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class GuestWrite : DynamicPage
    {
        private int m_ModelId;
        private int m_NodeId;

        protected void EBtnSubmit_Cancel(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(SiteConfig.SiteInfo.VirtualPath + "Category_" + this.m_NodeId.ToString() + "/index.aspx");
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int nodeId = DataConverter.CLng(this.DropCategoryId.SelectedValue);
            if (nodeId == 0)
            {
                nodeId = this.m_NodeId;
            }
            if (nodeId == 0)
            {
                DynamicPage.WriteErrMsg("<li>节点ID不存在！</li>");
            }
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId);
            if (cacheNodeById.IsNull)
            {
                DynamicPage.WriteErrMsg("<li>栏目节点不存在！</li>");
            }
            DataTable contentData = this.GetContentData(nodeId);
            string successMessage = "您的留言已经提交请等待管理员的审核！";
            if (cacheNodeById.WorkFlowId == -1)
            {
                contentData.Select("FieldName = 'status'")[0]["FieldValue"] = "99";
                successMessage = "您的留言已经发送成功！";
            }
            if (ContentManage.Add(this.m_ModelId, contentData))
            {
                DynamicPage.WriteSuccessMsg(successMessage, SiteConfig.SiteInfo.VirtualPath + "Category_" + this.m_NodeId.ToString() + "/index.aspx");
            }
            else
            {
                DynamicPage.WriteErrMsg("发表留言失败！");
            }
        }

        private DataTable GetContentData(int nodeId)
        {
            string text;
            string str3;
            string str4;
            string str5;
            string str6;
            string userName = "";
            if ((HttpContext.Current != null) && !string.IsNullOrEmpty(PEContext.Current.User.UserName))
            {
                userName = PEContext.Current.User.UserName;
            }
            if (PEContext.Current.User.Identity.IsAuthenticated)
            {
                if (Users.GetUsersByUserName(PEContext.Current.User.UserName).IsNull)
                {
                    DynamicPage.WriteErrMsg("登录用户不存在！");
                }
                text = this.LblGuestName.Text;
                str3 = this.LblGuestEmail.Text;
                str4 = this.LblGuestOicq.Text;
                str5 = this.LblGuestMsn.Text;
                str6 = this.LblGuestHomepage.Text;
            }
            else
            {
                text = this.TxtGuestName.Text;
                str3 = this.TxtGuestEmail.Text;
                str4 = this.TxtGuestOicq.Text;
                str5 = this.TxtGuestMsn.Text;
                str6 = this.TxtGuestHomepage.Text;
            }
            DataTable table = new DataTable();
            table.Columns.Add("FieldName");
            table.Columns.Add("FieldValue");
            table.Columns.Add("FieldType");
            table.Columns.Add("FieldLevel");
            DataRow row = table.NewRow();
            row["FieldName"] = "nodeid";
            row["FieldValue"] = nodeId.ToString();
            row["FieldType"] = FieldType.NodeType;
            row["FieldLevel"] = 0;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "Title";
            row["FieldValue"] = this.TxtSubject.Text;
            row["FieldType"] = FieldType.TextType;
            row["FieldLevel"] = 0;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "Inputer";
            row["FieldValue"] = userName;
            row["FieldType"] = FieldType.TextType;
            row["FieldLevel"] = 0;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "status";
            row["FieldValue"] = "0";
            row["FieldType"] = FieldType.NumberType;
            row["FieldLevel"] = 0;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "EliteLevel";
            row["FieldValue"] = "0";
            row["FieldType"] = FieldType.NumberType;
            row["FieldLevel"] = 0;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "Priority";
            row["FieldValue"] = "0";
            row["FieldType"] = FieldType.NumberType;
            row["FieldLevel"] = 0;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "CreateTime";
            row["FieldValue"] = DateTime.Now;
            row["FieldType"] = FieldType.DateTimeType;
            row["FieldLevel"] = 0;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "UpdateTime";
            row["FieldValue"] = DateTime.Now;
            row["FieldType"] = FieldType.DateTimeType;
            row["FieldLevel"] = 0;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "GuestName";
            row["FieldValue"] = text;
            row["FieldType"] = FieldType.TextType;
            row["FieldLevel"] = 1;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "GuestImages";
            row["FieldValue"] = this.DropGuestImages.SelectedValue;
            row["FieldType"] = FieldType.TextType;
            row["FieldLevel"] = 1;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "GuestEmail";
            row["FieldValue"] = str3;
            row["FieldType"] = FieldType.TextType;
            row["FieldLevel"] = 1;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "GuestOicq";
            row["FieldValue"] = str4;
            row["FieldType"] = FieldType.TextType;
            row["FieldLevel"] = 1;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "GuestMsn";
            row["FieldValue"] = str5;
            row["FieldType"] = FieldType.TextType;
            row["FieldLevel"] = 1;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "GuestHomepage";
            row["FieldValue"] = str6;
            row["FieldType"] = FieldType.TextType;
            row["FieldLevel"] = 1;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "GuestFace";
            row["FieldValue"] = this.RadlGuestFace.Text;
            row["FieldType"] = FieldType.TextType;
            row["FieldLevel"] = 1;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "GuestContent";
            row["FieldValue"] = StringHelper.RemoveXss(this.EditorGuestContent.Value);
            row["FieldType"] = FieldType.MultipleHtmlTextType;
            row["FieldLevel"] = 1;
            table.Rows.Add(row);
            row = table.NewRow();
            row["FieldName"] = "GuestIsPrivate";
            row["FieldValue"] = DataConverter.CBoolean(this.RadlGuestIsPrivate.SelectedValue);
            row["FieldType"] = FieldType.BoolType;
            row["FieldLevel"] = 1;
            table.Rows.Add(row);
            return table;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_NodeId = BasePage.RequestInt32("id");
            this.m_ModelId = BasePage.RequestInt32("modelId");
            this.DropGuestImages.Attributes.Add("onchange", "document.getElementById('showphoto').src = '../Images/Comment/' + this.value + '.gif';");
            if (this.m_ModelId == 0)
            {
                DynamicPage.WriteErrMsg("没有模型ID！");
            }
            if (PEContext.Current.User.Identity.IsAuthenticated)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                if (usersByUserName.IsNull)
                {
                    DynamicPage.WriteErrMsg("登录用户不存在！");
                }
                this.TxtGuestName.Visible = false;
                this.ValrGuestName.Visible = false;
                this.TxtGuestEmail.Visible = false;
                this.ValrEmail.Visible = false;
                this.Vmail.Visible = false;
                this.TxtGuestOicq.Visible = false;
                this.TxtGuestMsn.Visible = false;
                this.TxtGuestHomepage.Visible = false;
                this.LblGuestName.Visible = true;
                this.LblGuestEmail.Visible = true;
                this.LblGuestOicq.Visible = true;
                this.LblGuestMsn.Visible = true;
                this.LblGuestHomepage.Visible = true;
                this.LblGuestName.Text = usersByUserName.UserName;
                this.LblGuestEmail.Text = usersByUserName.Email;
                ContacterInfo contacterByUserName = new ContacterInfo();
                contacterByUserName = Contacter.GetContacterByUserName(usersByUserName.UserName);
                if (contacterByUserName != null)
                {
                    this.LblGuestOicq.Text = contacterByUserName.QQ;
                    this.LblGuestMsn.Text = contacterByUserName.Msn;
                    this.LblGuestHomepage.Text = contacterByUserName.Homepage;
                }
            }
            if (!this.Page.IsPostBack)
            {
                IList<NodeInfo> nodesListByParentId = EasyOne.Contents.Nodes.GetNodesListByParentId(this.m_NodeId);
                this.DropCategoryId.DataSource = nodesListByParentId;
                this.DropCategoryId.DataTextField = "NodeName";
                this.DropCategoryId.DataValueField = "NodeID";
                this.DropCategoryId.DataBind();
            }
        }
    }
}


namespace EasyOne.WebSite.Controls
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class ShowMessageList : BaseUserControl
    {

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = this.GdvMessageList.SelectList;
            if (selectList.Length == 0)
            {
                BaseUserControl.WriteErrMsg("<li>对不起，您还没选择要删除的短消息！</li>");
            }
            else if (Message.Clear(this.ManageType, PEContext.Current.User.UserName, selectList.ToString()))
            {
                this.GdvMessageList.DataBind();
            }
            else
            {
                BaseUserControl.WriteErrMsg("<li>删除失败！！</li>");
            }
        }

        protected void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (Message.Clear(this.ManageType, PEContext.Current.User.UserName))
            {
                if (this.ManageType == MessageManageType.Recycle)
                {
                    BaseUserControl.WriteSuccessMsg("删除短信息成功。", "MessageManager.aspx?ManageType=" + this.ManageType.ToString("D"));
                }
                else
                {
                    BaseUserControl.WriteSuccessMsg("删除短消息成功。删除的消息将转移到您的废件箱中。", "MessageManager.aspx?ManageType=" + this.ManageType.ToString("D"));
                }
            }
            else
            {
                BaseUserControl.WriteErrMsg("删除失败！");
            }
        }

        protected void GdvMessageList_DataBound(object sender, EventArgs e)
        {
            this.BtnDelete.Enabled = this.BtnDeleteAll.Enabled = this.GdvMessageList.Rows.Count > 0;
        }

        protected void GdvMessageList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GdvMessageList.PageIndex = e.NewPageIndex;
            this.GdvMessageList.DataBind();
        }

        protected void GdvMessageList_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                if (Message.Clear(this.ManageType, PEContext.Current.User.UserName, e.CommandArgument.ToString()))
                {
                    this.GdvMessageList.DataBind();
                }
                else
                {
                    BaseUserControl.WriteErrMsg("<li>删除失败！！</li>");
                }
            }
        }

        protected void GdvMessageList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MessageInfo dataItem = e.Row.DataItem as MessageInfo;
                HyperLink link = e.Row.FindControl("HlnkMessage") as HyperLink;
                if (link != null)
                {
                    if (this.ManageType == MessageManageType.Outbox)
                    {
                        link.NavigateUrl = "~/User/Message/Message.aspx?Action=Edit&MessageID=" + dataItem.MessageId.ToString();
                    }
                    else
                    {
                        link.NavigateUrl = "~/User/Message/MessageRead.aspx?ManageType=" + this.ManageType.ToString("D") + "&MessageID=" + dataItem.MessageId.ToString();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.OdsMessageList.SelectParameters.Add(new Parameter("userName", TypeCode.String, PEContext.Current.User.UserName));
                this.OdsMessageList.SelectParameters.Add(new ControlParameter("manageType", this.HdnManageType.ID));
                switch (this.ManageType)
                {
                    case MessageManageType.Inbox:
                        this.BtnDeleteAll.Text = "清空收件箱";
                        this.GdvMessageList.Columns[1].Visible = false;
                        return;

                    case MessageManageType.Outbox:
                        this.BtnDeleteAll.Text = "清空草稿箱";
                        this.GdvMessageList.Columns[2].Visible = false;
                        return;

                    case MessageManageType.IsSend:
                        this.BtnDeleteAll.Text = "清空发件箱";
                        this.GdvMessageList.Columns[2].Visible = false;
                        return;

                    case MessageManageType.Recycle:
                        this.BtnDeleteAll.Text = "清空废件箱";
                        this.GdvMessageList.Columns[1].Visible = false;
                        return;

                    default:
                        return;
                }
            }
        }

        public MessageManageType ManageType
        {
            get
            {
                return (MessageManageType) DataConverter.CLng(this.HdnManageType.Value);
            }
            set
            {
                this.HdnManageType.Value = value.ToString("D");
            }
        }

        public MessageSearchField SearchField
        {
            get
            {
                return (MessageSearchField) DataConverter.CLng(this.HdnSearchField.Value);
            }
            set
            {
                this.HdnSearchField.Value = value.ToString("D");
            }
        }

        public string SearchKeyword
        {
            get
            {
                return this.HdnKeyword.Value;
            }
            set
            {
                this.HdnKeyword.Value = value;
            }
        }
    }
}


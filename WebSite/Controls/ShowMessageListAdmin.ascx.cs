namespace EasyOne.WebSite.Controls
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class ShowMessageListAdmin : BaseUserControl
    {

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = this.GdvMessageList.SelectList;
            if (selectList.Length == 0)
            {
                BaseUserControl.WriteErrMsg("<li>对不起，您还没选择要删除的短消息！</li>");
            }
            else if (Message.Delete(MessageDelType.Id, selectList.ToString()))
            {
                this.GdvMessageList.DataBind();
            }
            else
            {
                BaseUserControl.WriteErrMsg("<li>删除失败！！</li>");
            }
        }

        protected void GdvMessageList_DataBound(object sender, EventArgs e)
        {
            this.BtnDelete.Enabled = this.GdvMessageList.Rows.Count > 0;
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
                if (Message.Delete(MessageDelType.Id, e.CommandArgument.ToString()))
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
                    if (dataItem.IsSend == 0)
                    {
                        link.NavigateUrl = "~/Admin/Accessories/MessageSend.aspx?Action=Edit&MessageID=" + dataItem.MessageId.ToString();
                    }
                    else
                    {
                        link.NavigateUrl = "~/Admin/Accessories/MessageRead.aspx?MessageID=" + dataItem.MessageId.ToString();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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


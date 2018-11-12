namespace EasyOne.WebSite.User
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class MessageManager : DynamicPage
    {

        private static string GetNodeContent(MessageManageType manageType)
        {
            switch (manageType)
            {
                case MessageManageType.Inbox:
                    return "收件箱";

                case MessageManageType.Outbox:
                    return "草稿箱";

                case MessageManageType.IsSend:
                    return "已发送";

                case MessageManageType.Recycle:
                    return "废件箱";
            }
            return "收件箱";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MessageManageType manageType = (MessageManageType) BasePage.RequestInt32("ManageType");
            string text = this.TxtKeyword.Text;
            MessageSearchField searchField = (MessageSearchField) DataConverter.CLng(this.DropSearch.SelectedValue);
            if (string.IsNullOrEmpty(PEContext.Current.User.UserName))
            {
                this.BtnSubmit.Enabled = false;
            }
            if (!string.IsNullOrEmpty(text) && (searchField != MessageSearchField.All))
            {
                this.PageDataBind(text, searchField, (MessageManageType) DataConverter.CLng(this.DropManageType.SelectedValue));
            }
            else
            {
                this.ShowUserMessageList.ManageType = manageType;
                this.Page.Title = GetNodeContent(manageType);
                this.YourPosition.AdditionalNode = GetNodeContent(manageType) + " >> 所有短消息";
            }
        }

        private void PageDataBind(string keyword, MessageSearchField searchField, MessageManageType manageType)
        {
            string str;
            this.Page.Title = GetNodeContent(manageType);
            this.ShowUserMessageList.ManageType = manageType;
            this.ShowUserMessageList.SearchField = searchField;
            this.ShowUserMessageList.SearchKeyword = keyword;
            switch (searchField)
            {
                case MessageSearchField.Title:
                    str = "主题中含有 <span style='Color:#F00'> " + keyword + " </span> 的短消息";
                    break;

                case MessageSearchField.Content:
                    str = "内容中含有 <span style='Color:#F00'> " + keyword + " </span> 的短消息";
                    break;

                case MessageSearchField.Incept:
                    str = "收件人为 <span style='Color:#F00'> " + keyword + " </span> 的短消息";
                    break;

                case MessageSearchField.Sender:
                    str = "发件人为 <span style='Color:#F00'> " + keyword + " </span> 的短消息";
                    break;

                default:
                    str = "所有短消息";
                    break;
            }
            this.YourPosition.AdditionalNode = GetNodeContent(manageType) + " >> " + str;
        }
    }
}


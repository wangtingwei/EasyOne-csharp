namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.WebControls;

    public partial class MessageManage : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            this.PageDataBind(this.TxtKeyword.Text, (MessageSearchField) DataConverter.CLng(this.DropSearch.SelectedValue));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (string.Compare(BasePage.RequestString("action"), "me", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.ShowAdminMessageList.SearchField = MessageSearchField.OnePeople;
                    this.ShowAdminMessageList.SearchKeyword = PEContext.Current.Admin.AdminName;
                    this.SmpNavigator.AdditionalNode = "我的短消息";
                }
                else
                {
                    this.ShowAdminMessageList.SearchField = MessageSearchField.All;
                    this.ShowAdminMessageList.SearchKeyword = string.Empty;
                    this.SmpNavigator.AdditionalNode = "所有短消息";
                }
            }
        }

        private void PageDataBind(string keyword, MessageSearchField searchField)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                this.ShowAdminMessageList.SearchField = searchField;
                this.ShowAdminMessageList.SearchKeyword = keyword;
                switch (searchField)
                {
                    case MessageSearchField.Title:
                        this.SmpNavigator.AdditionalNode = "主题中含有 <span style='Color:#F00'> " + keyword + " </span> 的短消息";
                        return;

                    case MessageSearchField.Content:
                        this.SmpNavigator.AdditionalNode = "内容中含有 <span style='Color:#F00'> " + keyword + " </span> 的短消息";
                        return;

                    case MessageSearchField.Incept:
                        this.SmpNavigator.AdditionalNode = "收件人为 <span style='Color:#F00'> " + keyword + " </span> 的短消息";
                        return;

                    case MessageSearchField.Sender:
                        this.SmpNavigator.AdditionalNode = "发件人为 <span style='Color:#F00'> " + keyword + " </span> 的短消息";
                        return;

                    case MessageSearchField.OnePeople:
                        this.SmpNavigator.AdditionalNode = "<span style='Color:#F00'> " + keyword + " </span> 的短消息";
                        return;
                }
                this.SmpNavigator.AdditionalNode = "所有短消息";
            }
        }
    }
}


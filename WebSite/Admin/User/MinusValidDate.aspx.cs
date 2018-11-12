namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class MinusValidDate : AdminPage
    {
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int num3;
            IEncourageStrategy<int> strategy;
            string toUser = this.HdnUsersId.Value;
            int num = DataConverter.CLng(this.TxtValidNum.Text.Trim());
            int num2 = DataConverter.CLng(this.DropValidUnit.SelectedValue);
            string reason = this.TxtReason.Text.Trim();
            string text = this.TxtMemo.Text;
            if (this.RadValidType.Checked && ((num <= 0) || (num > 0x270f)))
            {
                AdminPage.WriteErrMsg("<li>指定期限必须为数字并且必须大于0小于9999！</li>");
            }
            if (this.RadValidType.Checked)
            {
                switch (num2)
                {
                    case 1:
                        num3 = num;
                        goto Label_00BF;

                    case 2:
                        num3 = num * 30;
                        goto Label_00BF;

                    case 3:
                        num3 = num * 0x16d;
                        goto Label_00BF;
                }
                num3 = num;
            }
            else
            {
                num3 = 0x270f;
            }
        Label_00BF:
            strategy = new UserDate();
            if (strategy.IncreaseForUsers(toUser, -num3, reason, true, text))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<li>用户扣除有效期成功！</li>");
                if (this.ChkIsSendMessage.Checked)
                {
                    UserInfo userById = Users.GetUserById(DataConverter.CLng(toUser));
                    string payoutPeriodMessage = SiteConfig.SmsConfig.PayoutPeriodMessage;
                    if (this.RadValidType2.Checked)
                    {
                        payoutPeriodMessage = payoutPeriodMessage.Replace("{$Valid}", "归零");
                    }
                    else
                    {
                        payoutPeriodMessage = payoutPeriodMessage.Replace("{$Valid}", num3.ToString());
                    }
                    payoutPeriodMessage = payoutPeriodMessage.Replace("{$Reason}", reason);
                    builder.Append(Users.SendMessageToUser(userById, payoutPeriodMessage));
                }
                AdminPage.WriteSuccessMsg(builder.ToString(), "UserShow.aspx?UserID=" + toUser);
            }
            else
            {
                AdminPage.WriteErrMsg("<li>用户扣除有效期失败！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.HdnUsersId.Value = BasePage.RequestInt32("UserID").ToString();
            }
        }
    }
}


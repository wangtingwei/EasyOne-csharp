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

    public partial class AddValidDate : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int num3;
            IEncourageStrategy<int> strategy;
            UserInfo userInfo = this.showUserInfo.UserInfo;
            if (!this.Page.IsValid)
            {
                return;
            }
            if (userInfo == null)
            {
                AdminPage.WriteErrMsg("<li>找不到指定的会员！</li>");
                return;
            }
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
                        goto Label_00DE;

                    case 2:
                        num3 = num * 30;
                        goto Label_00DE;

                    case 3:
                        num3 = num * 0x16d;
                        goto Label_00DE;
                }
                num3 = num;
            }
            else
            {
                num3 = 0x270f;
            }
        Label_00DE:
            strategy = new UserDate();
            if (strategy.IncreaseForUsers(userInfo.UserId.ToString(), num3, reason, true, text))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<li>用户添加有效期成功！</li>");
                if (this.ChkIsSendMessage.Checked)
                {
                    string encouragePeriodMessage = SiteConfig.SmsConfig.EncouragePeriodMessage;
                    if (this.RadValidType2.Checked)
                    {
                        encouragePeriodMessage = encouragePeriodMessage.Replace("{$Valid}", "无限期");
                    }
                    else
                    {
                        encouragePeriodMessage = encouragePeriodMessage.Replace("{$Valid}", num3.ToString());
                    }
                    encouragePeriodMessage = encouragePeriodMessage.Replace("{$Reason}", reason);
                    builder.Append(Users.SendMessageToUser(userInfo, encouragePeriodMessage));
                }
                AdminPage.WriteSuccessMsg(builder.ToString(), "UserShow.aspx?UserID=" + userInfo.UserId);
            }
            else
            {
                AdminPage.WriteErrMsg("<li>用户添加有效期失败！</li>", "UserShow.aspx?UserID=" + userInfo.UserId);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


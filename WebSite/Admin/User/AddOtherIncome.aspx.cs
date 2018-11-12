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

    public partial class AddOtherIncome : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                decimal howMany = DataConverter.CDecimal(this.TxtMoney.Text);
                UserInfo userInfo = this.ShowUserInfo.UserInfo;
                string reason = this.TxtRemark.Text.Trim();
                string text = this.TxtMemo.Text;
                if (userInfo == null)
                {
                    AdminPage.WriteErrMsg("<li>找不到指定的会员！</li>");
                }
                else
                {
                    IEncourageStrategy<decimal> strategy = new UserMoney();
                    if (strategy.IncreaseForUsers(userInfo.UserId.ToString(), howMany, reason, true, text))
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append("<li>给会员添加收入成功！</li>");
                        if (this.ChkIsSendMessage.Checked)
                        {
                            string sendContent = SiteConfig.SmsConfig.IncomeLogMessage.Replace("{$Money}", Math.Abs(howMany).ToString()).Replace("{$Reason}", reason);
                            builder.Append(Users.SendMessageToUser(userInfo, sendContent));
                        }
                        AdminPage.WriteSuccessMsg(builder.ToString(), "UserShow.aspx?UserID=" + userInfo.UserId);
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>给会员添加收入失败！</li>", "UserShow.aspx?UserID=" + userInfo.UserId);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


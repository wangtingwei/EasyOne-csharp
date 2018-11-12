namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class AddPoint : AdminPage
    {
        protected string m_PointName = SiteConfig.UserConfig.PointName;

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = this.showUserInfo.UserInfo;
            string reason = this.TxtReason.Text.Trim();
            string text = this.TxtMemo.Text;
            if (this.Page.IsValid)
            {
                int howMany = DataConverter.CLng(this.TxtPoint.Text);
                if (userInfo == null)
                {
                    AdminPage.WriteErrMsg("<li>找不到指定的会员！</li>");
                }
                else
                {
                    IEncourageStrategy<int> strategy = new UserPoint();
                    if (strategy.IncreaseForUsers(userInfo.UserId.ToString(), howMany, reason, true, text))
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append("<li>会员奖励" + this.m_PointName + "成功！</li>");
                        if (this.ChkIsSendMessage.Checked)
                        {
                            string sendContent = SiteConfig.SmsConfig.EncouragePointMessage.Replace("{$Point}", howMany.ToString()).Replace("{$Reason}", this.TxtReason.Text);
                            builder.Append(Users.SendMessageToUser(userInfo, sendContent));
                        }
                        AdminPage.WriteSuccessMsg(builder.ToString(), "UserShow.aspx?UserID=" + userInfo.UserId);
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>会员奖励" + this.m_PointName + "失败！</li>");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LblTitle.Text = "奖励" + this.m_PointName;
            this.ValrPoint.ErrorMessage = "奖励" + this.m_PointName + "不能为空";
        }
    }
}


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

    public partial class MinusPoint : AdminPage
    {
        protected string m_PointName = SiteConfig.UserConfig.PointName;

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string toUser = this.HdnUsersId.Value;
            int num = DataConverter.CLng(this.TxtPoint.Text);
            if ((num <= 0) || (num > 0x5f5e100))
            {
                AdminPage.WriteErrMsg("<li>请输入" + this.m_PointName + "，" + this.m_PointName + "不能为负数，数字不能大过1亿！</li>");
            }
            IEncourageStrategy<int> strategy = new UserPoint();
            if (strategy.IncreaseForUsers(toUser, -num, this.TxtReason.Text.Trim(), true, this.TxtMemo.Text.Trim()))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<li>会员扣除" + this.m_PointName + "成功！</li>");
                if (this.ChkIsSendMessage.Checked)
                {
                    UserInfo userById = Users.GetUserById(DataConverter.CLng(toUser));
                    string sendContent = SiteConfig.SmsConfig.PayoutPointMessage.Replace("{$Point}", num.ToString()).Replace("{$Reason}", this.TxtReason.Text);
                    builder.Append(Users.SendMessageToUser(userById, sendContent));
                }
                AdminPage.WriteSuccessMsg(builder.ToString(), "UserShow.aspx?UserID=" + toUser);
            }
            else
            {
                AdminPage.WriteErrMsg("<li>会员扣除" + this.m_PointName + "失败！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LblTitle.Text = "扣除" + this.m_PointName;
            this.ValrPoint.ErrorMessage = "扣除" + this.m_PointName + "数不能为空";
            this.ValrReason.ErrorMessage = "扣除" + this.m_PointName + "原因不能为空";
            if (!base.IsPostBack)
            {
                this.HdnUsersId.Value = BasePage.RequestInt32("UserID").ToString();
            }
        }
    }
}


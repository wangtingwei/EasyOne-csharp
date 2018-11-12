namespace EasyOne.WebSite.User.Info
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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class DonatePoint : DynamicPage
    {

        protected ShowUserInfo showUserInfo;
 

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int howMany = DataConverter.CLng(this.TxtPoint.Text);
                if (PEContext.Current.User.UserName == this.TxtDonateUserName.Text)
                {
                    DynamicPage.WriteErrMsg("不能自己赠送绐自己！");
                }
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                UserInfo info2 = Users.GetUsersByUserName(this.TxtDonateUserName.Text);
                if (usersByUserName.IsNull)
                {
                    DynamicPage.WriteErrMsg("非法用户登录！");
                }
                if (info2.IsNull)
                {
                    DynamicPage.WriteErrMsg("赠送的用户不存在！");
                }
                if (usersByUserName.UserPoint <= howMany)
                {
                    DynamicPage.WriteErrMsg("剩余的" + SiteConfig.UserConfig.PointUnit + "数不够此次赠送！");
                }
                UserPoint point = new UserPoint();
                if (point.IncreaseForUsers(usersByUserName.UserId.ToString(), -howMany, string.Concat(new object[] { "赠送给：", this.TxtDonateUserName.Text, "用户", howMany, SiteConfig.UserConfig.PointName, "成功！" }), true, ""))
                {
                    if (point.IncreaseForUsers(info2.UserId.ToString(), howMany, "获得用户：" + usersByUserName.UserName + "赠送", true, ""))
                    {
                        DynamicPage.WriteSuccessMsg("赠送给" + info2.UserName + "用户 " + howMany.ToString() + SiteConfig.UserConfig.PointName + "成功！", "DonatePoint.aspx");
                    }
                    else
                    {
                        DynamicPage.WriteErrMsg("赠送失败");
                    }
                }
                else
                {
                    DynamicPage.WriteErrMsg("赠送失败");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                if (!usersByUserName.UserPurview.EnableGivePointToOthers)
                {
                    DynamicPage.WriteErrMsg("对不起，您没有自助赠送点券权限！");
                }
                this.showUserInfo.UserInfo = usersByUserName;
            }
        }
    }
}


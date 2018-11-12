namespace EasyOne.WebSite.User.Info
{
    using AjaxControlToolkit;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class PayPassword : DynamicPage
    {

        protected string payPassword;


        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.TxtPasswords.Text != this.TxtConfirmPassword.Text)
            {
                DynamicPage.WriteErrMsg("<li>确认密码与新密码不一致！</li>");
            }
            UserInfo userInfo = PEContext.Current.User.UserInfo;
            if ((SiteConfig.ShopConfig.IsPayPassword && !string.IsNullOrEmpty(this.payPassword)) && (StringHelper.MD5(this.TxtOldPassword.Text.Trim()) != this.payPassword))
            {
                DynamicPage.WriteErrMsg("<li>输入的旧支付密码不正确！</li>");
            }
            userInfo.PayPassword = StringHelper.MD5(this.TxtPasswords.Text);
            Users.Update(userInfo);
            DynamicPage.WriteSuccessMsg("<li>支付密码修改成功！</li>", "../Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!SiteConfig.ShopConfig.IsPayPassword)
                {
                    this.LblMessage.Text = "<font color=\"red\">系统已关闭预付款支付密码功能</font>";
                    this.TblPayPassword.Visible = false;
                }
                else
                {
                    this.LblUserName.Text = PEContext.Current.User.UserName;
                    UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                    this.payPassword = usersByUserName.PayPassword;
                    if (string.IsNullOrEmpty(this.payPassword))
                    {
                        this.trOldPassword.Visible = false;
                    }
                }
            }
        }
    }
}


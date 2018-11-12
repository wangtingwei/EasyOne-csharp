namespace EasyOne.WebSite.User
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class RegisterCheck : DynamicPage
    {

        protected void BtnRegCheck_Click(object sender, EventArgs e)
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(this.TxtUserName.Text);
            if (!usersByUserName.IsNull)
            {
                if (StringHelper.ValidateMD5(usersByUserName.UserPassword, StringHelper.MD5(this.TxtPassword.Text)))
                {
                    if ((usersByUserName.Status & UserStatus.WaitValidateByEmail) == UserStatus.WaitValidateByEmail)
                    {
                        if (usersByUserName.CheckNum == this.TxtCheckNum.Text)
                        {
                            UserStatus status = usersByUserName.Status ^ UserStatus.WaitValidateByEmail;
                            usersByUserName.Status = status;
                            Users.Update(usersByUserName);
                            if (status == UserStatus.None)
                            {
                                DynamicPage.WriteSuccessMsg("恭喜你正式成为本站的一员，请返回首页登录。", "Login.aspx");
                            }
                            else
                            {
                                DynamicPage.WriteSuccessMsg("恭喜你通过了Email验证。请等待管理开通你的帐号。开通后，你就正式正为本站的一员了。", "Login.aspx");
                            }
                        }
                        else
                        {
                            DynamicPage.WriteErrMsg("验证码不正确", "../Default.aspx");
                        }
                    }
                    else
                    {
                        DynamicPage.WriteErrMsg("已通过验证", "../Default.aspx");
                    }
                }
                else
                {
                    DynamicPage.WriteErrMsg("密码不正确", "../Default.aspx");
                }
            }
            else
            {
                DynamicPage.WriteErrMsg("不存在此用户", "../Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.TxtUserName.Text = BasePage.RequestString("UserName");
                this.TxtCheckNum.Text = BasePage.RequestString("CheckNum");
            }
        }
    }
}


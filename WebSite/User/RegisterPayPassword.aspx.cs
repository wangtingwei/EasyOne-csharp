namespace EasyOne.WebSite.User
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class RegisterPayPassword : DynamicPage
    {
        protected EasyOne.Controls.RequiredFieldValidator RegLogOnPassword;
        protected EasyOne.Controls.RequiredFieldValidator ReqTxtPwdConfirm;


        protected void BtnSetPayPassword_Click(object sender, EventArgs e)
        {
            string newValue = string.Empty;
            if (SiteConfig.UserConfig.EnableRegCompany)
            {
                newValue = "<li><a href='Company/RegCompany.aspx'>继续注册企业?</a></li>";
            }
            UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
            usersByUserName.PayPassword = StringHelper.MD5(this.TxtPassword.Text);
            Users.Update(usersByUserName);
            if (BasePage.RequestString("Url") == "Register")
            {
                DynamicPage.WriteSuccessMsg("<li>设置支付密码成功！</li>{$regCompanyMsg}".Replace("{$regCompanyMsg}", newValue), "../User/default.aspx");
            }
            else
            {
                DynamicPage.WriteSuccessMsg("<li>设置支付密码成功！</li>{$regCompanyMsg}".Replace("{$regCompanyMsg}", newValue), "../Shop/Payment.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Users.GetUsersByUserName(PEContext.Current.User.UserName).PayPassword))
            {
                base.Response.Redirect("default.aspx");
            }
        }
    }
}


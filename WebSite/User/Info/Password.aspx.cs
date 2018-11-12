namespace EasyOne.WebSite.User.Info
{
    using AjaxControlToolkit;
    using EasyOne.Api;
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

    public partial class Password : DynamicPage
    {


        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.TxtPasswords.Text != this.TxtConfirmPassword.Text)
            {
                DynamicPage.WriteErrMsg("<li>确认密码与新密码不一致！</li>");
            }
            UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
            if (StringHelper.MD5(this.TxtOldPassword.Text.Trim()) != usersByUserName.UserPassword)
            {
                DynamicPage.WriteErrMsg("<li>输入的旧密码不正确！</li>");
            }
            else
            {
                usersByUserName.UserPassword = StringHelper.MD5(this.TxtPasswords.Text);
                if (ApiData.IsAPiEnable())
                {
                    string str = ApiFunction.UpdateUser(usersByUserName.UserName, this.TxtPasswords.Text, null, null, null, null, null, null, null, null, null, null, null, null, null);
                    if (str != "true")
                    {
                        DynamicPage.WriteErrMsg("<li>" + str + "</li>");
                    }
                }
                Users.Update(usersByUserName);
                DynamicPage.WriteSuccessMsg("<li>密码修改成功！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.LblUserName.Text = PEContext.Current.User.UserName;
            }
        }
    }
}


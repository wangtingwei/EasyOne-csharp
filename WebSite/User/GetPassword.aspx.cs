namespace EasyOne.WebSite
{
    using EasyOne.Accessories;
    using EasyOne.Api;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class GetPassword : DynamicPage
    {
        protected string m_NewPassword = DataSecurity.MakeRandomString("abcdefghijklmnopqrstuvwxyz0123456789_*", 10);

        protected void btnChangePayPassword_Click(object sender, EventArgs e)
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(this.TxtUserName.Text);
            usersByUserName.PayPassword = StringHelper.MD5(this.txtNewPayPassword.Text);
            if (Users.Update(usersByUserName))
            {
                DynamicPage.WriteSuccessMsg("修改预付款支付密码成功！", "default.aspx");
            }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(this.TxtUserName.Text);
            if (this.RadPwdType.SelectedValue == "0")
            {
                string newPassword = this.m_NewPassword;
                usersByUserName.UserPassword = StringHelper.MD5(newPassword);
                MailInfo mailInfo = new MailInfo();
                mailInfo.FromName = SiteConfig.SiteInfo.WebmasterEmail;
                List<MailAddress> list = new List<MailAddress>();
                list.Add(new MailAddress(usersByUserName.Email));
                mailInfo.MailToAddressList = list;
                mailInfo.MailBody = "你在" + SiteConfig.SiteInfo.SiteName + "网站的用户新密码为" + newPassword + "，请及时到" + SiteConfig.SiteInfo.SiteUrl + "用户中心登录修改密码！";
                mailInfo.Subject = "新密码已发送到你的邮箱";
                if (SendMail.Send(mailInfo) == MailState.Ok)
                {
                    if (ApiData.IsAPiEnable())
                    {
                        string str2 = ApiFunction.UpdateUser(usersByUserName.UserName, newPassword, null, null, null, null, null, null, null, null, null, null, null, null, null);
                        if (str2 != "true")
                        {
                            DynamicPage.WriteErrMsg("<li>" + str2 + "</li>");
                        }
                    }
                    if (Users.Update(usersByUserName))
                    {
                        DynamicPage.WriteSuccessMsg("新密码已发送到你的邮箱！请注意查收并及时修改密码！ 如无法接收到，请与网站管理员联系！", "../default.aspx");
                    }
                    else
                    {
                        DynamicPage.WriteErrMsg("新密码已发送到你的邮箱！但网站系统无法修改旧密码，使用新密码可能无法登录！ 请与网站管理员联系！", "../default.aspx");
                    }
                }
                else
                {
                    DynamicPage.WriteErrMsg("新密码发送到你的邮箱时不成功，请与网站管理员联系！", "../default.aspx");
                }
            }
            else
            {
                string input = this.m_NewPassword;
                usersByUserName.PayPassword = StringHelper.MD5(input);
                MailInfo info3 = new MailInfo();
                info3.FromName = SiteConfig.SiteInfo.WebmasterEmail;
                List<MailAddress> list2 = new List<MailAddress>();
                list2.Add(new MailAddress(usersByUserName.Email));
                info3.MailToAddressList = list2;
                info3.MailBody = "你在" + SiteConfig.SiteInfo.SiteName + "网站的用户新预付款支付密码为" + input + "，请及时到" + SiteConfig.SiteInfo.SiteUrl + "用户中心登录修改预付款支付密码！";
                info3.Subject = "新预付款支付密码已发送到你的邮箱";
                if (SendMail.Send(info3) == MailState.Ok)
                {
                    if (Users.Update(usersByUserName))
                    {
                        DynamicPage.WriteSuccessMsg("新预付款支付密码已发送到你的邮箱！请注意查收并及时修改密码！ 如无法接收到，请与网站管理员联系！", "../default.aspx");
                    }
                    else
                    {
                        DynamicPage.WriteErrMsg("新预付款支付密码已发送到你的邮箱！但网站系统无法修改旧密码，使用新密码可能无法登录！ 请与网站管理员联系！", "../default.aspx");
                    }
                }
                else
                {
                    DynamicPage.WriteErrMsg("新预付款支付密码发送到你的邮箱时不成功，请与网站管理员联系！", "../default.aspx");
                }
            }
        }

        protected void BtnStep1_Click(object sender, EventArgs e)
        {
            this.PnlStep1.Visible = false;
            UserInfo usersByUserName = Users.GetUsersByUserName(this.TxtUserName.Text);
            if (!usersByUserName.IsNull)
            {
                if (string.IsNullOrEmpty(usersByUserName.Question) && string.IsNullOrEmpty(usersByUserName.Answer))
                {
                    this.PnlSendToEmail.Visible = true;
                }
                else
                {
                    this.LitQuestion.Text = usersByUserName.Question;
                    this.PnlStep2.Visible = true;
                }
            }
            else
            {
                DynamicPage.WriteErrMsg("对不起，不存在该用户。");
            }
        }

        protected void BtnStep2_Click(object sender, EventArgs e)
        {
            this.PnlStep1.Visible = false;
            this.PnlStep2.Visible = false;
            if (this.TxtValidateCode.Text == this.VcodeLogOn.ValidateCodeValue)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(this.TxtUserName.Text);
                if (!usersByUserName.IsNull)
                {
                    string str = StringHelper.MD5(this.TxtAnswer.Text);
                    if (this.RadListPwdType.SelectedValue == "0")
                    {
                        if (StringHelper.ValidateMD5(usersByUserName.Answer, str))
                        {
                            if (SiteConfig.UserConfig.UserGetPasswordType != 0)
                            {
                                string newPassword = this.m_NewPassword;
                                usersByUserName.UserPassword = StringHelper.MD5(newPassword);
                                MailInfo mailInfo = new MailInfo();
                                mailInfo.FromName = SiteConfig.SiteInfo.WebmasterEmail;
                                List<MailAddress> list = new List<MailAddress>();
                                list.Add(new MailAddress(usersByUserName.Email));
                                mailInfo.MailToAddressList = list;
                                mailInfo.MailBody = "你在" + SiteConfig.SiteInfo.SiteName + "网站的用户新密码为" + newPassword + "，请及时到" + SiteConfig.SiteInfo.SiteUrl + "用户中心登录修改密码！";
                                mailInfo.Subject = "新密码已发送到你的邮箱";
                                if (SendMail.Send(mailInfo) == MailState.Ok)
                                {
                                    if (ApiData.IsAPiEnable())
                                    {
                                        string str3 = ApiFunction.UpdateUser(usersByUserName.UserName, newPassword, null, null, null, null, null, null, null, null, null, null, null, null, null);
                                        if (str3 != "true")
                                        {
                                            DynamicPage.WriteErrMsg("<li>" + str3 + "</li>");
                                        }
                                    }
                                    if (Users.Update(usersByUserName))
                                    {
                                        DynamicPage.WriteSuccessMsg("新密码已发送到你的邮箱！请注意查收并及时修改密码！ 如无法接收到，请与网站管理员联系！", "../default.aspx");
                                    }
                                    else
                                    {
                                        DynamicPage.WriteErrMsg("新密码已发送到你的邮箱！但网站系统无法修改旧密码，使用新密码可能无法登录！ 请与网站管理员联系！", "../default.aspx");
                                    }
                                }
                                else
                                {
                                    DynamicPage.WriteErrMsg("新密码发送到你的邮箱时不成功，请与网站管理员联系！", "../default.aspx");
                                }
                            }
                            else
                            {
                                this.PnlStep3.Visible = true;
                            }
                        }
                        else
                        {
                            DynamicPage.WriteErrMsg("你回答的问题不对");
                        }
                    }
                    else if (SiteConfig.UserConfig.UserGetPasswordType != 0)
                    {
                        string input = this.m_NewPassword;
                        usersByUserName.PayPassword = StringHelper.MD5(input);
                        MailInfo info3 = new MailInfo();
                        info3.FromName = SiteConfig.SiteInfo.WebmasterEmail;
                        List<MailAddress> list2 = new List<MailAddress>();
                        list2.Add(new MailAddress(usersByUserName.Email));
                        info3.MailToAddressList = list2;
                        info3.MailBody = "你在" + SiteConfig.SiteInfo.SiteName + "网站的用户新预付款支付密码为" + input + "，请及时到" + SiteConfig.SiteInfo.SiteUrl + "用户中心登录修改预付款支付密码！";
                        info3.Subject = "新预付款支付密码已发送到你的邮箱";
                        if (SendMail.Send(info3) == MailState.Ok)
                        {
                            if (Users.Update(usersByUserName))
                            {
                                DynamicPage.WriteSuccessMsg("新预付款支付密码已发送到你的邮箱！请注意查收并及时修改密码！ 如无法接收到，请与网站管理员联系！", "../default.aspx");
                            }
                            else
                            {
                                DynamicPage.WriteErrMsg("新预付款支付密码已发送到你的邮箱！但网站系统无法修改旧密码，使用新密码可能无法登录！ 请与网站管理员联系！", "../default.aspx");
                            }
                        }
                        else
                        {
                            DynamicPage.WriteErrMsg("新预付款支付密码发送到你的邮箱时不成功，请与网站管理员联系！", "../default.aspx");
                        }
                    }
                    else
                    {
                        this.PalChangePayPassword.Visible = true;
                    }
                }
            }
            else
            {
                DynamicPage.WriteErrMsg("你输入的验证码不对");
            }
        }

        protected void BtnStep3_Click(object sender, EventArgs e)
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(this.TxtUserName.Text);
            usersByUserName.UserPassword = StringHelper.MD5(this.TxtPassword.Text);
            if (ApiData.IsAPiEnable())
            {
                string str = ApiFunction.UpdateUser(usersByUserName.UserName, this.TxtPassword.Text, null, null, null, null, null, null, null, null, null, null, null, null, null);
                if (str != "true")
                {
                    DynamicPage.WriteErrMsg("<li>" + str + "</li>");
                }
            }
            if (Users.Update(usersByUserName))
            {
                DynamicPage.WriteSuccessMsg("修改密码成功！", "default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.PnlStep1.Visible = true;
                if (!SiteConfig.ShopConfig.IsPayPassword)
                {
                    this.RadListPwdType.Items.RemoveAt(1);
                    this.RadPwdType.Items.RemoveAt(1);
                }
            }
        }
    }
}


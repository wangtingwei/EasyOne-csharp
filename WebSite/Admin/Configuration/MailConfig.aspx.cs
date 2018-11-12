namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Net.Mail;
    using System.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class MailConfig : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                SiteConfigInfo config = SiteConfig.ConfigReadFromFile();
                config.MailConfig.MailServer = this.TxtMailServer.Text.Trim();
                config.MailConfig.MailServerUserName = this.TxtMailServerUserName.Text.Trim();
                config.MailConfig.MailServerPassWord = this.TxtMailServerPassWord.Text.Trim();
                config.MailConfig.MailFrom = this.TxtMailFrom.Text.Trim();
                config.MailConfig.Port = DataConverter.CLng(this.TxtPort.Text);
                config.MailConfig.EnabledSsl = this.ChkSsl.Checked;
                if (this.RadBasic.Checked)
                {
                    config.MailConfig.AuthenticationType = AuthenticationType.Basic;
                }
                else if (this.RadNTLM.Checked)
                {
                    config.MailConfig.AuthenticationType = AuthenticationType.Ntlm;
                }
                else
                {
                    config.MailConfig.AuthenticationType = AuthenticationType.None;
                }
                try
                {
                    new SiteConfig().Update(config);
                    SiteCache.Remove("EasyOneSiteConfig");
                    AdminPage.WriteSuccessMsg("邮件参数配置成功！", "MailConfig.aspx");
                }
                catch (SecurityException exception)
                {
                    AdminPage.WriteErrMsg(exception.Message);
                }
                catch (UnauthorizedAccessException exception2)
                {
                    AdminPage.WriteErrMsg(exception2.Message);
                }
            }
        }

        protected void BtnTest_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                MailInfo mailInfo = new MailInfo();
                mailInfo.Subject = mailInfo.MailBody = "这是一封测试邮件，如果您可以成功收到此邮件，则说明您的“邮件参数配置”设置正确。";
                mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
                mailInfo.MailToAddressList.Add(new MailAddress(this.TxtTestMail.Text.Trim()));
                if (SendMail.Send(mailInfo) == MailState.Ok)
                {
                    AdminPage.WriteSuccessMsg("邮件发送成功！");
                }
                else
                {
                    AdminPage.WriteErrMsg(mailInfo.Msg);
                }
            }
        }

        private void Modify()
        {
            if (!base.IsPostBack)
            {
                SiteConfigInfo info = SiteConfig.ConfigInfo();
                this.TxtMailServer.Text = info.MailConfig.MailServer;
                this.TxtMailServerUserName.Text = info.MailConfig.MailServerUserName;
                this.TxtMailServerPassWord.Text = info.MailConfig.MailServerPassWord;
                this.TxtMailFrom.Text = info.MailConfig.MailFrom;
                this.TxtPort.Text = info.MailConfig.Port.ToString();
                this.ChkSsl.Checked = info.MailConfig.EnabledSsl;
                switch (info.MailConfig.AuthenticationType)
                {
                    case AuthenticationType.None:
                        this.RadNone.Checked = true;
                        break;

                    case AuthenticationType.Basic:
                        this.RadBasic.Checked = true;
                        this.PalBasic.Enabled = true;
                        break;

                    case AuthenticationType.Ntlm:
                        this.RadNTLM.Checked = true;
                        break;

                    default:
                        this.RadNone.Checked = true;
                        break;
                }
                this.TxtMailServerPassWord.Attributes.Add("value", info.MailConfig.MailServerPassWord);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.TxtMailFrom.Attributes.Add("onkeyup", "GetMailConfig();");
                this.TxtMailFrom.Attributes.Add("onchange", "GetMailConfig();");
            }
            this.Modify();
        }
    }
}


namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Security;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class SmsConfig : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                SiteConfigInfo config = SiteConfig.ConfigReadFromFile();
                config.SmsConfig.UserName = this.TxtUserName.Text.Trim();
                config.SmsConfig.MD5Key = this.TxtMD5Key.Text.Trim();
                config.SmsConfig.AdminPhoneNumber = this.TxtAdminPhoneNumber.Text.Trim();
                config.SmsConfig.IsAutoSendMessage = DataConverter.CBoolean(this.RadlIsAutoSend.SelectedValue);
                config.SmsConfig.AdminPhoneNumber = this.TxtAdminPhoneNumber.Text.Trim();
                config.SmsConfig.OrderMessage = this.TxtOrderMessage.Text.Trim();
                config.SmsConfig.IsAutoSendCardNumber = DataConverter.CBoolean(this.RadlIsAutoSendCardNumber.SelectedValue);
                config.SmsConfig.ConfirmOrderMessage = this.TxtConfirmOrderMessage.Text.Trim();
                config.SmsConfig.RemitMessage = this.TxtRemitMessage.Text.Trim();
                config.SmsConfig.RefundmentMessage = this.TxtRefundmentMessage.Text.Trim();
                config.SmsConfig.InvoiceMessage = this.TxtInvoiceMessage.Text.Trim();
                config.SmsConfig.ConsignmentMessage = this.TxtConsignmentMessage.Text.Trim();
                config.SmsConfig.SendCardNumberMessage = this.TxtSendCardNumberMessage.Text.Trim();
                config.SmsConfig.UseLabel = this.TxtUseLabel.Text.Trim();
                config.SmsConfig.BankLogMessage = this.TxtBankLogMessage.Text.Trim();
                config.SmsConfig.IncomeLogMessage = this.TxtIncomeLogMessage.Text.Trim();
                config.SmsConfig.PayoutLogMessage = this.TxtPayoutLogMessage.Text.Trim();
                config.SmsConfig.ExchangePointMessage = this.TxtExchangePointMessage.Text.Trim();
                config.SmsConfig.EncouragePointMessage = this.TxtEncouragePointMessage.Text.Trim();
                config.SmsConfig.PayoutPointMessage = this.TxtPayoutPointMessage.Text.Trim();
                config.SmsConfig.ExchangePeriodMessage = this.TxtExchangePeriodMessage.Text.Trim();
                config.SmsConfig.EncouragePeriodMessage = this.TxtEncouragePeriodMessage.Text.Trim();
                config.SmsConfig.PayoutPeriodMessage = this.TxtPayoutPeriodMessage.Text.Trim();
                config.SmsConfig.CartInformMessage = this.TxtCartInformMessage.Text.Trim();
                config.SmsConfig.IsAutoSendStateMessage = DataConverter.CBoolean(this.RadlIsAutoSendStateMessage.SelectedValue);
                config.SmsConfig.ChangeStateMessage = this.TxtChangeStateMessage.Text.Trim();
                try
                {
                    new SiteConfig().Update(config);
                    SiteCache.Remove("EasyOneSiteConfig");
                    AdminPage.WriteSuccessMsg("手机短信配置保存成功！", "SmsConfig.aspx");
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

        private void ModifySmsConfig()
        {
            EasyOne.Components.SmsConfig smsConfig = SiteConfig.ConfigInfo().SmsConfig;
            this.TxtUserName.Text = smsConfig.UserName;
            this.TxtMD5Key.Attributes.Add("value", smsConfig.MD5Key);
            this.TxtMD5Key.Text = smsConfig.MD5Key;
            this.TxtAdminPhoneNumber.Text = smsConfig.AdminPhoneNumber;
            this.RadlIsAutoSend.SelectedValue = this.SelectValue(smsConfig.IsAutoSendMessage);
            this.TxtAdminPhoneNumber.Text = smsConfig.AdminPhoneNumber;
            this.TxtOrderMessage.Text = smsConfig.OrderMessage;
            this.RadlIsAutoSendCardNumber.SelectedValue = this.SelectValue(smsConfig.IsAutoSendCardNumber);
            this.TxtConfirmOrderMessage.Text = smsConfig.ConfirmOrderMessage;
            this.TxtRemitMessage.Text = smsConfig.RemitMessage;
            this.TxtRefundmentMessage.Text = smsConfig.RefundmentMessage;
            this.TxtInvoiceMessage.Text = smsConfig.InvoiceMessage;
            this.TxtConsignmentMessage.Text = smsConfig.ConsignmentMessage;
            this.TxtSendCardNumberMessage.Text = smsConfig.SendCardNumberMessage;
            this.TxtUseLabel.Text = smsConfig.UseLabel;
            this.TxtBankLogMessage.Text = smsConfig.BankLogMessage;
            this.TxtIncomeLogMessage.Text = smsConfig.IncomeLogMessage;
            this.TxtPayoutLogMessage.Text = smsConfig.PayoutLogMessage;
            this.TxtExchangePointMessage.Text = smsConfig.ExchangePointMessage;
            this.TxtEncouragePointMessage.Text = smsConfig.EncouragePointMessage;
            this.TxtPayoutPointMessage.Text = smsConfig.PayoutPointMessage;
            this.TxtExchangePeriodMessage.Text = smsConfig.ExchangePeriodMessage;
            this.TxtEncouragePeriodMessage.Text = smsConfig.EncouragePeriodMessage;
            this.TxtPayoutPeriodMessage.Text = smsConfig.PayoutPeriodMessage;
            this.TxtCartInformMessage.Text = smsConfig.CartInformMessage;
            this.RadlIsAutoSendStateMessage.SelectedValue = this.SelectValue(smsConfig.IsAutoSendStateMessage);
            this.TxtChangeStateMessage.Text = smsConfig.ChangeStateMessage;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteConfig.SiteInfo.ProductEdition.CompareTo("eShop") < 0)
            {
                this.CartInformMessage.Style.Add("display", "none");
                this.UseLabel.Style.Add("display", "none");
                this.ConsignmentMessage.Style.Add("display", "none");
                this.SendCardNumberMessage.Style.Add("display", "none");
                this.InvoiceMessage.Style.Add("display", "none");
                this.RefundmentMessage.Style.Add("display", "none");
                this.ConfirmOrderMessage.Style.Add("display", "none");
                this.OrderMessage.Style.Add("display", "none");
            }
            if (!this.Page.IsPostBack)
            {
                this.ModifySmsConfig();
            }
        }

        private string SelectValue(bool selected)
        {
            if (selected)
            {
                return "true";
            }
            return "false";
        }
    }
}


namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class PaymentLogDetail : AdminPage
    {

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BasePage.RequestString("ReturnUrl")))
            {
                BasePage.ResponseRedirect("PaymentLogManage.aspx");
            }
            else
            {
                BasePage.ResponseRedirect(BasePage.RequestString("ReturnUrl"));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                PaymentLogInfo paymentLogById = PaymentLog.GetPaymentLogById(BasePage.RequestInt32("PaymentLogID"));
                if (!paymentLogById.IsNull)
                {
                    this.LblPaymentNum.Text = paymentLogById.PaymentNum;
                    this.LblUserName.Text = paymentLogById.UserName;
                    PayPlatformInfo payPlatformById = PayPlatform.GetPayPlatformById(paymentLogById.PlatformId);
                    if (!paymentLogById.IsNull)
                    {
                        this.LblPlatform.Text = payPlatformById.PayPlatformName;
                    }
                    this.LblPayTime.Text = paymentLogById.PayTime.ToString();
                    this.LblMoneyPay.Text = paymentLogById.MoneyPay.ToString("0.00");
                    this.LblMoneyTrue.Text = paymentLogById.MoneyTrue.ToString("0.00");
                    this.LblStatus.Text = PaymentLog.GetStatusDepict(paymentLogById.PlatformId, paymentLogById.Status);
                    this.LblPlatformInfo.Text = paymentLogById.PlatformInfo;
                    this.LblRemark.Text = paymentLogById.Remark;
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>找不到对应的支付记录</li>");
                }
            }
        }
    }
}


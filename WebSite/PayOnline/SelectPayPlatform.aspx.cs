namespace EasyOne.WebSite.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using EasyOne.ModelControls;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class SelectPayPlatform : DynamicPage
    {
        private int m_OrderId;
        private int m_PointAmonut;
        protected ShowPointName ShowPointName1;
        protected ShowPointName ShowPointName5;
        protected ShowPointName ShowPointName6;


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(string.Concat(new object[] { "PayOnline.aspx?PayPlatformID=", this.DropPayPlatform.SelectedValue, "&vMoney=", this.TxtvMoney.Text, "&OrderID=", this.m_OrderId, "&PointAmount=", this.m_PointAmonut }));
        }

        private void InitBuyPoint()
        {
            double num = SiteConfig.UserConfig.MoneyExchangePointByMoney / SiteConfig.UserConfig.MoneyExchangePointByPoint;
            this.LblPointPrice.Text = num.ToString("0.00");
            this.LblPointAmount.Text = this.m_PointAmonut.ToString();
            this.LblPayForPoint.Text = (num * this.m_PointAmonut).ToString("0.00");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_OrderId = BasePage.RequestInt32("OrderID");
            this.m_PointAmonut = BasePage.RequestInt32("PointAmount");
            if (!this.Page.IsPostBack)
            {
                if (this.m_OrderId > 0)
                {
                    OrderInfo orderById = Order.GetOrderById(this.m_OrderId);
                    this.PlhOrderInfo.Visible = true;
                    this.PlhMoney.Visible = false;
                    this.PlhBuyPoint.Visible = false;
                    this.LblOrderNum.Text = orderById.OrderNum;
                    this.LblMoneyTotal.Text = orderById.MoneyTotal.ToString("0.00");
                    this.LblMoneyReceipt.Text = orderById.MoneyReceipt.ToString("0.00");
                    this.LblNeedPay.Text = (orderById.MoneyTotal - orderById.MoneyReceipt).ToString("0.00");
                }
                else
                {
                    if (string.IsNullOrEmpty(PEContext.Current.User.UserName))
                    {
                        base.Response.Redirect("../User/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode("../PayOnline/SelectPayPlatform.aspx?PointAmount=" + this.m_PointAmonut));
                    }
                    if (this.m_PointAmonut > 0)
                    {
                        this.PlhOrderInfo.Visible = false;
                        this.PlhMoney.Visible = false;
                        this.PlhBuyPoint.Visible = true;
                        this.InitBuyPoint();
                    }
                }
                this.DropPayPlatform.DataSource = PayPlatform.GetListOfEnabled();
                this.DropPayPlatform.DataBind();
            }
        }
    }
}


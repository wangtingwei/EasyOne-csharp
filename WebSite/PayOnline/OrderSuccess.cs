namespace EasyOne.WebSite.Shop
{
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class OrderSuccess : DynamicPage
    {
        protected Button BtnPayByBalance;
        protected Button BtnReturn;
        protected Button BtnShowOrder;
        protected HtmlForm form1;
        protected HiddenField HdnContacterName;
        protected HiddenField HdnOrderNum;
        protected Label LblOrderNum;
        protected Label LblTotolMoney;

        protected void BtnPayByBalance_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("../User/Shop/AddPayment.aspx?AddOrder=success&OrderId=" + BasePage.RequestInt32("OrderID").ToString());
        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            string againBuyUrl = SiteConfig.ShopConfig.AgainBuyUrl;
            if (string.IsNullOrEmpty(againBuyUrl))
            {
                againBuyUrl = "~/Category.aspx?id=4";
            }
            BasePage.ResponseRedirect(againBuyUrl);
        }

        protected void BtnShowOrder_Click(object sender, EventArgs e)
        {
            int num = BasePage.RequestInt32("OrderID");
            if (num > 0)
            {
                if (PEContext.Current.User.Identity.IsAuthenticated)
                {
                    BasePage.ResponseRedirect("../User/Shop/ShowOrder.aspx?OrderId=" + num.ToString());
                }
                else
                {
                    BasePage.ResponseRedirect("OrderForm.aspx?OrderNum=" + this.HdnOrderNum.Value + "&Name=" + this.HdnContacterName.Value);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (BasePage.RequestInt32("OrderID") > 0)
            {
                OrderInfo orderById = Order.GetOrderById(BasePage.RequestInt32("OrderID"));
                if (!orderById.IsNull)
                {
                    this.LblOrderNum.Text = orderById.OrderNum;
                    this.LblTotolMoney.Text = orderById.MoneyTotal.ToString("N2");
                    this.HdnOrderNum.Value = orderById.OrderNum;
                    this.HdnContacterName.Value = orderById.ContacterName;
                    this.BtnPayByBalance.Visible = orderById.PaymentType == 2;
                }
            }
        }
    }
}


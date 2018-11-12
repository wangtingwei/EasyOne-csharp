namespace EasyOne.WebSite.Shop
{
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class OrderForm : DynamicPage
    {
        protected Button btnSubmit;
        protected HtmlForm form1;
        protected Panel panOrderDetail;
        protected Panel panSearchForm;
        protected RequiredFieldValidator regContactName;
        protected RequiredFieldValidator regOrderId;
        protected ShowOrderDetail showOrderDetail;
        protected TextBox txtContactName;
        protected TextBox txtOrderNum;

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            this.ShowOrder(this.txtOrderNum.Text, this.txtContactName.Text);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.panSearchForm.Visible = true;
                this.panOrderDetail.Visible = false;
                string str = BasePage.RequestString("OrderNum");
                string str2 = BasePage.RequestString("Name");
                if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
                {
                    this.ShowOrder(str, str2);
                }
            }
        }

        private void ShowOrder(string orderNum, string contentName)
        {
            if (string.IsNullOrEmpty(orderNum))
            {
                DynamicPage.WriteErrMsg("<li>请输入订单编号！</li>", "OrderForm.aspx");
            }
            if (string.IsNullOrEmpty(contentName))
            {
                DynamicPage.WriteErrMsg("<li>请输入收货人姓名！</li>", "OrderForm.aspx");
            }
            OrderInfo anonymousOrderInfo = Order.GetAnonymousOrderInfo(orderNum, contentName);
            if (anonymousOrderInfo.IsNull)
            {
                DynamicPage.WriteErrMsg("<li>指定的订单信息不存在！</li>", "OrderForm.aspx");
            }
            else
            {
                this.panSearchForm.Visible = false;
                this.panOrderDetail.Visible = true;
                this.showOrderDetail.ShowInfo(anonymousOrderInfo);
            }
        }
    }
}


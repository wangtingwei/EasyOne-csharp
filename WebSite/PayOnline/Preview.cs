namespace EasyOne.WebSite.Shop
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.Shop;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Preview : DynamicPage
    {
        protected Button BtnCoupon;
        protected Button BtnModifyInfo;
        protected Button BtnSubmitOrder;
        protected HtmlForm form1;
        protected HiddenField HdnCouponID;
        protected HiddenField HdnDeliverType;
        protected HiddenField HdnPaymentTypeId;
        protected HiddenField HdnPayPlatformId;
        protected HiddenField HdnTotalMoney;
        protected Label LblAddress;
        protected Label LblAgentName;
        protected Label LblContacterName;
        protected Label LblDeliverTime;
        protected Label LblDeliverType;
        protected Label LblEmail;
        protected Label LblInvoiceContent;
        protected Label LblMobile;
        protected Label LblPaymentType;
        protected Label LblPhone;
        protected Label LblRemark;
        protected Label LblUseCouponMsg;
        protected Label LblZipCode;
        protected LinkButton LbtnCancelCoupon;
        protected PlaceHolder PlhCoupon;
        protected ScriptManager ScriptManager1;
        protected EasyOne.WebSite.Controls.ShoppingCartUI ShoppingCart1;
        protected TextBox TxtCouponNum;
        protected UpdatePanel UpdatePanel1;
        protected UpdatePanel UpnlCoupon;
        protected RequiredFieldValidator ValrCoupon;

        protected void BtnCoupon_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                CouponItemInfo couponItemInfo = CouponItem.GetCouponItemInfo(this.TxtCouponNum.Text, PEContext.Current.User.UserId);
                if (couponItemInfo.IsNull)
                {
                    this.LblUseCouponMsg.Text = "对不起，不存在该优惠券，或者该优惠券您没有使用权限！";
                }
                else
                {
                    decimal totalMoney = DataConverter.CDecimal(this.HdnTotalMoney.Value);
                    CouponInfo couponInfoById = Coupon.GetCouponInfoById(couponItemInfo.CouponId);
                    string str = "";
                    switch (Coupon.CheckUsePurview(couponInfoById, couponItemInfo, totalMoney, this.FlowInfo.ShoppingCartId))
                    {
                        case 1:
                            str = "对不起，该优惠券使用次数已经超过限用次数，不能再使用！";
                            break;

                        case 2:
                            str = "对不起，该优惠券已经过期，不能再使用！";
                            break;

                        case 3:
                            str = "对不起，使用该优惠券需要订单总额达到" + couponInfoById.OrderTotalMoney.ToString("0.00") + "元！";
                            break;

                        case 4:
                        case 5:
                            str = "对不起，需购买指定的商品才能使用该优惠券！";
                            break;
                    }
                    if (!string.IsNullOrEmpty(str))
                    {
                        this.LblUseCouponMsg.Text = str;
                    }
                    else
                    {
                        this.LblUseCouponMsg.Text = "使用" + couponInfoById.CouponName + "，面值为" + couponInfoById.Money.ToString("0.00") + "元";
                        this.LblUseCouponMsg.ForeColor = Color.Blue;
                        this.ShoppingCart1.CouponMoney = couponInfoById.Money;
                        this.LbtnCancelCoupon.Visible = true;
                        this.HdnCouponID.Value = couponInfoById.CouponId.ToString();
                    }
                }
                this.ShoppingCart1.UseCoupon = true;
                this.ShoppingCart1.CartId = this.FlowInfo.ShoppingCartId;
                this.ShoppingCart1.ZipCode = this.FlowInfo.ZipCode;
                this.ShoppingCart1.PaymentType = this.FlowInfo.PaymentType;
                this.ShoppingCart1.DeliverType = this.FlowInfo.DeliverType;
                this.ShoppingCart1.NeedInvoice = this.FlowInfo.NeedInvoice;
            }
        }

        protected void BtnSubmitOrder_Click(object sender, EventArgs e)
        {
            OrderFlowInfo flowInfo = this.FlowInfo;
            flowInfo.OrderId = Order.GetMaxId() + 1;
            flowInfo.BeginDate = DateTime.Today;
            UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
            int category = PaymentType.GetPaymentTypeById(DataConverter.CLng(this.HdnPaymentTypeId.Value)).Category;
            int couponId = DataConverter.CLng(this.HdnCouponID.Value);
            switch (Order.Add(flowInfo, usersByUserName, flowInfo.ShoppingCartId, couponId, this.ShoppingCart1.TrueTotalMoney))
            {
                case 1:
                case 2:
                {
                    string continueBuy = SiteConfig.ShopConfig.ContinueBuy;
                    if (string.IsNullOrEmpty(continueBuy))
                    {
                        continueBuy = "~/Category.aspx?id=4";
                    }
                    DynamicPage.WriteErrMsg("<li>您好！目前您的购物车中没有任何商品，点击返回上一页继续购物？</li>", continueBuy);
                    return;
                }
                case 0x63:
                    if (couponId > 0)
                    {
                        CouponItem.AddUseTimes(this.TxtCouponNum.Text, PEContext.Current.User.UserId);
                    }
                    switch (category)
                    {
                        case 1:
                            base.Response.Redirect("../PayOnline/PayOnline.aspx?AddOrder=success&OrderID=" + flowInfo.OrderId.ToString() + "&PayPlatformId=" + this.HdnPayPlatformId.Value);
                            return;
                    }
                    base.Response.Redirect("OrderSuccess.aspx?OrderID=" + flowInfo.OrderId.ToString());
                    return;
            }
        }

        protected void LbtnCancelCoupon_Click(object sender, EventArgs e)
        {
            this.ShoppingCart1.UseCoupon = true;
            this.ShoppingCart1.CouponMoney = 0M;
            this.ShoppingCart1.CartId = this.FlowInfo.ShoppingCartId;
            this.ShoppingCart1.ZipCode = this.FlowInfo.ZipCode;
            this.ShoppingCart1.PaymentType = this.FlowInfo.PaymentType;
            this.ShoppingCart1.DeliverType = this.FlowInfo.DeliverType;
            this.ShoppingCart1.NeedInvoice = this.FlowInfo.NeedInvoice;
            this.LbtnCancelCoupon.Visible = false;
            this.TxtCouponNum.Text = "";
            this.LblUseCouponMsg.Text = "";
            this.HdnCouponID.Value = "";
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);
            this.HdnTotalMoney.Value = this.ShoppingCart1.TrueTotalMoney.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ShoppingCart1.IsPreview = 1;
            if (!base.IsPostBack)
            {
                if (base.PreviousPage != null)
                {
                    Payment previousPage = base.PreviousPage as Payment;
                    if (previousPage != null)
                    {
                        if (!string.IsNullOrEmpty(PEContext.Current.User.UserName) && SiteConfig.ShopConfig.EnableCoupon)
                        {
                            this.PlhCoupon.Visible = true;
                        }
                        OrderFlowInfo flowInfo = previousPage.FlowInfo;
                        this.FlowInfo = flowInfo;
                        this.ShoppingCart1.CartId = flowInfo.ShoppingCartId;
                        DropDownList list = (DropDownList) previousPage.FindControl("DropDeliverType");
                        DropDownList list2 = (DropDownList) previousPage.FindControl("DropPaymentType");
                        this.ShoppingCart1.ZipCode = flowInfo.ZipCode;
                        this.ShoppingCart1.PaymentType = flowInfo.PaymentType;
                        this.ShoppingCart1.DeliverType = flowInfo.DeliverType;
                        this.ShoppingCart1.NeedInvoice = flowInfo.NeedInvoice;
                        this.LblContacterName.Text = flowInfo.ConsigneeName;
                        if (flowInfo.Country != "中华人民共和国")
                        {
                            this.LblAddress.Text = flowInfo.Country;
                        }
                        string text = this.LblAddress.Text;
                        this.LblAddress.Text = text + flowInfo.Province + flowInfo.City + flowInfo.Area + flowInfo.Address;
                        this.LblEmail.Text = flowInfo.Email;
                        this.LblMobile.Text = flowInfo.Mobile;
                        this.LblPhone.Text = flowInfo.HomePhone;
                        this.LblZipCode.Text = flowInfo.ZipCode;
                        this.LblAgentName.Text = flowInfo.AgentName;
                        if (flowInfo.NeedInvoice)
                        {
                            this.LblInvoiceContent.Text = flowInfo.InvoiceContent;
                        }
                        this.LblRemark.Text = flowInfo.Remark;
                        this.LblDeliverType.Text = list.SelectedItem.Text;
                        this.HdnDeliverType.Value = list.SelectedValue;
                        this.LblPaymentType.Text = list2.SelectedItem.Text;
                        this.HdnPaymentTypeId.Value = list2.SelectedValue;
                        this.HdnPayPlatformId.Value = ((RadioButtonList) previousPage.FindControl("RadlPayPlatform")).SelectedValue;
                        this.LblDeliverTime.Text = flowInfo.DeliveryTime;
                    }
                }
                if ((base.PreviousPage == null) || !(base.PreviousPage is Payment))
                {
                    this.BtnModifyInfo.Enabled = false;
                    this.BtnSubmitOrder.Enabled = false;
                }
            }
        }

        public OrderFlowInfo FlowInfo
        {
            get
            {
                if (this.ViewState["FlowInfo"] == null)
                {
                    this.ViewState["FlowInfo"] = new OrderFlowInfo();
                }
                return (OrderFlowInfo) this.ViewState["FlowInfo"];
            }
            private set
            {
                this.ViewState["FlowInfo"] = value;
            }
        }
    }
}


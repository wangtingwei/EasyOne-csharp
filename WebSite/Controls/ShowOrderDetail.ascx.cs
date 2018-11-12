namespace EasyOne.WebSite.Controls
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ShowOrderDetail : BaseUserControl
    {
        private bool m_HaveCard;
        private bool m_HavePracticality;
        private bool m_HaveService;
        private bool m_HaveSoft;
        protected string m_MoneyReceipt = "";
        protected decimal m_SubTotal;
        protected StringBuilder m_SumInfo;
        protected decimal m_TotalMoney;
        private int m_TotalPresentExp;
        private decimal m_TotalPresentMoney;
        private int m_TotalPresentPoint;

        private void BindControl(OrderInfo info)
        {
            this.LblOrderNum.Text = info.OrderNum;
            this.HlkUserName.Text = info.UserName;
            this.HlkAgentName.Text = info.AgentName;
            this.HlkClientName.Text = info.ClientName;
            if (this.IsAdminPage)
            {
                this.HlkUserName.NavigateUrl = string.Format("~/Admin/User/UserShow.aspx?UserName={0}", base.Server.UrlEncode(info.UserName));
                this.HlkClientName.NavigateUrl = string.Format("~/Admin/Crm/ClientShow.aspx?ClientId={0}", info.ClientId);
                this.HlkAgentName.NavigateUrl = string.Format("~/Admin/User/UserShow.aspx?UserName={0}", base.Server.UrlEncode(info.AgentName));
            }
            this.CliendId = info.ClientId;
            if (info.NeedInvoice)
            {
                this.LblNeedInvoice.Text = "√";
            }
            else
            {
                this.LblNeedInvoice.Text = "\x00d7";
                this.LblNeedInvoice.ForeColor = Color.FromArgb(0xff0000);
            }
            if (info.Invoiced)
            {
                this.LblInvoiced.Text = "√";
            }
            else
            {
                this.LblInvoiced.Text = "\x00d7";
                this.LblInvoiced.ForeColor = Color.FromArgb(0xff0000);
            }
            this.LblStatus.Text = BaseUserControl.EnumToHtml<OrderStatus>(info.Status);
            switch (Order.GetPayStatus(info))
            {
                case PayStatus.WaitForPay:
                    this.LblMoneyTotal.Text = BaseUserControl.EnumToHtml<PayStatus>(PayStatus.WaitForPay);
                    break;

                case PayStatus.ReceivedEarnest:
                    this.LblMoneyTotal.Text = BaseUserControl.EnumToHtml<PayStatus>(PayStatus.ReceivedEarnest);
                    break;

                case PayStatus.Payoff:
                    this.LblMoneyTotal.Text = BaseUserControl.EnumToHtml<PayStatus>(PayStatus.Payoff);
                    break;
            }
            this.LblDeliverStatus.Text = BaseUserControl.EnumToHtml<DeliverStatus>(info.DeliverStatus);
            if (info.NeedInvoice)
            {
                this.LblInvoiceContent.Text = info.InvoiceContent;
            }
            this.LblRemark.Text = info.Remark;
            this.LblBeginDate.Text = info.BeginDate.ToString("yyyy-MM-dd");
            this.LblInputTime.Text = info.InputTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.LblContacterName.Text = info.ContacterName;
            this.LblAddress.Text = info.Address;
            this.LblZipCode.Text = info.ZipCode;
            this.LblMobile.Text = info.Mobile;
            this.LblPhone.Text = info.Phone;
            this.LblEmail.Text = info.Email;
            this.LblPaymentType.Text = PaymentType.GetPaymentTypeById(info.PaymentType).TypeName;
            this.LblDeliverType.Text = DeliverType.GetDeliverTypeById(info.DeliverType).TypeName;
            this.LblOutOfStockProject.Text = BaseUserControl.EnumToHtml<OutOfStockProject>(info.OutOfStockProject);
            this.LblDeliverTime.Text = info.DeliveryTime;
            if (this.IsAdminPage)
            {
                this.LblMemo.Text = info.Memo;
                this.LblMemo.Visible = true;
                this.LtrMemoTitle.Visible = true;
                this.ShowFunctionary.Visible = true;
                this.LblFunctionary.Text = info.Functionary;
                this.LblOrderType.Text = Choiceset.GetDataText("PE_Orders", "OrderType", info.OrderType);
            }
            else
            {
                this.LblMemo.Visible = false;
                this.LtrMemoTitle.Visible = false;
                this.ShowFunctionary.Visible = false;
            }
        }

        protected string GetProductName(string productName, string property, int saleType)
        {
            property = string.IsNullOrEmpty(property) ? string.Empty : ("（" + property + "）");
            return (productName + property + Product.ShowProductType(saleType));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RptOrderItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ShopConfig shopConfig = SiteConfig.ShopConfig;
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                OrderItemInfo dataItem = (OrderItemInfo) e.Item.DataItem;
                Literal literal = e.Item.FindControl("LtrServiceTerm") as Literal;
                HyperLink link = e.Item.FindControl("LnkProduct") as HyperLink;
                if (this.IsAdminPage)
                {
                    if (!string.IsNullOrEmpty(dataItem.TableName))
                    {
                        link.NavigateUrl = string.Concat(new object[] { "../", SiteConfig.SiteOption.ManageDir, "/Shop/ProductView.aspx?GeneralID=", dataItem.ProductId });
                    }
                    else
                    {
                        link.NavigateUrl = string.Concat(new object[] { "../", SiteConfig.SiteOption.ManageDir, "/Shop/PresentView.aspx?presentId=", dataItem.ProductId });
                    }
                }
                else
                {
                    link.NavigateUrl = new InsideStaticLabel().GetInfoPath(dataItem.ProductId.ToString());
                    link.Target = "_Blank";
                }
                if (dataItem.ServiceTerm != 0)
                {
                    DateTime time;
                    if (dataItem.ServiceTermUnit == ServiceTermUnit.Year)
                    {
                        time = dataItem.BeginDate.AddYears(dataItem.ServiceTerm);
                    }
                    else if (dataItem.ServiceTermUnit == ServiceTermUnit.Month)
                    {
                        time = dataItem.BeginDate.AddMonths(dataItem.ServiceTerm);
                    }
                    else
                    {
                        time = dataItem.BeginDate.AddDays((double) dataItem.ServiceTerm);
                    }
                    if (DateTime.Compare(time, DateTime.Now) > 0)
                    {
                        literal.Text = dataItem.ServiceTerm.ToString() + BaseUserControl.EnumToHtml<ServiceTermUnit>(dataItem.ServiceTermUnit);
                    }
                    else
                    {
                        literal.Text = "<font color='red'>" + dataItem.ServiceTerm.ToString() + BaseUserControl.EnumToHtml<ServiceTermUnit>(dataItem.ServiceTermUnit) + "</font>";
                    }
                }
                else
                {
                    literal.Text = dataItem.ServiceTerm.ToString() + BaseUserControl.EnumToHtml<ServiceTermUnit>(dataItem.ServiceTermUnit);
                }
                if (!string.IsNullOrEmpty(dataItem.Remark))
                {
                    ((Label) e.Item.FindControl("LblItemRemark")).Text = "查看";
                    ((Label) e.Item.FindControl("LblItemRemark")).ToolTip = dataItem.Remark;
                }
                if ((!this.m_HaveCard && Product.CharacterIsExists(dataItem.ProductCharacter, ProductCharacter.Card)) && Cards.GetCardByOrderItemId(dataItem.ProductId, dataItem.TableName, dataItem.ItemId).IsNull)
                {
                    this.m_HaveCard = true;
                }
                if (!this.m_HaveSoft && Product.CharacterIsExists(dataItem.ProductCharacter, ProductCharacter.Download))
                {
                    this.m_HaveSoft = true;
                }
                if (!this.m_HavePracticality && Product.CharacterIsExists(dataItem.ProductCharacter, ProductCharacter.Practicality))
                {
                    this.m_HavePracticality = true;
                }
                if (!this.m_HaveService && Product.CharacterIsExists(dataItem.ProductCharacter, ProductCharacter.Service))
                {
                    this.m_HaveService = true;
                }
                ExtendedImage image = (ExtendedImage) e.Item.FindControl("extendedImage");
                Control control = e.Item.FindControl("ProductImage");
                if (!shopConfig.IsOrderProductListShowThumb)
                {
                    image.Visible = false;
                    control.Visible = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(dataItem.TableName))
                    {
                        ProductInfo productById = Product.GetProductById(dataItem.ProductId);
                        if (!string.IsNullOrEmpty(productById.ProductThumb))
                        {
                            image.Src = productById.ProductThumb;
                        }
                        else
                        {
                            image.Src = SiteConfig.SiteInfo.VirtualPath + "Images/nopic.gif";
                        }
                    }
                    else
                    {
                        PresentInfo presentById = Present.GetPresentById(dataItem.ProductId);
                        if (!string.IsNullOrEmpty(presentById.PresentThumb))
                        {
                            image.Src = presentById.PresentThumb;
                        }
                        else
                        {
                            image.Src = SiteConfig.SiteInfo.VirtualPath + "Images/nopic.gif";
                        }
                    }
                    if (shopConfig.OrderProductListThumbsHeight != 0)
                    {
                        image.ImageHeight = shopConfig.OrderProductListThumbsHeight;
                    }
                    else if (shopConfig.OrderProductListThumbsWidth != 0)
                    {
                        image.ImageWidth = shopConfig.OrderProductListThumbsWidth;
                    }
                }
                this.m_SubTotal += dataItem.Amount * dataItem.TruePrice;
                this.m_TotalPresentExp += dataItem.Amount * dataItem.PresentExp;
                this.m_TotalPresentMoney += dataItem.Amount * dataItem.PresentMoney;
                this.m_TotalPresentPoint += dataItem.Amount * dataItem.PresentPoint;
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                Control control2 = e.Item.FindControl("ProductImageTitle");
                if (!shopConfig.IsOrderProductListShowThumb)
                {
                    control2.Visible = false;
                }
            }
        }

        public void ShowInfo(OrderInfo info)
        {
            if (!info.IsNull)
            {
                this.BindControl(info);
                this.RptOrderItem.DataSource = OrderItem.GetInfoListByOrderId(info.OrderId);
                this.RptOrderItem.DataBind();
                this.m_SumInfo = new StringBuilder();
                this.m_SumInfo.Append(" 运费：");
                this.m_SumInfo.Append(info.ChargeDeliver.ToString("N2"));
                this.m_SumInfo.Append("元 ");
                decimal subTotal = this.m_SubTotal;
                if (info.CouponId > 0)
                {
                    CouponInfo couponInfoById = Coupon.GetCouponInfoById(info.CouponId);
                    if (!couponInfoById.IsNull)
                    {
                        this.m_SumInfo.Append("<br>使用" + couponInfoById.CouponName + "，面值为" + couponInfoById.Money.ToString("N2") + "元");
                        this.m_SumInfo.Append("，使用后商品实际价格为：");
                        subTotal = this.m_SubTotal - couponInfoById.Money;
                        if (subTotal < 0M)
                        {
                            subTotal = 0M;
                        }
                        this.m_SumInfo.Append(subTotal.ToString("N2"));
                        this.m_SumInfo.Append("元");
                    }
                }
                string str = "";
                this.m_TotalMoney = subTotal;
                str = ("实际金额：" + this.m_TotalMoney.ToString("N2")) + " + " + info.ChargeDeliver.ToString("N2");
                this.m_TotalMoney += info.ChargeDeliver;
                str = str + "＝" + this.m_TotalMoney.ToString("N2") + "元";
                this.m_SumInfo.Append("<br/>");
                this.m_SumInfo.Append(str);
                this.m_SumInfo.Append("<br/>");
                this.m_SumInfo.Append("返还 <font color='red'>");
                this.m_SumInfo.Append((info.PresentMoney + this.m_TotalPresentMoney).ToString("N2"));
                this.m_SumInfo.Append("</font>");
                this.m_SumInfo.Append(" 元现金券，赠送 <font color='red'>");
                this.m_SumInfo.Append((info.PresentExp + this.m_TotalPresentExp).ToString());
                this.m_SumInfo.Append("</font>");
                this.m_SumInfo.Append("  点积分，赠送 <font color='red'>");
                this.m_SumInfo.Append((info.PresentPoint + this.m_TotalPresentPoint).ToString());
                this.m_SumInfo.Append("</font>");
                this.m_SumInfo.Append(SiteConfig.UserConfig.PointUnit + SiteConfig.UserConfig.PointName);
                if (info.MoneyReceipt < info.MoneyTotal)
                {
                    this.m_MoneyReceipt = "<font color=red>" + info.MoneyReceipt.ToString("N2") + "</font>";
                    this.m_MoneyReceipt = this.m_MoneyReceipt + "<br /><font color=blue> 尚欠款：" + ((info.MoneyTotal - info.MoneyReceipt)).ToString("N2") + "</font>";
                }
                else
                {
                    this.m_MoneyReceipt = "<font color=red>" + info.MoneyReceipt.ToString("N2") + "</font>";
                }
            }
        }

        public int CliendId
        {
            get
            {
                return DataConverter.CLng(this.ViewState["ClientId"]);
            }
            set
            {
                this.ViewState["ClientId"] = value;
            }
        }

        public string Email
        {
            get
            {
                return this.LblEmail.Text;
            }
        }

        public bool HaveCard
        {
            get
            {
                return this.m_HaveCard;
            }
        }

        public bool HavePracticality
        {
            get
            {
                return this.m_HavePracticality;
            }
        }

        public bool HaveService
        {
            get
            {
                return this.m_HaveService;
            }
        }

        public bool HaveSoft
        {
            get
            {
                return this.m_HaveSoft;
            }
        }

        public bool IsAdminPage
        {
            get
            {
                return ((this.ViewState["IsAdminPage"] != null) && Convert.ToBoolean(this.ViewState["IsAdminPage"]));
            }
            set
            {
                this.ViewState["IsAdminPage"] = value;
            }
        }

        public string OrderDetailHtml
        {
            get
            {
                StringWriter writer = new StringWriter();
                HtmlTextWriter writer2 = new HtmlTextWriter(writer);
                base.Render(writer2);
                return writer.ToString();
            }
        }

        public string UserName
        {
            get
            {
                return this.HlkUserName.Text;
            }
        }
    }
}


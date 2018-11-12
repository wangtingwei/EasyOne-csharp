namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.Shop;
    using EasyOne.Templates;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ShoppingCartUI : BaseUserControl
    {
        protected int changePresentId;
        private string m_CartId;
        private decimal m_CouponMoney;
        private int m_DeliverType;
        private int m_IsPreview;
        private bool m_NeedInvoice;
        private int m_PaymentType;
        private bool m_UseCoupon;
        private UserInfo m_UserInfo;
        private string m_ZipCode;
        protected string presentExpInfomation;
        protected StringBuilder presentList;
        protected StringBuilder presentList2;
        protected string presentTableName;
        protected StringBuilder priceInfomation;
        protected Product product;
        protected decimal total;
        protected int totalExp;
        protected decimal totalMoney;
        protected int totalPoint;
        protected double weight;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.product = new Product();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!this.UseCoupon)
            {
                this.PresentId = base.Request.Form["RdbPresentId"];
            }
            string userName = string.Empty;
            IList<ShoppingCartInfo> list = ShoppingCart.GetList(0, 0x7fffffff, 4, this.m_CartId);
            if (this.m_IsPreview == 3)
            {
                foreach (ShoppingCartInfo info in list)
                {
                    userName = info.UserName;
                    break;
                }
            }
            else
            {
                userName = PEContext.Current.User.UserName;
            }
            if (!string.IsNullOrEmpty(userName))
            {
                this.m_UserInfo = Users.GetUsersByUserName(userName);
                UserPurviewInfo userPurview = this.m_UserInfo.UserPurview;
                this.ViewState["HaveWholesalePurview"] = userPurview.Enablepm;
            }
            else
            {
                this.ViewState["HaveWholesalePurview"] = false;
            }
            this.RptShoppingCart.DataSource = list;
            this.RptShoppingCart.DataBind();
            if (this.m_IsPreview == 0)
            {
                PresentProjectInfo presentProjectByTotalMoney = PresentProject.GetPresentProjectByTotalMoney(this.total);
                if (!presentProjectByTotalMoney.IsNull && presentProjectByTotalMoney.PresentContent.Contains("1"))
                {
                    this.RptPresentList.Visible = true;
                    this.Note.Visible = true;
                    this.LblPrice.Visible = true;
                    this.LblPrice.Text = presentProjectByTotalMoney.Price.ToString("0.00");
                    string[] strArray = presentProjectByTotalMoney.PresentId.Split(new char[] { ',' });
                    IList<PresentInfo> list2 = new List<PresentInfo>();
                    foreach (string str2 in strArray)
                    {
                        list2.Add(Present.GetPresentById(DataConverter.CLng(str2)));
                    }
                    this.RptPresentList.DataSource = list2;
                    this.RptPresentList.DataBind();
                }
            }
            else if (this.m_IsPreview == 1)
            {
                this.RptPresentList.Visible = false;
                this.Note.Visible = false;
                this.LblPrice.Visible = false;
            }
        }

        protected bool PresentExist(string cartId, int presentId)
        {
            foreach (ShoppingCartInfo info in ShoppingCart.GetInfoByCart(cartId, true))
            {
                if (info.ProductId == presentId)
                {
                    return true;
                }
            }
            return false;
        }

        protected void RptPresentList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ShopConfig shopConfig = SiteConfig.ShopConfig;
            bool isPaymentShowProducdtThumb = true;
            int paymentThumbsWidth = 0;
            int paymentThumbsHeight = 0;
            bool isShowPaymentProductType = true;
            bool isShowPaymentSaleType = true;
            bool isShowPaymentMarkPrice = true;
            if (this.IsPreview == 0)
            {
                isPaymentShowProducdtThumb = shopConfig.IsPaymentShowProducdtThumb;
                isShowPaymentProductType = shopConfig.IsShowPaymentProductType;
                isShowPaymentSaleType = shopConfig.IsShowPaymentSaleType;
                isShowPaymentMarkPrice = shopConfig.IsShowPaymentMarkPrice;
                paymentThumbsWidth = shopConfig.PaymentThumbsWidth;
                paymentThumbsHeight = shopConfig.PaymentThumbsHeight;
            }
            else if (this.IsPreview == 1)
            {
                isPaymentShowProducdtThumb = shopConfig.IsPreviewShowProducdtThumb;
                isShowPaymentProductType = shopConfig.IsShowPreviewProductType;
                isShowPaymentSaleType = shopConfig.IsShowPreviewSaleType;
                isShowPaymentMarkPrice = shopConfig.IsShowPreviewMarkPrice;
                paymentThumbsWidth = shopConfig.PreviewThumbsWidth;
                paymentThumbsHeight = shopConfig.PreviewThumbsHeight;
            }
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                PresentInfo dataItem = (PresentInfo) e.Item.DataItem;
                ((Literal) e.Item.FindControl("LitChangePresentPriceMarket")).Text = dataItem.PriceMarket.ToString("0.00");
                ((Literal) e.Item.FindControl("LitChangePresentTruePrice")).Text = this.LblPrice.Text;
                ((Literal) e.Item.FindControl("LitChangePresentSubTotal")).Text = this.LblPrice.Text;
                Control control = e.Item.FindControl("changePresentImage");
                Control control2 = e.Item.FindControl("changePresentType");
                Control control3 = e.Item.FindControl("changeSaleType");
                Control control4 = e.Item.FindControl("changeMarkPrice");
                if (!isPaymentShowProducdtThumb)
                {
                    control.Visible = false;
                }
                else
                {
                    ExtendedImage image = (ExtendedImage) e.Item.FindControl("changePresentListImage");
                    if (!string.IsNullOrEmpty(dataItem.PresentThumb))
                    {
                        image.Src = dataItem.PresentThumb;
                    }
                    else
                    {
                        image.Src = SiteConfig.SiteInfo.VirtualPath + "Images/nopic.gif";
                    }
                    image.Width = paymentThumbsWidth;
                    image.Height = paymentThumbsHeight;
                }
                if (!isShowPaymentProductType)
                {
                    control2.Visible = false;
                }
                if (!isShowPaymentSaleType)
                {
                    control3.Visible = false;
                }
                if (!isShowPaymentMarkPrice)
                {
                    control4.Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                Control control5 = e.Item.FindControl("changePresentHeaderImage");
                Control control6 = e.Item.FindControl("changePresentHeaderProductType");
                Control control7 = e.Item.FindControl("changePresentHeaderSaleType");
                Control control8 = e.Item.FindControl("changePresentHeaderMarkPrice");
                if (!isPaymentShowProducdtThumb)
                {
                    control5.Visible = false;
                }
                if (!isShowPaymentProductType)
                {
                    control6.Visible = false;
                }
                if (!isShowPaymentSaleType)
                {
                    control7.Visible = false;
                }
                if (!isShowPaymentMarkPrice)
                {
                    control8.Visible = false;
                }
            }
        }

        protected void RptShoppingCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ShopConfig shopConfig = SiteConfig.ShopConfig;
            bool isPaymentShowProducdtThumb = true;
            int paymentThumbsWidth = 0;
            int paymentThumbsHeight = 0;
            bool isShowPaymentProductType = true;
            bool isShowPaymentSaleType = true;
            bool isShowPaymentMarkPrice = true;
            if (this.IsPreview == 0)
            {
                isPaymentShowProducdtThumb = shopConfig.IsPaymentShowProducdtThumb;
                isShowPaymentProductType = shopConfig.IsShowPaymentProductType;
                isShowPaymentSaleType = shopConfig.IsShowPaymentSaleType;
                isShowPaymentMarkPrice = shopConfig.IsShowPaymentMarkPrice;
                paymentThumbsWidth = shopConfig.PaymentThumbsWidth;
                paymentThumbsHeight = shopConfig.PaymentThumbsHeight;
            }
            else if (this.IsPreview == 1)
            {
                isPaymentShowProducdtThumb = shopConfig.IsPreviewShowProducdtThumb;
                isShowPaymentProductType = shopConfig.IsShowPreviewProductType;
                isShowPaymentSaleType = shopConfig.IsShowPreviewSaleType;
                isShowPaymentMarkPrice = shopConfig.IsShowPreviewMarkPrice;
                paymentThumbsWidth = shopConfig.PreviewThumbsWidth;
                paymentThumbsHeight = shopConfig.PreviewThumbsHeight;
            }
            if ((e.Item.ItemType != ListItemType.Item) && (e.Item.ItemType != ListItemType.AlternatingItem))
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    PresentProjectInfo presentProInfo = new PresentProjectInfo(true);
                    if (this.m_IsPreview != 3)
                    {
                        presentProInfo = PresentProject.GetPresentProjectByTotalMoney(this.total);
                        this.presentExpInfomation = this.ShowPresentExp(presentProInfo).ToString();
                    }
                    if (this.m_IsPreview == 1)
                    {
                        int presentId = DataConverter.CLng(this.PresentId);
                        PlaceHolder holder = (PlaceHolder) e.Item.FindControl("PlhPresentInfo");
                        holder.Visible = false;
                        if (((presentId > 0) && !presentProInfo.IsNull) && (presentProInfo.PresentContent.Contains("1") && (presentId > 0)))
                        {
                            holder.Visible = true;
                            AbstractItemInfo info7 = new ConcretePresentProject(presentId, presentProInfo);
                            info7.GetItemInfo();
                            Label label2 = (Label) e.Item.FindControl("LblProductName");
                            Label label3 = (Label) e.Item.FindControl("LblUnit");
                            Label label4 = (Label) e.Item.FindControl("LblPresentPriceMarket");
                            Label label5 = (Label) e.Item.FindControl("LblPresentPrice");
                            Label label6 = (Label) e.Item.FindControl("LblPresentPrice1");
                            label2.Text = info7.ProductName;
                            label3.Text = info7.Unit;
                            label4.Text = info7.PriceMarket.ToString("0.00");
                            label5.Text = info7.Price.ToString("0.00");
                            label6.Text = info7.Price.ToString("0.00");
                            this.weight += info7.TotalWeight;
                            this.total += info7.Price;
                        }
                        ((PlaceHolder) e.Item.FindControl("PlhMoneyInfo")).Visible = true;
                        Label label7 = (Label) e.Item.FindControl("LblDeliverCharge");
                        Label label8 = (Label) e.Item.FindControl("LblTaxRate");
                        Label label9 = (Label) e.Item.FindControl("LblIncludeTax");
                        Label label10 = (Label) e.Item.FindControl("LblCoupon");
                        Label label11 = (Label) e.Item.FindControl("LblTotalMoney");
                        Label label12 = (Label) e.Item.FindControl("LblTrueTotalMoney");
                        PackageInfo packageByGoodsWeight = Package.GetPackageByGoodsWeight(this.weight);
                        if (!packageByGoodsWeight.IsNull)
                        {
                            this.weight += packageByGoodsWeight.PackageWeight;
                        }
                        decimal num7 = DeliverCharge.GetDeliverCharge(this.m_DeliverType, this.weight, this.m_ZipCode, this.total, this.m_NeedInvoice);
                        DeliverTypeInfo deliverTypeById = EasyOne.Shop.DeliverType.GetDeliverTypeById(this.m_DeliverType);
                        label7.Text = num7.ToString("0.00");
                        label8.Text = deliverTypeById.TaxRate.ToString();
                        if ((deliverTypeById.IncludeTax == TaxRateType.IncludeTaxNoInvoiceFavourable) || (deliverTypeById.IncludeTax == TaxRateType.IncludeTaxNoInvoiceNoFavourable))
                        {
                            label9.Text = "是";
                        }
                        else
                        {
                            label9.Text = "否";
                        }
                        decimal num8 = this.total + num7;
                        if (this.m_CouponMoney > 0M)
                        {
                            label10.Visible = true;
                            decimal num9 = this.total - this.m_CouponMoney;
                            if (num9 < 0M)
                            {
                                num9 = 0M;
                            }
                            num8 = num9 + num7;
                            label10.Text = "使用优惠券，面值为：" + this.m_CouponMoney.ToString("0.00") + "元，商品实际价格为：" + num9.ToString("0.00") + "元 <br>";
                            label11.Text = num9.ToString("0.00") + "+" + num7.ToString("0.00") + "=" + num8.ToString("0.00") + "元";
                            label12.Text = num8.ToString("0.00");
                        }
                        else
                        {
                            label10.Visible = false;
                            label11.Text = this.total.ToString("0.00") + "+" + num7.ToString("0.00") + "=" + num8.ToString("0.00") + "元";
                            label12.Text = num8.ToString("0.00");
                        }
                        this.ViewState["TrueTotalMoney"] = num8;
                    }
                    else
                    {
                        ((PlaceHolder) e.Item.FindControl("PlhMoneyInfo")).Visible = false;
                    }
                    ExtendedImage image3 = (ExtendedImage) e.Item.FindControl("presentImage");
                    Control control9 = e.Item.FindControl("footerPresentImage");
                    Control control10 = e.Item.FindControl("footerTdThemeImage");
                    Control control11 = e.Item.FindControl("footerTdProductType");
                    Control control12 = e.Item.FindControl("footerTdSaleType");
                    Control control13 = e.Item.FindControl("footerTdMarkPrice");
                    Control control14 = e.Item.FindControl("footerTdThemeImage");
                    Control control15 = e.Item.FindControl("footerTdMoneyInfoSaleType");
                    Control control16 = e.Item.FindControl("footerTdMoneyInfoMarkPrice");
                    Control control17 = e.Item.FindControl("footerPresentType");
                    Control control18 = e.Item.FindControl("footerPresentSaleType");
                    Control control19 = e.Item.FindControl("footerPresentMarkPrice");
                    if (!isPaymentShowProducdtThumb)
                    {
                        control10.Visible = false;
                        control9.Visible = false;
                    }
                    else
                    {
                        PresentInfo presentById = Present.GetPresentById(DataConverter.CLng(this.PresentId));
                        if (!string.IsNullOrEmpty(presentById.PresentThumb))
                        {
                            image3.Src = presentById.PresentThumb;
                        }
                        else
                        {
                            image3.Src = SiteConfig.SiteInfo.VirtualPath + "Images/nopic.gif";
                        }
                        image3.Width = paymentThumbsWidth;
                        image3.Height = paymentThumbsHeight;
                    }
                    if (!isShowPaymentProductType)
                    {
                        control11.Visible = false;
                        control14.Visible = false;
                        control17.Visible = false;
                    }
                    if (!isShowPaymentSaleType)
                    {
                        control12.Visible = false;
                        control15.Visible = false;
                        control18.Visible = false;
                    }
                    if (!isShowPaymentMarkPrice)
                    {
                        control13.Visible = false;
                        control16.Visible = false;
                        control19.Visible = false;
                        return;
                    }
                }
                else if (e.Item.ItemType == ListItemType.Header)
                {
                    Control control20 = e.Item.FindControl("ProductImageTitle");
                    Control control21 = e.Item.FindControl("tdProductTypeTitle");
                    Control control22 = e.Item.FindControl("tdSaleTypeTitle");
                    Control control23 = e.Item.FindControl("tdMarkPriceTitle");
                    if (!isPaymentShowProducdtThumb)
                    {
                        control20.Visible = false;
                    }
                    if (!isShowPaymentProductType)
                    {
                        control21.Visible = false;
                    }
                    if (!isShowPaymentSaleType)
                    {
                        control22.Visible = false;
                    }
                    if (!isShowPaymentMarkPrice)
                    {
                        control23.Visible = false;
                    }
                }
                return;
            }
            int productNum = 0;
            string str = "";
            string str2 = "";
            decimal subTotal = 0M;
            ShoppingCartInfo dataItem = (ShoppingCartInfo) e.Item.DataItem;
            if (dataItem == null)
            {
                return;
            }
            productNum = dataItem.Quantity;
            bool haveWholesalePurview = Convert.ToBoolean(this.ViewState["HaveWholesalePurview"]);
            str2 = ShoppingCart.GetSaleType(dataItem.ProductInfomation, productNum, haveWholesalePurview);
            str = ShoppingCart.GetProductType(dataItem.ProductInfomation, productNum, haveWholesalePurview);
            AbstractItemInfo info2 = new ConcreteProductInfo(productNum, dataItem.Property, dataItem.ProductInfomation, this.m_UserInfo, false, false, haveWholesalePurview);
            info2.GetItemInfo();
            subTotal = info2.SubTotal;
            this.total += subTotal;
            this.totalExp += dataItem.ProductInfomation.PresentExp * productNum;
            this.totalMoney += dataItem.ProductInfomation.PresentMoney * productNum;
            this.totalPoint += dataItem.ProductInfomation.PresentPoint * productNum;
            this.weight += info2.TotalWeight;
            if (!string.IsNullOrEmpty(dataItem.Property))
            {
                ((Literal) e.Item.FindControl("LitProperty")).Text = "（" + info2.Property + "）";
            }
            InsideStaticLabel label = new InsideStaticLabel();
            string str3 = "<a href='";
            str3 = (str3 + label.GetInfoPath(info2.ProductId.ToString())) + "' Target='_blank'>" + info2.ProductName + "</a>";
            ((Literal) e.Item.FindControl("LitProductName")).Text = str3;
            ((Literal) e.Item.FindControl("LitProductUnit")).Text = info2.Unit;
            ((Literal) e.Item.FindControl("LitTruePrice")).Text = info2.Price.ToString("0.00");
            ((Literal) e.Item.FindControl("LitSubTotal")).Text = subTotal.ToString("0.00");
            ExtendedImage image = (ExtendedImage) e.Item.FindControl("extendedImage");
            ExtendedImage image2 = (ExtendedImage) e.Item.FindControl("extendedPresentImage");
            Control control = e.Item.FindControl("ProductImage");
            Control control2 = e.Item.FindControl("presentImage");
            if (!isPaymentShowProducdtThumb)
            {
                image.Visible = false;
                control.Visible = false;
                control2.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(dataItem.ProductInfomation.ProductThumb))
                {
                    image.Src = dataItem.ProductInfomation.ProductThumb;
                }
                else
                {
                    image.Src = SiteConfig.SiteInfo.VirtualPath + "Images/nopic.gif";
                }
                image.ImageHeight = paymentThumbsHeight;
                image.ImageWidth = paymentThumbsWidth;
                if (dataItem.ProductInfomation.PresentId > 0)
                {
                    PresentInfo info3 = Present.GetPresentById(dataItem.ProductInfomation.PresentId);
                    if (!string.IsNullOrEmpty(info3.PresentThumb))
                    {
                        image2.Src = info3.PresentThumb;
                    }
                    else
                    {
                        image2.Src = SiteConfig.SiteInfo.VirtualPath + "Images/nopic.gif";
                    }
                    image2.ImageHeight = paymentThumbsHeight;
                    image2.ImageWidth = paymentThumbsWidth;
                }
            }
            if (!isShowPaymentProductType)
            {
                e.Item.FindControl("tdProductType").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitProductType")).Text = str;
            }
            if (!isShowPaymentSaleType)
            {
                e.Item.FindControl("tdSaleType").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitSaleType")).Text = str2;
            }
            if (!isShowPaymentMarkPrice)
            {
                e.Item.FindControl("tdMarkPrice").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitPriceMarket")).Text = info2.PriceMarket.ToString("0.00");
            }
            int num5 = Order.CountBuyNum(PEContext.Current.User.UserName, dataItem.ProductId);
            ProductInfo productById = Product.GetProductById(dataItem.ProductId);
            if ((productById.LimitNum > 0) && ((dataItem.Quantity + num5) > productById.LimitNum))
            {
                BaseUserControl.WriteErrMsg(string.Concat(new object[] { "您订购了", num5, productById.Unit, productById.ProductName, "，曾经购买了", num5, productById.Unit, "，而此商品每人限购数量为", productById.LimitNum, productById.Unit, "，请重新调整您的购物车！" }), "ShoppingCart.aspx");
            }
            if ((dataItem.ProductInfomation.SalePromotionType <= 0) || (productNum < dataItem.ProductInfomation.MinNumber))
            {
                return;
            }
            e.Item.FindControl("PresentInfomation").Visible = true;
            string str4 = "";
            string str5 = "";
            string str6 = "";
            AbstractItemInfo info5 = new ConcreteSalePromotionType(productNum, dataItem.ProductInfomation, false, null);
            info5.GetItemInfo();
            switch (dataItem.ProductInfomation.SalePromotionType)
            {
                case 1:
                case 3:
                    str5 = "<font color=red>（赠品）</font>";
                    str4 = "赠送礼品";
                    str6 = "赠送";
                    goto Label_06A1;

                case 2:
                case 4:
                    if (info5.Price <= 0M)
                    {
                        str5 = "<font color=red>（赠送赠品）</font>";
                        str6 = "赠送";
                        break;
                    }
                    str5 = "<font color=red>（换购赠品）</font>";
                    str6 = "换购";
                    break;

                default:
                    goto Label_06A1;
            }
            str4 = "促销礼品";
        Label_06A1:
            if (this.PresentExist(this.m_CartId, info5.Id))
            {
                ((HiddenField) e.Item.FindControl("HdnPresentId")).Value = info5.Id.ToString();
                ExtendedLiteral literal = (ExtendedLiteral) e.Item.FindControl("LitPresentName");
                literal.Text = info5.ProductName;
                literal.EndTag = str5;
                ((Literal) e.Item.FindControl("LitPresentUnit")).Text = info5.Unit;
                ((Literal) e.Item.FindControl("LitPresentNum")).Text = info5.Amount.ToString();
                ((Literal) e.Item.FindControl("LitPresentTruePrice")).Text = info5.Price.ToString("0.00");
                ((Literal) e.Item.FindControl("LitPresentSubtotal")).Text = info5.SubTotal.ToString("0.00");
                this.total += info5.SubTotal;
                this.weight += info5.TotalWeight;
            }
            if (!isShowPaymentProductType)
            {
                e.Item.FindControl("tdPresentType").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitPresentType")).Text = str4;
            }
            if (!isShowPaymentSaleType)
            {
                e.Item.FindControl("tdPresentSaleType").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitPresentSaleType")).Text = str6;
            }
            if (!isShowPaymentMarkPrice)
            {
                e.Item.FindControl("tdPresentMarkPrice").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitPresentPriceOriginal")).Text = info5.PriceMarket.ToString("0.00");
            }
        }

        private StringBuilder ShowPresentExp(PresentProjectInfo presentProInfo)
        {
            StringBuilder builder = new StringBuilder();
            if (!presentProInfo.IsNull && (((presentProInfo.PresentContent.Contains("2") || presentProInfo.PresentContent.Contains("3")) || (presentProInfo.PresentContent.Contains("4") || (this.totalExp > 0))) || ((this.totalMoney > 0M) || ((this.totalPoint > 0) && !string.IsNullOrEmpty(PEContext.Current.User.UserName)))))
            {
                builder.Append("<tr><td colspan='8' align='left'><b>另外，你还可以得到 ");
                if (presentProInfo.PresentContent.Contains("2") || (this.totalMoney > 0M))
                {
                    builder.Append("<font color='red'>");
                    builder.Append((presentProInfo.Cash + this.totalMoney).ToString("0.00"));
                    builder.Append("</font> 元现金券");
                }
                if (presentProInfo.PresentContent.Contains("3") || (this.totalExp > 0))
                {
                    if (presentProInfo.PresentContent.Contains("2") || (this.totalMoney > 0M))
                    {
                        builder.Append("和");
                    }
                    builder.Append("<font color='red'>");
                    builder.Append((int) (presentProInfo.PresentExp + this.totalExp));
                    builder.Append("</font> 点积分");
                }
                if (presentProInfo.PresentContent.Contains("4") || (this.totalPoint > 0))
                {
                    if ((presentProInfo.PresentContent.Contains("2") || presentProInfo.PresentContent.Contains("3")) || ((this.totalExp > 0) || (this.totalMoney > 0M)))
                    {
                        builder.Append("和");
                    }
                    builder.Append("<font color='red'>");
                    builder.Append((int) (presentProInfo.PresentPoint + this.totalPoint));
                    builder.Append("</font> " + SiteConfig.UserConfig.PointUnit + SiteConfig.UserConfig.PointName);
                }
                builder.Append("</b></td></tr>");
            }
            return builder;
        }

        public string CartId
        {
            get
            {
                return this.m_CartId;
            }
            set
            {
                this.m_CartId = value;
            }
        }

        public decimal CouponMoney
        {
            get
            {
                return this.m_CouponMoney;
            }
            set
            {
                this.m_CouponMoney = value;
            }
        }

        public int DeliverType
        {
            get
            {
                return this.m_DeliverType;
            }
            set
            {
                this.m_DeliverType = value;
            }
        }

        public int IsPreview
        {
            get
            {
                return this.m_IsPreview;
            }
            set
            {
                this.m_IsPreview = value;
            }
        }

        public bool NeedInvoice
        {
            get
            {
                return this.m_NeedInvoice;
            }
            set
            {
                this.m_NeedInvoice = value;
            }
        }

        public int PaymentType
        {
            get
            {
                return this.m_PaymentType;
            }
            set
            {
                this.m_PaymentType = value;
            }
        }

        public string PresentId
        {
            get
            {
                if (this.ViewState["PresentId"] != null)
                {
                    return Convert.ToString(this.ViewState["PresentId"]);
                }
                return "";
            }
            set
            {
                this.ViewState["PresentId"] = value;
            }
        }

        public decimal TrueTotalMoney
        {
            get
            {
                return DataConverter.CDecimal(this.ViewState["TrueTotalMoney"]);
            }
        }

        public bool UseCoupon
        {
            get
            {
                return this.m_UseCoupon;
            }
            set
            {
                this.m_UseCoupon = value;
            }
        }

        public string ZipCode
        {
            get
            {
                return this.m_ZipCode;
            }
            set
            {
                this.m_ZipCode = value;
            }
        }
    }
}


namespace EasyOne.WebSite.Shop
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.Shop;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ShoppingCartUI : DynamicPage
    {
        protected Button BtnClearCart;
        protected Button BtnModifyCart;
        protected Button BtnPayment;
        protected Button BtnShopping;
        protected string cartId;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Label LblBalance;
        protected Label LblDiscount_Member;
        protected Label LblGroupName;
        protected Label LblUserExp;
        protected Label LblUserName;
        protected Label LblUserPoint;
        protected HyperLink LnkLogin;
        private string m_Action;
        private int m_ProductCount;
        private string m_UserName = PEContext.Current.User.UserName;
        protected Panel PanEmptyText;
        protected StringBuilder productNumList;
        protected Repeater RptShoppingCart;
        protected HtmlTableRow ShowTips_Login;
        protected decimal total;

        protected void AddToCart()
        {
            ShopConfig shopConfig = SiteConfig.ShopConfig;
            if ((shopConfig.OrderProductNumber != 0) && (shopConfig.OrderProductNumber <= ShoppingCart.GetInfoByCart(this.cartId, false).Count))
            {
                DynamicPage.WriteErrMsg("<li>超出系统所设置的购物车商品种类数量：" + shopConfig.OrderProductNumber + "</li>", "ShoppingCart.aspx");
            }
            ShoppingCartInfo shoppingcartinfo = new ShoppingCartInfo();
            int productId = 0;
            string tableName = string.Empty;
            CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(BasePage.RequestInt32("ID"));
            if (commonModelInfoById.IsNull)
            {
                DynamicPage.WriteErrMsg("<li>找不到指定的商品</li>");
            }
            else
            {
                productId = commonModelInfoById.ItemId;
                tableName = commonModelInfoById.TableName;
            }
            int num3 = Order.CountBuyNum(PEContext.Current.User.UserName, productId);
            ProductInfo productById = Product.GetProductById(productId);
            int minimum = DataConverter.CLng(this.Page.Request.QueryString["Num"]);
            if ((productById.Minimum > minimum) && (productById.Minimum > 0))
            {
                minimum = productById.Minimum;
            }
            else if (minimum == 0)
            {
                minimum = 1;
            }
            if ((productById.LimitNum > 0) && ((num3 + minimum) > productById.LimitNum))
            {
                DynamicPage.WriteErrMsg(string.Concat(new object[] { "您订购了", num3, productById.Unit, productById.ProductName, "，曾经购买了", num3, productById.Unit, "，而此商品每人限购数量为", productById.LimitNum, productById.Unit, "，请重新调整您的购物车！" }), "ShoppingCart.aspx");
            }
            string property = DataSecurity.FilterBadChar(BasePage.RequestString("Property"));
            if (!this.ProductExist(this.cartId, productId, tableName, property))
            {
                if (Product.IsEnableSale(productId, tableName, property, this.cartId))
                {
                    shoppingcartinfo.Quantity = minimum;
                    shoppingcartinfo.ProductId = productId;
                    shoppingcartinfo.TableName = tableName;
                    shoppingcartinfo.Property = property;
                    shoppingcartinfo.UserName = this.m_UserName;
                    shoppingcartinfo.UpdateTime = DateTime.Now;
                    shoppingcartinfo.IsPresent = false;
                    shoppingcartinfo.CartId = this.cartId;
                    ShoppingCart.Add(shoppingcartinfo);
                }
                else
                {
                    DynamicPage.WriteErrMsg(Product.ErrMsgOfEnableSale);
                }
            }
        }

        private void BatchAddToCart()
        {
            string str = BasePage.RequestString("IDList");
            if (string.IsNullOrEmpty(str))
            {
                DynamicPage.WriteErrMsg("对不起，传递的参数不正确，请从正确的路径访问！");
            }
            foreach (string str2 in str.Split(new char[] { ',' }))
            {
                string[] strArray2 = str2.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                string property = string.Empty;
                if (strArray2.Length == 3)
                {
                    property = strArray2[2];
                }
                int generalId = DataConverter.CLng(strArray2[0]);
                int num2 = DataConverter.CLng(strArray2[1], 1);
                ShoppingCartInfo shoppingcartinfo = new ShoppingCartInfo();
                int productId = 0;
                string tableName = string.Empty;
                CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(generalId);
                if (!commonModelInfoById.IsNull)
                {
                    productId = commonModelInfoById.ItemId;
                    tableName = commonModelInfoById.TableName;
                }
                if (!this.ProductExist(this.cartId, productId, tableName, property) && Product.IsEnableSale(productId, tableName, property, this.cartId))
                {
                    shoppingcartinfo.Quantity = num2;
                    shoppingcartinfo.ProductId = productId;
                    shoppingcartinfo.TableName = tableName;
                    shoppingcartinfo.Property = property;
                    shoppingcartinfo.UserName = this.m_UserName;
                    shoppingcartinfo.UpdateTime = DateTime.Now;
                    shoppingcartinfo.IsPresent = false;
                    shoppingcartinfo.CartId = this.cartId;
                    ShoppingCart.Add(shoppingcartinfo);
                }
            }
        }

        protected void BtnClearCart_Click(object sender, EventArgs e)
        {
            ShoppingCart.Delete(this.cartId);
            this.RptShoppingCart_DataBind();
        }

        protected void BtnModifyCart_Click(object sender, EventArgs e)
        {
            this.ModifyCart();
        }

        protected void BtnPayment_Click(object sender, EventArgs e)
        {
            this.ModifyCart();
            if (this.m_ProductCount == 0)
            {
                DynamicPage.WriteErrMsg("购物车为空，不能去收银台！");
            }
            if (string.IsNullOrEmpty(PEContext.Current.User.UserName))
            {
                base.Response.Redirect("FastRegister.aspx");
            }
            else
            {
                base.Response.Redirect("Payment.aspx");
            }
        }

        protected void BtnShopping_Click(object sender, EventArgs e)
        {
            string continueBuy = SiteConfig.ShopConfig.ContinueBuy;
            if (string.IsNullOrEmpty(continueBuy))
            {
                continueBuy = "~/Category.aspx?id=4";
            }
            BasePage.ResponseRedirect(continueBuy);
        }

        private string GetParameterList(string checkboxId, string productIdControlId, string numberControlId, string tableNameControlId, string propertyControlId)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            StringBuilder builder = new StringBuilder("");
            IList<ShoppingCartInfo> list = this.ViewState["CartList"] as IList<ShoppingCartInfo>;
            StringBuilder sb = new StringBuilder();
            foreach (ShoppingCartInfo info in list)
            {
                if (!info.IsPresent)
                {
                    StringHelper.AppendString(sb, info.ProductId + "|" + info.TableName);
                }
            }
            for (int i = this.RptShoppingCart.Items.Count - 1; i >= 0; i--)
            {
                if ((this.RptShoppingCart.Items[i].ItemType == ListItemType.Item) || (this.RptShoppingCart.Items[i].ItemType == ListItemType.AlternatingItem))
                {
                    CheckBox box = (CheckBox) this.RptShoppingCart.Items[i].FindControl(checkboxId);
                    if ((box != null) && box.Checked)
                    {
                        string str4 = "";
                        str = ((HiddenField) this.RptShoppingCart.Items[i].FindControl(productIdControlId)).Value;
                        int amount = DataConverter.CLng(((TextBox) this.RptShoppingCart.Items[i].FindControl(numberControlId)).Text, 1);
                        if (amount <= 0)
                        {
                            amount = 1;
                        }
                        str2 = ((HiddenField) this.RptShoppingCart.Items[i].FindControl(tableNameControlId)).Value;
                        str3 = ((HiddenField) this.RptShoppingCart.Items[i].FindControl(propertyControlId)).Value;
                        if (list != null)
                        {
                            str4 = Product.CheckStocks(list[i].ProductInfomation, list[i].Property, amount, sb);
                        }
                        if (string.IsNullOrEmpty(str4))
                        {
                            if (builder.Length != 0)
                            {
                                builder.Append("$$$");
                            }
                            builder.Append(str);
                            builder.Append(",");
                            builder.Append(str2);
                            builder.Append(",");
                            builder.Append(amount.ToString());
                            builder.Append(",");
                            builder.Append(str3);
                        }
                        else
                        {
                            DynamicPage.WriteErrMsg(str4);
                            break;
                        }
                    }
                }
            }
            return builder.ToString();
        }

        private string GetPresentParameterList(string checkboxId, string idControl, string numberControlId)
        {
            string str = "";
            string str2 = "";
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < this.RptShoppingCart.Items.Count; i++)
            {
                CheckBox box = (CheckBox) this.RptShoppingCart.Items[i].FindControl(checkboxId);
                if ((box != null) && box.Checked)
                {
                    str = ((HiddenField) this.RptShoppingCart.Items[i].FindControl(idControl)).Value.ToString();
                    str2 = ((Literal) this.RptShoppingCart.Items[i].FindControl(numberControlId)).Text.ToString();
                    if (builder.Length != 0)
                    {
                        builder.Append("$$$");
                    }
                    builder.Append(str);
                    builder.Append(",");
                    builder.Append(str2);
                }
            }
            return builder.ToString();
        }

        private void ModifyCart()
        {
            ShoppingCartInfo shoppingcartinfo = new ShoppingCartInfo();
            shoppingcartinfo.UserName = this.m_UserName;
            shoppingcartinfo.UpdateTime = DateTime.Now;
            shoppingcartinfo.CartId = this.cartId;
            string[] strArray = this.GetParameterList("ChkProductId", "HdfProductId", "TxtProductAmount", "HdfTableName", "HdfProperty").Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
            this.m_ProductCount = strArray.Length;
            string[] strArray2 = this.GetPresentParameterList("ChkPresentId", "HdnPresentId", "LitPresentNum").Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
            ShoppingCart.Delete(this.cartId);
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray3 = strArray[i].Split(new char[] { ',' });
                shoppingcartinfo.IsPresent = false;
                shoppingcartinfo.ProductId = DataConverter.CLng(strArray3[0]);
                shoppingcartinfo.TableName = DataSecurity.FilterBadChar(strArray3[1]);
                shoppingcartinfo.Quantity = DataConverter.CLng(strArray3[2], 1);
                shoppingcartinfo.Property = DataSecurity.FilterBadChar(strArray3[3]);
                ShoppingCart.Add(shoppingcartinfo);
            }
            if (strArray2.Length != 0)
            {
                for (int j = 0; j < strArray2.Length; j++)
                {
                    string[] strArray4 = strArray2[j].Split(new char[] { ',' });
                    shoppingcartinfo.IsPresent = true;
                    shoppingcartinfo.ProductId = DataConverter.CLng(strArray4[0]);
                    shoppingcartinfo.Quantity = DataConverter.CLng(strArray4[1]);
                    ShoppingCart.Add(shoppingcartinfo);
                }
            }
            this.RptShoppingCart_DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string applicationName = PayOnline.GetApplicationName();
            if (base.Request.Cookies["Cart" + applicationName] == null)
            {
                this.cartId = Guid.NewGuid().ToString();
                base.Response.Cookies["Cart" + applicationName]["CartID"] = this.cartId;
                base.Response.Cookies["Cart" + applicationName].Expires = DateTime.Now.AddYears(1);
            }
            else
            {
                this.cartId = base.Request.Cookies["Cart" + applicationName]["CartID"];
            }
            this.m_Action = BasePage.RequestStringToLower("Action");
            if (!base.IsPostBack)
            {
                if (string.IsNullOrEmpty(this.m_UserName))
                {
                    this.LblUserName.Text = "";
                    this.LblGroupName.Text = "游客";
                    this.LblDiscount_Member.Text = "100";
                    this.LblBalance.Text = "0";
                    this.LblUserPoint.Text = "0";
                    this.LblUserExp.Text = "0";
                    this.ShowTips_Login.Visible = true;
                    this.LnkLogin.NavigateUrl = "../User/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode("../Shop/ShoppingCart.aspx");
                }
                else
                {
                    UserInfo userInfo = PEContext.Current.User.UserInfo;
                    this.LblUserName.Text = this.m_UserName;
                    this.LblGroupName.Text = PEContext.Current.User.UserInfo.GroupName;
                    this.LblDiscount_Member.Text = PEContext.Current.User.PurviewInfo.Discount.ToString();
                    this.LblBalance.Text = userInfo.Balance.ToString("0.00");
                    this.LblUserPoint.Text = userInfo.UserPoint.ToString();
                    this.LblUserExp.Text = userInfo.UserExp.ToString();
                }
                if (this.m_Action == "addtocart")
                {
                    this.AddToCart();
                }
                if ((this.m_Action == "wholesale") || (this.m_Action == "batchadd"))
                {
                    this.BatchAddToCart();
                }
                this.RptShoppingCart_DataBind();
            }
        }

        protected bool PresentExist(string shoppingCartId, int productId)
        {
            foreach (ShoppingCartInfo info in ShoppingCart.GetInfoByCart(shoppingCartId, true))
            {
                if ((info.ProductId == productId) && info.IsPresent)
                {
                    return true;
                }
            }
            return false;
        }

        protected bool ProductExist(string shoppingCartId, int productId, string tableName, string property)
        {
            foreach (ShoppingCartInfo info in ShoppingCart.GetInfoByCart(shoppingCartId, false))
            {
                if (((info.ProductId == productId) && (info.TableName == tableName)) && (info.Property == property))
                {
                    return true;
                }
            }
            return false;
        }

        private void RptShoppingCart_DataBind()
        {
            IList<ShoppingCartInfo> list = ShoppingCart.GetList(0, 0, 4, this.cartId);
            if (list.Count == 0)
            {
                this.RptShoppingCart.Visible = false;
                this.PanEmptyText.Visible = true;
                this.BtnClearCart.Visible = false;
                this.BtnModifyCart.Visible = false;
                this.BtnPayment.Visible = false;
            }
            this.RptShoppingCart.DataSource = list;
            this.RptShoppingCart.DataBind();
            this.ViewState["CartList"] = list;
        }

        protected void RptShoppingCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ShopConfig shopConfig = SiteConfig.ShopConfig;
            if ((e.Item.ItemType != ListItemType.Item) && (e.Item.ItemType != ListItemType.AlternatingItem))
            {
                goto Label_07BA;
            }
            int productNum = 0;
            string str = "";
            string str2 = "";
            decimal subTotal = 0M;
            ((CheckBox) e.Item.FindControl("ChkProductId")).Checked = true;
            ShoppingCartInfo dataItem = e.Item.DataItem as ShoppingCartInfo;
            if (dataItem == null)
            {
                goto Label_07BA;
            }
            productNum = dataItem.Quantity;
            ((TextBox) e.Item.FindControl("TxtProductAmount")).Text = productNum.ToString();
            bool haveWholesalePurview = false;
            if (PEContext.Current.User.PurviewInfo != null)
            {
                haveWholesalePurview = PEContext.Current.User.PurviewInfo.Enablepm;
            }
            str2 = ShoppingCart.GetSaleType(dataItem.ProductInfomation, productNum, haveWholesalePurview);
            str = ShoppingCart.GetProductType(dataItem.ProductInfomation, productNum, haveWholesalePurview);
            AbstractItemInfo info2 = new ConcreteProductInfo(productNum, dataItem.Property, dataItem.ProductInfomation, PEContext.Current.User.UserInfo, false, false, haveWholesalePurview);
            info2.GetItemInfo();
            subTotal = info2.SubTotal;
            this.total += subTotal;
            if (!string.IsNullOrEmpty(dataItem.Property))
            {
                ((Literal) e.Item.FindControl("LitProperty")).Text = "（" + info2.Property + "）";
            }
            ProductInfo productById = Product.GetProductById(dataItem.ProductId);
            if (productById.Minimum > 0)
            {
                ((Literal) e.Item.FindControl("LblMark")).Text = "(<font color=\"red\">最低购买量" + productById.Minimum.ToString() + "</font>)";
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
            if (!shopConfig.IsGwcShowProducdtThumb)
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
                image.ImageHeight = shopConfig.GwcThumbsHeight;
                image.ImageWidth = shopConfig.GwcThumbsWidth;
                if (dataItem.ProductInfomation.PresentId > 0)
                {
                    PresentInfo presentById = Present.GetPresentById(dataItem.ProductInfomation.PresentId);
                    if (!string.IsNullOrEmpty(presentById.PresentThumb))
                    {
                        image2.Src = presentById.PresentThumb;
                    }
                    else
                    {
                        image2.Src = SiteConfig.SiteInfo.VirtualPath + "Images/nopic.gif";
                    }
                    image2.ImageHeight = shopConfig.GwcThumbsHeight;
                    image2.ImageWidth = shopConfig.GwcThumbsWidth;
                }
            }
            if (!shopConfig.IsShowGwcProductType)
            {
                e.Item.FindControl("tdProductType").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitProductType")).Text = str;
            }
            if (!shopConfig.IsShowGwcSaleType)
            {
                e.Item.FindControl("tdSaleType").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitSaleType")).Text = str2;
            }
            if (!shopConfig.IsShowGwcMarkPrice)
            {
                e.Item.FindControl("tdMarkPrice").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitPriceMarket")).Text = info2.PriceMarket.ToString("0.00");
            }
            if (((dataItem.ProductInfomation.SalePromotionType <= 0) || (productNum < dataItem.ProductInfomation.MinNumber)) || dataItem.IsPresent)
            {
                goto Label_07BA;
            }
            e.Item.FindControl("PresentInfomation").Visible = true;
            string str4 = "";
            string str5 = "";
            string str6 = "";
            int productId = 0;
            AbstractItemInfo info5 = new ConcreteSalePromotionType(productNum, dataItem.ProductInfomation, false, null);
            info5.GetItemInfo();
            switch (dataItem.ProductInfomation.SalePromotionType)
            {
                case 1:
                case 3:
                    str5 = "<font color=red>（赠品）</font>";
                    str4 = "赠送礼品";
                    str6 = "赠送";
                    productId = info5.Id;
                    goto Label_05B2;

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
                    goto Label_05B2;
            }
            str4 = "促销礼品";
            productId = info5.Id;
        Label_05B2:
            ((HiddenField) e.Item.FindControl("HdnPresentId")).Value = productId.ToString();
            ((Literal) e.Item.FindControl("LitPresentName")).Text = info5.ProductName + str5;
            ((Literal) e.Item.FindControl("LitPresentUnit")).Text = info5.Unit;
            ((Literal) e.Item.FindControl("LitPresentNum")).Text = info5.Amount.ToString();
            ((Literal) e.Item.FindControl("LitPresentTruePrice")).Text = info5.Price.ToString("0.00");
            ((Literal) e.Item.FindControl("LitPresentSubtotal")).Text = info5.SubTotal.ToString("0.00");
            if (this.PresentExist(this.cartId, productId))
            {
                ((CheckBox) e.Item.FindControl("ChkPresentId")).Checked = true;
                this.total += info5.SubTotal;
            }
            if (!shopConfig.IsShowGwcProductType)
            {
                e.Item.FindControl("tdPresentType").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitPresentType")).Text = str4;
            }
            if (!shopConfig.IsShowGwcSaleType)
            {
                e.Item.FindControl("tdPresentSaleType").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitPresentSaleType")).Text = str6;
            }
            if (!shopConfig.IsShowGwcMarkPrice)
            {
                e.Item.FindControl("tdPresentMarkPrice").Visible = false;
            }
            else
            {
                ((Literal) e.Item.FindControl("LitPresentPriceMarket")).Text = info5.PriceMarket.ToString("0.00");
            }
        Label_07BA:
            if (e.Item.ItemType == ListItemType.Header)
            {
                Control control9 = e.Item.FindControl("ProductImageTitle");
                Control control10 = e.Item.FindControl("tdProductTypeTitle");
                Control control11 = e.Item.FindControl("tdSaleTypeTitle");
                Control control12 = e.Item.FindControl("tdMarkPriceTitle");
                if (!shopConfig.IsGwcShowProducdtThumb)
                {
                    control9.Visible = false;
                }
                if (!shopConfig.IsShowGwcProductType)
                {
                    control10.Visible = false;
                }
                if (!shopConfig.IsShowGwcSaleType)
                {
                    control11.Visible = false;
                }
                if (!shopConfig.IsShowGwcMarkPrice)
                {
                    control12.Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Control control13 = e.Item.FindControl("footerTdThemeImage");
                Control control14 = e.Item.FindControl("footerTdProductType");
                Control control15 = e.Item.FindControl("footerTdSaleType");
                Control control16 = e.Item.FindControl("footerTdMarkPrice");
                if (!shopConfig.IsGwcShowProducdtThumb)
                {
                    control13.Visible = false;
                }
                if (!shopConfig.IsShowGwcProductType)
                {
                    control14.Visible = false;
                }
                if (!shopConfig.IsShowGwcSaleType)
                {
                    control15.Visible = false;
                }
                if (!shopConfig.IsShowGwcMarkPrice)
                {
                    control16.Visible = false;
                }
            }
        }
    }
}


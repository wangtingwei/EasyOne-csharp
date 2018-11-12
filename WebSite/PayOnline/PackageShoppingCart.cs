namespace EasyOne.WebSite.Shop
{
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;

    public class PackageShoppingCart : AdminPage
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            string applicationName = PayOnline.GetApplicationName();
            int num = 0;
            decimal num2 = 0M;
            if (base.Request.Cookies["Cart" + applicationName] != null)
            {
                string keyword = base.Request.Cookies["Cart" + applicationName]["CartID"];
                IList<ShoppingCartInfo> list = ShoppingCart.GetList(0, 0, 4, keyword);
                bool haveWholesalePurview = false;
                if (PEContext.Current.User.PurviewInfo != null)
                {
                    haveWholesalePurview = PEContext.Current.User.PurviewInfo.Enablepm;
                }
                foreach (ShoppingCartInfo info in list)
                {
                    int quantity = info.Quantity;
                    AbstractItemInfo info2 = new ConcreteProductInfo(quantity, info.Property, info.ProductInfomation, PEContext.Current.User.UserInfo, false, false, haveWholesalePurview);
                    info2.GetItemInfo();
                    num2 += info2.SubTotal;
                    num += quantity;
                }
            }
            string str3 = string.Concat(new object[] { "商品数量：", num, "&nbsp;总额：￥", num2.ToString("N2") });
            base.Response.Write("document.write('" + str3 + "');");
            base.Response.End();
        }
    }
}


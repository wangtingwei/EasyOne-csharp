namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using EasyOne.DalFactory;

    public sealed class ShoppingCart
    {
        private static readonly IShoppingCart dal = DataAccess.CreateShoppingCart();

        private ShoppingCart()
        {
        }

        public static bool Add(ShoppingCartInfo shoppingcartinfo)
        {
            return dal.Add(shoppingcartinfo);
        }

        public static bool Delete(int datePartType)
        {
            DateTime datePart = DateTime.Now.AddYears(-5);
            switch (datePartType)
            {
                case 0:
                    datePart = DateTime.Now.AddDays(-1.0);
                    break;

                case 1:
                    datePart = DateTime.Now.AddDays(-7.0);
                    break;

                case 2:
                    datePart = DateTime.Now.AddMonths(-1);
                    break;

                case 3:
                    datePart = DateTime.Now.AddMonths(-3);
                    break;
            }
            return dal.Delete(datePart);
        }

        public static bool Delete(string cartId)
        {
            return dal.Delete(DataSecurity.FilterBadChar(cartId));
        }

        public static string GetdiscountMessage(ProductInfo productInfo, int productNum)
        {
            string str = "";
            if (!productInfo.EnableWholesale || (productNum < productInfo.PriceInfo.NumberWholesale1))
            {
                ProductType productType = productInfo.ProductType;
                if ((productType != ProductType.Normal) && (productType != ProductType.Promotion))
                {
                    return str;
                }
            }
            return "---";
        }

        public static IList<ShoppingCartInfo> GetInfoByCart(string cartId, bool isPresent)
        {
            return dal.GetInfoByCart(DataSecurity.FilterBadChar(cartId), isPresent);
        }

        public static string GetInformMessage(string informMessage, string cartId)
        {
            if (!string.IsNullOrEmpty(informMessage))
            {
                IList<ShoppingCartInfo> list = GetList(0, 0x7fffffff, 4, cartId);
                if (list.Count == 0)
                {
                    return informMessage;
                }
                informMessage = informMessage.Replace("{$UserName}", list[0].UserName);
                informMessage = informMessage.Replace("{$UpdateTime}", list[0].UpdateTime.ToString("yyyy-MM-hh HH:mm:ss"));
                if (informMessage.IndexOf("{$CartInfo}", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    return informMessage;
                }
                StringBuilder sb = new StringBuilder();
                foreach (ShoppingCartInfo info in list)
                {
                    decimal num = info.ProductInfomation.PriceInfo.Price * info.Quantity;
                    StringHelper.AppendString(sb, string.Concat(new object[] { info.Quantity, info.ProductInfomation.Unit, info.ProductInfomation.ProductName, "，价格：", num.ToString("0.00"), "元" }), "；");
                }
                informMessage = informMessage.Replace("{$CartInfo}", sb.ToString());
            }
            return informMessage;
        }

        public static IList<ShoppingCartInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static DataTable GetListOfDifferentCart(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            Product product = new Product();
            DataTable table = new DataTable();
            table.Columns.Add("CartId", typeof(string));
            table.Columns.Add("UserName", typeof(string));
            table.Columns.Add("UpdateTime", typeof(DateTime));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("TotalMoney", typeof(decimal));
            foreach (ShoppingCartInfo info in GetList(startRowIndexId, maxNumberRows, searchType, keyword))
            {
                DataRow row = table.NewRow();
                row["CartId"] = info.CartId;
                row["UserName"] = info.UserName;
                row["UpdateTime"] = info.UpdateTime;
                row["Quantity"] = info.Quantity;
                decimal num = 0M;
                foreach (ShoppingCartInfo info2 in GetInfoByCart(info.CartId, false))
                {
                    product.GetProductAllDataById(info2.ProductId, info2.TableName);
                    num += product.ProductInfoData.PriceInfo.Price * info2.Quantity;
                }
                row["TotalMoney"] = num;
                table.Rows.Add(row);
            }
            return table;
        }

        public static StringBuilder GetProductIdAndTableNameInCart(string cartId, bool isPresent)
        {
            IList<ShoppingCartInfo> infoByCart = GetInfoByCart(cartId, isPresent);
            StringBuilder sb = new StringBuilder();
            foreach (ShoppingCartInfo info in infoByCart)
            {
                StringHelper.AppendString(sb, info.ProductId + "|" + info.TableName);
            }
            return sb;
        }

        public static string GetProductType(ProductInfo productInfo, int productNum, bool haveWholesalePurview)
        {
            string str = "";
            if ((productInfo.EnableWholesale && (productNum >= productInfo.PriceInfo.NumberWholesale1)) && haveWholesalePurview)
            {
                return "批发";
            }
            switch (productInfo.ProductType)
            {
                case ProductType.Normal:
                    return "正常销售";

                case ((ProductType) 1):
                case ((ProductType) 2):
                    return str;

                case ProductType.Special:
                    return "特价商品";

                case ProductType.Promotion:
                    return "礼品";
            }
            return str;
        }

        public static string GetSaleType(ProductInfo productInfo, int productNum, bool haveWholesalePurview)
        {
            if ((productInfo.EnableWholesale && (productNum >= productInfo.PriceInfo.NumberWholesale1)) && haveWholesalePurview)
            {
                return "批发";
            }
            return "零售";
        }

        public static int GetTotalOfShoppingCart(int searchType, string keyword)
        {
            return dal.GetTotalOfShoppingCart();
        }

        public static void UpdateInformState(string cartId, int state)
        {
            dal.UpdateInformState(cartId, state);
        }

        public static void UpdateUserName(string cartId, string userName)
        {
            dal.UpdateUserName(cartId, userName);
        }
    }
}


namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public class ConcreteSalePromotionType : AbstractItemInfo
    {
        private ProductInfo m_ProductInfo;
        private int m_Quantity;
        private IList<ShoppingCartInfo> m_ShoppingCartPresentInfoList;

        public ConcreteSalePromotionType(int quantity, ProductInfo productInfo, bool needTaxRateCompute, IList<ShoppingCartInfo> shoppingCartPresentInfoList)
        {
            this.m_Quantity = quantity;
            this.m_ProductInfo = productInfo;
            this.m_ShoppingCartPresentInfoList = shoppingCartPresentInfoList;
            base.NeedTaxRateCompute = needTaxRateCompute;
        }

        public override void GetItemInfo()
        {
            int presentNumber;
            switch (this.m_ProductInfo.SalePromotionType)
            {
                case 1:
                case 3:
                    if ((this.m_ShoppingCartPresentInfoList != null) && !AbstractItemInfo.FoundInCart(this.m_ShoppingCartPresentInfoList, this.m_ProductInfo.ProductId))
                    {
                        base.IsNull = true;
                        return;
                    }
                    base.ProductName = this.m_ProductInfo.ProductName;
                    base.Unit = this.m_ProductInfo.Unit;
                    if ((this.m_ProductInfo.SalePromotionType == 1) && (this.m_ProductInfo.MinNumber != 0))
                    {
                        presentNumber = DataConverter.CLng(this.m_Quantity / this.m_ProductInfo.MinNumber) * this.m_ProductInfo.PresentNumber;
                    }
                    else
                    {
                        presentNumber = this.m_ProductInfo.PresentNumber;
                    }
                    base.Amount = presentNumber;
                    base.PriceMarket = this.m_ProductInfo.PriceMarket;
                    base.Price = 0M;
                    base.ServiceTerm = this.m_ProductInfo.ServiceTerm;
                    base.ServiceTermUnit = this.m_ProductInfo.ServiceTermUnit;
                    base.Remark = GetSalePromotionTypeRemark(this.m_ProductInfo);
                    base.SaleType = 3;
                    base.BeginDate = DateTime.Today;
                    base.PresentExp = 0;
                    base.PresentMoney = 0M;
                    base.PresentPoint = 0;
                    base.ProductKind = this.m_ProductInfo.ProductKind;
                    base.TotalWeight = this.m_ProductInfo.Weight * base.Amount;
                    base.SubTotal = 0M;
                    base.Id = this.m_ProductInfo.ProductId;
                    base.isPresent = true;
                    base.Weight = this.m_ProductInfo.Weight;
                    base.ProductCharacter = this.m_ProductInfo.ProductCharacter;
                    base.TableName = this.m_ProductInfo.TableName;
                    return;

                case 2:
                case 4:
                {
                    PresentInfo presentById = Present.GetPresentById(DataConverter.CLng(this.m_ProductInfo.PresentId));
                    if (presentById.IsNull)
                    {
                        base.IsNull = true;
                        return;
                    }
                    if ((this.m_ShoppingCartPresentInfoList != null) && !AbstractItemInfo.FoundInCart(this.m_ShoppingCartPresentInfoList, presentById.PresentId))
                    {
                        base.IsNull = true;
                        return;
                    }
                    base.ProductName = presentById.PresentName;
                    base.Unit = presentById.Unit;
                    if ((this.m_ProductInfo.SalePromotionType == 2) && (this.m_ProductInfo.MinNumber != 0))
                    {
                        presentNumber = DataConverter.CLng(this.m_Quantity / this.m_ProductInfo.MinNumber) * this.m_ProductInfo.PresentNumber;
                    }
                    else
                    {
                        presentNumber = this.m_ProductInfo.PresentNumber;
                    }
                    base.Amount = presentNumber;
                    base.PriceMarket = presentById.PriceMarket;
                    base.Price = presentById.Price;
                    if (presentById.Price > 0M)
                    {
                        base.SaleType = 2;
                    }
                    else
                    {
                        base.SaleType = 3;
                    }
                    base.ServiceTerm = presentById.ServiceTerm;
                    base.ServiceTermUnit = presentById.ServiceTermUnit;
                    base.Remark = GetSalePromotionTypeRemark(this.m_ProductInfo);
                    base.BeginDate = DateTime.Today;
                    base.PresentExp = 0;
                    base.PresentMoney = 0M;
                    base.PresentPoint = 0;
                    base.TotalWeight = this.m_ProductInfo.Weight * base.Amount;
                    base.SubTotal = presentById.Price * presentNumber;
                    base.Id = presentById.PresentId;
                    base.isPresent = true;
                    base.Weight = presentById.Weight;
                    base.ProductCharacter = presentById.ProductCharacter;
                    return;
                }
            }
        }

        public static string GetSalePromotionTypeRemark(ProductInfo productInfo)
        {
            switch (productInfo.SalePromotionType)
            {
                case 1:
                    return string.Concat(new object[] { "买", productInfo.MinNumber, "送", productInfo.PresentNumber, "同样商品" });

                case 2:
                    return string.Concat(new object[] { "买", productInfo.MinNumber, "送/换购", productInfo.PresentNumber, "指定商品" });

                case 3:
                    return ("买就送" + productInfo.PresentNumber + "同样商品");

                case 4:
                    return ("买就送/换购" + productInfo.PresentNumber + "指定商品");
            }
            return "";
        }
    }
}


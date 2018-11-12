namespace EasyOne.IDal.Shop
{
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;

    public interface IStatistics
    {
        IList<CategorySaleroomInfo> GetCategorySaleroomList(int startRowIndexId, int maxNumberRows, int orderType, DateTime time, bool isAll);
        IList<CompareHitsOrderNumberInfo> GetCompareHitsOrderNumberList(int startRowIndexId, int maxNumberRows, int orderType);
        int GetHaveOrderOfMember();
        IList<MemberExpenditureInfo> GetMemberExpenditureList(int startRowIndexId, int maxNumberRows);
        IList<MemberOrderlinessInfo> GetMemberOrderlinessList(int startRowIndexId, int maxNumberRows, string userName, DateTime time, bool isAll);
        IList<MemberOrdersInfo> GetMemberOrdersList(int startRowIndexId, int maxNumberRows);
        IList<ProductHitsInfo> GetProductHitsList(int startRowIndexId, int maxNumberRows);
        IList<ProductOrderNumberInfo> GetProductOrderNumberList(int startRowIndexId, int maxNumberRows);
        decimal GetSaleOfDay(DateTime saleTime);
        decimal GetSaleOfMonth(DateTime saleTime);
        decimal GetSaleOfYear(DateTime saleTime);
        int GetTotalHits();
        int GetTotalMember();
        decimal GetTotalMoney(DateTime beginDate, DateTime endDate);
        int GetTotalOfCategorySaleroom();
        int GetTotalOfCompareHitsOrderNumber();
        int GetTotalOfMemberExpenditure();
        int GetTotalOfMemberOrderliness();
        int GetTotalOfMemberOrders();
        int GetTotalOfProductHits();
        int GetTotalOfProductOrderNumber();
        int GetTotalOrder(DateTime beginDate, DateTime endDate);
    }
}


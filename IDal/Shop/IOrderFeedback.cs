namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IOrderFeedback
    {
        bool Add(OrderFeedbackInfo orderFeedbackInfo);
        bool Delete(string id);
        IList<OrderFeedbackInfo> GetList(int orderId);
        IList<OrderFeedbackInfo> GetList(int startRowIndexId, int maxNumberRows);
        IList<OrderFeedbackDetailInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword);
        OrderFeedbackInfo GetOrderFeedbackById(int id);
        int GetTotalOfOrderFeedback();
        int TotalOfOrderFeedbackDetail();
        bool Update(OrderFeedbackInfo orderFeedbackInfo);
    }
}


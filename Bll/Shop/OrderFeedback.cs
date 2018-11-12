namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class OrderFeedback
    {
        private static readonly IOrderFeedback dal = DataAccess.CreateOrderFeedback();

        private OrderFeedback()
        {
        }

        public static bool Add(OrderFeedbackInfo orderFeedbackInfo)
        {
            return dal.Add(orderFeedbackInfo);
        }

        public static bool Delete(string id)
        {
            if (!DataValidator.IsValidId(id))
            {
                return false;
            }
            return dal.Delete(id);
        }

        public static IList<OrderFeedbackInfo> GetList(int orderId)
        {
            return dal.GetList(orderId);
        }

        public static IList<OrderFeedbackInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetList(startRowIndexId, maxNumberRows);
        }

        public static IList<OrderFeedbackDetailInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static OrderFeedbackInfo GetOrderFeedbackById(int id)
        {
            return dal.GetOrderFeedbackById(id);
        }

        public static int GetTotalOfOrderFeedback()
        {
            return dal.GetTotalOfOrderFeedback();
        }

        public static int TotalOfOrderFeedbackDetail(int searchType, string keyword)
        {
            return dal.TotalOfOrderFeedbackDetail();
        }

        public static bool Update(OrderFeedbackInfo orderFeedbackInfo)
        {
            return dal.Update(orderFeedbackInfo);
        }
    }
}


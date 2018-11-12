namespace EasyOne.IDal.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IOrder
    {
        bool Add(OrderInfo orderInfo);
        bool CancelConfirm(int orderId);
        int Confirm(int orderId);
        int CountBuyNum(string userName, int productId);
        int CountByNoConsignment();
        int CountByOrderStatus(OrderStatus status);
        string Delete(string orderId);
        bool DoDownload(int orderId, bool enableDownload);
        OrderInfo GetAnonymousOrderInfo(string orderNo, string contactName);
        IList<UserOrderCommonInfo> GetCardList(string userName);
        IList<UserOrderCommonInfo> GetDownList(string userName);
        UserOrderCommonInfo GetDownloadInfo(string userName, int orderItemId);
        string GetLastFunctionary();
        IList<OrderInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword, string action);
        IDictionary<int, string> GetListByUserName(string userName);
        int GetMaxId();
        OrderInfo GetMyOrderById(int orderId, string userName);
        OrderInfo GetOrderById(int orderId);
        OrderInfo GetOrderByOrderNum(string orderNum);
        ArrayList GetTotalofMoneyAndReceipt();
        ArrayList GetTotalofMoneyAndReceiptByAgentName(string agentName);
        ArrayList GetTotalofMoneyAndReceiptByUserName(string userName);
        int GetTotalOfOrder();
        ArrayList GetTotalofthisMoneyAndReceipt(string field);
        bool GoPause(int orderId);
        bool GoRubbish(int orderId);
        bool Recieve(int orderId);
        bool Transfer(int orderId, int clientId, string userName);
        bool Update(OrderInfo orderInfo);
        bool UpdateDeliverStatus(int orderId, DeliverStatus statusValue);
        bool UserPayment(int orderId, decimal moneyReceipt, OrderStatus status);
    }
}


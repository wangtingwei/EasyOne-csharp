namespace EasyOne.IDal.Crm
{
    using System;
    using System.Collections.Generic;

    public interface IClientDelete
    {
        bool DelBankrollItem(int clientId);
        bool DelBankrollItem(string userName);
        bool DelCompany(int clientId);
        bool DelComplainItem(int clientId);
        bool DelContacter(int clientId);
        bool DelContacter(string userName);
        bool DelOrder(int clientId);
        bool DelOrder(string userName);
        bool DelOrderItem(int orderId);
        bool DelPaymentLog(string userName);
        bool DelPointLog(string userName);
        bool DelServiceItem(int clientId);
        bool DelUser(int clientId);
        bool DelValidLog(string userName);
        int GetBankrollItemCount(int clientId);
        int GetComplainItemCount(int clientId);
        int GetOrderCount(int clientId);
        IList<int> GetOrderId(string userName);
        int GetServiceItemCount(int clientId);
        IDictionary<int, string> GetUserIdByClientId(int clientId);
        bool UpdateBankrollItem(int clientId);
        bool UpdateCompany(int clientId);
        bool UpdateContacter(int clientId);
        bool UpdateUser(int clientId);
    }
}


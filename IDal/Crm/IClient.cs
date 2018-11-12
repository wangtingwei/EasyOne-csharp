namespace EasyOne.IDal.Crm
{
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;

    public interface IClient
    {
        bool Add(ClientInfo clientInfo);
        bool CheckClientName(string clientName);
        bool CheckShortedForm(string shortedForm);
        bool Delete(string clientId);
        string GetAllClientId();
        ClientInfo GetClientById(int clientId);
        string GetClientIdByGroup(string groupIdList);
        string GetClientNameById(int clientId);
        IList<ClientInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int quickSearch, int groupId);
        int GetMaxId();
        int GetTotalOfClient();
        bool Income(int clientId, decimal money);
        bool Payment(int clientId, decimal money);
        bool Update(ClientInfo clientInfo);
        bool UpdateClientType(int clientId, int clientType);
        bool UpdateForCompany(int clientId, string companyName);
    }
}


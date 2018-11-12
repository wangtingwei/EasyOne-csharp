namespace EasyOne.IDal.Crm
{
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;

    public interface IService
    {
        bool Add(ServiceInfo serviceInfo);
        bool Delete(string id);
        IList<ServiceInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword);
        IList<ServiceInfo> GetListByClientName(int startRowIndexId, int maxNumberRows, string clientName);
        int GetMaxId();
        ServiceInfo GetServiceById(int id);
        int GetTotalOfService();
        bool Update(ServiceInfo serviceInfo);
    }
}


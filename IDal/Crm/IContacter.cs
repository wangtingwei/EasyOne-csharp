namespace EasyOne.IDal.Crm
{
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;

    public interface IContacter
    {
        bool Add(ContacterInfo contacterInfo);
        bool CheckExistsHomepage(string homepage);
        bool CheckExistsMsn(string msn);
        bool CheckExistsPhone(string phone);
        bool CheckExistsQQ(string qq);
        bool Delete(string contacterId);
        bool DeleteByUserName(string userName);
        bool Exists(int contacterId);
        bool Exists(string userName);
        IList<ContacterInfo> GetAllMobileContacters();
        ContacterInfo GetContacterByClientId(int clientId);
        ContacterInfo GetContacterById(int contacterId);
        ContacterInfo GetContacterByUserName(string userName);
        Dictionary<int, string> GetContacterListByClientId(int clientId);
        IList<ContacterInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword);
        int GetMaxId();
        IList<ContacterInfo> GetMobileContacterByGroupId(string groupId);
        IList<ContacterInfo> GetMobileContacterByRegion(string country, string province, string city);
        IList<ContacterInfo> GetMobileContacterByTrueName(string trueName);
        IList<ContacterInfo> GetMobileContacterByUserId(int startId, int endId);
        IList<ContacterInfo> GetMobileContacterByUserName(string userName);
        int GetTotalOfContacter();
        bool Update(ContacterInfo contacterInfo);
        bool UpdateClientForSameCompany(int clientId, int companyId);
    }
}


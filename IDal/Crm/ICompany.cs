namespace EasyOne.IDal.Crm
{
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;

    public interface ICompany
    {
        bool Add(CompanyInfo companyInfo);
        bool CheckExistsHomepage(string homepage);
        bool CheckExistsPhone(string phone);
        bool Delete(int companyId);
        bool Exists(int companyId, string companyName);
        CompanyInfo GetByCompanyName(string companyName);
        CompanyInfo GetCompanyByClientId(int clientId);
        IList<CompanyInfo> GetCompanyList(int startRowIndexId, int maxNumberRows, string keyword);
        IList<CompanyInfo> GetCompanyList(int startRowIndexId, int maxNumberRows, string field, string keyword, bool allowEmptyName);
        CompanyInfo GetCompayById(int compayId);
        IList<CompanyInfo> GetList(int startRowIndexId, int maxNumberRows, string keyword);
        int GetTotalOfCompany();
        bool RemoveUsers(int companyId);
        bool Update(CompanyInfo companyInfo);
    }
}


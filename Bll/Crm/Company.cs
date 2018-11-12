namespace EasyOne.Crm
{
    using EasyOne.Common;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Company
    {
        private static readonly ICompany dal = DataAccess.CreateCompany();

        private Company()
        {
        }

        public static bool Add(CompanyInfo companyInfo)
        {
            return dal.Add(companyInfo);
        }

        public static bool CheckExistsHomepage(string homepage)
        {
            return ((!string.IsNullOrEmpty(homepage) && (string.Compare("http://", homepage, true) != 0)) && dal.CheckExistsHomepage(homepage));
        }

        public static bool CheckExistsPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }
            return dal.CheckExistsPhone(phone);
        }

        public static bool Delete(int companyId)
        {
            return dal.Delete(companyId);
        }

        public static void Delete(string companyIdList)
        {
            if (DataValidator.IsValidId(companyIdList))
            {
                foreach (string str in companyIdList.Split(new char[] { ',' }))
                {
                    int compayId = DataConverter.CLng(str);
                    if (compayId != 0)
                    {
                        Client.UpdateClientType(GetCompayById(compayId).ClientId, 1);
                        RemoveUsers(compayId);
                        Delete(compayId);
                    }
                }
            }
        }

        public static bool Exists(int companyId, string companyName)
        {
            return dal.Exists(companyId, companyName);
        }

        public static CompanyInfo GetByCompanyName(string companyName)
        {
            return dal.GetByCompanyName(DataSecurity.FilterBadChar(companyName));
        }

        public static CompanyInfo GetCompanyByClientId(int clientId)
        {
            return dal.GetCompanyByClientId(clientId);
        }

        public static IList<CompanyInfo> GetCompanyList(int startRowIndexId, int maxNumberRows, string keyword)
        {
            return dal.GetCompanyList(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(keyword));
        }

        public static IList<CompanyInfo> GetCompanyList(int startRowIndexId, int maxNumberRows, string field, string keyword, bool allowEmptyName)
        {
            return dal.GetCompanyList(startRowIndexId, maxNumberRows, field, DataSecurity.FilterBadChar(keyword), allowEmptyName);
        }

        public static CompanyInfo GetCompayById(int compayId)
        {
            return dal.GetCompayById(compayId);
        }

        public static IList<CompanyInfo> GetList(int startRowIndexId, int maxNumberRows, string keyword)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(keyword));
        }

        public static int GetTotalOfCompany(string keyword)
        {
            return dal.GetTotalOfCompany();
        }

        public static int GetTotalOfCompany(string field, string keyword, bool allowEmptyName)
        {
            return dal.GetTotalOfCompany();
        }

        public static bool RemoveUsers(int companyId)
        {
            return dal.RemoveUsers(companyId);
        }

        public static bool Update(CompanyInfo companyInfo)
        {
            return dal.Update(companyInfo);
        }
    }
}


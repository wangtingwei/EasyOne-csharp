namespace EasyOne.Crm
{
    using EasyOne.Common;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Contacter
    {
        private static readonly IContacter dal = DataAccess.CreateContacter();

        private Contacter()
        {
        }

        public static bool Add(ContacterInfo contacterInfo)
        {
            return dal.Add(contacterInfo);
        }

        public static bool CheckExistsHomepage(string homepage)
        {
            return ((!string.IsNullOrEmpty(homepage) && (string.Compare("http://", homepage, true) != 0)) && dal.CheckExistsHomepage(homepage));
        }

        public static bool CheckExistsMsn(string msn)
        {
            if (string.IsNullOrEmpty(msn))
            {
                return false;
            }
            return dal.CheckExistsMsn(DataSecurity.FilterBadChar(msn));
        }

        public static bool CheckExistsPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }
            return dal.CheckExistsPhone(DataSecurity.FilterBadChar(phone));
        }

        public static bool CheckExistsQQ(string qq)
        {
            if (string.IsNullOrEmpty(qq))
            {
                return false;
            }
            return dal.CheckExistsQQ(DataSecurity.FilterBadChar(qq));
        }

        public static bool Delete(string contacterId)
        {
            bool flag = false;
            if (DataValidator.IsValidId(contacterId))
            {
                flag = dal.Delete(contacterId);
            }
            return flag;
        }

        public static bool DeleteByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            return dal.DeleteByUserName(DataSecurity.FilterBadChar(userName));
        }

        public static bool Exists(int contacterId)
        {
            return dal.Exists(contacterId);
        }

        public static bool Exists(string userName)
        {
            return dal.Exists(userName);
        }

        public static IList<ContacterInfo> GetAllMobileContacters()
        {
            return dal.GetAllMobileContacters();
        }

        public static ContacterInfo GetContacterByClientId(int clientId)
        {
            return dal.GetContacterByClientId(clientId);
        }

        public static ContacterInfo GetContacterById(int contacterId)
        {
            return dal.GetContacterById(contacterId);
        }

        public static ContacterInfo GetContacterByUserName(string userName)
        {
            return dal.GetContacterByUserName(userName);
        }

        public static Dictionary<int, string> GetContacterListByClientId(int clientId)
        {
            if (clientId <= 0)
            {
                return new Dictionary<int, string>();
            }
            return dal.GetContacterListByClientId(clientId);
        }

        public static IList<ContacterInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            if (searchType == 1)
            {
                keyword = DataConverter.CLng(keyword).ToString();
            }
            else
            {
                keyword = DataSecurity.FilterBadChar(keyword);
            }
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, keyword);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static IList<ContacterInfo> GetMobileContacterByGroupId(string groupId)
        {
            if (!DataValidator.IsValidId(groupId))
            {
                return new List<ContacterInfo>();
            }
            return dal.GetMobileContacterByGroupId(groupId);
        }

        public static IList<ContacterInfo> GetMobileContacterByRegion(string country, string province, string city)
        {
            if (string.IsNullOrEmpty(country))
            {
                country = "中华人民共和国";
            }
            return dal.GetMobileContacterByRegion(country, province, city);
        }

        public static IList<ContacterInfo> GetMobileContacterByTrueName(string trueName)
        {
            trueName = DataSecurity.FilterBadChar(trueName);
            trueName = StringHelper.ReplaceIgnoreCase(trueName, ",", "','");
            return dal.GetMobileContacterByTrueName(trueName);
        }

        public static IList<ContacterInfo> GetMobileContacterByUserId(int startId, int endId)
        {
            return dal.GetMobileContacterByUserId(startId, endId);
        }

        public static IList<ContacterInfo> GetMobileContacterByUserName(string userName)
        {
            userName = DataSecurity.FilterBadChar(userName);
            userName = StringHelper.ReplaceIgnoreCase(userName, ",", "','");
            return dal.GetMobileContacterByUserName(userName);
        }

        public static int GetTotalOfContacter(string searchType, string keyword)
        {
            return dal.GetTotalOfContacter();
        }

        public static bool Update(ContacterInfo contacterInfo)
        {
            return dal.Update(contacterInfo);
        }

        public static bool UpdateClientForSameCompany(int clientId, int companyId)
        {
            bool flag = true;
            if (companyId > 0)
            {
                flag = dal.UpdateClientForSameCompany(clientId, companyId);
            }
            return flag;
        }
    }
}


namespace EasyOne.Crm
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Service
    {
        private static readonly IService dal = DataAccess.CreateService();

        private Service()
        {
        }

        public static bool Add(ServiceInfo serviceInfo)
        {
            return dal.Add(serviceInfo);
        }

        public static bool Delete(string itemId)
        {
            return (DataValidator.IsValidId(itemId) && dal.Delete(itemId));
        }

        public static string[] GetField(string tableName, string fieldName)
        {
            return Choiceset.GetDataTextFields(tableName, fieldName);
        }

        public static string GetFiledNameById(string filed, int filedId)
        {
            foreach (ChoicesetValueInfo info in Choiceset.GetDictionaryFieldValueByName("PE_ServiceItem", filed))
            {
                if (info.DataValueField == filedId)
                {
                    return info.DataTextField.ToString();
                }
            }
            return "";
        }

        public static IList<ServiceInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!(searchType == "10"))
                {
                    keyword = DataSecurity.FilterBadChar(keyword);
                }
                else
                {
                    string str = field;
                    if (str != null)
                    {
                        if (!(str == "ClientID"))
                        {
                            if (str == "ServiceTime")
                            {
                                keyword = DataConverter.CDate(keyword).ToString("yyyy-MM-dd");
                            }
                        }
                        else if (DataConverter.CLng(keyword) == 0)
                        {
                            return new List<ServiceInfo>();
                        }
                    }
                }
            }
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, field, keyword);
        }

        public static IList<ServiceInfo> GetListByClientName(int startRowIndexId, int maxNumberRows, string clientName)
        {
            return dal.GetListByClientName(startRowIndexId, maxNumberRows, clientName);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static ServiceInfo GetServiceById(int id)
        {
            return dal.GetServiceById(id);
        }

        public static int GetTotalOfService(string searchType, string field, string keyword)
        {
            return dal.GetTotalOfService();
        }

        public static bool Update(ServiceInfo serviceInfo)
        {
            return dal.Update(serviceInfo);
        }
    }
}


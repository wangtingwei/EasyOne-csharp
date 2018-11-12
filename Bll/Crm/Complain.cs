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

    public sealed class Complain
    {
        private static readonly IComplain dal = DataAccess.CreateComplain();
        private static string[] Status = new string[] { "未处理", "处理中", "已处理", "已回访" };

        private Complain()
        {
        }

        public static bool Add(ComplainItemInfo info)
        {
            return ((info != null) && dal.Add(info));
        }

        public static bool Delete(string itemId)
        {
            return (DataValidator.IsValidId(itemId) && dal.Delete(itemId));
        }

        public static ComplainItemInfo GetComplainById(int itemId)
        {
            return dal.GetComplainById(itemId);
        }

        public static string GetFiledNameById(string filed, int filedId)
        {
            foreach (ChoicesetValueInfo info in Choiceset.GetDictionaryFieldValueByName("PE_ComplainItem", filed))
            {
                if (info.DataValueField == filedId)
                {
                    return info.DataTextField.ToString();
                }
            }
            return "";
        }

        public static IList<ComplainItemInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, int field, string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (field)
                {
                    case 0:
                        keyword = DataConverter.CLng(keyword).ToString();
                        break;

                    case 1:
                        keyword = DataSecurity.FilterBadChar(keyword);
                        break;

                    case 2:
                        keyword = DataConverter.CDate(keyword).ToString("yyyy-MM-dd");
                        break;
                }
            }
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, field, keyword);
        }

        public static IList<ComplainItemInfo> GetListByClientName(int startRowIndexId, int maxNumberRows, string clientName)
        {
            return dal.GetListByClientName(startRowIndexId, maxNumberRows, clientName);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static string GetStatus(int index)
        {
            return DataSecurity.GetArrayValue(index, Status);
        }

        public static int GetTotal(int searchType, int field, string keyword)
        {
            return dal.GetTotal();
        }

        public static bool Update(ComplainItemInfo info)
        {
            return ((info != null) && dal.Update(info));
        }
    }
}


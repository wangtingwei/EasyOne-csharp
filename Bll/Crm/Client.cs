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

    public sealed class Client
    {
        private static readonly IClient dal = DataAccess.CreateClient();

        private Client()
        {
        }

        public static bool Add(ClientInfo clientInfo)
        {
            return dal.Add(clientInfo);
        }

        public static bool CheckClientName(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                return false;
            }
            return dal.CheckClientName(clientName);
        }

        public static bool CheckShortedForm(string shortedForm)
        {
            if (string.IsNullOrEmpty(shortedForm))
            {
                return false;
            }
            return dal.CheckShortedForm(shortedForm);
        }

        public static bool Delete(string clientId)
        {
            return (DataValidator.IsValidId(clientId) && dal.Delete(clientId));
        }

        public static string GetAllClientId()
        {
            return dal.GetAllClientId();
        }

        public static ClientInfo GetClientById(int clientId)
        {
            return dal.GetClientById(clientId);
        }

        public static string GetClientGroupName(int groupId)
        {
            foreach (ChoicesetValueInfo info in Choiceset.GetDictionaryFieldValueByName("PE_Client", "GroupID"))
            {
                if (info.DataValueField == groupId)
                {
                    return info.DataTextField.ToString();
                }
            }
            return "";
        }

        public static string GetClientIdByGroup(string groupIdList)
        {
            if (!DataValidator.IsValidId(groupIdList))
            {
                return string.Empty;
            }
            return dal.GetClientIdByGroup(groupIdList);
        }

        public static string GetClientNameById(int clientId)
        {
            return dal.GetClientNameById(clientId);
        }

        public static string GetClientNum()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssff");
        }

        public static string GetClientTypeName(int clientType)
        {
            switch (clientType)
            {
                case 0:
                    return "企业客户";

                case 1:
                    return "个人客户";
            }
            return "";
        }

        public static IList<ClientInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, string quickSearch, string groupId)
        {
            int num = DataConverter.CLng(searchType);
            if (num == 1)
            {
                keyword = DataConverter.CLng(keyword).ToString();
            }
            return dal.GetList(startRowIndexId, maxNumberRows, num, DataSecurity.FilterBadChar(keyword), DataConverter.CLng(quickSearch), DataConverter.CLng(groupId, -1));
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static int GetTotalOfClient(string searchType, string keyword, string quickSearch, string groupId)
        {
            return dal.GetTotalOfClient();
        }

        public static bool Income(int clientId, decimal money, string remark, string inputer, string memo)
        {
            bool flag = false;
            if (dal.Income(clientId, money))
            {
                BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
                bankrollItemInfo.Inputer = inputer;
                bankrollItemInfo.UserName = "";
                bankrollItemInfo.ClientId = clientId;
                bankrollItemInfo.CurrencyType = 1;
                bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
                bankrollItemInfo.Money = money;
                bankrollItemInfo.MoneyType = 4;
                bankrollItemInfo.OrderId = 0;
                bankrollItemInfo.Remark = remark;
                bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
                bankrollItemInfo.Memo = memo;
                flag = BankrollItem.Add(bankrollItemInfo);
            }
            return flag;
        }

        public static bool Payment(int clientId, decimal money, string remark, string inputer, string memo)
        {
            bool flag = false;
            if (dal.Payment(clientId, money))
            {
                BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
                bankrollItemInfo.Inputer = inputer;
                bankrollItemInfo.UserName = "";
                bankrollItemInfo.ClientId = clientId;
                bankrollItemInfo.CurrencyType = 1;
                bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
                bankrollItemInfo.Money = (money > 0M) ? (-1M * money) : money;
                bankrollItemInfo.MoneyType = 4;
                bankrollItemInfo.OrderId = 0;
                bankrollItemInfo.Remark = remark;
                bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
                bankrollItemInfo.Memo = memo;
                flag = BankrollItem.Add(bankrollItemInfo);
            }
            return flag;
        }

        public static bool Remit(int clientId, decimal money, DateTime receiptDate, string bank, string remark, string inputer, string memo)
        {
            bool flag = false;
            if (dal.Income(clientId, money))
            {
                BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
                bankrollItemInfo.Inputer = inputer;
                bankrollItemInfo.UserName = "";
                bankrollItemInfo.ClientId = clientId;
                bankrollItemInfo.CurrencyType = 1;
                bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
                bankrollItemInfo.DateAndTime = new DateTime?(receiptDate);
                bankrollItemInfo.Money = money;
                bankrollItemInfo.MoneyType = 4;
                bankrollItemInfo.OrderId = 0;
                bankrollItemInfo.Bank = bank;
                bankrollItemInfo.Remark = remark;
                bankrollItemInfo.Memo = memo;
                flag = BankrollItem.Add(bankrollItemInfo);
            }
            return flag;
        }

        public static bool Update(ClientInfo clientInfo)
        {
            return dal.Update(clientInfo);
        }

        public static bool UpdateClientType(int clientId, int clientType)
        {
            return dal.UpdateClientType(clientId, clientType);
        }

        public static bool UpdateForCompany(int clientId, string companyName)
        {
            return dal.UpdateForCompany(clientId, companyName);
        }
    }
}


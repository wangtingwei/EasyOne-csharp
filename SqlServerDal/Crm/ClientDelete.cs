namespace EasyOne.SqlServerDal.Crm
{
    using EasyOne.Common;
    using EasyOne.IDal.Crm;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ClientDelete : IClientDelete
    {
        public bool DelBankrollItem(int clientId)
        {
            return Delete("PE_BankrollItem", clientId);
        }

        public bool DelBankrollItem(string userName)
        {
            return Delete("PE_BankrollItem", userName);
        }

        public bool DelCompany(int clientId)
        {
            return Delete("PE_Company", clientId);
        }

        public bool DelComplainItem(int clientId)
        {
            return Delete("PE_ComplainItem", clientId);
        }

        public bool DelContacter(int clientId)
        {
            return Delete("PE_Contacter", clientId);
        }

        public bool DelContacter(string userName)
        {
            return Delete("PE_Contacter", userName);
        }

        private static bool Delete(string tableName, int clientId)
        {
            return DBHelper.ExecuteSql("DELETE FROM " + DBHelper.FilterBadChar(tableName) + " WHERE ClientID = @ClientID", new Parameters("@ClientID", DbType.Int32, clientId));
        }

        private static bool Delete(string tableName, string userName)
        {
            return DBHelper.ExecuteSql("DELETE FROM " + DBHelper.FilterBadChar(tableName) + " WHERE UserName = @UserName", new Parameters("@UserName", DbType.String, userName));
        }

        public bool DelOrder(int clientId)
        {
            return Delete("PE_Orders", clientId);
        }

        public bool DelOrder(string userName)
        {
            return Delete("PE_Orders", userName);
        }

        public bool DelOrderItem(int orderId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_OrderItem WHERE OrderID = @OrderID", new Parameters("@OrderID", DbType.Int32, orderId));
        }

        public bool DelPaymentLog(string userName)
        {
            return Delete("PE_PaymentLog", userName);
        }

        public bool DelPointLog(string userName)
        {
            return Delete("PE_PointLog", userName);
        }

        public bool DelServiceItem(int clientId)
        {
            return Delete("PE_ServiceItem", clientId);
        }

        public bool DelUser(int clientId)
        {
            return Delete("PE_Users", clientId);
        }

        public bool DelValidLog(string userName)
        {
            return Delete("PE_ValidLog", userName);
        }

        public int GetBankrollItemCount(int clientId)
        {
            return GetCount("PE_BankrollItem", clientId);
        }

        public int GetComplainItemCount(int clientId)
        {
            return GetCount("PE_ComplainItem", clientId);
        }

        private static int GetCount(string tableName, int clientId)
        {
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT COUNT(0) FROM " + DBHelper.FilterBadChar(tableName) + " WHERE ClientID = @ClientID", new Parameters("@ClientID", DbType.Int32, clientId)));
        }

        public int GetOrderCount(int clientId)
        {
            return GetCount("PE_Orders", clientId);
        }

        public IList<int> GetOrderId(string userName)
        {
            IList<int> list = new List<int>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT OrderID FROM PE_Orders WHERE UserName = @UserName", new Parameters("@UserName", DbType.String, userName)))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetInt32("OrderID"));
                }
            }
            return list;
        }

        public int GetServiceItemCount(int clientId)
        {
            return GetCount("PE_ServiceItem", clientId);
        }

        public IDictionary<int, string> GetUserIdByClientId(int clientId)
        {
            IDictionary<int, string> dictionary = new Dictionary<int, string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT UserID, UserName FROM PE_Users WHERE ClientID = @ClientID", new Parameters("@ClientID", DbType.Int32, clientId)))
            {
                if (reader.Read())
                {
                    dictionary.Add(reader.GetInt32("UserID"), reader.GetString("UserName"));
                }
            }
            return dictionary;
        }

        private static bool Update(string tableName, int clientId)
        {
            return DBHelper.ExecuteSql("UPDATE " + DBHelper.FilterBadChar(tableName) + " SET ClientID = 0 WHERE ClientID = @ClientID", new Parameters("@ClientID", DbType.Int32, clientId));
        }

        public bool UpdateBankrollItem(int clientId)
        {
            return Update("PE_BankrollItem", clientId);
        }

        public bool UpdateCompany(int clientId)
        {
            return Update("PE_Company", clientId);
        }

        public bool UpdateContacter(int clientId)
        {
            return Update("PE_Contacter", clientId);
        }

        public bool UpdateUser(int clientId)
        {
            return Update("PE_Users", clientId);
        }
    }
}


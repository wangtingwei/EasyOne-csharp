namespace EasyOne.SqlServerDal.Crm
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class Address : IAddress
    {
        private int m_TotalOfAddress;

        public bool Add(AddressInfo addressInfo)
        {
            addressInfo.AddressId = this.GetMaxId() + 1;
            return DBHelper.ExecuteProc("PR_Crm_Address_Add", GetParameters(addressInfo));
        }

        private static AddressInfo AddressFromrdr(NullableDataReader rdr)
        {
            AddressInfo info = new AddressInfo();
            info.AddressId = rdr.GetInt32("AddressID");
            info.UserName = rdr.GetString("UserName");
            info.HomePhone = rdr.GetString("HomePhone");
            info.Mobile = rdr.GetString("Mobile");
            info.Country = rdr.GetString("Country");
            info.Province = rdr.GetString("Province");
            info.City = rdr.GetString("City");
            info.Area = rdr.GetString("Area");
            info.Address = rdr.GetString("Address");
            info.ZipCode = rdr.GetString("ZipCode");
            info.IsDefault = rdr.GetBoolean("IsDefault");
            info.ConsigneeName = rdr.GetString("ConsigneeName");
            return info;
        }

        public bool Delete(string addressList, string userName)
        {
            return DBHelper.ExecuteSql(string.Format("DELETE FROM PE_Address WHERE AddressID IN ({0})  IF NOT EXISTS(SELECT * FROM PE_Address WHERE UserName = '{1}' AND IsDefault = 1)  UPDATE PE_Address SET IsDefault = 1 WHERE AddressID= (SELECT TOP 1 AddressID FROM PE_Address WHERE UserName = '{1}')", DBHelper.ToValidId(addressList), DBHelper.FilterBadChar(userName)));
        }

        public bool DeleteById(int addressId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Address WHERE AddressID = @AddressID", new Parameters("@AddressID", DbType.Int32, addressId));
        }

        public bool DeleteByUserName(string userName)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Address WHERE UserName = @UserName", new Parameters("@UserName", DbType.String, userName));
        }

        public AddressInfo GetAddressById(int addressId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Address WHERE AddressID = @AddressID", new Parameters("@AddressID", DbType.Int32, addressId)))
            {
                if (reader.Read())
                {
                    return AddressFromrdr(reader);
                }
                return new AddressInfo(true);
            }
        }

        public IList<AddressInfo> GetAddressList(int startRowIndexId, int maxNumberRows, string type, string key)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "AddressID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Address");
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(key))
            {
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, string.Empty);
            }
            else if (string.Compare(type, "Area", StringComparison.OrdinalIgnoreCase) == 0)
            {
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "Province LIKE '%" + DBHelper.FilterBadChar(key) + "%' OR City LIKE '%" + DBHelper.FilterBadChar(key) + "%' OR Area LIKE '%" + DBHelper.FilterBadChar(key) + "%'");
            }
            else
            {
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, type + " LIKE '%" + DBHelper.FilterBadChar(key) + "%'");
            }
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<AddressInfo> list = new List<AddressInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(AddressFromrdr(reader));
                }
            }
            this.m_TotalOfAddress = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<AddressInfo> GetAddressListByUserName(string userName)
        {
            IList<AddressInfo> list = new List<AddressInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Address WHERE UserName = @UserName", new Parameters("@UserName", DbType.String, userName)))
            {
                while (reader.Read())
                {
                    list.Add(AddressFromrdr(reader));
                }
            }
            return list;
        }

        public AddressInfo GetDefaultAddressByUserName(string userName)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 1 * FROM PE_Address WHERE UserName = @UserName AND IsDefault = 1", new Parameters("@UserName", DbType.String, userName)))
            {
                if (reader.Read())
                {
                    return AddressFromrdr(reader);
                }
                return new AddressInfo(true);
            }
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Address", "AddressID");
        }

        private static Parameters GetParameters(AddressInfo addressInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@AddressID", DbType.Int32, addressInfo.AddressId);
            parameters.AddInParameter("@UserName", DbType.String, addressInfo.UserName);
            parameters.AddInParameter("@HomePhone", DbType.String, addressInfo.HomePhone);
            parameters.AddInParameter("@Mobile", DbType.String, addressInfo.Mobile);
            parameters.AddInParameter("@Country", DbType.String, addressInfo.Country);
            parameters.AddInParameter("@Province", DbType.String, addressInfo.Province);
            parameters.AddInParameter("@City", DbType.String, addressInfo.City);
            parameters.AddInParameter("@Area", DbType.String, addressInfo.Area);
            parameters.AddInParameter("@Address", DbType.String, addressInfo.Address);
            parameters.AddInParameter("@ZipCode", DbType.String, addressInfo.ZipCode);
            parameters.AddInParameter("@ConsigneeName", DbType.String, addressInfo.ConsigneeName);
            return parameters;
        }

        public int GetTotalOfAddress()
        {
            return this.m_TotalOfAddress;
        }

        public bool SetDefault(int addressId, string userName)
        {
            string strSql = "UPDATE PE_Address SET IsDefault = 0 WHERE UserName = @UserName AND IsDefault = 1  UPDATE PE_Address set IsDefault = 1 WHERE AddressID = @AddressID";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@AddressID", DbType.Int32, addressId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Update(AddressInfo addressInfo)
        {
            return DBHelper.ExecuteProc("PR_Crm_Address_Update", GetParameters(addressInfo));
        }
    }
}


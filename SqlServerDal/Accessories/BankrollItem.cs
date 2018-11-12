namespace EasyOne.SqlServerDal.Accessories
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class BankrollItem : IBankrollItem
    {
        private int m_TotalOfBankrollItem;
        private int m_TotalOfBill;

        public bool Add(BankrollItemInfo bankrollItemInfo)
        {
            Parameters cmdParams = GetParameters(bankrollItemInfo);
            cmdParams.AddInParameter("@Memo", DbType.String, bankrollItemInfo.Memo);
            return DBHelper.ExecuteProc("PR_Accessories_BankrollItem_Add", cmdParams);
        }

        private static BankrollItemInfo BankrollItemFromrdr(NullableDataReader rdr)
        {
            BankrollItemInfo info = new BankrollItemInfo();
            info.ItemId = rdr.GetInt32("ItemID");
            info.UserName = rdr.GetString("UserName");
            info.ClientId = rdr.GetInt32("ClientID");
            info.DateAndTime = rdr.GetNullableDateTime("DateAndTime");
            info.Money = rdr.GetDecimal("Money");
            info.MoneyType = rdr.GetInt32("MoneyType");
            info.CurrencyType = rdr.GetInt32("CurrencyType");
            info.EBankId = rdr.GetInt32("eBankID");
            info.Bank = rdr.GetString("Bank");
            info.OrderId = rdr.GetInt32("OrderID");
            info.PaymentId = rdr.GetInt32("PaymentID");
            info.Remark = rdr.GetString("Remark");
            info.LogTime = rdr.GetNullableDateTime("LogTime");
            info.Inputer = rdr.GetString("Inputer");
            info.IP = rdr.GetString("IP");
            info.Status = (BankrollItemStatus) rdr.GetInt32("Status");
            info.Memo = rdr.GetString("Memo");
            return info;
        }

        private static void ComplexSearch(string keyword, Database db, DbCommand procdbComm)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                string str = string.Empty;
                string[] strArray = keyword.Split(new char[] { '|' });
                int num = DataConverter.CLng(strArray[0]);
                int num2 = DataConverter.CLng(strArray[1]);
                string str2 = strArray[2];
                string str3 = strArray[3];
                string str4 = strArray[4];
                string str5 = strArray[5];
                string str6 = strArray[6];
                if (num > 0)
                {
                    str = (str + (string.IsNullOrEmpty(str) ? "" : " AND ")) + "B.ItemID >= " + num.ToString();
                }
                if (num2 > 0)
                {
                    str = (str + (string.IsNullOrEmpty(str) ? "" : " AND ")) + "B.ItemID <= " + num2.ToString();
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    str = (str + (string.IsNullOrEmpty(str) ? "" : " AND ")) + "B.DateAndTime >='" + str2.Replace("'", "") + "'";
                }
                if (!string.IsNullOrEmpty(str3))
                {
                    str = (str + (string.IsNullOrEmpty(str) ? "" : " AND ")) + "B.DateAndTime <= '" + str3.Replace("'", "") + "'";
                }
                if (!string.IsNullOrEmpty(str4))
                {
                    str = (str + (string.IsNullOrEmpty(str) ? "" : " AND ")) + "(C.ShortedForm LIKE '%" + DBHelper.FilterBadChar(str4) + "%') ";
                }
                if (!string.IsNullOrEmpty(str5))
                {
                    str = (str + (string.IsNullOrEmpty(str) ? "" : " AND ")) + "(B.UserName LIKE '%" + DBHelper.FilterBadChar(str5) + "%') ";
                }
                if (!string.IsNullOrEmpty(str6))
                {
                    str = (str + (string.IsNullOrEmpty(str) ? "" : " AND ")) + " (B.Bank = '" + DBHelper.FilterBadChar(str6) + "') ";
                }
                db.SetParameterValue(procdbComm, "@Filter", str);
            }
        }

        public bool Confirm(int itemId, BankrollItemStatus status)
        {
            string strSql = "UPDATE PE_BankrollItem SET Status = @Status WHERE itemId = @itemId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, (int) status);
            cmdParams.AddInParameter("@itemId", DbType.Int32, itemId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(int itemId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, itemId);
            return DBHelper.ExecuteSql("DELETE FROM PE_BankrollItem WHERE [ItemId] = @ItemId", cmdParams);
        }

        public bool ExistsConfirmRemittance(int orderId)
        {
            string strSql = "SELECT ItemID FROM PE_BankrollItem WHERE OrderID = @OrderID AND Status = 0";
            return DBHelper.ExistsSql(strSql, new Parameters("@OrderID", DbType.Int32, orderId));
        }

        public bool ExistsPaymentLog(int paymentId)
        {
            return DBHelper.Exists(CommandType.Text, "SELECT * FROM PE_BankrollItem WHERE PaymentID = @PaymentID", new Parameters("@PaymentID", DbType.Int32, paymentId));
        }

        public BankrollItemInfo GetBankrollItemById(int itemId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, itemId);
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, "SELECT * From PE_BankrollItem WHERE [ItemId] = @ItemId", cmdParams))
            {
                if (reader.Read())
                {
                    return BankrollItemFromrdr(reader);
                }
                return new BankrollItemInfo(true);
            }
        }

        public DataTable GetBillOfAgent(int startRowIndexId, int maxNumberRows, string userName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Accessories_BankroolItem_GetBill");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "B.OrderID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "Max(O.OrderNum) AS tOrderNum, SUM(B.Money) AS tMoney, Max(B.DateAndTime) AS tDateAndTime, Max(B.Remark) AS tRemark");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "B.UserName = '" + DBHelper.FilterBadChar(userName) + "' AND B.Status=1 ");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_BankrollItem B LEFT JOIN PE_Orders O On B.OrderID = O.OrderID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            DataTable table = new DataTable();
            table.Columns.Add("DateAndTime", typeof(DateTime));
            table.Columns.Add("OrderNum", typeof(string));
            table.Columns.Add("Money", typeof(decimal));
            table.Columns.Add("Remark", typeof(string));
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["DateAndTime"] = reader.GetDateTime("tDateAndTime");
                    row["OrderNum"] = reader.GetString("tOrderNum");
                    row["Money"] = reader.GetDecimal("tMoney");
                    row["Remark"] = reader.GetString("tRemark");
                    table.Rows.Add(row);
                }
            }
            this.m_TotalOfBill = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return table;
        }

        private static ArrayList GetIncomeAndPayout(string sql)
        {
            ArrayList list = new ArrayList();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(sql))
            {
                if (reader.Read())
                {
                    list.Add(reader[0]);
                }
                if (reader.NextResult() && reader.Read())
                {
                    list.Add(reader[0]);
                }
            }
            return list;
        }

        public IList<BankrollItemInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, int field, string keyword)
        {
            Database db = DatabaseFactory.CreateDatabase();
            IList<BankrollItemInfo> list = new List<BankrollItemInfo>();
            DbCommand storedProcCommand = db.GetStoredProcCommand("PR_Common_GetList");
            db.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            db.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            db.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "B.ItemID");
            db.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "B.*, C.ShortedForm AS ClientName");
            db.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            db.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            db.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_BankrollItem B LEFT JOIN PE_Client C ON B.ClientID = C.ClientID");
            db.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            switch (searchType)
            {
                case 1:
                    db.SetParameterValue(storedProcCommand, "@Filter", " DATEDIFF(dd, B.DateAndTime, GETDATE()) < 10 ");
                    break;

                case 2:
                    db.SetParameterValue(storedProcCommand, "@Filter", " DATEDIFF(mm, B.DateAndTime, GETDATE()) < 1 ");
                    break;

                case 3:
                    db.SetParameterValue(storedProcCommand, "@Filter", " B.Money > 0 ");
                    break;

                case 4:
                    db.SetParameterValue(storedProcCommand, "@Filter", " B.Money<0 ");
                    break;

                case 5:
                    db.SetParameterValue(storedProcCommand, "@Filter", " B.Status = 1");
                    break;

                case 6:
                    db.SetParameterValue(storedProcCommand, "@Filter", " B.Status = 0");
                    break;

                case 10:
                    HighLevelSearch(field, keyword, db, storedProcCommand);
                    break;

                case 11:
                    ComplexSearch(keyword, db, storedProcCommand);
                    break;
            }
            using (NullableDataReader reader = new NullableDataReader(db.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    BankrollItemInfo item = BankrollItemFromrdr(reader);
                    item.ClientName = reader.GetString("ClientName");
                    list.Add(item);
                }
            }
            this.m_TotalOfBankrollItem = (int) db.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxItemId()
        {
            return DBHelper.GetMaxId("PE_BankrollItem", "ItemID");
        }

        private static Parameters GetParameters(BankrollItemInfo bankrollItemInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@UserName", DbType.String, bankrollItemInfo.UserName);
            parameters.AddInParameter("@ClientID", DbType.Int32, bankrollItemInfo.ClientId);
            parameters.AddInParameter("@DateAndTime", DbType.DateTime, bankrollItemInfo.DateAndTime);
            parameters.AddInParameter("@Money", DbType.Currency, bankrollItemInfo.Money);
            parameters.AddInParameter("@MoneyType", DbType.Int32, bankrollItemInfo.MoneyType);
            parameters.AddInParameter("@CurrencyType", DbType.Int32, bankrollItemInfo.CurrencyType);
            parameters.AddInParameter("@eBankID", DbType.Int32, bankrollItemInfo.EBankId);
            parameters.AddInParameter("@Bank", DbType.String, bankrollItemInfo.Bank);
            parameters.AddInParameter("@OrderID", DbType.Int32, bankrollItemInfo.OrderId);
            parameters.AddInParameter("@PaymentID", DbType.Int32, bankrollItemInfo.PaymentId);
            parameters.AddInParameter("@Remark", DbType.String, bankrollItemInfo.Remark);
            parameters.AddInParameter("@LogTime", DbType.DateTime, bankrollItemInfo.LogTime);
            parameters.AddInParameter("@Inputer", DbType.String, bankrollItemInfo.Inputer);
            parameters.AddInParameter("@IP", DbType.String, bankrollItemInfo.IP);
            parameters.AddInParameter("@Status", DbType.Int32, bankrollItemInfo.Status);
            return parameters;
        }

        public ArrayList GetTotalInComeAndPayOutAll()
        {
            return GetIncomeAndPayout("SELECT ISNULL(SUM(money), 0) FROM PE_BankrollItem WHERE [Money]>0 AND Status = 1 ; SELECT ISNULL(SUM(money), 0) FROM PE_BankrollItem WHERE Money<0 AND Status = 1");
        }

        public ArrayList GetTotalInComeAndPayOutAll(int clientId)
        {
            return GetIncomeAndPayout(string.Format("SELECT ISNULL(SUM(money), 0) FROM PE_BankrollItem WHERE clientId = {0} AND [Money]>0 AND Status = 1 ; SELECT ISNULL(SUM(money), 0) FROM PE_BankrollItem WHERE clientId = {0} AND Money < 0 AND Status = 1", clientId));
        }

        public ArrayList GetTotalInComeAndPayOutAll(string userName)
        {
            return GetIncomeAndPayout(string.Format("SELECT ISNULL(SUM(money), 0) FROM PE_BankrollItem WHERE UserName = '{0}' AND [Money]>0 AND Status = 1 ; SELECT ISNULL(SUM(money), 0) FROM PE_BankrollItem WHERE UserName = '{0}' AND Money < 0 AND Status = 1", DBHelper.FilterBadChar(userName)));
        }

        public int GetTotalOfBankrollItem()
        {
            return this.m_TotalOfBankrollItem;
        }

        public int GetTotalOfBill()
        {
            return this.m_TotalOfBill;
        }

        private static void HighLevelSearch(int field, string keyword, Database db, DbCommand procdbComm)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (field)
                {
                    case 0:
                        db.SetParameterValue(procdbComm, "@Filter", " C.ShortedForm LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ");
                        return;

                    case 1:
                        db.SetParameterValue(procdbComm, "@Filter", " B.UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ");
                        return;

                    case 2:
                        db.SetParameterValue(procdbComm, "@Filter", " B.Bank = '" + DBHelper.FilterBadChar(keyword) + "' ");
                        return;

                    case 3:
                        db.SetParameterValue(procdbComm, "@Filter", " DATEDIFF(dd, B.DateAndTime, '" + keyword + "') = 0 ");
                        return;

                    case 4:
                        db.SetParameterValue(procdbComm, "@Filter", " B.ClientID = " + DBHelper.ToNumber(keyword));
                        return;

                    case 5:
                        db.SetParameterValue(procdbComm, "@Filter", "B.OrderID = " + DBHelper.ToNumber(keyword) + " AND B.Money<0");
                        return;

                    case 6:
                        db.SetParameterValue(procdbComm, "@Filter", " B.UserName = '" + DBHelper.FilterBadChar(keyword) + "'");
                        return;

                    case 7:
                        db.SetParameterValue(procdbComm, "@Filter", " B.UserName = '" + DBHelper.FilterBadChar(keyword) + "' AND B.Money > 0 AND B.Status = 1");
                        return;

                    case 8:
                        db.SetParameterValue(procdbComm, "@Filter", " B.UserName = '" + DBHelper.FilterBadChar(keyword) + "' AND B.Money<0 AND B.Status = 1");
                        return;

                    default:
                        return;
                }
            }
        }

        public bool Update(BankrollItemInfo bankrollItemInfo)
        {
            Parameters cmdParams = GetParameters(bankrollItemInfo);
            cmdParams.AddInParameter("@ItemId", DbType.Int32, bankrollItemInfo.ItemId);
            return DBHelper.ExecuteProc("PR_Accessories_BankrollItem_Update", cmdParams);
        }
    }
}


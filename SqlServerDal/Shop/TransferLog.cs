namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public sealed class TransferLog : ITransferLog
    {
        private int m_TotalOfTransferLog;

        public bool Add(TransferLogInfo info)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderId", DbType.Int32, info.OrderId);
            cmdParams.AddInParameter("@TransferTime", DbType.DateTime, info.TransferTime);
            cmdParams.AddInParameter("@Poundage", DbType.Decimal, info.Poundage);
            cmdParams.AddInParameter("@OwnerUserName", DbType.String, info.OwnerUserName);
            cmdParams.AddInParameter("@TargetUserName", DbType.String, info.TargetUserName);
            cmdParams.AddInParameter("@PayerUserName", DbType.String, info.PayerUserName);
            cmdParams.AddInParameter("@Inputer", DbType.String, info.Inputer);
            cmdParams.AddInParameter("@Remark", DbType.String, info.Remark);
            return DBHelper.ExecuteProc("PR_Shop_TransferLog_Add", cmdParams);
        }

        public IList<TransferLogInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String);
            database.SetParameterValue(storedProcCommand, "@StartRows", startRowIndexId);
            database.SetParameterValue(storedProcCommand, "@PageSize", maxNumberRows);
            database.SetParameterValue(storedProcCommand, "@SortColumn", "TransferLogId");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "T.*, O.OrderNum, O.UserName");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_TransferLog T INNER JOIN PE_Orders O ON T.OrderID = O.OrderID");
            switch (Convert.ToInt32(searchType))
            {
                case 0:
                    database.SetParameterValue(storedProcCommand, "@Filter", "");
                    goto Label_0278;

                case 1:
                    database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(dd, T.TransferTime, GETDATE()) < 10");
                    goto Label_0278;

                case 2:
                    database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(m, T.TransferTime, GETDATE()) < 1");
                    goto Label_0278;

                case 10:
                {
                    string str = field;
                    if (str == null)
                    {
                        break;
                    }
                    if (!(str == "UserName"))
                    {
                        if (str == "ContacterName")
                        {
                            database.SetParameterValue(storedProcCommand, "@Filter", "O.ContacterName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ");
                            goto Label_0278;
                        }
                        if (str == "Inputer")
                        {
                            database.SetParameterValue(storedProcCommand, "@Filter", "T.Inputer LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ");
                            goto Label_0278;
                        }
                        if (str == "TransferTime")
                        {
                            database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(dd, T.TransferTime, '" + DBHelper.FilterBadChar(keyword) + "') < 1 ");
                            goto Label_0278;
                        }
                        if (str == "OrderID")
                        {
                            database.SetParameterValue(storedProcCommand, "@Filter", "T.OrderID = " + DBHelper.ToNumber(keyword));
                            goto Label_0278;
                        }
                        break;
                    }
                    database.SetParameterValue(storedProcCommand, "@Filter", "O.UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ");
                    goto Label_0278;
                }
                default:
                    database.SetParameterValue(storedProcCommand, "@Filter", "");
                    goto Label_0278;
            }
            database.SetParameterValue(storedProcCommand, "@Filter", "");
        Label_0278:
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            IList<TransferLogInfo> list = new List<TransferLogInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(TransferLogFromrdr(reader));
                }
            }
            this.m_TotalOfTransferLog = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetTotalOfTransferLog()
        {
            return this.m_TotalOfTransferLog;
        }

        public TransferLogInfo GetTransferLogById(int transferLogId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT T.*, O.OrderNum, O.UserName FROM PE_TransferLog T INNER JOIN PE_Orders O ON T.OrderID = O.OrderID WHERE TransferLogID = @TransferLogID", new Parameters("@TransferLogId", DbType.Int32, transferLogId)))
            {
                if (reader.Read())
                {
                    return TransferLogFromrdr(reader);
                }
                return new TransferLogInfo(true);
            }
        }

        private static TransferLogInfo TransferLogFromrdr(NullableDataReader rdr)
        {
            TransferLogInfo info = new TransferLogInfo();
            info.TransferLogId = rdr.GetInt32("TransferLogID");
            info.OrderId = rdr.GetInt32("OrderId");
            info.TransferTime = rdr.GetNullableDateTime("TransferTime");
            info.Poundage = rdr.GetDecimal("Poundage");
            info.OwnerUserName = rdr.GetString("OwnerUserName");
            info.TargetUserName = rdr.GetString("TargetUserName");
            info.PayerUserName = rdr.GetString("PayerUserName");
            info.Inputer = rdr.GetString("Inputer");
            info.Remark = rdr.GetString("Remark");
            info.UserName = rdr.GetString("UserName");
            info.OrderNum = rdr.GetString("OrderNum");
            return info;
        }
    }
}


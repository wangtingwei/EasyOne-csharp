namespace EasyOne.SqlServerDal.Accessories
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class PaymentLog : IPaymentLog
    {
        private int m_TotalOfPaymentLog;

        public bool Add(PaymentLogInfo paymentLogInfo)
        {
            paymentLogInfo.PaymentLogId = GetMaxId() + 1;
            return DBHelper.ExecuteProc("PR_Shop_PaymentLog_Add", GetParameters(paymentLogInfo));
        }

        public bool Delete(DateTime tempDate)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_PaymentLog WHERE Status = 1 AND PayTime < @Date", new Parameters("@Date", DbType.DateTime, tempDate));
        }

        public bool Delete(string paymentLogId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@paymentLogId", DbType.String, paymentLogId);
            return DBHelper.ExecuteProc("PR_Accessories_PaymentLog_Delete", cmdParams);
        }

        public PaymentLogInfo GetInfoByPaymentNum(string paymentNum)
        {
            string strSql = "SELECT TOP 1 * FROM PE_PaymentLog WHERE PaymentNum = @PaymentNum ";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, new Parameters("@PaymentNum", DbType.String, paymentNum)))
            {
                if (reader.Read())
                {
                    return PaymentLogFromrdr(reader);
                }
                return new PaymentLogInfo(true);
            }
        }

        public IList<PaymentLogInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword)
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
            database.SetParameterValue(storedProcCommand, "@SortColumn", "PaymentLogId");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "*");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_PaymentLog");
            if (string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(searchType))
                {
                    switch (searchType)
                    {
                        case "0":
                            database.SetParameterValue(storedProcCommand, "@Filter", "");
                            goto Label_02E4;

                        case "1":
                            database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(dd, PayTime, GETDATE()) < 10");
                            goto Label_02E4;

                        case "2":
                            database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(m, PayTime, GETDATE()) < 1");
                            goto Label_02E4;

                        case "3":
                            database.SetParameterValue(storedProcCommand, "@Filter", "Status = 1");
                            goto Label_02E4;

                        case "4":
                            database.SetParameterValue(storedProcCommand, "@Filter", "Status = 2");
                            goto Label_02E4;

                        case "5":
                            database.SetParameterValue(storedProcCommand, "@Filter", "Status = 3");
                            goto Label_02E4;

                        case "6":
                            database.SetParameterValue(storedProcCommand, "@Filter", "UserName = '" + DBHelper.FilterBadChar(field) + "'");
                            goto Label_02E4;
                    }
                    database.SetParameterValue(storedProcCommand, "@Filter", "");
                }
                else
                {
                    database.SetParameterValue(storedProcCommand, "@Filter", "");
                }
            }
            else if (!string.IsNullOrEmpty(field))
            {
                if (field == "PayTime")
                {
                    database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(dd, PayTime, '" + keyword + "') = 0");
                }
                else
                {
                    database.SetParameterValue(storedProcCommand, "@Filter", field + " LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                }
            }
            else
            {
                database.SetParameterValue(storedProcCommand, "@Filter", "");
            }
        Label_02E4:
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            IList<PaymentLogInfo> list = new List<PaymentLogInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(PaymentLogFromrdr(reader));
                }
            }
            this.m_TotalOfPaymentLog = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<PaymentLogInfo> GetListByOrderId(int orderId)
        {
            IList<PaymentLogInfo> list = new List<PaymentLogInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_PaymentLog WHERE OrderID = @OrderID", new Parameters("@OrderID", DbType.Int32, orderId)))
            {
                while (reader.Read())
                {
                    list.Add(PaymentLogFromrdr(reader));
                }
            }
            return list;
        }

        public IList<PaymentLogInfo> GetListByUserName(int startRowIndexId, int maxNumberRows, string userName)
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
            database.SetParameterValue(storedProcCommand, "@SortColumn", "PaymentLogId");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "*");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_PaymentLog");
            database.SetParameterValue(storedProcCommand, "@Filter", "UserName = '" + DBHelper.FilterBadChar(userName) + "'");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            IList<PaymentLogInfo> list = new List<PaymentLogInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(PaymentLogFromrdr(reader));
                }
            }
            this.m_TotalOfPaymentLog = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_PaymentLog", "PaymentLogId");
        }

        private static Parameters GetParameters(PaymentLogInfo paymentLogInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@PaymentLogID", DbType.Int32, paymentLogInfo.PaymentLogId);
            parameters.AddInParameter("@UserName", DbType.String, paymentLogInfo.UserName);
            parameters.AddInParameter("@OrderID", DbType.Int32, paymentLogInfo.OrderId);
            parameters.AddInParameter("@PaymentNum", DbType.String, paymentLogInfo.PaymentNum);
            parameters.AddInParameter("@PlatformID", DbType.Int32, paymentLogInfo.PlatformId);
            parameters.AddInParameter("@MoneyPay", DbType.Currency, paymentLogInfo.MoneyPay);
            parameters.AddInParameter("@MoneyTrue", DbType.Currency, paymentLogInfo.MoneyTrue);
            parameters.AddInParameter("@PayTime", DbType.DateTime, paymentLogInfo.PayTime);
            parameters.AddInParameter("@SuccessTime", DbType.DateTime, paymentLogInfo.SuccessTime);
            parameters.AddInParameter("@Status", DbType.Int32, paymentLogInfo.Status);
            parameters.AddInParameter("@PlatformInfo", DbType.String, paymentLogInfo.PlatformInfo);
            parameters.AddInParameter("@Remark", DbType.String, paymentLogInfo.Remark);
            parameters.AddInParameter("@Point", DbType.Int32, paymentLogInfo.Point);
            return parameters;
        }

        public PaymentLogInfo GetPaymentLogById(int paymentLogId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_PaymentLog WHERE PaymentLogID = @PaymentLogID", new Parameters("@PaymentLogID", DbType.Int32, paymentLogId)))
            {
                if (reader.Read())
                {
                    return PaymentLogFromrdr(reader);
                }
                return new PaymentLogInfo(true);
            }
        }

        public int GetTotalOfPaymentLog()
        {
            return this.m_TotalOfPaymentLog;
        }

        private static PaymentLogInfo PaymentLogFromrdr(NullableDataReader rdr)
        {
            PaymentLogInfo info = new PaymentLogInfo();
            info.PaymentLogId = rdr.GetInt32("PaymentLogId");
            info.UserName = rdr.GetString("UserName");
            info.OrderId = rdr.GetInt32("OrderId");
            info.PaymentNum = rdr.GetString("PaymentNum");
            info.PlatformId = rdr.GetInt32("PlatformId");
            info.MoneyPay = rdr.GetDecimal("MoneyPay");
            info.MoneyTrue = rdr.GetDecimal("MoneyTrue");
            info.PayTime = rdr.GetNullableDateTime("PayTime");
            info.SuccessTime = rdr.GetNullableDateTime("SuccessTime");
            info.Status = rdr.GetInt32("Status");
            info.PlatformInfo = rdr.GetString("PlatformInfo");
            info.Remark = rdr.GetString("Remark");
            info.Point = rdr.GetInt32("Point");
            return info;
        }

        public bool Update(PaymentLogInfo info)
        {
            Parameters cmdParams = GetParameters(info);
            return DBHelper.ExecuteProc("PR_Accessories_PaymentLog_Update2", cmdParams);
        }

        public int Update(int paymentLogId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Accessories_PaymentLog_Update");
            database.AddInParameter(storedProcCommand, "@paymentLogId", DbType.Int32, paymentLogId);
            database.AddInParameter(storedProcCommand, "@Status", DbType.Int32, 3);
            database.AddInParameter(storedProcCommand, "@PlatformInfo", DbType.String, "支付完成");
            database.AddInParameter(storedProcCommand, "@Remark", DbType.String, "未知");
            database.AddParameter(storedProcCommand, "@RETURN_VALUE", DbType.String, ParameterDirection.ReturnValue, "", DataRowVersion.Current, null);
            database.ExecuteNonQuery(storedProcCommand);
            return (int) storedProcCommand.Parameters["@RETURN_VALUE"].Value;
        }
    }
}


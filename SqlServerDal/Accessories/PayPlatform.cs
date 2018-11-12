namespace EasyOne.SqlServerDal.Accessories
{
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public sealed class PayPlatform : IPayPlatform
    {
        public bool Add(PayPlatformInfo payPlatformInfo)
        {
            Parameters cmdParams = new Parameters();
            int num = GetMaxId() + 1;
            if (num < 100)
            {
                num = 100;
            }
            payPlatformInfo.PayPlatformId = num;
            cmdParams.AddInParameter("@PayPlatformID", DbType.Int32, payPlatformInfo.PayPlatformId);
            cmdParams.AddInParameter("@PayPlatformName", DbType.String, payPlatformInfo.PayPlatformName);
            cmdParams.AddInParameter("@AccountsID", DbType.String, payPlatformInfo.AccountsId);
            cmdParams.AddInParameter("@MD5", DbType.String, payPlatformInfo.MD5);
            cmdParams.AddInParameter("@Rate", DbType.Double, payPlatformInfo.Rate);
            cmdParams.AddInParameter("@IsDisabled", DbType.Boolean, payPlatformInfo.IsDisabled);
            cmdParams.AddInParameter("@IsDefault", DbType.Boolean, payPlatformInfo.IsDefault);
            return DBHelper.ExecuteProc("PR_Accessories_PayPlatform_Add", cmdParams);
        }

        public bool CheckSameName(string payPlatformName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@PayPlatformName", DbType.String, payPlatformName);
            return DBHelper.ExistsSql("SELECT * FROM PE_PayPlatForm WHERE PayPlatformName = @PayPlatformName", cmdParams);
        }

        public bool Delete(int payPlatformId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@PayPlatformId", DbType.Int32, payPlatformId);
            return DBHelper.ExecuteProc("PR_Accessories_PayPlatform_Delete", cmdParams);
        }

        public bool DisablePayPlatform(int payPlatformId, bool isDisabled)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@PayPlatformId", DbType.Int32, payPlatformId);
            cmdParams.AddInParameter("@IsDisabled", DbType.Boolean, isDisabled);
            try
            {
                return (DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "PR_Accessories_PayPlatform_DisablePayPlatform", cmdParams) > 0);
            }
            catch
            {
                return false;
            }
        }

        public PayPlatformInfo GetInfoByName(string payPlatformName)
        {
            string strSql = "SELECT TOP 1 * FROM PE_PayPlatForm WHERE PayPlatformName = @PayPlatformName";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, new Parameters("@PayPlatformName", DbType.String, payPlatformName)))
            {
                if (reader.Read())
                {
                    return PayPlatformFromDataReader(reader);
                }
                return new PayPlatformInfo(true);
            }
        }

        public IList<PayPlatformInfo> GetList()
        {
            List<PayPlatformInfo> list = new List<PayPlatformInfo>();
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Accessories_PayPlatform_GetList")))
            {
                while (reader.Read())
                {
                    list.Add(PayPlatformFromDataReader(reader));
                }
            }
            return list;
        }

        public IList<PayPlatformInfo> GetListOfDisabled(bool isDisabled)
        {
            List<PayPlatformInfo> list = new List<PayPlatformInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@isdisabled", DbType.Boolean, isDisabled);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReader(CommandType.StoredProcedure, "PR_Accessories_PayPlatform_GetListOfdisabled", cmdParams)))
            {
                while (reader.Read())
                {
                    list.Add(PayPlatformFromDataReader(reader));
                }
            }
            return list;
        }

        private static int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_PayPlatForm", "PayPlatformID");
        }

        public PayPlatformInfo GetPayPlatformById(int payPlatformId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@PayPlatformID", DbType.Int32, payPlatformId);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReader(CommandType.StoredProcedure, "PR_Accessories_PayPlatform_GetInfoById", cmdParams)))
            {
                if (reader.Read())
                {
                    return PayPlatformFromDataReader(reader);
                }
                return new PayPlatformInfo(true);
            }
        }

        private static PayPlatformInfo PayPlatformFromDataReader(NullableDataReader rdr)
        {
            PayPlatformInfo info = new PayPlatformInfo();
            info.PayPlatformId = rdr.GetInt32("PayPlatformID");
            info.PayPlatformName = rdr.GetString("PayPlatformName");
            info.Rate = rdr.GetDouble("Rate");
            info.MD5 = rdr.GetString("MD5");
            info.AccountsId = rdr.GetString("AccountsId");
            info.OrderId = rdr.GetInt32("OrderID");
            info.IsDisabled = rdr.GetBoolean("IsDisabled");
            info.IsDefault = rdr.GetBoolean("IsDefault");
            return info;
        }

        public bool SetDefault(int payPlatformId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@PayPlatformId", DbType.Int32, payPlatformId);
            return DBHelper.ExecuteProc("PR_Accessories_PayPlatform_SetDefault", cmdParams);
        }

        public bool SetOrderId(int payPlatformId, int orderId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@PayPlatformId", DbType.Int32, payPlatformId);
            cmdParams.AddInParameter("@OrderId", DbType.Int32, orderId);
            return DBHelper.ExecuteProc("PR_Accessories_PayPlatform_SetOrderId", cmdParams);
        }

        public bool Update(PayPlatformInfo payPlatformInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@PayPlatformId", DbType.Int32, payPlatformInfo.PayPlatformId);
            cmdParams.AddInParameter("@PayPlatformName", DbType.String, payPlatformInfo.PayPlatformName);
            cmdParams.AddInParameter("@AccountsID", DbType.String, payPlatformInfo.AccountsId);
            cmdParams.AddInParameter("@MD5", DbType.String, payPlatformInfo.MD5);
            cmdParams.AddInParameter("@Rate", DbType.Double, payPlatformInfo.Rate);
            cmdParams.AddInParameter("@OrderId", DbType.Int32, payPlatformInfo.OrderId);
            cmdParams.AddInParameter("@IsDisabled", DbType.Boolean, payPlatformInfo.IsDisabled);
            cmdParams.AddInParameter("@IsDefault", DbType.Boolean, payPlatformInfo.IsDefault);
            return DBHelper.ExecuteProc("PR_Accessories_PayPlatform_Update", cmdParams);
        }
    }
}


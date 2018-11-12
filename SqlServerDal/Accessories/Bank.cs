namespace EasyOne.SqlServerDal.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public sealed class Bank : IBank
    {
        public bool Add(BankInfo bankInfo)
        {
            Parameters cmdParams = GetParameters(bankInfo);
            return DBHelper.ExecuteProc("PR_Accessories_Bank_Add", cmdParams);
        }

        private static BankInfo BankFromDataReader(NullableDataReader rdr)
        {
            BankInfo info = new BankInfo();
            info.BankId = rdr.GetInt32("BankID");
            info.BankName = rdr.GetString("BankName");
            info.BankShortName = rdr.GetString("BankShortName");
            info.BankPic = rdr.GetString("BankPic");
            info.CardNum = rdr.GetString("CardNum");
            info.HolderName = rdr.GetString("HolderName");
            info.IsDefault = rdr.GetBoolean("IsDefault");
            info.IsDisabled = rdr.GetBoolean("IsDisabled");
            info.OrderId = rdr.GetInt32("OrderID");
            info.BankIntro = rdr.GetString("BankIntro");
            info.Accounts = rdr.GetString("Accounts");
            return info;
        }

        public int Count()
        {
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_Bank"));
        }

        public bool Delete(int bankId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@BankID", DbType.Int32, bankId);
            return DBHelper.ExecuteProc("PR_Accessories_Bank_Delete", cmdParams);
        }

        public bool ExistBankShortName(string bankShortName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@BankShortName", DbType.String, bankShortName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Bank WHERE BankShortName = @BankShortName", cmdParams);
        }

        public BankInfo GetBankById(int bankId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@BankID", DbType.Int32, bankId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_Bank_GetBankInfoByID", cmdParams))
            {
                if (reader.Read())
                {
                    return BankFromDataReader(reader);
                }
                return new BankInfo(true);
            }
        }

        public IList<BankInfo> GetList()
        {
            List<BankInfo> list = new List<BankInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_Bank_GetList"))
            {
                while (reader.Read())
                {
                    list.Add(BankFromDataReader(reader));
                }
            }
            return list;
        }

        public IList<BankInfo> GetList(int startRowIndexId, int maxiNumRows)
        {
            List<BankInfo> list = new List<BankInfo>();
            CommonListParameters cmdParams = new CommonListParameters(startRowIndexId, maxiNumRows);
            cmdParams.TableName = "PE_Bank";
            cmdParams.StrColumn = "*";
            cmdParams.SortColumn = "OrderID";
            cmdParams.Sorts = Sorts.Asc;
            cmdParams.CreateParameter();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Common_GetList", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(BankFromDataReader(reader));
                }
            }
            return list;
        }

        public IList<BankInfo> GetListByEnabled()
        {
            IList<BankInfo> list = new List<BankInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Bank WHERE IsDisabled = 0 ORDER BY OrderID"))
            {
                while (reader.Read())
                {
                    list.Add(BankFromDataReader(reader));
                }
            }
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Bank", "BankID");
        }

        private static Parameters GetParameters(BankInfo bankInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@BankShortName", DbType.String, bankInfo.BankShortName);
            parameters.AddInParameter("@BankName", DbType.String, bankInfo.BankName);
            parameters.AddInParameter("@Accounts", DbType.String, bankInfo.Accounts);
            parameters.AddInParameter("@CardNum", DbType.String, bankInfo.CardNum);
            parameters.AddInParameter("@HolderName", DbType.String, bankInfo.HolderName);
            parameters.AddInParameter("@BankIntro", DbType.String, bankInfo.BankIntro);
            parameters.AddInParameter("@BankPic", DbType.String, bankInfo.BankPic);
            parameters.AddInParameter("@IsDefault", DbType.Boolean, bankInfo.IsDefault);
            parameters.AddInParameter("@IsDisabled", DbType.Boolean, bankInfo.IsDisabled);
            parameters.AddInParameter("@BankID", DbType.Int32, bankInfo.BankId);
            return parameters;
        }

        public bool SetDefault(int bankId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@BankID", DbType.Int32, bankId);
            return DBHelper.ExecuteProc("PR_Accessories_Bank_SetDefault", cmdParams);
        }

        public bool SetDisabled(int bankId, bool isDisabled)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@BankID", DbType.Int32, bankId);
            cmdParams.AddInParameter("@IsDisabled", DbType.Boolean, isDisabled);
            return DBHelper.ExecuteProc("PR_Accessories_Bank_SetDisabled", cmdParams);
        }

        public bool SetOrderId(int bankId, int orderId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@BankID", DbType.Int32, bankId);
            cmdParams.AddInParameter("@OrderID", DbType.Int32, orderId);
            return (DBHelper.ExecuteNonQuerySql("UPDATE PE_Bank SET [OrderID]=@OrderID WHERE [BankID]=@BankID", cmdParams) > 0);
        }

        public bool Update(BankInfo bankInfo)
        {
            Parameters cmdParams = GetParameters(bankInfo);
            cmdParams.AddInParameter("@OrderID", DbType.Int32, bankInfo.OrderId);
            return DBHelper.ExecuteProc("PR_Accessories_Bank_Update", cmdParams);
        }
    }
}


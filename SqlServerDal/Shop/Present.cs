namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class Present : IPresent
    {
        private int m_TotalOfPresents;
        private const string s_PresentFields = "PresentID, PresentName, PresentNum, Unit, PresentPic, PresentThumb, ServiceTermUnit, ServiceTerm, Price, Price_Market, Weight, Stocks, AlarmNum, ProductCharacter, DownloadUrl, Remark, PresentIntro, PresentExplain";
        private const string s_PresentParameters = "@PresentID, @PresentName, @PresentNum, @Unit, @PresentPic, @PresentThumb, @ServiceTermUnit, @ServiceTerm, @Price, @Price_Market, @Weight, @Stocks, @AlarmNum, @ProductCharacter, @DownloadUrl, @Remark, @PresentIntro, @PresentExplain";

        public bool AddPresent(PresentInfo info)
        {
            info.PresentId = GetMaxId() + 1;
            string strSql = "INSERT INTO PE_Present(PresentID, PresentName, PresentNum, Unit, PresentPic, PresentThumb, ServiceTermUnit, ServiceTerm, Price, Price_Market, Weight, Stocks, AlarmNum, ProductCharacter, DownloadUrl, Remark, PresentIntro, PresentExplain) VALUES (@PresentID, @PresentName, @PresentNum, @Unit, @PresentPic, @PresentThumb, @ServiceTermUnit, @ServiceTerm, @Price, @Price_Market, @Weight, @Stocks, @AlarmNum, @ProductCharacter, @DownloadUrl, @Remark, @PresentIntro, @PresentExplain)";
            return DBHelper.ExecuteSql(strSql, GetPresentParameters(info));
        }

        public bool AddStocks(int id, int quantity)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            cmdParams.AddInParameter("@Quantity", DbType.Int32, quantity);
            return DBHelper.ExecuteSql("UPDATE PE_Present SET Stocks = ISNULL(Stocks, 0)+@Quantity WHERE PresentID = @ID ", cmdParams);
        }

        public bool DeletePresents(string idList)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Present WHERE PresentID IN (" + DBHelper.ToValidId(idList) + ")");
        }

        private static int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Present", "PresentID");
        }

        public IList<PresentInfo> GetPresentByCharacter(ProductCharacter productCharacter)
        {
            IList<PresentInfo> list = new List<PresentInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Present WHERE (ProductCharacter&@ProductCharacter) > 0", new Parameters("@ProductCharacter", DbType.Int32, (int) productCharacter)))
            {
                while (reader.Read())
                {
                    PresentInfo item = PresentFromrdr(reader);
                    list.Add(item);
                }
            }
            return list;
        }

        public PresentInfo GetPresentById(int id)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Present WHERE PresentID = @id", new Parameters("@id", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    return PresentFromrdr(reader);
                }
                return new PresentInfo(true);
            }
        }

        public PresentInfo GetPresentByPresentNum(string presentNum)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Present WHERE PresentId = @PresentNum", new Parameters("@PresentNum", DbType.String, presentNum)))
            {
                if (reader.Read())
                {
                    return PresentFromrdr(reader);
                }
                return new PresentInfo(true);
            }
        }

        public IList<PresentInfo> GetPresentList(int startRowIndexId, int maxNumberRows)
        {
            return this.GetPresentList(startRowIndexId, maxNumberRows, 0, "");
        }

        public IList<PresentInfo> GetPresentList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            IList<PresentInfo> list = new List<PresentInfo>();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "PresentID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Present");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            string str = "";
            if ((searchType == 1) && !string.IsNullOrEmpty(keyword))
            {
                str = "PresentName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
            }
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    PresentInfo item = PresentFromrdr(reader);
                    list.Add(item);
                }
            }
            this.m_TotalOfPresents = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public string GetPresentNameById(int id)
        {
            string str = "";
            string strSql = "SELECT PresentName FROM PE_Present WHERE PresentID = @ID";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.String, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    str = reader.GetString("PresentName");
                }
            }
            return str;
        }

        private static Parameters GetPresentParameters(PresentInfo PresentInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@PresentID", DbType.Int32, PresentInfo.PresentId);
            parameters.AddInParameter("@PresentName", DbType.String, PresentInfo.PresentName);
            parameters.AddInParameter("@PresentPic", DbType.String, PresentInfo.PresentPic);
            parameters.AddInParameter("PresentThumb", DbType.String, PresentInfo.PresentThumb);
            parameters.AddInParameter("@Unit", DbType.String, PresentInfo.Unit);
            parameters.AddInParameter("@PresentNum", DbType.String, PresentInfo.PresentNum);
            parameters.AddInParameter("@ServiceTermUnit", DbType.Int32, PresentInfo.ServiceTermUnit);
            parameters.AddInParameter("@ServiceTerm", DbType.Int32, PresentInfo.ServiceTerm);
            parameters.AddInParameter("@Price", DbType.Currency, PresentInfo.Price);
            parameters.AddInParameter("@Price_Market", DbType.Currency, PresentInfo.PriceMarket);
            parameters.AddInParameter("@Weight", DbType.Double, PresentInfo.Weight);
            parameters.AddInParameter("@Stocks", DbType.Int32, PresentInfo.Stocks);
            parameters.AddInParameter("@AlarmNum", DbType.Int32, PresentInfo.AlarmNum);
            parameters.AddInParameter("@ProductCharacter", DbType.Int32, PresentInfo.ProductCharacter);
            parameters.AddInParameter("@DownloadUrl", DbType.String, PresentInfo.DownloadUrl);
            parameters.AddInParameter("@Remark", DbType.String, PresentInfo.Remark);
            parameters.AddInParameter("@PresentIntro", DbType.String, PresentInfo.PresentIntro);
            parameters.AddInParameter("@PresentExplain", DbType.String, PresentInfo.PresentExplain);
            return parameters;
        }

        public int GetPresentTotal()
        {
            return this.m_TotalOfPresents;
        }

        public IList<string> GetUnitList()
        {
            IList<string> list = new List<string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT DISTINCT Unit FROM PE_Present"))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString("Unit"));
                }
            }
            return list;
        }

        private static PresentInfo PresentFromrdr(NullableDataReader rdr)
        {
            PresentInfo info = new PresentInfo();
            info.PresentId = rdr.GetInt32("PresentID");
            info.PresentName = rdr.GetString("PresentName");
            info.PresentPic = rdr.GetString("PresentPic");
            info.PresentThumb = rdr.GetString("PresentThumb");
            info.Unit = rdr.GetString("Unit");
            info.PresentNum = rdr.GetString("PresentNum");
            info.ServiceTermUnit = (ServiceTermUnit) rdr.GetInt32("ServiceTermUnit");
            info.ServiceTerm = rdr.GetInt32("ServiceTerm");
            info.Price = rdr.GetDecimal("Price");
            info.PriceMarket = rdr.GetDecimal("Price_Market");
            info.Weight = rdr.GetDouble("Weight");
            info.ProductCharacter = (ProductCharacter) rdr.GetInt32("ProductCharacter");
            info.Stocks = rdr.GetInt32("Stocks");
            info.DownloadUrl = rdr.GetString("DownloadUrl");
            info.Remark = rdr.GetString("Remark");
            info.AlarmNum = rdr.GetInt32("AlarmNum");
            info.PresentIntro = rdr.GetString("PresentIntro");
            info.PresentExplain = rdr.GetString("PresentExplain");
            return info;
        }

        public bool UpdatePressent(PresentInfo info)
        {
            StringBuilder builder = new StringBuilder(0x80);
            builder.Append("UPDATE ");
            builder.Append("PE_Present");
            builder.Append(" SET ");
            string[] strArray = "PresentID, PresentName, PresentNum, Unit, PresentPic, PresentThumb, ServiceTermUnit, ServiceTerm, Price, Price_Market, Weight, Stocks, AlarmNum, ProductCharacter, DownloadUrl, Remark, PresentIntro, PresentExplain".Split(new char[] { ',' });
            string[] strArray2 = "@PresentID, @PresentName, @PresentNum, @Unit, @PresentPic, @PresentThumb, @ServiceTermUnit, @ServiceTerm, @Price, @Price_Market, @Weight, @Stocks, @AlarmNum, @ProductCharacter, @DownloadUrl, @Remark, @PresentIntro, @PresentExplain".Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                builder.Append(strArray[i]);
                builder.Append("=");
                builder.Append(strArray2[i]);
                builder.Append(",");
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" WHERE PresentID = @PresentID");
            return DBHelper.ExecuteSql(builder.ToString(), GetPresentParameters(info));
        }
    }
}


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

    public class Coupon : ICoupon
    {
        private int m_TotalOfAllCoupon;
        private int m_TotalOfCoupon;

        public bool Add(CouponInfo couponInfo)
        {
            couponInfo.CouponId = GetNextId();
            Parameters cmdParams = GetParameters(couponInfo);
            return DBHelper.ExecuteSql("INSERT INTO PE_Coupon (CouponID, CouponName, CouponNumPattern, Money, State, UserGroup, BeginDate, EndDate, LimitNum, ProductLimitType, ProductLimitContent, CouponCreateType, CouponCreateContent, OrderTotalMoney, UseEndDate) VALUES (@CouponID, @CouponName, @CouponNumPattern, @Money, @State, @UserGroup, @BeginDate, @EndDate, @LimitNum, @ProductLimitType, @ProductLimitContent, @CouponCreateType, @CouponCreateContent, @OrderTotalMoney, @UseEndDate)", cmdParams);
        }

        public bool Delete(string couponId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Coupon WHERE CouponID IN (" + DBHelper.ToValidId(couponId) + ")");
        }

        public IList<CouponDetailInfo> GetAllDetailList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            string str = "1 = 1";
            switch (searchType)
            {
                case 1:
                    str = str + " AND I.Usetimes = 0";
                    break;

                case 2:
                    str = str + " AND I.Usetimes > 0";
                    break;

                case 3:
                    str = str + " AND C.UseEndDate + 1 >= GETDATE()";
                    break;

                case 4:
                    str = str + " AND C.UseEndDate + 1 < GETDATE()";
                    break;
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (searchType)
                {
                    case 10:
                        str = str + " AND C.CouponName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                        break;

                    case 11:
                        str = str + " AND U.UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                        break;

                    case 12:
                        str = str + " AND I.CouponNum LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                        break;
                }
            }
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "I.ID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "C.*, I.*, U.UserName");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CouponItem I LEFT JOIN PE_Users U ON I.UserID = U.UserID INNER JOIN PE_Coupon C ON I.CouponID = C.CouponID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<CouponDetailInfo> list = new List<CouponDetailInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CouponDetailInfo item = new CouponDetailInfo();
                    item.CouponInfo = GetCouponInfoFromrdataReader(reader);
                    item.CouponItemInfo = CouponItem.GetItemInfoFromrdataReader(reader);
                    item.UserName = reader.GetString("UserName");
                    list.Add(item);
                }
            }
            this.m_TotalOfAllCoupon = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetAllTotalOfCoupon()
        {
            return this.m_TotalOfAllCoupon;
        }

        public CouponInfo GetCouponInfoById(int couponId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Coupon WHERE CouponID = @CouponID", new Parameters("@CouponID", DbType.Int32, couponId)))
            {
                if (reader.Read())
                {
                    return GetCouponInfoFromrdataReader(reader);
                }
                return new CouponInfo(true);
            }
        }

        private static CouponInfo GetCouponInfoFromrdataReader(NullableDataReader dataReader)
        {
            CouponInfo info = new CouponInfo();
            info.BeginDate = dataReader.GetDateTime("BeginDate");
            info.CouponCreateContent = dataReader.GetString("CouponCreateContent");
            info.CouponCreateType = (CouponCreateType) dataReader.GetInt32("CouponCreateType");
            info.CouponId = dataReader.GetInt32("CouponId");
            info.CouponName = dataReader.GetString("CouponName");
            info.EndDate = dataReader.GetDateTime("EndDate");
            info.LimitNum = dataReader.GetInt32("LimitNum");
            info.Money = dataReader.GetDecimal("Money");
            info.OrderTotalMoney = dataReader.GetDecimal("OrderTotalMoney");
            info.CouponNumPattern = dataReader.GetString("CouponNumPattern");
            info.ProductLimitContent = dataReader.GetString("ProductLimitContent");
            info.ProductLimitType = (ProductLimitType) dataReader.GetInt32("ProductLimitType");
            info.State = dataReader.GetInt32("State");
            info.UserGroup = dataReader.GetString("UserGroup");
            info.UseEndDate = dataReader.GetDateTime("UseEndDate");
            return info;
        }

        public IList<CouponDetailInfo> GetDetailList(int startRowIndexId, int maxNumberRows, int userId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "I.ID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "I.UserID =" + userId);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CouponItem I INNER JOIN PE_Coupon C ON I.CouponID = C.CouponID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<CouponDetailInfo> list = new List<CouponDetailInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CouponDetailInfo item = new CouponDetailInfo();
                    item.CouponInfo = GetCouponInfoFromrdataReader(reader);
                    item.CouponItemInfo = CouponItem.GetItemInfoFromrdataReader(reader);
                    list.Add(item);
                }
            }
            this.m_TotalOfCoupon = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<CouponInfo> GetList()
        {
            IList<CouponInfo> list = new List<CouponInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Coupon WHERE (BeginDate <= GETDATE()) AND (EndDate >= GETDATE()) AND (State = 1)"))
            {
                while (reader.Read())
                {
                    list.Add(GetCouponInfoFromrdataReader(reader));
                }
            }
            return list;
        }

        public IList<CouponInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "CouponID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Coupon");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<CouponInfo> list = new List<CouponInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(GetCouponInfoFromrdataReader(reader));
                }
            }
            this.m_TotalOfCoupon = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static int GetNextId()
        {
            return (DBHelper.GetMaxId("PE_Coupon", "CouponID") + 1);
        }

        private static Parameters GetParameters(CouponInfo couponInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@CouponId", DbType.String, couponInfo.CouponId);
            parameters.AddInParameter("@BeginDate", DbType.DateTime, couponInfo.BeginDate);
            parameters.AddInParameter("@CouponCreateContent", DbType.String, couponInfo.CouponCreateContent);
            parameters.AddInParameter("@CouponCreateType", DbType.Int32, Convert.ToInt32(couponInfo.CouponCreateType));
            parameters.AddInParameter("@CouponName", DbType.String, couponInfo.CouponName);
            parameters.AddInParameter("@EndDate", DbType.DateTime, couponInfo.EndDate);
            parameters.AddInParameter("@LimitNum", DbType.Int32, couponInfo.LimitNum);
            parameters.AddInParameter("@Money", DbType.Decimal, couponInfo.Money);
            parameters.AddInParameter("@OrderTotalMoney", DbType.Decimal, couponInfo.OrderTotalMoney);
            parameters.AddInParameter("@CouponNumPattern", DbType.String, couponInfo.CouponNumPattern);
            parameters.AddInParameter("@ProductLimitContent", DbType.String, couponInfo.ProductLimitContent);
            parameters.AddInParameter("@ProductLimitType", DbType.Int32, Convert.ToInt32(couponInfo.ProductLimitType));
            parameters.AddInParameter("@State", DbType.Int32, couponInfo.State);
            parameters.AddInParameter("@UserGroup", DbType.String, couponInfo.UserGroup);
            parameters.AddInParameter("@UseEndDate", DbType.DateTime, couponInfo.UseEndDate);
            return parameters;
        }

        public int GetTotalOfCoupon()
        {
            return this.m_TotalOfCoupon;
        }

        public bool SetState(int couponId, int state)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CouponID", DbType.Int32, couponId);
            cmdParams.AddInParameter("@State", DbType.Int32, state);
            return DBHelper.ExecuteSql("UPDATE PE_Coupon SET State = @State WHERE CouponID = @CouponID", cmdParams);
        }

        public bool Update(CouponInfo couponInfo)
        {
            Parameters cmdParams = GetParameters(couponInfo);
            return DBHelper.ExecuteSql("UPDATE PE_Coupon SET CouponName = @CouponName, CouponNumPattern = @CouponNumPattern, Money = @Money, State = @State, UserGroup = @UserGroup, BeginDate = @BeginDate, EndDate = @EndDate, LimitNum = @LimitNum, ProductLimitType = @ProductLimitType, ProductLimitContent = @ProductLimitContent, CouponCreateType = @CouponCreateType, CouponCreateContent = @CouponCreateContent, OrderTotalMoney = @OrderTotalMoney, UseEndDate = @UseEndDate WHERE CouponID = @CouponID", cmdParams);
        }
    }
}


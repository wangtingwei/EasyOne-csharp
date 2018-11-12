namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class CouponItem : ICouponItem
    {
        public bool Add(CouponItemInfo couponItemInfo)
        {
            Parameters cmdParams = GetParameters(couponItemInfo);
            return DBHelper.ExecuteSql("INSERT INTO PE_CouponItem (CouponID, CouponNum, UserID, OrderID) VALUES (@CouponID, @CouponNum, @UserID, @OrderID)", cmdParams);
        }

        public bool AddUseTimes(string couponNum, int userId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CouponNum", DbType.String, couponNum);
            cmdParams.AddInParameter("@UserID", DbType.Int32, userId);
            return DBHelper.ExecuteSql("UPDATE PE_CouponItem SET Usetimes = Usetimes + 1 WHERE CouponNum = @CouponNum AND UserID = @UserID", cmdParams);
        }

        public bool Delete(string couponId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_CouponItem WHERE CouponID IN (" + DBHelper.ToValidId(couponId) + ")");
        }

        public CouponItemInfo GetCouponItemInfo(string couponNum, int userId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CouponNum", DbType.String, couponNum);
            cmdParams.AddInParameter("@UserID", DbType.Int32, userId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_CouponItem WHERE CouponNum = @CouponNum AND UserID = @UserID", cmdParams))
            {
                if (reader.Read())
                {
                    return GetItemInfoFromrdataReader(reader);
                }
                return new CouponItemInfo(true);
            }
        }

        public static CouponItemInfo GetItemInfoFromrdataReader(NullableDataReader dataReader)
        {
            CouponItemInfo info = new CouponItemInfo();
            info.Id = dataReader.GetInt32("ID");
            info.CouponId = dataReader.GetInt32("CouponID");
            info.CouponNum = dataReader.GetString("CouponNum");
            info.OrderId = dataReader.GetInt32("OrderID");
            info.UserId = dataReader.GetInt32("UserID");
            info.UseTimes = dataReader.GetInt32("UseTimes");
            return info;
        }

        private static Parameters GetParameters(CouponItemInfo couponItemInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@CouponID", DbType.Int32, couponItemInfo.CouponId);
            parameters.AddInParameter("@CouponNum", DbType.String, couponItemInfo.CouponNum);
            parameters.AddInParameter("@UserID", DbType.Int32, couponItemInfo.UserId);
            parameters.AddInParameter("@OrderID", DbType.Int32, couponItemInfo.OrderId);
            return parameters;
        }
    }
}


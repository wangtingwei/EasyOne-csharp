namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class Refund : IRefund
    {
        public bool Add(RefundInfo refundInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderID", DbType.Int32, refundInfo.OrderId);
            cmdParams.AddInParameter("@Money", DbType.Decimal, refundInfo.Money);
            cmdParams.AddInParameter("@HandlingCharge", DbType.Decimal, refundInfo.HandlingCharge);
            cmdParams.AddInParameter("@RefundType", DbType.Int32, refundInfo.RefundType);
            return DBHelper.ExecuteProc("PR_Shop_Refund_Add", cmdParams);
        }

        public RefundInfo GetByOrderId(int orderId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("OrderId", DbType.Int32, orderId);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Shop_Refund_GetByOrderId", cmdParams)))
            {
                if (reader.Read())
                {
                    RefundInfo info = new RefundInfo();
                    info.UserName = reader.GetString("UserName");
                    info.ClientId = reader.GetInt32("ClientID");
                    info.OrderNum = reader.GetString("OrderNum");
                    info.ClientName = reader.GetString("ClientName");
                    info.MoneyTotal = reader.GetDecimal("MoneyTotal");
                    info.MoneyReceipt = reader.GetDecimal("MoneyReceipt");
                    info.Email = reader.GetString("Email");
                    return info;
                }
                return new RefundInfo(true);
            }
        }
    }
}


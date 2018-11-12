namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class Remittance : IRemittance
    {
        public bool Add(RemittanceInfo remittanceInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderID", DbType.Int32, remittanceInfo.OrderId);
            cmdParams.AddInParameter("@Money", DbType.Decimal, remittanceInfo.Money);
            return DBHelper.ExecuteProc("PR_Shop_Remittance_Add", cmdParams);
        }

        public RemittanceInfo GetByOrderId(int orderId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("OrderId", DbType.Int32, orderId);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Shop_Remittance_GetByOrderId", cmdParams)))
            {
                if (reader.Read())
                {
                    RemittanceInfo info = new RemittanceInfo();
                    info.OrderId = reader.GetInt32("OrderID");
                    info.ClientId = reader.GetInt32("ClientID");
                    info.UserName = reader.GetString("UserName");
                    info.OrderNum = reader.GetString("OrderNum");
                    info.ClientName = reader.GetString("ClientName");
                    info.MoneyTotal = reader.GetDecimal("MoneyTotal");
                    info.MoneyReceipt = reader.GetDecimal("MoneyReceipt");
                    info.Email = reader.GetString("Email");
                    return info;
                }
                return new RemittanceInfo(true);
            }
        }
    }
}


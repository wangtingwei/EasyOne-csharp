namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class DeliverItem : IDeliverItem
    {
        private int m_TotalOfDeliverItem;

        public bool Add(DeliverItemInfo deliverItemInfo)
        {
            try
            {
                return (DBHelper.ExecuteNonQueryProc("PR_Shop_DeliverItem_Add", GetDeliverParms(deliverItemInfo)) > 0);
            }
            catch
            {
                return false;
            }
        }

        private static DeliverItemInfo DeliverItemFromrdr(NullableDataReader rdr)
        {
            DeliverItemInfo info = new DeliverItemInfo();
            info.DeliverId = rdr.GetInt32("DeliverID");
            info.OrderId = rdr.GetInt32("OrderID");
            info.DeliverDate = rdr.GetDateTime("DeliverDate");
            info.DeliverDirection = rdr.GetInt32("DeliverDirection");
            info.HandlerName = rdr.GetString("HandlerName");
            info.CourierId = rdr.GetInt32("CourierId");
            info.ExpressNumber = rdr.GetString("ExpressNumber");
            info.Inputer = rdr.GetString("Inputer");
            info.Remark = rdr.GetString("Remark");
            info.Received = rdr.GetBoolean("Received");
            info.Memo = rdr.GetString("Memo");
            return info;
        }

        private static DeliverItemInfo DetailDeliverItemFromrdr(NullableDataReader rdr)
        {
            DeliverItemInfo info = DeliverItemFromrdr(rdr);
            info.ClientId = rdr.GetInt32("ClientID");
            info.ClientName = rdr.GetString("ClientName");
            info.ContacterName = rdr.GetString("ContacterName");
            info.UserName = rdr.GetString("UserName");
            info.OrderNum = rdr.GetString("OrderNum");
            info.CourierId = rdr.GetInt32("CourierId");
            return info;
        }

        public DeliverItemInfo GetDeliverItemById(int deliverItemId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT D.*, O.OrderNum, O.UserName, O.ContacterName, O.ClientID, C.ShortedForm AS ClientName FROM PE_DeliverItem D LEFT JOIN (PE_Orders O LEFT JOIN PE_Client C ON O.ClientID = C.ClientID) ON D.OrderID = O.OrderID WHERE D.DeliverID = @DeliverID", new Parameters("@DeliverID", DbType.Int32, deliverItemId)))
            {
                if (reader.Read())
                {
                    return DetailDeliverItemFromrdr(reader);
                }
                return new DeliverItemInfo(true);
            }
        }

        public DeliverItemInfo GetDeliverItemByOrderId(int orderId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("OrderId", DbType.Int32, orderId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Shop_DeliverItem_GetByOrderId", cmdParams))
            {
                if (reader.Read())
                {
                    DeliverItemInfo info = new DeliverItemInfo();
                    info.UserName = reader.GetString("UserName");
                    info.OrderNum = reader.GetString("OrderNum");
                    info.ClientName = reader.GetString("ClientName");
                    info.MoneyTotal = reader.GetDecimal("MoneyTotal");
                    info.MoneyReceipt = reader.GetDecimal("MoneyReceipt");
                    info.DeliverTypeName = reader.GetString("DeliverTypeName");
                    info.ContacterName = reader.GetString("ContacterName");
                    info.Email = reader.GetString("Email");
                    return info;
                }
                return new DeliverItemInfo(true);
            }
        }

        public DeliverItemInfo GetDeliverItemByOrderId(int orderId, int deliverDirection)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderId", DbType.Int32, orderId);
            cmdParams.AddInParameter("@DeliverDirection", DbType.Int32, deliverDirection);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_DeliverItem WHERE OrderId = @OrderId AND DeliverDirection = @DeliverDirection", cmdParams))
            {
                if (reader.Read())
                {
                    return DeliverItemFromrdr(reader);
                }
                return new DeliverItemInfo(true);
            }
        }

        private static Parameters GetDeliverParms(DeliverItemInfo deliverItemInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@OrderID", DbType.Int32, deliverItemInfo.OrderId);
            parameters.AddInParameter("@DeliverDate", DbType.DateTime, deliverItemInfo.DeliverDate);
            parameters.AddInParameter("@DeliverDirection", DbType.Int32, deliverItemInfo.DeliverDirection);
            parameters.AddInParameter("@HandlerName", DbType.String, deliverItemInfo.HandlerName);
            parameters.AddInParameter("@CourierId", DbType.Int32, deliverItemInfo.CourierId);
            parameters.AddInParameter("@ExpressNumber", DbType.String, deliverItemInfo.ExpressNumber);
            parameters.AddInParameter("@Inputer", DbType.String, deliverItemInfo.Inputer);
            parameters.AddInParameter("@Remark", DbType.String, deliverItemInfo.Remark);
            parameters.AddInParameter("@Memo", DbType.String, deliverItemInfo.Memo);
            return parameters;
        }

        public ArrayList GetExpressCompannyList()
        {
            ArrayList list = new ArrayList();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Shop_DeliverItem_GetExpressCompannyList"))
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader.GetString("ExpressCompany")))
                    {
                        list.Add(reader.GetString("ExpressCompany"));
                    }
                }
            }
            return list;
        }

        public IList<DeliverItemInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int quickSearch)
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
            database.SetParameterValue(storedProcCommand, "@SortColumn", "DeliverID");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "D.*, O.OrderNum, O.UserName, O.ContacterName, O.ClientID, C.ShortedForm AS ClientName, Co.ShortName");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_DeliverItem D LEFT JOIN (PE_Orders O LEFT JOIN PE_Client C ON O.ClientID = C.ClientID) ON D.OrderID = O.OrderID Left JOIN PE_Courier Co ON D.CourierId = Co.CourierId");
            switch (quickSearch)
            {
                case 1:
                    database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(dd, D.DeliverDate, GETDATE()) < 10");
                    break;

                case 2:
                    database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(m, D.DeliverDate, GETDATE()) < 1");
                    break;

                case 3:
                    database.SetParameterValue(storedProcCommand, "@Filter", "D.DeliverDirection = 1");
                    break;

                case 4:
                    database.SetParameterValue(storedProcCommand, "@Filter", "D.DeliverDirection = 2");
                    break;

                default:
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        switch (searchType)
                        {
                            case 1:
                                database.SetParameterValue(storedProcCommand, "@Filter", "C.ShortedForm LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                                break;

                            case 2:
                                database.SetParameterValue(storedProcCommand, "@Filter", "O.ContacterName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                                break;

                            case 3:
                                database.SetParameterValue(storedProcCommand, "@Filter", "O.UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                                break;

                            case 4:
                                database.SetParameterValue(storedProcCommand, "@Filter", "Co.ShortName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                                break;

                            case 5:
                                database.SetParameterValue(storedProcCommand, "@Filter", "Co.ShortName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                                break;

                            case 6:
                                database.SetParameterValue(storedProcCommand, "@Filter", "D.HandlerName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                                break;

                            case 7:
                                database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(dd, D.DeliverDate, '" + keyword + "') = 0");
                                break;

                            case 8:
                                database.SetParameterValue(storedProcCommand, "@Filter", "D.OrderID = " + DBHelper.ToNumber(keyword));
                                break;
                        }
                    }
                    else
                    {
                        database.SetParameterValue(storedProcCommand, "@Filter", "");
                    }
                    break;
            }
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            IList<DeliverItemInfo> list = new List<DeliverItemInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(DetailDeliverItemFromrdr(reader));
                }
            }
            this.m_TotalOfDeliverItem = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetTotalOfDeliverItem()
        {
            return this.m_TotalOfDeliverItem;
        }

        public void UpdateReceive(int orderId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderID", DbType.Int32, orderId);
            DBHelper.ExecuteSql("UPDATE PE_DeliverItem SET Received = 1 WHERE OrderID = @OrderID AND Received = 0", cmdParams);
        }
    }
}


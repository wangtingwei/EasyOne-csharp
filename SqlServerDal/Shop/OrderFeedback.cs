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

    public class OrderFeedback : IOrderFeedback
    {
        private int m_TotalOfOrderFeedback;
        private int m_TotalOfOrderFeedbackDetail;

        public bool Add(OrderFeedbackInfo orderFeedbackInfo)
        {
            Parameters cmdParams = GetParameters(orderFeedbackInfo);
            return DBHelper.ExecuteSql("INSERT INTO PE_OrderFeedback (OrderID, UserName, Content, WriteTime, ReplyName, ReplyContent, ReplyTime) VALUES (@OrderID, @UserName, @Content, @WriteTime, @ReplyName, @ReplyContent, @ReplyTime)", cmdParams);
        }

        public bool Delete(string id)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_OrderFeedback WHERE ID IN (" + id + ")");
        }

        public IList<OrderFeedbackInfo> GetList(int orderId)
        {
            IList<OrderFeedbackInfo> list = new List<OrderFeedbackInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_OrderFeedback WHERE OrderID = @OrderID ORDER BY ID DESC", new Parameters("@orderId", DbType.Int32, orderId)))
            {
                while (reader.Read())
                {
                    list.Add(GetOrderFeedbackInfoFromrdr(reader));
                }
            }
            return list;
        }

        public IList<OrderFeedbackInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<OrderFeedbackInfo> list = new List<OrderFeedbackInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_OrderFeedback");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(GetOrderFeedbackInfoFromrdr(reader));
                }
            }
            this.m_TotalOfOrderFeedback = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<OrderFeedbackDetailInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<OrderFeedbackDetailInfo> list = new List<OrderFeedbackDetailInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            string str = "1 = 1";
            if (searchType > 10)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    switch (searchType)
                    {
                        case 11:
                            str = str + " AND O.OrderNum LIKE '%" + keyword + "%'";
                            goto Label_00B9;

                        case 12:
                            str = str + " AND O.UserName LIKE '%" + keyword + "%'";
                            goto Label_00B9;

                        case 13:
                            str = str + " AND C.ClientName LIKE '%" + keyword + "%'";
                            goto Label_00B9;
                    }
                }
            }
            else
            {
                switch (searchType)
                {
                    case 1:
                        str = str + " AND (DATALENGTH(B.ReplyContent) = 0) OR (B.ReplyContent IS NULL)";
                        goto Label_00B9;

                    case 2:
                        str = str + " AND (DATALENGTH(B.ReplyContent) <> 0) AND (B.ReplyContent IS NOT NULL)";
                        goto Label_00B9;
                }
            }
        Label_00B9:
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "B.*, O.OrderNum, C.ShortedForm AS ClientName");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_OrderFeedback B INNER JOIN PE_Orders O ON B.OrderID = O.OrderID LEFT JOIN PE_Client C ON O.ClientID = C.ClientID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    OrderFeedbackDetailInfo item = new OrderFeedbackDetailInfo();
                    item.OrderFeedbackInfo = GetOrderFeedbackInfoFromrdr(reader);
                    item.ClientName = reader.GetString("ClientName");
                    item.OrderNum = reader.GetString("OrderNum");
                    list.Add(item);
                }
            }
            this.m_TotalOfOrderFeedbackDetail = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public OrderFeedbackInfo GetOrderFeedbackById(int id)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_OrderFeedback WHERE ID = @ID", new Parameters("@ID", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    return GetOrderFeedbackInfoFromrdr(reader);
                }
                return new OrderFeedbackInfo(true);
            }
        }

        private static OrderFeedbackInfo GetOrderFeedbackInfoFromrdr(NullableDataReader rdr)
        {
            OrderFeedbackInfo info = new OrderFeedbackInfo();
            info.Id = rdr.GetInt32("ID");
            info.OrderId = rdr.GetInt32("OrderID");
            info.UserName = rdr.GetString("UserName");
            info.Content = rdr.GetString("Content");
            info.WriteTime = rdr.GetDateTime("WriteTime");
            info.ReplyName = rdr.GetString("ReplyName");
            info.ReplyContent = rdr.GetString("ReplyContent");
            info.ReplyTime = rdr.GetDateTime("ReplyTime");
            return info;
        }

        private static Parameters GetParameters(OrderFeedbackInfo orderFeedbackInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@OrderID", DbType.Int32, orderFeedbackInfo.OrderId);
            parameters.AddInParameter("@UserName", DbType.String, orderFeedbackInfo.UserName);
            parameters.AddInParameter("@Content", DbType.String, orderFeedbackInfo.Content);
            parameters.AddInParameter("@WriteTime", DbType.DateTime, orderFeedbackInfo.WriteTime);
            parameters.AddInParameter("@ReplyName", DbType.String, orderFeedbackInfo.ReplyName);
            parameters.AddInParameter("@ReplyContent", DbType.String, orderFeedbackInfo.ReplyContent);
            parameters.AddInParameter("@ReplyTime", DbType.DateTime, orderFeedbackInfo.ReplyTime);
            return parameters;
        }

        public int GetTotalOfOrderFeedback()
        {
            return this.m_TotalOfOrderFeedback;
        }

        public int TotalOfOrderFeedbackDetail()
        {
            return this.m_TotalOfOrderFeedbackDetail;
        }

        public bool Update(OrderFeedbackInfo orderFeedbackInfo)
        {
            Parameters cmdParams = GetParameters(orderFeedbackInfo);
            cmdParams.AddInParameter("@ID", DbType.Int32, orderFeedbackInfo.Id);
            return DBHelper.ExecuteSql("UPDATE PE_OrderFeedback SET OrderID = @OrderID, UserName = @UserName, Content = @Content, WriteTime = @WriteTime, ReplyName = @ReplyName, ReplyContent = @ReplyContent, ReplyTime = @ReplyTime WHERE ID = @ID ", cmdParams);
        }
    }
}


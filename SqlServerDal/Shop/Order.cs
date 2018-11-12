namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Order : IOrder
    {
        private string m_TableTotal;
        private int m_TotalOfOrder;
        private static string s_ThisSearchCondition;

        public bool Add(OrderInfo orderInfo)
        {
            try
            {
                return (DBHelper.ExecuteNonQueryProc("PR_Shop_Order_Add", GetOrderParameter(orderInfo)) > 0);
            }
            catch
            {
                return false;
            }
        }

        public bool CancelConfirm(int orderId)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Orders SET Status = 0 WHERE OrderID = @OrderID", new Parameters("@OrderID", DbType.Int32, orderId));
        }

        private static string ComplexSearch(string keyword)
        {
            StringBuilder output = new StringBuilder("");
            output.Append("1 = 1");
            if (!string.IsNullOrEmpty(keyword))
            {
                string[] strArray = keyword.Split(new char[] { '|' });
                int num = Convert.ToInt32(strArray[0]);
                int num2 = Convert.ToInt32(strArray[1]);
                string str = strArray[2];
                string str2 = strArray[3];
                decimal num3 = Convert.ToDecimal(strArray[4]);
                decimal num4 = Convert.ToDecimal(strArray[5]);
                int num5 = Convert.ToInt32(strArray[6]);
                int payStatus = Convert.ToInt32(strArray[7]);
                int num7 = Convert.ToInt32(strArray[8]);
                string str3 = strArray[9];
                string str4 = strArray[10];
                string str5 = strArray[11];
                string str6 = strArray[12];
                string str7 = strArray[13];
                string str8 = strArray[14];
                string str9 = strArray[15];
                string str10 = strArray[0x10];
                string str11 = strArray[0x11];
                string str12 = strArray[0x12];
                int num8 = Convert.ToInt32(strArray[0x13]);
                string str13 = strArray[20];
                string str14 = strArray[0x15];
                string str15 = strArray[0x16];
                string str16 = strArray[0x17];
                int num9 = Convert.ToInt32(strArray[0x18]);
                int num10 = Convert.ToInt32(strArray[0x19]);
                if (num > 0)
                {
                    output.Append(" AND O.OrderID >= " + num);
                }
                if (num2 > 0)
                {
                    output.Append(" AND O.OrderID <= " + num2);
                }
                if (!string.IsNullOrEmpty(str))
                {
                    output.Append(" AND O.InputTime>='" + str.Replace("'", "") + "'");
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    output.Append(" AND O.InputTime<='" + str2.Replace("'", "") + "'");
                }
                if (num3 > 0M)
                {
                    output.Append(" AND O.MoneyTotal>='" + num3 + "'");
                }
                if (num4 > 0M)
                {
                    output.Append(" AND O.MoneyTotal<='" + num4 + "'");
                }
                if (num5 >= 0)
                {
                    output.Append(" AND O.Status = " + num5);
                }
                GetPayStatusSql(output, payStatus);
                if (num7 >= 0)
                {
                    output.Append(" AND O.DeliverStatus = " + num7);
                }
                if (!string.IsNullOrEmpty(str3))
                {
                    output.Append(" AND O.OrderNum = '" + DBHelper.FilterBadChar(str3) + "' ");
                }
                if (!string.IsNullOrEmpty(str4))
                {
                    output.Append(" AND C.ShortedForm LIKE '%" + DBHelper.FilterBadChar(str4) + "%' ");
                }
                if (!string.IsNullOrEmpty(str5))
                {
                    output.Append(" AND O.UserName LIKE '%" + DBHelper.FilterBadChar(str5) + "%' ");
                }
                if (!string.IsNullOrEmpty(str6))
                {
                    output.Append(" AND O.AgentName = '" + DBHelper.FilterBadChar(str6) + "' ");
                }
                if (!string.IsNullOrEmpty(str7))
                {
                    output.Append(" AND O.ContacterName LIKE '%" + DBHelper.FilterBadChar(str7) + "%' ");
                }
                if (!string.IsNullOrEmpty(str8))
                {
                    output.Append(" AND O.Address LIKE '%" + str8.Replace("'", "") + "%' ");
                }
                if (!string.IsNullOrEmpty(str9))
                {
                    output.Append(" AND O.Phone LIKE '%" + str9.Replace("'", "") + "%' ");
                }
                if (!string.IsNullOrEmpty(str10))
                {
                    output.Append(" AND O.Mobile LIKE '%" + str10.Replace("'", "") + "%' ");
                }
                if (!string.IsNullOrEmpty(str11))
                {
                    output.Append(" AND O.Remark LIKE '%" + str11.Replace("'", "") + "%' ");
                }
                if (!string.IsNullOrEmpty(str12))
                {
                    output.Append(" AND P.ProductName LIKE '%" + DBHelper.FilterBadChar(str12) + "%' ");
                }
                if (num8 >= 0)
                {
                    output.Append(" AND O.OrderType = " + num8);
                }
                if (!string.IsNullOrEmpty(str13))
                {
                    output.Append(" AND O.Functionary = '" + str13 + "'");
                }
                if (!string.IsNullOrEmpty(str14))
                {
                    output.Append(" AND O.email LIKE '%" + DBHelper.FilterBadChar(str14) + "%' ");
                }
                if (!string.IsNullOrEmpty(str15))
                {
                    output.Append(" AND O.InvoiceContent LIKE '%" + DBHelper.FilterBadChar(str15) + "%' ");
                }
                if (!string.IsNullOrEmpty(str16))
                {
                    output.Append(" AND O.Memo LIKE '%" + DBHelper.FilterBadChar(str16) + "%' ");
                }
                if (num9 > 0)
                {
                    output.Append(" AND O.PaymentType = " + num9 + " ");
                }
                if (num10 > 0)
                {
                    output.Append(" AND O.DeliverType = " + num10 + " ");
                }
            }
            return output.ToString();
        }

        public int Confirm(int orderId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Order_Confirm");
            database.AddInParameter(storedProcCommand, "@orderId", DbType.Int32, orderId);
            database.AddParameter(storedProcCommand, "@RETURN_VALUE", DbType.String, ParameterDirection.ReturnValue, "", DataRowVersion.Current, null);
            database.ExecuteNonQuery(storedProcCommand);
            return (int) storedProcCommand.Parameters["@RETURN_VALUE"].Value;
        }

        public int CountBuyNum(string userName, int productId)
        {
            int num = 0;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT SUM(Amount) AS CountNum FROM [PE_OrderItem] WHERE OrderId IN(SELECT OrderId FROM [PE_Orders] WHERE UserName = @UserName AND ProductID = @ProductId)", cmdParams))
            {
                if (reader.Read())
                {
                    num = reader.GetInt32("CountNum");
                }
            }
            return num;
        }

        public int CountByNoConsignment()
        {
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM  [dbo].[PE_Orders] WHERE MoneyTotal <= MoneyReceipt AND DeliverStatus<1  AND Status != 3"));
        }

        public int CountByOrderStatus(OrderStatus status)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, status);
            return (int) DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM  [dbo].[PE_Orders] o WHERE o.Status = @Status", cmdParams);
        }

        public string Delete(string orderId)
        {
            bool flag;
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Order_Delete");
            database.AddInParameter(storedProcCommand, "@OrderId", DbType.String, orderId);
            database.AddOutParameter(storedProcCommand, "@ErrOrderNum", DbType.String, 0xfa0);
            try
            {
                flag = database.ExecuteNonQuery(storedProcCommand) > 0;
            }
            catch
            {
                flag = false;
            }
            string str2 = Convert.IsDBNull(database.GetParameterValue(storedProcCommand, "@ErrOrderNum")) ? "" : Convert.ToString(database.GetParameterValue(storedProcCommand, "@ErrOrderNum"));
            if (string.IsNullOrEmpty(str2))
            {
                if (flag)
                {
                    return "ok";
                }
                return "notOk";
            }
            return str2;
        }

        public bool DoDownload(int orderId, bool enableDownload)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderID", DbType.Int32, orderId);
            cmdParams.AddInParameter("@EnableDownload", DbType.Boolean, enableDownload);
            return DBHelper.ExecuteSql("UPDATE PE_Orders SET EnableDownload = @EnableDownload WHERE OrderID = @OrderID", cmdParams);
        }

        public OrderInfo GetAnonymousOrderInfo(string orderNo, string contactName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderNo", DbType.String, orderNo);
            cmdParams.AddInParameter("@ContacterName", DbType.String, contactName);
            string strSql = "SELECT O.*, C.ShortedForm AS ClientName FROM PE_Orders O LEFT JOIN PE_Client C ON O.ClientId = C.ClientID WHERE O.UserName = '' AND O.OrderNum = @OrderNo AND O.ContacterName = @ContacterName";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                return GetOrderInfoByReader(reader);
            }
        }

        public IList<UserOrderCommonInfo> GetCardList(string userName)
        {
            IList<UserOrderCommonInfo> list = new List<UserOrderCommonInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT I.* FROM PE_Orders O INNER JOIN PE_OrderItem I ON O.OrderID = I.OrderID WHERE I.ProductCharacter&" + 8 + " >0 AND O.UserName = @UserName", new Parameters("@UserName", DbType.String, userName)))
            {
                while (reader.Read())
                {
                    list.Add(GetUserOrderCommonInfo(reader));
                }
            }
            return list;
        }

        private static ArrayList GetDataofExecute(string sqlString, Parameters parms)
        {
            ArrayList list = new ArrayList();
            using (NullableDataReader reader = (parms == null) ? DBHelper.ExecuteReaderSql(sqlString) : DBHelper.ExecuteReaderSql(sqlString, parms))
            {
                if (reader.Read())
                {
                    list.Add(reader[0]);
                    list.Add(reader[1]);
                }
            }
            return list;
        }

        public IList<UserOrderCommonInfo> GetDownList(string userName)
        {
            IList<UserOrderCommonInfo> list = new List<UserOrderCommonInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT I.* FROM PE_Orders O INNER JOIN PE_OrderItem I ON O.OrderID = I.OrderID WHERE I.ProductCharacter&" + 4 + " >0 AND O.UserName = @UserName AND EnableDownload = 1", new Parameters("@UserName", DbType.String, userName)))
            {
                while (reader.Read())
                {
                    list.Add(GetUserOrderCommonInfo(reader));
                }
            }
            return list;
        }

        public UserOrderCommonInfo GetDownloadInfo(string userName, int orderItemId)
        {
            UserOrderCommonInfo info = new UserOrderCommonInfo();
            string strSql = "SELECT I.* FROM PE_Orders O INNER JOIN PE_OrderItem I ON O.OrderID = I.OrderID WHERE O.UserName = @UserName AND EnableDownload = 1 AND I.ItemID = @ItemID";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@ItemID", DbType.Int32, orderItemId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return GetUserOrderCommonInfo(reader);
                }
                return new UserOrderCommonInfo(true);
            }
        }

        private static string GetFilterString(string searchType, string field, string keyword)
        {
            string str = "";
            switch (searchType)
            {
                case "0":
                    str = "1 = 1";
                    break;

                case "1":
                    str = "DATEDIFF(dd, O.InputTime, GETDATE()) < 1";
                    break;

                case "2":
                    str = "DATEDIFF(dd, O.InputTime, GETDATE()) < 10";
                    break;

                case "3":
                    str = "DATEDIFF(m, O.InputTime, GETDATE()) < 1";
                    break;

                case "4":
                    str = "O.Status = 0";
                    s_ThisSearchCondition = "Status = 0";
                    break;

                case "5":
                    str = "O.MoneyTotal > 0 AND O.MoneyReceipt = 0 AND O.Status != 3";
                    break;

                case "6":
                    str = "O.MoneyTotal > O.MoneyReceipt AND O.Status != 3";
                    break;

                case "7":
                    str = "O.MoneyTotal <= O.MoneyReceipt AND O.DeliverStatus<1  AND O.Status != 3";
                    break;

                case "8":
                    str = "O.MoneyTotal <= O.MoneyReceipt AND O.DeliverStatus = 1";
                    break;

                case "9":
                    str = "O.NeedInvoice = 1 AND O.Invoiced = 0 AND O.Status != 3";
                    break;

                case "11":
                    str = "O.MoneyTotal <= O.MoneyReceipt AND O.DeliverStatus = 2 AND O.Status > 0 AND O.Status<2";
                    break;

                case "12":
                    str = "O.Status = 2";
                    break;

                case "13":
                    str = "O.DeliverStatus = 1";
                    break;

                case "14":
                    str = "O.DeliverStatus = 2";
                    break;

                case "15":
                    str = "O.ClientID =" + DBHelper.ToNumber(keyword);
                    break;

                case "16":
                    str = "O.Status = 3";
                    break;

                case "17":
                    str = "O.Status = 4";
                    break;

                case "18":
                    str = "O.Functionary = '" + DBHelper.FilterBadChar(PEContext.Current.Admin.AdminName) + "'";
                    break;

                case "19":
                    str = "O.UserName = '" + DBHelper.FilterBadChar(keyword) + "'";
                    break;

                case "10":
                    str = HighLevelSearch(field, keyword);
                    break;

                case "20":
                    str = ComplexSearch(keyword);
                    break;

                default:
                    str = "1 = 1";
                    break;
            }
            s_ThisSearchCondition = str;
            return str;
        }

        public string GetLastFunctionary()
        {
            string str = "";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 1 Functionary FROM PE_Orders WHERE Functionary<>''ORDER BY OrderID DESC"))
            {
                if (reader.Read())
                {
                    str = reader.GetString("Functionary");
                }
            }
            return str;
        }

        public IList<OrderInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword, string action)
        {
            string str;
            string str2;
            Database database = DatabaseFactory.CreateDatabase();
            IList<OrderInfo> list = new List<OrderInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "O.OrderID");
            this.GetsqlStr(searchType, field, keyword, action, out str, out str2);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, GetFilterString(searchType, field, keyword));
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(OrderFromrdr(reader, action));
                }
            }
            this.m_TotalOfOrder = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IDictionary<int, string> GetListByUserName(string userName)
        {
            string str;
            IDictionary<int, string> dictionary = new Dictionary<int, string>();
            if (string.IsNullOrEmpty(userName))
            {
                str = "SELECT TOP 50 OrderID, OrderNum FROM PE_Orders WHERE Status = 0 ORDER BY OrderID DESC";
            }
            else
            {
                str = "SELECT TOP 50 OrderID, OrderNum FROM PE_Orders WHERE Status = 0 AND UserName = @UserName ORDER BY OrderID DESC";
            }
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderSql(str, new Parameters("@UserName", DbType.String, userName))))
            {
                while (reader.Read())
                {
                    dictionary.Add(reader.GetInt32("OrderID"), reader.GetString("OrderNum"));
                }
            }
            return dictionary;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Orders", "OrderID");
        }

        public OrderInfo GetMyOrderById(int orderId, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderId", DbType.Int32, orderId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            string strSql = "SELECT O.*, C.ShortedForm AS ClientName \r\n                           FROM PE_Orders O LEFT JOIN PE_Client C ON O.ClientID = C.ClientID \r\n                           WHERE OrderID = @OrderId AND (UserName = @UserName OR AgentName = @UserName)";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                return GetOrderInfoByReader(reader);
            }
        }

        public OrderInfo GetOrderById(int orderId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderId", DbType.Int32, orderId);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Shop_Order_GetByOrderId", cmdParams)))
            {
                return GetOrderInfoByReader(reader);
            }
        }

        public OrderInfo GetOrderByOrderNum(string orderNum)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT O.*, C.ShortedForm AS ClientName FROM PE_Orders O LEFT JOIN PE_Client C ON O.ClientID = C.ClientID WHERE OrderNum = @OrderNum", new Parameters("@OrderNum", DbType.String, orderNum)))
            {
                return GetOrderInfoByReader(reader);
            }
        }

        private static OrderInfo GetOrderInfoByReader(NullableDataReader rdr)
        {
            if (rdr.Read())
            {
                return OrderFromrdr(rdr, "");
            }
            return new OrderInfo(true);
        }

        private static Parameters GetOrderParameter(OrderInfo orderInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@OrderID", DbType.Int32, orderInfo.OrderId);
            parameters.AddInParameter("@OrderNum", DbType.String, orderInfo.OrderNum);
            parameters.AddInParameter("@UserName", DbType.String, orderInfo.UserName);
            parameters.AddInParameter("@AgentName", DbType.String, orderInfo.AgentName);
            parameters.AddInParameter("@Functionary", DbType.String, orderInfo.Functionary);
            parameters.AddInParameter("@ClientID", DbType.Int32, orderInfo.ClientId);
            parameters.AddInParameter("@MoneyTotal", DbType.Currency, orderInfo.MoneyTotal);
            parameters.AddInParameter("@MoneyGoods", DbType.Currency, orderInfo.MoneyGoods);
            parameters.AddInParameter("@NeedInvoice", DbType.Boolean, orderInfo.NeedInvoice);
            parameters.AddInParameter("@InvoiceContent", DbType.String, orderInfo.InvoiceContent);
            parameters.AddInParameter("@Invoiced", DbType.Boolean, orderInfo.Invoiced);
            parameters.AddInParameter("@Remark", DbType.String, orderInfo.Remark);
            parameters.AddInParameter("@MoneyReceipt", DbType.Currency, orderInfo.MoneyReceipt);
            parameters.AddInParameter("@BeginDate", DbType.DateTime, orderInfo.BeginDate);
            parameters.AddInParameter("@InputTime", DbType.DateTime, orderInfo.InputTime);
            parameters.AddInParameter("@ContacterName", DbType.String, orderInfo.ContacterName);
            parameters.AddInParameter("@Address", DbType.String, orderInfo.Address);
            parameters.AddInParameter("@ZipCode", DbType.String, orderInfo.ZipCode);
            parameters.AddInParameter("@Mobile", DbType.String, orderInfo.Mobile);
            parameters.AddInParameter("@Phone", DbType.String, orderInfo.Phone);
            parameters.AddInParameter("@Email", DbType.String, orderInfo.Email);
            parameters.AddInParameter("@PaymentType", DbType.Int32, orderInfo.PaymentType);
            parameters.AddInParameter("@DeliverType", DbType.Int32, orderInfo.DeliverType);
            parameters.AddInParameter("@Status", DbType.Int32, (int) orderInfo.Status);
            parameters.AddInParameter("@DeliverStatus", DbType.Int32, (int) orderInfo.DeliverStatus);
            parameters.AddInParameter("@EnableDownload", DbType.Boolean, orderInfo.EnableDownload);
            parameters.AddInParameter("@PresentMoney", DbType.Currency, orderInfo.PresentMoney);
            parameters.AddInParameter("@PresentPoint", DbType.Int32, orderInfo.PresentPoint);
            parameters.AddInParameter("@PresentExp", DbType.Int32, orderInfo.PresentExp);
            parameters.AddInParameter("@Discount_Payment", DbType.Double, orderInfo.DiscountPayment);
            parameters.AddInParameter("@Charge_Deliver", DbType.Currency, orderInfo.ChargeDeliver);
            parameters.AddInParameter("@Memo", DbType.String, orderInfo.Memo);
            parameters.AddInParameter("@OutOfStockProject", DbType.Int32, (int) orderInfo.OutOfStockProject);
            parameters.AddInParameter("@OrderType", DbType.Int32, orderInfo.OrderType);
            parameters.AddInParameter("@CouponID", DbType.Int32, orderInfo.CouponId);
            parameters.AddInParameter("@DeliveryTime", DbType.String, orderInfo.DeliveryTime);
            return parameters;
        }

        private static void GetPayStatusSql(StringBuilder output, int payStatus)
        {
            if (payStatus >= 0)
            {
                switch (payStatus)
                {
                    case 0:
                        output.Append(" AND O.MoneyTotal > 0 AND O.MoneyReceipt = 0");
                        return;

                    case 1:
                        output.Append(" AND O.MoneyTotal > O.MoneyReceipt AND O.MoneyReceipt > 0");
                        return;

                    case 2:
                        output.Append(" AND O.MoneyTotal <= O.MoneyReceipt AND O.MoneyReceipt > 0");
                        return;

                    default:
                        return;
                }
            }
        }

        private void GetsqlStr(string searchType, string field, string keyword, string action, out string sqlStrColumn, out string sqlTableName)
        {
            string str = "PE_CommonProduct P INNER JOIN (PE_OrderItem I INNER JOIN (PE_Orders O LEFT JOIN PE_Client C On O.ClientID = C.ClientID) ON I.OrderID = O.OrderID) ON P.ProductID = I.ProductID AND P.TableName = I.TableName";
            string str2 = "PE_Contacter T INNER JOIN (PE_Users U INNER JOIN (PE_Orders O LEFT JOIN PE_Client C On C.ClientID = O.ClientID) ON U.UserName = O.UserName) ON T.ClientID = U.ClientID";
            string str3 = "PE_Orders O LEFT JOIN PE_Client C ON O.ClientID = C.ClientID";
            if (action == "outExcel")
            {
                if ((((searchType == "10") && (field == "ProductName")) && !string.IsNullOrEmpty(keyword)) || ((searchType == "20") && NullProductName(keyword)))
                {
                    sqlTableName = str;
                }
                else if (((searchType == "10") && (field == "QQ")) && !string.IsNullOrEmpty(keyword))
                {
                    sqlTableName = str2;
                }
                else
                {
                    sqlTableName = str3;
                }
            }
            else if (((searchType == "10") && (field == "ProductName")) || ((searchType == "20") && NullProductName(keyword)))
            {
                sqlTableName = str;
            }
            else if (((searchType == "10") && (field == "QQ")) && !string.IsNullOrEmpty(keyword))
            {
                sqlTableName = str2;
            }
            else
            {
                sqlTableName = str3;
            }
            sqlStrColumn = "O.*, ISNULL(C.ShortedForm, '') AS ClientName";
            this.m_TableTotal = sqlTableName;
        }

        public ArrayList GetTotalofMoneyAndReceipt()
        {
            string sqlString = "SELECT ISNULL(SUM(MoneyTotal), 0) , ISNULL(SUM(MoneyReceipt), 0) FROM PE_Orders";
            return GetDataofExecute(sqlString, null);
        }

        public ArrayList GetTotalofMoneyAndReceiptByAgentName(string agentName)
        {
            string sqlString = "SELECT ISNULL(SUM(MoneyTotal), 0) , ISNULL(SUM(MoneyReceipt), 0) FROM PE_Orders WHERE AgentName = @AgentName";
            return GetDataofExecute(sqlString, new Parameters("@AgentName", DbType.String, agentName));
        }

        public ArrayList GetTotalofMoneyAndReceiptByUserName(string userName)
        {
            string sqlString = "SELECT ISNULL(SUM(MoneyTotal), 0) , ISNULL(SUM(MoneyReceipt), 0) FROM PE_Orders WHERE UserName = @UserName";
            return GetDataofExecute(sqlString, new Parameters("@UserName", DbType.String, userName));
        }

        public int GetTotalOfOrder()
        {
            return this.m_TotalOfOrder;
        }

        public ArrayList GetTotalofthisMoneyAndReceipt(string field)
        {
            ArrayList list = new ArrayList();
            if (string.IsNullOrEmpty(s_ThisSearchCondition))
            {
                list.Add("search error");
                list.Add("search error");
                return list;
            }
            string tableTotal = this.m_TableTotal;
            string str3 = field;
            if (str3 != null)
            {
                if (!(str3 == "ClientName"))
                {
                    if (str3 == "ProductName")
                    {
                        tableTotal = "PE_CommonProduct P INNER JOIN (PE_OrderItem I INNER JOIN PE_Orders O ON I.OrderID = O.OrderID) ON P.ProductID = I.ProductID AND P.TableName = I.TableName";
                    }
                    else if (str3 == "QQ")
                    {
                        tableTotal = "PE_Contacter T INNER JOIN PE_Orders O ON T.ClientID = O.ClientID";
                    }
                }
                else
                {
                    tableTotal = "PE_Orders O INNER JOIN PE_Client C ON O.ClientID = C.ClientID";
                }
            }
            return GetDataofExecute("SELECT ISNULL(SUM(O.MoneyTotal), 0), ISNULL(SUM(O.MoneyReceipt), 0) FROM " + DBHelper.FilterBadChar(tableTotal) + " WHERE " + s_ThisSearchCondition, null);
        }

        private static Parameters GetUpdateParms(OrderInfo orderInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@OrderID", DbType.Int32, orderInfo.OrderId);
            parameters.AddInParameter("@ContacterName", DbType.String, orderInfo.ContacterName);
            parameters.AddInParameter("@UserName", DbType.String, orderInfo.UserName);
            parameters.AddInParameter("@AgentName", DbType.String, orderInfo.AgentName);
            parameters.AddInParameter("@Functionary", DbType.String, orderInfo.Functionary);
            parameters.AddInParameter("@MoneyTotal", DbType.Currency, orderInfo.MoneyTotal);
            parameters.AddInParameter("@MoneyGoods", DbType.Currency, orderInfo.MoneyGoods);
            parameters.AddInParameter("@NeedInvoice", DbType.Boolean, orderInfo.NeedInvoice);
            parameters.AddInParameter("@InvoiceContent", DbType.String, orderInfo.InvoiceContent);
            parameters.AddInParameter("@Remark", DbType.String, orderInfo.Remark);
            parameters.AddInParameter("@BeginDate", DbType.DateTime, orderInfo.BeginDate);
            parameters.AddInParameter("@Address", DbType.String, orderInfo.Address);
            parameters.AddInParameter("@ZipCode", DbType.String, orderInfo.ZipCode);
            parameters.AddInParameter("@Mobile", DbType.String, orderInfo.Mobile);
            parameters.AddInParameter("@Phone", DbType.String, orderInfo.Phone);
            parameters.AddInParameter("@Email", DbType.String, orderInfo.Email);
            parameters.AddInParameter("@PaymentType", DbType.Int32, orderInfo.PaymentType);
            parameters.AddInParameter("@DeliverType", DbType.Int32, orderInfo.DeliverType);
            parameters.AddInParameter("@Discount_Payment", DbType.Double, orderInfo.DiscountPayment);
            parameters.AddInParameter("@Charge_Deliver", DbType.Currency, orderInfo.ChargeDeliver);
            parameters.AddInParameter("@Status", DbType.Int32, (int) orderInfo.Status);
            parameters.AddInParameter("@PresentMoney", DbType.Currency, orderInfo.PresentMoney);
            parameters.AddInParameter("@PresentPoint", DbType.Int32, orderInfo.PresentPoint);
            parameters.AddInParameter("@PresentExp", DbType.Int32, orderInfo.PresentExp);
            parameters.AddInParameter("@MoneyReceipt", DbType.Currency, orderInfo.MoneyReceipt);
            parameters.AddInParameter("@DeliverStatus", DbType.Int32, (int) orderInfo.DeliverStatus);
            parameters.AddInParameter("@EnableDownload", DbType.Boolean, orderInfo.EnableDownload);
            parameters.AddInParameter("@Memo", DbType.String, orderInfo.Memo);
            parameters.AddInParameter("@OutOfStockProject", DbType.Int32, (int) orderInfo.OutOfStockProject);
            parameters.AddInParameter("@OrderType", DbType.Int32, orderInfo.OrderType);
            parameters.AddInParameter("@CouponID", DbType.Int32, orderInfo.CouponId);
            parameters.AddInParameter("@InputTime", DbType.DateTime, orderInfo.InputTime);
            parameters.AddInParameter("@DeliveryTime", DbType.String, orderInfo.DeliveryTime);
            return parameters;
        }

        private static UserOrderCommonInfo GetUserOrderCommonInfo(NullableDataReader rdr)
        {
            UserOrderCommonInfo info = new UserOrderCommonInfo();
            info.ProductId = rdr.GetInt32("ProductId");
            info.TableName = rdr.GetString("TableName");
            info.ProductName = rdr.GetString("ProductName");
            info.Unit = rdr.GetString("Unit");
            info.Amount = rdr.GetInt32("Amount");
            info.BeginDate = rdr.GetDateTime("BeginDate");
            info.Price = rdr.GetDecimal("Price");
            info.TruePrice = rdr.GetDecimal("TruePrice");
            info.ServiceTerm = rdr.GetInt32("ServiceTerm");
            info.Remark = rdr.GetString("Remark");
            info.ServiceTermUnit = (ServiceTermUnit) rdr.GetInt32("ServiceTermUnit");
            info.OrderItemId = rdr.GetInt32("ItemId");
            return info;
        }

        public bool GoPause(int orderId)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Orders SET Status = 4 WHERE OrderID = @OrderID", new Parameters("@OrderID", DbType.Int32, orderId));
        }

        public bool GoRubbish(int orderId)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Orders SET Status = 3 WHERE OrderID = @OrderID", new Parameters("@OrderID", DbType.Int32, orderId));
        }

        private static string HighLevelSearch(string field, string keyword)
        {
            string str = "";
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (field)
                {
                    case "OrderNum":
                        str = "O.OrderNum LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "ClientName":
                        str = "C.ClientName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' OR C.ShortedForm LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "UserName":
                        str = "O.UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "RealUserName":
                        str = "O.UserName = '" + DBHelper.FilterBadChar(keyword) + "' AND O.Status != 3 AND O.Status !=4";
                        break;

                    case "AgentName":
                        str = "O.AgentName = '" + DBHelper.FilterBadChar(keyword) + "'";
                        break;

                    case "ContacterName":
                        str = "O.ContacterName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "Address":
                        str = "O.Address LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "Phone":
                        str = "O.Phone LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "DateAndTime":
                        str = "DATEDIFF(dd, O.InputTime, '" + keyword + "') = 0 ";
                        break;

                    case "Remark":
                        str = "O.Remark LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "ProductName":
                        str = "P.ProductName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "QQ":
                        str = "T.QQ = '" + DBHelper.FilterBadChar(keyword) + "'";
                        break;

                    case "Email":
                        str = "O.Email LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "Mobile":
                        str = "O.Mobile LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "InvoiceContent":
                        str = "O.InvoiceContent LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "Memo":
                        str = "O.Memo LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case "Functionary":
                        str = "O.Functionary LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;
                }
            }
            s_ThisSearchCondition = str;
            return str;
        }

        private static bool NullProductName(string keyword)
        {
            return (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(keyword.Split(new char[] { '|' })[0x12]));
        }

        private static OrderInfo OrderFromrdr(NullableDataReader rdr, string action)
        {
            OrderInfo info = new OrderInfo();
            info.OrderId = rdr.GetInt32("OrderID");
            info.OrderNum = rdr.GetString("OrderNum");
            info.UserName = rdr.GetString("UserName");
            info.ClientId = rdr.GetInt32("ClientID");
            info.ClientName = rdr.GetString("ClientName");
            info.MoneyTotal = rdr.GetDecimal("MoneyTotal");
            info.NeedInvoice = rdr.GetBoolean("NeedInvoice");
            info.Invoiced = rdr.GetBoolean("Invoiced");
            info.Remark = rdr.GetString("Remark");
            info.MoneyReceipt = rdr.GetDecimal("MoneyReceipt");
            info.InputTime = rdr.GetDateTime("InputTime");
            info.Status = (OrderStatus) rdr.GetInt32("Status");
            info.DeliverStatus = (DeliverStatus) rdr.GetInt32("DeliverStatus");
            if (string.IsNullOrEmpty(action))
            {
                info.EnableDownload = rdr.GetBoolean("EnableDownload");
                info.PresentMoney = rdr.GetDecimal("PresentMoney");
                info.PresentPoint = rdr.GetInt32("PresentPoint");
                info.PresentExp = rdr.GetInt32("PresentExp");
                info.DiscountPayment = rdr.GetDouble("Discount_Payment");
                info.ChargeDeliver = rdr.GetDecimal("Charge_Deliver");
            }
            info.AgentName = rdr.GetString("AgentName");
            info.Functionary = rdr.GetString("Functionary");
            info.InvoiceContent = rdr.GetString("InvoiceContent");
            info.BeginDate = rdr.GetDateTime("BeginDate");
            info.ContacterName = rdr.GetString("ContacterName");
            info.Address = rdr.GetString("Address");
            info.ZipCode = rdr.GetString("ZipCode");
            info.Mobile = rdr.GetString("Mobile");
            info.Phone = rdr.GetString("Phone");
            info.Email = rdr.GetString("Email");
            info.PaymentType = rdr.GetInt32("PaymentType");
            info.DeliverType = rdr.GetInt32("DeliverType");
            info.Memo = rdr.GetString("Memo");
            info.OutOfStockProject = (OutOfStockProject) rdr.GetInt32("OutOfStockProject");
            info.OrderType = rdr.GetInt32("OrderType");
            info.CouponId = rdr.GetInt32("CouponID");
            info.DeliveryTime = rdr.GetString("DeliveryTime");
            return info;
        }

        public bool Recieve(int orderId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderID", DbType.Int32, orderId);
            return DBHelper.ExecuteSql("UPDATE PE_Orders SET DeliverStatus = 2 WHERE OrderID = @OrderID", cmdParams);
        }

        public bool Transfer(int orderId, int clientId, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderID", DbType.Int32, orderId);
            cmdParams.AddInParameter("@ClientID", DbType.Int32, clientId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExecuteSql("UPDATE PE_Orders SET ClientID = @ClientID, UserName = @UserName WHERE OrderID = @OrderID", cmdParams);
        }

        public bool Update(OrderInfo orderInfo)
        {
            try
            {
                return (DBHelper.ExecuteNonQueryProc("PR_Shop_Order_Update", GetUpdateParms(orderInfo)) > 0);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDeliverStatus(int orderId, DeliverStatus statusValue)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderID", DbType.Int32, orderId);
            cmdParams.AddInParameter("@DeliverStatus", DbType.Int32, (int) statusValue);
            return DBHelper.ExecuteSql("UPDATE PE_Orders SET DeliverStatus = @DeliverStatus WHERE OrderID = @OrderId", cmdParams);
        }

        public bool UserPayment(int orderId, decimal moneyReceipt, OrderStatus status)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderID", DbType.Int32, orderId);
            cmdParams.AddInParameter("@MoneyReceipt", DbType.Decimal, moneyReceipt);
            cmdParams.AddInParameter("@Status", DbType.Int32, (int) status);
            return DBHelper.ExecuteSql("UPDATE PE_Orders SET MoneyReceipt = @MoneyReceipt, Status = @Status WHERE OrderID = @OrderID", cmdParams);
        }
    }
}


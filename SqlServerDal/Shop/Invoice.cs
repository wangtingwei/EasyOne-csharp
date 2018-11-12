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

    public sealed class Invoice : IInvoice
    {
        private int m_TotalOfInvoice;

        public bool Add(InvoiceInfo invoiceInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ClientId", DbType.Int32, invoiceInfo.ClientId);
            cmdParams.AddInParameter("@UserName", DbType.String, invoiceInfo.UserName);
            cmdParams.AddInParameter("@OrderId", DbType.Int32, invoiceInfo.OrderId);
            cmdParams.AddInParameter("@InvoiceType", DbType.Int32, invoiceInfo.InvoiceType);
            cmdParams.AddInParameter("@InvoiceDate", DbType.DateTime, invoiceInfo.InvoiceDate);
            cmdParams.AddInParameter("@InvoiceNum", DbType.String, invoiceInfo.InvoiceNum);
            cmdParams.AddInParameter("@InvoiceTitle", DbType.String, invoiceInfo.InvoiceTitle);
            cmdParams.AddInParameter("@InvoiceContent", DbType.String, invoiceInfo.InvoiceContent);
            cmdParams.AddInParameter("@TotalMoney", DbType.Decimal, invoiceInfo.TotalMoney);
            cmdParams.AddInParameter("@Drawer", DbType.String, invoiceInfo.Drawer);
            cmdParams.AddInParameter("@Inputer", DbType.String, invoiceInfo.Inputer);
            cmdParams.AddInParameter("@Memo", DbType.String, invoiceInfo.Memo);
            return DBHelper.ExecuteProc("PR_Shop_Invoice_Add", cmdParams);
        }

        public InvoiceInfo GetInvoiceInfoById(int invoiceId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@InvoiceId", DbType.Int32, invoiceId);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Shop_Invoice_GetById", cmdParams)))
            {
                if (reader.Read())
                {
                    InvoiceInfo info = InvoiceItemFromrdr(reader);
                    info.MoneyTotal = reader.GetDecimal("MoneyTotal");
                    info.MoneyReceipt = reader.GetDecimal("MoneyReceipt");
                    info.Memo = reader.GetString("Memo");
                    return info;
                }
                return new InvoiceInfo(true);
            }
        }

        public InvoiceInfo GetInvoiceInfoByOrderId(int orderId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderId", DbType.Int32, orderId);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Shop_Invoice_GetByOrderId", cmdParams)))
            {
                if (reader.Read())
                {
                    InvoiceInfo info = new InvoiceInfo();
                    info.ClientId = reader.GetInt32("ClientID");
                    info.UserName = reader.GetString("UserName");
                    info.InvoiceContent = reader.GetString("InvoiceContent");
                    info.OrderNum = reader.GetString("OrderNum");
                    info.ClientName = reader.GetString("ClientName");
                    info.MoneyTotal = reader.GetDecimal("MoneyTotal");
                    info.MoneyReceipt = reader.GetDecimal("MoneyReceipt");
                    info.Email = reader.GetString("Email");
                    return info;
                }
                return new InvoiceInfo(true);
            }
        }

        public IList<InvoiceInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int quickSearch)
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
            database.SetParameterValue(storedProcCommand, "@SortColumn", "InvoiceID");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "I.*, O.OrderNum, C.ShortedForm AS ClientName");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_InvoiceItem AS I LEFT JOIN PE_Orders AS O LEFT JOIN PE_Client AS C ON O.ClientID = C.ClientID ON I.OrderID = O.OrderID");
            switch (quickSearch)
            {
                case 1:
                    database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(dd, I.InvoiceDate, GETDATE()) < 10");
                    break;

                case 2:
                    database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(m, I.InvoiceDate, GETDATE()) < 1");
                    break;

                case 3:
                    database.SetParameterValue(storedProcCommand, "@Filter", "I.InvoiceType = 0");
                    break;

                case 4:
                    database.SetParameterValue(storedProcCommand, "@Filter", "I.InvoiceType = 1");
                    break;

                case 5:
                    database.SetParameterValue(storedProcCommand, "@Filter", "I.InvoiceType = 2");
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
                                database.SetParameterValue(storedProcCommand, "@Filter", "Drawer LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                                break;

                            case 3:
                                database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(dd, I.InvoiceDate, '" + keyword + "') = 0");
                                break;

                            case 4:
                                database.SetParameterValue(storedProcCommand, "@Filter", "I.ClientID = " + DBHelper.ToNumber(keyword));
                                break;

                            case 5:
                                database.SetParameterValue(storedProcCommand, "@Filter", "I.OrderID = " + DBHelper.ToNumber(keyword));
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
            IList<InvoiceInfo> list = new List<InvoiceInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(InvoiceItemFromrdr(reader));
                }
            }
            this.m_TotalOfInvoice = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetTotalOfInvoiceItem()
        {
            return this.m_TotalOfInvoice;
        }

        private static InvoiceInfo InvoiceItemFromrdr(NullableDataReader rdr)
        {
            InvoiceInfo info = new InvoiceInfo();
            info.InvoiceId = rdr.GetInt32("InvoiceID");
            info.ClientId = rdr.GetInt32("ClientID");
            info.UserName = rdr.GetString("UserName");
            info.OrderId = rdr.GetInt32("OrderID");
            info.InvoiceType = rdr.GetInt32("InvoiceType");
            info.InvoiceNum = rdr.GetString("InvoiceNum");
            info.InvoiceTitle = rdr.GetString("InvoiceTitle");
            info.InvoiceContent = rdr.GetString("InvoiceContent");
            info.InvoiceDate = rdr.GetDateTime("InvoiceDate");
            info.TotalMoney = rdr.GetDecimal("TotalMoney");
            info.Drawer = rdr.GetString("Drawer");
            info.Inputer = rdr.GetString("Inputer");
            info.InputTime = rdr.GetDateTime("InputTime");
            info.OrderNum = rdr.GetString("OrderNum");
            info.ClientName = rdr.GetString("ClientName");
            return info;
        }
    }
}


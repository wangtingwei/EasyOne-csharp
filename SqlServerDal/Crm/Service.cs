namespace EasyOne.SqlServerDal.Crm
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class Service : IService
    {
        private int m_TotalOfService;

        public bool Add(ServiceInfo serviceInfo)
        {
            if (serviceInfo.ItemId <= 0)
            {
                serviceInfo.ItemId = this.GetMaxId() + 1;
            }
            return DBHelper.ExecuteProc("PR_Crm_Service_Add", GetParameters(serviceInfo));
        }

        private static string ComplexSearch(string keyword)
        {
            string str = " 1 = 1";
            if (string.IsNullOrEmpty(keyword))
            {
                return str;
            }
            ServiceInfo info = new Serialize<ServiceInfo>().DeserializeField(keyword);
            if (info == null)
            {
                return str;
            }
            if (!string.IsNullOrEmpty(info.ClientName))
            {
                str = str + " AND C.ClientName LIKE '%" + DBHelper.FilterBadChar(info.ClientName) + "%'";
            }
            if (!string.IsNullOrEmpty(info.ServiceTitle))
            {
                str = str + " AND S.ServiceTitle LIKE '%" + DBHelper.FilterBadChar(info.ServiceTitle) + "%'";
            }
            if (!string.IsNullOrEmpty(info.Processor))
            {
                str = str + " AND S.Processor LIKE '%" + DBHelper.FilterBadChar(info.Processor) + "%'";
            }
            if (!string.IsNullOrEmpty(info.Inputer))
            {
                str = str + " AND S.Inputer LIKE '%" + DBHelper.FilterBadChar(info.Inputer) + "%' ";
            }
            if (!string.IsNullOrEmpty(info.ConfirmCaller))
            {
                str = str + " AND S.ConfirmCaller LIKE '%" + DBHelper.FilterBadChar(info.ConfirmCaller) + "%'";
            }
            if (info.TakeTime != -1)
            {
                str = str + " AND S.TakeTime = " + info.TakeTime.ToString();
            }
            if (info.ServiceType != "-1")
            {
                str = str + " AND S.ServiceType = " + DBHelper.ToNumber(info.ServiceType);
            }
            if (info.ServiceMode != "-1")
            {
                str = str + " AND S.ServiceMode = " + DBHelper.ToNumber(info.ServiceMode);
            }
            if (info.ServiceResult != -1)
            {
                str = str + " AND S.ServiceResult = " + info.ServiceResult.ToString();
            }
            if (info.ConfirmScore != -1)
            {
                str = str + " AND S.ConfirmScore = " + info.ConfirmScore.ToString();
            }
            string[] strArray = info.Remark.Split(new char[] { '|' });
            if (strArray[0] != "-1")
            {
                if (strArray[0] == "0")
                {
                    str = str + " AND S.ConfirmTime IS NULL";
                }
                else
                {
                    str = str + " AND S.ConfirmTime IS NOT NULL";
                }
            }
            return (str + GetTimePart("S.ServiceTime", strArray[1], strArray[2]) + GetTimePart("S.ConfirmTime", strArray[3], strArray[4]));
        }

        public bool Delete(string id)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_ServiceItem WHERE ItemID IN (" + DBHelper.ToValidId(id) + ")");
        }

        private static string GetFilterString(string searchType, string field, string keyword)
        {
            string str = "";
            string str2 = searchType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    string str3;
                    if (str2 == "2")
                    {
                        return "DATEDIFF(dd, S.ServiceTime, GETDATE()) < 30";
                    }
                    if (str2 == "3")
                    {
                        return ("S.Inputer = '" + DBHelper.FilterBadChar(PEContext.Current.Admin.AdminName) + "'");
                    }
                    if (str2 != "10")
                    {
                        return str;
                    }
                    if (string.IsNullOrEmpty(keyword) || ((str3 = field) == null))
                    {
                        return str;
                    }
                    if (!(str3 == "ClientID"))
                    {
                        if (str3 != "ClientName")
                        {
                            if (str3 == "Processor")
                            {
                                return ("S.Processor LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ");
                            }
                            if (str3 == "ServiceTime")
                            {
                                return ("DATEDIFF(dd, S.ServiceTime, '" + keyword + "') < 1 ");
                            }
                            if (str3 != "ComplexSearch")
                            {
                                return str;
                            }
                            return ComplexSearch(keyword);
                        }
                    }
                    else
                    {
                        return ("C.ClientID = " + DBHelper.ToNumber(keyword));
                    }
                    return ("C.ClientName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ");
                }
            }
            else
            {
                return "";
            }
            return "DATEDIFF(dd, S.ServiceTime, GETDATE()) < 10";
        }

        public IList<ServiceInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<ServiceInfo> list = new List<ServiceInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ItemID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "S.*, C.ShortedForm, C.ClientName");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, GetFilterString(searchType, field, keyword));
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_ServiceItem S LEFT JOIN PE_Client C ON S.ClientID = C.ClientID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(ServiceFromrdr(reader));
                }
            }
            this.m_TotalOfService = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<ServiceInfo> GetListByClientName(int startRowIndexId, int maxNumberRows, string clientName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<ServiceInfo> list = new List<ServiceInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ItemID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "S.*, C.ShortedForm, C.ClientName");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, " C.ClientName = '" + (string.IsNullOrEmpty(clientName) ? "" : clientName.Replace("'", "''")) + "' ");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_ServiceItem S LEFT JOIN PE_Client C ON S.ClientID = C.ClientID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(ServiceFromrdr(reader));
                }
            }
            this.m_TotalOfService = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_ServiceItem", "ItemId");
        }

        private static Parameters GetParameters(ServiceInfo serviceInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ClientId", DbType.Int32, serviceInfo.ClientId);
            parameters.AddInParameter("@ContacterId", DbType.Int32, serviceInfo.ContacterId);
            parameters.AddInParameter("@OrderId", DbType.Int32, serviceInfo.OrderId);
            parameters.AddInParameter("@ServiceTime", DbType.DateTime, serviceInfo.ServiceTime);
            parameters.AddInParameter("@ServiceType", DbType.String, serviceInfo.ServiceType);
            parameters.AddInParameter("@ServiceMode", DbType.String, serviceInfo.ServiceMode);
            parameters.AddInParameter("@ServiceTitle", DbType.String, serviceInfo.ServiceTitle);
            parameters.AddInParameter("@ServiceContent", DbType.String, serviceInfo.ServiceContent);
            parameters.AddInParameter("@ServiceResult", DbType.Int32, serviceInfo.ServiceResult);
            parameters.AddInParameter("@TakeTime", DbType.Int32, serviceInfo.TakeTime);
            parameters.AddInParameter("@Processor", DbType.String, serviceInfo.Processor);
            parameters.AddInParameter("@Inputer", DbType.String, serviceInfo.Inputer);
            parameters.AddInParameter("@Feedback", DbType.String, serviceInfo.Feedback);
            parameters.AddInParameter("@ConfirmTime", DbType.DateTime, serviceInfo.ConfirmTime);
            parameters.AddInParameter("@ConfirmCaller", DbType.String, serviceInfo.ConfirmCaller);
            parameters.AddInParameter("@ConfirmScore", DbType.Int32, serviceInfo.ConfirmScore);
            parameters.AddInParameter("@ConfirmFeedback", DbType.String, serviceInfo.ConfirmFeedback);
            parameters.AddInParameter("@Remark", DbType.String, serviceInfo.Remark);
            return parameters;
        }

        public ServiceInfo GetServiceById(int id)
        {
            ServiceInfo info = new ServiceInfo();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT S.*, C.ShortedForm, C.ClientName FROM PE_ServiceItem S LEFT JOIN PE_Client C ON S.ClientID = C.ClientID WHERE S.ItemID = @ItemId", new Parameters("@ItemId", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    info = ServiceFromrdr(reader);
                }
            }
            return info;
        }

        private static string GetTimePart(string timeField, string beginTime, string endTime)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(beginTime) || !string.IsNullOrEmpty(endTime))
            {
                if (!string.IsNullOrEmpty(beginTime))
                {
                    string str2 = str;
                    str = str2 + " AND " + DBHelper.FilterBadChar(timeField) + ">='" + beginTime.Replace("'", "") + "'";
                }
                if (!string.IsNullOrEmpty(endTime))
                {
                    string str3 = str;
                    str = str3 + " AND " + DBHelper.FilterBadChar(timeField) + "<='" + endTime.Replace("'", "") + "'";
                }
            }
            return str;
        }

        public int GetTotalOfService()
        {
            return this.m_TotalOfService;
        }

        private static ServiceInfo ServiceFromrdr(NullableDataReader rdr)
        {
            ServiceInfo info = new ServiceInfo();
            info.ItemId = rdr.GetInt32("ItemId");
            info.ClientId = rdr.GetInt32("ClientId");
            info.ContacterId = rdr.GetInt32("ContacterId");
            info.OrderId = rdr.GetInt32("OrderId");
            info.ServiceTime = rdr.GetDateTime("ServiceTime");
            info.ServiceType = rdr.GetString("ServiceType");
            info.ServiceMode = rdr.GetString("ServiceMode");
            info.ServiceTitle = rdr.GetString("ServiceTitle");
            info.ServiceContent = rdr.GetString("ServiceContent");
            info.ServiceResult = rdr.GetInt32("ServiceResult");
            info.TakeTime = rdr.GetInt32("TakeTime");
            info.Processor = rdr.GetString("Processor");
            info.Inputer = rdr.GetString("Inputer");
            info.Feedback = rdr.GetString("Feedback");
            info.ConfirmTime = rdr.GetNullableDateTime("ConfirmTime");
            info.ConfirmCaller = rdr.GetString("ConfirmCaller");
            info.ConfirmScore = rdr.GetInt32("ConfirmScore");
            info.ConfirmFeedback = rdr.GetString("ConfirmFeedback");
            info.Remark = rdr.GetString("Remark");
            info.ClientName = rdr.GetString("ClientName");
            info.ShortedForm = rdr.GetString("ShortedForm");
            return info;
        }

        public bool Update(ServiceInfo serviceInfo)
        {
            Parameters cmdParams = GetParameters(serviceInfo);
            cmdParams.AddInParameter("@ItemId", DbType.Int32, serviceInfo.ItemId);
            return DBHelper.ExecuteProc("PR_Crm_Service_Update", cmdParams);
        }
    }
}


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
    using System.Text;

    public sealed class Client : IClient
    {
        private int m_TotalOfClient;

        public bool Add(ClientInfo clientInfo)
        {
            Parameters parms = new Parameters();
            GetParameters(clientInfo, parms);
            parms.AddInParameter("@CreateTime", DbType.DateTime, DateTime.Now);
            return DBHelper.ExecuteProc("PR_Crm_Client_Add", parms);
        }

        public bool CheckClientName(string clienName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ClienName", DbType.String, clienName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Client WHERE ClientName = @ClienName", cmdParams);
        }

        public bool CheckShortedForm(string shortedForm)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ShortedForm", DbType.String, shortedForm);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Client WHERE ShortedForm = @ShortedForm", cmdParams);
        }

        private static ClientInfo ClientFromrdr(NullableDataReader rdr)
        {
            ClientInfo info = new ClientInfo();
            info.ClientId = rdr.GetInt32("ClientID");
            info.ParentId = rdr.GetInt32("ParentID");
            info.ClientNum = rdr.GetString("ClientNum");
            info.ClientType = rdr.GetInt32("ClientType");
            info.ClientName = rdr.GetString("ClientName");
            info.ShortedForm = rdr.GetString("ShortedForm");
            info.Area = rdr.GetInt32("Area");
            info.ClientField = rdr.GetInt32("ClientField");
            info.ValueLevel = rdr.GetInt32("ValueLevel");
            info.CreditLevel = rdr.GetInt32("CreditLevel");
            info.Importance = rdr.GetInt32("Importance");
            info.ConnectionLevel = rdr.GetInt32("ConnectionLevel");
            info.GroupId = rdr.GetInt32("GroupID");
            info.SourceType = rdr.GetInt32("SourceType");
            info.PhaseType = rdr.GetInt32("PhaseType");
            info.Remark = rdr.GetString("Remark");
            info.VisitTimes = rdr.GetInt32("VisitTimes");
            info.ServiceTimes = rdr.GetInt32("ServiceTimes");
            info.ComplainTimes = rdr.GetInt32("ComplainTimes");
            info.LastVisitTime = rdr.GetDateTime("LastVisitTime");
            info.LastServiceTime = rdr.GetDateTime("LastServiceTime");
            info.LastComplainTime = rdr.GetDateTime("LastComplainTime");
            info.CreateTime = rdr.GetDateTime("CreateTime");
            info.UpdateTime = rdr.GetDateTime("UpdateTime");
            info.Owner = rdr.GetString("Owner");
            info.Balance = rdr.GetDecimal("Balance");
            return info;
        }

        public bool Delete(string clientId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Client WHERE ClientID IN (" + DBHelper.ToValidId(clientId) + ")");
        }

        public string GetAllClientId()
        {
            StringBuilder sb = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT ClientID FROM PE_Client"))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetInt32("ClientID").ToString());
                }
            }
            return sb.ToString();
        }

        public ClientInfo GetClientById(int clientId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Crm_Client_GetById", new Parameters("@ClientID", DbType.Int32, clientId)))
            {
                if (reader.Read())
                {
                    return ClientFromrdr(reader);
                }
                return new ClientInfo(true);
            }
        }

        public string GetClientIdByGroup(string groupIdList)
        {
            StringBuilder sb = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT ClientID FROM PE_Client WHERE GroupID IN (" + DBHelper.ToValidId(groupIdList) + ")"))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetInt32("ClientID").ToString());
                }
            }
            return sb.ToString();
        }

        public string GetClientNameById(int clientId)
        {
            return Convert.ToString(DBHelper.ExecuteScalarSql("SELECT ClientName FROM PE_Client WHERE ClientID = @ClientID", new Parameters("@ClientID", DbType.Int32, clientId)));
        }

        public IList<ClientInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int quickSearch, int groupId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            List<ClientInfo> list = new List<ClientInfo>();
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ClientID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Client");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            switch (searchType)
            {
                case 1:
                    if (!(keyword == "0"))
                    {
                        database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "ClientID = " + Convert.ToInt32(keyword));
                    }
                    else
                    {
                        database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
                    }
                    break;

                case 2:
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "ClientName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    }
                    else
                    {
                        database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
                    }
                    break;

                case 5:
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "ClientType = 0 AND ClientName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    }
                    else
                    {
                        database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "ClientType = 0");
                    }
                    break;

                default:
                    if (groupId >= 0)
                    {
                        database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "GroupID = " + groupId);
                    }
                    else
                    {
                        switch (quickSearch)
                        {
                            case 1:
                                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "ClientType = 0");
                                break;

                            case 2:
                                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "ClientType = 1");
                                break;

                            case 3:
                                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "Owner = '" + DBHelper.FilterBadChar(PEContext.Current.Admin.AdminName) + "'");
                                break;
                        }
                        database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
                    }
                    break;
            }
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(ClientFromrdr(reader));
                }
            }
            this.m_TotalOfClient = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Client", "ClientID");
        }

        private static void GetParameters(ClientInfo clientInfo, Parameters parms)
        {
            parms.AddInParameter("@ClientID", DbType.Int32, clientInfo.ClientId);
            parms.AddInParameter("@ParentID", DbType.Int32, clientInfo.ParentId);
            parms.AddInParameter("@ClientNum", DbType.String, clientInfo.ClientNum);
            parms.AddInParameter("@ClientType", DbType.Int32, clientInfo.ClientType);
            parms.AddInParameter("@ClientName", DbType.String, clientInfo.ClientName);
            parms.AddInParameter("@ShortedForm", DbType.String, clientInfo.ShortedForm);
            parms.AddInParameter("@Area", DbType.Int32, clientInfo.Area);
            parms.AddInParameter("@ClientField", DbType.Int32, clientInfo.ClientField);
            parms.AddInParameter("@ValueLevel", DbType.Int32, clientInfo.ValueLevel);
            parms.AddInParameter("@CreditLevel", DbType.Int32, clientInfo.CreditLevel);
            parms.AddInParameter("@Importance", DbType.Int32, clientInfo.Importance);
            parms.AddInParameter("@ConnectionLevel", DbType.Int32, clientInfo.ConnectionLevel);
            parms.AddInParameter("@GroupID", DbType.Int32, clientInfo.GroupId);
            parms.AddInParameter("@SourceType", DbType.Int32, clientInfo.SourceType);
            parms.AddInParameter("@PhaseType", DbType.Int32, clientInfo.PhaseType);
            parms.AddInParameter("@Remark", DbType.String, clientInfo.Remark);
            parms.AddInParameter("@UpdateTime", DbType.DateTime, DateTime.Now);
            parms.AddInParameter("@Owner", DbType.String, clientInfo.Owner);
        }

        public int GetTotalOfClient()
        {
            return this.m_TotalOfClient;
        }

        public bool Income(int clientId, decimal money)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Money", DbType.Decimal, money);
            cmdParams.AddInParameter("@ClientID", DbType.Int32, clientId);
            return DBHelper.ExecuteSql("UPDATE PE_Client SET Balance = Balance + @Money WHERE ClientID = @ClientID", cmdParams);
        }

        public bool Payment(int clientId, decimal money)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Money", DbType.Decimal, money);
            cmdParams.AddInParameter("@ClientID", DbType.Int32, clientId);
            return DBHelper.ExecuteSql("UPDATE PE_Client SET Balance = Balance - @Money WHERE ClientID = @ClientID", cmdParams);
        }

        public bool Update(ClientInfo clientInfo)
        {
            Parameters parms = new Parameters();
            GetParameters(clientInfo, parms);
            return DBHelper.ExecuteProc("PR_Crm_Client_Update", parms);
        }

        public bool UpdateClientType(int clientId, int clientType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ClientID", DbType.Int32, clientId);
            cmdParams.AddInParameter("@ClientType", DbType.Int32, clientType);
            return DBHelper.ExecuteSql("UPDATE PE_Client SET ClientType = @ClientType WHERE ClientID = @ClientID", cmdParams);
        }

        public bool UpdateForCompany(int clientId, string companyName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ClientID", DbType.Int32, clientId);
            cmdParams.AddInParameter("@CompanyName", DbType.String, companyName);
            cmdParams.AddInParameter("@ShortedForm", DbType.String, companyName.Substring(0, 5));
            return DBHelper.ExecuteSql("UPDATE PE_Client SET ClientName = @CompanyName, ShortedForm = @ShortedForm, ClientType = 0 WHERE ClientID = @ClientID", cmdParams);
        }
    }
}


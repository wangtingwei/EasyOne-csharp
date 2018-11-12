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

    public class Complain : IComplain
    {
        private int m_Total;

        public bool Add(ComplainItemInfo info)
        {
            if (info.ItemId <= 0)
            {
                info.ItemId = this.GetMaxId() + 1;
            }
            return DBHelper.ExecuteProc("PR_Crm_ComplainItem_Add", GetParameters(info));
        }

        private static ComplainItemInfo ComplainFromDataReader(NullableDataReader rdr)
        {
            ComplainItemInfo info = new ComplainItemInfo();
            info.ItemId = rdr.GetInt32("ItemID");
            info.ClientId = rdr.GetInt32("ClientID");
            info.ContacterId = rdr.GetInt32("ContacterID");
            info.ComplainMode = rdr.GetInt32("ComplainMode");
            info.ComplainType = rdr.GetInt32("ComplainType");
            info.ConfirmCaller = rdr.GetString("ConfirmCaller");
            info.ConfirmFeedback = rdr.GetString("ConfirmFeedback");
            info.ConfirmScore = rdr.GetInt32("ConfirmScore");
            info.ConfirmTime = rdr.GetNullableDateTime("ConfirmTime");
            info.Content = rdr.GetString("Content");
            info.CurrentOwner = rdr.GetString("CurrentOwner");
            info.DateAndTime = rdr.GetDateTime("DateAndTime");
            info.EndTime = rdr.GetNullableDateTime("EndTime");
            info.Feedback = rdr.GetString("Feedback");
            info.FirstReceiver = rdr.GetString("FirstReceiver");
            info.MagnitudeOfExigence = rdr.GetInt32("MagnitudeOfExigence");
            info.Process = rdr.GetString("Process");
            info.Processor = rdr.GetString("Processor");
            info.Remark = rdr.GetString("Remark");
            info.Result = rdr.GetString("Result");
            info.ShortedForm = rdr.GetString("ShortedForm");
            info.Status = rdr.GetInt32("Status");
            info.Title = rdr.GetString("Title");
            info.Defendant = rdr.GetString("Defendant");
            return info;
        }

        private static string ComplexSearch(string keyword)
        {
            string str = string.Empty;
            ComplainItemInfo info = new Serialize<ComplainItemInfo>().DeserializeField(keyword);
            if (!string.IsNullOrEmpty(info.ShortedForm))
            {
                str = str + " AND C.ClientName LIKE '%" + info.ShortedForm.Replace("'", "") + "%'";
            }
            if (!string.IsNullOrEmpty(info.FirstReceiver))
            {
                str = str + " AND S.FirstReceiver LIKE '%" + DBHelper.FilterBadChar(info.FirstReceiver) + "%'";
            }
            if (!string.IsNullOrEmpty(info.Processor))
            {
                str = str + " AND S.Processor LIKE '%" + DBHelper.FilterBadChar(info.Processor) + "%'";
            }
            if (!string.IsNullOrEmpty(info.ConfirmCaller))
            {
                str = str + " AND S.ConfirmCaller LIKE '%" + DBHelper.FilterBadChar(info.ConfirmCaller) + "%'";
            }
            if (!string.IsNullOrEmpty(info.Title))
            {
                str = str + " AND S.Title LIKE '%" + info.Title.Replace("'", "") + "%'";
            }
            if (info.Status != -1)
            {
                str = str + " AND S.Status = " + info.Status.ToString();
            }
            if (info.ComplainMode != -1)
            {
                str = str + " AND S.ComplainMode = " + info.ComplainMode.ToString();
            }
            if (info.ComplainType != -1)
            {
                str = str + " AND S.ComplainType = " + info.ComplainType.ToString();
            }
            if (info.MagnitudeOfExigence != -1)
            {
                str = str + " AND S.MagnitudeOfExigence = " + info.MagnitudeOfExigence.ToString();
            }
            if (info.ConfirmScore != -1)
            {
                str = str + " AND S.ConfirmScore = " + info.ConfirmScore.ToString();
            }
            if (info.Remark != "|||||")
            {
                string[] strArray = info.Remark.Split(new char[] { '|' });
                str = (str + GetTimePart("S.DateAndTime", strArray[0], strArray[1])) + GetTimePart("S.EndTime", strArray[2], strArray[3]) + GetTimePart("S.ConfirmTime", strArray[4], strArray[5]);
            }
            return str;
        }

        public bool Delete(string itemId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_ComplainItem WHERE ItemID IN (" + DBHelper.ToValidId(itemId) + ")");
        }

        public ComplainItemInfo GetComplainById(int itemId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT S.*, C.ShortedForm FROM PE_ComplainItem S INNER JOIN PE_Client C ON S.ClientID = C.ClientID WHERE S.ItemId = @itemId", new Parameters("@itemId", DbType.Int32, itemId)))
            {
                if (reader.Read())
                {
                    return ComplainFromDataReader(reader);
                }
                return new ComplainItemInfo(true);
            }
        }

        public IList<ComplainItemInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, int field, string keyword)
        {
            IList<ComplainItemInfo> list = new List<ComplainItemInfo>();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "S.ItemID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "S.*, C.ClientName AS ShortedForm");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_ComplainItem S INNER JOIN PE_Client C ON S.ClientID = C.ClientID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            StringBuilder builder = new StringBuilder(0x100);
            builder.Append("1 = 1");
            if (string.IsNullOrEmpty(keyword))
            {
                switch (searchType)
                {
                    case 0:
                        goto Label_01D7;

                    case 1:
                        builder.Append(" AND DATEDIFF(d, S.DateAndTime, GETDATE()) < 10");
                        goto Label_01D7;

                    case 2:
                        builder.Append(" AND DATEDIFF(d, S.DateAndTime, GETDATE()) < 30");
                        goto Label_01D7;

                    case 3:
                        builder.Append(" AND FirstReceiver LIKE '%" + DBHelper.FilterBadChar(PEContext.Current.Admin.AdminName) + "%'");
                        goto Label_01D7;
                }
            }
            else
            {
                switch (field)
                {
                    case 0:
                        builder.Append(" AND S.ClientID = " + DBHelper.ToNumber(keyword));
                        goto Label_01D7;

                    case 1:
                        builder.Append(" AND C.ClientName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                        goto Label_01D7;

                    case 2:
                        builder.Append(" AND DATEDIFF(d, S.DateAndTime, '" + DBHelper.FilterBadChar(keyword) + "') = 0");
                        goto Label_01D7;

                    case 3:
                        builder.Append(" AND S.Defendant = '" + DBHelper.FilterBadChar(keyword) + "'");
                        goto Label_01D7;

                    case 0x63:
                        builder.Append(ComplexSearch(keyword));
                        goto Label_01D7;
                }
            }
        Label_01D7:
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(ComplainFromDataReader(reader));
                }
                reader.NextResult();
            }
            this.m_Total = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<ComplainItemInfo> GetListByClientName(int startRowIndexId, int maxNumberRows, string clientName)
        {
            IList<ComplainItemInfo> list = new List<ComplainItemInfo>();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "S.ItemID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "S.*, C.ClientName AS ShortedForm");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_ComplainItem S INNER JOIN PE_Client C ON S.ClientID = C.ClientID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "C.ClientName = '" + (string.IsNullOrEmpty(clientName) ? "" : clientName.Replace("'", "''")) + "'");
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(ComplainFromDataReader(reader));
                }
            }
            this.m_Total = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_ComplainItem", "ItemID");
        }

        private static Parameters GetParameters(ComplainItemInfo info)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ItemID", DbType.Int32, info.ItemId);
            parameters.AddInParameter("@ClientID", DbType.Int32, info.ClientId);
            parameters.AddInParameter("@ContacterID", DbType.Int32, info.ContacterId);
            parameters.AddInParameter("@ComplainType", DbType.Int32, info.ComplainType);
            parameters.AddInParameter("@ComplainMode", DbType.Int32, info.ComplainMode);
            parameters.AddInParameter("@Title", DbType.String, info.Title);
            parameters.AddInParameter("@Content", DbType.String, info.Content);
            parameters.AddInParameter("@FirstReceiver", DbType.String, info.FirstReceiver);
            parameters.AddInParameter("@DateAndTime", DbType.DateTime, info.DateAndTime);
            parameters.AddInParameter("@MagnitudeOfExigence", DbType.Int32, info.MagnitudeOfExigence);
            parameters.AddInParameter("@Process", DbType.String, info.Process);
            parameters.AddInParameter("@Processor", DbType.String, info.Processor);
            parameters.AddInParameter("@Result", DbType.String, info.Result);
            parameters.AddInParameter("@EndTime", DbType.DateTime, info.EndTime);
            parameters.AddInParameter("@Feedback", DbType.String, info.Feedback);
            parameters.AddInParameter("@ConfirmTime", DbType.DateTime, info.ConfirmTime);
            parameters.AddInParameter("@ConfirmCaller", DbType.String, info.ConfirmCaller);
            parameters.AddInParameter("@ConfirmScore", DbType.Int32, info.ConfirmScore);
            parameters.AddInParameter("@ConfirmFeedback", DbType.String, info.ConfirmFeedback);
            parameters.AddInParameter("@Status", DbType.Int32, info.Status);
            parameters.AddInParameter("@CurrentOwner", DbType.String, info.CurrentOwner);
            parameters.AddInParameter("@Remark", DbType.String, info.Remark);
            parameters.AddInParameter("@Defendant", DbType.String, info.Defendant);
            return parameters;
        }

        private static string GetTimePart(string timeField, string beginTime, string endTime)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(beginTime) || !string.IsNullOrEmpty(endTime))
            {
                if (!string.IsNullOrEmpty(beginTime))
                {
                    string str2 = str;
                    str = str2 + " AND " + timeField + ">='" + beginTime.Replace("'", "") + "'";
                }
                if (!string.IsNullOrEmpty(endTime))
                {
                    string str3 = str;
                    str = str3 + " AND " + timeField + "<='" + endTime.Replace("'", "") + "'";
                }
            }
            return str;
        }

        public int GetTotal()
        {
            return this.m_Total;
        }

        public bool Update(ComplainItemInfo info)
        {
            return DBHelper.ExecuteProc("PR_Crm_ComplainItem_Update", GetParameters(info));
        }
    }
}


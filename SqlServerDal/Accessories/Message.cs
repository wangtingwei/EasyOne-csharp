namespace EasyOne.SqlServerDal.Accessories
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class Message : IMessage
    {
        private int m_NumMessages;

        public bool Add(MessageInfo messageInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Title", DbType.String, messageInfo.Title);
            cmdParams.AddInParameter("@Content", DbType.String, messageInfo.Content);
            cmdParams.AddInParameter("@Sender", DbType.String, messageInfo.Sender);
            cmdParams.AddInParameter("@Incept", DbType.String, messageInfo.Incept);
            cmdParams.AddInParameter("@SendTime", DbType.DateTime, messageInfo.SendTime);
            cmdParams.AddInParameter("@IsSend", DbType.Int32, messageInfo.IsSend);
            cmdParams.AddInParameter("@IsDelInbox", DbType.Int32, messageInfo.IsDelInbox);
            cmdParams.AddInParameter("@IsDelSendbox", DbType.Int32, messageInfo.IsDelSendbox);
            cmdParams.AddInParameter("@IsRead", DbType.Int32, messageInfo.IsRead);
            return DBHelper.ExecuteProc("PR_Accessories_Message_Add", cmdParams);
        }

        public bool Clear(MessageManageType manageType, string userName, string messageIdList)
        {
            string format = string.Empty;
            switch (manageType)
            {
                case MessageManageType.Inbox:
                    format = "UPDATE PE_Message SET IsDelInbox = 1 WHERE Incept = '{0}' AND IsDelInbox = 0 {1}";
                    break;

                case MessageManageType.Outbox:
                    format = "UPDATE PE_Message SET IsDelSendbox = 1 WHERE Sender = '{0}' AND IsDelSendbox = 0 AND IsSend = 0 {1}";
                    break;

                case MessageManageType.IsSend:
                    format = "UPDATE PE_Message SET IsDelSendbox = 1 WHERE Sender = '{0}' AND IsDelSendbox = 0 AND IsSend = 1 {1}";
                    break;

                case MessageManageType.Recycle:
                    format = "DELETE FROM PE_Message WHERE Incept = '{0}' AND IsDelInbox = 1 AND IsDelSendbox = 2 {1} ; DELETE FROM PE_Message WHERE Sender = '{0}' AND IsDelSendbox = 1 AND IsDelInbox = 2 {1} ; DELETE FROM PE_Message WHERE Sender = '{0}' AND IsDelSendbox = 1 AND IsSend = 0 {1} ; Update PE_Message Set IsDelSendbox = 2 WHERE Sender = '{0}' AND IsDelSendbox = 1 {1} ; Update PE_Message Set IsDelInbox = 2 WHERE Incept = '{0}' AND IsDelInbox = 1 {1} ";
                    break;

                default:
                    return false;
            }
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(messageIdList))
            {
                str2 = " AND MessageID In(" + DBHelper.ToValidId(messageIdList) + ")";
            }
            return DBHelper.ExecuteSql(string.Format(format, DBHelper.FilterBadChar(userName), str2));
        }

        public int Count()
        {
            return this.m_NumMessages;
        }

        public bool Delete(MessageDelType deleteType, string deleteValue)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@DeleteType", DbType.Int32, (int) deleteType);
            cmdParams.AddInParameter("@DeleteValue", DbType.String, deleteValue);
            return DBHelper.ExecuteProc("PR_Accessories_Message_Delete", cmdParams);
        }

        private IList<MessageInfo> GetList(int startRowIndexId, int maxNumberRows, Database db, DbCommand ProcdbComm)
        {
            db.AddInParameter(ProcdbComm, "@StartRows", DbType.Int32, startRowIndexId);
            db.AddInParameter(ProcdbComm, "@PageSize", DbType.Int32, maxNumberRows);
            db.AddInParameter(ProcdbComm, "@SortColumn", DbType.String, "MessageID");
            db.AddInParameter(ProcdbComm, "@StrColumn", DbType.String, "*");
            db.AddInParameter(ProcdbComm, "@Sorts", DbType.String, "DESC");
            db.AddInParameter(ProcdbComm, "@TableName", DbType.String, "PE_Message");
            db.AddOutParameter(ProcdbComm, "@Total", DbType.Int32, maxNumberRows);
            IList<MessageInfo> list = new List<MessageInfo>();
            using (NullableDataReader reader = new NullableDataReader(db.ExecuteReader(ProcdbComm)))
            {
                while (reader.Read())
                {
                    list.Add(MessageFromrdr(reader));
                }
            }
            this.m_NumMessages = (int) db.GetParameterValue(ProcdbComm, "@Total");
            return list;
        }

        private static MessageInfo GetMessage(int messsageId, string userName, string sqlCmd)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@ID", DbType.Int32, messsageId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(sqlCmd, cmdParams))
            {
                if (reader.Read())
                {
                    return MessageFromrdr(reader);
                }
                return new MessageInfo(true);
            }
        }

        public MessageInfo GetMessageById(int messageId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Accessories_Message_GetById");
            database.AddInParameter(storedProcCommand, "@MessageId", DbType.Int32, messageId);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                if (reader.Read())
                {
                    return MessageFromrdr(reader);
                }
                return new MessageInfo(true);
            }
        }

        public IList<MessageInfo> GetMessageList(int startRowIndexId, int maxNumberRows, MessageSearchField searchType, string keyword)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = db.GetStoredProcCommand("PR_Common_GetList");
            if ((searchType != MessageSearchField.All) && !string.IsNullOrEmpty(keyword))
            {
                if (searchType == MessageSearchField.OnePeople)
                {
                    string str = "Sender LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' OR Incept LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    db.AddInParameter(storedProcCommand, "@Filter", DbType.String, str);
                }
                else
                {
                    db.AddInParameter(storedProcCommand, "@Filter", DbType.String, searchType.ToString() + " LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                }
            }
            else
            {
                db.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            }
            return this.GetList(startRowIndexId, maxNumberRows, db, storedProcCommand);
        }

        public IList<MessageInfo> GetMessageList(int startRowIndexId, int maxNumberRows, string userName, int read)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder builder = new StringBuilder();
            switch (read)
            {
                case -1:
                    builder.Append(" IsRead = 1 AND ");
                    break;

                case 1:
                    builder.Append(" IsRead = 0 AND ");
                    break;
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.Append("Incept = '" + DBHelper.FilterBadChar(userName) + "' AND ");
            }
            if (builder.Length > 0)
            {
                builder.Remove(builder.Length - 4, 4);
            }
            DbCommand storedProcCommand = db.GetStoredProcCommand("PR_Common_GetList");
            db.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            return this.GetList(startRowIndexId, maxNumberRows, db, storedProcCommand);
        }

        public IList<MessageInfo> GetMessageList(int startRowIndexId, int maxNumberRows, string userName, MessageManageType manageType, MessageSearchField searchField, string keyword)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder builder = new StringBuilder();
            switch (manageType)
            {
                case MessageManageType.Inbox:
                    builder.Append("IsSend = 1 AND IsDelInbox = 0 AND Incept = '" + DBHelper.FilterBadChar(userName) + "'");
                    break;

                case MessageManageType.Outbox:
                    builder.Append("Sender = '" + userName + "' AND IsSend = 0 AND IsDelSendbox = 0");
                    break;

                case MessageManageType.IsSend:
                    builder.Append("Sender = '" + userName + "' AND IsSend = 1 AND IsDelSendbox = 0");
                    break;

                case MessageManageType.Recycle:
                    builder.Append("((Sender = '" + userName + "' AND IsDelSendbox = 1) OR (Incept = '" + DBHelper.FilterBadChar(userName) + "' AND IsDelInbox = 1))");
                    break;

                default:
                    builder.Append("IsSend = 1 AND IsDelInbox = 0 AND Incept = '" + DBHelper.FilterBadChar(userName) + "'");
                    break;
            }
            if ((searchField != MessageSearchField.All) && !string.IsNullOrEmpty(keyword))
            {
                builder.Append(" AND " + searchField.ToString() + " LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
            }
            DbCommand storedProcCommand = db.GetStoredProcCommand("PR_Common_GetList");
            db.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            return this.GetList(startRowIndexId, maxNumberRows, db, storedProcCommand);
        }

        public MessageInfo GetMessageOfEdit(int messageId, string userName)
        {
            string sqlCmd = "SELECT * FROM PE_Message WHERE Sender = @UserName AND IsSend = 0 AND MessageID = @ID";
            return GetMessage(messageId, userName, sqlCmd);
        }

        public MessageInfo GetMessageOfForward(int messageId, string userName)
        {
            string sqlCmd = "SELECT * FROM PE_Message WHERE (Incept = @UserName OR Sender = @UserName) AND MessageID = @ID";
            return GetMessage(messageId, userName, sqlCmd);
        }

        public MessageInfo GetMessageOfReply(int messageId, string userName)
        {
            string sqlCmd = "SELECT * FROM PE_Message WHERE Incept = @UserName AND MessageID = @ID";
            return GetMessage(messageId, userName, sqlCmd);
        }

        public int GetUnreadMessageFirstId(string userName)
        {
            string strSql = "SELECT MAX(MessageID) FROM PE_Message WHERE incept = @UserName AND IsSend = 1 AND IsRead = 0 AND isdelinbox = 0";
            object obj2 = DBHelper.ExecuteScalarSql(strSql, new Parameters("@UserName", DbType.String, userName));
            if (obj2 != null)
            {
                return (int) obj2;
            }
            return 0;
        }

        public IList<string> GetUserNameList(string groupId)
        {
            string str;
            IList<string> list = new List<string>();
            Database database = DatabaseFactory.CreateDatabase();
            if (string.IsNullOrEmpty(groupId))
            {
                str = "SELECT UserName FROM PE_Users";
            }
            else
            {
                str = "SELECT UserName FROM PE_Users WHERE GroupID IN ( " + DBHelper.ToValidId(groupId) + " )";
            }
            DbCommand sqlStringCommand = database.GetSqlStringCommand(str);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(sqlStringCommand)))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString("UserName"));
                }
            }
            return list;
        }

        private static MessageInfo MessageFromrdr(NullableDataReader rdr)
        {
            MessageInfo info = new MessageInfo();
            info.MessageId = rdr.GetInt32("MessageId");
            info.Title = rdr.GetString("Title");
            info.Content = rdr.GetString("Content");
            info.Sender = rdr.GetString("Sender");
            info.Incept = rdr.GetString("Incept");
            info.SendTime = rdr.GetDateTime("SendTime");
            info.IsSend = rdr.GetInt32("IsSend");
            info.IsDelInbox = rdr.GetInt32("IsDelInbox");
            info.IsDelSendbox = rdr.GetInt32("IsDelSendbox");
            info.IsRead = rdr.GetInt32("IsRead");
            return info;
        }

        public int UnreadMessageCount(string userName)
        {
            string strSql = "SELECT COUNT(*) FROM PE_Message WHERE incept = @UserName AND IsSend = 1 AND IsRead = 0 AND isdelinbox = 0";
            object obj2 = DBHelper.ExecuteScalarSql(strSql, new Parameters("@UserName", DbType.String, userName));
            if (obj2 != null)
            {
                return (int) obj2;
            }
            return 0;
        }

        public bool Update(MessageInfo messageInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Title", DbType.String, messageInfo.Title);
            cmdParams.AddInParameter("@Content", DbType.String, messageInfo.Content);
            cmdParams.AddInParameter("@Sender", DbType.String, messageInfo.Sender);
            cmdParams.AddInParameter("@Incept", DbType.String, messageInfo.Incept);
            cmdParams.AddInParameter("@SendTime", DbType.DateTime, messageInfo.SendTime);
            cmdParams.AddInParameter("@IsSend", DbType.Int32, messageInfo.IsSend);
            cmdParams.AddInParameter("@IsRead", DbType.Int32, messageInfo.IsRead);
            cmdParams.AddInParameter("@MessageId", DbType.Int32, messageInfo.MessageId);
            return DBHelper.ExecuteSql("UPDATE PE_Message SET Title = @Title, Content = @Content, Sender = @Sender, Incept = @Incept, SendTime = @SendTime, IsSend = @IsSend, IsRead = @IsRead WHERE MessageId = @MessageId", cmdParams);
        }

        public bool UpdateState(int messageId)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Message SET IsRead = 1 WHERE MessageID = @MessageId", new Parameters("@MessageId", DbType.Int32, messageId));
        }
    }
}


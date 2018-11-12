namespace EasyOne.SqlServerDal.Contents
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class Comment : IComment
    {
        private int m_TotalOfCommentInfo;

        public bool Add(CommentInfo commentInfo)
        {
            Parameters parms = new Parameters();
            GetParameters(commentInfo, parms);
            return DBHelper.ExecuteSql("INSERT INTO [PE_Comment](CommentId, GeneralId, TopicId, NodeId, CommentTitle, [Content], UpdateDateTime, Score, [Position], Status, Agree, Oppose, Neutral, IP, IsElite, IsPrivate, UserName, Face, Email, ReplyUserName) VALUES (@CommentId, @GeneralId, @TopicId, @NodeId, @CommentTitle, @Content, @UpdateDateTime, @Score, @Position, @Status, 0, 0, 0, @IP, 0, @IsPrivate, @UserName, @Face, @Email, @ReplyUserName)", parms);
        }

        public bool AdministratorReply(CommentInfo commentInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CommentId", DbType.Int32, commentInfo.CommentId);
            cmdParams.AddInParameter("@Reply", DbType.String, commentInfo.Reply);
            cmdParams.AddInParameter("@ReplyAdmin", DbType.String, commentInfo.ReplyAdmin);
            cmdParams.AddInParameter("@ReplyDateTime", DbType.DateTime, commentInfo.ReplyDateTime);
            cmdParams.AddInParameter("@ReplyIsPrivate", DbType.Boolean, commentInfo.ReplyIsPrivate);
            return DBHelper.ExecuteSql("UPDATE PE_Comment SET Reply = @Reply, ReplyAdmin = @ReplyAdmin, ReplyDateTime = @ReplyDateTime, ReplyIsPrivate = @ReplyIsPrivate WHERE CommentId = @CommentId", cmdParams);
        }

        private static CommentInfo CommentInfoAndUserInfoFromDataReader(NullableDataReader dr)
        {
            CommentInfo info = CommentInfoCommonFromDataReader(dr);
            info.UserFace = dr.GetString("UserFace");
            info.UserPoint = dr.GetInt32("UserPoint");
            info.UserExp = dr.GetInt32("UserExp");
            info.FaceHeight = dr.GetInt32("FaceHeight");
            info.FaceWidth = dr.GetInt32("FaceWidth");
            info.UserRegTime = dr.GetDateTime("RegTime");
            info.UserId = dr.GetInt32("UserID");
            info.Email = dr.GetString("Email");
            return info;
        }

        private static CommentInfo CommentInfoCommonFromDataReader(NullableDataReader dr)
        {
            CommentInfo info = new CommentInfo();
            info.CommentId = dr.GetInt32("CommentId");
            info.Agree = dr.GetInt32("Agree");
            info.CommentTitle = dr.GetString("CommentTitle");
            info.Content = dr.GetString("Content");
            info.GeneralId = dr.GetInt32("GeneralId");
            info.IP = dr.GetString("IP");
            info.IsElite = dr.GetBoolean("IsElite");
            info.IsPrivate = dr.GetBoolean("IsPrivate");
            info.Neutral = dr.GetInt32("Neutral");
            info.Oppose = dr.GetInt32("Oppose");
            info.Position = dr.GetInt32("Position");
            info.Reply = dr.GetString("Reply");
            info.ReplyAdmin = dr.GetString("ReplyAdmin");
            info.ReplyDateTime = dr.GetDateTime("ReplyDateTime");
            info.ReplyIsPrivate = dr.GetBoolean("ReplyIsPrivate");
            info.Status = dr.GetBoolean("Status");
            info.TopicId = dr.GetInt32("TopicId");
            info.UpdateDateTime = dr.GetDateTime("UpdateDateTime");
            info.UserName = dr.GetString("UserName");
            info.NodeId = dr.GetInt32("NodeId");
            info.Score = dr.GetInt32("Score");
            info.Face = dr.GetString("Face");
            info.ReplyUserName = dr.GetString("ReplyUserName");
            return info;
        }

        private static CommentInfo CommentInfoFromDataReader(NullableDataReader dr)
        {
            CommentInfo info = CommentInfoCommonFromDataReader(dr);
            info.Email = dr.GetString("Email");
            return info;
        }

        public bool Delete(int commentId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CommentId", DbType.Int32, commentId);
            return DBHelper.ExecuteSql("DELETE FROM PE_Comment WHERE CommentId = @CommentId", cmdParams);
        }

        public bool Delete(string commentIds)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Comment WHERE CommentId IN (" + DBHelper.ToValidId(commentIds) + ")");
        }

        public bool Delete(int commentId, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CommentId", DbType.Int32, commentId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExecuteSql("DELETE FROM PE_Comment WHERE CommentId = @CommentId AND UserName = @UserName", cmdParams);
        }

        public bool Elite(int commentId, bool isElite)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CommentId", DbType.Int32, commentId);
            cmdParams.AddInParameter("@IsElite", DbType.Boolean, isElite);
            return DBHelper.ExecuteSql("UPDATE PE_Comment SET IsElite = @IsElite WHERE CommentId = @CommentId", cmdParams);
        }

        public CommentInfo GetCommentInfo(int commentId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CommentId", DbType.Int32, commentId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Comment WHERE CommentId = @CommentId", cmdParams))
            {
                if (reader.Read())
                {
                    return CommentInfoFromDataReader(reader);
                }
                return new CommentInfo(true);
            }
        }

        public int GetCountByStatus(int status)
        {
            string str = "";
            switch (status)
            {
                case 0:
                    str = " 1 = 1 ";
                    break;

                case 1:
                    str = " PE_Comment.Status = 1 ";
                    break;

                case 2:
                    str = " PE_Comment.Status = 0 ";
                    break;
            }
            Parameters cmdParams = new Parameters();
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_Comment WHERE " + str, cmdParams));
        }

        public CommentInfo GetExtendCommentInfo(int commentId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CommentId", DbType.Int32, commentId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Comment LEFT JOIN PE_Users ON PE_Comment.UserName = PE_Users.UserName WHERE PE_Comment.CommentId = @CommentId", cmdParams))
            {
                if (reader.Read())
                {
                    return CommentInfoAndUserInfoFromDataReader(reader);
                }
                return new CommentInfo(true);
            }
        }

        public IList<CommentInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return this.GetList(startRowIndexId, maxNumberRows, "DESC", "");
        }

        public IList<CommentInfo> GetList(int startRowIndexId, int maxNumberRows, int generalId)
        {
            string filter = " GeneralId = " + generalId.ToString() + " ";
            return this.GetList(startRowIndexId, maxNumberRows, "DESC", filter);
        }

        public IList<CommentInfo> GetList(int startRowIndexId, int maxNumberRows, int generalId, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" PE_Comment.GeneralId = " + generalId.ToString() + " ");
            switch (type)
            {
                case 1:
                    builder.Append("AND PE_Comment.Status = 1 ");
                    break;

                case 2:
                    builder.Append("AND PE_Comment.Status = 0 ");
                    break;
            }
            return this.GetList(startRowIndexId, maxNumberRows, "ASC", builder.ToString());
        }

        public IList<CommentInfo> GetList(int startRowIndexId, int maxNumberRows, string sorts, string filter)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Statistics_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "GeneralId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, sorts);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Comment LEFT JOIN PE_Users ON PE_Comment.UserName = PE_Users.UserName");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, filter);
            database.AddInParameter(storedProcCommand, "@ID", DbType.String, "CommentId");
            database.AddInParameter(storedProcCommand, "@Group", DbType.String, " ");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            List<CommentInfo> list = new List<CommentInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(CommentInfoAndUserInfoFromDataReader(reader));
                }
            }
            this.m_TotalOfCommentInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<CommentInfo> GetListByNodeId(int startRowIndexId, int maxNumberRows, int nodeId, int type)
        {
            StringBuilder builder = new StringBuilder();
            if (nodeId != 0)
            {
                builder.Append(" PE_Comment.NodeId = " + nodeId.ToString() + " ");
            }
            if (builder.Length > 0)
            {
                builder.Append(" AND ");
            }
            switch (type)
            {
                case 0:
                    builder.Append(" 1 = 1 ");
                    break;

                case 1:
                    builder.Append(" PE_Comment.Status = 1 ");
                    break;

                case 2:
                    builder.Append(" PE_Comment.Status = 0 ");
                    break;
            }
            return this.GetList(startRowIndexId, maxNumberRows, "DESC", builder.ToString());
        }

        private static void GetParameters(CommentInfo commentInfo, Parameters parms)
        {
            parms.AddInParameter("@CommentId", DbType.Int32, commentInfo.CommentId);
            parms.AddInParameter("@GeneralId", DbType.Int32, commentInfo.GeneralId);
            parms.AddInParameter("@TopicId", DbType.Int32, commentInfo.TopicId);
            parms.AddInParameter("@NodeId", DbType.Int32, commentInfo.NodeId);
            parms.AddInParameter("@CommentTitle", DbType.String, commentInfo.CommentTitle);
            parms.AddInParameter("@Content", DbType.String, commentInfo.Content);
            parms.AddInParameter("@UpdateDateTime", DbType.DateTime, commentInfo.UpdateDateTime);
            parms.AddInParameter("@Position", DbType.Int32, commentInfo.Position);
            parms.AddInParameter("@Score", DbType.Int32, commentInfo.Score);
            parms.AddInParameter("@Status", DbType.Boolean, commentInfo.Status);
            parms.AddInParameter("@Ip", DbType.String, commentInfo.IP);
            parms.AddInParameter("@IsPrivate", DbType.Boolean, commentInfo.IsPrivate);
            parms.AddInParameter("@UserName", DbType.String, commentInfo.UserName);
            parms.AddInParameter("@Face", DbType.String, commentInfo.Face);
            parms.AddInParameter("@Email", DbType.String, commentInfo.Email);
            parms.AddInParameter("@ReplyUserName", DbType.String, commentInfo.ReplyUserName);
        }

        public int GetTotalOfCommentInfo()
        {
            return this.m_TotalOfCommentInfo;
        }

        public IList<CommentInfo> GetUserCommentList(int startRowIndexId, int maxNumberRows, int nodeId, string userName)
        {
            StringBuilder builder = new StringBuilder();
            if (nodeId != 0)
            {
                builder.Append(" PE_Comment.NodeId = " + nodeId.ToString() + " ");
            }
            if (builder.Length > 0)
            {
                builder.Append(" AND ");
            }
            builder.Append(" PE_Users.UserName = '" + DBHelper.FilterBadChar(userName) + "'");
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Statistics_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "GeneralId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Comment LEFT JOIN PE_Users ON PE_Comment.UserName = PE_Users.UserName");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            database.AddInParameter(storedProcCommand, "@ID", DbType.String, "CommentId");
            database.AddInParameter(storedProcCommand, "@Group", DbType.String, " ");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            List<CommentInfo> list = new List<CommentInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(CommentInfoAndUserInfoFromDataReader(reader));
                }
            }
            this.m_TotalOfCommentInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int MaxCommentId()
        {
            return DBHelper.GetMaxId("PE_Comment", "CommentId");
        }

        public int ScoreCount(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            int num = 0;
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT Score FROM PE_Comment WHERE GeneralId = @GeneralId", cmdParams))
            {
                while (reader.Read())
                {
                    num += reader.GetInt32("Score");
                }
            }
            return num;
        }

        public bool SetStatus(int commentId, bool status)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CommentId", DbType.Int32, commentId);
            cmdParams.AddInParameter("@Status", DbType.Boolean, status);
            return DBHelper.ExecuteSql("UPDATE PE_Comment SET Status = @Status WHERE CommentId = @CommentId", cmdParams);
        }

        public bool Update(CommentInfo commentInfo)
        {
            Parameters parms = new Parameters();
            GetParameters(commentInfo, parms);
            parms.AddInParameter("@Reply", DbType.String, commentInfo.Reply);
            parms.AddInParameter("@ReplyAdmin", DbType.String, commentInfo.ReplyAdmin);
            parms.AddInParameter("@ReplyDateTime", DbType.DateTime, commentInfo.ReplyDateTime);
            parms.AddInParameter("@ReplyIsPrivate", DbType.Boolean, commentInfo.ReplyIsPrivate);
            return DBHelper.ExecuteSql("UPDATE PE_Comment SET CommentId = @CommentId, GeneralId = @GeneralId, TopicId = @TopicId, NodeId = @NodeId, CommentTitle = @CommentTitle, Email = @Email, Content = @Content, UpdateDateTime = @UpdateDateTime, Position = @Position, Score = @Score, Status = @Status, Ip = @Ip, IsPrivate = @IsPrivate, UserName = @UserName, Face = @Face, Reply = @Reply, ReplyAdmin = @ReplyAdmin, ReplyDateTime = @ReplyDateTime, ReplyIsPrivate = @ReplyIsPrivate, ReplyUserName = @ReplyUserName WHERE CommentId = @CommentId", parms);
        }
    }
}


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

    public class CommentPKZone : ICommentPKZone
    {
        private int m_TotalOfCommentPKZoneInfo;

        public void Add(CommentPKZoneInfo commentPKZoneInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@PKId", DbType.Int32, commentPKZoneInfo.PKId);
            cmdParams.AddInParameter("@CommentId", DbType.Int32, commentPKZoneInfo.CommentId);
            cmdParams.AddInParameter("@Title", DbType.String, commentPKZoneInfo.Title);
            cmdParams.AddInParameter("@Content", DbType.String, commentPKZoneInfo.Content);
            cmdParams.AddInParameter("@IP", DbType.String, commentPKZoneInfo.IP);
            cmdParams.AddInParameter("@UpdateTime", DbType.DateTime, commentPKZoneInfo.UpdateTime);
            cmdParams.AddInParameter("@UserName", DbType.String, commentPKZoneInfo.UserName);
            cmdParams.AddInParameter("@Position", DbType.Int32, commentPKZoneInfo.Position);
            DBHelper.ExecuteProc("PR_Contents_CommentPK_Add", cmdParams);
        }

        private static CommentPKZoneInfo CommentPKZoneInfoFromDataReader(NullableDataReader rdr)
        {
            CommentPKZoneInfo info = new CommentPKZoneInfo();
            info.PKId = rdr.GetInt32("PKId");
            info.CommentId = rdr.GetInt32("CommentId");
            info.Title = rdr.GetString("Title");
            info.Content = rdr.GetString("Content");
            info.IP = rdr.GetString("IP");
            info.Position = rdr.GetInt32("Position");
            info.UpdateTime = rdr.GetDateTime("UpdateTime");
            info.UserName = rdr.GetString("UserName");
            return info;
        }

        public void Delete(int pkId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@PKId", DbType.Int32, pkId);
            DBHelper.ExecuteSql("DELETE FROM PE_CommentPK WHERE PKId = @PKId", cmdParams);
        }

        public void DeleteByCommentId(int commentId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CommentId", DbType.Int32, commentId);
            DBHelper.ExecuteSql("DELETE FROM PE_CommentPK WHERE CommentId = @CommentId", cmdParams);
        }

        public IList<CommentPKZoneInfo> GetList(int startRowIndexId, int maxNumberRows, int commentId)
        {
            string filter = " CommentId = " + commentId.ToString() + " ";
            return this.GetList(startRowIndexId, maxNumberRows, filter);
        }

        private IList<CommentPKZoneInfo> GetList(int startRowIndexId, int maxNumberRows, string filter)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "PKId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommentPK");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, filter);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            List<CommentPKZoneInfo> list = new List<CommentPKZoneInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(CommentPKZoneInfoFromDataReader(reader));
                }
            }
            this.m_TotalOfCommentPKZoneInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<CommentPKZoneInfo> GetList(int startRowIndexId, int maxNumberRows, int commentId, int position)
        {
            string filter = " CommentId = " + commentId.ToString() + " AND Position = " + position.ToString() + " ";
            return this.GetList(startRowIndexId, maxNumberRows, filter);
        }

        public CommentPKZoneInfo GetModelInfo(int pkId)
        {
            CommentPKZoneInfo info = null;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@PKId", DbType.Int32, pkId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_CommentPK WHERE PKId = @PKId", cmdParams))
            {
                if (reader.Read())
                {
                    info = CommentPKZoneInfoFromDataReader(reader);
                }
            }
            return info;
        }

        public int GetPKCount(int commentId, int position)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CommentId", DbType.Int32, commentId);
            cmdParams.AddInParameter("@Position", DbType.Int32, position);
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT COUNT(Position) FROM PE_CommentPK WHERE Position = @Position AND CommentId = @CommentId", cmdParams));
        }

        public int GetTotalOfCommentPKZoneInfo()
        {
            return this.m_TotalOfCommentPKZoneInfo;
        }

        public int MaxCommentPKZoneId()
        {
            return DBHelper.GetMaxId("PE_CommentPK", "PKId");
        }
    }
}


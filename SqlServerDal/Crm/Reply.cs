namespace EasyOne.SqlServerDal.Crm
{
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Reply : IReply
    {
        public bool Add(ReplyInfo info)
        {
            Parameters cmdParams = GetParameters(info);
            cmdParams.AddInParameter("@QuestionID", DbType.Int32, info.QuestionId);
            return DBHelper.ExecuteSql("INSERT INTO PE_Reply(ID, QuestionID, ReplyCreator, ReplyTime, ReplyContent) VALUES (@ID, @QuestionID, @ReplyCreator, @ReplyTime, @ReplyContent)", cmdParams);
        }

        public bool DeleteById(int id)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Reply WHERE ID = @ID", new Parameters("@ID", DbType.Int32, id));
        }

        public bool DeleteByQuestionId(int questionId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Reply WHERE QuestionID = @QuestionID", new Parameters("@QuestionID", DbType.Int32, questionId));
        }

        public bool DeleteByQuestionId(string questionIdList)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Reply WHERE QuestionID In (" + DBHelper.ToValidId(questionIdList) + ")");
        }

        private static ReplyInfo GetInfobyReader(NullableDataReader rdr)
        {
            ReplyInfo info = new ReplyInfo();
            info.Id = rdr.GetInt32("ID");
            info.QuestionId = rdr.GetInt32("QuestionID");
            info.ReplyCreator = rdr.GetString("ReplyCreator");
            info.ReplyTime = rdr.GetDateTime("ReplyTime");
            info.ReplyContent = rdr.GetString("ReplyContent");
            return info;
        }

        public ReplyInfo GetLastReplyById(int questionId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 1 ReplyCreator, ReplyTime FROM PE_Reply WHERE QuestionID = @ID ORDER BY ReplyTime DESC", new Parameters("@ID", DbType.Int32, questionId)))
            {
                if (reader.Read())
                {
                    ReplyInfo info = new ReplyInfo();
                    info.ReplyCreator = reader.GetString("ReplyCreator");
                    info.ReplyTime = reader.GetDateTime("ReplyTime");
                    return info;
                }
                return new ReplyInfo(true);
            }
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Reply", "ID");
        }

        private static Parameters GetParameters(ReplyInfo info)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ID", DbType.Int32, info.Id);
            parameters.AddInParameter("@ReplyCreator", DbType.String, info.ReplyCreator);
            parameters.AddInParameter("@ReplyTime", DbType.DateTime, info.ReplyTime);
            parameters.AddInParameter("@ReplyContent", DbType.String, info.ReplyContent);
            return parameters;
        }

        public ReplyInfo GetReplyById(int id)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Reply WHERE ID = @ID", new Parameters("@ID", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    return GetInfobyReader(reader);
                }
                return new ReplyInfo(true);
            }
        }

        public IList<ReplyInfo> GetReplyByQuestionId(int questionId)
        {
            IList<ReplyInfo> list = new List<ReplyInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Reply WHERE QuestionID = @QuestionID ORDER BY ID", new Parameters("@QuestionID", DbType.Int32, questionId)))
            {
                while (reader.Read())
                {
                    list.Add(GetInfobyReader(reader));
                }
            }
            return list;
        }

        public DataTable GetReplyStatistic()
        {
            DataTable table = new DataTable();
            table.Columns.Add("AdminName", typeof(string));
            table.Columns.Add("QuestionCount", typeof(int));
            table.Columns.Add("ReplyCount", typeof(int));
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT ReplyCreator, count(DISTINCT QuestionID)as QuestionCount, Count(QuestionID) AS ReplyCount FROM PE_Reply WHERE ReplyCreator IN (SELECT AdminName FROM PE_Admin) Group by replyCreator ORDER BY QuestionCount DESC"))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["AdminName"] = reader.GetString("ReplyCreator");
                    row["QuestionCount"] = reader.GetInt32("QuestionCount");
                    row["ReplyCount"] = reader.GetInt32("ReplyCount");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public bool HasOtherReplyer(int questionId)
        {
            return DBHelper.ExistsSql("SELECT R.ID FROM PE_Reply R INNER JOIN PE_Question Q ON R.QuestionID = Q.ID WHERE Q.ID = @ID AND R.ReplyCreator != Q.QuestionCreator", new Parameters("@ID", DbType.Int32, questionId));
        }

        public bool Update(ReplyInfo info)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Reply SET ReplyCreator = @ReplyCreator, ReplyTime = @ReplyTime, ReplyContent = @ReplyContent WHERE ID = @ID", GetParameters(info));
        }
    }
}


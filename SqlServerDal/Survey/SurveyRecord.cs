namespace EasyOne.SqlServerDal.Survey
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.IDal.Survey;
    using EasyOne.Model.Survey;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class SurveyRecord : ISurveyRecord
    {
        private int m_TotalOfSurveyRecord;

        public bool Delete(string recordId, int surveyId)
        {
            return DBHelper.ExecuteSql(string.Concat(new object[] { "DELETE FROM PE_SurveyRecord", surveyId, " WHERE RecordID IN (", DBHelper.ToValidId(recordId), ")" }));
        }

        public void DeleteTable(int surveyId)
        {
            DBHelper.ExecuteNonQuery(CommandType.Text, string.Format("if object_ID('{0}') is not null drop table [{0}]", "PE_SurveyRecord" + surveyId.ToString()), null);
        }

        public IList<SurveyRecordInfo> GetList(int startRowIndexId, int maxNumberRows, int surveyId, int recordId)
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
            database.SetParameterValue(storedProcCommand, "@SortColumn", "RecordID");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "*");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_SurveyRecord" + surveyId);
            if (recordId > 0)
            {
                database.SetParameterValue(storedProcCommand, "@Filter", "RecordID = " + recordId);
            }
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            IList<SurveyRecordInfo> list = new List<SurveyRecordInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                DataTable dataTable = new DataTable();
                bool flag = false;
                while (reader.Read())
                {
                    if (!flag)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.GetName(i).Contains("Q") || reader.GetName(i).Contains("Input"))
                            {
                                dataTable.Columns.Add(reader.GetName(i));
                            }
                        }
                        flag = true;
                    }
                    list.Add(SurveyRecordFromrdr(reader, dataTable));
                }
            }
            this.m_TotalOfSurveyRecord = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<string> GetQuestionAnswer(int surveyId, string questionId)
        {
            IList<string> list = new List<string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_SurveyRecord" + surveyId))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(questionId.Trim()));
                }
            }
            return list;
        }

        public int GetTotalOfSurveyRecord()
        {
            return this.m_TotalOfSurveyRecord;
        }

        public int GetTotalOfSurveyRecord(int surveyId)
        {
            object input = DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_SurveyRecord" + surveyId);
            if (input != null)
            {
                return DataConverter.CLng(input);
            }
            return 0;
        }

        public bool SaveSurveyRecord(SurveyRecordInfo surveyrecordinfo)
        {
            int count = surveyrecordinfo.Answer.Rows.Count;
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO PE_SurveyRecord");
            builder.Append(surveyrecordinfo.SurveyId.ToString());
            builder.Append(" (");
            builder.Append("SurveyID");
            builder.Append(", UserName");
            builder.Append(", IP");
            builder.Append(", SubmitTime");
            if (count != 0)
            {
                for (int i = 0; i < count; i++)
                {
                    builder.Append(", Q" + surveyrecordinfo.Answer.Rows[i]["QuestionId"]);
                    if (((DataConverter.CLng(surveyrecordinfo.Answer.Rows[i]["QuestionType"]) == 2) || (DataConverter.CLng(surveyrecordinfo.Answer.Rows[i]["QuestionType"]) == 3)) && (DataConverter.CLng(surveyrecordinfo.Answer.Rows[i]["InputType"]) != 0))
                    {
                        builder.Append(", Q" + surveyrecordinfo.Answer.Rows[i]["QuestionId"] + "Input");
                    }
                }
            }
            builder.Append(")");
            builder.Append(" VALUES(");
            builder.Append(surveyrecordinfo.SurveyId.ToString());
            builder.Append(", '");
            builder.Append(DBHelper.FilterBadChar(surveyrecordinfo.UserName));
            builder.Append("', '");
            builder.Append(DBHelper.FilterBadChar(surveyrecordinfo.IP));
            builder.Append("', '");
            builder.Append(surveyrecordinfo.SubmitTime.ToString());
            builder.Append("'");
            if (count != 0)
            {
                for (int j = 0; j < count; j++)
                {
                    builder.Append(", '" + surveyrecordinfo.Answer.Rows[j]["Option"] + "'");
                    if (((DataConverter.CLng(surveyrecordinfo.Answer.Rows[j]["QuestionType"]) == 2) || (DataConverter.CLng(surveyrecordinfo.Answer.Rows[j]["QuestionType"]) == 3)) && (DataConverter.CLng(surveyrecordinfo.Answer.Rows[j]["InputType"]) != 0))
                    {
                        builder.Append(", '" + surveyrecordinfo.Answer.Rows[j]["Input"] + "'");
                    }
                }
            }
            builder.Append(")");
            return DBHelper.ExecuteSql(builder.ToString());
        }

        private static SurveyRecordInfo SurveyRecordFromrdr(NullableDataReader rdr, DataTable dataTable)
        {
            SurveyRecordInfo info = new SurveyRecordInfo();
            info.RecordId = rdr.GetInt32("RecordID");
            info.SurveyId = rdr.GetInt32("SurveyID");
            info.UserName = rdr.GetString("UserName");
            info.IP = rdr.GetString("IP");
            info.SubmitTime = rdr.GetDateTime("SubmitTime");
            DataRow row = dataTable.NewRow();
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                if (rdr.GetName(i).Contains("Q") || rdr.GetName(i).Contains("Input"))
                {
                    row[rdr.GetName(i)] = rdr[i];
                }
            }
            dataTable.Rows.Add(row);
            info.Answer = dataTable;
            return info;
        }
    }
}


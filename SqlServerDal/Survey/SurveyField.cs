namespace EasyOne.SqlServerDal.Survey
{
    using EasyOne.IDal.Survey;
    using EasyOne.Model.Survey;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;
    using System.Text;

    public class SurveyField : ISurveyField
    {
        public bool Add(int surveyId, string questionInfoList)
        {
            return UpdateQuestionField(surveyId, questionInfoList, "UPDATE PE_Survey SET QuestionMaxId = QuestionMaxId + 1, QuestionField = @QuestionField WHERE SurveyId = @SurveyId");
        }

        public bool AddFieldToTable(SurveyFieldInfo surveyFieldInfo, string tableName)
        {
            string addColumnToTableSql = GetAddColumnToTableSql(surveyFieldInfo, tableName);
            try
            {
                DBHelper.ExecuteNonQuerySql(addColumnToTableSql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int surveyId, string questionInfoList)
        {
            return UpdateQuestionField(surveyId, questionInfoList, "UPDATE PE_Survey SET QuestionField = @QuestionField WHERE SurveyId = @SurveyId");
        }

        public bool DeleteColumn(int questionId, int surveyId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ALTER TABLE ");
            builder.Append("PE_SurveyRecord" + surveyId);
            builder.Append(" DROP COLUMN ");
            builder.Append("Q" + questionId);
            try
            {
                DBHelper.ExecuteNonQuerySql(builder.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteInputColumn(int questionId, int surveyId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ALTER TABLE ");
            builder.Append("PE_SurveyRecord" + surveyId);
            builder.Append(" DROP COLUMN ");
            builder.Append("Q" + questionId + "Input");
            try
            {
                DBHelper.ExecuteNonQuerySql(builder.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetAddColumnToTableSql(SurveyFieldInfo surveyFieldInfo, string tableName)
        {
            int questionType = surveyFieldInfo.QuestionType;
            StringBuilder builder = new StringBuilder();
            builder.Append("ALTER TABLE [");
            builder.Append(DBHelper.FilterBadChar(tableName));
            builder.Append("] ADD [");
            builder.Append("Q" + surveyFieldInfo.QuestionId);
            builder.Append("] ");
            switch (questionType)
            {
                case 0:
                    builder.Append("[nvarchar] (255)");
                    break;

                case 1:
                    builder.Append("[ntext]");
                    break;

                case 2:
                    builder.Append("[nvarchar] (5)");
                    switch (surveyFieldInfo.InputType)
                    {
                        case 1:
                            builder.Append(", [");
                            builder.Append("Q" + surveyFieldInfo.QuestionId + "Input");
                            builder.Append("] ");
                            builder.Append("[nvarchar] (255)");
                            break;

                        case 2:
                            builder.Append(", [");
                            builder.Append("Q" + surveyFieldInfo.QuestionId + "Input");
                            builder.Append("] ");
                            builder.Append("[ntext]");
                            break;
                    }
                    break;

                case 3:
                    builder.Append("[nvarchar] (50)");
                    switch (surveyFieldInfo.InputType)
                    {
                        case 1:
                            builder.Append(", [");
                            builder.Append("Q" + surveyFieldInfo.QuestionId + "Input");
                            builder.Append("] ");
                            builder.Append("[nvarchar] (255)");
                            break;

                        case 2:
                            builder.Append(", [");
                            builder.Append("Q" + surveyFieldInfo.QuestionId + "Input");
                            builder.Append("] ");
                            builder.Append("[ntext]");
                            break;
                    }
                    break;

                case 4:
                    builder.Append("[nvarchar] (5)");
                    break;

                case 5:
                    builder.Append("[nvarchar] (50)");
                    break;

                case 6:
                    builder.Append("[nvarchar] (50)");
                    break;

                case 7:
                    builder.Append("[nvarchar] (5)");
                    break;

                case 8:
                    builder.Append("[nvarchar] (255)");
                    break;

                case 9:
                    builder.Append("[nvarchar] (255)");
                    break;
            }
            return builder.ToString();
        }

        public string GetXmlFieldBySurveyId(int surveyId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SurveyId", DbType.Int32, surveyId);
            object obj2 = DBHelper.ExecuteScalarSql("SELECT QuestionField FROM PE_Survey WHERE SurveyId = @SurveyId", cmdParams);
            if (obj2 == null)
            {
                return null;
            }
            return Convert.ToString(obj2);
        }

        public bool Update(int surveyId, string questionInfoList)
        {
            return UpdateQuestionField(surveyId, questionInfoList, "UPDATE PE_Survey SET QuestionField = @QuestionField WHERE SurveyId = @SurveyId");
        }

        private static bool UpdateQuestionField(int surveyId, string questionInfoList, string sql)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@QuestionField", DbType.String, questionInfoList);
            cmdParams.AddInParameter("@SurveyId", DbType.Int32, surveyId);
            return DBHelper.ExecuteSql(sql, cmdParams);
        }
    }
}


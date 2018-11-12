namespace EasyOne.SqlServerDal.Survey
{
    using EasyOne.IDal.Survey;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SurveyVote : ISurveyVote
    {
        public bool Add(int surveyId, int questionId, int optionCount)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SurveyId", DbType.Int32, surveyId);
            cmdParams.AddInParameter("@QuestionId", DbType.Int32, questionId);
            cmdParams.AddInParameter("@OptionCount", DbType.Int32, optionCount);
            return DBHelper.ExecuteProc("PR_Survey_SurveyVote_Add", cmdParams);
        }

        public bool Delete(int surveyId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_SurveyVote WHERE SurveyId = @SurveyId", new Parameters("@SurveyId", DbType.Int32, surveyId));
        }

        public bool Delete(string surveyIdList)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_SurveyVote WHERE SurveyId IN ( " + DBHelper.ToValidId(surveyIdList) + " ) ");
        }

        public bool Delete(int surveyId, int questionId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SurveyId", DbType.Int32, surveyId);
            cmdParams.AddInParameter("@QuestionId", DbType.Int32, questionId);
            return DBHelper.ExecuteSql("DELETE FROM PE_SurveyVote WHERE SurveyId = @SurveyId AND QuestionId = @QuestionId", cmdParams);
        }

        public bool Delete(int surveyId, string questionIdList)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_SurveyVote WHERE SurveyId = @SurveyId AND QuestionId IN ( " + DBHelper.ToValidId(questionIdList) + " )", new Parameters("@SurveyId", DbType.Int32, surveyId));
        }

        public IList<int> GetQuestionVoteAmountList(int surveyId, int questionId)
        {
            IList<int> list = new List<int>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SurveyId", DbType.Int32, surveyId);
            cmdParams.AddInParameter("@QuestionId", DbType.Int32, questionId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT VoteAmount FROM PE_SurveyVote WHERE SurveyId = @SurveyId AND QuestionId = @QuestionId", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetInt32(0));
                }
            }
            return list;
        }

        public bool Vote(int surveyId, int questionId, int optionId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SurveyId", DbType.Int32, surveyId);
            cmdParams.AddInParameter("@QuestionId", DbType.Int32, questionId);
            cmdParams.AddInParameter("@OptionId", DbType.Int32, optionId);
            return DBHelper.ExecuteSql("UPDATE PE_SurveyVote SET VoteAmount = VoteAmount + 1 WHERE SurveyId = @SurveyId AND QuestionId = @QuestionId AND OptionId = @OptionId", cmdParams);
        }
    }
}


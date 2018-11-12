namespace EasyOne.Survey
{
    using EasyOne.Common;
    using EasyOne.IDal.Survey;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class SurveyVote
    {
        private static readonly ISurveyVote dal = DataAccess.CreateSurveyVote();

        private SurveyVote()
        {
        }

        public static bool Add(int surveyId, int questionId, int optionCount)
        {
            return dal.Add(surveyId, questionId, optionCount);
        }

        public static bool Delete(int surveyId)
        {
            return dal.Delete(surveyId);
        }

        public static bool Delete(string surveyIdList)
        {
            bool flag = false;
            if (DataValidator.IsValidId(surveyIdList))
            {
                flag = dal.Delete(surveyIdList);
            }
            return flag;
        }

        public static bool Delete(int surveyId, int questionId)
        {
            return dal.Delete(surveyId, questionId);
        }

        public static bool Delete(int surveyId, string questionIdList)
        {
            bool flag = false;
            if (DataValidator.IsValidId(questionIdList))
            {
                flag = dal.Delete(surveyId, questionIdList);
            }
            return flag;
        }

        public static IList<int> GetQuestionVoteAmountList(int surveyId, int questionId)
        {
            return dal.GetQuestionVoteAmountList(surveyId, questionId);
        }

        public static bool Vote(int surveyId, int questionId, int optionId)
        {
            return dal.Vote(surveyId, questionId, optionId);
        }
    }
}


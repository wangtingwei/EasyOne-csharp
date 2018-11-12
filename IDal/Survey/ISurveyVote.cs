namespace EasyOne.IDal.Survey
{
    using System;
    using System.Collections.Generic;

    public interface ISurveyVote
    {
        bool Add(int surveyId, int questionId, int optionCount);
        bool Delete(int surveyId);
        bool Delete(string surveyIdList);
        bool Delete(int surveyId, int questionId);
        bool Delete(int surveyId, string questionIdList);
        IList<int> GetQuestionVoteAmountList(int surveyId, int questionId);
        bool Vote(int surveyId, int questionId, int optionId);
    }
}


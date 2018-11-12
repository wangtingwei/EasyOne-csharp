namespace EasyOne.IDal.Survey
{
    using EasyOne.Model.Survey;
    using System;
    using System.Collections.Generic;

    public interface ISurveyRecord
    {
        bool Delete(string recordId, int surveyId);
        void DeleteTable(int surveyId);
        IList<SurveyRecordInfo> GetList(int startRowIndexId, int maxNumberRows, int surveyId, int recordId);
        IList<string> GetQuestionAnswer(int surveyId, string questionId);
        int GetTotalOfSurveyRecord();
        int GetTotalOfSurveyRecord(int surveyId);
        bool SaveSurveyRecord(SurveyRecordInfo surveyrecordinfo);
    }
}


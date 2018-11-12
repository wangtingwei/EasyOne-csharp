namespace EasyOne.IDal.Survey
{
    using EasyOne.Model.Survey;
    using System;
    using System.Collections.Generic;

    public interface ISurveyManager
    {
        bool Add(SurveyInfo surveyInfo);
        bool Delete(string id);
        IList<SurveyInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword);
        int GetMaxId();
        int GetRecordNumByIP(string ip, int surveyId);
        SurveyInfo GetSurveyById(int id);
        int GetTotalOfSurvey();
        bool SetForbid(int surveyId);
        bool SetPassed(int surveyId);
        bool SetPassedOfForbid(int surveyId);
        bool SurveyIdOfPassedExists(int surveyId);
        bool Update(SurveyInfo surveyInfo);
    }
}


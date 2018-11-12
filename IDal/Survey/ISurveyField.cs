namespace EasyOne.IDal.Survey
{
    using EasyOne.Model.Survey;
    using System;

    public interface ISurveyField
    {
        bool Add(int surveyId, string questionInfoList);
        bool AddFieldToTable(SurveyFieldInfo surveyFieldInfo, string tableName);
        bool Delete(int surveyId, string questionInfoList);
        bool DeleteColumn(int questionId, int surveyId);
        bool DeleteInputColumn(int questionId, int surveyId);
        string GetXmlFieldBySurveyId(int surveyId);
        bool Update(int surveyId, string questionInfoList);
    }
}


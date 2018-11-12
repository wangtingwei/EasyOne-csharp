namespace EasyOne.Survey
{
    using EasyOne.Common;
    using EasyOne.IDal.Survey;
    using EasyOne.Model.Survey;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class SurveyRecord
    {
        private static readonly ISurveyRecord dal = DataAccess.CreateSurveyRecord();

        private SurveyRecord()
        {
        }

        public static bool Delete(string recordId, int surveyId)
        {
            return (DataValidator.IsValidId(recordId) && dal.Delete(recordId, surveyId));
        }

        public static void DeleteTable(int surveyId)
        {
            dal.DeleteTable(surveyId);
        }

        public static IList<SurveyRecordInfo> GetList(int startRowIndexId, int maxNumberRows, int surveyId, int recordId)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, surveyId, recordId);
        }

        public static IList<string> GetQuestionAnswer(int surveyId, string questionId)
        {
            return dal.GetQuestionAnswer(surveyId, questionId);
        }

        public static int GetTotalOfSurveyRecord()
        {
            return dal.GetTotalOfSurveyRecord();
        }

        public static int GetTotalOfSurveyRecord(int surveyId)
        {
            return dal.GetTotalOfSurveyRecord(surveyId);
        }

        public static bool SaveSurveyRecord(SurveyRecordInfo surveyrecordinfo)
        {
            return dal.SaveSurveyRecord(surveyrecordinfo);
        }
    }
}


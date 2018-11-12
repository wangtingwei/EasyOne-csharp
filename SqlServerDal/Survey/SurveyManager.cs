namespace EasyOne.SqlServerDal.Survey
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Survey;
    using EasyOne.Model.Survey;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class SurveyManager : ISurveyManager
    {
        private int m_TotalOfSurvey;

        public bool Add(SurveyInfo surveyInfo)
        {
            if (surveyInfo.SurveyId <= 0)
            {
                surveyInfo.SurveyId = this.GetMaxId() + 1;
            }
            return DBHelper.ExecuteProc("PR_Survey_SurveyManager_Add", GetParameters(surveyInfo));
        }

        public bool Delete(string id)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Survey WHERE SurveyID IN (" + DBHelper.ToValidId(id) + ")");
        }

        public IList<SurveyInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<SurveyInfo> list = new List<SurveyInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "SurveyID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Survey");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            string str = "1 = 1";
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (searchType)
                {
                    case 0:
                        str = str + " AND SurveyName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case 1:
                        str = str + " AND DATEDIFF(d, CreateDate, '" + DBHelper.FilterBadChar(keyword) + "') = 0";
                        break;

                    case 2:
                        str = str + " AND DATEDIFF(d, EndTime, '" + DBHelper.FilterBadChar(keyword) + "') = 0";
                        break;
                }
            }
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(SurveyFromrdr(reader));
                }
            }
            this.m_TotalOfSurvey = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Survey", "SurveyId");
        }

        private static Parameters GetParameters(SurveyInfo surveyInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@SurveyID", DbType.Int32, surveyInfo.SurveyId);
            parameters.AddInParameter("@SurveyName", DbType.String, surveyInfo.SurveyName);
            parameters.AddInParameter("@Description", DbType.String, surveyInfo.Description);
            parameters.AddInParameter("@FileName", DbType.String, surveyInfo.FileName);
            parameters.AddInParameter("@IPRepeat", DbType.Int32, surveyInfo.IPRepeat);
            parameters.AddInParameter("@CreateDate", DbType.DateTime, surveyInfo.CreateDate);
            parameters.AddInParameter("@EndTime", DbType.DateTime, surveyInfo.EndTime);
            parameters.AddInParameter("@IsOpen", DbType.Int32, surveyInfo.IsOpen);
            parameters.AddInParameter("@NeedLogin", DbType.Int32, surveyInfo.NeedLogin);
            parameters.AddInParameter("@PresentPoint", DbType.Int32, surveyInfo.PresentPoint);
            parameters.AddInParameter("@LockIPType", DbType.Int32, surveyInfo.LockIPType);
            parameters.AddInParameter("@SetIPLock", DbType.String, surveyInfo.SetIPLock);
            parameters.AddInParameter("@LockUrl", DbType.String, surveyInfo.LockUrl);
            parameters.AddInParameter("@SetPassword", DbType.String, surveyInfo.SetPassword);
            parameters.AddInParameter("@Template", DbType.String, surveyInfo.Template);
            return parameters;
        }

        public int GetRecordNumByIP(string ip, int surveyId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@IP", DbType.String, ip);
            cmdParams.AddInParameter("@SurveyID", DbType.Int32, surveyId);
            return DBHelper.ObjectToInt32(DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_SurveyRecord" + surveyId + " WHERE IP = @IP AND SurveyID = @SurveyID", cmdParams));
        }

        public SurveyInfo GetSurveyById(int id)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Survey WHERE SurveyID = @SurveyID", new Parameters("@SurveyID", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    return SurveyFromrdr(reader);
                }
                return new SurveyInfo(true);
            }
        }

        public int GetTotalOfSurvey()
        {
            return this.m_TotalOfSurvey;
        }

        public bool SetForbid(int surveyId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SurveyID", DbType.Int32, surveyId);
            return DBHelper.ExecuteSql("UPDATE PE_Survey SET IsOpen = 2 WHERE SurveyID = @SurveyID", cmdParams);
        }

        public bool SetPassed(int surveyId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SurveyID", DbType.Int32, surveyId);
            cmdParams.AddInParameter("@TableName", DbType.String, "PE_SurveyRecord" + surveyId);
            return DBHelper.ExecuteProc("PR_Survey_SurveyManager_SetPassed", cmdParams);
        }

        public bool SetPassedOfForbid(int surveyId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SurveyID", DbType.Int32, surveyId);
            return DBHelper.ExecuteSql("UPDATE PE_Survey SET IsOpen = 1 WHERE SurveyID = @SurveyID", cmdParams);
        }

        private static SurveyInfo SurveyFromrdr(NullableDataReader rdr)
        {
            SurveyInfo info = new SurveyInfo();
            info.SurveyId = rdr.GetInt32("SurveyID");
            info.SurveyName = rdr.GetString("SurveyName");
            info.Description = rdr.GetString("Description");
            info.FileName = rdr.GetString("FileName");
            info.IPRepeat = rdr.GetInt32("IPRepeat");
            info.CreateDate = rdr.GetNullableDateTime("CreateDate");
            info.EndTime = rdr.GetNullableDateTime("EndTime");
            info.IsOpen = rdr.GetInt32("IsOpen");
            info.NeedLogin = rdr.GetInt32("NeedLogin");
            info.PresentPoint = rdr.GetInt32("PresentPoint");
            info.LockIPType = rdr.GetInt32("LockIPType");
            info.SetIPLock = rdr.GetString("SetIPLock");
            info.LockUrl = rdr.GetString("LockUrl");
            info.SetPassword = rdr.GetString("SetPassword");
            info.Template = rdr.GetString("Template");
            info.QuestionField = rdr.GetString("QuestionField");
            info.QuestionMaxId = rdr.GetInt32("QuestionMaxId");
            return info;
        }

        public bool SurveyIdOfPassedExists(int surveyId)
        {
            return DBHelper.ExistsSql("SELECT * FROM PE_Survey WHERE SurveyID = @SurveyID AND IsOpen > 0", new Parameters("@SurveyID", DbType.String, surveyId));
        }

        public bool Update(SurveyInfo surveyInfo)
        {
            return DBHelper.ExecuteProc("PR_Survey_SurveyManager_Update", GetParameters(surveyInfo));
        }
    }
}


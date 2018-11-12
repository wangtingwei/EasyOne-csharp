namespace EasyOne.Survey
{
    using EasyOne.Common;
    using EasyOne.IDal.Survey;
    using EasyOne.Model.Survey;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class SurveyManager
    {
        private static readonly ISurveyManager dal = DataAccess.CreateSurvey();

        private SurveyManager()
        {
        }

        public static bool Add(SurveyInfo surveyInfo)
        {
            return dal.Add(surveyInfo);
        }

        private static bool CheckBlackLockIP(double checkIP, string[] arrBlackLockIP)
        {
            foreach (string str in arrBlackLockIP)
            {
                string[] strArray = str.Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                if (((strArray.Length == 2) && (DataConverter.CDouble(strArray[0]) <= checkIP)) && (checkIP <= DataConverter.CDouble(strArray[1])))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckIPLock(string userIP, SurveyInfo surveyInfo)
        {
            bool flag = false;
            string[] strArray = surveyInfo.SetIPLock.Split(new string[] { "|||" }, StringSplitOptions.None);
            if (strArray.Length >= 2)
            {
                double checkIP = StringHelper.EncodeIP(userIP);
                string[] arrWhiteLockIP = strArray[0].Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                string[] arrBlackLockIP = strArray[1].Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                switch (surveyInfo.LockIPType)
                {
                    case 1:
                        return !CheckWhiteLockIP(checkIP, arrWhiteLockIP);

                    case 2:
                        return CheckBlackLockIP(checkIP, arrBlackLockIP);

                    case 3:
                        if (CheckWhiteLockIP(checkIP, arrWhiteLockIP))
                        {
                            if (CheckBlackLockIP(checkIP, arrBlackLockIP))
                            {
                                flag = true;
                            }
                            return flag;
                        }
                        return true;

                    case 4:
                        return (CheckBlackLockIP(checkIP, arrBlackLockIP) && !CheckWhiteLockIP(checkIP, arrWhiteLockIP));
                }
            }
            return flag;
        }

        public static bool CheckLockUrl(string url, SurveyInfo surveyInfo)
        {
            return (string.IsNullOrEmpty(surveyInfo.LockUrl) || StringHelper.FoundCharInArr(surveyInfo.LockUrl, url, "\n"));
        }

        public static bool CheckRepeatIP(string userIP, int surveyId, SurveyInfo surveyInfo)
        {
            return (dal.GetRecordNumByIP(userIP, surveyId) >= surveyInfo.IPRepeat);
        }

        private static bool CheckWhiteLockIP(double checkIP, string[] arrWhiteLockIP)
        {
            foreach (string str in arrWhiteLockIP)
            {
                string[] strArray = str.Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                if (((strArray.Length == 2) && (DataConverter.CDouble(strArray[0]) <= checkIP)) && (checkIP <= DataConverter.CDouble(strArray[1])))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool Delete(string surveyId)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(surveyId) && DataValidator.IsValidId(surveyId))
            {
                flag = dal.Delete(surveyId);
                if (!flag)
                {
                    return flag;
                }
                SurveyVote.Delete(surveyId);
                foreach (string str in surveyId.Split(new char[] { ',' }))
                {
                    SurveyRecord.DeleteTable(DataConverter.CLng(str));
                }
            }
            return flag;
        }

        public static string GetDecodeLockIP(string lockIP, int ipType)
        {
            string[] strArray = lockIP.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                return StringHelper.DecodeLockIP(strArray[ipType]);
            }
            catch (IndexOutOfRangeException)
            {
                return "";
            }
        }

        public static IList<SurveyInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            if (((searchType == 1) || (searchType == 2)) && !string.IsNullOrEmpty(keyword))
            {
                keyword = DataConverter.CDate(keyword).ToString("yyyy-MM-dd");
            }
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, keyword);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static string GetStateName(int surveyState)
        {
            switch (surveyState)
            {
                case 0:
                    return "未启用";

                case 1:
                    return "启用";

                case 2:
                    return "禁用";
            }
            return "未启用";
        }

        public static SurveyInfo GetSurveyById(int id)
        {
            return dal.GetSurveyById(id);
        }

        public static int GetTotalOfSurvey(int searchType, string keyword)
        {
            return dal.GetTotalOfSurvey();
        }

        public static bool SetForbid(int surveyId)
        {
            return dal.SetForbid(surveyId);
        }

        public static bool SetPassed(int surveyId)
        {
            bool flag = false;
            if (dal.SetPassed(surveyId))
            {
                flag = true;
                string questionField = dal.GetSurveyById(surveyId).QuestionField;
                string tableName = "PE_SurveyRecord" + surveyId;
                IList<SurveyFieldInfo> list = new List<SurveyFieldInfo>();
                foreach (SurveyFieldInfo info2 in SurveyField.DeserializeFieldList(questionField))
                {
                    if (!SurveyField.AddFieldToTable(info2, tableName))
                    {
                        return false;
                    }
                }
            }
            return flag;
        }

        public static bool SetPassedOfForbid(int surveyId)
        {
            return dal.SetPassedOfForbid(surveyId);
        }

        public static bool SurveyIdOfPassedExists(int surveyId)
        {
            return dal.SurveyIdOfPassedExists(surveyId);
        }

        public static bool Update(SurveyInfo surveyInfo)
        {
            return dal.Update(surveyInfo);
        }
    }
}


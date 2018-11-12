namespace EasyOne.Survey
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Survey;
    using EasyOne.Model.Survey;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class SurveyField
    {
        private static readonly ISurveyField dal = DataAccess.CreateSurveyField();
        private static readonly string[] QuestionType = new string[] { "单行填空", "多行填空", "单选", "多选", "下拉", "多选列表", "日期和时间", "是/否（复选框）", "数字", "Email" };
        private static Serialize<SurveyFieldInfo> ser = new Serialize<SurveyFieldInfo>();

        private SurveyField()
        {
        }

        public static bool Add(int surveyId, SurveyFieldInfo info)
        {
            IList<SurveyFieldInfo> fieldList = GetFieldList(surveyId);
            info.QuestionId = SurveyManager.GetSurveyById(surveyId).QuestionMaxId + 1;
            fieldList.Add(info);
            if (info.Settings != null)
            {
                SurveyVote.Add(surveyId, info.QuestionId, info.Settings.Count);
            }
            return dal.Add(surveyId, SerializeFieldList(fieldList));
        }

        public static bool AddFieldToTable(SurveyFieldInfo surveyFieldInfo, string tableName)
        {
            return dal.AddFieldToTable(surveyFieldInfo, DataSecurity.FilterBadChar(tableName));
        }

        public static bool BatchDelete(int surveyId, string questionIds)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(questionIds))
            {
                XmlManage manage = XmlManage.Instance(GetXmlFieldBySurveyId(surveyId), XmlType.Content);
                string[] strArray = questionIds.Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    SurveyFieldInfo fieldInfoById = new SurveyFieldInfo();
                    fieldInfoById = GetFieldInfoById(surveyId, DataConverter.CLng(strArray[i]));
                    manage.Remove(GetNodePath(DataConverter.CLng(strArray[i])));
                    DeleteColumn(DataConverter.CLng(strArray[i]), surveyId);
                    if (((fieldInfoById.QuestionType == 2) || (fieldInfoById.QuestionType == 3)) && (fieldInfoById.InputType != 0))
                    {
                        DeleteInputColumn(DataConverter.CLng(strArray[i]), surveyId);
                    }
                }
                flag = dal.Update(surveyId, manage.Xml);
                if (flag)
                {
                    SurveyVote.Delete(surveyId, questionIds);
                }
            }
            return flag;
        }

        public static bool Delete(int surveyId, int questionId)
        {
            SurveyFieldInfo fieldInfoById = new SurveyFieldInfo();
            fieldInfoById = GetFieldInfoById(surveyId, questionId);
            XmlManage manage = XmlManage.Instance(GetXmlFieldBySurveyId(surveyId), XmlType.Content);
            manage.Remove(GetNodePath(questionId));
            bool flag = dal.Delete(surveyId, manage.Xml);
            if (flag)
            {
                SurveyVote.Delete(surveyId, questionId);
                DeleteColumn(questionId, surveyId);
                if ((fieldInfoById.QuestionType != 2) && (fieldInfoById.QuestionType != 3))
                {
                    return flag;
                }
                if (fieldInfoById.InputType != 0)
                {
                    DeleteInputColumn(questionId, surveyId);
                }
            }
            return flag;
        }

        public static bool DeleteColumn(int questionId, int surveyId)
        {
            return dal.DeleteColumn(questionId, surveyId);
        }

        public static bool DeleteInputColumn(int questionId, int surveyId)
        {
            return dal.DeleteInputColumn(questionId, surveyId);
        }

        private static SurveyFieldInfo DeserializeField(string xmlField)
        {
            if (string.IsNullOrEmpty(xmlField))
            {
                return new SurveyFieldInfo();
            }
            return ser.DeserializeField(xmlField);
        }

        public static IList<SurveyFieldInfo> DeserializeFieldList(string xmlFieldList)
        {
            if (string.IsNullOrEmpty(xmlFieldList))
            {
                return new List<SurveyFieldInfo>();
            }
            return ser.DeserializeFieldList(xmlFieldList);
        }

        public static SurveyFieldInfo GetFieldInfoById(int surveyId, int questionId)
        {
            return DeserializeField(XmlManage.Instance(GetXmlFieldBySurveyId(surveyId), XmlType.Content).SelectNode(GetNodePath(questionId)));
        }

        public static IList<SurveyFieldInfo> GetFieldList(int surveyId)
        {
            return DeserializeFieldList(GetXmlFieldBySurveyId(surveyId));
        }

        private static string GetNodePath(int questionId)
        {
            return ("/ArrayOfSurveyFieldInfo/SurveyFieldInfo[@QuestionId=\"" + questionId.ToString() + "\"]");
        }

        public static string GetQuestionType(int type)
        {
            return DataSecurity.GetArrayValue(type, QuestionType);
        }

        private static string GetXmlFieldBySurveyId(int surveyId)
        {
            return dal.GetXmlFieldBySurveyId(surveyId);
        }

        private static string SerializeFieldList(IList<SurveyFieldInfo> infoList)
        {
            return ser.SerializeFieldList(infoList);
        }

        public static bool Update(int surveyId, SurveyFieldInfo info)
        {
            int num = -1;
            IList<SurveyFieldInfo> fieldList = GetFieldList(surveyId);
            for (int i = 0; i < fieldList.Count; i++)
            {
                if (fieldList[i].QuestionId == info.QuestionId)
                {
                    num = i;
                    break;
                }
            }
            if (num == -1)
            {
                return false;
            }
            fieldList[num] = info;
            if (info.Settings != null)
            {
                SurveyVote.Delete(surveyId, info.QuestionId);
                SurveyVote.Add(surveyId, info.QuestionId, info.Settings.Count);
            }
            return dal.Update(surveyId, SerializeFieldList(fieldList));
        }

        public static bool Update(int surveyId, IList<SurveyFieldInfo> infoList)
        {
            return dal.Update(surveyId, SerializeFieldList(infoList));
        }
    }
}


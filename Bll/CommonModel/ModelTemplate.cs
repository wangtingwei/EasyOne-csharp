namespace EasyOne.CommonModel
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.CommonModel;
    using EasyOne.Model.CommonModel;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class ModelTemplate
    {
        private static readonly IModelTemplate dal = DataAccess.CreateCotentModelTemplate();
        private static Serialize<FieldInfo> ser = new Serialize<FieldInfo>();

        private ModelTemplate()
        {
        }

        public static DataActionState Add(ModelTemplatesInfo modelTemplatesInfo)
        {
            DataActionState unknown = DataActionState.Unknown;
            if (dal.Add(modelTemplatesInfo))
            {
                unknown = DataActionState.Successed;
            }
            return unknown;
        }

        public static bool Delete(string id)
        {
            return (DataValidator.IsValidId(id) && dal.Delete(id));
        }

        public static bool ExportData(string templateId, string importPath, bool chkFormatConn)
        {
            return (DataValidator.IsValidId(templateId) && dal.ExportData(templateId, importPath.Replace("'", ""), chkFormatConn));
        }

        public static int GetCountNumber(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetCountNumber();
        }

        public static int GetCountNumber(int startRowIndexId, int maxNumberRows, ModelType type)
        {
            return dal.GetCountNumber();
        }

        public static string GetField(int id)
        {
            return dal.GetField(id);
        }

        public static IList<FieldInfo> GetFieldListByTemplateId(int templateId)
        {
            string field = GetField(templateId);
            return ser.DeserializeFieldList(field);
        }

        public static IList<ModelTemplatesInfo> GetImportList(string importPath)
        {
            return dal.GetImportList(importPath.Replace("'", ""));
        }

        public static IList<ModelTemplatesInfo> GetImportList(string importPath, ModelType type)
        {
            return dal.GetImportList(importPath.Replace("'", ""), type);
        }

        public static ModelTemplatesInfo GetInfoById(int id)
        {
            return dal.GetInfoById(id);
        }

        public static IList<ModelTemplatesInfo> GetModelTemplateInfoList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetModelTemplateInfoList(startRowIndexId, maxNumberRows);
        }

        public static IList<ModelTemplatesInfo> GetModelTemplateInfoList(int startRowIndexId, int maxNumberRows, ModelType type)
        {
            return dal.GetModelTemplateInfoList(startRowIndexId, maxNumberRows, type);
        }

        public static bool ImportData(string templateId, string importPath)
        {
            return (DataValidator.IsValidId(templateId) && dal.ImportData(templateId, importPath.Replace("'", "")));
        }

        public static DataActionState Update(ModelTemplatesInfo modelTemplatesInfo)
        {
            DataActionState unknown = DataActionState.Unknown;
            if (dal.Update(modelTemplatesInfo))
            {
                unknown = DataActionState.Successed;
            }
            return unknown;
        }

        public static bool UpdateField(int id, string fieldList)
        {
            return dal.UpdateField(id, fieldList);
        }
    }
}


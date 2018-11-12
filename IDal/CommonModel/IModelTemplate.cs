namespace EasyOne.IDal.CommonModel
{
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using System;
    using System.Collections.Generic;

    public interface IModelTemplate
    {
        bool Add(ModelTemplatesInfo templateInfo);
        bool Delete(string id);
        bool ExportData(string templateId, string importPath, bool chkFormatConn);
        int GetCountNumber();
        string GetField(int id);
        IList<ModelTemplatesInfo> GetImportList(string importPath);
        IList<ModelTemplatesInfo> GetImportList(string importPath, ModelType type);
        ModelTemplatesInfo GetInfoById(int id);
        IList<ModelTemplatesInfo> GetModelTemplateInfoList(int startRowIndexId, int maxNumberRows);
        IList<ModelTemplatesInfo> GetModelTemplateInfoList(int startRowIndexId, int maxNumberRows, ModelType type);
        bool ImportData(string templateId, string importPath);
        bool Update(ModelTemplatesInfo modelTemplatesInfo);
        bool UpdateField(int id, string fieldList);
    }
}


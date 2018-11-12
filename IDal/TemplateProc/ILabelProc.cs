namespace EasyOne.IDal.TemplateProc
{
    using EasyOne.Model.TemplateProc;
    using System;

    public interface ILabelProc
    {
        LabelInfo GetOdbcQuery(LabelInfo labelInfo);
        LabelInfo GetOleQuery(LabelInfo labelInfo);
        LabelInfo GetOracleQuery(LabelInfo labelInfo);
        LabelInfo GetOutSqlQuery(LabelInfo labelInfo);
        int GetSqlCommand(LabelInfo labelInfo);
        LabelInfo GetSqlQuery(LabelInfo labelInfo);
        int GetStoreCommand(LabelInfo labelInfo);
        LabelInfo GetStoreQuery(LabelInfo labelInfo);
        LabelInfo GetXmlQuery(LabelInfo labelInfo);
    }
}


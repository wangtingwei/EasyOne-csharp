namespace EasyOne.IDal.TemplateProc
{
    using EasyOne.Enumerations;
    using System;
    using System.Data;
    using System.Xml;

    public interface ILabelManage
    {
        string GetMainDBQuery(string sqlstr, XmlNodeList attrib, bool dbtype);
        string GetOdbcDBQuery(string dbconn, string sqlstr);
        string GetOleDBQuery(string dbconn, string sqlstr);
        string GetOracleDBQuery(string dbconn, string sqlstr);
        string GetOutSqlDBQuery(string dbconn, string sqlstr);
        DataTable GetSchemaDataBase(string dbconn, DataSourceType dataSourceType);
        DataTable GetSchemaTable(string tableName, string dbconn, DataSourceType dataSourceType);
        bool TestOdbc(string dbconn);
        bool TestOle(string dbconn);
        bool TestOracle(string dbconn);
        bool TestOutSql(string dbconn);
    }
}


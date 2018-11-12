namespace EasyOne.SqlServerDal.TemplateProc
{
    using EasyOne.Enumerations;
    using EasyOne.IDal.TemplateProc;
    using EasyOne.SqlServerDal;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Odbc;
    using System.Data.OleDb;
    using System.Data.OracleClient;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;
    using System.Xml;

    public sealed class LabelManage : ILabelManage
    {
        public string GetMainDBQuery(string sqlstr, XmlNodeList attrib, bool dbtype)
        {
            string str = null;
            Parameters cmdParams = new Parameters();
            try
            {
                if (dbtype)
                {
                    foreach (XmlNode node in attrib)
                    {
                        string[] strArray = node.SelectSingleNode("default").InnerText.Split(new string[] { "|||" }, StringSplitOptions.None);
                        if (strArray.Length > 1)
                        {
                            cmdParams.AddInParameter("@" + node.SelectSingleNode("name").InnerText, DbType.String, strArray[0]);
                        }
                        else
                        {
                            cmdParams.AddInParameter("@" + node.SelectSingleNode("name").InnerText, DbType.String, node.SelectSingleNode("default").InnerText);
                        }
                    }
                    using (DataSet set = DBHelper.ExecuteDataSetProc(sqlstr, cmdParams))
                    {
                        if (set == null)
                        {
                            return null;
                        }
                        return set.GetXml();
                    }
                }
                using (DataSet set2 = DBHelper.ExecuteDataSetSql(sqlstr, cmdParams))
                {
                    if (set2 == null)
                    {
                        return null;
                    }
                    return set2.GetXml();
                }
            }
            catch (SqlException)
            {
                str = "queryerr";
            }
            return str;
        }

        public string GetOdbcDBQuery(string dbconn, string sqlstr)
        {
            string xml = string.Empty;
            try
            {
                OdbcConnection selectConnection = new OdbcConnection(dbconn);
                selectConnection.Open();
                if (string.Compare(selectConnection.State.ToString(), "Open", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    OdbcDataAdapter adapter = new OdbcDataAdapter(sqlstr, selectConnection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    if (dataSet != null)
                    {
                        xml = dataSet.GetXml();
                    }
                    adapter.Dispose();
                }
                selectConnection.Dispose();
            }
            catch (OdbcException)
            {
                xml = "queryerr";
            }
            catch (SystemException)
            {
                xml = "queryerr";
            }
            return xml;
        }

        private static DataTable GetOdbcSchema(string strConn)
        {
            OdbcConnection connection = new OdbcConnection(strConn);
            DataTable schema = new DataTable();
            try
            {
                connection.Open();
                schema = connection.GetSchema("TABLES");
            }
            catch
            {
                schema = null;
            }
            finally
            {
                connection.Close();
            }
            return schema;
        }

        private static DataTable GetOdbcTableField(string myConn, string strSql)
        {
            OdbcConnection connection = new OdbcConnection(myConn);
            if (string.IsNullOrEmpty(strSql))
            {
                return null;
            }
            OdbcCommand command = new OdbcCommand(strSql, connection);
            DataTable schemaTable = new DataTable();
            try
            {
                connection.Open();
                schemaTable = command.ExecuteReader(CommandBehavior.SchemaOnly).GetSchemaTable();
            }
            catch (OdbcException)
            {
                schemaTable = null;
            }
            finally
            {
                connection.Close();
            }
            return schemaTable;
        }

        public string GetOleDBQuery(string dbconn, string sqlstr)
        {
            string xml = string.Empty;
            try
            {
                OleDbConnection selectConnection = new OleDbConnection(dbconn);
                selectConnection.Open();
                if (string.Compare(selectConnection.State.ToString(), "Open", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter(sqlstr, selectConnection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    if (dataSet != null)
                    {
                        xml = dataSet.GetXml();
                    }
                    adapter.Dispose();
                }
                selectConnection.Dispose();
            }
            catch (OleDbException)
            {
                xml = "queryerr";
            }
            catch (SystemException)
            {
                xml = "queryerr";
            }
            return xml;
        }

        private static DataTable GetOleSchema(string strConn)
        {
            OleDbConnection connection = new OleDbConnection(strConn);
            DataTable schema = new DataTable();
            try
            {
                connection.Open();
                schema = connection.GetSchema("TABLES");
            }
            catch
            {
                schema = null;
            }
            finally
            {
                connection.Close();
            }
            return schema;
        }

        private static DataTable GetOleTableField(string myConn, string strSql)
        {
            OleDbConnection connection = new OleDbConnection(myConn);
            if (string.IsNullOrEmpty(strSql))
            {
                return null;
            }
            OleDbCommand command = new OleDbCommand(strSql, connection);
            DataTable schemaTable = new DataTable();
            try
            {
                connection.Open();
                schemaTable = command.ExecuteReader(CommandBehavior.SchemaOnly).GetSchemaTable();
            }
            catch (OleDbException)
            {
                schemaTable = null;
            }
            finally
            {
                connection.Close();
            }
            return schemaTable;
        }

        public string GetOracleDBQuery(string dbconn, string sqlstr)
        {
            string xml = string.Empty;
            try
            {
                OracleConnection selectConnection = new OracleConnection(dbconn);
                selectConnection.Open();
                if (string.Compare(selectConnection.State.ToString(), "Open", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    OracleDataAdapter adapter = new OracleDataAdapter(sqlstr, selectConnection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    if (dataSet != null)
                    {
                        xml = dataSet.GetXml();
                    }
                    adapter.Dispose();
                }
                selectConnection.Dispose();
            }
            catch (OracleException)
            {
                xml = "queryerr";
            }
            catch (SystemException)
            {
                xml = "queryerr";
            }
            return xml;
        }

        private static DataTable GetOracleSchema(string strConn)
        {
            OracleConnection connection = new OracleConnection(strConn);
            DataTable schema = new DataTable();
            try
            {
                connection.Open();
                schema = connection.GetSchema("TABLES");
            }
            catch
            {
                schema = null;
            }
            finally
            {
                connection.Close();
            }
            return schema;
        }

        private static DataTable GetOracleTableField(string myConn, string strSql)
        {
            OracleConnection connection = new OracleConnection(myConn);
            if (string.IsNullOrEmpty(strSql))
            {
                return null;
            }
            OracleCommand command = new OracleCommand(strSql, connection);
            DataTable schemaTable = new DataTable();
            try
            {
                connection.Open();
                schemaTable = command.ExecuteReader(CommandBehavior.SchemaOnly).GetSchemaTable();
            }
            catch (OracleException)
            {
                schemaTable = null;
            }
            finally
            {
                connection.Close();
            }
            return schemaTable;
        }

        public string GetOutSqlDBQuery(string dbconn, string sqlstr)
        {
            string xml = string.Empty;
            try
            {
                SqlConnection selectConnection = new SqlConnection(dbconn);
                selectConnection.Open();
                if (string.Compare(selectConnection.State.ToString(), "Open", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlstr, selectConnection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    if (dataSet != null)
                    {
                        xml = dataSet.GetXml();
                    }
                    adapter.Dispose();
                }
                selectConnection.Dispose();
            }
            catch (SqlException)
            {
                xml = "queryerr";
            }
            catch (SystemException)
            {
                xml = "queryerr";
            }
            return xml;
        }

        private static DataTable GetOutSqlTableField(string myConn, string strSql)
        {
            SqlConnection connection = new SqlConnection(myConn);
            if (string.IsNullOrEmpty(strSql))
            {
                return null;
            }
            SqlCommand command = new SqlCommand(strSql, connection);
            DataTable schemaTable = new DataTable();
            try
            {
                connection.Open();
                schemaTable = command.ExecuteReader(CommandBehavior.SchemaOnly).GetSchemaTable();
            }
            catch (SqlException)
            {
                schemaTable = null;
            }
            finally
            {
                connection.Close();
            }
            return schemaTable;
        }

        public DataTable GetSchemaDataBase(string dbconn, DataSourceType dataSourceType)
        {
            DataTable table = new DataTable();
            switch (dataSourceType)
            {
                case DataSourceType.Ole:
                    return GetOleSchema(dbconn);

                case DataSourceType.Sql:
                    return GetSqlDatabaseSchema(dbconn);

                case DataSourceType.Odbc:
                    return GetOdbcSchema(dbconn);

                case DataSourceType.Oracle:
                    return GetOracleSchema(dbconn);
            }
            return GetSqlDatabaseSchema(ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString);
        }

        public DataTable GetSchemaTable(string tableName, string dbconn, DataSourceType dataSourceType)
        {
            string str;
            if (!string.IsNullOrEmpty(tableName))
            {
                if (Regex.IsMatch(dbconn, "excel", RegexOptions.IgnoreCase))
                {
                    str = "SELECT TOP 1 * FROM [" + tableName + "]";
                }
                else
                {
                    str = "SELECT * FROM " + tableName + " WHERE 1 = 0";
                }
            }
            else
            {
                str = null;
            }
            DataTable table = new DataTable();
            switch (dataSourceType)
            {
                case DataSourceType.Ole:
                    return GetOleTableField(dbconn, str);

                case DataSourceType.Sql:
                    return GetOutSqlTableField(dbconn, str);

                case DataSourceType.Odbc:
                    return GetOdbcTableField(dbconn, str);

                case DataSourceType.Oracle:
                    return GetOracleTableField(dbconn, str);
            }
            return DBHelper.ExecuteReaderSql(str).GetSchemaTable();
        }

        private static DataTable GetSqlDatabaseSchema(string strConn)
        {
            SqlConnection connection = new SqlConnection(strConn);
            DataTable schema = new DataTable();
            try
            {
                connection.Open();
                schema = connection.GetSchema("TABLES");
            }
            catch
            {
                schema = null;
            }
            finally
            {
                connection.Close();
            }
            return schema;
        }

        public bool TestOdbc(string dbconn)
        {
            OdbcConnection connection;
            try
            {
                connection = new OdbcConnection(dbconn);
            }
            catch (OdbcException)
            {
                return false;
            }
            connection.Open();
            if (string.Compare(connection.State.ToString(), "Open", StringComparison.OrdinalIgnoreCase) == 0)
            {
                connection.Close();
                connection.Dispose();
                return true;
            }
            connection.Close();
            connection.Dispose();
            return false;
        }

        public bool TestOle(string dbconn)
        {
            OleDbConnection connection;
            try
            {
                connection = new OleDbConnection(dbconn);
            }
            catch (OleDbException)
            {
                return false;
            }
            connection.Open();
            if (string.Compare(connection.State.ToString(), "Open", StringComparison.OrdinalIgnoreCase) == 0)
            {
                connection.Close();
                connection.Dispose();
                return true;
            }
            connection.Close();
            connection.Dispose();
            return false;
        }

        public bool TestOracle(string dbconn)
        {
            OracleConnection connection;
            try
            {
                connection = new OracleConnection(dbconn);
            }
            catch (OracleException)
            {
                return false;
            }
            connection.Open();
            if (string.Compare(connection.State.ToString(), "Open", StringComparison.OrdinalIgnoreCase) == 0)
            {
                connection.Close();
                connection.Dispose();
                return true;
            }
            connection.Close();
            connection.Dispose();
            return false;
        }

        public bool TestOutSql(string dbconn)
        {
            SqlConnection connection;
            try
            {
                connection = new SqlConnection(dbconn);
            }
            catch (SqlException)
            {
                return false;
            }
            connection.Open();
            if (string.Compare(connection.State.ToString(), "Open", StringComparison.OrdinalIgnoreCase) == 0)
            {
                connection.Close();
                connection.Dispose();
                return true;
            }
            connection.Close();
            connection.Dispose();
            return false;
        }
    }
}


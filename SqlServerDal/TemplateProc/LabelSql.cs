namespace EasyOne.SqlServerDal.TemplateProc
{
    using EasyOne.IDal.TemplateProc;
    using EasyOne.Model.TemplateProc;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;
    using System.Data.Odbc;
    using System.Data.OleDb;
    using System.Data.OracleClient;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using System.Xml;

    public sealed class LabelSql : ILabelProc
    {
        public LabelInfo GetOdbcQuery(LabelInfo labelInfo)
        {
            string str = labelInfo.LabelDefineData["LabelDataSource"];
            string str2 = labelInfo.LabelDefineData["LabelSqlString"];
            if (string.IsNullOrEmpty(str))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"数据源连接为空！]");
                return labelInfo;
            }
            if (string.IsNullOrEmpty(str2))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"查询语句为空！]");
                return labelInfo;
            }
            str = PareProc(str, labelInfo);
            str2 = PareProc(str2, labelInfo);
            if (labelInfo.PageSize > 0)
            {
                OdbcDataAdapter adapter;
                string istr = labelInfo.LabelDefineData["LabelSqlPage"];
                string str4 = labelInfo.LabelDefineData["LabelSqlCount"];
                istr = PareProc(istr, labelInfo);
                str4 = PareProc(str4, labelInfo);
                if (string.IsNullOrEmpty(istr))
                {
                    labelInfo.Error = 1;
                    labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"分页查询语句为空！]");
                    return labelInfo;
                }
                if (string.IsNullOrEmpty(str4))
                {
                    labelInfo.Error = 1;
                    labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"统计查询语句为空！]");
                    return labelInfo;
                }
                str = str.Replace("@page", labelInfo.Page.ToString());
                str2 = str2.Replace("@pagesize", labelInfo.PageSize.ToString());
                istr = istr.Replace("@pagesize", labelInfo.PageSize.ToString());
                int num = (labelInfo.Page - 1) * labelInfo.PageSize;
                if (num < 0)
                {
                    num = 0;
                }
                istr = istr.Replace("@startrow", Convert.ToString(num));
                OdbcConnection connection = new OdbcConnection(str);
                connection.Open();
                if (!string.IsNullOrEmpty(str4))
                {
                    try
                    {
                        OdbcCommand command = new OdbcCommand(str4, connection);
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader[0] != DBNull.Value)
                                {
                                    labelInfo.TotalPub = reader.GetInt32(0);
                                }
                            }
                            else
                            {
                                labelInfo.TotalPub = 0;
                            }
                        }
                    }
                    catch
                    {
                        labelInfo.TotalPub = 0;
                    }
                }
                else
                {
                    labelInfo.TotalPub = 0;
                }
                if ((labelInfo.Page > 1) && !string.IsNullOrEmpty(istr))
                {
                    adapter = new OdbcDataAdapter(istr, connection);
                }
                else
                {
                    adapter = new OdbcDataAdapter(str2, connection);
                }
                DataSet dataSet = new DataSet();
                try
                {
                    adapter.Fill(dataSet);
                    if (labelInfo.TotalPub == 0)
                    {
                        DataTable table = dataSet.Tables[0];
                        labelInfo.TotalPub = table.Rows.Count;
                    }
                    labelInfo.LabelContent = new StringBuilder(dataSet.GetXml());
                }
                catch (OdbcException exception)
                {
                    labelInfo.Error = 2;
                    labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"ODBC数据源错误，原因：" + exception.Message + "]");
                }
                finally
                {
                    adapter.Dispose();
                }
                connection.Dispose();
                return labelInfo;
            }
            str2 = str2.Replace("@pagesize", "500");
            OdbcConnection selectConnection = new OdbcConnection(str);
            selectConnection.Open();
            OdbcDataAdapter adapter2 = new OdbcDataAdapter(str2, selectConnection);
            try
            {
                DataSet set2 = new DataSet();
                adapter2.Fill(set2);
                DataTable table2 = set2.Tables[0];
                labelInfo.TotalPub = table2.Rows.Count;
                labelInfo.LabelContent = new StringBuilder(set2.GetXml());
            }
            catch (OdbcException exception2)
            {
                labelInfo.Error = 2;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"ODBC数据源读取错误，原因：" + exception2.Message + "]");
            }
            finally
            {
                adapter2.Dispose();
                selectConnection.Dispose();
            }
            return labelInfo;
        }

        public LabelInfo GetOleQuery(LabelInfo labelInfo)
        {
            string istr = labelInfo.LabelDefineData["LabelDataSource"];
            string str2 = labelInfo.LabelDefineData["LabelSqlString"];
            istr = PareProc(istr, labelInfo);
            str2 = PareProc(str2, labelInfo);
            if (string.IsNullOrEmpty(istr))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"数据源连接为空！]");
                return labelInfo;
            }
            if (string.IsNullOrEmpty(str2))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"查询语句为空！]");
                return labelInfo;
            }
            if (labelInfo.PageSize > 0)
            {
                OleDbDataAdapter adapter;
                string str3 = labelInfo.LabelDefineData["LabelSqlPage"];
                string str4 = labelInfo.LabelDefineData["LabelSqlCount"];
                str3 = PareProc(str3, labelInfo);
                str4 = PareProc(str4, labelInfo);
                if (string.IsNullOrEmpty(str3))
                {
                    labelInfo.Error = 1;
                    labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"分页查询语句为空！]");
                    return labelInfo;
                }
                if (string.IsNullOrEmpty(str4))
                {
                    labelInfo.Error = 1;
                    labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"统计查询语句为空！]");
                    return labelInfo;
                }
                istr = istr.Replace("@page", labelInfo.Page.ToString());
                str2 = str2.Replace("@pagesize", labelInfo.PageSize.ToString());
                str3 = str3.Replace("@pagesize", labelInfo.PageSize.ToString());
                int num = (labelInfo.Page - 1) * labelInfo.PageSize;
                if (num < 0)
                {
                    num = 0;
                }
                str3 = str3.Replace("@startrow", Convert.ToString(num));
                OleDbConnection connection = new OleDbConnection(istr);
                connection.Open();
                if (!string.IsNullOrEmpty(str4))
                {
                    try
                    {
                        OleDbCommand command = new OleDbCommand(str4, connection);
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader[0] != DBNull.Value)
                                {
                                    labelInfo.TotalPub = reader.GetInt32(0);
                                }
                            }
                            else
                            {
                                labelInfo.TotalPub = 0;
                            }
                        }
                    }
                    catch
                    {
                        labelInfo.TotalPub = 0;
                    }
                }
                else
                {
                    labelInfo.TotalPub = 0;
                }
                if (labelInfo.Page > 1)
                {
                    adapter = new OleDbDataAdapter(str3, connection);
                }
                else
                {
                    adapter = new OleDbDataAdapter(str2, connection);
                }
                DataSet dataSet = new DataSet();
                try
                {
                    adapter.Fill(dataSet);
                    if (labelInfo.TotalPub == 0)
                    {
                        DataTable table = dataSet.Tables[0];
                        labelInfo.TotalPub = table.Rows.Count;
                    }
                    labelInfo.LabelContent = new StringBuilder(dataSet.GetXml());
                }
                catch (OleDbException exception)
                {
                    labelInfo.Error = 2;
                    labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"错误，原因：" + exception.Message + "]");
                }
                finally
                {
                    adapter.Dispose();
                }
                connection.Dispose();
                return labelInfo;
            }
            str2 = str2.Replace("@pagesize", "500");
            OleDbConnection selectConnection = new OleDbConnection(istr);
            selectConnection.Open();
            OleDbDataAdapter adapter2 = new OleDbDataAdapter(str2, selectConnection);
            try
            {
                DataSet set2 = new DataSet();
                adapter2.Fill(set2);
                DataTable table2 = set2.Tables[0];
                labelInfo.TotalPub = table2.Rows.Count;
                labelInfo.LabelContent = new StringBuilder(set2.GetXml());
            }
            catch (OleDbException exception2)
            {
                labelInfo.Error = 2;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"数据源[" + istr + "]错误，原因：" + exception2.Message + "]");
            }
            finally
            {
                adapter2.Dispose();
                selectConnection.Dispose();
            }
            return labelInfo;
        }

        public LabelInfo GetOracleQuery(LabelInfo labelInfo)
        {
            string istr = labelInfo.LabelDefineData["LabelDataSource"];
            string str2 = labelInfo.LabelDefineData["LabelSqlString"];
            istr = PareProc(istr, labelInfo);
            str2 = PareProc(str2, labelInfo);
            if (string.IsNullOrEmpty(istr))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"数据源连接为空！]");
                return labelInfo;
            }
            if (string.IsNullOrEmpty(str2))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"查询语句为空！]");
                return labelInfo;
            }
            OracleConnection connection = new OracleConnection(istr);
            connection.Open();
            if (labelInfo.PageSize > 0)
            {
                string str3 = labelInfo.LabelDefineData["LabelSqlCount"];
                str3 = PareProc(str3, labelInfo);
                if (string.IsNullOrEmpty(str3))
                {
                    labelInfo.Error = 1;
                    labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"统计查询语句为空！]");
                    return labelInfo;
                }
                str2 = str2.Replace("@pagesize", labelInfo.PageSize.ToString());
                str3 = str3.Replace("@pagesize", labelInfo.PageSize.ToString());
                int num = (labelInfo.Page - 1) * labelInfo.PageSize;
                if (num < 0)
                {
                    num = 0;
                }
                str2 = str2.Replace("@startrow", num.ToString());
                OracleCommand command = new OracleCommand(str3.Replace("@startrow", num.ToString()), connection);
                if (labelInfo.PageSize > 0)
                {
                    try
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader[0] != DBNull.Value)
                                {
                                    labelInfo.TotalPub = reader.GetInt32(0);
                                }
                            }
                            else
                            {
                                labelInfo.TotalPub = 0;
                            }
                        }
                    }
                    catch
                    {
                        labelInfo.TotalPub = 0;
                    }
                }
            }
            else
            {
                str2 = str2.Replace("@pagesize", "1000").Replace("@startrow", "0");
            }
            OracleDataAdapter adapter = new OracleDataAdapter(str2, connection);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                if (labelInfo.TotalPub == 0)
                {
                    DataTable table = dataSet.Tables[0];
                    labelInfo.TotalPub = table.Rows.Count;
                }
                labelInfo.LabelContent = new StringBuilder(dataSet.GetXml());
            }
            catch (OracleException exception)
            {
                labelInfo.Error = 2;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"Oracle数据源读取错，原因：" + exception.Message + "]");
            }
            finally
            {
                adapter.Dispose();
            }
            connection.Dispose();
            return labelInfo;
        }

        public LabelInfo GetOutSqlQuery(LabelInfo labelInfo)
        {
            string istr = labelInfo.LabelDefineData["LabelDataSource"];
            string str2 = labelInfo.LabelDefineData["LabelSqlString"];
            istr = PareProc(istr, labelInfo);
            str2 = PareProc(str2, labelInfo);
            if (string.IsNullOrEmpty(istr))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"数据源连接为空！]");
                return labelInfo;
            }
            if (string.IsNullOrEmpty(str2))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"查询语句为空！]");
                return labelInfo;
            }
            SqlConnection connection = new SqlConnection(istr);
            connection.Open();
            if (labelInfo.PageSize > 0)
            {
                string str3 = labelInfo.LabelDefineData["LabelSqlCount"];
                str3 = PareProc(str3, labelInfo);
                if (string.IsNullOrEmpty(str3))
                {
                    labelInfo.Error = 1;
                    labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"统计查询语句为空！]");
                    return labelInfo;
                }
                str2 = str2.Replace("@pagesize", labelInfo.PageSize.ToString());
                str3 = str3.Replace("@pagesize", labelInfo.PageSize.ToString());
                int num = (labelInfo.Page - 1) * labelInfo.PageSize;
                if (num < 0)
                {
                    num = 0;
                }
                str2 = str2.Replace("@startrow", num.ToString());
                SqlCommand command = new SqlCommand(str3.Replace("@startrow", num.ToString()), connection);
                if (labelInfo.PageSize > 0)
                {
                    try
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader[0] != DBNull.Value)
                                {
                                    labelInfo.TotalPub = reader.GetInt32(0);
                                }
                            }
                            else
                            {
                                labelInfo.TotalPub = 0;
                            }
                        }
                    }
                    catch
                    {
                        labelInfo.TotalPub = 0;
                    }
                }
            }
            else
            {
                str2 = str2.Replace("@pagesize", "1000").Replace("@startrow", "0");
            }
            SqlDataAdapter adapter = new SqlDataAdapter(str2, connection);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                if (labelInfo.TotalPub == 0)
                {
                    DataTable table = dataSet.Tables[0];
                    labelInfo.TotalPub = table.Rows.Count;
                }
                labelInfo.LabelContent = new StringBuilder(dataSet.GetXml());
            }
            catch (SqlException exception)
            {
                labelInfo.Error = 2;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"SQL数据源读取错，原因：" + exception.Message + "]");
            }
            finally
            {
                adapter.Dispose();
            }
            connection.Dispose();
            return labelInfo;
        }

        public int GetSqlCommand(LabelInfo labelInfo)
        {
            string str = labelInfo.LabelDefineData["LabelSqlString"];
            if (string.IsNullOrEmpty(str))
            {
                return -999;
            }
            str = PareProc(str, labelInfo);
            try
            {
                return DBHelper.ExecuteNonQuerySql(str);
            }
            catch (SqlException)
            {
                return -999;
            }
        }

        public LabelInfo GetSqlQuery(LabelInfo labelInfo)
        {
            string istr = labelInfo.LabelDefineData["LabelSqlString"];
            istr = PareProc(istr, labelInfo);
            if (string.IsNullOrEmpty(istr))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"查询命令为空！]");
            }
            if (labelInfo.PageSize > 0)
            {
                string str2 = labelInfo.LabelDefineData["LabelSqlCount"];
                str2 = PareProc(str2, labelInfo);
                istr = istr.Replace("@pagesize", labelInfo.PageSize.ToString());
                str2 = str2.Replace("@pagesize", labelInfo.PageSize.ToString());
                int num = (labelInfo.Page - 1) * labelInfo.PageSize;
                if (num < 0)
                {
                    num = 0;
                }
                istr = istr.Replace("@startrow", num.ToString());
                str2 = str2.Replace("@startrow", num.ToString());
                if (labelInfo.PageSize <= 0)
                {
                    goto Label_0153;
                }
                using (IDataReader reader = DBHelper.ExecuteReaderSql(str2))
                {
                    if (reader.Read())
                    {
                        if (reader[0] != DBNull.Value)
                        {
                            labelInfo.TotalPub = reader.GetInt32(0);
                        }
                    }
                    else
                    {
                        labelInfo.TotalPub = 0;
                    }
                    goto Label_0153;
                }
            }
            istr = istr.Replace("@pagesize", "1000").Replace("@startrow", "0");
        Label_0153:
            try
            {
                using (DataSet set = DBHelper.ExecuteDataSetSql(istr))
                {
                    if (labelInfo.TotalPub == 0)
                    {
                        DataTable table = set.Tables[0];
                        labelInfo.TotalPub = table.Rows.Count;
                    }
                    labelInfo.LabelContent = new StringBuilder(set.GetXml());
                    return labelInfo;
                }
            }
            catch (SqlException exception)
            {
                labelInfo.Error = 2;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"数据源读取错，原因：" + exception.Message + "]");
            }
            return labelInfo;
        }

        public int GetStoreCommand(LabelInfo labelInfo)
        {
            string str = labelInfo.LabelDefineData["LabelSqlString"];
            if (string.IsNullOrEmpty(str))
            {
                labelInfo.Error = 1;
                return -999;
            }
            Parameters cmdParams = new Parameters();
            foreach (string str2 in labelInfo.AttributesData.AllKeys)
            {
                cmdParams.AddInParameter("@" + str2, DbType.String, labelInfo.AttributesData[str2]);
            }
            try
            {
                return DBHelper.ExecuteNonQueryProc(str, cmdParams);
            }
            catch (SqlException)
            {
                labelInfo.Error = 2;
                return -999;
            }
        }

        public LabelInfo GetStoreQuery(LabelInfo labelInfo)
        {
            string str = labelInfo.LabelDefineData["LabelSqlString"];
            if (string.IsNullOrEmpty(str))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"请指定存储过程]");
            }
            Parameters cmdParams = new Parameters();
            foreach (string str2 in labelInfo.AttributesData.AllKeys)
            {
                cmdParams.AddInParameter("@" + str2, DbType.String, labelInfo.AttributesData[str2]);
            }
            try
            {
                using (DataSet set = DBHelper.ExecuteDataSetProc(str, cmdParams))
                {
                    labelInfo.LabelContent = new StringBuilder();
                    foreach (DataTable table in set.Tables)
                    {
                        labelInfo.TotalPub = table.Rows.Count;
                        labelInfo.LabelContent.Append(set.GetXml());
                    }
                    return labelInfo;
                }
            }
            catch (SqlException exception)
            {
                labelInfo.Error = 2;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"存储过程没有取得正确的数据，原因：" + exception.Message + "]");
            }
            return labelInfo;
        }

        public LabelInfo GetXmlQuery(LabelInfo labelInfo)
        {
            string path = labelInfo.LabelDefineData["LabelDataSource"];
            if (string.IsNullOrEmpty(path.ToString()))
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"XML地址为空！]");
                return labelInfo;
            }
            if ((path.IndexOf("http://", 0, 7, StringComparison.OrdinalIgnoreCase) < 0) && !Path.IsPathRooted(path))
            {
                path = Path.Combine(labelInfo.RootPath, path);
            }
            foreach (string str2 in labelInfo.AttributesData.AllKeys)
            {
                path = path.Replace("@" + str2, labelInfo.AttributesData[str2]);
            }
            path = path.Replace("@page", labelInfo.Page.ToString());
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path.ToString());
                labelInfo.LabelContent = new StringBuilder(document.OuterXml);
            }
            catch (XmlException exception)
            {
                labelInfo.Error = 1;
                labelInfo.LabelContent = new StringBuilder("[err:标签\"" + labelInfo.OriginalData["id"] + "\"XML源\"" + path + "\"读取错误，原因：" + exception.Message);
            }
            return labelInfo;
        }

        private static string PareProc(string istr, LabelInfo labelInfo)
        {
            foreach (string str in labelInfo.AttributesData.AllKeys)
            {
                istr = istr.Replace("@" + str, labelInfo.AttributesData[str]);
            }
            return istr;
        }
    }
}


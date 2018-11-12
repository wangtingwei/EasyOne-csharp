namespace EasyOne.SqlServerDal.CommonModel
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Enumerations;
    using EasyOne.IDal.CommonModel;
    using EasyOne.Model.CommonModel;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.OleDb;
    using System.Text;

    public class ModelTemplate : IModelTemplate
    {
        private int m_CountNumber;

        public bool Add(ModelTemplatesInfo templateInfo)
        {
            Parameters parms = new Parameters();
            templateInfo.TemplateId = GetMaxId() + 1;
            GetParameters(templateInfo, parms);
            return DBHelper.ExecuteProc("PR_Contents_ModelTemplates_Add", parms);
        }

        public bool Delete(string id)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_ModelTemplates WHERE TemplateId IN (" + DBHelper.ToValidId(id) + ")");
        }

        public bool ExportData(string templateId, string importPath, bool chkFormatConn)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Contents_ModelTemplates_GetExportList");
            OleDbConnection connection = new OleDbConnection("Provider = Microsoft.Jet.OleDb.4.0;Data Source = " + importPath);
            if (chkFormatConn)
            {
                OleDbCommand command2 = new OleDbCommand("DELETE FROM [PE_ModelTemplates]", connection);
                command2.CommandType = CommandType.Text;
                connection.Open();
                try
                {
                    command2.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command2.Cancel();
                    connection.Close();
                }
            }
            database.AddInParameter(storedProcCommand, "@TemplateID", DbType.String, templateId);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                connection.Open();
                object obj2 = new OleDbCommand("SELECT MAX(templateId) FROM PE_ModelTemplates", connection).ExecuteScalar();
                int num = Convert.IsDBNull(obj2) ? 1 : (Convert.ToInt32(obj2) + 1);
                bool flag = false;
                OleDbCommand command4 = new OleDbCommand("INSERT INTO PE_ModelTemplates (templateId, TemplateName, TemplateDescription, Field, IsEshop) VALUES (@TemplateID, @TemplateName, @TemplateDescription, @Field, @IsEshop)", connection);
                OleDbParameter parameter = new OleDbParameter();
                OleDbParameter parameter2 = new OleDbParameter();
                OleDbParameter parameter3 = new OleDbParameter();
                OleDbParameter parameter4 = new OleDbParameter();
                OleDbParameter parameter5 = new OleDbParameter();
                parameter.ParameterName = "@TemplateID";
                parameter2.ParameterName = "@TemplateName";
                parameter3.ParameterName = "@TemplateDescription";
                parameter4.ParameterName = "@Field";
                parameter5.ParameterName = "@IsEshop";
                command4.Parameters.Add(parameter);
                command4.Parameters.Add(parameter2);
                command4.Parameters.Add(parameter3);
                command4.Parameters.Add(parameter4);
                command4.Parameters.Add(parameter5);
                while (reader.Read())
                {
                    if (!flag)
                    {
                        flag = true;
                    }
                    else
                    {
                        num++;
                    }
                    try
                    {
                        parameter.Value = num;
                        parameter2.Value = reader.GetString("TemplateName");
                        parameter3.Value = reader.GetString("TemplateDescription");
                        parameter4.Value = reader.GetString("Field");
                        parameter5.Value = reader.GetBoolean("IsEshop");
                        command4.ExecuteNonQuery();
                        continue;
                    }
                    catch
                    {
                        connection.Close();
                        return false;
                    }
                }
                connection.Close();
                return true;
            }
        }

        public int GetCountNumber()
        {
            return this.m_CountNumber;
        }

        public string GetField(int id)
        {
            string str = "";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TemplateId", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Contents_ModelTemplates_GetField", cmdParams))
            {
                if (reader.Read())
                {
                    str = reader.GetString("Field");
                }
            }
            return str;
        }

        public IList<ModelTemplatesInfo> GetImportList(string importPath)
        {
            OleDbConnection connection = new OleDbConnection("Provider = Microsoft.Jet.OleDb.4.0;Data Source = " + importPath);
            IList<ModelTemplatesInfo> list = new List<ModelTemplatesInfo>();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(" SELECT * FROM PE_ModelTemplates ORDER BY TemplateID ASC", connection);
                NullableDataReader rdr = new NullableDataReader(command.ExecuteReader(CommandBehavior.CloseConnection));
                while (rdr.Read())
                {
                    list.Add(ModelTemplatesInfoFromDataReader(rdr));
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        public IList<ModelTemplatesInfo> GetImportList(string importPath, ModelType type)
        {
            OleDbConnection connection = new OleDbConnection("Provider = Microsoft.Jet.OleDb.4.0;Data Source = " + importPath);
            IList<ModelTemplatesInfo> list = new List<ModelTemplatesInfo>();
            string cmdText = string.Empty;
            switch (type)
            {
                case ModelType.Content:
                    cmdText = " SELECT * FROM PE_ModelTemplates WHERE IsEshop = False ORDER BY TemplateID ASC";
                    break;

                case ModelType.Shop:
                    cmdText = " SELECT * FROM PE_ModelTemplates WHERE IsEshop = True ORDER BY TemplateID ASC";
                    break;

                default:
                    cmdText = " SELECT * FROM PE_ModelTemplates ORDER BY TemplateID ASC";
                    break;
            }
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(cmdText, connection);
                NullableDataReader rdr = new NullableDataReader(command.ExecuteReader(CommandBehavior.CloseConnection));
                while (rdr.Read())
                {
                    list.Add(ModelTemplatesInfoFromDataReader(rdr));
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        public ModelTemplatesInfo GetInfoById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TemplateId", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Contents_ModelTemplates_GetInfoById", cmdParams))
            {
                if (reader.Read())
                {
                    return ModelTemplatesInfoFromDataReader(reader);
                }
                return new ModelTemplatesInfo(true);
            }
        }

        private static int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_ModelTemplates", "TemplateId");
        }

        public IList<ModelTemplatesInfo> GetModelTemplateInfoList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<ModelTemplatesInfo> list = new List<ModelTemplatesInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "TemplateID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_ModelTemplates");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(ModelTemplatesInfoFromDataReader(reader));
                }
            }
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<ModelTemplatesInfo> GetModelTemplateInfoList(int startRowIndexId, int maxNumberRows, ModelType type)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<ModelTemplatesInfo> list = new List<ModelTemplatesInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "TemplateID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            switch (type)
            {
                case ModelType.Content:
                    database.AddInParameter(storedProcCommand, "@Filter", DbType.String, " IsEshop = 0");
                    break;

                case ModelType.Shop:
                    database.AddInParameter(storedProcCommand, "@Filter", DbType.String, " IsEshop = 1");
                    break;
            }
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_ModelTemplates");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(ModelTemplatesInfoFromDataReader(reader));
                }
            }
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static void GetParameters(ModelTemplatesInfo modelTemplateInfo, Parameters parms)
        {
            parms.AddInParameter("@TemplateId", DbType.Int32, modelTemplateInfo.TemplateId);
            parms.AddInParameter("@TemplateName", DbType.String, modelTemplateInfo.TemplateName);
            parms.AddInParameter("@TemplateDescription", DbType.String, modelTemplateInfo.TemplateDescription);
            parms.AddInParameter("@Field", DbType.String, modelTemplateInfo.Field);
            if (modelTemplateInfo.IsEshop == ModelType.Content)
            {
                parms.AddInParameter("@IsEshop", DbType.Int32, 0);
            }
            else
            {
                parms.AddInParameter("@IsEshop", DbType.Int32, 1);
            }
        }

        private static DbCommand GetProcdbComm(Database db, string proName, ModelTemplatesInfo modelTemplate)
        {
            DbCommand storedProcCommand = db.GetStoredProcCommand(proName);
            db.AddInParameter(storedProcCommand, "@TemplateId", DbType.Int32, modelTemplate.TemplateId);
            db.AddInParameter(storedProcCommand, "@TemplateName", DbType.String, modelTemplate.TemplateName);
            db.AddInParameter(storedProcCommand, "@TemplateDescription", DbType.String, modelTemplate.TemplateDescription);
            db.AddInParameter(storedProcCommand, "@Field", DbType.String, modelTemplate.Field);
            int num = (modelTemplate.IsEshop == ModelType.Shop) ? 1 : 0;
            db.AddInParameter(storedProcCommand, "@IsEshop", DbType.Int32, num);
            return storedProcCommand;
        }

        public bool ImportData(string templateId, string importPath)
        {
            bool flag;
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder builder = new StringBuilder("");
            builder.Append(" SELECT * FROM PE_ModelTemplates ");
            OleDbConnection connection = new OleDbConnection("Provider = Microsoft.Jet.OleDb.4.0;Data Source = " + importPath);
            try
            {
                connection.Open();
                builder.Append("WHERE TemplateID IN (" + DBHelper.ToValidId(templateId) + ") ORDER BY templateId");
                OleDbCommand command = new OleDbCommand(builder.ToString(), connection);
                NullableDataReader reader = new NullableDataReader(command.ExecuteReader(CommandBehavior.CloseConnection));
                while (reader.Read())
                {
                    ModelTemplatesInfo modelTemplate = new ModelTemplatesInfo();
                    modelTemplate.TemplateId = GetMaxId() + 1;
                    modelTemplate.TemplateName = reader.GetString("TemplateName");
                    modelTemplate.TemplateDescription = reader.GetString("TemplateDescription");
                    modelTemplate.Field = reader.GetString("Field");
                    modelTemplate.IsEshop = reader.GetBoolean("IsEshop") ? ModelType.Shop : ModelType.Content;
                    DbCommand command2 = GetProcdbComm(db, "PR_Contents_ModelTemplates_Add", modelTemplate);
                    try
                    {
                        db.ExecuteNonQuery(command2);
                        continue;
                    }
                    catch
                    {
                        reader.Close();
                        return false;
                    }
                }
                flag = true;
            }
            catch
            {
                flag = false;
            }
            finally
            {
                connection.Close();
            }
            return flag;
        }

        private static ModelTemplatesInfo ModelTemplatesInfoFromDataReader(NullableDataReader rdr)
        {
            ModelTemplatesInfo info = new ModelTemplatesInfo();
            info.TemplateId = rdr.GetInt32("TemplateID");
            info.TemplateName = rdr.GetString("TemplateName");
            info.TemplateDescription = rdr.GetString("TemplateDescription");
            info.Field = rdr.GetString("Field");
            info.IsEshop = rdr.GetBoolean("IsEshop") ? ModelType.Shop : ModelType.Content;
            return info;
        }

        public bool Update(ModelTemplatesInfo modelTemplatesInfo)
        {
            Parameters parms = new Parameters();
            GetParameters(modelTemplatesInfo, parms);
            return DBHelper.ExecuteProc("PR_Contents_ModelTemplates_Update", parms);
        }

        public bool UpdateField(int id, string fieldList)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TemplateId", DbType.Int32, id);
            cmdParams.AddInParameter("@Field", DbType.String, fieldList);
            return DBHelper.ExecuteSql("UPDATE PE_ModelTemplates SET Field = @Field WHERE TemplateID = @TemplateId", cmdParams);
        }
    }
}


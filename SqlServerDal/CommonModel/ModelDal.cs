namespace EasyOne.SqlServerDal.CommonModel
{
    using EasyOne.Enumerations;
    using EasyOne.IDal.CommonModel;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.SqlServerDal;
    using EasyOne.SqlServerDal.Contents;
    using EasyOne.SqlServerDal.Shop;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class ModelDal : IModel
    {
        public bool Add(ModelInfo modelInfo)
        {
            if (TableExists(modelInfo.TableName))
            {
                return false;
            }
            Parameters parms = new Parameters();
            modelInfo.ModelId = DBHelper.GetMaxId("PE_Model", "ModelID") + 1;
            GetParameters(modelInfo, parms);
            return DBHelper.ExecuteProc("PR_Contents_Model_Add", parms);
        }

        public void AddFieldToTable(FieldInfo fieldInfo, string tableName)
        {
            DBHelper.ExecuteNonQuerySql(Query.GetAddColumnToTableSql(fieldInfo, tableName));
        }

        public bool AddModelForNodes(int nodeId, int modelId, string templateId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@ModelId", DbType.Int32, modelId);
            cmdParams.AddInParameter("@DefaultTemplateFile", DbType.String, templateId);
            return DBHelper.ExecuteProc("PR_Contents_Model_AddModelForNodes", cmdParams);
        }

        public bool AddNodesModelTemplateRelationShip(NodesModelTemplateRelationShipInfo info)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, info.NodeId);
            cmdParams.AddInParameter("@ModelId", DbType.Int32, info.ModelId);
            cmdParams.AddInParameter("@DefaultTemplateFile", DbType.String, info.DefaultTemplateFile);
            string strSql = "INSERT INTO PE_Nodes_Model_Template(NodeId, ModelId, DefaultTemplateFile) VALUES (@NodeId, @ModelId, @DefaultTemplateFile)";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool AddTemplateForNodes(int nodeId, int templateId, bool isDefault)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@TemplateId", DbType.Int32, templateId);
            cmdParams.AddInParameter("@IsDefault", DbType.Boolean, isDefault);
            return DBHelper.ExecuteProc("PR_Contents_Model_AddTemplateForNodes", cmdParams);
        }

        public bool Delete(int modelId)
        {
            ModelInfo modelInfoById = this.GetModelInfoById(modelId);
            if (modelInfoById.IsEshop)
            {
                OrderItem item = new OrderItem();
                if (item.ExistsProduct(modelInfoById.TableName))
                {
                    return false;
                }
            }
            if (this.DeleteTable(modelId))
            {
                Parameters cmdParams = new Parameters();
                cmdParams.AddInParameter("@ModelId", DbType.Int32, modelId);
                string strSql = "DELETE FROM PE_Nodes_Model_Template WHERE ModelId = @ModelId";
                DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
                if (DBHelper.ExecuteSql("DELETE FROM PE_Model WHERE ModelId = @ModelId", cmdParams))
                {
                    try
                    {
                        DBHelper.ExecuteSql("DELETE FROM PE_CommonModel WHERE ModelId = @ModelId", cmdParams);
                        if (modelInfoById.IsEshop)
                        {
                            DBHelper.ExecuteProc("PR_Shop_Product_DeleteModel", new Parameters("@TableName", DbType.String, modelInfoById.TableName));
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool DeleteNodeModel(int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            return DBHelper.ExecuteSql("DELETE FROM PE_Nodes_Model_Template WHERE NodeId = @NodeId", cmdParams);
        }

        public bool DeleteNodesModelTemplateRelationShip(int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            string strSql = "DELETE FROM PE_Nodes_Model_Template WHERE NodeId = @NodeId";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteNodeTemplateId(int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            return DBHelper.ExecuteSql("DELETE FROM PE_Nodes_Template WHERE NodeID = @NodeID", cmdParams);
        }

        private bool DeleteTable(int modelId)
        {
            string tableName = this.GetModelInfoById(modelId).TableName;
            string strSql = "DROP TABLE " + tableName;
            try
            {
                DBHelper.ExecuteSql(strSql);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void DeleteTableField(string fieldName, string tableName)
        {
            DBHelper.ExecuteNonQuerySql(Query.GetDeleteColumnFromTableSql(fieldName, tableName));
        }

        public bool Disable(int id, bool disabled)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ModelID", DbType.Int32, id);
            cmdParams.AddInParameter("@Disabled", DbType.Boolean, disabled);
            return DBHelper.ExecuteProc("PR_Contents_Model_Disabled", cmdParams);
        }

        public bool EnableCharge(int id, bool charge)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ModelID", DbType.Int32, id);
            cmdParams.AddInParameter("@EnableCharge", DbType.Boolean, charge);
            string strSql = "UPDATE PE_Model SET EnableCharge = @EnableCharge WHERE ModelID = @ModelID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool EnableSignIn(int id, bool signIn)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ModelID", DbType.Int32, id);
            cmdParams.AddInParameter("@EnableSignin", DbType.Boolean, signIn);
            string strSql = "UPDATE PE_Model SET EnableSignin = @EnableSignin WHERE ModelID = @ModelID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool ExistsNodesModelTemplateRelationShip(NodesModelTemplateRelationShipInfo info)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, info.NodeId);
            cmdParams.AddInParameter("@ModelId", DbType.Int32, info.ModelId);
            cmdParams.AddInParameter("@DefaultTemplateFile", DbType.String, info.DefaultTemplateFile);
            string strSql = "SELECT COUNT(*) FROM PE_Nodes_Model_Template WHERE NodeId = @NodeId AND ModelId = @ModelId AND DefaultTemplateFile = @DefaultTemplateFile";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistsNodesModelTemplateRelationShip(int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            string strSql = "SELECT COUNT(*) FROM PE_Nodes_Model_Template WHERE NodeId = @NodeId";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public DataTable GetContentModelListByNodeId(int nodeId, bool enable)
        {
            bool flag = false;
            if (!enable)
            {
                flag = true;
            }
            DataTable table = new DataTable();
            table.Columns.Add("NodeId");
            table.Columns.Add("ModelId");
            table.Columns.Add("DefaultTemplateFile");
            table.Columns.Add("ModelName");
            table.Columns.Add("ItemName");
            table.Columns.Add("IsEshop");
            table.Columns.Add("AddInfoFilePath");
            table.Columns.Add("ManageInfoFilePath");
            table.Columns.Add("PreviewInfoFilePath");
            table.Columns.Add("BatchInfoFilePath");
            table.Columns.Add("MaxPerUser");
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@Disabled", DbType.Boolean, flag);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT R.NodeId, R.ModelId, R.DefaultTemplateFile, M.ModelName, M.ItemName, M.IsEshop, M.AddInfoFilePath, M.ManageInfoFilePath, M.PreviewInfoFilePath, M.BatchInfoFilePath, M.MaxPerUser FROM PE_Nodes_Model_Template R INNER JOIN PE_Model M ON R.ModelId = M.ModelID WHERE R.NodeId = @NodeId AND M.Disabled = @Disabled AND M.IsEshop = 0", cmdParams))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["NodeId"] = reader.GetInt32("NodeId");
                    row["ModelId"] = reader.GetInt32("ModelId");
                    row["DefaultTemplateFile"] = reader.GetString("DefaultTemplateFile");
                    row["ModelName"] = reader.GetString("ModelName");
                    row["ItemName"] = reader.GetString("ItemName");
                    row["IsEshop"] = reader.GetBoolean("IsEshop");
                    row["AddInfoFilePath"] = reader.GetString("AddInfoFilePath");
                    row["ManageInfoFilePath"] = reader.GetString("ManageInfoFilePath");
                    row["PreviewInfoFilePath"] = reader.GetString("PreviewInfoFilePath");
                    row["BatchInfoFilePath"] = reader.GetString("BatchInfoFilePath");
                    row["MaxPerUser"] = reader.GetInt32("MaxPerUser");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public ArrayList GetLookupField(string tableName, string fieldName, int modelId)
        {
            ArrayList list = new ArrayList();
            string strSql = "SELECT " + fieldName + " FROM " + tableName;
            if (tableName == "PE_CommonModel")
            {
                strSql = strSql + " WHERE ModelID = " + modelId.ToString();
            }
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(fieldName));
                }
            }
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Model", "ModelID");
        }

        public ModelInfo GetModelInfoById(int id)
        {
            ModelInfo info = new ModelInfo(true);
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ModelID", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Model WHERE ModelID = @ModelID", cmdParams))
            {
                if (reader.Read())
                {
                    info = ModelInfoFromDataReader(reader);
                }
            }
            return info;
        }

        public IList<ModelInfo> GetModelList(ModelType modelTyp, ModelShowType showType)
        {
            IList<ModelInfo> list = new List<ModelInfo>();
            string strSql = "SELECT * FROM PE_Model WHERE 1 = 1 ";
            Parameters cmdParams = new Parameters();
            switch (modelTyp)
            {
                case ModelType.Content:
                    strSql = strSql + " AND IsEshop = @IsEshop ";
                    cmdParams.AddInParameter("@IsEshop", DbType.Boolean, false);
                    break;

                case ModelType.Shop:
                    strSql = strSql + " AND IsEshop = @IsEshop ";
                    cmdParams.AddInParameter("@IsEshop", DbType.Boolean, true);
                    break;
            }
            switch (showType)
            {
                case ModelShowType.Enable:
                    strSql = strSql + " AND Disabled = @Disabled ";
                    cmdParams.AddInParameter("@Disabled", DbType.Boolean, false);
                    break;

                case ModelShowType.Disable:
                    strSql = strSql + " AND Disabled = @Disabled ";
                    cmdParams.AddInParameter("@Disabled", DbType.Boolean, true);
                    break;
            }
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(ModelInfoFromDataReader(reader));
                }
            }
            return list;
        }

        public DataTable GetModelListByNodeId(int nodeId, bool enable)
        {
            bool flag = false;
            if (!enable)
            {
                flag = true;
            }
            DataTable table = new DataTable();
            table.Columns.Add("NodeId");
            table.Columns.Add("ModelId");
            table.Columns.Add("DefaultTemplateFile");
            table.Columns.Add("ModelName");
            table.Columns.Add("ItemName");
            table.Columns.Add("IsEshop");
            table.Columns.Add("AddInfoFilePath");
            table.Columns.Add("ManageInfoFilePath");
            table.Columns.Add("PreviewInfoFilePath");
            table.Columns.Add("BatchInfoFilePath");
            table.Columns.Add("MaxPerUser");
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@Disabled", DbType.Boolean, flag);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT R.NodeId, R.ModelId, R.DefaultTemplateFile, M.ModelName, M.ItemName, M.IsEshop, M.AddInfoFilePath, M.ManageInfoFilePath, M.PreviewInfoFilePath, M.BatchInfoFilePath, M.MaxPerUser FROM PE_Nodes_Model_Template R INNER JOIN PE_Model M ON R.ModelId = M.ModelID WHERE R.NodeId = @NodeId AND M.Disabled = @Disabled", cmdParams))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["NodeId"] = reader.GetInt32("NodeId");
                    row["ModelId"] = reader.GetInt32("ModelId");
                    row["DefaultTemplateFile"] = reader.GetString("DefaultTemplateFile");
                    row["ModelName"] = reader.GetString("ModelName");
                    row["ItemName"] = reader.GetString("ItemName");
                    row["IsEshop"] = reader.GetBoolean("IsEshop");
                    row["AddInfoFilePath"] = reader.GetString("AddInfoFilePath");
                    row["ManageInfoFilePath"] = reader.GetString("ManageInfoFilePath");
                    row["PreviewInfoFilePath"] = reader.GetString("PreviewInfoFilePath");
                    row["BatchInfoFilePath"] = reader.GetString("BatchInfoFilePath");
                    row["MaxPerUser"] = reader.GetInt32("MaxPerUser");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public int GetNodeModeId(int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            object obj2 = DBHelper.ExecuteScalarSql("SELECT TOP 1 ModelID FROM PE_Nodes_Model_Template WHERE NodeID = @NodeID", cmdParams);
            if (obj2 == null)
            {
                return 0;
            }
            return (int) obj2;
        }

        public IList<NodesModelTemplateRelationShipInfo> GetNodesModelTemplateList(int nodeId)
        {
            IList<NodesModelTemplateRelationShipInfo> list = new List<NodesModelTemplateRelationShipInfo>();
            string strSql = "SELECT * FROM PE_Nodes_Model_Template WHERE NodeId = @NodeId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    NodesModelTemplateRelationShipInfo item = new NodesModelTemplateRelationShipInfo();
                    item.NodeId = reader.GetInt32("NodeId");
                    item.ModelId = reader.GetInt32("ModelId");
                    item.DefaultTemplateFile = reader.GetString("DefaultTemplateFile");
                    list.Add(item);
                }
            }
            return list;
        }

        public NodesModelTemplateRelationShipInfo GetNodesModelTemplateRelationShip(int nodeId, int modelId)
        {
            NodesModelTemplateRelationShipInfo info = new NodesModelTemplateRelationShipInfo();
            string strSql = "SELECT * FROM PE_Nodes_Model_Template WHERE NodeId = @NodeId AND ModelId = @ModelId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@ModelId", DbType.Int32, modelId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    info.NodeId = reader.GetInt32("NodeId");
                    info.ModelId = reader.GetInt32("ModelId");
                    info.DefaultTemplateFile = reader.GetString("DefaultTemplateFile");
                    return info;
                }
                return new NodesModelTemplateRelationShipInfo(true);
            }
        }

        private static void GetParameters(ModelInfo modelInfo, Parameters parms)
        {
            parms.AddInParameter("@ModelID", DbType.Int32, modelInfo.ModelId);
            parms.AddInParameter("@ModelName", DbType.String, modelInfo.ModelName);
            parms.AddInParameter("@IsEshop", DbType.Boolean, modelInfo.IsEshop);
            parms.AddInParameter("@Description", DbType.String, modelInfo.Description);
            parms.AddInParameter("@TableName", DbType.String, modelInfo.TableName);
            parms.AddInParameter("@ItemName", DbType.String, modelInfo.ItemName);
            parms.AddInParameter("@ItemUnit", DbType.String, modelInfo.ItemUnit);
            parms.AddInParameter("@ItemIcon", DbType.String, modelInfo.ItemIcon);
            parms.AddInParameter("@IsCountHits", DbType.Boolean, modelInfo.IsCountHits);
            parms.AddInParameter("@Disabled", DbType.Boolean, modelInfo.Disabled);
            parms.AddInParameter("@Field", DbType.String, modelInfo.Field);
            parms.AddInParameter("@DefaultTemplateFile", DbType.String, modelInfo.DefaultTemplateFile);
            parms.AddInParameter("@EnableCharge", DbType.Boolean, modelInfo.EnableCharge);
            parms.AddInParameter("@EnableSignin", DbType.Boolean, modelInfo.EnableSignIn);
            parms.AddInParameter("@ChargeTips", DbType.String, modelInfo.ChargeTips);
            parms.AddInParameter("@EnableVote", DbType.Boolean, modelInfo.EnbaleVote);
            parms.AddInParameter("@AddInfoFilePath", DbType.String, modelInfo.AddInfoFilePath);
            parms.AddInParameter("@ManageInfoFilePath", DbType.String, modelInfo.ManageInfoFilePath);
            parms.AddInParameter("@PreviewInfoFilePath", DbType.String, modelInfo.PreviewInfoFilePath);
            parms.AddInParameter("@BatchInfoFilePath", DbType.String, modelInfo.BatchInfoFilePath);
            parms.AddInParameter("@Character", DbType.Int32, (int) modelInfo.Character);
            parms.AddInParameter("@MaxPerUser", DbType.Int32, modelInfo.MaxPerUser);
            parms.AddInParameter("@PrintTemplate", DbType.String, modelInfo.PrintTemplate);
            parms.AddInParameter("@SearchTemplate", DbType.String, modelInfo.SearchTemplate);
            parms.AddInParameter("@AdvanceSearchFormTemplate", DbType.String, modelInfo.AdvanceSearchFormTemplate);
            parms.AddInParameter("@AdvanceSearchTemplate", DbType.String, modelInfo.AdvanceSearchTemplate);
            parms.AddInParameter("@CommentManageTemplate", DbType.String, modelInfo.CommentManageTemplate);
        }

        public DataTable GetShopModelListByNodeId(int nodeId, bool enable)
        {
            bool flag = false;
            if (!enable)
            {
                flag = true;
            }
            DataTable table = new DataTable();
            table.Columns.Add("NodeId");
            table.Columns.Add("ModelId");
            table.Columns.Add("DefaultTemplateFile");
            table.Columns.Add("ModelName");
            table.Columns.Add("ItemName");
            table.Columns.Add("IsEshop");
            table.Columns.Add("AddInfoFilePath");
            table.Columns.Add("ManageInfoFilePath");
            table.Columns.Add("PreviewInfoFilePath");
            table.Columns.Add("BatchInfoFilePath");
            table.Columns.Add("MaxPerUser");
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@Disabled", DbType.Boolean, flag);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT R.NodeId, R.ModelId, R.DefaultTemplateFile, M.ModelName, M.ItemName, M.IsEshop, M.AddInfoFilePath, M.ManageInfoFilePath, M.PreviewInfoFilePath, M.BatchInfoFilePath, M.MaxPerUser FROM PE_Nodes_Model_Template R INNER JOIN PE_Model M ON R.ModelId = M.ModelID WHERE R.NodeId = @NodeId AND M.Disabled = @Disabled AND M.IsEshop = 1", cmdParams))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["NodeId"] = reader.GetInt32("NodeId");
                    row["ModelId"] = reader.GetInt32("ModelId");
                    row["DefaultTemplateFile"] = reader.GetString("DefaultTemplateFile");
                    row["ModelName"] = reader.GetString("ModelName");
                    row["ItemName"] = reader.GetString("ItemName");
                    row["IsEshop"] = reader.GetBoolean("IsEshop");
                    row["AddInfoFilePath"] = reader.GetString("AddInfoFilePath");
                    row["ManageInfoFilePath"] = reader.GetString("ManageInfoFilePath");
                    row["PreviewInfoFilePath"] = reader.GetString("PreviewInfoFilePath");
                    row["BatchInfoFilePath"] = reader.GetString("BatchInfoFilePath");
                    row["MaxPerUser"] = reader.GetInt32("MaxPerUser");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public IList<int> GetTemplateIdList(int nodeId)
        {
            List<int> list = new List<int>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TemplateID FROM PE_Nodes_Template WHERE NodeID = @NodeID", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetInt32("TemplateID"));
                }
            }
            return list;
        }

        public string GetXmlFieldByModelId(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Id", DbType.Int32, id);
            object obj2 = DBHelper.ExecuteScalarSql("SELECT Field FROM PE_Model WHERE ModelId = @Id", cmdParams);
            if (obj2 == null)
            {
                return null;
            }
            return Convert.ToString(obj2);
        }

        public bool ModelIdExists(int nodeId, int modelId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@ModelId", DbType.Int32, modelId);
            return DBHelper.ExistsProc("PR_Contents_Model_ModelIdExists", cmdParams);
        }

        private static ModelInfo ModelInfoFromDataReader(NullableDataReader rdr)
        {
            ModelInfo info = new ModelInfo();
            info.ModelId = rdr.GetInt32("ModelID");
            info.ModelName = rdr.GetString("ModelName");
            info.IsEshop = rdr.GetBoolean("IsEshop");
            info.Description = rdr.GetString("Description");
            info.TableName = rdr.GetString("TableName");
            info.ItemName = rdr.GetString("ItemName");
            info.ItemUnit = rdr.GetString("ItemUnit");
            info.ItemIcon = rdr.GetString("ItemIcon");
            info.IsCountHits = rdr.GetBoolean("IsCountHits");
            info.Disabled = rdr.GetBoolean("Disabled");
            info.Field = rdr.GetString("Field");
            info.DefaultTemplateFile = rdr.GetString("DefaultTemplateFile");
            info.EnableCharge = rdr.GetBoolean("EnableCharge");
            info.EnableSignIn = rdr.GetBoolean("EnableSignin");
            info.EnbaleVote = rdr.GetBoolean("EnableVote");
            info.ChargeTips = rdr.GetString("ChargeTips");
            info.AddInfoFilePath = rdr.GetString("AddInfoFilePath");
            info.ManageInfoFilePath = rdr.GetString("ManageInfoFilePath");
            info.PreviewInfoFilePath = rdr.GetString("PreviewInfoFilePath");
            info.BatchInfoFilePath = rdr.GetString("BatchInfoFilePath");
            info.MaxPerUser = rdr.GetInt32("MaxPerUser");
            info.PrintTemplate = rdr.GetString("PrintTemplate");
            info.SearchTemplate = rdr.GetString("SearchTemplate");
            info.AdvanceSearchFormTemplate = rdr.GetString("AdvanceSearchFormTemplate");
            info.AdvanceSearchTemplate = rdr.GetString("AdvanceSearchTemplate");
            info.CommentManageTemplate = rdr.GetString("CommentManageTemplate");
            info.Character = (ProductCharacter) rdr.GetInt32("Character");
            return info;
        }

        public bool ModelNameExists(string modelName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ModelName", DbType.String, modelName);
            return DBHelper.ExistsProc("PR_Contents_Model_IsModelNameExists", cmdParams);
        }

        private static bool TableExists(string tableName)
        {
            string strSql = "SELECT COUNT(*) FROM [PE_Model] WHERE TableName = @TableName";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool TableNameExists(string tableName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            return DBHelper.ExistsProc("PR_Contents_Model_IsTableNameExists", cmdParams);
        }

        public bool TemplateIdExists(int nodeId, int templateId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@TemplateId", DbType.Int32, templateId);
            return DBHelper.ExistsProc("PR_Contents_Model_TemplateIdExists", cmdParams);
        }

        public bool Update(ModelInfo modelInfo)
        {
            string strSql = "UPDATE [PE_Model]  SET ModelID = @ModelID, ModelName = @ModelName, Description = @Description, TableName = @TableName, ItemName = @ItemName, ItemUnit = @ItemUnit, ItemIcon = @ItemIcon, IsCountHits = @IsCountHits, IsEshop = @IsEshop, Disabled = @Disabled, DefaultTemplateFile = @DefaultTemplateFile, EnableCharge = @EnableCharge, EnableSignin = @EnableSignin, AddInfoFilePath = @AddInfoFilePath, ManageInfoFilePath = @ManageInfoFilePath, PreviewInfoFilePath = @PreviewInfoFilePath, BatchInfoFilePath = @BatchInfoFilePath, MaxPerUser = @MaxPerUser, PrintTemplate = @PrintTemplate, EnableVote = @EnableVote, SearchTemplate = @SearchTemplate, AdvanceSearchFormTemplate = @AdvanceSearchFormTemplate, AdvanceSearchTemplate = @AdvanceSearchTemplate, ChargeTips = @ChargeTips, CommentManageTemplate = @CommentManageTemplate WHERE ModelID = @ModelID";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ModelID", DbType.Int32, modelInfo.ModelId);
            cmdParams.AddInParameter("@ModelName", DbType.String, modelInfo.ModelName);
            cmdParams.AddInParameter("@IsEshop", DbType.Boolean, modelInfo.IsEshop);
            cmdParams.AddInParameter("@Description", DbType.String, modelInfo.Description);
            cmdParams.AddInParameter("@TableName", DbType.String, modelInfo.TableName);
            cmdParams.AddInParameter("@ItemName", DbType.String, modelInfo.ItemName);
            cmdParams.AddInParameter("@ItemUnit", DbType.String, modelInfo.ItemUnit);
            cmdParams.AddInParameter("@ItemIcon", DbType.String, modelInfo.ItemIcon);
            cmdParams.AddInParameter("@IsCountHits", DbType.Boolean, modelInfo.IsCountHits);
            cmdParams.AddInParameter("@Disabled", DbType.Boolean, modelInfo.Disabled);
            cmdParams.AddInParameter("@DefaultTemplateFile", DbType.String, modelInfo.DefaultTemplateFile);
            cmdParams.AddInParameter("@EnableCharge", DbType.Boolean, modelInfo.EnableCharge);
            cmdParams.AddInParameter("@ChargeTips", DbType.String, modelInfo.ChargeTips);
            cmdParams.AddInParameter("@EnableSignin", DbType.Boolean, modelInfo.EnableSignIn);
            cmdParams.AddInParameter("@EnableVote", DbType.Boolean, modelInfo.EnbaleVote);
            cmdParams.AddInParameter("@AddInfoFilePath", DbType.String, modelInfo.AddInfoFilePath);
            cmdParams.AddInParameter("@ManageInfoFilePath", DbType.String, modelInfo.ManageInfoFilePath);
            cmdParams.AddInParameter("@PreviewInfoFilePath", DbType.String, modelInfo.PreviewInfoFilePath);
            cmdParams.AddInParameter("@BatchInfoFilePath", DbType.String, modelInfo.BatchInfoFilePath);
            cmdParams.AddInParameter("@MaxPerUser", DbType.Int32, modelInfo.MaxPerUser);
            cmdParams.AddInParameter("@PrintTemplate", DbType.String, modelInfo.PrintTemplate);
            cmdParams.AddInParameter("@SearchTemplate", DbType.String, modelInfo.SearchTemplate);
            cmdParams.AddInParameter("@AdvanceSearchFormTemplate", DbType.String, modelInfo.AdvanceSearchFormTemplate);
            cmdParams.AddInParameter("@AdvanceSearchTemplate", DbType.String, modelInfo.AdvanceSearchTemplate);
            cmdParams.AddInParameter("@CommentManageTemplate", DbType.String, modelInfo.CommentManageTemplate);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void UpdateField(int modelId, string fieldList)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ModelID", DbType.Int32, modelId);
            cmdParams.AddInParameter("@Field", DbType.String, fieldList);
            DBHelper.ExecuteNonQuerySql("UPDATE PE_Model SET Field = @Field WHERE ModelID = @ModelID", cmdParams);
        }

        public void UpdateTableField(FieldInfo fieldInfo, string tableName)
        {
            DBHelper.ExecuteNonQuerySql(Query.GetAlterColumnToTableSql(fieldInfo, tableName));
        }
    }
}


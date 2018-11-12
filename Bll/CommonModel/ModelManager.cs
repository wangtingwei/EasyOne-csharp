namespace EasyOne.CommonModel
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.CommonModel;
    using EasyOne.Logging;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web;

    public sealed class ModelManager
    {
        private static readonly IModel dal = EasyOne.DalFactory.DataAccess.CreateCotentModel();

        private ModelManager()
        {
        }

        public static bool Add(ModelInfo modelInfo, int modelTemplateId)
        {
            CreateCommonListPageTemplate(modelInfo, false);
            return Add(modelInfo, modelTemplateId, ModelType.Content);
        }

        public static bool Add(ModelInfo modelInfo, int modelTemplateId, ModelType type)
        {
            bool flag = false;
            if ((modelTemplateId == 0) && (type == ModelType.Content))
            {
                modelInfo.Field = AddDefaultField();
            }
            else
            {
                modelInfo.Field = ModelTemplate.GetField(modelTemplateId);
            }
            if (type == ModelType.Shop)
            {
                CreateCommonListPageTemplate(modelInfo, true);
            }
            if (dal.Add(modelInfo))
            {
                flag = true;
                if (modelTemplateId != 0)
                {
                    List<FieldInfo> list = new Serialize<FieldInfo>().DeserializeFieldList(modelInfo.Field);
                    if (list != null)
                    {
                        list.Sort(new FieldInfoComparer());
                    }
                    foreach (FieldInfo info in list)
                    {
                        if (info.FieldLevel != 0)
                        {
                            dal.AddFieldToTable(info, modelInfo.TableName);
                        }
                        if (info.FieldType == FieldType.ContentType)
                        {
                            FieldInfo fieldInfo = new FieldInfo();
                            fieldInfo.FieldName = info.Settings[2];
                            fieldInfo.FieldType = FieldType.ContentType;
                            fieldInfo.FieldLevel = 1;
                            AddTableField(fieldInfo, modelInfo.ModelId);
                            FieldInfo info3 = new FieldInfo();
                            info3.FieldName = info.Settings[5];
                            info3.FieldType = FieldType.ContentType;
                            info3.FieldLevel = 1;
                            AddTableField(info3, modelInfo.ModelId);
                        }
                    }
                }
            }
            RemoveCache();
            return flag;
        }

        public static string AddDefaultField()
        {
            IList<FieldInfo> fieldList = new List<FieldInfo>();
            string[] strArray = new string[] { "Title", "Status", "EliteLevel", "Priority", "Hits", "DayHits", "WeekHits", "MonthHits", "UpdateTime", "NodeId", "TemplateFile", "InfoId", "SpecialId", "DefaultPicUrl" };
            string[] strArray2 = new string[] { "标题", "状态", "推荐级别", "优先级", "点击数", "日点击数", "周点击数", "月点击数", "更新时间", "所属节点", "内容页模板", "所属其它节点", "专题", "首页图片" };
            string[] strArray3 = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            string[] strArray4 = new string[] { "", "审核状态", "&nbsp;<span style='color:Blue;'>注：数字越大推荐级别越高</span>", "&nbsp;<span style='color:Blue;'>注：数字越大优先级别越高</span>", "", "", "", "", "更新时间", "", "内容页模板地址", "如一篇文章同时属于多个栏目，在此选择即可！", "", "" };
            string[] strArray5 = new string[] { "", "0", "0", "0", "0", "0", "0", "0", "Now", "", "", "", "", "" };
            FieldType[] typeArray = new FieldType[] { FieldType.TitleType, FieldType.StatusType, FieldType.NumberType, FieldType.NumberType, FieldType.NumberType, FieldType.NumberType, FieldType.NumberType, FieldType.NumberType, FieldType.DateTimeType, FieldType.NodeType, FieldType.TemplateType, FieldType.InfoType, FieldType.SpecialType, FieldType.PictureType };
            Collection<string> item = new Collection<string>();
            item.Add("255");
            item.Add("50");
            item.Add("0");
            Collection<string> collection2 = new Collection<string>();
            collection2.Add("0");
            collection2.Add("100");
            collection2.Add("-1");
            collection2.Add("0");
            Collection<string> collection3 = new Collection<string>();
            collection3.Add("0");
            collection3.Add("100");
            collection3.Add("-1");
            collection3.Add("0");
            Collection<string> collection4 = new Collection<string>();
            collection4.Add("0");
            collection4.Add("100");
            collection4.Add("-1");
            collection4.Add("0");
            Collection<string> collection5 = new Collection<string>();
            collection5.Add("0");
            collection5.Add("");
            collection5.Add("-1");
            collection5.Add("0");
            Collection<string> collection6 = new Collection<string>();
            collection6.Add("0");
            collection6.Add("");
            collection6.Add("-1");
            collection6.Add("0");
            Collection<string> collection7 = new Collection<string>();
            collection7.Add("0");
            collection7.Add("");
            collection7.Add("-1");
            collection7.Add("0");
            Collection<string> collection8 = new Collection<string>();
            collection8.Add("0");
            collection8.Add("");
            collection8.Add("-1");
            collection8.Add("0");
            Collection<string> collection9 = new Collection<string>();
            collection9.Add("yyyy-MM-dd HH:mm:ss");
            collection9.Add("1");
            collection9.Add("1");
            collection9.Add("");
            Collection<string> collection10 = new Collection<string>();
            collection10.Add("");
            collection10.Add("");
            collection10.Add("");
            collection10.Add("");
            Collection<string> collection11 = new Collection<string>();
            collection11.Add("");
            collection11.Add("");
            collection11.Add("");
            collection11.Add("");
            Collection<string> collection12 = new Collection<string>();
            collection12.Add("");
            collection12.Add("");
            collection12.Add("");
            collection12.Add("");
            Collection<string> collection13 = new Collection<string>();
            collection13.Add("");
            collection13.Add("");
            collection13.Add("");
            collection13.Add("");
            Collection<string> collection14 = new Collection<string>();
            collection14.Add("30");
            collection14.Add("1024");
            collection14.Add("jpg|gif|bmp");
            collection14.Add("0");
            collection14.Add("0");
            collection14.Add("0");
            collection14.Add("False");
            collection14.Add("False");
            Collection<Collection<string>> collection15 = new Collection<Collection<string>>();
            collection15.Add(item);
            collection15.Add(collection2);
            collection15.Add(collection3);
            collection15.Add(collection4);
            collection15.Add(collection5);
            collection15.Add(collection6);
            collection15.Add(collection7);
            collection15.Add(collection8);
            collection15.Add(collection9);
            collection15.Add(collection10);
            collection15.Add(collection11);
            collection15.Add(collection12);
            collection15.Add(collection13);
            collection15.Add(collection14);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 14; i++)
            {
                FieldInfo info = new FieldInfo();
                info.Id = strArray[i];
                info.FieldName = strArray[i];
                info.FieldAlias = strArray2[i];
                info.Tips = strArray3[i];
                info.OrderId = i + 1;
                info.FieldType = typeArray[i];
                info.Disabled = false;
                info.Description = strArray4[i];
                info.DefaultValue = strArray5[i];
                if (i == 10)
                {
                    info.EnableNull = false;
                }
                else
                {
                    info.EnableNull = true;
                }
                info.FieldLevel = 0;
                info.CopyToSettings(collection15[i]);
                info.EnableShowOnSearchForm = true;
                fieldList.Add(info);
            }
            builder.Append(Field.SerializeFieldList(fieldList));
            return builder.ToString();
        }

        public static bool AddFieldToTable(FieldInfo fieldInfo, int modelId)
        {
            IList<FieldInfo> list = new List<FieldInfo>();
            string xmlFieldByModelId = GetXmlFieldByModelId(modelId);
            Serialize<FieldInfo> serialize = new Serialize<FieldInfo>();
            if (!string.IsNullOrEmpty(xmlFieldByModelId))
            {
                list = serialize.DeserializeFieldList(xmlFieldByModelId);
            }
            else
            {
                LogInfo info = new LogInfo();
                info.Category = LogCategory.SystemAction;
                info.Message = "模型ID 为" + modelId.ToString() + "的模型没有获取到字段";
                info.ScriptName = "ModelManager.cs";
                info.Source = "GetXmlFieldByModelId(modelId)返回的内容为空";
                info.Timestamp = DateTime.Now;
                info.Title = "AddFieldToTable方法出错";
                info.UserName = "System";
                LogFactory.CreateLog().Add(info);
            }
            list.Add(fieldInfo);
            xmlFieldByModelId = serialize.SerializeFieldList(list);
            bool flag = false;
            flag = UpdateField(modelId, xmlFieldByModelId);
            if (flag && (fieldInfo.FieldType != FieldType.Property))
            {
                flag = AddTableField(fieldInfo, modelId);
            }
            return flag;
        }

        public static bool AddModelForNodes(int nodeId, DataTable contentData)
        {
            bool flag = true;
            int count = contentData.Rows.Count;
            if (count == 0)
            {
                flag = false;
            }
            for (int i = 0; i < count; i++)
            {
                int modelId = DataConverter.CLng(contentData.Rows[i]["ModelId"]);
                string templateId = contentData.Rows[i]["TemplateFile"].ToString();
                if (!ModelIdExists(nodeId, modelId) && !dal.AddModelForNodes(nodeId, modelId, templateId))
                {
                    return false;
                }
            }
            return flag;
        }

        public static bool AddNodesModelTemplateRelationShip(NodesModelTemplateRelationShipInfo info)
        {
            bool flag = true;
            if ((!string.IsNullOrEmpty(info.DefaultTemplateFile) && (info.NodeId > 0)) && (info.ModelId > 0))
            {
                info.DefaultTemplateFile = info.DefaultTemplateFile;
                flag = dal.AddNodesModelTemplateRelationShip(info);
            }
            return flag;
        }

        public static bool AddTableField(FieldInfo fieldInfo, int modelId)
        {
            ModelInfo modelInfoById = GetModelInfoById(modelId);
            try
            {
                dal.AddFieldToTable(fieldInfo, modelInfoById.TableName);
                return true;
            }
            catch (DataException exception)
            {
                LogInfo info = new LogInfo();
                info.Category = LogCategory.SystemAction;
                info.Message = "模型ID 为" + modelId.ToString() + "的模型在修改" + modelInfoById.TableName + "表的字段时产生错误！";
                info.ScriptName = "ModelManager.cs";
                info.Source = "dal.AddFieldToTable(fieldInfo, modelInfo.TableName)错误<br/>" + exception.Source;
                info.Timestamp = DateTime.Now;
                info.Title = "AddTableField方法出错";
                info.UserName = "System";
                LogFactory.CreateLog().Add(info);
                return false;
            }
        }

        public static bool AddTemplateForNodes(int nodeId, IList<int> templateIdList, int defaultTemplateId)
        {
            bool flag = true;
            if (templateIdList.Count == 0)
            {
                flag = false;
            }
            foreach (int num2 in templateIdList)
            {
                bool isDefault = false;
                if (num2 == defaultTemplateId)
                {
                    isDefault = true;
                }
                if (!TemplateIdExists(nodeId, num2) && !dal.AddTemplateForNodes(nodeId, num2, isDefault))
                {
                    return false;
                }
            }
            return flag;
        }

        public static IList<ModelInfo> ContentModelList(ModelShowType showType)
        {
            return GetModelList(ModelType.Content, showType);
        }

        private static void CreateCommonLable(ModelInfo modelInfo)
        {
            string virtualPath = HttpContext.Current.Server.MapPath("~/");
            FileInfo[] files = new DirectoryInfo(VirtualPathUtility.AppendTrailingSlash(virtualPath) + "CommonTemplate/").GetFiles();
            string str3 = VirtualPathUtility.AppendTrailingSlash(virtualPath + SiteConfig.SiteOption.LabelDir);
            foreach (FileInfo info2 in files)
            {
                if ((string.Compare(info2.Extension, ".config", StringComparison.OrdinalIgnoreCase) == 0) && (string.Compare(info2.Name.Replace(".config", ""), "内容页", StringComparison.OrdinalIgnoreCase) != 0))
                {
                    string str4 = info2.Name.ToLower().Replace(".config", "_" + modelInfo.ModelName + ".config");
                    using (StreamReader reader = info2.OpenText())
                    {
                        string str5 = reader.ReadToEnd().Replace("{$$$ItemName$$$}", modelInfo.ItemName).Replace("{$$$ModelName$$$}", modelInfo.ModelName).Replace("{$$$TableName$$$}", modelInfo.TableName);
                        using (StreamWriter writer = new StreamWriter(str3 + str4))
                        {
                            writer.Write(str5);
                        }
                    }
                }
            }
        }

        private static void CreateCommonListPageTemplate(ModelInfo modelInfo, bool isEshop)
        {
            string str2;
            string virtualPath = HttpContext.Current.Server.MapPath("~/");
            string fileContent = FileSystemObject.ReadFile(VirtualPathUtility.AppendTrailingSlash(virtualPath) + "CommonTemplate/栏目列表页模板.htm").Replace("{$$$TableName$$$}", modelInfo.TableName).Replace("{$$$ModelName$$$}", modelInfo.ModelName).Replace("{$$$ItemName$$$}", modelInfo.ItemName);
            string str4 = VirtualPathUtility.AppendTrailingSlash(virtualPath) + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.TemplateDir) + modelInfo.ModelName;
            FileSystemObject.WriteFile(str4 + "/栏目列表页模板.html", fileContent);
            if (isEshop)
            {
                str2 = VirtualPathUtility.AppendTrailingSlash(virtualPath) + "CommonTemplate/内容页模板_商品模型.htm";
            }
            else
            {
                str2 = VirtualPathUtility.AppendTrailingSlash(virtualPath) + "CommonTemplate/内容页模板.htm";
            }
            fileContent = FileSystemObject.ReadFile(str2).Replace("{$$$ContentLabelName$$$}", "内容页_" + modelInfo.ModelName).Replace("{$$$ModelName$$$}", modelInfo.ModelName).Replace("{$$$ItemName$$$}", modelInfo.ItemName);
            FileSystemObject.WriteFile(str4 + "/内容页模板.html", fileContent);
            CreateCommonLable(modelInfo);
            CreateContentLabel(virtualPath, modelInfo);
        }

        private static void CreateContentLabel(string path, ModelInfo modelInfo)
        {
            string str = "内容页.config";
            string str2 = path + "CommonTemplate/" + str;
            string str3 = VirtualPathUtility.AppendTrailingSlash(path + SiteConfig.SiteOption.LabelDir);
            string fileContent = string.Empty;
            using (StreamReader reader = File.OpenText(str2))
            {
                fileContent = reader.ReadToEnd();
            }
            fileContent = fileContent.Replace("{$$$TableName$$$}", modelInfo.TableName).Replace("{$$$ItemName$$$}", modelInfo.ItemName).Replace("{$$$ModelName$$$}", modelInfo.ModelName).Replace("{$$$Custom$$$}", "");
            FileSystemObject.WriteFile(str3 + "内容页_" + modelInfo.ModelName + ".config", fileContent);
        }

        public static bool Delete(int modelId)
        {
            DeleteTemplateAndLabel(modelId);
            if (dal.Delete(modelId))
            {
                RemoveCache();
                return true;
            }
            return false;
        }

        public static bool DeleteNodeModel(int nodeId)
        {
            return dal.DeleteNodeModel(nodeId);
        }

        public static bool DeleteNodesModelTemplateRelationShip(int nodeId)
        {
            return dal.DeleteNodesModelTemplateRelationShip(nodeId);
        }

        public static bool DeleteNodeTemplateId(int nodeId)
        {
            return dal.DeleteNodeTemplateId(nodeId);
        }

        public static bool DeleteTableField(string fieldName, string tableName)
        {
            try
            {
                dal.DeleteTableField(fieldName, tableName);
                return true;
            }
            catch (DataException exception)
            {
                LogInfo info = new LogInfo();
                info.Category = LogCategory.Exception;
                info.Message = exception.Message;
                info.Priority = LogPriority.Low;
                info.Source = exception.Source;
                info.Timestamp = DateTime.Now;
                info.Title = "删除" + tableName + "表的" + fieldName + "字段出错！";
                info.UserName = "System";
                LogFactory.CreateLog().Add(info);
                return false;
            }
        }

        private static void DeleteTemplateAndLabel(int modelId)
        {
            if (modelId >= 0x17)
            {
                ModelInfo modelInfoById = GetModelInfoById(modelId);
                if (!modelInfoById.IsNull)
                {
                    string virtualPath = HttpContext.Current.Server.MapPath("~/");
                    string path = VirtualPathUtility.AppendTrailingSlash(virtualPath) + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.TemplateDir) + VirtualPathUtility.AppendTrailingSlash(modelInfoById.ModelName);
                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                    FileInfo[] files = new DirectoryInfo(VirtualPathUtility.AppendTrailingSlash(virtualPath) + "CommonTemplate/").GetFiles();
                    string str4 = VirtualPathUtility.AppendTrailingSlash(virtualPath + SiteConfig.SiteOption.LabelDir);
                    foreach (FileInfo info3 in files)
                    {
                        if (string.Compare(info3.Extension, ".config", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            string str5 = info3.Name.ToLower().Replace(".config", "_" + modelInfoById.ModelName + ".config");
                            if (File.Exists(str4 + str5))
                            {
                                File.Delete(str4 + str5);
                            }
                        }
                    }
                }
            }
        }

        public static bool Disable(int id, bool disabled)
        {
            return dal.Disable(id, disabled);
        }

        public static bool EnableCharge(int id, bool charge)
        {
            return dal.EnableCharge(id, charge);
        }

        public static bool EnableSignIn(int id, bool signIn)
        {
            return dal.EnableSignIn(id, signIn);
        }

        public static bool ExistsNodesModelTemplateRelationShip(NodesModelTemplateRelationShipInfo info)
        {
            return dal.ExistsNodesModelTemplateRelationShip(info);
        }

        public static bool ExistsNodesModelTemplateRelationShip(int nodeId)
        {
            return dal.ExistsNodesModelTemplateRelationShip(nodeId);
        }

        public static string GetCacheContentModelIdList()
        {
            string key = "CK_CommonModel_String_ModelIdArr_Content";
            string str2 = SiteCache.Get(key) as string;
            if (string.IsNullOrEmpty(str2))
            {
                StringBuilder builder = new StringBuilder();
                foreach (ModelInfo info in GetModelList(ModelType.Content, ModelShowType.Enable))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append(info.ModelId.ToString());
                }
                str2 = builder.ToString();
                SiteCache.Insert(key, str2, 0x4380);
            }
            return str2;
        }

        public static ModelInfo GetCacheModelById(int modelId)
        {
            IList<ModelInfo> cacheModelList = GetCacheModelList();
            ModelInfo info = new ModelInfo(true);
            foreach (ModelInfo info2 in cacheModelList)
            {
                if (info2.ModelId == modelId)
                {
                    return info2;
                }
            }
            return info;
        }

        public static ModelInfo GetCacheModelByTableName(string tableName)
        {
            IList<ModelInfo> cacheModelList = GetCacheModelList();
            ModelInfo info = new ModelInfo(true);
            foreach (ModelInfo info2 in cacheModelList)
            {
                if (info2.TableName == tableName)
                {
                    return info2;
                }
            }
            return info;
        }

        public static IList<ModelInfo> GetCacheModelList()
        {
            string key = "CK_CommonModel_ModelList_All";
            IList<ModelInfo> modelList = SiteCache.Get(key) as IList<ModelInfo>;
            if (modelList == null)
            {
                modelList = GetModelList(ModelType.None, ModelShowType.None);
                SiteCache.Insert(key, modelList, 0x4380);
            }
            return modelList;
        }

        public static DataTable GetContentModelListByNodeId(int nodeId, bool enable)
        {
            return dal.GetContentModelListByNodeId(nodeId, enable);
        }

        public static IList<FieldInfo> GetFieldListByModelId(int modelId)
        {
            string xmlFieldByModelId = GetXmlFieldByModelId(modelId);
            List<FieldInfo> list = new Serialize<FieldInfo>().DeserializeFieldList(xmlFieldByModelId);
            if (list != null)
            {
                list.Sort(new FieldInfoComparer());
            }
            return list;
        }

        public static ArrayList GetLookupField(string tableName, string fieldName, int modelId)
        {
            return dal.GetLookupField(tableName, fieldName, modelId);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static ModelInfo GetModelInfoById(int id)
        {
            string key = "CK_CommonModel_ModelInfo_ModelId_" + id.ToString();
            ModelInfo modelInfoById = SiteCache.Get(key) as ModelInfo;
            if (modelInfoById == null)
            {
                modelInfoById = dal.GetModelInfoById(id);
                SiteCache.Insert(key, modelInfoById);
            }
            return modelInfoById;
        }

        public static IList<ModelInfo> GetModelList(ModelType modelType, ModelShowType showType)
        {
            return dal.GetModelList(modelType, showType);
        }

        public static DataTable GetModelListByNodeId(int nodeId, bool enable)
        {
            return dal.GetModelListByNodeId(nodeId, enable);
        }

        public static IList<FieldInfo> GetNodeFieldList(int nodeId)
        {
            return GetFieldListByModelId(dal.GetNodeModeId(nodeId));
        }

        public static IList<NodesModelTemplateRelationShipInfo> GetNodesModelTemplateList(int nodeId)
        {
            return dal.GetNodesModelTemplateList(nodeId);
        }

        public static NodesModelTemplateRelationShipInfo GetNodesModelTemplateRelationShip(int nodeId, int modelId)
        {
            return dal.GetNodesModelTemplateRelationShip(nodeId, modelId);
        }

        public static IList<ModelInfo> GetShopModelList(ModelShowType showType)
        {
            return GetModelList(ModelType.Shop, showType);
        }

        public static DataTable GetShopModelListByNodeId(int nodeId, bool enable)
        {
            return dal.GetShopModelListByNodeId(nodeId, enable);
        }

        public static IList<int> GetTemplateIdList(int nodeId)
        {
            return dal.GetTemplateIdList(nodeId);
        }

        public static string GetXmlFieldByModelId(int id)
        {
            return dal.GetXmlFieldByModelId(id);
        }

        public static bool ModelIdExists(int nodeId, int modelId)
        {
            return dal.ModelIdExists(nodeId, modelId);
        }

        public static bool ModelNameExists(string modelName)
        {
            return dal.ModelNameExists(modelName);
        }

        public static void RemoveCache()
        {
            SiteCache.RemoveByPattern(@"CK_CommonModel_\S*");
        }

        public static bool TableNameExists(string tableName)
        {
            return dal.TableNameExists(tableName);
        }

        public static bool TemplateIdExists(int nodeId, int templateId)
        {
            return dal.TemplateIdExists(nodeId, templateId);
        }

        public static bool Update(ModelInfo modelInfo)
        {
            if (dal.Update(modelInfo))
            {
                RemoveCache();
                return true;
            }
            return false;
        }

        public static bool UpdateField(int modelId, string fieldList)
        {
            try
            {
                dal.UpdateField(modelId, fieldList);
                return true;
            }
            catch (DataException exception)
            {
                LogInfo info = new LogInfo();
                info.Category = LogCategory.SystemAction;
                info.Message = exception.Message;
                info.ScriptName = "ModelManager.cs";
                info.Source = exception.Source;
                info.Timestamp = DateTime.Now;
                info.Title = "模型ID为" + modelId + "的fieldlist字段更新出错";
                info.UserName = "System";
                LogFactory.CreateLog().Add(info);
                return false;
            }
        }

        public static bool UpdateFieldOfTable(FieldInfo fieldInfo, int modelId)
        {
            IList<FieldInfo> fieldListByModelId = new List<FieldInfo>();
            fieldListByModelId = GetFieldListByModelId(modelId);
            FieldInfo item = null;
            foreach (FieldInfo info2 in fieldListByModelId)
            {
                if (info2.Id == fieldInfo.Id)
                {
                    item = info2;
                }
            }
            fieldListByModelId.Remove(item);
            fieldListByModelId.Add(fieldInfo);
            string fieldList = new Serialize<FieldInfo>().SerializeFieldList(fieldListByModelId);
            bool flag = false;
            FieldInfo fieldInfoByFieldName = Field.GetFieldInfoByFieldName(modelId, fieldInfo.FieldName);
            if (fieldInfoByFieldName.IsNull)
            {
                return false;
            }
            if (!UpdateField(modelId, fieldList))
            {
                return flag;
            }
            if (fieldInfo.FieldLevel != 0)
            {
                switch (fieldInfo.FieldType)
                {
                    case FieldType.MultipleTextType:
                    case FieldType.MultipleHtmlTextType:
                    case FieldType.ContentType:
                    case FieldType.MultiplePhotoType:
                        if (fieldInfoByFieldName.FieldType == FieldType.TextType)
                        {
                            return UpdateTableField(fieldInfo, modelId);
                        }
                        return true;
                }
            }
            return true;
        }

        public static bool UpdateNodesModelTemplateRelationShip(int nodeId, IList<NodesModelTemplateRelationShipInfo> infoList)
        {
            bool flag = true;
            bool flag2 = true;
            if (ExistsNodesModelTemplateRelationShip(nodeId))
            {
                flag2 = DeleteNodesModelTemplateRelationShip(nodeId);
            }
            if (!flag2)
            {
                return false;
            }
            foreach (NodesModelTemplateRelationShipInfo info in infoList)
            {
                info.NodeId = nodeId;
                if ((!string.IsNullOrEmpty(info.DefaultTemplateFile) && (info.NodeId > 0)) && (info.ModelId > 0))
                {
                    flag = AddNodesModelTemplateRelationShip(info);
                }
            }
            return flag;
        }

        public static bool UpdateTableField(FieldInfo fieldInfo, int modelId)
        {
            ModelInfo modelInfoById = GetModelInfoById(modelId);
            try
            {
                dal.UpdateTableField(fieldInfo, modelInfoById.TableName);
                return true;
            }
            catch (DataException exception)
            {
                LogInfo info = new LogInfo();
                info.Category = LogCategory.Exception;
                info.Message = exception.Message;
                info.Priority = LogPriority.Low;
                info.Source = exception.Source;
                info.Timestamp = DateTime.Now;
                info.Title = "更新" + modelInfoById.ModelName + "模型的" + fieldInfo.FieldName + "字段出错！";
                info.UserName = "System";
                LogFactory.CreateLog().Add(info);
                return false;
            }
        }
    }
}


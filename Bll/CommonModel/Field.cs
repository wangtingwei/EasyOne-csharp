namespace EasyOne.CommonModel
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public sealed class Field
    {
        private static Serialize<FieldInfo> ser = new Serialize<FieldInfo>();

        private Field()
        {
        }

        public static DataActionState Add(FieldInfo fieldInfo, int modelId)
        {
            DataActionState successed = DataActionState.Successed;
            if (FieldExists(modelId, fieldInfo.FieldName))
            {
                return DataActionState.Exist;
            }
            if (((fieldInfo.FieldType == FieldType.FileType) && DataConverter.CBoolean(fieldInfo.Settings[3])) && !string.IsNullOrEmpty(fieldInfo.Settings[4]))
            {
                FieldInfo info = new FieldInfo();
                info.Id = fieldInfo.Settings[4];
                info.FieldName = fieldInfo.Settings[4];
                info.FieldType = FieldType.TextType;
                info.FieldLevel = 2;
                info.OrderId = 0;
                DataActionState state2 = Add(info, modelId);
                if (state2 != DataActionState.Successed)
                {
                    return state2;
                }
            }
            if (!ModelManager.AddFieldToTable(fieldInfo, modelId))
            {
                successed = DataActionState.Unknown;
            }
            return successed;
        }

        public static bool Delete(string fieldName, int modelId)
        {
            fieldName = fieldName.ToLower();
            FieldInfo fieldInfoByFieldName = GetFieldInfoByFieldName(modelId, fieldName);
            if (fieldInfoByFieldName != null)
            {
                if (fieldInfoByFieldName.FieldLevel == 0)
                {
                    return false;
                }
                if (((fieldInfoByFieldName.FieldType == FieldType.FileType) && !DataConverter.CBoolean(fieldInfoByFieldName.Settings[3])) && !string.IsNullOrEmpty(fieldInfoByFieldName.Settings[4]))
                {
                    Delete(fieldInfoByFieldName.Settings[4], modelId);
                }
            }
            ModelInfo modelInfoById = ModelManager.GetModelInfoById(modelId);
            XmlManage manage = XmlManage.Instance(modelInfoById.Field, XmlType.Content);
            string nodeName = "/ArrayOfFieldInfo/FieldInfo[@Id=\"" + fieldName + "\"]";
            manage.Remove(nodeName);
            if (!UpdateField(modelId, manage.Xml))
            {
                return false;
            }
            return (((fieldInfoByFieldName != null) && (fieldInfoByFieldName.FieldType == FieldType.Property)) || ModelManager.DeleteTableField(fieldName, modelInfoById.TableName));
        }

        public static bool FieldExists(int modelId, string fieldName)
        {
            string xmlFieldList = GetXmlFieldList(modelId);
            if (string.IsNullOrEmpty(xmlFieldList))
            {
                return false;
            }
            string nodeName = "/ArrayOfFieldInfo/FieldInfo[@Id='" + fieldName.ToLower() + "']";
            return XmlManage.Instance(xmlFieldList, XmlType.Content).ExistsNode(nodeName);
        }

        private static string FieldTypeName(FieldType fieldType)
        {
            Dictionary<FieldType, string> dictionary = new Dictionary<FieldType, string>();
            dictionary.Add(FieldType.TextType, "单行文本");
            dictionary.Add(FieldType.MultipleTextType, "多行文本（不支持HTML）");
            dictionary.Add(FieldType.MultipleHtmlTextType, "多行文本（支持HTML）");
            dictionary.Add(FieldType.ListBoxType, "选项");
            dictionary.Add(FieldType.NumberType, "数字");
            dictionary.Add(FieldType.MoneyType, "货币");
            dictionary.Add(FieldType.DateTimeType, "日期和时间");
            dictionary.Add(FieldType.LookType, "查阅项");
            dictionary.Add(FieldType.LinkType, "超链接");
            dictionary.Add(FieldType.BoolType, "是/否（复选框）");
            dictionary.Add(FieldType.CountType, "计算值");
            dictionary.Add(FieldType.PictureType, "图片");
            dictionary.Add(FieldType.StatusType, "状态");
            dictionary.Add(FieldType.FileType, "文件");
            dictionary.Add(FieldType.MultiplePhotoType, "多图片");
            dictionary.Add(FieldType.ColorType, "颜色代码");
            dictionary.Add(FieldType.NodeType, "节点");
            dictionary.Add(FieldType.TemplateType, "模板");
            dictionary.Add(FieldType.InfoType, "虚链接");
            dictionary.Add(FieldType.SpecialType, "专题");
            dictionary.Add(FieldType.AuthorType, "作者");
            dictionary.Add(FieldType.SourceType, "来源");
            dictionary.Add(FieldType.KeywordType, "关键字");
            dictionary.Add(FieldType.OperatingType, "运行平台");
            dictionary.Add(FieldType.SkinType, "风格");
            dictionary.Add(FieldType.Producer, "厂商");
            dictionary.Add(FieldType.Trademark, "品牌");
            dictionary.Add(FieldType.ContentType, "内容");
            dictionary.Add(FieldType.TitleType, "标题");
            dictionary.Add(FieldType.Property, "商品属性");
            dictionary.Add(FieldType.DownServerType, "下载服务器");
            return dictionary[fieldType];
        }

        public static Type GetFieldDataType(FieldType fieldtype)
        {
            switch (fieldtype)
            {
                case FieldType.TextType:
                case FieldType.MultipleTextType:
                case FieldType.MultipleHtmlTextType:
                case FieldType.ListBoxType:
                case FieldType.LookType:
                case FieldType.LinkType:
                case FieldType.CountType:
                case FieldType.PictureType:
                case FieldType.FileType:
                case FieldType.ColorType:
                case FieldType.TemplateType:
                case FieldType.AuthorType:
                case FieldType.SourceType:
                case FieldType.KeywordType:
                case FieldType.OperatingType:
                case FieldType.DownServerType:
                case FieldType.Producer:
                case FieldType.Trademark:
                case FieldType.ContentType:
                case FieldType.TitleType:
                case FieldType.MultiplePhotoType:
                    return typeof(string);

                case FieldType.NumberType:
                    return typeof(double);

                case FieldType.MoneyType:
                    return typeof(decimal);

                case FieldType.DateTimeType:
                    return typeof(DateTime);

                case FieldType.BoolType:
                    return typeof(bool);
            }
            return typeof(int);
        }

        public static FieldInfo GetFieldInfoByFieldName(int modelId, string fieldName)
        {
            string xmlField = XmlManage.Instance(GetXmlFieldList(modelId), XmlType.Content).SelectNode("/ArrayOfFieldInfo/FieldInfo[@Id=\"" + fieldName.ToLower() + "\"]");
            return ser.DeserializeField(xmlField);
        }

        public static IList<FieldInfo> GetFieldList(int modelId)
        {
            IList<FieldInfo> fieldListByModelId = ModelManager.GetFieldListByModelId(modelId);
            List<FieldInfo> list2 = new List<FieldInfo>();
            if ((fieldListByModelId != null) && (fieldListByModelId.Count > 0))
            {
                foreach (FieldInfo info in fieldListByModelId)
                {
                    if (info.FieldLevel < 2)
                    {
                        list2.Add(info);
                    }
                }
            }
            return list2;
        }

        public static IList<FieldInfo> GetFieldList(int modelId, bool disable)
        {
            List<FieldInfo> list = new List<FieldInfo>();
            foreach (FieldInfo info in GetFieldList(modelId))
            {
                if (info.Disabled == disable)
                {
                    list.Add(info);
                }
            }
            return list;
        }

        public static DataTable GetFieldNames(int modelId, FieldType[] fieldType)
        {
            DataTable table = new DataTable();
            table.Columns.Add("FieldAlias");
            table.Columns.Add("FieldName");
            foreach (FieldInfo info in ModelManager.GetFieldListByModelId(modelId))
            {
                if ((info.FieldType == fieldType[0]) || (info.FieldType == fieldType[1]))
                {
                    DataRow row = table.NewRow();
                    row["FieldAlias"] = info.FieldAlias;
                    row["FieldName"] = info.FieldName;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static string GetFieldTypeName(int fieldType)
        {
            FieldType type = (FieldType) fieldType;
            return FieldTypeName(type);
        }

        public static string GetXmlFieldList(int modelId)
        {
            return ModelManager.GetXmlFieldByModelId(modelId);
        }

        public static string SerializeField(FieldInfo fieldInfo)
        {
            return ser.SerializeField(fieldInfo);
        }

        public static string SerializeFieldList(IList<FieldInfo> fieldList)
        {
            return ser.SerializeFieldList(fieldList);
        }

        public static bool SetDisabled(string fieldName, int modelId, bool disabled)
        {
            XmlManage manage = XmlManage.Instance(GetXmlFieldList(modelId), XmlType.Content);
            string nodeName = "/ArrayOfFieldInfo/FieldInfo[@Id=\"" + fieldName.ToLower() + "\"]";
            manage.SetAttributesValue(nodeName, "Disabled", disabled.ToString().ToLower());
            return UpdateField(modelId, manage.Xml);
        }

        public static bool SetOrderId(IList<FieldInfo> fieldInfoList, int modelId)
        {
            XmlManage manage = XmlManage.Instance(GetXmlFieldList(modelId), XmlType.Content);
            foreach (FieldInfo info in fieldInfoList)
            {
                string nodeName = "/ArrayOfFieldInfo/FieldInfo[@Id=\"" + info.Id.ToLower() + "\"]";
                manage.SetAttributesValue(nodeName, "OrderId", info.OrderId.ToString());
            }
            return UpdateField(modelId, manage.Xml);
        }

        public static bool SetOrderId(string fieldName, int modelId, int orderId)
        {
            XmlManage manage = XmlManage.Instance(GetXmlFieldList(modelId), XmlType.Content);
            string nodeName = "/ArrayOfFieldInfo/FieldInfo[@Id=\"" + fieldName.ToLower() + "\"]";
            manage.SetAttributesValue(nodeName, "OrderId", orderId.ToString());
            return UpdateField(modelId, manage.Xml);
        }

        public static DataActionState Update(FieldInfo fieldInfo, int modelId)
        {
            DataActionState unknown = DataActionState.Unknown;
            if (fieldInfo.FieldType == FieldType.FileType)
            {
                FieldInfo fieldInfoByFieldName = GetFieldInfoByFieldName(modelId, fieldInfo.FieldName);
                if ((DataConverter.CBoolean(fieldInfoByFieldName.Settings[3]) && !DataConverter.CBoolean(fieldInfo.Settings[3])) && !string.IsNullOrEmpty(fieldInfo.Settings[4]))
                {
                    Delete(fieldInfo.Settings[4], modelId);
                }
                if ((!DataConverter.CBoolean(fieldInfoByFieldName.Settings[3]) && DataConverter.CBoolean(fieldInfo.Settings[3])) && !string.IsNullOrEmpty(fieldInfo.Settings[4]))
                {
                    FieldInfo info2 = new FieldInfo();
                    info2.Id = fieldInfo.Settings[4];
                    info2.FieldName = fieldInfo.Settings[4];
                    info2.FieldType = FieldType.TextType;
                    info2.FieldLevel = 2;
                    info2.OrderId = 0;
                    DataActionState state2 = Add(info2, modelId);
                    if (state2 != DataActionState.Successed)
                    {
                        return state2;
                    }
                }
            }
            if (ModelManager.UpdateFieldOfTable(fieldInfo, modelId))
            {
                unknown = DataActionState.Successed;
            }
            return unknown;
        }

        private static bool UpdateField(int modelId, string xmlFieldList)
        {
            return ModelManager.UpdateField(modelId, xmlFieldList);
        }
    }
}


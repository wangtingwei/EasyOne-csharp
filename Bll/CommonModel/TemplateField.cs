namespace EasyOne.CommonModel
{
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    public sealed class TemplateField
    {
        private TemplateField()
        {
        }

        public static bool Delete(string fieldName, int templateId)
        {
            if (GetFieldInfoByFieldName(templateId, fieldName).FieldLevel == 0)
            {
                return false;
            }
            XmlManage manage = XmlManage.Instance(ModelTemplate.GetField(templateId), XmlType.Content);
            string nodeName = "/ArrayOfFieldInfo/FieldInfo[@Id=\"" + fieldName.ToLower() + "\"]";
            manage.Remove(nodeName);
            return ModelTemplate.UpdateField(templateId, manage.Xml);
        }

        public static FieldInfo GetFieldInfoByFieldName(int templateId, string fieldName)
        {
            string str2 = XmlManage.Instance(ModelTemplate.GetField(templateId), XmlType.Content).SelectNode("/ArrayOfFieldInfo/FieldInfo[@Id=\"" + fieldName.ToLower() + "\"]");
            FieldInfo info = null;
            if (!string.IsNullOrEmpty(str2))
            {
                TextReader textReader = new StringReader(str2);
                XmlSerializer serializer = new XmlSerializer(typeof(FieldInfo));
                info = (FieldInfo) serializer.Deserialize(textReader);
                textReader.Close();
            }
            return info;
        }

        public static IList<FieldInfo> GetFieldList(int templateId)
        {
            return ModelTemplate.GetFieldListByTemplateId(templateId);
        }

        public static IList<FieldInfo> GetFieldList(int templateId, bool disable)
        {
            List<FieldInfo> list = new List<FieldInfo>();
            foreach (FieldInfo info in GetFieldList(templateId))
            {
                if (info.Disabled == disable)
                {
                    list.Add(info);
                }
            }
            return list;
        }
    }
}


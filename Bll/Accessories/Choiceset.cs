namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.UI.WebControls;
    using EasyOne.DalFactory;

    public sealed class Choiceset
    {
        private static readonly IChoiceset dal = DataAccess.CreateChoiceset();

        private Choiceset()
        {
        }

        public static void DropDownListDataBind(string tableName, string fieldName, ListControl drop)
        {
            DropDownListDataBind(tableName, fieldName, drop, -1);
        }

        public static void DropDownListDataBind(string tableName, string fieldName, ListControl drop, int dataValueField)
        {
            ChoicesetValueInfo item = new ChoicesetValueInfo();
            item.DataTextField = "请选择";
            item.DataValueField = -1;
            item.IsDefault = false;
            ChoicesetValueInfoCollection dictionaryFieldValueByName = GetDictionaryFieldValueByName(tableName, fieldName);
            dictionaryFieldValueByName.Insert(0, item);
            drop.DataSource = dictionaryFieldValueByName;
            drop.DataTextField = "DataTextField";
            drop.DataValueField = "DataValueField";
            drop.DataBind();
            if (dataValueField != -1)
            {
                drop.SelectedValue = dataValueField.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                foreach (ChoicesetValueInfo info2 in dictionaryFieldValueByName)
                {
                    if (info2.IsDefault)
                    {
                        drop.SelectedValue = info2.DataValueField.ToString(CultureInfo.CurrentCulture);
                        break;
                    }
                }
            }
        }

        public static ChoicesetInfo GetChoicesetInfoByFieldAndTableName(string tableName, string fieldName)
        {
            return dal.GetChoicesetInfoByFieldAndTableName(tableName, fieldName);
        }

        public static IList<ChoicesetInfo> GetChoicesetList()
        {
            return dal.GetChoicesetList();
        }

        public static string GetDataText(string tableName, string fieldName, int dataValueField)
        {
            foreach (ChoicesetValueInfo info in GetDictionaryFieldValueByName(tableName, fieldName))
            {
                if (info.DataValueField == dataValueField)
                {
                    return info.DataTextField;
                }
            }
            return "";
        }

        public static string[] GetDataTextFields(string tableName, string fieldName)
        {
            ChoicesetValueInfoCollection dictionaryFieldValueByName = GetDictionaryFieldValueByName(tableName, fieldName);
            string[] strArray = new string[dictionaryFieldValueByName.Count];
            for (int i = 0; i < dictionaryFieldValueByName.Count; i++)
            {
                strArray[i] = dictionaryFieldValueByName[i].DataTextField;
            }
            return strArray;
        }

        public static ChoicesetValueInfoCollection GetDictionaryFieldValue(ChoicesetInfo choicesetInfo)
        {
            return GetDictionaryFieldValue(choicesetInfo, false);
        }

        public static ChoicesetValueInfoCollection GetDictionaryFieldValue(ChoicesetInfo choicesetInfo, bool isShowEnable)
        {
            ChoicesetValueInfoCollection infos = new ChoicesetValueInfoCollection();
            if (!string.IsNullOrEmpty(choicesetInfo.FieldValue))
            {
                string[] strArray = choicesetInfo.FieldValue.Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    string[] strArray2 = strArray[i].Split(new char[] { '|' });
                    if (strArray2.Length == 3)
                    {
                        ChoicesetValueInfo item = new ChoicesetValueInfo();
                        item.DataTextField = strArray2[0];
                        item.IsEnable = DataConverter.CBoolean(strArray2[1]);
                        item.IsDefault = DataConverter.CBoolean(strArray2[2]);
                        item.DataValueField = i;
                        if (isShowEnable)
                        {
                            if (item.IsEnable)
                            {
                                infos.Add(item);
                            }
                        }
                        else
                        {
                            infos.Add(item);
                        }
                    }
                }
            }
            return infos;
        }

        public static ChoicesetValueInfoCollection GetDictionaryFieldValueByName(string tableName, string fieldName)
        {
            return GetDictionaryFieldValue(GetChoicesetInfoByFieldAndTableName(tableName, fieldName), true);
        }

        public static bool SetFieldValue(string fieldValue, string tableName, string fieldName)
        {
            return dal.SetFieldValue(fieldValue, tableName, fieldName);
        }
    }
}


namespace EasyOne.SqlServerDal.Accessories
{
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Choiceset : IChoiceset
    {
        private static ChoicesetInfo ChoicesetInfoFromrdr(NullableDataReader rdr)
        {
            ChoicesetInfo info = new ChoicesetInfo();
            info.FieldId = rdr.GetInt32("FieldID");
            info.Title = rdr.GetString("Title");
            info.TableName = rdr.GetString("TableName");
            info.FieldName = rdr.GetString("FieldName");
            info.FieldValue = rdr.GetString("FieldValue");
            return info;
        }

        public ChoicesetInfo GetChoicesetInfoByFieldAndTableName(string tableName, string fieldName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_Choiceset_GetChoicesetFieldValueByName", cmdParams))
            {
                if (reader.Read())
                {
                    return ChoicesetInfoFromrdr(reader);
                }
                return new ChoicesetInfo(true);
            }
        }

        public IList<ChoicesetInfo> GetChoicesetList()
        {
            IList<ChoicesetInfo> list = new List<ChoicesetInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_Choiceset_GetList"))
            {
                while (reader.Read())
                {
                    list.Add(ChoicesetInfoFromrdr(reader));
                }
            }
            return list;
        }

        public bool SetFieldValue(string fieldValue, string tableName, string fieldName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FieldValue", DbType.String, fieldValue);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            return DBHelper.ExecuteProc("PR_Accessories_Choiceset_SetFieldValue", cmdParams);
        }
    }
}


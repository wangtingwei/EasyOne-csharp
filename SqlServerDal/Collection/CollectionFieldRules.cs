namespace EasyOne.SqlServerDal.Collection
{
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class CollectionFieldRules : ICollectionFieldRules
    {
        public bool Add(EasyOne.Model.Collection.CollectionFieldRuleInfo collectionFieldRuleInfo)
        {
            string strSql = "INSERT INTO PE_CollectionFieldRules(FieldRuleId, ItemId, FieldName, FieldType, RuleType, BeginCode, EndCode, PrivateFilter, FilterRuleId, UsePaging, SpecialSetting, ExclosionID) VALUES (@FieldRuleId, @ItemId, @FieldName, @FieldType, @RuleType, @BeginCode, @EndCode, @PrivateFilter, @FilterRuleId, @UsePaging, @SpecialSetting, @ExclosionID)";
            collectionFieldRuleInfo.FieldRuleId = GetMaxId() + 1;
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionFieldRuleInfo));
        }

        private static EasyOne.Model.Collection.CollectionFieldRuleInfo CollectionFieldRuleInfo(NullableDataReader rdr)
        {
            EasyOne.Model.Collection.CollectionFieldRuleInfo info = new EasyOne.Model.Collection.CollectionFieldRuleInfo();
            info.FieldRuleId = rdr.GetInt32("FieldRuleId");
            info.ItemId = rdr.GetInt32("ItemId");
            info.FieldName = rdr.GetString("FieldName");
            info.FieldType = rdr.GetString("FieldType");
            info.RuleType = rdr.GetInt32("RuleType");
            info.BeginCode = rdr.GetString("BeginCode");
            info.EndCode = rdr.GetString("EndCode");
            info.PrivateFilter = rdr.GetString("PrivateFilter");
            info.FilterRuleId = rdr.GetString("FilterRuleId");
            info.UsePaging = rdr.GetBoolean("UsePaging");
            info.SpecialSetting = rdr.GetString("SpecialSetting");
            info.ExclosionId = rdr.GetInt32("ExclosionID");
            return info;
        }

        public bool Delete(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FieldRuleId", DbType.Int32, id);
            string strSql = "DELETE FROM PE_CollectionFieldRules WHERE FieldRuleId = @FieldRuleId";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteFieldName(int itemId, string fieldName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, itemId);
            cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            string strSql = "DELETE FROM PE_CollectionFieldRules WHERE ItemId = @ItemId AND FieldName = @FieldName";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteItem(int itemId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, itemId);
            string strSql = "DELETE FROM PE_CollectionFieldRules WHERE ItemId = @ItemId";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Exists(int itemId, string fieldName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, itemId);
            cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_CollectionFieldRules WHERE @ItemId = ItemId AND FieldName = @FieldName", cmdParams);
        }

        public EasyOne.Model.Collection.CollectionFieldRuleInfo GetInfoById(int itemId, string fieldName)
        {
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_CollectionFieldRules WHERE ItemId = @ItemId AND FieldName = @FieldName";
            cmdParams.AddInParameter("@ItemId", DbType.Int32, itemId);
            cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    return CollectionFieldRuleInfo(reader);
                }
                return new EasyOne.Model.Collection.CollectionFieldRuleInfo(true);
            }
        }

        public IList<EasyOne.Model.Collection.CollectionFieldRuleInfo> GetList(int itemId)
        {
            string strCommand = "SELECT * FROM PE_CollectionFieldRules WHERE ItemID = @ItemID";
            IList<EasyOne.Model.Collection.CollectionFieldRuleInfo> list = new List<EasyOne.Model.Collection.CollectionFieldRuleInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemID", DbType.Int32, itemId);
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(CollectionFieldRuleInfo(reader));
                }
            }
            return list;
        }

        private static int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_CollectionFieldRules", "FieldRuleId");
        }

        private static Parameters GetParameters(EasyOne.Model.Collection.CollectionFieldRuleInfo collectionFieldRuleInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@FieldRuleId", DbType.Int32, collectionFieldRuleInfo.FieldRuleId);
            parameters.AddInParameter("@ItemId", DbType.Int32, collectionFieldRuleInfo.ItemId);
            parameters.AddInParameter("@FieldName", DbType.String, collectionFieldRuleInfo.FieldName);
            parameters.AddInParameter("@FieldType", DbType.String, collectionFieldRuleInfo.FieldType);
            parameters.AddInParameter("@RuleType", DbType.Int32, collectionFieldRuleInfo.RuleType);
            parameters.AddInParameter("@BeginCode", DbType.String, collectionFieldRuleInfo.BeginCode);
            parameters.AddInParameter("@EndCode", DbType.String, collectionFieldRuleInfo.EndCode);
            parameters.AddInParameter("@PrivateFilter", DbType.String, collectionFieldRuleInfo.PrivateFilter);
            parameters.AddInParameter("@FilterRuleId", DbType.String, collectionFieldRuleInfo.FilterRuleId);
            parameters.AddInParameter("@UsePaging", DbType.Boolean, collectionFieldRuleInfo.UsePaging);
            parameters.AddInParameter("@SpecialSetting", DbType.String, collectionFieldRuleInfo.SpecialSetting);
            parameters.AddInParameter("@ExclosionID", DbType.Int32, collectionFieldRuleInfo.ExclosionId);
            return parameters;
        }

        public bool Update(EasyOne.Model.Collection.CollectionFieldRuleInfo collectionFieldRuleInfo)
        {
            string strSql = "UPDATE PE_CollectionFieldRules SET ItemId = @ItemId, FieldName = @FieldName, FieldType = @FieldType, RuleType = @RuleType, BeginCode = @BeginCode, EndCode = @EndCode, PrivateFilter = @PrivateFilter, FilterRuleId = @FilterRuleId, UsePaging = @UsePaging, SpecialSetting = @SpecialSetting, ExclosionID = @ExclosionID WHERE ItemId = @ItemId AND FieldName = @FieldName";
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionFieldRuleInfo));
        }

        public bool UpdateExclosionId(int exclosionId)
        {
            string strSql = "UPDATE PE_CollectionFieldRules SET ExclosionID = 0 WHERE ExclosionId = @ExclosionId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ExclosionID", DbType.Int32, exclosionId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }
    }
}


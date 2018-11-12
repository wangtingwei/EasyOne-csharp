namespace EasyOne.SqlServerDal.Collection
{
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class CollectionPagingRules : ICollectionPagingRules
    {
        public bool Add(EasyOne.Model.Collection.CollectionPagingRuleInfo collectionPagingRuleInfo)
        {
            string strSql = "INSERT INTO PE_CollectionPagingRules (PagingRuleId, ItemId, RuleType, CorrelationRuleId, PagingType, PagingBeginCode, PagingEndCode, LinkBeginCode, LinkEndCode, DesignatedUrl, ScopeBegin, ScopeEnd, PagingUrlList) VALUES (@PagingRuleId, @ItemId, @RuleType, @CorrelationRuleId, @PagingType, @PagingBeginCode, @PagingEndCode, @LinkBeginCode, @LinkEndCode, @DesignatedUrl, @ScopeBegin, @ScopeEnd, @PagingUrlList)";
            collectionPagingRuleInfo.PagingRuleId = GetMaxId() + 1;
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionPagingRuleInfo));
        }

        private static EasyOne.Model.Collection.CollectionPagingRuleInfo CollectionPagingRuleInfo(NullableDataReader rdr)
        {
            EasyOne.Model.Collection.CollectionPagingRuleInfo info = new EasyOne.Model.Collection.CollectionPagingRuleInfo();
            info.PagingRuleId = rdr.GetInt32("PagingRuleId");
            info.ItemId = rdr.GetInt32("ItemId");
            info.RuleType = rdr.GetInt32("RuleType");
            info.CorrelationRuleId = rdr.GetInt32("CorrelationRuleId");
            info.PagingType = rdr.GetInt32("PagingType");
            info.PagingBeginCode = rdr.GetString("PagingBeginCode");
            info.PagingEndCode = rdr.GetString("PagingEndCode");
            info.LinkBeginCode = rdr.GetString("LinkBeginCode");
            info.LinkEndCode = rdr.GetString("LinkEndCode");
            info.DesignatedUrl = rdr.GetString("DesignatedUrl");
            info.ScopeBegin = rdr.GetInt32("ScopeBegin");
            info.ScopeEnd = rdr.GetInt32("ScopeEnd");
            info.PagingUrlList = rdr.GetString("PagingUrlList");
            return info;
        }

        public bool Delete(int id, int ruleType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, id);
            cmdParams.AddInParameter("@RuleType", DbType.Int32, ruleType);
            string strSql = "DELETE FROM PE_CollectionPagingRules WHERE ItemId = @ItemId AND RuleType = @RuleType";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Exists(int id, int ruleType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, id);
            cmdParams.AddInParameter("@RuleType", DbType.Int32, ruleType);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_CollectionPagingRules WHERE @ItemId = ItemId AND RuleType = @RuleType", cmdParams);
        }

        public IList<EasyOne.Model.Collection.CollectionPagingRuleInfo> GetCollectionPagingRuleList(int itemId)
        {
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_CollectionPagingRules WHERE ItemId = @ItemId";
            cmdParams.AddInParameter("@ItemId", DbType.Int32, itemId);
            IList<EasyOne.Model.Collection.CollectionPagingRuleInfo> list = new List<EasyOne.Model.Collection.CollectionPagingRuleInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    list.Add(CollectionPagingRuleInfo(reader));
                }
            }
            return list;
        }

        public EasyOne.Model.Collection.CollectionPagingRuleInfo GetInfoById(int id, int ruleType)
        {
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_CollectionPagingRules WHERE ItemId = @ItemId AND RuleType = @RuleType";
            cmdParams.AddInParameter("@ItemId", DbType.Int32, id);
            cmdParams.AddInParameter("@RuleType", DbType.Int32, ruleType);
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    return CollectionPagingRuleInfo(reader);
                }
                return new EasyOne.Model.Collection.CollectionPagingRuleInfo(true);
            }
        }

        private static int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_CollectionPagingRules ", "PagingRuleId");
        }

        private static Parameters GetParameters(EasyOne.Model.Collection.CollectionPagingRuleInfo collectionPagingRuleInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@PagingRuleId", DbType.Int32, collectionPagingRuleInfo.PagingRuleId);
            parameters.AddInParameter("@ItemId", DbType.Int32, collectionPagingRuleInfo.ItemId);
            parameters.AddInParameter("@RuleType", DbType.Int32, collectionPagingRuleInfo.RuleType);
            parameters.AddInParameter("@CorrelationRuleId", DbType.Int32, collectionPagingRuleInfo.CorrelationRuleId);
            parameters.AddInParameter("@PagingType", DbType.Int32, collectionPagingRuleInfo.PagingType);
            parameters.AddInParameter("@PagingBeginCode", DbType.String, collectionPagingRuleInfo.PagingBeginCode);
            parameters.AddInParameter("@PagingEndCode", DbType.String, collectionPagingRuleInfo.PagingEndCode);
            parameters.AddInParameter("@LinkBeginCode", DbType.String, collectionPagingRuleInfo.LinkBeginCode);
            parameters.AddInParameter("@LinkEndCode", DbType.String, collectionPagingRuleInfo.LinkEndCode);
            parameters.AddInParameter("@DesignatedUrl", DbType.String, collectionPagingRuleInfo.DesignatedUrl);
            parameters.AddInParameter("@ScopeBegin", DbType.Int32, collectionPagingRuleInfo.ScopeBegin);
            parameters.AddInParameter("@ScopeEnd", DbType.Int32, collectionPagingRuleInfo.ScopeEnd);
            parameters.AddInParameter("@PagingUrlList", DbType.String, collectionPagingRuleInfo.PagingUrlList);
            return parameters;
        }

        public bool Update(EasyOne.Model.Collection.CollectionPagingRuleInfo collectionPagingRuleInfo)
        {
            string strSql = "UPDATE PE_CollectionPagingRules SET ItemId = @ItemId, RuleType = @RuleType, PagingType = @PagingType, PagingBeginCode = @PagingBeginCode, PagingEndCode = @PagingEndCode, LinkBeginCode = @LinkBeginCode, LinkEndCode = @LinkEndCode, DesignatedUrl = @DesignatedUrl, ScopeBegin = @ScopeBegin, ScopeEnd = @ScopeEnd, PagingUrlList = @PagingUrlList WHERE ItemId = @ItemId AND RuleType = @RuleType";
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionPagingRuleInfo));
        }
    }
}


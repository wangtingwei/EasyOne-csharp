namespace EasyOne.SqlServerDal.Collection
{
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class CollectionListRules : ICollectionListRules
    {
        public bool Add(EasyOne.Model.Collection.CollectionListRuleInfo collectionListRuleInfo)
        {
            string strSql = "INSERT INTO PE_CollectionListRules (ItemId, ListBeginCode, ListEndCode, LinkBeginCode, LinkEndCode, IsLinkSpecialSolution, RedirectUrl, UsePaging) VALUES (@ItemId, @ListBeginCode, @ListEndCode, @LinkBeginCode, @LinkEndCode, @IsLinkSpecialSolution, @RedirectUrl, @UsePaging)";
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionListRuleInfo));
        }

        private static EasyOne.Model.Collection.CollectionListRuleInfo CollectionListRuleInfo(NullableDataReader rdr)
        {
            EasyOne.Model.Collection.CollectionListRuleInfo info = new EasyOne.Model.Collection.CollectionListRuleInfo();
            info.ItemId = rdr.GetInt32("ItemId");
            info.ListBeginCode = rdr.GetString("ListBeginCode");
            info.ListEndCode = rdr.GetString("ListEndCode");
            info.LinkBeginCode = rdr.GetString("LinkBeginCode");
            info.LinkEndCode = rdr.GetString("LinkEndCode");
            info.IsLinkSpecialSolution = rdr.GetBoolean("IsLinkSpecialSolution");
            info.RedirectUrl = rdr.GetString("RedirectUrl");
            info.UsePaging = rdr.GetBoolean("UsePaging");
            return info;
        }

        public bool Delete(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, id);
            string strSql = "DELETE FROM PE_CollectionListRules WHERE ItemId = @ItemId";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Exists(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, id);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_CollectionListRules WHERE @ItemId = ItemId", cmdParams);
        }

        public EasyOne.Model.Collection.CollectionListRuleInfo GetInfoById(int id)
        {
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_CollectionListRules WHERE ItemId = @ItemId";
            cmdParams.AddInParameter("@ItemId", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    return CollectionListRuleInfo(reader);
                }
                return new EasyOne.Model.Collection.CollectionListRuleInfo(true);
            }
        }

        private static Parameters GetParameters(EasyOne.Model.Collection.CollectionListRuleInfo collectionListRuleInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ItemId", DbType.Int32, collectionListRuleInfo.ItemId);
            parameters.AddInParameter("@ListBeginCode", DbType.String, collectionListRuleInfo.ListBeginCode);
            parameters.AddInParameter("@ListEndCode", DbType.String, collectionListRuleInfo.ListEndCode);
            parameters.AddInParameter("@LinkBeginCode", DbType.String, collectionListRuleInfo.LinkBeginCode);
            parameters.AddInParameter("@LinkEndCode", DbType.String, collectionListRuleInfo.LinkEndCode);
            parameters.AddInParameter("@IsLinkSpecialSolution", DbType.Boolean, collectionListRuleInfo.IsLinkSpecialSolution);
            parameters.AddInParameter("@RedirectUrl", DbType.String, collectionListRuleInfo.RedirectUrl);
            parameters.AddInParameter("@UsePaging", DbType.Boolean, collectionListRuleInfo.UsePaging);
            return parameters;
        }

        public bool Update(EasyOne.Model.Collection.CollectionListRuleInfo collectionListRuleInfo)
        {
            string strSql = "UPDATE PE_CollectionListRules SET ItemId = @ItemId, ListBeginCode = @ListBeginCode, ListEndCode = @ListEndCode, LinkBeginCode = @LinkBeginCode, LinkEndCode = @LinkEndCode, IsLinkSpecialSolution = @IsLinkSpecialSolution, RedirectUrl = @RedirectUrl, UsePaging = @UsePaging WHERE ItemId = @ItemId";
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionListRuleInfo));
        }
    }
}


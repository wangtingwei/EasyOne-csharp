namespace EasyOne.SqlServerDal.Collection
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class CollectionFilterRules : ICollectionFilterRules
    {
        private int m_CountNumber;

        public bool Add(EasyOne.Model.Collection.CollectionFilterRuleInfo collectionFilterRuleInfo)
        {
            string strSql = "INSERT INTO PE_CollectionFilterRules(FilterRuleId, FilterName, FilterType, BeginCode, EndCode, Replace) VALUES (@FilterRuleId, @FilterName, @FilterType, @BeginCode, @EndCode, @Replace)";
            collectionFilterRuleInfo.FilterRuleId = GetMaxId() + 1;
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionFilterRuleInfo));
        }

        private static EasyOne.Model.Collection.CollectionFilterRuleInfo CollectionFilterRuleInfo(NullableDataReader rdr)
        {
            EasyOne.Model.Collection.CollectionFilterRuleInfo info = new EasyOne.Model.Collection.CollectionFilterRuleInfo();
            info.FilterRuleId = rdr.GetInt32("FilterRuleId");
            info.FilterName = rdr.GetString("FilterName");
            info.FilterType = rdr.GetInt32("FilterType");
            info.BeginCode = rdr.GetString("BeginCode");
            info.EndCode = rdr.GetString("EndCode");
            info.Replace = rdr.GetString("Replace");
            return info;
        }

        public bool Delete(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FilterRuleId", DbType.Int32, id);
            string strSql = "DELETE FROM PE_CollectionFilterRules WHERE FilterRuleId = @FilterRuleId";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(string id)
        {
            Parameters cmdParams = new Parameters();
            return DBHelper.ExecuteSql("DELETE FROM PE_CollectionFilterRules WHERE FilterRuleId IN (" + DBHelper.ToValidId(id) + ")", cmdParams);
        }

        public bool Exists(string filterName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FilterName", DbType.String, filterName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_CollectionFilterRules WHERE FilterName = @FilterName", cmdParams);
        }

        public int GetCountNumber()
        {
            return this.m_CountNumber;
        }

        public EasyOne.Model.Collection.CollectionFilterRuleInfo GetInfoById(int id)
        {
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_CollectionFilterRules WHERE FilterRuleId = @FilterRuleId";
            cmdParams.AddInParameter("@FilterRuleId", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    return CollectionFilterRuleInfo(reader);
                }
                return new EasyOne.Model.Collection.CollectionFilterRuleInfo(true);
            }
        }

        public IList<EasyOne.Model.Collection.CollectionFilterRuleInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<EasyOne.Model.Collection.CollectionFilterRuleInfo> list = new List<EasyOne.Model.Collection.CollectionFilterRuleInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "FilterRuleId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CollectionFilterRules");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(CollectionFilterRuleInfo(reader));
                }
            }
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_CollectionFilterRules", "FilterRuleId");
        }

        private static Parameters GetParameters(EasyOne.Model.Collection.CollectionFilterRuleInfo collectionFilterRuleInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@FilterRuleId", DbType.Int32, collectionFilterRuleInfo.FilterRuleId);
            parameters.AddInParameter("@FilterName", DbType.String, collectionFilterRuleInfo.FilterName);
            parameters.AddInParameter("@FilterType", DbType.Int32, collectionFilterRuleInfo.FilterType);
            parameters.AddInParameter("@BeginCode", DbType.String, collectionFilterRuleInfo.BeginCode);
            parameters.AddInParameter("@EndCode", DbType.String, collectionFilterRuleInfo.EndCode);
            parameters.AddInParameter("@Replace", DbType.String, collectionFilterRuleInfo.Replace);
            return parameters;
        }

        public bool Update(EasyOne.Model.Collection.CollectionFilterRuleInfo collectionFilterRuleInfo)
        {
            string strSql = "UPDATE PE_CollectionFilterRules SET FilterRuleId = @FilterRuleId, FilterName = @FilterName, FilterType = @FilterType, BeginCode = @BeginCode, EndCode = @EndCode, Replace = @Replace WHERE FilterRuleId = @FilterRuleId";
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionFilterRuleInfo));
        }
    }
}


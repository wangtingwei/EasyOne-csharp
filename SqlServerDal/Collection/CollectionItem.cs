namespace EasyOne.SqlServerDal.Collection
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class CollectionItem : ICollectionItem
    {
        private int m_CountNumber;

        public bool Add(EasyOne.Model.Collection.CollectionItemInfo collectionItemInfo)
        {
            string strSql = "INSERT INTO PE_CollectionItem (ItemId, ItemName, UrlName, CodeType, Url, Intro, NodeId, InfoNodeId, ModelId, SpecialId, OrderType, MaxNum, Detection, AutoCreateHtml) VALUES (@ItemId, @ItemName, @UrlName, @CodeType, @Url, @Intro, @NodeId, @InfoNodeId, @ModelId, @SpecialId, @OrderType, @MaxNum, @Detection, @AutoCreateHtml)";
            collectionItemInfo.ItemId = this.GetMaxId() + 1;
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionItemInfo));
        }

        private static EasyOne.Model.Collection.CollectionItemInfo CollectionItemInfo(NullableDataReader rdr)
        {
            EasyOne.Model.Collection.CollectionItemInfo info = new EasyOne.Model.Collection.CollectionItemInfo();
            info.ItemId = rdr.GetInt32("ItemId");
            info.ItemName = rdr.GetString("ItemName");
            info.UrlName = rdr.GetString("UrlName");
            info.CodeType = rdr.GetString("CodeType");
            info.Url = rdr.GetString("Url");
            info.Intro = rdr.GetString("Intro");
            info.NodeId = rdr.GetInt32("NodeId");
            info.InfoNodeId = rdr.GetString("InfoNodeId");
            info.ModelId = rdr.GetInt32("ModelId");
            info.SpecialId = rdr.GetString("SpecialId");
            info.OrderType = rdr.GetInt32("OrderType");
            info.MaxNum = rdr.GetInt32("MaxNum");
            info.NewsCollecDate = rdr.GetDateTime("NewsCollecDate");
            info.AutoCreateHtml = rdr.GetBoolean("AutoCreateHtml");
            info.Detection = rdr.GetBoolean("Detection");
            return info;
        }

        public bool Delete(int id)
        {
            bool flag = false;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, id);
            string strSql = "DELETE FROM PE_CollectionItem WHERE ItemId = @ItemId";
            flag = DBHelper.ExecuteSql(strSql, cmdParams);
            if (flag)
            {
                DBHelper.ExecuteSql("DELETE FROM PE_CollectionFieldRules WHERE ItemId = @ItemId", cmdParams);
                DBHelper.ExecuteSql("DELETE FROM PE_CollectionHistory WHERE ItemId = @ItemId", cmdParams);
                DBHelper.ExecuteSql("DELETE FROM PE_CollectionListRules WHERE ItemId = @ItemId", cmdParams);
                DBHelper.ExecuteSql("DELETE FROM PE_CollectionPagingRules WHERE ItemId = @ItemId", cmdParams);
            }
            return flag;
        }

        public bool Disabled(int itemId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, itemId);
            return DBHelper.ExistsSql("UPDATE PE_CollectionItem SET Detection = 0 where ItemId=@ItemId", cmdParams);
        }

        public bool Exists(string itemName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemName", DbType.String, itemName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_CollectionItem WHERE ItemName = @ItemName", cmdParams);
        }

        public bool ExistsCreateHtml(string itemIds)
        {
            Parameters cmdParams = new Parameters();
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_CollectionItem WHERE ItemId in (" + DataSecurity.FilterBadChar(itemIds) + ") AND AutoCreateHtml = 1", cmdParams);
        }

        public IList<EasyOne.Model.Collection.CollectionItemInfo> GetCollectionList(string itemId)
        {
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_CollectionItem WHERE ItemId IN ( " + DBHelper.ToValidId(itemId) + ")";
            IList<EasyOne.Model.Collection.CollectionItemInfo> list = new List<EasyOne.Model.Collection.CollectionItemInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    list.Add(CollectionItemInfo(reader));
                }
            }
            return list;
        }

        public DataTable GetCollectionList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "I.ItemId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "I.*, M.ModelName, N.NodeName");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "((PE_CollectionItem I left JOIN PE_Nodes N ON I.NodeID = N.NodeID) left JOIN PE_Model M ON I.ModelID =M.ModelID)");
            database.SetParameterValue(storedProcCommand, "@Filter", " I.Detection = 1");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            DataSet set = database.ExecuteDataSet(storedProcCommand);
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return set.Tables[0];
        }

        public int GetCountNumber()
        {
            return this.m_CountNumber;
        }

        public EasyOne.Model.Collection.CollectionItemInfo GetInfoById(int id)
        {
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_CollectionItem WHERE ItemId = @ItemId";
            cmdParams.AddInParameter("@ItemId", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    return CollectionItemInfo(reader);
                }
                return new EasyOne.Model.Collection.CollectionItemInfo(true);
            }
        }

        public DataTable GetList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "I.ItemId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "I.*, M.ModelName, N.NodeName");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "((PE_CollectionItem I left JOIN PE_Nodes N ON I.NodeID = N.NodeID) left JOIN PE_Model M ON I.ModelID =M.ModelID)");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            DataSet set = database.ExecuteDataSet(storedProcCommand);
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return set.Tables[0];
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_CollectionItem ", "ItemId");
        }

        private static Parameters GetParameters(EasyOne.Model.Collection.CollectionItemInfo collectionItemInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ItemId", DbType.Int32, collectionItemInfo.ItemId);
            parameters.AddInParameter("@ItemName", DbType.String, collectionItemInfo.ItemName);
            parameters.AddInParameter("@UrlName", DbType.String, collectionItemInfo.UrlName);
            parameters.AddInParameter("@CodeType", DbType.String, collectionItemInfo.CodeType);
            parameters.AddInParameter("@Url", DbType.String, collectionItemInfo.Url);
            parameters.AddInParameter("@Intro", DbType.String, collectionItemInfo.Intro);
            parameters.AddInParameter("@NodeId", DbType.Int32, collectionItemInfo.NodeId);
            parameters.AddInParameter("@InfoNodeId", DbType.String, collectionItemInfo.InfoNodeId);
            parameters.AddInParameter("@ModelId", DbType.Int32, collectionItemInfo.ModelId);
            parameters.AddInParameter("SpecialId", DbType.String, collectionItemInfo.SpecialId);
            parameters.AddInParameter("@OrderType", DbType.Int32, collectionItemInfo.OrderType);
            parameters.AddInParameter("@MaxNum", DbType.Int32, collectionItemInfo.MaxNum);
            parameters.AddInParameter("@Detection", DbType.Boolean, collectionItemInfo.Detection);
            parameters.AddInParameter("@AutoCreateHtml", DbType.Boolean, collectionItemInfo.AutoCreateHtml);
            return parameters;
        }

        public bool Update(EasyOne.Model.Collection.CollectionItemInfo collectionItemInfo)
        {
            string strSql = "UPDATE PE_CollectionItem SET ItemId = @ItemId, ItemName = @ItemName, UrlName = @UrlName, CodeType = @CodeType, Url = @Url, Intro = @Intro, NodeId = @NodeId, InfoNodeId = @InfoNodeId, ModelId = @ModelId, SpecialId = @SpecialId, OrderType = @OrderType, MaxNum = @MaxNum, Detection = @Detection, AutoCreateHtml = @AutoCreateHtml WHERE ItemId = @ItemId";
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionItemInfo));
        }

        public bool UpdateCollecDate(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, id);
            string strSql = "UPDATE PE_CollectionItem SET NewsCollecDate = GETDATE() WHERE ItemId = @ItemId";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }
    }
}


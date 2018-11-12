namespace EasyOne.SqlServerDal.Collection
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class CollectionHistory : ICollectionHistory
    {
        private int m_CountNumber;

        public bool Add(CollectionHistoryInfo collectionHistoryInfo)
        {
            string strSql = "INSERT INTO PE_CollectionHistory(HistoryId, ItemId, ModelID, NodeID, GeneralID, Title, CollectionTime, Result, NewsUrl) VALUES (@HistoryID, @ItemId, @ModelID, @NodeID, @GeneralID, @Title, @CollectionTime, @Result, @NewsUrl)";
            collectionHistoryInfo.HistoryId = this.GetMaxId() + 1;
            return DBHelper.ExecuteSql(strSql, GetParameters(collectionHistoryInfo));
        }

        public bool Delete()
        {
            Parameters cmdParams = new Parameters();
            string strSql = "DELETE FROM PE_CollectionHistory";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(string id)
        {
            Parameters cmdParams = new Parameters();
            return DBHelper.ExecuteSql("DELETE FROM PE_CollectionHistory WHERE HistoryID IN (" + DBHelper.ToValidId(id) + ")", cmdParams);
        }

        public bool DeleteErr()
        {
            Parameters cmdParams = new Parameters();
            string strSql = "DELETE FROM PE_CollectionHistory WHERE Result = 0";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteSuccess()
        {
            Parameters cmdParams = new Parameters();
            string strSql = "DELETE FROM PE_CollectionHistory WHERE Result = 1";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Exists(string title)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Title", DbType.String, title);
            return DBHelper.ExistsSql("SELECT COUNT(Title) From PE_CollectionHistory WHERE Title = @Title AND Result = 1", cmdParams);
        }

        public DataTable GetCollectionHistory(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "H.HistoryID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "H.*, I.ItemName, M.ModelName, N.NodeName");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, " DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "((PE_CollectionHistory H left JOIN PE_CollectionItem I ON H.ItemId = I.ItemId) left JOIN PE_Nodes N ON H.NodeID = N.NodeID) left JOIN PE_Model M ON H.ModelID = M.ModelID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            DataSet set = database.ExecuteDataSet(storedProcCommand);
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return set.Tables[0];
        }

        public int GetCountNumber()
        {
            return this.m_CountNumber;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_CollectionHistory", "HistoryID");
        }

        private static Parameters GetParameters(CollectionHistoryInfo collectionHistoryInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@HistoryID", DbType.Int32, collectionHistoryInfo.HistoryId);
            parameters.AddInParameter("@ItemId", DbType.Int32, collectionHistoryInfo.ItemId);
            parameters.AddInParameter("@ModelID", DbType.Int32, collectionHistoryInfo.ModelId);
            parameters.AddInParameter("@NodeID", DbType.Int32, collectionHistoryInfo.NodeId);
            parameters.AddInParameter("@GeneralID", DbType.Int32, collectionHistoryInfo.GeneralId);
            parameters.AddInParameter("@Title", DbType.String, collectionHistoryInfo.Title);
            parameters.AddInParameter("@CollectionTime", DbType.DateTime, collectionHistoryInfo.CollectionTime);
            parameters.AddInParameter("@Result", DbType.Boolean, collectionHistoryInfo.Result);
            parameters.AddInParameter("@NewsUrl", DbType.String, collectionHistoryInfo.NewsUrl);
            return parameters;
        }

        public int RecordCount(bool countType, int itemId)
        {
            Parameters cmdParams = new Parameters();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) FROM PE_CollectionHistory WHERE Result = @Result ");
            if (itemId > 0)
            {
                builder.Append(" AND ItemId = @ItemId ");
                cmdParams.AddInParameter("@ItemId", DbType.Int32, itemId);
            }
            cmdParams.AddInParameter("@Result", DbType.Boolean, countType);
            return DataConverter.CLng(DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParams));
        }
    }
}


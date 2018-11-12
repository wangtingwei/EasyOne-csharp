namespace EasyOne.SqlServerDal.Accessories
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public sealed class Favorite : IFavorite
    {
        private int m_TotalOfFavorite;

        public bool Add(FavoriteInfo favoriteInfo)
        {
            string strSql = "INSERT INTO PE_Favorite (FavoriteId, UserId, InfoId, FavoriteTime) VALUES (@FavoriteId, @UserId, @InfoId, @FavoriteTime)";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FavoriteId", DbType.Int32, favoriteInfo.FavoriteId);
            cmdParams.AddInParameter("@UserId", DbType.Int32, favoriteInfo.UserId);
            cmdParams.AddInParameter("@InfoId", DbType.Int32, favoriteInfo.InfoId);
            cmdParams.AddInParameter("@FavoriteTime", DbType.DateTime, favoriteInfo.FavoriteTime);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(int userId)
        {
            string strSql = "DELETE FROM PE_Favorite WHERE UserId = @UserId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserId", DbType.Int32, userId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(int userId, int infoId)
        {
            string strSql = "DELETE FROM PE_Favorite WHERE UserId = @UserId AND InfoId = @InfoId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserId", DbType.Int32, userId);
            cmdParams.AddInParameter("@InfoId", DbType.Int32, infoId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(int userId, string infoIds)
        {
            if (string.IsNullOrEmpty(infoIds))
            {
                return true;
            }
            string strSql = "DELETE FROM PE_Favorite WHERE UserId = @UserId AND (InfoId IN (" + DBHelper.ToValidId(infoIds) + "))";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserId", DbType.Int32, userId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Exists(int userId, int infoId)
        {
            string strSql = "SELECT COUNT(*) FROM PE_Favorite WHERE UserId = @UserId AND InfoId = @InfoId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserId", DbType.Int32, userId);
            cmdParams.AddInParameter("@InfoId", DbType.Int32, infoId);
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public IList<FavoriteInfo> GetList(int startRowIndexId, int maxNumberRows, int userId, int nodeId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<FavoriteInfo> list = new List<FavoriteInfo>();
            string str = "Favorite.UserId = " + userId.ToString() + " AND C.GeneralId IS NOT NULL";
            if (nodeId > 0)
            {
                str = str + " AND C.NodeId = " + nodeId.ToString();
            }
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "FavoriteId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "Favorite.*, C.Title");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Favorite Favorite LEFT JOIN PE_CommonModel C ON Favorite.InfoId = C.GeneralId");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    FavoriteInfo item = new FavoriteInfo();
                    item.UserId = userId;
                    item.InfoId = reader.GetInt32("InfoId");
                    item.FavoriteTime = reader.GetDateTime("FavoriteTime");
                    item.FavoriteId = reader.GetInt32("FavoriteId");
                    item.Title = reader.GetString("Title");
                    list.Add(item);
                }
            }
            this.m_TotalOfFavorite = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Favorite", "FavoriteId");
        }

        public int GetTotalOfFavorite()
        {
            return this.m_TotalOfFavorite;
        }

        public int GetUserFavoiteCount(int userId)
        {
            string strSql = "SELECT COUNT(*) FROM PE_Favorite WHERE UserId = @UserId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserId", DbType.Int32, userId);
            return (int) DBHelper.ExecuteScalarSql(strSql, cmdParams);
        }
    }
}


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
    using System.Text;

    public class CollectionExclosion : ICollectionExclosion
    {
        private int m_CountNumber;

        public bool Add(EasyOne.Model.Collection.CollectionExclosionInfo collectionExclosionInfo)
        {
            string str = "";
            string str2 = "";
            switch (collectionExclosionInfo.ExclosionType)
            {
                case 1:
                    str = "ExclosionStringType, ExclosionString";
                    str2 = "@ExclosionStringType, @ExclosionString";
                    break;

                case 2:
                    str = "IsExclosionDesignatedDateTime, IsExclosionMaxDateTime, IsExclosionMinDateTime, ExclosionDesignatedDateTime, ExclosionMaxDateTime, ExclosionMinDateTime";
                    str2 = "@IsExclosionDesignatedDateTime, @IsExclosionMaxDateTime, @IsExclosionMinDateTime, @ExclosionDesignatedDateTime, @ExclosionMaxDateTime, @ExclosionMinDateTime";
                    break;

                case 3:
                    str = "IsExclosionDesignatedNumber, IsExclosionMaxNumber, IsExclosionMinNumber, ExclosionDesignatedNumber, ExclosionMaxNumber, ExclosionMinNumber";
                    str2 = "@IsExclosionDesignatedNumber, @IsExclosionMaxNumber, @IsExclosionMinNumber, @ExclosionDesignatedNumber, @ExclosionMaxNumber, @ExclosionMinNumber";
                    break;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO PE_CollectionExclosion(ExclosionID, ExclosionName, ExclosionType," + str);
            builder.Append(") VALUES (@ExclosionID, @ExclosionName, @ExclosionType," + str2 + ")");
            collectionExclosionInfo.ExclosionId = GetMaxId() + 1;
            return DBHelper.ExecuteSql(builder.ToString(), GetParameters(collectionExclosionInfo));
        }

        private static EasyOne.Model.Collection.CollectionExclosionInfo CollectionExclosionInfo(NullableDataReader rdr)
        {
            EasyOne.Model.Collection.CollectionExclosionInfo info = new EasyOne.Model.Collection.CollectionExclosionInfo();
            info.ExclosionId = rdr.GetInt32("ExclosionID");
            info.ExclosionName = rdr.GetString("ExclosionName");
            info.ExclosionType = rdr.GetInt32("ExclosionType");
            info.ExclosionStringType = rdr.GetInt32("ExclosionStringType");
            info.ExclosionString = rdr.GetString("ExclosionString");
            info.ExclosionDesignatedNumber = rdr.GetInt32("ExclosionDesignatedNumber");
            info.ExclosionMaxNumber = rdr.GetInt32("ExclosionMaxNumber");
            info.ExclosionMinNumber = rdr.GetInt32("ExclosionMinNumber");
            info.ExclosionDesignatedDateTime = new DateTime?(rdr.GetDateTime("ExclosionDesignatedDateTime"));
            info.ExclosionMaxDateTime = new DateTime?(rdr.GetDateTime("ExclosionMaxDateTime"));
            info.ExclosionMinDateTime = new DateTime?(rdr.GetDateTime("ExclosionMinDateTime"));
            info.IsExclosionDesignatedNumber = rdr.GetBoolean("IsExclosionDesignatedNumber");
            info.IsExclosionMaxNumber = rdr.GetBoolean("IsExclosionMaxNumber");
            info.IsExclosionMinNumber = rdr.GetBoolean("IsExclosionMinNumber");
            info.IsExclosionDesignatedDateTime = rdr.GetBoolean("IsExclosionDesignatedDateTime");
            info.IsExclosionMaxDateTime = rdr.GetBoolean("IsExclosionMaxDateTime");
            info.IsExclosionMinDateTime = rdr.GetBoolean("IsExclosionMinDateTime");
            return info;
        }

        public bool Delete(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ExclosionID", DbType.Int32, id);
            string strSql = "DELETE FROM PE_CollectionExclosion WHERE ExclosionID = @ExclosionID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Exists(string exclosionName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ExclosionName", DbType.String, exclosionName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_CollectionExclosion WHERE ExclosionName = @ExclosionName", cmdParams);
        }

        public int GetCountNumber()
        {
            return this.m_CountNumber;
        }

        public EasyOne.Model.Collection.CollectionExclosionInfo GetInfoById(int id)
        {
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_CollectionExclosion WHERE ExclosionID = @ExclosionID";
            cmdParams.AddInParameter("@ExclosionID", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    return CollectionExclosionInfo(reader);
                }
                return new EasyOne.Model.Collection.CollectionExclosionInfo(true);
            }
        }

        public IList<EasyOne.Model.Collection.CollectionExclosionInfo> GetList(int exclosionType)
        {
            string strCommand = "SELECT * FROM PE_CollectionExclosion WHERE ExclosionType = @ExclosionType";
            IList<EasyOne.Model.Collection.CollectionExclosionInfo> list = new List<EasyOne.Model.Collection.CollectionExclosionInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ExclosionType", DbType.Int32, exclosionType);
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(CollectionExclosionInfo(reader));
                }
            }
            return list;
        }

        public IList<EasyOne.Model.Collection.CollectionExclosionInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<EasyOne.Model.Collection.CollectionExclosionInfo> list = new List<EasyOne.Model.Collection.CollectionExclosionInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ExclosionId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CollectionExclosion");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(CollectionExclosionInfo(reader));
                }
            }
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_CollectionExclosion", "ExclosionID");
        }

        private static Parameters GetParameters(EasyOne.Model.Collection.CollectionExclosionInfo collectionExclosionInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ExclosionID", DbType.Int32, collectionExclosionInfo.ExclosionId);
            parameters.AddInParameter("@ExclosionName", DbType.String, collectionExclosionInfo.ExclosionName);
            parameters.AddInParameter("@ExclosionType", DbType.Int32, collectionExclosionInfo.ExclosionType);
            parameters.AddInParameter("@ExclosionStringType", DbType.Int32, collectionExclosionInfo.ExclosionStringType);
            parameters.AddInParameter("@ExclosionString", DbType.String, collectionExclosionInfo.ExclosionString);
            parameters.AddInParameter("@ExclosionDesignatedNumber", DbType.Int32, collectionExclosionInfo.ExclosionDesignatedNumber);
            parameters.AddInParameter("@ExclosionMaxNumber", DbType.Int32, collectionExclosionInfo.ExclosionMaxNumber);
            parameters.AddInParameter("@ExclosionMinNumber", DbType.Int32, collectionExclosionInfo.ExclosionMinNumber);
            parameters.AddInParameter("@ExclosionDesignatedDateTime", DbType.DateTime, collectionExclosionInfo.ExclosionDesignatedDateTime);
            parameters.AddInParameter("@ExclosionMaxDateTime", DbType.DateTime, collectionExclosionInfo.ExclosionMaxDateTime);
            parameters.AddInParameter("@ExclosionMinDateTime", DbType.DateTime, collectionExclosionInfo.ExclosionMinDateTime);
            parameters.AddInParameter("@IsExclosionDesignatedNumber", DbType.Boolean, collectionExclosionInfo.IsExclosionDesignatedNumber);
            parameters.AddInParameter("@IsExclosionMaxNumber", DbType.Boolean, collectionExclosionInfo.IsExclosionMaxNumber);
            parameters.AddInParameter("@IsExclosionMinNumber", DbType.Boolean, collectionExclosionInfo.IsExclosionMinNumber);
            parameters.AddInParameter("@IsExclosionDesignatedDateTime", DbType.Boolean, collectionExclosionInfo.IsExclosionDesignatedDateTime);
            parameters.AddInParameter("@IsExclosionMaxDateTime", DbType.Boolean, collectionExclosionInfo.IsExclosionMaxDateTime);
            parameters.AddInParameter("@IsExclosionMinDateTime", DbType.Boolean, collectionExclosionInfo.IsExclosionMinDateTime);
            return parameters;
        }

        public bool Update(EasyOne.Model.Collection.CollectionExclosionInfo collectionExclosionInfo)
        {
            string str = "";
            switch (collectionExclosionInfo.ExclosionType)
            {
                case 1:
                    str = "ExclosionStringType = @ExclosionStringType, ExclosionString = @ExclosionString";
                    break;

                case 2:
                    str = "IsExclosionDesignatedDateTime = @IsExclosionDesignatedDateTime, IsExclosionMaxDateTime = @IsExclosionMaxDateTime, IsExclosionMinDateTime = @IsExclosionMinDateTime, ExclosionDesignatedDateTime = @ExclosionDesignatedDateTime, ExclosionMaxDateTime = @ExclosionMaxDateTime, ExclosionMinDateTime = @ExclosionMinDateTime";
                    break;

                case 3:
                    str = "IsExclosionDesignatedNumber = @IsExclosionDesignatedNumber, IsExclosionMaxNumber = @IsExclosionMaxNumber, IsExclosionMinNumber = @IsExclosionMinNumber, ExclosionDesignatedNumber = @ExclosionDesignatedNumber, ExclosionMaxNumber = @ExclosionMaxNumber, ExclosionMinNumber = @ExclosionMinNumber";
                    break;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE PE_CollectionExclosion SET ExclosionName = @ExclosionName, ExclosionType = @ExclosionType," + str);
            builder.Append(" WHERE ExclosionID = @ExclosionID");
            return DBHelper.ExecuteSql(builder.ToString(), GetParameters(collectionExclosionInfo));
        }
    }
}


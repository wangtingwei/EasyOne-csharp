namespace EasyOne.SqlServerDal.Contents
{
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class PermissionContent : IPermissionContent
    {
        public bool Add(ContentPermissionInfo contentPermissionInfo)
        {
            Parameters parms = new Parameters();
            GetParameter(contentPermissionInfo, parms);
            string strSql = "INSERT INTO PE_ContentPermission(GeneralId, PermissionType, ArrGroupId) VALUES (@GeneralId, @PermissionType, @ArrGroupId)";
            return DBHelper.ExecuteSql(strSql, parms);
        }

        public bool BatchUpdate(ContentPermissionInfo contentPermissionInfo, string itemId, Dictionary<string, bool> checkItem)
        {
            StringBuilder builder = new StringBuilder();
            Parameters cmdParams = new Parameters();
            builder.Append("UPDATE PE_ContentPermission SET ");
            if (checkItem["InfoPurview"])
            {
                builder.Append("PermissionType = @PermissionType,");
                cmdParams.AddInParameter("@PermissionType", DbType.Int32, contentPermissionInfo.PermissionType);
                builder.Append("ArrGroupId = @ArrGroupId,");
                cmdParams.AddInParameter("@ArrGroupId", DbType.String, contentPermissionInfo.ArrGroupId);
            }
            if (builder.Length <= 0x20)
            {
                return true;
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" WHERE ");
            builder.Append(" GeneralID IN ( ");
            builder.Append(DBHelper.ToValidId(itemId));
            builder.Append(" )");
            return DBHelper.ExecuteSql(builder.ToString(), cmdParams);
        }

        private static ContentPermissionInfo ContentPermissionFromDataReader(NullableDataReader dr)
        {
            ContentPermissionInfo info = new ContentPermissionInfo();
            info.GeneralId = dr.GetInt32("GeneralId");
            info.PermissionType = dr.GetInt32("PermissionType");
            info.ArrGroupId = dr.GetString("ArrGroupId");
            return info;
        }

        public void Delete(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            DBHelper.ExecuteSql("DELETE FROM PE_ContentPermission WHERE GeneralId = @GeneralId", cmdParams);
        }

        public void Delete(string generalId)
        {
            DBHelper.ExecuteSql("DELETE FROM PE_ContentPermission WHERE GeneralId IN(" + DBHelper.ToValidId(generalId) + ")");
        }

        public bool Exists(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_ContentPermission WHERE GeneralId = @GeneralId", cmdParams);
        }

        public ContentPermissionInfo GetContentPermissionInfoById(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_ContentPermission WHERE GeneralId = @GeneralId", cmdParams))
            {
                if (reader.Read())
                {
                    return ContentPermissionFromDataReader(reader);
                }
                return new ContentPermissionInfo(true);
            }
        }

        private static void GetParameter(ContentPermissionInfo contentPermissionInfo, Parameters parms)
        {
            parms.AddInParameter("@GeneralId", DbType.Int32, contentPermissionInfo.GeneralId);
            parms.AddInParameter("@PermissionType", DbType.Int32, contentPermissionInfo.PermissionType);
            parms.AddInParameter("@ArrGroupId", DbType.String, contentPermissionInfo.ArrGroupId);
        }

        public bool Update(ContentPermissionInfo contentPermissionInfo)
        {
            Parameters parms = new Parameters();
            GetParameter(contentPermissionInfo, parms);
            return DBHelper.ExecuteSql("UPDATE PE_ContentPermission SET GeneralId = @GeneralId, PermissionType = @PermissionType, ArrGroupId = @ArrGroupId WHERE GeneralId = @GeneralId", parms);
        }
    }
}


namespace EasyOne.SqlServerDal.WorkFlows
{
    using EasyOne.IDal.WorkFlow;
    using EasyOne.Model.WorkFlow;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public sealed class Status : IStatus
    {
        public bool Add(StatusInfo statusInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StatusID", DbType.Int32, this.GetMaxId() + 1);
            cmdParams.AddInParameter("@StatusCode", DbType.Int32, statusInfo.StatusCode);
            cmdParams.AddInParameter("@StatusName", DbType.String, statusInfo.StatusName);
            cmdParams.AddInParameter("@StatusType", DbType.Int32, statusInfo.StatusType);
            string strSql = "INSERT INTO PE_Status(StatusID, StatusCode, StatusName, StatusType) VALUES (@StatusID, @StatusCode, @StatusName, @StatusType)";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(int statusId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StatusID", DbType.Int32, statusId);
            string strSql = "DELETE FROM PE_Status WHERE StatusID = @StatusID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Exists(int statusCode)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StatusCode", DbType.Int32, statusCode);
            string strSql = "SELECT StatusCode FROM PE_Status WHERE StatusCode = @StatusCode";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Status", "StatusID");
        }

        public StatusInfo GetStatusById(int statusId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StatusID", DbType.Int32, statusId);
            string strSql = "SELECT StatusID, StatusCode, StatusName, StatusType FROM PE_Status WHERE StatusID = @StatusID";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return StatusInfoFromrdataReader(reader);
                }
                return new StatusInfo(true);
            }
        }

        public IList<StatusInfo> GetStatusList()
        {
            IList<StatusInfo> list = new List<StatusInfo>();
            string strSql = "SELECT StatusID, StatusCode, StatusName, StatusType FROM PE_Status ORDER BY StatusCode DESC";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    list.Add(StatusInfoFromrdataReader(reader));
                }
            }
            return list;
        }

        public IList<StatusInfo> GetStatusList(int listType)
        {
            IList<StatusInfo> list = new List<StatusInfo>();
            string str = "SELECT StatusID, StatusCode, StatusName, StatusType FROM PE_Status";
            switch (listType)
            {
                case 1:
                    str = str + " WHERE StatusCode >= 0 AND StatusCode < 99";
                    break;

                case 2:
                    str = str + " WHERE StatusCode > 0 ";
                    break;

                case 3:
                    str = str + " WHERE (StatusCode >= 0 AND StatusCode < 99) OR StatusCode = -2 ";
                    break;
            }
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(str + "ORDER BY StatusType ASC, StatusID ASC"))
            {
                while (reader.Read())
                {
                    list.Add(StatusInfoFromrdataReader(reader));
                }
            }
            return list;
        }

        private static StatusInfo StatusInfoFromrdataReader(NullableDataReader rdr)
        {
            StatusInfo info = new StatusInfo();
            info.StatusId = rdr.GetInt32("StatusID");
            info.StatusCode = rdr.GetInt32("StatusCode");
            info.StatusName = rdr.GetString("StatusName");
            info.StatusType = rdr.GetInt32("StatusType");
            return info;
        }

        public bool Update(StatusInfo statusInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StatusID", DbType.Int32, statusInfo.StatusId);
            cmdParams.AddInParameter("@StatusCode", DbType.Int32, statusInfo.StatusCode);
            cmdParams.AddInParameter("@StatusName", DbType.String, statusInfo.StatusName);
            cmdParams.AddInParameter("@StatusType", DbType.Int32, statusInfo.StatusType);
            string strSql = "UPDATE PE_Status SET StatusCode = @StatusCode, StatusName = @StatusName, StatusType = @StatusType WHERE StatusID = @StatusID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }
    }
}


namespace EasyOne.SqlServerDal.Accessories
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class DownloadError : IDownloadError
    {
        private int m_TotalOfDownloadError;

        public bool Add(DownloadErrorInfo downloadErrorInfo)
        {
            Parameters parms = new Parameters();
            downloadErrorInfo.ErrorId = DBHelper.GetMaxId("PE_DownloadError", "ErrorID") + 1;
            GetDownloadErrorParameters(downloadErrorInfo, parms);
            return DBHelper.ExecuteProc("PR_Accessories_DownloadError_Add", parms);
        }

        public bool Clear()
        {
            return DBHelper.ExecuteProc("PR_Accessories_DownloadError_Clear");
        }

        public bool Delete(string errorId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ErrorID", DbType.String, errorId);
            cmdParams.AddInParameter("@SplitChar", DbType.String, ",");
            return DBHelper.ExecuteProc("PR_Accessories_DownloadError_Delete", cmdParams);
        }

        private static DownloadErrorInfo DownloadErrorInfoFromrdr(NullableDataReader rdr)
        {
            DownloadErrorInfo info = new DownloadErrorInfo();
            info.ErrorId = rdr.GetInt32("ErrorID");
            info.InfoId = rdr.GetInt32("InfoID");
            info.ErrorUrl = rdr.GetString("ErrorUrl");
            info.ErrorTimes = rdr.GetInt32("ErrorTimes");
            info.ErrorDate = rdr.GetDateTime("ErrorDate");
            return info;
        }

        public bool Exists(int infoId, string errorInformation)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@InfoId", DbType.Int32, infoId);
            cmdParams.AddInParameter("@ErrorUrl", DbType.String, errorInformation);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_DownloadError WHERE InfoID = @InfoId AND ErrorUrl = @ErrorUrl", cmdParams);
        }

        public IList<DownloadErrorInfo> GetDownloadErrorList(int startRowIndexId, int maxiNumRows, string searchType, string keyword)
        {
            IList<DownloadErrorInfo> list = new List<DownloadErrorInfo>();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String);
            database.SetParameterValue(storedProcCommand, "@StartRows", startRowIndexId);
            database.SetParameterValue(storedProcCommand, "@PageSize", maxiNumRows);
            database.SetParameterValue(storedProcCommand, "@SortColumn", "ErrorID");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "*");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            if (string.IsNullOrEmpty(searchType) || string.IsNullOrEmpty(keyword))
            {
                database.SetParameterValue(storedProcCommand, "@Filter", "");
            }
            else if (searchType == "ErrorDate")
            {
                database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(d, ErrorDate, GETDATE())<=" + DataConverter.CLng(keyword));
            }
            else
            {
                database.SetParameterValue(storedProcCommand, "@Filter", "InfoID IN(SELECT GeneralID FROM PE_CommonModel WHERE Status = 99 AND Title LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')");
            }
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_DownloadError");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(DownloadErrorInfoFromrdr(reader));
                }
            }
            this.m_TotalOfDownloadError = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static void GetDownloadErrorParameters(DownloadErrorInfo downloadErrorInfo, Parameters parms)
        {
            parms.AddInParameter("@ErrorID", DbType.Int32, downloadErrorInfo.ErrorId);
            parms.AddInParameter("@InfoID", DbType.Int32, downloadErrorInfo.InfoId);
            parms.AddInParameter("@ErrorUrl", DbType.String, downloadErrorInfo.ErrorUrl);
            parms.AddInParameter("@ErrorTimes", DbType.Int32, downloadErrorInfo.ErrorTimes);
            parms.AddInParameter("@ErrorDate", DbType.DateTime, downloadErrorInfo.ErrorDate);
        }

        public int GetTotalOfDownloadError()
        {
            return this.m_TotalOfDownloadError;
        }

        public bool UpdateErrorTimes(int infoId, string errorInformation)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@InfoId", DbType.Int32, infoId);
            cmdParams.AddInParameter("@ErrorUrl", DbType.String, errorInformation);
            return DBHelper.ExecuteSql("UPDATE PE_DownloadError SET ErrorTimes = ErrorTimes + 1 WHERE InfoID = @InfoId AND ErrorUrl = @ErrorUrl", cmdParams);
        }
    }
}


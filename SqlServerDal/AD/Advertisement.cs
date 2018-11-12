namespace EasyOne.SqlServerDal.AD
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.AD;
    using EasyOne.Model.AD;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class Advertisement : IAdvertisement
    {
        private int m_NumAdvertisements;

        public bool Add(AdvertisementInfo advertisementInfo)
        {
            advertisementInfo.ADId = GetMaxAdvertisementId() + 1;
            return DBHelper.ExecuteProc("PR_AD_Advertisement_Add", GetParameters(advertisementInfo));
        }

        private static AdvertisementInfo AdvertisementFromrdr(NullableDataReader rdr)
        {
            AdvertisementInfo info = new AdvertisementInfo();
            info.ADId = rdr.GetInt32("ADID");
            info.UserId = rdr.GetInt32("UserID");
            info.ADType = rdr.GetInt32("ADType");
            info.ADName = rdr.GetString("ADName");
            info.ImgUrl = rdr.GetString("ImgUrl");
            info.ImgWidth = rdr.GetInt32("ImgWidth");
            info.ImgHeight = rdr.GetInt32("ImgHeight");
            info.FlashWmode = rdr.GetInt32("FlashWmode");
            info.ADIntro = rdr.GetString("ADIntro");
            info.LinkUrl = rdr.GetString("LinkUrl");
            info.LinkTarget = rdr.GetInt32("LinkTarget");
            info.LinkAlt = rdr.GetString("LinkAlt");
            info.Priority = rdr.GetInt32("Priority");
            info.Setting = rdr.GetString("Setting");
            info.CountView = rdr.GetBoolean("CountView");
            info.Views = rdr.GetInt32("Views");
            info.CountClick = rdr.GetBoolean("CountClick");
            info.Clicks = rdr.GetInt32("Clicks");
            info.Passed = rdr.GetBoolean("Passed");
            info.OverdueDate = rdr.GetDateTime("OverdueDate");
            return info;
        }

        public bool CancelPassedAdvertisement(string id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@strAdId", DbType.String, id);
            return DBHelper.ExecuteProc("PR_AD_Advertisement_CancelPassed", cmdParams);
        }

        public bool CopyAdvertisement(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ADId", DbType.Int32, id);
            cmdParams.AddInParameter("@MaxId", DbType.Int32, GetMaxAdvertisementId() + 1);
            string strSql = "INSERT INTO PE_Advertisement \r\n                                SELECT @MaxId AS ADId, UserID, ADType, '复制'+Convert(Varchar, @MaxId)+ADName AS ADName, ImgUrl, ImgWidth, ImgHeight, FlashWmode, ADIntro, LinkUrl, LinkTarget, LinkAlt, Priority, Setting, CountView, Views, CountClick, Clicks, Passed, OverdueDate \r\n                                FROM PE_Advertisement WHERE ADId = @ADId";
            bool flag = false;
            try
            {
                if (DBHelper.ExecuteNonQuerySql(strSql, cmdParams) > 0)
                {
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public bool Delete(string id)
        {
            string strSql = "DELETE FROM PE_Advertisement WHERE ADId IN(" + DBHelper.ToValidId(id) + ")";
            DBHelper.ExecuteSql("DELETE FROM PE_Zone_Advertisement WHERE AdId IN(" + DBHelper.ToValidId(id) + ")");
            return DBHelper.ExecuteSql(strSql);
        }

        public IList<AdvertisementInfo> GetADList(int zoneId)
        {
            string strSql = "SELECT * \r\n                            FROM PE_Advertisement A INNER JOIN PE_Zone_Advertisement Z ON Z.ADID = A.ADId \r\n                            WHERE A.Passed = @Passed AND ZoneId = @ZoneID\r\n                            ORDER BY A.Priority DESC, A.ADID DESC";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Passed", DbType.Boolean, true);
            cmdParams.AddInParameter("@ZoneId", DbType.Int32, zoneId);
            IList<AdvertisementInfo> list = new List<AdvertisementInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(AdvertisementFromrdr(reader));
                }
            }
            return list;
        }

        public IList<AdvertisementInfo> GetADList(int zoneId, int type)
        {
            string strSql = "SELECT * \r\n                            FROM PE_Advertisement A INNER JOIN PE_Zone_Advertisement Z ON Z.ADID = A.ADId \r\n                            WHERE A.Passed = @Passed AND ZoneId = @ZoneID";
            if (type == 3)
            {
                strSql = strSql + " ORDER BY A.ADID DESC, A.Priority DESC";
            }
            if ((type == 2) || (type == 1))
            {
                strSql = strSql + " ORDER BY A.Priority DESC, A.ADID DESC";
            }
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Passed", DbType.Boolean, true);
            cmdParams.AddInParameter("@ZoneId", DbType.Int32, zoneId);
            IList<AdvertisementInfo> list = new List<AdvertisementInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(AdvertisementFromrdr(reader));
                }
            }
            return list;
        }

        public AdvertisementInfo GetAdvertisementById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ADId", DbType.Int32, id);
            string zondIdList = GetZondIdList(id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Advertisement WHERE AdId = @ADId", cmdParams))
            {
                if (reader.Read())
                {
                    AdvertisementInfo info = AdvertisementFromrdr(reader);
                    info.ZoneId = zondIdList;
                    return info;
                }
                return new AdvertisementInfo(true);
            }
        }

        public IList<AdvertisementInfo> GetAdvertisementList(int startRowIndexId, int maxNumberRows, int zoneId, ADSearchType listType, string keyword)
        {
            string str2;
            Database database = DatabaseFactory.CreateDatabase();
            IList<AdvertisementInfo> list = new List<AdvertisementInfo>();
            string storedProcedureName = "PR_Common_GetList";
            DbCommand storedProcCommand = database.GetStoredProcCommand(storedProcedureName);
            switch (listType)
            {
                case ADSearchType.ADName:
                    str2 = " ADName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                    break;

                case ADSearchType.Intro:
                    str2 = "ADIntro LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                    break;

                case ADSearchType.Zone:
                    str2 = " AdId IN (SELECT ADID FROM PE_Zone_Advertisement WHERE ZoneId = " + zoneId + ") ";
                    break;

                default:
                    str2 = null;
                    break;
            }
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ADId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Advertisement");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(AdvertisementFromrdr(reader));
                }
            }
            this.m_NumAdvertisements = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static int GetMaxAdvertisementId()
        {
            return DBHelper.GetMaxId("PE_Advertisement", "ADId");
        }

        private static Parameters GetParameters(AdvertisementInfo advertisementInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ADID", DbType.Int32, advertisementInfo.ADId);
            parameters.AddInParameter("@strZoneID", DbType.String, advertisementInfo.ZoneId);
            parameters.AddInParameter("@UserID", DbType.Int32, advertisementInfo.UserId);
            parameters.AddInParameter("@ADType", DbType.Int32, advertisementInfo.ADType);
            parameters.AddInParameter("@ADName", DbType.String, advertisementInfo.ADName);
            parameters.AddInParameter("@ImgUrl", DbType.String, advertisementInfo.ImgUrl);
            parameters.AddInParameter("@ImgWidth", DbType.Int32, advertisementInfo.ImgWidth);
            parameters.AddInParameter("@ImgHeight", DbType.Int32, advertisementInfo.ImgHeight);
            parameters.AddInParameter("@FlashWmode", DbType.Int32, advertisementInfo.FlashWmode);
            parameters.AddInParameter("@ADIntro", DbType.String, advertisementInfo.ADIntro);
            parameters.AddInParameter("@LinkUrl", DbType.String, advertisementInfo.LinkUrl);
            parameters.AddInParameter("@LinkTarget", DbType.Int32, advertisementInfo.LinkTarget);
            parameters.AddInParameter("@LinkAlt", DbType.String, advertisementInfo.LinkAlt);
            parameters.AddInParameter("@Priority", DbType.Int32, advertisementInfo.Priority);
            parameters.AddInParameter("@Setting", DbType.String, advertisementInfo.Setting);
            parameters.AddInParameter("@CountView", DbType.Boolean, advertisementInfo.CountView);
            parameters.AddInParameter("@Views", DbType.Int32, advertisementInfo.Views);
            parameters.AddInParameter("@CountClick", DbType.Boolean, advertisementInfo.CountClick);
            parameters.AddInParameter("@Clicks", DbType.Int32, advertisementInfo.Clicks);
            parameters.AddInParameter("@Passed", DbType.Boolean, advertisementInfo.Passed);
            parameters.AddInParameter("@OverdueDate", DbType.DateTime, advertisementInfo.OverdueDate);
            return parameters;
        }

        public int GetTotalOfAdvertisements()
        {
            return this.m_NumAdvertisements;
        }

        private static string GetZondIdList(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ADId", DbType.Int32, id);
            StringBuilder sb = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT ZoneId FROM PE_Zone_Advertisement WHERE AdId = @AdId", cmdParams))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetInt32("ZoneId").ToString());
                }
            }
            return sb.ToString();
        }

        public bool PassedAdvertisement(string id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@strAdId", DbType.String, id);
            return DBHelper.ExecuteProc("PR_AD_Advertisement_Passed", cmdParams);
        }

        public bool Update(AdvertisementInfo advertisementInfo)
        {
            return DBHelper.ExecuteProc("PR_AD_Advertisement_Update", GetParameters(advertisementInfo));
        }
    }
}


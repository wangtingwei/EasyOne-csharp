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

    public sealed class Region : IRegion
    {
        private int m_TotalOfRegion;

        public bool Add(RegionInfo regionInfo)
        {
            Parameters cmdParams = GetParameters(regionInfo);
            return DBHelper.ExecuteProc("PR_Accessories_Region_Add", cmdParams);
        }

        public bool AreaExists(string area, string province, string city)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Area", DbType.String, area);
            cmdParams.AddInParameter("@Province", DbType.String, province);
            cmdParams.AddInParameter("@City", DbType.String, city);
            return DBHelper.ExistsProc("PR_Accessories_Region_CheckArea", cmdParams);
        }

        public bool Delete(string regionId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RegionId", DbType.String, regionId);
            return DBHelper.ExecuteProc("PR_Accessories_Region_Delete", cmdParams);
        }

        public IList<RegionInfo> GetAreaListByCity(string city)
        {
            IList<RegionInfo> list = new List<RegionInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@City", DbType.String, city);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT DISTINCT Area FROM PE_Region WHERE City = @City", cmdParams))
            {
                while (reader.Read())
                {
                    RegionInfo item = new RegionInfo();
                    item.Area = reader.GetString("Area");
                    list.Add(item);
                }
            }
            return list;
        }

        public RegionInfo GetByPostCodeOfFourNumber(string postCode)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Region WHERE LEFT(PostCode, 4) = LEFT(@PostCode, 4)", new Parameters("@PostCode", DbType.Int32, postCode)))
            {
                if (reader.Read())
                {
                    return RegionFromDataReader(reader);
                }
                return new RegionInfo(true);
            }
        }

        public IList<RegionInfo> GetCityListByProvince(string province)
        {
            IList<RegionInfo> list = new List<RegionInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Province", DbType.String, province);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT DISTINCT City FROM PE_Region WHERE Province = @Province", cmdParams))
            {
                while (reader.Read())
                {
                    RegionInfo item = new RegionInfo();
                    item.City = reader.GetString("City");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<RegionInfo> GetCountryList()
        {
            IList<RegionInfo> list = new List<RegionInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT DISTINCT Country FROM PE_Region ORDER BY Country"))
            {
                while (reader.Read())
                {
                    RegionInfo item = new RegionInfo();
                    item.Country = reader.GetString("Country");
                    list.Add(item);
                }
            }
            return list;
        }

        private static Parameters GetParameters(RegionInfo regionInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@Country", DbType.String, regionInfo.Country);
            parameters.AddInParameter("@Province", DbType.String, regionInfo.Province);
            parameters.AddInParameter("@City", DbType.String, regionInfo.City);
            parameters.AddInParameter("@Area", DbType.String, regionInfo.Area);
            parameters.AddInParameter("@PostCode", DbType.String, regionInfo.PostCode);
            parameters.AddInParameter("@AreaCode", DbType.String, regionInfo.AreaCode);
            return parameters;
        }

        public IList<RegionInfo> GetProvinceListByCountry(string country)
        {
            IList<RegionInfo> list = new List<RegionInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Country", DbType.String, country);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT DISTINCT Province FROM PE_Region WHERE Country = @Country", cmdParams))
            {
                while (reader.Read())
                {
                    RegionInfo item = new RegionInfo();
                    item.Province = reader.GetString("Province");
                    list.Add(item);
                }
            }
            return list;
        }

        public RegionInfo GetRegionById(int regionId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RegionID", DbType.Int32, regionId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_Region_GetByID", cmdParams))
            {
                if (reader.Read())
                {
                    return RegionFromDataReader(reader);
                }
                return new RegionInfo(true);
            }
        }

        public IList<RegionInfo> GetRegionList(int startRowIndexId, int maxiNumRows, string searchType, string keyword)
        {
            IList<RegionInfo> list = new List<RegionInfo>();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxiNumRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "RegionID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            if (!string.IsNullOrEmpty(searchType) && !string.IsNullOrEmpty(keyword))
            {
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, searchType + " LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
            }
            else
            {
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            }
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Region");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    RegionInfo item = new RegionInfo();
                    item.RegionId = reader.GetInt32("RegionId");
                    item.Country = reader.GetString("Country");
                    item.Province = reader.GetString("Province");
                    item.City = Convert.IsDBNull(reader["City"]) ? "" : reader.GetString("City");
                    item.Area = Convert.IsDBNull(reader["Area"]) ? "" : reader.GetString("Area");
                    item.PostCode = reader.GetString("PostCode");
                    item.AreaCode = reader.GetString("AreaCode");
                    list.Add(item);
                }
            }
            this.m_TotalOfRegion = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetTotalOfRegion()
        {
            return this.m_TotalOfRegion;
        }

        public string GetZipCodeByArea(string country, string province, string city, string area)
        {
            string strSql = "SELECT TOP 1 postcode FROM PE_Region WHERE country = @country AND province = @province AND city = @city AND area = @area";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@country", DbType.String, country);
            cmdParams.AddInParameter("@province", DbType.String, province);
            cmdParams.AddInParameter("@city", DbType.String, city);
            cmdParams.AddInParameter("@area", DbType.String, area);
            object obj2 = DBHelper.ExecuteScalarSql(strSql, cmdParams);
            if (obj2 == null)
            {
                return string.Empty;
            }
            return obj2.ToString();
        }

        public bool PostCodeExists(string postCode)
        {
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Region WHERE LEFT(PostCode, 2) = LEFT(@PostCode, 2)", new Parameters("@PostCode", DbType.Int32, postCode));
        }

        private static RegionInfo RegionFromDataReader(NullableDataReader rdr)
        {
            RegionInfo info = new RegionInfo();
            info.Area = rdr.GetString("Area");
            info.AreaCode = rdr.GetString("AreaCode");
            info.City = rdr.GetString("City");
            info.Country = rdr.GetString("Country");
            info.PostCode = rdr.GetString("PostCode");
            info.Province = rdr.GetString("Province");
            info.RegionId = rdr.GetInt32("RegionID");
            return info;
        }

        public bool Update(RegionInfo regionInfo)
        {
            Parameters cmdParams = GetParameters(regionInfo);
            cmdParams.AddInParameter("@RegionId", DbType.Int32, regionInfo.RegionId);
            return DBHelper.ExecuteProc("PR_Accessories_Region_Update", cmdParams);
        }
    }
}


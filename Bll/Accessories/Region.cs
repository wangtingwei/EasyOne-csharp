namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using EasyOne.DalFactory;

    public sealed class Region
    {
        private static readonly IRegion dal = DataAccess.CreateRegion();

        private Region()
        {
        }

        public static bool Add(EasyOne.Model.Accessories.RegionInfo regionInfo)
        {
            return dal.Add(regionInfo);
        }

        public static bool AreaExists(string area, string province, string city)
        {
            return dal.AreaExists(area, province, city);
        }

        public static bool Delete(EasyOne.Model.Accessories.RegionInfo regionInfo)
        {
            return dal.Delete(regionInfo.RegionId.ToString(CultureInfo.CurrentCulture));
        }

        public static bool Delete(string regionId)
        {
            bool flag = false;
            if (DataValidator.IsValidId(regionId))
            {
                flag = dal.Delete(regionId);
            }
            return flag;
        }

        public static IList<EasyOne.Model.Accessories.RegionInfo> GetAreaListByCity(string city)
        {
            return dal.GetAreaListByCity(DataSecurity.FilterBadChar(city));
        }

        public static EasyOne.Model.Accessories.RegionInfo GetByPostCodeOfFourNumber(string postCode)
        {
            EasyOne.Model.Accessories.RegionInfo byPostCodeOfFourNumber = new EasyOne.Model.Accessories.RegionInfo(true);
            if (DataValidator.IsNumber(postCode) && (DataSecurity.Len(postCode) == 6))
            {
                byPostCodeOfFourNumber = dal.GetByPostCodeOfFourNumber(postCode);
            }
            return byPostCodeOfFourNumber;
        }

        public static IList<EasyOne.Model.Accessories.RegionInfo> GetCityListByProvince(string province)
        {
            return dal.GetCityListByProvince(DataSecurity.FilterBadChar(province));
        }

        public static IList<EasyOne.Model.Accessories.RegionInfo> GetCountryList()
        {
            return dal.GetCountryList();
        }

        public static IList<EasyOne.Model.Accessories.RegionInfo> GetProvinceListByCountry(string country)
        {
            return dal.GetProvinceListByCountry(DataSecurity.FilterBadChar(country));
        }

        public static EasyOne.Model.Accessories.RegionInfo GetRegionById(int regionId)
        {
            return dal.GetRegionById(regionId);
        }

        public static IList<EasyOne.Model.Accessories.RegionInfo> GetRegionList(string searchType, string keyword, int startRowIndexId, int maxiNumRows)
        {
            searchType = DataSecurity.FilterBadChar(searchType);
            keyword = DataSecurity.FilterBadChar(keyword);
            return dal.GetRegionList(startRowIndexId, maxiNumRows, searchType, keyword);
        }

        public static int GetTotalOfRegion(string searchType, string keyword, int startRowIndexId, int maxiNumRows)
        {
            return dal.GetTotalOfRegion();
        }

        public static string GetZipCodeByArea(string country, string province, string city, string area)
        {
            area = string.IsNullOrEmpty(area) ? string.Empty : area;
            return dal.GetZipCodeByArea(country, province, city, area);
        }

        public static bool PostCodeExists(string postCode)
        {
            return ((DataValidator.IsNumber(postCode) && (DataSecurity.Len(postCode) == 6)) && dal.PostCodeExists(postCode));
        }

        public static bool Update(EasyOne.Model.Accessories.RegionInfo regionInfo)
        {
            return dal.Update(regionInfo);
        }
    }
}


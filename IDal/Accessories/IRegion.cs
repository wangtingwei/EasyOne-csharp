namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IRegion
    {
        bool Add(RegionInfo regionInfo);
        bool AreaExists(string area, string province, string city);
        bool Delete(string regionId);
        IList<RegionInfo> GetAreaListByCity(string city);
        RegionInfo GetByPostCodeOfFourNumber(string postCode);
        IList<RegionInfo> GetCityListByProvince(string province);
        IList<RegionInfo> GetCountryList();
        IList<RegionInfo> GetProvinceListByCountry(string country);
        RegionInfo GetRegionById(int regionId);
        IList<RegionInfo> GetRegionList(int startRowIndexId, int maxiNumRows, string searchType, string keyword);
        int GetTotalOfRegion();
        string GetZipCodeByArea(string country, string province, string city, string area);
        bool PostCodeExists(string postCode);
        bool Update(RegionInfo regionInfo);
    }
}


namespace EasyOne.IDal.AD
{
    using EasyOne.Enumerations;
    using EasyOne.Model.AD;
    using System;
    using System.Collections.Generic;

    public interface IAdvertisement
    {
        bool Add(AdvertisementInfo advertisementInfo);
        bool CancelPassedAdvertisement(string id);
        bool CopyAdvertisement(int id);
        bool Delete(string id);
        IList<AdvertisementInfo> GetADList(int zoneId);
        IList<AdvertisementInfo> GetADList(int zoneId, int type);
        AdvertisementInfo GetAdvertisementById(int id);
        IList<AdvertisementInfo> GetAdvertisementList(int startRowIndexId, int maxNumberRows, int zoneId, ADSearchType listType, string keyword);
        int GetTotalOfAdvertisements();
        bool PassedAdvertisement(string id);
        bool Update(AdvertisementInfo advertisementInfo);
    }
}


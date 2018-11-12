namespace EasyOne.IDal.AD
{
    using EasyOne.Enumerations;
    using EasyOne.Model.AD;
    using System;
    using System.Collections.Generic;

    public interface IADZone
    {
        bool ActiveADZone(string id);
        bool Add(ADZoneInfo adZoneInfo);
        bool ClearADZone(int id);
        bool CopyADZone(int id);
        bool Delete(string id);
        bool ExportData(string zoneId, string importDatabase, bool chkFormatConn);
        ADZoneInfo GetADZoneById(int id);
        IList<ADZoneInfo> GetAllADZone(int startRowIndexId, int maxNumberRows, ADZoneSearchType listType, string keywords);
        IList<ADZoneInfo> GetImportList(string importDatabase);
        string GetNewJSName();
        int GetTotalOfADZone();
        bool ImportData(string zoneId, string importDatabase);
        bool PauseADZone(string id);
        bool Update(ADZoneInfo adZoneInfo);
    }
}


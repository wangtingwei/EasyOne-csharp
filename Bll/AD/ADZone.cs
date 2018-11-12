namespace EasyOne.AD
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.AD;
    using EasyOne.Model.AD;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web;
    using EasyOne.DalFactory;

    public sealed class ADZone
    {
        private static readonly IADZone dal = DataAccess.CreateAdZone();

        private ADZone()
        {
        }

        public static bool ActiveADZone(string id)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(id))
            {
                flag = dal.ActiveADZone(id);
                CreateJS(id);
            }
            return flag;
        }

        public static DataActionState Add(ADZoneInfo adZoneInfo)
        {
            DataActionState unknown = DataActionState.Unknown;
            if (!dal.Add(adZoneInfo))
            {
                return unknown;
            }
            if (adZoneInfo.Active)
            {
                CreateJS(adZoneInfo.ZoneId.ToString(CultureInfo.CurrentCulture));
            }
            return DataActionState.Successed;
        }

        public static bool ClearADZone(string zoneId)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(zoneId))
            {
                int id = Convert.ToInt32(zoneId, CultureInfo.CurrentCulture);
                flag = dal.ClearADZone(id);
                CreateJS(zoneId);
            }
            return flag;
        }

        public static bool CopyADZone(string id)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(id))
            {
                int num = DataConverter.CLng(id);
                flag = dal.CopyADZone(num);
            }
            return flag;
        }

        public static void CreateJS(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string[] strArray = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                ADZoneJS ejs = new ADZoneJS();
                for (int i = 0; i < strArray.Length; i++)
                {
                    ADZoneInfo adZoneById = GetAdZoneById(DataConverter.CLng(strArray[i]));
                    if (adZoneById.Active)
                    {
                        IList<AdvertisementInfo> aDList = Advertisement.GetADList(adZoneById.ZoneId, adZoneById.ShowType);
                        if (aDList.Count > 0)
                        {
                            ejs.CreateJS(adZoneById, aDList);
                        }
                    }
                }
            }
        }

        public static bool Delete(string id)
        {
            bool flag = false;
            if (DataValidator.IsValidId(id))
            {
                flag = dal.Delete(id);
            }
            return flag;
        }

        public static void DeleteJS(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string[] strArray = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string advertisementDir = SiteConfig.SiteOption.AdvertisementDir;
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    advertisementDir = current.Server.MapPath("~/" + advertisementDir);
                }
                for (int i = 0; i < strArray.Length; i++)
                {
                    ADZoneInfo adZoneById = GetAdZoneById(DataConverter.CLng(strArray[i]));
                    FileSystemObject.Delete(VirtualPathUtility.AppendTrailingSlash(advertisementDir) + adZoneById.ZoneJSName, FsoMethod.File);
                }
            }
        }

        public static bool ExportData(string zoneId, string importDatabase, bool chkFormatConn)
        {
            return dal.ExportData(DataSecurity.FilterBadChar(zoneId), importDatabase.Replace("'", ""), chkFormatConn);
        }

        public static ADZoneInfo GetAdZoneById(int id)
        {
            return dal.GetADZoneById(id);
        }

        public static IList<ADZoneInfo> GetADZoneList(int startRowIndex, int maximumRows)
        {
            return GetADZoneList(startRowIndex, maximumRows, ADZoneSearchType.None, null);
        }

        public static IList<ADZoneInfo> GetADZoneList(int startRowIndex, int maximumRows, ADZoneSearchType listType, string keyword)
        {
            return dal.GetAllADZone(startRowIndex, maximumRows, listType, keyword);
        }

        public static IList<ADZoneInfo> GetImportList(string importDatabase)
        {
            return dal.GetImportList(importDatabase.Replace("'", ""));
        }

        public static string GetNewJSName()
        {
            return dal.GetNewJSName();
        }

        public static int GetTotalOfADZone(ADZoneSearchType listType, string keyword)
        {
            return dal.GetTotalOfADZone();
        }

        public static bool ImportData(string zoneId, string importDatabase)
        {
            return dal.ImportData(DataSecurity.FilterBadChar(zoneId), importDatabase.Replace("'", ""));
        }

        public static bool PauseADZone(string id)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(id))
            {
                flag = dal.PauseADZone(id);
                DeleteJS(id);
            }
            return flag;
        }

        public static bool Update(ADZoneInfo adZoneInfo)
        {
            return dal.Update(adZoneInfo);
        }
    }
}


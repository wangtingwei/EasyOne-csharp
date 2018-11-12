namespace EasyOne.AD
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.AD;
    using EasyOne.Model.AD;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using EasyOne.DalFactory;

    public sealed class Advertisement
    {
        private static readonly IAdvertisement dal = DataAccess.CreateAdvertisement();

        private Advertisement()
        {
        }

        public static DataActionState Add(AdvertisementInfo advertisementInfo)
        {
            DataActionState unknown = DataActionState.Unknown;
            if (dal.Add(advertisementInfo))
            {
                unknown = DataActionState.Successed;
            }
            return unknown;
        }

        public static bool CancelPassed(string id)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(id))
            {
                flag = dal.CancelPassedAdvertisement(id);
            }
            return flag;
        }

        public static bool CopyAdvertisement(string id)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(id))
            {
                flag = dal.CopyAdvertisement(DataConverter.CLng(id));
            }
            return flag;
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

        public static string GetAdContent(AdvertisementInfo advertisementInfo)
        {
            string imgUrl;
            StringBuilder builder = new StringBuilder();
            switch (advertisementInfo.ADType)
            {
                case 1:
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("<img src=\"");
                    imgUrl = advertisementInfo.ImgUrl;
                    imgUrl = Utility.ConvertAbsolutePath(SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.AdvertisementDir), imgUrl);
                    builder2.Append(imgUrl);
                    builder2.Append("\"");
                    if (advertisementInfo.ImgWidth > 0)
                    {
                        builder2.Append(" width=\"");
                        builder2.Append(advertisementInfo.ImgWidth);
                        builder2.Append("\"");
                    }
                    if (advertisementInfo.ImgHeight > 0)
                    {
                        builder2.Append("  height=\"");
                        builder2.Append(advertisementInfo.ImgHeight);
                        builder2.Append("\"");
                    }
                    builder2.Append(" border=\"0\"></img>");
                    if (string.IsNullOrEmpty(advertisementInfo.LinkUrl))
                    {
                        builder = builder2;
                        break;
                    }
                    builder.Append("<a href=\"");
                    builder.Append(advertisementInfo.LinkUrl);
                    builder.Append("\"");
                    if (advertisementInfo.LinkTarget == 0)
                    {
                        builder.Append(" target=\"_self\"");
                    }
                    else
                    {
                        builder.Append(" target=\"_blank\"");
                    }
                    builder.Append(" title=\"");
                    builder.Append(advertisementInfo.LinkAlt);
                    builder.Append("\"");
                    builder.Append(">");
                    builder.Append(builder2.ToString());
                    builder.Append("</a>");
                    break;
                }
                case 2:
                    builder.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\"");
                    if (advertisementInfo.ImgWidth > 0)
                    {
                        builder.Append(" width=\"");
                        builder.Append(advertisementInfo.ImgWidth);
                        builder.Append("\"");
                    }
                    if (advertisementInfo.ImgHeight > 0)
                    {
                        builder.Append("  height=\"");
                        builder.Append(advertisementInfo.ImgHeight);
                        builder.Append("\"");
                    }
                    builder.Append("><param name=\"movie\" value=\"");
                    imgUrl = advertisementInfo.ImgUrl;
                    imgUrl = Utility.ConvertAbsolutePath(SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.AdvertisementDir), imgUrl);
                    builder.Append(imgUrl);
                    builder.Append("\">");
                    if (advertisementInfo.FlashWmode == 1)
                    {
                        builder.Append("<param name=\"wmode\" value=\"transparent\">");
                    }
                    builder.Append("<param name=\"quality\" value=\"autohigh\">");
                    builder.Append("<embed src=\"");
                    builder.Append(imgUrl);
                    builder.Append("\" quality=\"autohigh\"  pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\"");
                    if (advertisementInfo.FlashWmode == 1)
                    {
                        builder.Append(" wmode=\"transparent\" ");
                    }
                    if (advertisementInfo.ImgWidth > 0)
                    {
                        builder.Append(" width=\"");
                        builder.Append(advertisementInfo.ImgWidth);
                        builder.Append("\"");
                    }
                    if (advertisementInfo.ImgHeight > 0)
                    {
                        builder.Append("  height=\"");
                        builder.Append(advertisementInfo.ImgHeight);
                        builder.Append("\"");
                    }
                    builder.Append("></embed></object>");
                    break;

                case 3:
                    builder.Append(advertisementInfo.ADIntro);
                    break;

                case 4:
                    builder.Append(advertisementInfo.ADIntro);
                    break;

                case 5:
                    builder.Append("<iframe id=\"AD_");
                    builder.Append(advertisementInfo.ADId);
                    builder.Append("\" marginwidth=0 marginheight=0 hspace=0 vspace=0 frameborder=0 scrolling=no width=100% height=100% src=\"");
                    builder.Append(advertisementInfo.ADIntro);
                    builder.Append("\">AD</iframe>");
                    break;
            }
            return builder.ToString();
        }

        public static IList<AdvertisementInfo> GetADList(int zoneId)
        {
            return dal.GetADList(zoneId);
        }

        public static IList<AdvertisementInfo> GetADList(int zoneId, int type)
        {
            return dal.GetADList(zoneId, type);
        }

        public static ArrayList GetADType()
        {
            ArrayList list = new ArrayList();
            list.Add("图片");
            list.Add("动画");
            list.Add("文本");
            list.Add("代码");
            list.Add("页面");
            return list;
        }

        public static AdvertisementInfo GetAdvertisementById(int id)
        {
            return dal.GetAdvertisementById(id);
        }

        public static IList<AdvertisementInfo> GetAdvertisementList(int startRowIndex, int maximumRows, int zoneId, ADSearchType listType, string keyword)
        {
            return dal.GetAdvertisementList(startRowIndex, maximumRows, zoneId, listType, keyword);
        }

        public static IList<AdvertisementInfo> GetAdvertisementListByZoneId(int zoneId)
        {
            return dal.GetAdvertisementList(0, 0, zoneId, ADSearchType.Zone, null);
        }

        public static int GetTotalOfAdvertisements(int zoneId, ADSearchType listType, string keyword)
        {
            return dal.GetTotalOfAdvertisements();
        }

        public static bool Passed(string id)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(id))
            {
                flag = dal.PassedAdvertisement(id);
            }
            return flag;
        }

        public static DataActionState Update(AdvertisementInfo advertisementInfo)
        {
            DataActionState unknown = DataActionState.Unknown;
            if (dal.Update(advertisementInfo))
            {
                unknown = DataActionState.Successed;
            }
            return unknown;
        }
    }
}


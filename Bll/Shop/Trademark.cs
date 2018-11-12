namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Trademark
    {
        private static readonly ITrademark dal = DataAccess.CreateTrademark();

        private Trademark()
        {
        }

        public static bool Add(TrademarkInfo trademarkInfo)
        {
            return dal.Add(trademarkInfo);
        }

        public static bool Delete(string trademarkId)
        {
            return (DataValidator.IsValidId(trademarkId) && dal.Delete(trademarkId));
        }

        public static IList<TrademarkInfo> GetList(int producerId)
        {
            if (producerId <= 0)
            {
                return new List<TrademarkInfo>();
            }
            return dal.GetList(producerId);
        }

        public static IList<TrademarkInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, string trademarkType, bool isPassed)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(searchType), DataSecurity.FilterBadChar(keyword), DataConverter.CLng(trademarkType, -1), isPassed);
        }

        public static string GetSearchTypeName(string searchType)
        {
            string str = "";
            string str2 = searchType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "TrademarkName"))
            {
                if (str2 != "TrademarkIntro")
                {
                    return str;
                }
            }
            else
            {
                return "品牌名称";
            }
            return "品牌简介";
        }

        public static int GetTotalOfTrademark(string searchType, string keyword, string trademarkType, bool isPassed)
        {
            return dal.GetTotalOfTrademark();
        }

        public static TrademarkInfo GetTrademarkById(int trademarkId)
        {
            return dal.GetTrademarkById(trademarkId);
        }

        public static string GetTrademarkType(int trademarkType)
        {
            switch (trademarkType)
            {
                case 0:
                    return "其他品牌";

                case 1:
                    return "大陆品牌";

                case 2:
                    return "港台品牌";

                case 3:
                    return "日韩品牌";

                case 4:
                    return "欧美品牌";
            }
            return "";
        }

        public static bool SetElite(int trademarkId, bool value)
        {
            return dal.SetElite(trademarkId, value);
        }

        public static bool SetOnTop(int trademarkId, bool value)
        {
            return dal.SetOnTop(trademarkId, value);
        }

        public static bool SetPassed(int trademarkId, bool value)
        {
            return dal.SetPassed(trademarkId, value);
        }

        public static bool TrademarkNameExists(string trademarkName)
        {
            return dal.TrademarkNameExists(trademarkName);
        }

        public static bool Update(TrademarkInfo trademarkInfo)
        {
            return dal.Update(trademarkInfo);
        }
    }
}


namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Producer
    {
        private static readonly IProducer dal = DataAccess.CreateProducer();

        private Producer()
        {
        }

        public static bool Add(ProducerInfo producerInfo)
        {
            return dal.Add(producerInfo);
        }

        public static bool Delete(string producerId)
        {
            return (DataValidator.IsValidId(producerId) && dal.Delete(producerId));
        }

        public static IList<ProducerInfo> GetList()
        {
            return dal.GetList();
        }

        public static IList<ProducerInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, string producerType, bool isPassed)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(searchType), DataSecurity.FilterBadChar(keyword), DataConverter.CLng(producerType, -1), isPassed);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static ProducerInfo GetProducerById(int producerId)
        {
            return dal.GetProducerById(producerId);
        }

        public static string GetProducerType(int producerType)
        {
            switch (producerType)
            {
                case 0:
                    return "其他厂商";

                case 1:
                    return "大陆厂商";

                case 2:
                    return "港台厂商";

                case 3:
                    return "日韩厂商";

                case 4:
                    return "欧美厂商";
            }
            return "";
        }

        public static string GetSearchTypeName(string searchType)
        {
            switch (searchType)
            {
                case "ProducerName":
                    return "厂商名称";

                case "ProducerShortName":
                    return "厂商缩写";

                case "Address":
                    return "厂商地址";

                case "Postcode":
                    return "厂商邮编";

                case "Phone":
                    return "厂商电话";

                case "ProducerIntro":
                    return "厂商简介";
            }
            return "";
        }

        public static int GetTotalOfProducer(string searchType, string keyword, string producerType, bool isPassed)
        {
            return dal.GetTotalOfProducer();
        }

        public static bool ProducerNameExists(string producerName)
        {
            return dal.ProducerNameExists(producerName);
        }

        public static bool SetElite(int producerId, bool value)
        {
            return dal.SetElite(producerId, value);
        }

        public static bool SetOnTop(int producerId, bool value)
        {
            return dal.SetOnTop(producerId, value);
        }

        public static bool SetPassed(int producerId, bool value)
        {
            return dal.SetPassed(producerId, value);
        }

        public static bool Update(ProducerInfo producerInfo)
        {
            return dal.Update(producerInfo);
        }
    }
}


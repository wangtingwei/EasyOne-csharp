namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IProducer
    {
        bool Add(ProducerInfo producerInfo);
        bool Delete(string producerId);
        IList<ProducerInfo> GetList();
        IList<ProducerInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, int producerType, bool isPassed);
        int GetMaxId();
        ProducerInfo GetProducerById(int producerId);
        int GetTotalOfProducer();
        bool ProducerNameExists(string producerName);
        bool SetElite(int producerId, bool value);
        bool SetOnTop(int producerId, bool value);
        bool SetPassed(int producerId, bool value);
        bool Update(ProducerInfo producerInfo);
    }
}


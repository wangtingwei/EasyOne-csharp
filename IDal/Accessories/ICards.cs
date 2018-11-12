namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface ICards
    {
        bool CardAdd(CardInfo info);
        bool DelCard(int cardId);
        CardInfo GetCardById(int cardId);
        CardInfo GetCardByNumAndPassword(string cardNum, string password);
        CardInfo GetCardByOrderItemId(int productId, string tableName, int orderItemId);
        IList<CardInfo> GetCardList(string tableName, int productId, int orderItemId);
        IList<CardInfo> GetCardList(int startRowIndexId, int maxiNumRows, int cardType, int cardStatus, int field, string keyword, string agentName);
        int GetTotalofCards(int cardType, int cardStatus, int field, string keyword, string agentName);
        IList<CardInfo> GetUnsoldCard(string tableName, int productId, int amount);
        bool Update(CardInfo info);
    }
}


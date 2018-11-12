namespace EasyOne.IDal.Contents
{
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;

    public interface IContentCharge
    {
        bool Add(ContentChargeInfo contentChargeInfo);
        bool BatchUpdate(ContentChargeInfo contentChargeInfo, string itemId, Dictionary<string, bool> checkItem);
        void Delete(int generalId);
        void Delete(string generalId);
        bool Exists(int generalId);
        ContentChargeInfo GetContentChargeInfoById(int generalId);
        bool Update(ContentChargeInfo contentChargeInfo);
    }
}


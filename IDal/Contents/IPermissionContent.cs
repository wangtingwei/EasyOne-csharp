namespace EasyOne.IDal.Contents
{
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;

    public interface IPermissionContent
    {
        bool Add(ContentPermissionInfo contentPermissionInfo);
        bool BatchUpdate(ContentPermissionInfo contentPermissionInfo, string itemId, Dictionary<string, bool> checkItem);
        void Delete(int generalId);
        void Delete(string generalId);
        bool Exists(int generalId);
        ContentPermissionInfo GetContentPermissionInfoById(int generalId);
        bool Update(ContentPermissionInfo contentPermissionInfo);
    }
}


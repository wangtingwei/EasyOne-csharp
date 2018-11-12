namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IFavorite
    {
        bool Add(FavoriteInfo favoriteInfo);
        bool Delete(int userId);
        bool Delete(int userId, int infoId);
        bool Delete(int userId, string infoIds);
        bool Exists(int userId, int infoId);
        IList<FavoriteInfo> GetList(int startRowIndexId, int maxNumberRows, int userId, int nodeId);
        int GetMaxId();
        int GetTotalOfFavorite();
        int GetUserFavoiteCount(int userId);
    }
}


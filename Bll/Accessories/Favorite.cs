namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Favorite
    {
        private static readonly IFavorite dal = DataAccess.CreateFavorite();

        private Favorite()
        {
        }

        public static bool Add(FavoriteInfo favoriteInfo)
        {
            if (Exists(favoriteInfo.UserId, favoriteInfo.InfoId))
            {
                return false;
            }
            favoriteInfo.FavoriteId = GetMaxId() + 1;
            return dal.Add(favoriteInfo);
        }

        public static bool Delete(int userId)
        {
            return dal.Delete(userId);
        }

        public static bool Delete(int userId, int infoId)
        {
            return dal.Delete(userId, infoId);
        }

        public static bool Delete(int userId, string infoIds)
        {
            if (!DataValidator.IsValidId(infoIds))
            {
                return false;
            }
            return dal.Delete(userId, infoIds);
        }

        public static bool Exists(int userId, int infoId)
        {
            return dal.Exists(userId, infoId);
        }

        public static IList<FavoriteInfo> GetList(int startRowIndexId, int maxNumberRows, int userId, int nodeId)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, userId, nodeId);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static int GetTotalOfFavorite(int userId, int nodeId)
        {
            return dal.GetTotalOfFavorite();
        }

        public static int GetUserFavoiteCount(int userId)
        {
            return dal.GetUserFavoiteCount(userId);
        }
    }
}


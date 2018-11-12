namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Author
    {
        private static readonly IAuthor dal = DataAccess.CreateAuthorInfo();
        private static readonly IUsers userDal = DataAccess.CreateUsers();

        private Author()
        {
        }

        public static bool Add(AuthorInfo ainfo)
        {
            if (dal.Exists(ainfo.Name))
            {
                return false;
            }
            return dal.Add(ainfo);
        }

        public static bool Delete(string authorId)
        {
            return (DataValidator.IsValidId(authorId) && dal.Delete(authorId));
        }

        public static bool ExistsPassedAuthor(string authorName)
        {
            if (string.IsNullOrEmpty(authorName))
            {
                return false;
            }
            return dal.ExistsPassedAuthor(authorName);
        }

        public static AuthorInfo GetAuthorInfoById(int id)
        {
            return dal.GetAuthorInfoById(id);
        }

        public static AuthorInfo GetAuthorInfoByUserId(int userId)
        {
            return dal.GetAuthorInfoByUserId(userId);
        }

        public static IList<AuthorInfo> GetAuthorList(int startRowIndexId, int maxNumberRows, int listType, string searchType, string keyword)
        {
            return dal.GetAuthorList(startRowIndexId, maxNumberRows, listType, searchType, keyword, false);
        }

        public static IList<AuthorInfo> GetAuthorList(int startRowIndexId, int maxNumberRows, int listType, string searchType, string keyword, bool isDisable)
        {
            return dal.GetAuthorList(startRowIndexId, maxNumberRows, listType, searchType, keyword, isDisable);
        }

        public static int GetTotalOfAuthor(int listType, string searchType, string keyword)
        {
            return dal.GetTotalOfAuthor();
        }

        public static int GetUserId(string userName)
        {
            return userDal.GetUsersByUserName(userName).UserId;
        }

        public static string GetUserName(int userId)
        {
            return userDal.GetUserById(userId).UserName;
        }

        public static bool Update(AuthorInfo ainfo)
        {
            if (string.IsNullOrEmpty(ainfo.Name))
            {
                return false;
            }
            return dal.Update(ainfo);
        }
    }
}


namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IAuthor
    {
        bool Add(AuthorInfo authorInfo);
        bool Delete(string strId);
        bool Exists(string authorname);
        bool ExistsPassedAuthor(string authorName);
        AuthorInfo GetAuthorInfoById(int id);
        AuthorInfo GetAuthorInfoByUserId(int userId);
        IList<AuthorInfo> GetAuthorList(int startRowIndexId, int maxNumberRows, int listType, string searchType, string keyword, bool isDisable);
        int GetTotalOfAuthor();
        bool Update(AuthorInfo authorInfo);
    }
}


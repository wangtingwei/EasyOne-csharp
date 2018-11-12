namespace EasyOne.IDal.Contents
{
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;

    public interface ICommentPKZone
    {
        void Add(CommentPKZoneInfo commentPKZoneInfo);
        void Delete(int pkId);
        void DeleteByCommentId(int commentId);
        IList<CommentPKZoneInfo> GetList(int startRowIndexId, int maxNumberRows, int commentId);
        IList<CommentPKZoneInfo> GetList(int startRowIndexId, int maxNumberRows, int commentId, int position);
        CommentPKZoneInfo GetModelInfo(int pkId);
        int GetPKCount(int commentId, int position);
        int GetTotalOfCommentPKZoneInfo();
        int MaxCommentPKZoneId();
    }
}


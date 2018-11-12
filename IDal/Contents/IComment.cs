namespace EasyOne.IDal.Contents
{
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;

    public interface IComment
    {
        bool Add(CommentInfo commentInfo);
        bool AdministratorReply(CommentInfo commentInfo);
        bool Delete(int commentId);
        bool Delete(string commentIds);
        bool Delete(int commentId, string userName);
        bool Elite(int commentId, bool isElite);
        CommentInfo GetCommentInfo(int commentId);
        int GetCountByStatus(int status);
        CommentInfo GetExtendCommentInfo(int commentId);
        IList<CommentInfo> GetList(int startRowIndexId, int maxNumberRows);
        IList<CommentInfo> GetList(int startRowIndexId, int maxNumberRows, int generalId);
        IList<CommentInfo> GetList(int startRowIndexId, int maxNumberRows, int generalId, int type);
        IList<CommentInfo> GetListByNodeId(int startRowIndexId, int maxNumberRows, int nodeId, int type);
        int GetTotalOfCommentInfo();
        IList<CommentInfo> GetUserCommentList(int startRowIndexId, int maxNumberRows, int nodeId, string userName);
        int MaxCommentId();
        int ScoreCount(int generalId);
        bool SetStatus(int commentId, bool status);
        bool Update(CommentInfo commentInfo);
    }
}


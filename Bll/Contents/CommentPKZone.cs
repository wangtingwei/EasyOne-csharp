namespace EasyOne.Contents
{
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class CommentPKZone
    {
        private static readonly ICommentPKZone dal = DataAccess.CreateCommentPKZone();

        private CommentPKZone()
        {
        }

        public static void Add(CommentPKZoneInfo commentPKZoneInfo)
        {
            commentPKZoneInfo.PKId = MaxCommentPKZoneId() + 1;
            dal.Add(commentPKZoneInfo);
        }

        public static void Delete(int pkId)
        {
            dal.Delete(pkId);
        }

        public static void DeleteByCommentId(int commentId)
        {
            dal.DeleteByCommentId(commentId);
        }

        public static IList<CommentPKZoneInfo> GetList(int startRowIndexId, int maxNumberRows, int commentId)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, commentId);
        }

        public static IList<CommentPKZoneInfo> GetList(int startRowIndexId, int maxNumberRows, int commentId, int position)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, commentId, position);
        }

        public static CommentPKZoneInfo GetModelInfo(int pkId)
        {
            return dal.GetModelInfo(pkId);
        }

        public static int GetPKCount(int commentId, int position)
        {
            return dal.GetPKCount(commentId, position);
        }

        public static int GetTotalOfCommentPKZoneInfo(int commentId)
        {
            return dal.GetTotalOfCommentPKZoneInfo();
        }

        public static int GetTotalOfCommentPKZoneInfo(int commentId, int position)
        {
            return dal.GetTotalOfCommentPKZoneInfo();
        }

        public static int MaxCommentPKZoneId()
        {
            return dal.MaxCommentPKZoneId();
        }
    }
}


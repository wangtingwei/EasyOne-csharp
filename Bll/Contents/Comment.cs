namespace EasyOne.Contents
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Comment
    {
        private static readonly IComment dal = DataAccess.CreateComment();

        private Comment()
        {
        }

        public static bool Add(CommentInfo commentInfo)
        {
            commentInfo.CommentId = MaxCommentId() + 1;
            if (dal.Add(commentInfo))
            {
                ContentManage.UpdateCommentAuditedAndUnaudited(commentInfo.GeneralId);
                return true;
            }
            return false;
        }

        public static bool AdministratorReply(CommentInfo commentInfo)
        {
            return dal.AdministratorReply(commentInfo);
        }

        public static bool Delete(int commentId)
        {
            CommentInfo commentInfo = dal.GetCommentInfo(commentId);
            if (dal.Delete(commentId))
            {
                ContentManage.UpdateCommentAuditedAndUnaudited(commentInfo.GeneralId);
                CommentPKZone.DeleteByCommentId(commentId);
                return true;
            }
            return false;
        }

        public static bool Delete(string commentIds)
        {
            if (!DataValidator.IsValidId(commentIds))
            {
                return false;
            }
            string[] strArray = commentIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                Delete(DataConverter.CLng(strArray[i]));
            }
            return true;
        }

        public static bool DeleteByGeneralId(int generalId)
        {
            bool flag = true;
            foreach (CommentInfo info in GetList(0, 0, generalId))
            {
                if (!Delete(info.CommentId))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public static bool DeleteByGeneralIdAndUserName(int generalId)
        {
            bool flag = true;
            string userName = PEContext.Current.User.UserName;
            foreach (CommentInfo info in GetList(0, 0, generalId))
            {
                if (((info.UserName == userName) && !info.Status) && !dal.Delete(info.CommentId, userName))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public static bool DeleteByUserName(int commentId)
        {
            string userName = PEContext.Current.User.UserName;
            CommentInfo commentInfo = dal.GetCommentInfo(commentId);
            if (((!commentInfo.IsNull && (commentInfo.UserName == userName)) && !commentInfo.Status) && dal.Delete(commentId, userName))
            {
                ContentManage.UpdateCommentAuditedAndUnaudited(commentInfo.GeneralId);
                CommentPKZone.DeleteByCommentId(commentId);
                return true;
            }
            return false;
        }

        public static bool Elite(int commentId, bool isElite)
        {
            return dal.Elite(commentId, isElite);
        }

        public static CommentInfo GetCommentInfo(int commentId)
        {
            return dal.GetCommentInfo(commentId);
        }

        public static int GetCountByStatus(int status)
        {
            return dal.GetCountByStatus(DataConverter.CLng(status));
        }

        public static CommentInfo GetExtendCommentInfo(int commentId)
        {
            return dal.GetExtendCommentInfo(commentId);
        }

        public static IList<CommentInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetList(startRowIndexId, maxNumberRows);
        }

        public static IList<CommentInfo> GetList(int startRowIndexId, int maxNumberRows, int generalId)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, generalId);
        }

        public static IList<CommentInfo> GetList(int startRowIndexId, int maxNumberRows, int generalId, int type)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, generalId, type);
        }

        public static IList<CommentInfo> GetListByNodeId(int startRowIndexId, int maxNumberRows, int nodeId, int type)
        {
            return dal.GetListByNodeId(startRowIndexId, maxNumberRows, nodeId, type);
        }

        public static int GetTotalOfCommentInfo()
        {
            return dal.GetTotalOfCommentInfo();
        }

        public static int GetTotalOfCommentInfo(int generalId)
        {
            return dal.GetTotalOfCommentInfo();
        }

        public static int GetTotalOfCommentInfo(int generalId, int type)
        {
            return dal.GetTotalOfCommentInfo();
        }

        public static IList<CommentInfo> GetUserCommentList(int startRowIndexId, int maxNumberRows, int nodeId, string userName)
        {
            return dal.GetUserCommentList(startRowIndexId, maxNumberRows, nodeId, DataSecurity.FilterBadChar(userName));
        }

        public static int MaxCommentId()
        {
            return dal.MaxCommentId();
        }

        public static int ScoreCount(int generalId)
        {
            return dal.ScoreCount(generalId);
        }

        public static bool SetStatus(int commentId, bool status)
        {
            CommentInfo commentInfo = dal.GetCommentInfo(commentId);
            if (!commentInfo.IsNull && dal.SetStatus(commentId, status))
            {
                ContentManage.UpdateCommentAuditedAndUnaudited(commentInfo.GeneralId);
                return true;
            }
            return false;
        }

        public static bool SetStatus(string commentIds, bool status)
        {
            if (!DataValidator.IsValidId(commentIds))
            {
                return false;
            }
            string[] strArray = commentIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                SetStatus(DataConverter.CLng(strArray[i]), status);
            }
            return true;
        }

        public static bool Update(CommentInfo commentInfo)
        {
            return dal.Update(commentInfo);
        }
    }
}


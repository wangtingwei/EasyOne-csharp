namespace EasyOne.Crm
{
    using EasyOne.Common;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using EasyOne.DalFactory;

    public sealed class Reply
    {
        private static readonly IReply dal = DataAccess.CreateReply();

        private Reply()
        {
        }

        public static bool Add(ReplyInfo info)
        {
            return dal.Add(info);
        }

        public static bool DeleteById(int id)
        {
            return dal.DeleteById(id);
        }

        public static bool DeleteByQuestionId(int questionId)
        {
            return dal.DeleteByQuestionId(questionId);
        }

        public static bool DeleteByQuestionId(string questionIdList)
        {
            return (DataValidator.IsValidId(questionIdList) && dal.DeleteByQuestionId(questionIdList));
        }

        public static ReplyInfo GetLastReplyById(int questionId)
        {
            return dal.GetLastReplyById(questionId);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static ReplyInfo GetReplyById(int id)
        {
            return dal.GetReplyById(id);
        }

        public static IList<ReplyInfo> GetReplyByQuestionId(int questionId)
        {
            return dal.GetReplyByQuestionId(questionId);
        }

        public static DataTable GetReplyStatistic()
        {
            return dal.GetReplyStatistic();
        }

        public static bool HasOtherReplyer(int questionId)
        {
            return dal.HasOtherReplyer(questionId);
        }

        public static bool Update(ReplyInfo info)
        {
            return dal.Update(info);
        }
    }
}


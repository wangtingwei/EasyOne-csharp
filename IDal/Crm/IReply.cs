namespace EasyOne.IDal.Crm
{
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IReply
    {
        bool Add(ReplyInfo info);
        bool DeleteById(int id);
        bool DeleteByQuestionId(int questionId);
        bool DeleteByQuestionId(string questionIdList);
        ReplyInfo GetLastReplyById(int questionId);
        int GetMaxId();
        ReplyInfo GetReplyById(int id);
        IList<ReplyInfo> GetReplyByQuestionId(int questionId);
        DataTable GetReplyStatistic();
        bool HasOtherReplyer(int questionId);
        bool Update(ReplyInfo info);
    }
}


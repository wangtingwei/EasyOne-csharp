namespace EasyOne.IDal.Crm
{
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IQuestion
    {
        bool Add(QuestionInfo info);
        void AddFavorite(int questionId);
        bool BatchSetSolved(string questionIdList);
        bool BatchSetTypeId(string questionIdList, int typeId);
        bool Delete(int questionId);
        bool Delete(string questionIdList);
        int GetAdminReplyJustNow();
        int GetMaxId();
        IList<QuestionInfo> GetQuestion(int startRowIndex, int maximumRows, int searchType, string keyword);
        QuestionInfo GetQuestionById(int id);
        int GetQuestionStatCount();
        DataTable GetQuestionStatistic(int startRowIndex, int maximumRows);
        int GetQuestionTotal();
        IList<QuestionInfo> GetQuestonsByUser(string userName, int startRowIndex, int maximumRows, int searchType, string keyword);
        IList<QuestionInfo> GetSolvedQuestions(int startRowIndex, int maximumRows, int searchType, string keyword);
        int GetTotalScore();
        int GetUserPointByUserName(string userName);
        int GetUserReplyJustNow();
        void RemoveFavoriteById(string questionIdList);
        bool RemoveFavorites(string questionIdList);
        bool SaveQuestionSetting(int questionId, int typeId, int score, bool isPublic, bool isSolved);
        bool Update(QuestionInfo info);
        bool Update(int questionId, string replyCreator, DateTime replyTime);
        bool Update(int questionId, string replyCreator, DateTime replyTime, bool isReply, bool isPublic, bool isSolved, int score);
        bool UpdateCreateTime(DateTime dt, int questionId);
    }
}


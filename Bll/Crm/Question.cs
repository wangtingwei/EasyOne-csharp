namespace EasyOne.Crm
{
    using EasyOne.Common;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using EasyOne.DalFactory;

    public sealed class Question
    {
        private static readonly IQuestion dal = DataAccess.CreateQuestion();

        private Question()
        {
        }

        public static bool Add(QuestionInfo info)
        {
            return dal.Add(info);
        }

        public static void AddFavorite(int questionId)
        {
            if (questionId > 0)
            {
                dal.AddFavorite(questionId);
            }
        }

        public static bool BatchSetSolved(string questionIdList)
        {
            return (DataValidator.IsValidId(questionIdList) && dal.BatchSetSolved(questionIdList));
        }

        public static bool BatchSetTypeId(string questionIdList, int typeId)
        {
            return (DataValidator.IsValidId(questionIdList) && dal.BatchSetTypeId(questionIdList, typeId));
        }

        public static bool Delete(int questionId)
        {
            bool flag = dal.Delete(questionId);
            if (flag)
            {
                RemoveFavoriteById(questionId.ToString());
            }
            return flag;
        }

        public static bool Delete(string questionIdList)
        {
            if (!DataValidator.IsValidId(questionIdList))
            {
                return false;
            }
            bool flag = dal.Delete(questionIdList);
            if (flag)
            {
                RemoveFavoriteById(questionIdList);
            }
            return flag;
        }

        public static int GetAdminReplyJustNow()
        {
            return dal.GetAdminReplyJustNow();
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static IList<QuestionInfo> GetQuestion(int startRowIndex, int maximumRows, int searchType, string keyword)
        {
            switch (searchType)
            {
                case 7:
                case 8:
                case 9:
                    if (string.IsNullOrEmpty(keyword))
                    {
                        searchType = 0;
                    }
                    break;
            }
            return dal.GetQuestion(startRowIndex, maximumRows, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static QuestionInfo GetQuestionById(int id)
        {
            return dal.GetQuestionById(id);
        }

        public static int GetQuestionStatCount()
        {
            return dal.GetQuestionStatCount();
        }

        public static DataTable GetQuestionStatistic(int startRowIndex, int maximumRows)
        {
            return dal.GetQuestionStatistic(startRowIndex, maximumRows);
        }

        public static int GetQuestionTotal()
        {
            return dal.GetQuestionTotal();
        }

        public static int GetQuestionTotal(int searchType, string keyword)
        {
            return GetQuestionTotal();
        }

        public static int GetQuestionTotal(string userName, int searchType, string keyword)
        {
            return GetQuestionTotal();
        }

        public static IList<QuestionInfo> GetQuestonsByUser(string userName, int startRowIndex, int maximumRows, int searchType, string keyword)
        {
            return dal.GetQuestonsByUser(userName, startRowIndex, maximumRows, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static IList<QuestionInfo> GetSolvedQuestions(int startRowIndex, int maximumRows, int searchType, string keyword)
        {
            return dal.GetSolvedQuestions(startRowIndex, maximumRows, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static int GetTotalScore()
        {
            return dal.GetTotalScore();
        }

        public static int GetUserPointByUserName(string userName)
        {
            return dal.GetUserPointByUserName(userName);
        }

        public static int GetUserReplyJustNow()
        {
            return dal.GetUserReplyJustNow();
        }

        public static void RemoveFavoriteById(string questionIdList)
        {
            if (DataValidator.IsValidId(questionIdList))
            {
                dal.RemoveFavoriteById(questionIdList);
            }
        }

        public static bool RemoveFavorites(string questionIdList)
        {
            return (DataValidator.IsValidId(questionIdList) && dal.RemoveFavorites(questionIdList));
        }

        public static bool SaveQuestionSetting(int questionId, int typeId, int score, bool isPublic, bool isSolved)
        {
            return dal.SaveQuestionSetting(questionId, typeId, score, isPublic, isSolved);
        }

        public static bool Update(QuestionInfo info)
        {
            return dal.Update(info);
        }

        public static bool Update(int questionId, string replyCreator, DateTime replyTime)
        {
            return dal.Update(questionId, replyCreator, replyTime);
        }

        public static bool Update(int questionId, string replyCreator, DateTime replyTime, bool isReply, bool isPublic, bool isSolved, int score)
        {
            return dal.Update(questionId, replyCreator, replyTime, isReply, isPublic, isSolved, score);
        }

        public static bool UpdateCreateTime(DateTime dt, int questionId)
        {
            return dal.UpdateCreateTime(dt, questionId);
        }
    }
}


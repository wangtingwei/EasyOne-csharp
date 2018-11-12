namespace EasyOne.Crm
{
    using EasyOne.Common;
    using EasyOne.IDal.Crm;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Crm;
    using EasyOne.DalFactory;

    public sealed class QuestionAllot
    {
        private static readonly IQuestionAllot dal = DataAccess.CreateQuestionAllot();

        private QuestionAllot()
        {
        }

        public static bool Add(int typeId, int adminId)
        {
            return dal.Add(typeId, adminId);
        }

        public static bool Delete(string adminIdlist)
        {
            return (DataValidator.IsValidId(adminIdlist) && dal.Delete(adminIdlist));
        }

        public static bool Delete(int typeId, int adminId)
        {
            return dal.Delete(typeId, adminId);
        }

        public static IList<QuestionAllotInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetList(startRowIndexId, maxNumberRows);
        }

        public static IList<QuestionAllotInfo> GetListByAdminId(int adminId)
        {
            return dal.GetListByAdminId(adminId);
        }

        public static int GetTotal()
        {
            return dal.GetTotal();
        }
    }
}


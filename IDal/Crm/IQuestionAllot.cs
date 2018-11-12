namespace EasyOne.IDal.Crm
{
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Crm;

    public interface IQuestionAllot
    {
        bool Add(int typeId, int adminId);
        bool Delete(string adminIdlist);
        bool Delete(int typeId, int adminId);
        IList<QuestionAllotInfo> GetList(int startRowIndexId, int maxNumberRows);
        IList<QuestionAllotInfo> GetListByAdminId(int adminId);
        int GetTotal();
    }
}


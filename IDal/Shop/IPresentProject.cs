namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IPresentProject
    {
        bool Add(PresentProjectInfo presentProjectInfo);
        bool Delete(int projectId);
        bool ExistsPresent(int presentId);
        IList<PresentProjectInfo> GetList(int startRowIndexId, int maxNumberRows);
        PresentProjectInfo GetPresentProjectById(int projectId);
        PresentProjectInfo GetPresentProjectByTotalMoney(decimal totalMoney);
        int GetTotalOfPresentProject();
        bool Locked(int projectId, bool isLocked);
        bool Update(PresentProjectInfo presentProjectInfo);
    }
}


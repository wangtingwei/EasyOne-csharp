namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class PresentProject
    {
        private static readonly IPresentProject dal = DataAccess.CreatePresentProject();

        private PresentProject()
        {
        }

        public static bool Add(PresentProjectInfo presentProject)
        {
            return dal.Add(presentProject);
        }

        public static bool Delete(int projectId)
        {
            return dal.Delete(projectId);
        }

        public static bool ExistsPresent(int presentId)
        {
            return dal.ExistsPresent(presentId);
        }

        public static IList<PresentProjectInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetList(startRowIndexId, maxNumberRows);
        }

        public static PresentProjectInfo GetPresentProjectById(int projectId)
        {
            return dal.GetPresentProjectById(projectId);
        }

        public static PresentProjectInfo GetPresentProjectByTotalMoney(decimal totalMoney)
        {
            return dal.GetPresentProjectByTotalMoney(totalMoney);
        }

        public static int GetTotalOfPresentProject()
        {
            return dal.GetTotalOfPresentProject();
        }

        public static bool Locked(int projectId, bool isLocked)
        {
            return dal.Locked(projectId, isLocked);
        }

        public static bool Update(PresentProjectInfo presentProject)
        {
            return dal.Update(presentProject);
        }
    }
}


namespace EasyOne.WorkFlows
{
    using EasyOne.Components;
    using EasyOne.IDal.WorkFlow;
    using EasyOne.Model.WorkFlow;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Status
    {
        private static readonly IStatus dal = DataAccess.CreateStatus();

        private Status()
        {
        }

        public static bool Add(StatusInfo statusInfo)
        {
            return dal.Add(statusInfo);
        }

        public static void Delete(int statusCode)
        {
            if (FlowProcess.ExistStatusCodeInProcessStatusCode(statusCode))
            {
                throw new CustomException("流程码已经被使用，不可以被删除！");
            }
            dal.Delete(statusCode);
        }

        public static bool Exists(int statusId)
        {
            return dal.Exists(statusId);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static StatusInfo GetStatusById(int statusId)
        {
            if (statusId <= 0)
            {
                return new StatusInfo();
            }
            return dal.GetStatusById(statusId);
        }

        public static IList<StatusInfo> GetStatusList()
        {
            return dal.GetStatusList();
        }

        public static IList<StatusInfo> GetStatusList(int listType)
        {
            return dal.GetStatusList(listType);
        }

        public static bool Update(StatusInfo statusInfo)
        {
            return dal.Update(statusInfo);
        }
    }
}


namespace EasyOne.IDal.WorkFlow
{
    using EasyOne.Model.WorkFlow;
    using System;
    using System.Collections.Generic;

    public interface IStatus
    {
        bool Add(StatusInfo statusInfo);
        bool Delete(int statusId);
        bool Exists(int statusCode);
        int GetMaxId();
        StatusInfo GetStatusById(int statusId);
        IList<StatusInfo> GetStatusList();
        IList<StatusInfo> GetStatusList(int listType);
        bool Update(StatusInfo statusInfo);
    }
}


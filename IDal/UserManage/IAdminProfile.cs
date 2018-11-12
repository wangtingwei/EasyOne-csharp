namespace EasyOne.IDal.UserManage
{
    using EasyOne.Model.UserManage;
    using System;

    public interface IAdminProfile
    {
        void Add(AdminProfileInfo adminProileInfo);
        bool ExistsAdminName(string adminName);
        AdminProfileInfo GetAdminProfile(string adminName);
        void Update(AdminProfileInfo adminProileInfo);
    }
}


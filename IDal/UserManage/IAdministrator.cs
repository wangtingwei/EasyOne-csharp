namespace EasyOne.IDal.UserManage
{
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;

    public interface IAdministrator
    {
        bool Add(AdministratorInfo administratorInfo);
        IList<AdministratorInfo> AdminList(int startRowIndexId, int maxNumberRows, string userName);
        IList<AdministratorInfo> AdminList(int startRowIndexId, int maxNumberRows, int roleId, int listType);
        IList<AdministratorInfo> AdminList(int startRowIndexId, int maxNumberRows, int searchType, string keyword);
        bool Delete(int adminId);
        AdministratorInfo GetAdministrator(int adminId, string adminName, string userName);
        IList<AdministratorInfo> GetAdminList(int startRowIndexId, int maxNumberRows, string adminName);
        IList<AdministratorInfo> GetAdminListByOperateCode(int startRowIndexId, int maxNumberRows, int operateCode);
        int GetTotalOfAdmin();
        bool IsExist(string adminName);
        bool Update(AdministratorInfo administratorInfo);
    }
}


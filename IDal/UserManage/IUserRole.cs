namespace EasyOne.IDal.UserManage
{
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.AccessManage;

    public interface IUserRole
    {
        bool AccessCheckNodePermissions(string nodeId, OperateCode operateCode);
        bool Add(RoleInfo roleInfo);
        bool AddFieldPermissionToRoles(int roleId, int modelId, string fieldName, OperateCode operateCode);
        void AddNodePermissionToRoles(int roleId, int nodeId, OperateCode operateCode);
        void AddPermissionToRoles(int roleId, OperateCode operateCode);
        void AddPermissionToRoles(int roleId, int operateCode);
        void AddSepcialPermissionToRoles(int roleId, int specialId, OperateCode operateCode);
        bool Delete(int roleId);
        void DeleteFieldPermissionFromRoles(int roleId, int modelId, string fieldName);
        void DeleteNodePermissionFromRoles(int roleId, int nodeId);
        void DeleteNodePermissionFromRoles(int roleId, string nodeId);
        void DeleteNodePermissionFromRoles(int roleId, int nodeId, OperateCode operateCode);
        void DeletePermissionFromRoles(int roleId);
        void DeleteSpecialPermissionFromRoles(int roleId, int specialId);
        IList<RoleNodePermissionsInfo> GetAllNodePermissionsById(int roleId, int nodeId);
        IList<RoleFieldPermissionsInfo> GetFieldPermissionsById(int roleId, int modelId, string fieldName, OperateCode operateCode);
        IList<RoleModulePermissionsInfo> GetModelPermissionsById(int roleId);
        IList<RoleNodePermissionsInfo> GetNodePermissionsById(int roleId, int nodeId);
        IList<RoleNodePermissionsInfo> GetNodePermissionsByNodeId(int nodeId);
        IList<RoleModulePermissionsInfo> GetOtherModelPermissionsById(int roleId);
        string GetRoleAllNodeId(string roleId);
        RoleInfo GetRoleInfoById(int roleId);
        IList<RoleInfo> GetRoleList(int startRowIndexId, int maxNumberRows);
        IList<RoleInfo> GetRoleListByFlowId(int flowId);
        IList<RoleInfo> GetRoleListByFlowIdAndProcessId(int flowId, int processId);
        IList<string> GetRoleListByOperateCode(OperateCode operateCode);
        IList<string> GetRoleListByOperateCode(int operateCode);
        IList<string> GetRoleListByOperateCodeFieldName(OperateCode operateCode, int modelId, string fieldName);
        IList<string> GetRoleListByOperateCodeNode(OperateCode operateCode, int nodeId);
        IList<string> GetRoleListByOperateCodeSpecialId(OperateCode operateCode, int specialId);
        IList<RoleInfo> GetRoleListByRoleId(int adminId);
        IList<RoleInfo> GetRoleListNotInRole(int adminId);
        string GetRoleNodeId(string roleId, OperateCode[] arrOperateCode);
        string GetRoleNodeId(string roleId, OperateCode operateCode);
        IList<RoleSpecialPermissionsInfo> GetSpecialPermissionsByRoleId(int roleId, OperateCode operateCode);
        IList<RoleSpecialPermissionsInfo> GetSpecialPermissionsBySpecialId(int specialId);
        IList<RoleSpecialPermissionsInfo> GetSpecialPermssionList(int roleId, int specialId);
        int GetTotalOfRoles();
        bool IsExist(string roleName);
        bool Update(RoleInfo roleInfo);
    }
}


namespace EasyOne.IDal.AccessManage
{
    using EasyOne.Enumerations;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.AccessManage;

    public interface IUserPermissions
    {
        bool AddFieldPermissions(int id, int modelId, string fieldName, OperateCode operateCode, int idType);
        bool AddNodePermissions(int id, OperateCode operateCode, int nodeId, int idType);
        bool AddSpecialPermissions(int id, OperateCode operateCode, int specialId, int idType);
        bool DeleteFieldPermissions(int id, int modelId, string fieldName, int idType);
        bool DeleteNodePermissions(int id, int nodeId, OperateCode operateCode, int idType);
        bool DeleteNodePermissionsByNodeId(int nodeId, OperateCode operateCode);
        bool DeleteSpecialPermissions(int id, int specialId, OperateCode operateCode, int idType);
        bool DeleteSpecialPermissionsBySpecialId(int specialId);
        bool DeleteSpecialPermissionsBySpecialId(int specialId, OperateCode operateCode);
        IList<RoleNodePermissionsInfo> GetAllNodePermissionsById(int id, int nodeId, int idType);
        IList<RoleFieldPermissionsInfo> GetFieldPermissionsById(int id, int modelId, string fieldName, int idType);
        IList<int> GetGroupListByOperateCodeSpecialId(OperateCode operateCode, int specialId, int idType);
        IList<RoleNodePermissionsInfo> GetNodePermissionsList(int id, int nodeId, OperateCode operateCode, int idType);
        IList<int> GetRoleListByOperateCodeFieldName(OperateCode operateCode, int modelId, string fieldName, int idType);
        IList<int> GetRoleListByOperateCodeNode(OperateCode operateCode, int nodeId, int idType);
        IList<int> GetRoleListByOperateCodeNode(OperateCode operateCode, string nodeId, int idType);
        string GetRoleNodeId(int roleId, OperateCode operateCode, int idType);
        IList<RoleSpecialPermissionsInfo> GetSpecialPermissionsList(int id, int specialId, OperateCode operateCode, int idType);
    }
}


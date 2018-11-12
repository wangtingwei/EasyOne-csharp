namespace EasyOne.AccessManage
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.AccessManage;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using EasyOne.Model.AccessManage;
    using EasyOne.DalFactory;

    public sealed class UserPermissions
    {
        private static readonly IUserPermissions dal = DataAccess.CreatePermissionAccess();
        private static char[] split = new char[] { ',' };

        private UserPermissions()
        {
        }

        public static bool AccessCheck(OperateCode operateCode, int nodeId)
        {
            int idType = 0;
            if (PEContext.Current.User.UserInfo.IsInheritGroupRole)
            {
                idType = 1;
            }
            return CheckRole(dal.GetRoleListByOperateCodeNode(operateCode, nodeId, idType));
        }

        public static bool AccessCheck(OperateCode operateCode, string nodeId)
        {
            if (!DataValidator.IsValidId(nodeId))
            {
                return false;
            }
            int idType = 0;
            if (PEContext.Current.User.UserInfo.IsInheritGroupRole)
            {
                idType = 1;
            }
            return CheckRole(dal.GetRoleListByOperateCodeNode(operateCode, nodeId, idType));
        }

        public static bool AccessCheck(OperateCode operateCode, int modelId, string fieldName)
        {
            return !CheckRole(GetRoleListByOperateCodeFieldName(operateCode, modelId, fieldName));
        }

        public static bool AccessCheckSpecial(OperateCode operateCode, int specialId)
        {
            int idType = 0;
            if (PEContext.Current.User.UserInfo.IsInheritGroupRole)
            {
                idType = 1;
            }
            return CheckRole(dal.GetGroupListByOperateCodeSpecialId(operateCode, specialId, idType));
        }

        private static bool AddFieldPermission(int id, int modelId, string fieldName, OperateCode operateCode, int idType)
        {
            return dal.AddFieldPermissions(id, modelId, fieldName, operateCode, idType);
        }

        public static bool AddFieldPermission(string ids, int modelId, string fieldName, OperateCode operateCode, int idType)
        {
            if ((string.IsNullOrEmpty(fieldName) || (idType < 0)) || (modelId <= 0))
            {
                return false;
            }
            DeleteFieldPermissions(0, modelId, fieldName, idType);
            foreach (string str in ids.Split(split, StringSplitOptions.RemoveEmptyEntries))
            {
                AddFieldPermission(DataConverter.CLng(str), modelId, fieldName, operateCode, idType);
            }
            return true;
        }

        public static bool AddFieldPermissions(int id, string modelIds, string fieldNames, OperateCode operateCode, int idType)
        {
            string[] strArray = fieldNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] strArray2 = modelIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != strArray2.Length)
            {
                return false;
            }
            DeleteFieldPermissions(id, idType);
            for (int i = 0; i < strArray2.Length; i++)
            {
                AddFieldPermission(id, DataConverter.CLng(strArray2[i]), strArray[i], operateCode, idType);
            }
            return true;
        }

        private static bool AddNodePermission(int id, OperateCode operateCode, int nodeId, int idType)
        {
            return dal.AddNodePermissions(id, operateCode, nodeId, idType);
        }

        public static bool AddNodePermissions(int id, OperateCode operateCode, string nodeIds, int idType)
        {
            string[] strArray = nodeIds.Split(split, StringSplitOptions.RemoveEmptyEntries);
            dal.DeleteNodePermissions(id, -2, operateCode, idType);
            foreach (string str in strArray)
            {
                AddNodePermission(id, operateCode, DataConverter.CLng(str), idType);
            }
            return true;
        }

        public static bool AddNodePermissions(string groupIds, OperateCode operateCode, int nodeId, int idType)
        {
            dal.DeleteNodePermissions(0, nodeId, operateCode, idType);
            dal.DeleteNodePermissions(0, -1, operateCode, idType);
            if (!string.IsNullOrEmpty(groupIds))
            {
                foreach (string str in groupIds.Split(split, StringSplitOptions.RemoveEmptyEntries))
                {
                    AddNodePermission(DataConverter.CLng(str), operateCode, nodeId, idType);
                }
            }
            return true;
        }

        private static bool AddSpecialPermission(int id, OperateCode opercateCode, int specialId, int idType)
        {
            return dal.AddSpecialPermissions(id, opercateCode, specialId, idType);
        }

        public static bool AddSpecialPermissions(int id, OperateCode operateCode, string specialIds, int idType)
        {
            dal.DeleteSpecialPermissions(id, 0, OperateCode.None, idType);
            foreach (string str in specialIds.Split(split, StringSplitOptions.RemoveEmptyEntries))
            {
                AddSpecialPermission(id, operateCode, DataConverter.CLng(str), idType);
            }
            return true;
        }

        public static bool AddSpecialPermissions(string groupIds, OperateCode operateCode, int specialId, int idType)
        {
            dal.DeleteSpecialPermissions(0, specialId, operateCode, -1);
            foreach (string str in groupIds.Split(split, StringSplitOptions.RemoveEmptyEntries))
            {
                AddSpecialPermission(DataConverter.CLng(str), operateCode, specialId, idType);
            }
            return true;
        }

        private static bool CheckRole(IList<int> groupIdList)
        {
            PEContext current = PEContext.Current;
            foreach (int num in groupIdList)
            {
                if (current.User.IsInRole(num.ToString(CultureInfo.CurrentCulture)))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool DeleteFieldPermissions(int groupId, int idType)
        {
            return DeleteFieldPermissions(groupId, 0, null, idType);
        }

        public static bool DeleteFieldPermissions(int id, int modelId, string fieldName, int idType)
        {
            return dal.DeleteFieldPermissions(id, modelId, fieldName, idType);
        }

        public static void DeleteNodePermissions(int id, int idType)
        {
            dal.DeleteNodePermissions(id, -3, OperateCode.None, idType);
        }

        public static bool DeleteNodePermissionsByNodeId(int nodeId, OperateCode operateCode)
        {
            return dal.DeleteNodePermissionsByNodeId(nodeId, operateCode);
        }

        public static bool DeleteSpecialPermissions(int groupId)
        {
            return DeleteSpecialPermissions(groupId, OperateCode.None);
        }

        public static bool DeleteSpecialPermissions(int groupId, OperateCode operateCode)
        {
            return dal.DeleteSpecialPermissions(groupId, 0, operateCode, -1);
        }

        public static bool DeleteSpecialPermissionsBySpecialId(int specialId)
        {
            return dal.DeleteSpecialPermissionsBySpecialId(specialId);
        }

        public static bool DeleteSpecialPermissionsBySpecialId(int specialId, OperateCode operateCode)
        {
            return dal.DeleteSpecialPermissionsBySpecialId(specialId, operateCode);
        }

        public static IList<RoleNodePermissionsInfo> GetAllNodePermissionsById(int id, int nodeId, int idType)
        {
            if (((id > 0) || (id == -2)) && (nodeId >= 0))
            {
                return dal.GetAllNodePermissionsById(id, nodeId, idType);
            }
            return new List<RoleNodePermissionsInfo>();
        }

        public static IList<RoleFieldPermissionsInfo> GetFieldPermissionById(int modelId, string fieldName)
        {
            return GetFieldPermissionsById(0, modelId, fieldName, 1);
        }

        public static IList<RoleFieldPermissionsInfo> GetFieldPermissionsByGroupId(int groupId)
        {
            return GetFieldPermissionsById(groupId, 0, null, 0);
        }

        public static IList<RoleFieldPermissionsInfo> GetFieldPermissionsByGroupId(int groupId, int idType)
        {
            return GetFieldPermissionsById(groupId, 0, null, idType);
        }

        private static IList<RoleFieldPermissionsInfo> GetFieldPermissionsById(int id, int modelId, string fieldName, int idType)
        {
            return dal.GetFieldPermissionsById(id, modelId, fieldName, idType);
        }

        private static IList<RoleNodePermissionsInfo> GetNodePermissionList(int id, int nodeId, OperateCode operateCode, int idType)
        {
            return dal.GetNodePermissionsList(id, nodeId, operateCode, idType);
        }

        public static IList<RoleNodePermissionsInfo> GetNodePermissionsById(int nodeId, int idType)
        {
            return GetNodePermissionList(0, nodeId, OperateCode.None, idType);
        }

        public static IList<RoleNodePermissionsInfo> GetNodePermissionsById(int id, int nodeId, int idType)
        {
            if ((id <= 0) && (id != -2))
            {
                return new List<RoleNodePermissionsInfo>();
            }
            return GetNodePermissionList(id, nodeId, OperateCode.None, idType);
        }

        public static IList<int> GetRoleListByOperateCodeFieldName(OperateCode operateCode, int modelId, string fieldName)
        {
            int idType = 0;
            if (PEContext.Current.User.UserInfo.IsInheritGroupRole)
            {
                idType = 1;
            }
            return dal.GetRoleListByOperateCodeFieldName(operateCode, modelId, fieldName, idType);
        }

        public static string GetRoleNodeId(int roleId, OperateCode operateCode, int idType)
        {
            return dal.GetRoleNodeId(roleId, operateCode, idType);
        }

        public static IList<RoleSpecialPermissionsInfo> GetSpecialPermissionsByGroupId(int groupId, OperateCode operateCode)
        {
            return GetSpecialPermssionList(groupId, 0, operateCode, -1);
        }

        public static IList<RoleSpecialPermissionsInfo> GetSpecialPermissionsBySpecialId(int specialId, int idType)
        {
            return GetSpecialPermssionList(0, specialId, OperateCode.None, idType);
        }

        public static IList<RoleSpecialPermissionsInfo> GetSpecialPermssionList(int id, int specialId, OperateCode operateCode, int idType)
        {
            return dal.GetSpecialPermissionsList(id, specialId, operateCode, idType);
        }
    }
}


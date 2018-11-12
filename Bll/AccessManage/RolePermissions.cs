namespace EasyOne.AccessManage
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.AccessManage;
    using EasyOne.Model.UserManage;
    using EasyOne.DalFactory;

    public sealed class RolePermissions
    {
        private static readonly IUserRole dal = DataAccess.CreateRoles();
        private static char[] split = new char[] { ',' };

        private RolePermissions()
        {
        }
        /// <summary>
        /// 根据页面操作码判断权限
        /// </summary>
        /// <param name="operateCode"></param>
        /// <returns></returns>
        public static bool AccessCheck(OperateCode operateCode)
        {
            return (PEContext.Current.Admin.IsSuperAdmin || IsInRole(dal.GetRoleListByOperateCode(operateCode)));
        }
        /// <summary>
        /// 根据页面操作码判断权限
        /// </summary>
        /// <param name="operateCode"></param>
        /// <returns></returns>
        public static bool AccessCheck(string operateCode)
        {
            if (PEContext.Current.Admin.IsSuperAdmin)
            {
                return true;
            }
            if (Enum.IsDefined(typeof(OperateCode), operateCode))
            {
                return AccessCheck((OperateCode) Enum.Parse(typeof(OperateCode), operateCode));
            }
            return IsInRole(dal.GetRoleListByOperateCode(MD5(operateCode)));
        }

        public static bool AccessCheckFieldPermission(OperateCode operateCode, int modelId, string fieldName)
        {
            return (PEContext.Current.Admin.IsSuperAdmin || !IsInRole(dal.GetRoleListByOperateCodeFieldName(operateCode, modelId, fieldName)));
        }

        public static bool AccessCheckNodePermission(OperateCode operateCode, int nodeId)
        {
            return (PEContext.Current.Admin.IsSuperAdmin || IsInRole(dal.GetRoleListByOperateCodeNode(operateCode, nodeId)));
        }

        public static bool AccessCheckNodePermissions(string nodeId, OperateCode operateCode)
        {
            if (!DataValidator.IsValidId(nodeId))
            {
                return false;
            }
            return dal.AccessCheckNodePermissions(nodeId, operateCode);
        }

        public static bool AccessCheckSpecialPermission(OperateCode operateCode, int specialId)
        {
            return (PEContext.Current.Admin.IsSuperAdmin || IsInRole(dal.GetRoleListByOperateCodeSpecialId(operateCode, specialId)));
        }

        public static bool AddFieldPermissions(int roleId, OperateCode operateCode, string modelIds, string fieldNames)
        {
            if (!DataValidator.IsValidId(modelIds))
            {
                return false;
            }
            string[] strArray = modelIds.Split(split, StringSplitOptions.RemoveEmptyEntries);
            string[] strArray2 = fieldNames.Split(split, StringSplitOptions.RemoveEmptyEntries);
            int length = strArray.Length;
            if (strArray.Length != strArray2.Length)
            {
                return false;
            }
            if (strArray.Length == 0)
            {
                length = 1;
                strArray[0] = modelIds;
                strArray2[0] = DataSecurity.FilterBadChar(fieldNames);
            }
            else
            {
                length = strArray.Length;
            }
            for (int i = 0; i < length; i++)
            {
                if (!AddFieldPermissionToRoles(roleId, DataConverter.CLng(strArray[i]), DataSecurity.FilterBadChar(strArray2[i]), operateCode))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool AddFieldPermissionToRoles(int roleId, int modelId, string fieldName, OperateCode operateCode)
        {
            return dal.AddFieldPermissionToRoles(roleId, modelId, fieldName, operateCode);
        }

        public static bool AddFieldPermissionToRoles(string roleIdList, int modelId, string fieldName, OperateCode operateCode)
        {
            if ((modelId <= 0) || string.IsNullOrEmpty(fieldName))
            {
                return false;
            }
            DeleteFieldPermissionFormRoles(-1, modelId, fieldName);
            foreach (string str in roleIdList.Split(split, StringSplitOptions.RemoveEmptyEntries))
            {
                AddFieldPermissionToRoles(DataConverter.CLng(str), modelId, fieldName, operateCode);
            }
            return true;
        }

        public static void AddModelPermissionToRoles(int roleId, string operateCodes)
        {
            foreach (string str in operateCodes.Split(split, StringSplitOptions.RemoveEmptyEntries))
            {
                if (Enum.IsDefined(typeof(OperateCode), str))
                {
                    AddPermissionToRoles(roleId, (OperateCode) Enum.Parse(typeof(OperateCode), str, true));
                }
                else
                {
                    dal.AddPermissionToRoles(roleId, MD5(str));
                }
            }
        }

        public static void AddNodePermissionToRoles(int roleId, OperateCode operateCode, string nodeIds)
        {
            foreach (string str in nodeIds.Split(split, StringSplitOptions.RemoveEmptyEntries))
            {
                AddNodePermissionToRoles(roleId, DataConverter.CLng(str), operateCode);
            }
        }

        public static void AddNodePermissionToRoles(int roleId, int nodeId, OperateCode operateCode)
        {
            dal.AddNodePermissionToRoles(roleId, nodeId, operateCode);
        }

        public static void AddNodePermissionToRoles(string roles, int nodeId, OperateCode operateCode)
        {
            foreach (string str in roles.Split(split, StringSplitOptions.RemoveEmptyEntries))
            {
                AddNodePermissionToRoles(DataConverter.CLng(str), nodeId, operateCode);
            }
        }

        public static void AddPermissionToRoles(int roleId, OperateCode operateCode)
        {
            dal.AddPermissionToRoles(roleId, operateCode);
        }

        private static void AddSepcialPermissionToRoles(int roleId, int specialId, OperateCode operateCode)
        {
            dal.AddSepcialPermissionToRoles(roleId, specialId, operateCode);
        }

        public static void AddSepcialPermissionToRoles(int roleId, string specialIds, OperateCode operateCode)
        {
            foreach (string str in specialIds.Split(split, StringSplitOptions.RemoveEmptyEntries))
            {
                AddSepcialPermissionToRoles(roleId, DataConverter.CLng(str), operateCode);
            }
        }

        public static void AddSepcialPermissionToRoles(string roleIds, int specialId, OperateCode operateCode)
        {
            if ((specialId > 0) && !string.IsNullOrEmpty(roleIds))
            {
                foreach (string str in roleIds.Split(split, StringSplitOptions.RemoveEmptyEntries))
                {
                    AddSepcialPermissionToRoles(DataConverter.CLng(str), specialId, operateCode);
                }
            }
        }

        public static void BusinessAccessCheck(OperateCode operateCode)
        {
            if (!AccessCheck(operateCode))
            {
                throw new CustomException(PEExceptionType.ExceedAuthority);
            }
        }

        public static void BusinessAccessCheck(OperateCode operateCode, int nodeId)
        {
            if (!AccessCheckNodePermission(operateCode, nodeId))
            {
                throw new CustomException(PEExceptionType.ExceedAuthority);
            }
        }

        public static void BusinessAccessCheck(OperateCode operateCode, int modelId, string fieldName)
        {
            if (!AccessCheckFieldPermission(operateCode, modelId, fieldName))
            {
                throw new CustomException(PEExceptionType.ExceedAuthority);
            }
        }

        public static void BusinesssAccessCheckSpecial(OperateCode operateCode, int specialId)
        {
            if (!AccessCheck(OperateCode.SpecialManage) && !AccessCheckSpecialPermission(operateCode, specialId))
            {
                throw new CustomException(PEExceptionType.ExceedAuthority);
            }
        }

        private static void DeleteFieldPermissionFormRoles(int roleId, int modelId, string fieldName)
        {
            dal.DeleteFieldPermissionFromRoles(roleId, modelId, fieldName);
        }

        public static void DeleteFieldPermissionFromRoles(int roleId)
        {
            DeleteFieldPermissionFormRoles(roleId, 0, null);
        }

        public static void DeleteNodePermissionFromRoles(int roleId, int nodeId)
        {
            dal.DeleteNodePermissionFromRoles(roleId, nodeId);
        }

        public static void DeleteNodePermissionFromRoles(int roleId, int nodeId, OperateCode operateCode)
        {
            dal.DeleteNodePermissionFromRoles(roleId, nodeId, operateCode);
        }

        public static void DeletePermissionFromRoles(int roleId)
        {
            dal.DeletePermissionFromRoles(roleId);
        }

        public static void DeleteSpecialPermissionFromRoles(int roleId)
        {
            if (roleId > 0)
            {
                dal.DeleteSpecialPermissionFromRoles(roleId, 0);
            }
        }

        public static void DeleteSpecialPermissionFromRoles(int roleId, int specialId)
        {
            dal.DeleteSpecialPermissionFromRoles(roleId, specialId);
        }

        public static IList<RoleNodePermissionsInfo> GetAllNodePermissionsById(int roleId, int nodeId)
        {
            return dal.GetAllNodePermissionsById(roleId, nodeId);
        }

        private static IList<RoleFieldPermissionsInfo> GetFieldPermission(int roleId, int modelId, string fieldName, OperateCode operateCode)
        {
            return dal.GetFieldPermissionsById(roleId, modelId, fieldName, operateCode);
        }

        public static IList<RoleFieldPermissionsInfo> GetFieldPermissionByModelId(int modelId, string fieldName)
        {
            if ((modelId > 0) && !string.IsNullOrEmpty(fieldName))
            {
                return GetFieldPermission(-1, modelId, fieldName, OperateCode.None);
            }
            return new List<RoleFieldPermissionsInfo>();
        }

        public static IList<RoleFieldPermissionsInfo> GetFieldPermissionsById(int id)
        {
            return GetFieldPermission(id, 0, null, OperateCode.None);
        }

        public static IList<RoleModulePermissionsInfo> GetModelPermissionsById(int roleId)
        {
            return dal.GetModelPermissionsById(roleId);
        }

        public static IList<RoleNodePermissionsInfo> GetNodePermissionsById(int roleId, int nodeId)
        {
            return dal.GetNodePermissionsById(roleId, nodeId);
        }

        public static IList<RoleNodePermissionsInfo> GetNodePermissionsByNodeId(int nodeId)
        {
            return dal.GetNodePermissionsByNodeId(nodeId);
        }

        public static IList<RoleModulePermissionsInfo> GetOtherModelPermissionsById(int roleId)
        {
            return dal.GetOtherModelPermissionsById(roleId);
        }

        public static string GetRoleAllNodeId(string roleId)
        {
            if (!DataValidator.IsValidId(roleId))
            {
                return string.Empty;
            }
            return dal.GetRoleAllNodeId(roleId);
        }

        public static IList<RoleInfo> GetRoleListByFlowId(int flowId)
        {
            return dal.GetRoleListByFlowId(flowId);
        }

        public static IList<RoleInfo> GetRoleListByFlowIdAndProcessId(int flowId, int processId)
        {
            return dal.GetRoleListByFlowIdAndProcessId(flowId, processId);
        }

        public static string GetRoleNodeId(string roleId, OperateCode[] arrOperateCode)
        {
            if (!DataValidator.IsValidId(roleId))
            {
                return string.Empty;
            }
            return dal.GetRoleNodeId(roleId, arrOperateCode);
        }

        public static string GetRoleNodeId(string roleId, OperateCode operateCode)
        {
            if (!DataValidator.IsValidId(roleId))
            {
                return string.Empty;
            }
            return dal.GetRoleNodeId(roleId, operateCode);
        }

        public static IList<RoleSpecialPermissionsInfo> GetSpecialPermissionsByRoleId(int roleId, OperateCode operateCode)
        {
            return dal.GetSpecialPermissionsByRoleId(roleId, operateCode);
        }

        public static IList<RoleSpecialPermissionsInfo> GetSpecialPermissionsBySpecialId(int specialId)
        {
            return dal.GetSpecialPermissionsBySpecialId(specialId);
        }

        public static IList<RoleSpecialPermissionsInfo> GetSpecialPermssionList(int roleId, int specialId)
        {
            return dal.GetSpecialPermssionList(roleId, specialId);
        }
        /// <summary>
        /// 判断用户在否在所属的权限范围内
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <returns></returns>
        private static bool IsInRole(IList<string> roleIdList)
        {
            PEContext current = PEContext.Current;
            foreach (string str in roleIdList)
            {
                if (current.Admin.IsInRole(str.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public static int MD5(string operateCode)
        {
            int num = DataConverter.CLng(operateCode);
            if ((num != 0) && !(operateCode.Substring(0, 1) != "9"))
            {
                return num;
            }
            return StringHelper.MD5D(operateCode);
        }

        public static IList<RoleNodePermissionsInfo> PermissionsNodeId(int nodeId)
        {
            return GetNodePermissionsById(-1, nodeId);
        }
    }
}


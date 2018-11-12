namespace EasyOne.Contents
{
    using EasyOne.Common;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EasyOne.DalFactory;

    public sealed class PermissionContent
    {
        private static readonly IPermissionContent dal = DataAccess.CreateContentPermission();

        private PermissionContent()
        {
        }

        public static bool Add(ContentPermissionInfo contentPermissionInfo)
        {
            return dal.Add(contentPermissionInfo);
        }

        public static bool BatchUpdate(ContentPermissionInfo contentPermissionInfo, string itemId, Dictionary<string, bool> checkItem, int batchType)
        {
            if (!DataValidator.IsValidId(itemId))
            {
                return false;
            }
            if (!checkItem.ContainsValue(true))
            {
                return false;
            }
            StringBuilder sb = new StringBuilder();
            int generalId = 0;
            if (batchType == 1)
            {
                foreach (CommonModelInfo info in ContentManage.GetCommonModelInfoList(itemId))
                {
                    if (!dal.Exists(info.GeneralId))
                    {
                        contentPermissionInfo.GeneralId = info.GeneralId;
                        dal.Add(contentPermissionInfo);
                    }
                    else
                    {
                        StringHelper.AppendString(sb, info.GeneralId.ToString());
                    }
                }
            }
            else
            {
                foreach (string str in itemId.Split(new char[] { ',' }))
                {
                    generalId = DataConverter.CLng(str);
                    if (generalId != 0)
                    {
                        if (!dal.Exists(generalId))
                        {
                            contentPermissionInfo.GeneralId = generalId;
                            dal.Add(contentPermissionInfo);
                        }
                        else
                        {
                            StringHelper.AppendString(sb, str);
                        }
                    }
                }
            }
            if (sb.Length > 0)
            {
                return dal.BatchUpdate(contentPermissionInfo, sb.ToString(), checkItem);
            }
            return true;
        }

        public static void Delete(int generalId)
        {
            dal.Delete(generalId);
        }

        public static void Delete(string generalId)
        {
            if (DataValidator.IsValidId(generalId))
            {
                dal.Delete(generalId);
            }
        }

        public static bool Exists(int generalId)
        {
            return dal.Exists(generalId);
        }

        public static ContentPermissionInfo GetContentPermissionInfoById(int generalId)
        {
            return dal.GetContentPermissionInfoById(generalId);
        }

        public static bool Update(ContentPermissionInfo contentPermissionInfo)
        {
            return dal.Update(contentPermissionInfo);
        }
    }
}


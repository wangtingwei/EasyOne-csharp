namespace EasyOne.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using EasyOne.DalFactory;

    public sealed class Special
    {
        private static readonly ISpecial dal = DataAccess.CreateSpecial();

        private Special()
        {
        }

        public static bool AddContentToSpecialInfoByGeneralId(string specialIds, string generalIds)
        {
            bool flag2 = true;
            if (!DataValidator.IsValidId(specialIds))
            {
                return false;
            }
            if (!DataValidator.IsValidId(generalIds))
            {
                return false;
            }
            string[] strArray = specialIds.Split(new char[] { ',' });
            string[] strArray2 = generalIds.Split(new char[] { ',' });
            foreach (string str in strArray)
            {
                foreach (string str2 in strArray2)
                {
                    int specialId = DataConverter.CLng(str);
                    int generalId = DataConverter.CLng(str2);
                    if (!ExistInSpecialInfos(specialId, generalId) && !dal.AddToSpecialInfos(specialId, generalId))
                    {
                        flag2 = false;
                        break;
                    }
                }
            }
            return flag2;
        }

        public static bool AddContentToSpecialInfos(int specialId, string generalIds)
        {
            if (!DataValidator.IsValidId(generalIds))
            {
                return false;
            }
            foreach (string str in generalIds.Split(new char[] { ',' }))
            {
                int generalId = DataConverter.CLng(str);
                if (!ExistInSpecialInfos(specialId, generalId) && !dal.AddToSpecialInfos(specialId, generalId))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool AddContentToSpecialInfos(string specialIds, string specialInfos)
        {
            bool flag2 = true;
            if (!DataValidator.IsValidId(specialIds))
            {
                return false;
            }
            if (!DataValidator.IsValidId(specialInfos))
            {
                return false;
            }
            string[] strArray = specialIds.Split(new char[] { ',' });
            string[] strArray2 = specialInfos.Split(new char[] { ',' });
            foreach (string str in strArray)
            {
                foreach (string str2 in strArray2)
                {
                    int specialId = DataConverter.CLng(str);
                    int generalId = DataConverter.CLng(GetGeneralIdBySpecialInfoId(str2));
                    if (!ExistInSpecialInfos(specialId, generalId) && !dal.AddToSpecialInfos(specialId, generalId))
                    {
                        flag2 = false;
                        break;
                    }
                }
            }
            return flag2;
        }

        public static bool AddSpecial(SpecialInfo specialInfo)
        {
            RolePermissions.AccessCheck(OperateCode.SpecialManage);
            return dal.AddSpecial(specialInfo);
        }

        public static bool AddSpecialCategory(SpecialCategoryInfo specialCategoryInfo)
        {
            return dal.AddSpecialCategory(specialCategoryInfo);
        }

        public static bool AddToSpecialInfos(string specialIds, int generalId)
        {
            if (!DataValidator.IsValidId(specialIds))
            {
                return false;
            }
            string[] strArray = specialIds.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (!AddContentToSpecialInfos(DataConverter.CLng(strArray[i]), generalId.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        private static void DeleteFolder(int specialId)
        {
            SpecialInfo specialInfoById = GetSpecialInfoById(specialId);
            SpecialCategoryInfo specialCategoryInfoById = GetSpecialCategoryInfoById(specialInfoById.SpecialCategoryId);
            string str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + VirtualPathUtility.AppendTrailingSlash(specialCategoryInfoById.SpecialCategoryDir) + specialInfoById.SpecialDir;
            string file = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, str);
            if (FileSystemObject.IsExist(file, FsoMethod.Folder))
            {
                FileSystemObject.Delete(file, FsoMethod.Folder);
            }
        }

        public static bool DeleteSpecialById(int specialId)
        {
            RolePermissions.AccessCheck(OperateCode.SpecialManage);
            DeleteFolder(specialId);
            DeleteSpecialInfoBySpecialId(specialId);
            return dal.DeleteSpecial(specialId);
        }

        public static bool DeleteSpecialCategoryById(int specialCategoryId)
        {
            RolePermissions.AccessCheck(OperateCode.SpecialManage);
            DeleteSpecialCategoryFolder(specialCategoryId);
            foreach (SpecialInfo info in GetSpecialList(specialCategoryId))
            {
                DeleteSpecialById(info.SpecialId);
            }
            return dal.DeleteSpecialCategoryById(specialCategoryId);
        }

        private static void DeleteSpecialCategoryFolder(int specialCategoryId)
        {
            SpecialCategoryInfo specialCategoryInfoById = GetSpecialCategoryInfoById(specialCategoryId);
            string str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + specialCategoryInfoById.SpecialCategoryDir;
            string file = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, str);
            if (FileSystemObject.IsExist(file, FsoMethod.Folder))
            {
                FileSystemObject.Delete(file, FsoMethod.Folder);
            }
        }

        public static bool DeleteSpecialIdInSpecialInfos(string specialIds)
        {
            if (!DataValidator.IsValidId(specialIds))
            {
                return false;
            }
            return dal.DeleteSpecialIdInSpecialInfos(specialIds);
        }

        public static bool DeleteSpecialInfoById(string specialInfoIds)
        {
            if (!DataValidator.IsValidId(specialInfoIds))
            {
                return false;
            }
            return dal.DeleteSpecialInfoById(specialInfoIds);
        }

        public static bool DeleteSpecialInfoById(string specialInfoIds, int specialId)
        {
            if (!DataValidator.IsValidId(specialInfoIds))
            {
                return false;
            }
            return dal.DeleteSpecialInfoById(specialInfoIds, specialId);
        }

        public static bool DeleteSpecialInfoBySpecialId(int specialId)
        {
            return dal.DeleteSpecialInfoBySpecialId(specialId);
        }

        public static bool DeleteSpecialInfos(int generalId)
        {
            return dal.DeleteSpecialInfos(generalId);
        }

        public static bool ExistInSpecialInfos(int generalId)
        {
            return dal.ExistInSpecialInfos(generalId);
        }

        public static bool ExistInSpecialInfos(int specialId, int generalId)
        {
            return dal.ExistInSpecialInfos(specialId, generalId);
        }

        public static bool ExistsSpecialCategoryIdInSpecials(int specialCategoryId)
        {
            return dal.ExistsSpecialCategoryIdInSpecials(specialCategoryId);
        }

        public static bool ExistsSpecialCategoryName(string specialCategoryName)
        {
            return dal.ExistsSpecialCategoryName(specialCategoryName);
        }

        public static bool ExistsSpecialDir(string specialDir)
        {
            return dal.ExistsSpecialDir(specialDir);
        }

        public static bool ExistsSpecialName(string specialName)
        {
            return dal.ExistsSpecialName(specialName);
        }

        public static int GetCountSpecial(int specialCategoryId)
        {
            return dal.GetCountSpecial(specialCategoryId);
        }

        public static string GetGeneralIdBySpecialId(string specialId)
        {
            return dal.GetGeneralIdBySpecialId(specialId);
        }

        public static string GetGeneralIdBySpecialInfoId(string specialInfoId)
        {
            string generalIdBySpecialInfoId = "";
            if (DataValidator.IsValidId(specialInfoId))
            {
                generalIdBySpecialInfoId = dal.GetGeneralIdBySpecialInfoId(specialInfoId);
            }
            return generalIdBySpecialInfoId;
        }

        public static int GetSpecialByIdCopySpecial(int specialId)
        {
            if (specialId > 0)
            {
                SpecialInfo specialInfoById = GetSpecialInfoById(specialId);
                specialInfoById.SpecialName = StringHelper.CopyString(specialInfoById.SpecialName);
                specialInfoById.SpecialIdentifier = StringHelper.CopyStringNum(specialInfoById.SpecialIdentifier);
                specialInfoById.SpecialDir = StringHelper.CopyStringNum(specialInfoById.SpecialDir);
                while (ExistsSpecialName(specialInfoById.SpecialName))
                {
                    specialInfoById.SpecialName = StringHelper.CopyString(specialInfoById.SpecialName);
                }
                while (ExistsSpecialDir(specialInfoById.SpecialDir))
                {
                    specialInfoById.SpecialDir = StringHelper.CopyStringNum(specialInfoById.SpecialDir);
                    specialInfoById.SpecialIdentifier = StringHelper.CopyStringNum(specialInfoById.SpecialIdentifier);
                }
                if (!AddSpecial(specialInfoById))
                {
                    return 0;
                }
                int maxSpecialId = dal.GetMaxSpecialId();
                if (maxSpecialId > 0)
                {
                    return maxSpecialId;
                }
            }
            return 0;
        }

        public static SpecialCategoryInfo GetSpecialCategoryInfoById(int specialCategoryId)
        {
            return dal.GetSpecialCategoryInfoById(specialCategoryId);
        }

        public static IList<SpecialCategoryInfo> GetSpecialCategoryList()
        {
            return dal.GetSpecialCategoryList();
        }

        public static IList<SpecialCategoryInfo> GetSpecialCategoryList(string specialCategoryId)
        {
            if (!DataValidator.IsValidId(specialCategoryId))
            {
                return new List<SpecialCategoryInfo>();
            }
            return dal.GetSpecialCategoryList(specialCategoryId);
        }

        public static SpecialInfo GetSpecialInfoById(int specialId)
        {
            return dal.GetSpecialInfoById(specialId);
        }

        public static string GetSpecialInfoIds(int generalId)
        {
            return dal.GetSpecialInfoIds(generalId);
        }

        public static IList<SpecialInfo> GetSpecialList()
        {
            return dal.GetSpecialList();
        }

        public static IList<SpecialInfo> GetSpecialList(int specialCategoryId)
        {
            return dal.GetSpecialList(specialCategoryId);
        }

        public static IList<SpecialInfo> GetSpecialList(string specialId)
        {
            if (!DataValidator.IsValidId(specialId))
            {
                return new List<SpecialInfo>();
            }
            return dal.GetSpecialList(specialId);
        }

        public static IList<SpecialInfo> GetSpecialList(int startRowIndexId, int maxNumberRows, int specialCategoryId, int listType)
        {
            return dal.GetSpecialList(startRowIndexId, maxNumberRows, specialCategoryId, listType);
        }

        public static IList<SpecialTree> GetSpecialTree()
        {
            IList<SpecialCategoryInfo> specialCategoryList = GetSpecialCategoryList();
            IList<SpecialTree> list2 = new List<SpecialTree>();
            foreach (SpecialCategoryInfo info in specialCategoryList)
            {
                IList<SpecialInfo> specialList = GetSpecialList(info.SpecialCategoryId);
                SpecialTree tree = new SpecialTree();
                tree.Id = info.SpecialCategoryId;
                tree.Name = info.SpecialCategoryName;
                if (specialList.Count > 0)
                {
                    tree.TreeLineType = 5;
                }
                else
                {
                    tree.TreeLineType = 4;
                }
                tree.IsSpecialCategory = true;
                int num = 0;
                list2.Add(tree);
                foreach (SpecialInfo info2 in specialList)
                {
                    SpecialTree tree2 = new SpecialTree();
                    tree2.Id = info2.SpecialId;
                    tree2.Name = info2.SpecialName;
                    if (num >= 0)
                    {
                        tree2.TreeLineType = 6;
                    }
                    num++;
                    if (num == specialList.Count)
                    {
                        tree2.TreeLineType = 7;
                    }
                    tree2.IsSpecialCategory = false;
                    list2.Add(tree2);
                }
            }
            SpecialTree item = new SpecialTree();
            item.Name = "所有专题";
            item.Id = -1;
            item.IsSpecialCategory = false;
            item.TreeLineType = 0;
            list2.Insert(0, item);
            return list2;
        }

        public static int GetTotalOfSpecial(int specialCategoryId, int listType)
        {
            return dal.GetTotalOfSpecial();
        }

        public static void MoveSpecialInfoBySpecialId(string sourceSpecialId, int targetSpecialId)
        {
            if (!DataValidator.IsValidId(sourceSpecialId))
            {
                throw new CustomException("错误的SourceSpecialID！");
            }
            dal.MoveSpecialInfoBySpecialId(sourceSpecialId, targetSpecialId);
        }

        public static void OrderSpecial(IList<SpecialInfo> list)
        {
            List<SpecialInfo> list2 = (List<SpecialInfo>) list;
            list2.Sort();
            foreach (SpecialInfo info in list2)
            {
                UpdateSpecial(info);
            }
        }

        public static void OrderSpecialCategory(IList<SpecialCategoryInfo> list)
        {
            List<SpecialCategoryInfo> list2 = (List<SpecialCategoryInfo>) list;
            list2.Sort();
            foreach (SpecialCategoryInfo info in list2)
            {
                UpdateSpecialCategory(info);
            }
        }

        public static bool SpecialBatchSet(SpecialInfo specialInfo, string specialIds, Dictionary<string, bool> checkItem)
        {
            if (!DataValidator.IsValidId(specialIds))
            {
                return false;
            }
            if (!checkItem.ContainsValue(true))
            {
                throw new CustomException("没有选择需要批量设置的选项！");
            }
            return dal.SpecialBatchSet(specialInfo, specialIds, checkItem);
        }

        public static string TreeLine(int type)
        {
            string str = "";
            str = HttpContext.Current.Request.ApplicationPath.Equals("/") ? string.Empty : HttpContext.Current.Request.ApplicationPath;
            str = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + str;
            string str2 = "<img src='" + str + "/Admin/images/Node/tree_line1.gif' width='17' height='16' valign='abvmiddle'>";
            string str3 = "<img src='" + str + "/Admin/images/Node/tree_line2.gif' width='17' height='16' valign='abvmiddle'>";
            string str4 = "<img src='" + str + "/Admin/images/Node/tree_line3.gif' width='17' height='16' valign='abvmiddle'>";
            string str5 = "<img src='" + str + "/Admin/images/Node/tree_folder3.gif' width='15' height='15' valign='abvmiddle'>";
            string str6 = "<img src='" + str + "/Admin/images/Node/tree_folder4.gif' width='15' height='15' valign='abvmiddle'>";
            string str7 = string.Empty;
            switch (type)
            {
                case 1:
                    return (str2 + "&nbsp" + str6);

                case 2:
                    return str3;

                case 3:
                    return str4;

                case 4:
                    return str5;

                case 5:
                    return str6;

                case 6:
                    return (str2 + "&nbsp" + str5);

                case 7:
                    return (str3 + "&nbsp" + str5);
            }
            return str7;
        }

        public static int UniteSpecial(int specialId, int targetSpecialId)
        {
            int num = 0;
            if (specialId <= 0)
            {
                return 1;
            }
            if (targetSpecialId <= 0)
            {
                return 2;
            }
            if (specialId == targetSpecialId)
            {
                return 3;
            }
            string[] strArray = GetGeneralIdBySpecialId(specialId.ToString()).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                int generalId = DataConverter.CLng(strArray[i]);
                if (!ExistInSpecialInfos(targetSpecialId, generalId))
                {
                    dal.AddToSpecialInfos(targetSpecialId, generalId);
                }
            }
            if (!DeleteSpecialById(specialId))
            {
                num = 5;
            }
            return num;
        }

        public static bool UpdateNeedCreateHtml(string arrSpecialId, bool needCreateHtml)
        {
            if (!DataValidator.IsValidId(arrSpecialId))
            {
                return false;
            }
            return dal.UpdateNeedCreateHtml(arrSpecialId, needCreateHtml);
        }

        public static bool UpdateSpecial(SpecialInfo specialInfo)
        {
            return dal.UpdateSpecial(specialInfo);
        }

        public static bool UpdateSpecialCategory(SpecialCategoryInfo specialCategoryInfo)
        {
            return dal.UpdateSpecialCategory(specialCategoryInfo);
        }

        public static bool UpdateSpecialCategoryNeedCreateHtml(string arrSpecialCategoryId, bool needCreateHtml)
        {
            if (!DataValidator.IsValidId(arrSpecialCategoryId))
            {
                return false;
            }
            return dal.UpdateSpecialCategoryNeedCreateHtml(arrSpecialCategoryId, needCreateHtml);
        }

        public static void UpdateSpecialIdByGeneralId(int specialId, int sourceSpecialId, string specialInfoId)
        {
            if (!DataValidator.IsValidId(specialInfoId))
            {
                throw new CustomException("错误的generalId！");
            }
            dal.UpdateSpecialIdByGeneralId(specialId, sourceSpecialId, specialInfoId);
        }

        public static bool UpdateSpecialInfos(string specialIds, int generalId)
        {
            bool flag = true;
            if (!ExistInSpecialInfos(generalId))
            {
                return flag;
            }
            if (DeleteSpecialInfos(generalId))
            {
                if (!string.IsNullOrEmpty(specialIds))
                {
                    flag = AddToSpecialInfos(specialIds, generalId);
                }
                return flag;
            }
            return false;
        }
    }
}


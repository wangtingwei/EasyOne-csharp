namespace EasyOne.UserManage
{
    using EasyOne.AccessManage;
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.Crm;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using EasyOne.DalFactory;

    public sealed class Users
    {
        private static readonly IUsers dal = DataAccess.CreateUsers();

        private Users()
        {
        }

        public static bool Add(UserInfo usersInfo, ContacterInfo contacterInfo)
        {
            bool flag = false;
            if (dal.Add(usersInfo))
            {
                flag = SaveContacter(contacterInfo);
            }
            return flag;
        }

        public static bool AddPoint(int infoPoint, string userName)
        {
            return dal.AddPoint(infoPoint, DataSecurity.FilterBadChar(userName));
        }

        public static bool AddToAdminCompany(string userName)
        {
            return dal.AddToAdminCompany(userName);
        }

        public static bool AgreeJoinCompany(string userName, int companyClientId)
        {
            return dal.AgreeJoinCompany(userName, companyClientId);
        }

        public static void BatchAuditing(string id)
        {
            if (DataValidator.IsValidId(id))
            {
                dal.BatchAuditing(id);
            }
        }

        public static bool BatchLock(int id)
        {
            return dal.LockUser(id);
        }

        public static void BatchLock(string id)
        {
            if (DataValidator.IsValidId(id))
            {
                dal.BatchLock(id);
            }
        }

        public static void BatchNormal(string id)
        {
            if (DataValidator.IsValidId(id))
            {
                dal.BatchUpdateUserStatus(id, UserStatus.None);
            }
        }

        public static void BatchUnlock(string id)
        {
            if (DataValidator.IsValidId(id))
            {
                dal.BatchUnlock(id);
            }
        }

        public static string ChangeStateSendMessageToUser(string userName, string sendContent, string title, string state)
        {
            if (string.IsNullOrEmpty(sendContent))
            {
                return "";
            }
            string str = IsUserMobile(userName);
            if (!string.IsNullOrEmpty(str))
            {
                return str;
            }
            string str2 = state;
            if (str2 != null)
            {
                if (!(str2 == "99"))
                {
                    if (str2 == "0")
                    {
                        state = "有待审核";
                    }
                    else if (str2 == "-1")
                    {
                        state = "草稿";
                    }
                    else if (str2 == "-2")
                    {
                        state = "退稿";
                    }
                    else if (str2 == "-3")
                    {
                        state = "删除";
                    }
                    else if (str2 == "100")
                    {
                        state = "归档";
                    }
                }
                else
                {
                    state = "审核通过";
                }
            }
            sendContent = sendContent.Replace("{$UserName}", userName);
            sendContent = sendContent.Replace("{$Title}", title);
            sendContent = sendContent.Replace("{$State}", state);
            return ("<li>" + SmsMessage.SendMessage(userName, sendContent, "0", "", PEContext.Current.Admin.AdminName) + "</li>");
        }

        public static double CheckValidNum(DateTime? endTime)
        {
            double days = 0.0;
            if (endTime.HasValue)
            {
                TimeSpan span = endTime.Value - DateTime.Now.Date;
                days = span.Days;
                if (days < 0.0)
                {
                    return 0.0;
                }
                if ((days == 0.0) && (span.Minutes > 0))
                {
                    days = 0.5;
                }
            }
            return days;
        }

        public static bool Delete(int userId)
        {
            if (dal.GetUserById(userId).UserName == PEContext.Current.Admin.UserName)
            {
                throw new CustomException("禁止自己删除自己！");
            }
            return dal.Delete(userId);
        }

        public static bool Delete(string id)
        {
            if (!DataValidator.IsValidId(id))
            {
                return false;
            }
            int userId = 0;
            foreach (string str in id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                userId = DataConverter.CLng(str);
                UserInfo userById = GetUserById(userId);
                if (Delete(userId))
                {
                    DeleteUserRelation(userId, userById.UserName);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static bool DeleteCompany(int companyId)
        {
            return dal.DeleteCompany(companyId);
        }

        public static bool DeleteFriendGroup(string userName, int friendGroupId)
        {
            UserFriend.Delete(userName, friendGroupId);
            return UpdateFriendGroupName(userName, friendGroupId, string.Empty);
        }

        public static void DeleteUserRelation(int id, string userName)
        {
            UserPermissions.DeleteFieldPermissions(id, 0);
            UserPermissions.DeleteNodePermissions(id, -2);
            UserPermissions.DeleteSpecialPermissions(id);
            UserValidLog.Delete(userName);
            UserPointLog.Delete(userName);
            Contacter.DeleteByUserName(userName);
        }

        public static bool Exists(string userName)
        {
            return dal.Exists(userName);
        }

        public static bool ExistsUserByClientId(int clientId)
        {
            return dal.ExistsUserByClientId(clientId);
        }

        public static int ExportDataToAccess(string databaseName, int groupId)
        {
            return dal.ExportDataToAccess(databaseName, groupId);
        }

        public static IList<UserInfo> GetAllUsers(int startRowIndexId, int maxNumberRows, int groupId, string keyword, int listType)
        {
            IList<UserInfo> list = dal.GetAllUsers(startRowIndexId, maxNumberRows, groupId, DataSecurity.FilterBadChar(keyword), DataConverter.CLng(listType));
            IList<UserInfo> list2 = new List<UserInfo>();
            foreach (UserInfo info in list)
            {
                GetGroupInfo(info);
                list2.Add(info);
            }
            return list2;
        }

        public static int GetAuditingCompanyMemberCount(int companyId)
        {
            return dal.GetAuditingCompanyMemberCount(companyId);
        }

        public static DataTable GetFriendGroup(string userName)
        {
            UserInfo usersByUserName = GetUsersByUserName(userName);
            if (usersByUserName.IsNull)
            {
                return null;
            }
            if (string.IsNullOrEmpty(usersByUserName.UserFriendGroup))
            {
                return null;
            }
            string[] strArray = usersByUserName.UserFriendGroup.Split(new char[] { '$' });
            DataTable table = new DataTable();
            table.Columns.Add("FriendGroupName");
            table.Columns.Add("FriendGroupID");
            for (int i = 0; i < strArray.Length; i++)
            {
                DataRow row = table.NewRow();
                row["FriendGroupName"] = strArray[i];
                row["FriendGroupID"] = i;
                table.Rows.Add(row);
            }
            return table;
        }

        private static void GetGroupInfo(UserInfo userInfo)
        {
            UserGroupsInfo userGroupById = UserGroups.GetUserGroupById(userInfo.GroupId);
            userInfo.GroupName = userGroupById.GroupName;
            if (userInfo.IsInheritGroupRole)
            {
                userInfo.UserSetting = userGroupById.GroupSetting;
            }
        }

        public static IList<UserInfo> GetListByCompanyId(int companyId)
        {
            IList<UserInfo> list = new List<UserInfo>();
            IList<UserInfo> listByCompanyId = dal.GetListByCompanyId(companyId);
            foreach (UserInfo info in listByCompanyId)
            {
                GetGroupInfo(info);
                list.Add(info);
            }
            return listByCompanyId;
        }

        public static int GetNumberOfUsersOnline(int groupId, string keyword, int listType)
        {
            return dal.GetNumberOfUsers();
        }

        public static UserInfo GetUserById(int userId)
        {
            UserInfo userById = dal.GetUserById(userId);
            GetGroupInfo(userById);
            return userById;
        }

        public static IList<string> GetUserMailByGroupId(int groupId)
        {
            return dal.GetUserMailByGroupId(groupId);
        }

        public static IList<string[]> GetUserNameAndEmailList(int type, string value)
        {
            return dal.GetUserNameAndEmailList(type, DataSecurity.FilterBadChar(value));
        }

        public static string GetUserNameByClientId(int clientId)
        {
            return dal.GetUserNameByClientId(clientId);
        }

        public static IList<string> GetUserNameList(int startRowIndexId, int maxiNumRows, int searchType, string keyword)
        {
            return dal.GetUserNameList(startRowIndexId, maxiNumRows, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static int GetUserNameListTotal(int searchType, string keyword)
        {
            return dal.GetUserNameListTotal(searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static UserInfo GetUsersByEmail(string email)
        {
            UserInfo usersByEmail = dal.GetUsersByEmail(email);
            GetGroupInfo(usersByEmail);
            return usersByEmail;
        }

        public static IList<UserInfo> GetUsersByGroupId(string groupId)
        {
            IList<UserInfo> list = new List<UserInfo>();
            if (DataValidator.IsValidId(groupId))
            {
                foreach (UserInfo info in dal.GetUsersByGroupId(groupId))
                {
                    GetGroupInfo(info);
                    list.Add(info);
                }
            }
            return list;
        }

        public static IList<UserInfo> GetUsersByPost()
        {
            IList<UserInfo> list = new List<UserInfo>();
            foreach (UserInfo info in dal.GetUserByPost())
            {
                GetGroupInfo(info);
                list.Add(info);
            }
            return list;
        }

        public static UserInfo GetUsersByUserName(string userName)
        {
            UserInfo usersByUserName = dal.GetUsersByUserName(userName);
            GetGroupInfo(usersByUserName);
            return usersByUserName;
        }

        public static string GetValidNum(DateTime? endTime)
        {
            double num = CheckValidNum(endTime);
            if (num >= 1.0)
            {
                return ("<span style='color:blue'>" + num.ToString() + "</span>");
            }
            if ((num > 0.0) && (num < 1.0))
            {
                return "<span style='color:blue'><1</span>";
            }
            return "<span style='color:red'>0</span>";
        }

        public static string IsUserMobile(string userName)
        {
            ContacterInfo contacterByUserName = Contacter.GetContacterByUserName(userName);
            if (contacterByUserName.IsNull)
            {
                return ("<li>" + userName + "会员没有填写联系方式，所以没有发送手机短信！</li>");
            }
            string mobile = contacterByUserName.Mobile;
            if (string.IsNullOrEmpty(mobile))
            {
                mobile = contacterByUserName.Phs;
            }
            if (string.IsNullOrEmpty(mobile))
            {
                return ("<li>" + userName + "会员没有填写手机号，所以没有发送手机短信！</li>");
            }
            return "";
        }

        public static bool MinusPoint(int infoPoint, string userName)
        {
            return dal.MinusPoint(infoPoint, DataSecurity.FilterBadChar(userName));
        }

        public static bool MoveBetweenUserId(int startUserId, int endUserId, int groupId)
        {
            return dal.MoveBetweenUserId(startUserId, endUserId, groupId);
        }

        public static bool MoveByGroups(string groupId, int targetGroupId)
        {
            if (!DataValidator.IsValidId(groupId))
            {
                return false;
            }
            return dal.MoveByGroups(groupId, targetGroupId);
        }

        public static bool MoveByUserName(string userName, int groupId)
        {
            userName = userName.Replace("'", "");
            return dal.MoveByUserName(userName, groupId);
        }

        public static bool MoveByUsers(string userId, int groupId)
        {
            if (!DataValidator.IsValidId(userId))
            {
                return false;
            }
            return dal.MoveByUsers(userId, groupId);
        }

        public static bool RemoveFromAdminCompany(string userName)
        {
            return dal.RemoveFromAdminCompany(userName);
        }

        public static bool RemoveFromCompany(string userName)
        {
            return dal.RemoveFromCompany(userName);
        }

        private static bool SaveContacter(ContacterInfo contacterInfo)
        {
            if (Contacter.Exists(contacterInfo.UserName))
            {
                return Contacter.Update(contacterInfo);
            }
            return Contacter.Add(contacterInfo);
        }

        public static bool SaveUserPurview(UserPurviewInfo userPurviewInfo, int userId)
        {
            return dal.SaveUserPurview(userPurviewInfo, userId);
        }

        public static bool SaveUserPurview(bool inheritGroupRole, int userId)
        {
            return dal.SaveUserPurview(inheritGroupRole, userId);
        }

        public static string SendMessageToUser(UserInfo userInfo, string sendContent)
        {
            if (string.IsNullOrEmpty(sendContent))
            {
                return "";
            }
            string str = IsUserMobile(userInfo.UserName);
            if (string.IsNullOrEmpty(str))
            {
                sendContent = sendContent.Replace("{$UserName}", userInfo.UserName);
                sendContent = sendContent.Replace("{$Balance}", userInfo.Balance.ToString());
                sendContent = sendContent.Replace("{$UserPoint}", userInfo.UserPoint.ToString());
                sendContent = sendContent.Replace("{$ValidDays}", GetValidNum(userInfo.EndTime));
                return ("<li>" + SmsMessage.SendMessage(userInfo.UserName, sendContent, "0", "", PEContext.Current.Admin.AdminName) + "</li>");
            }
            return str;
        }

        public static bool Update(UserInfo userInfo)
        {
            return dal.Update(userInfo);
        }

        public static bool Update(UserInfo usersInfo, ContacterInfo contacterInfo)
        {
            bool flag = false;
            if (!dal.Update(usersInfo))
            {
                return flag;
            }
            contacterInfo.ClientId = usersInfo.ClientId;
            if (usersInfo.UserType == UserType.Persional)
            {
                contacterInfo.UserType = ContacterType.Persional;
            }
            else
            {
                contacterInfo.UserType = ContacterType.EnterpriceMainContacter;
            }
            return SaveContacter(contacterInfo);
        }

        public static bool Update(int userId, string fieldName, string fieldValue)
        {
            return ((!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(fieldValue)) && dal.Update(userId, DataSecurity.FilterBadChar(fieldName), DataSecurity.FilterBadChar(fieldValue)));
        }

        public static bool UpdateForCompany(int companyId, string userName, UserType userType, int companyClientId)
        {
            return dal.UpdateForCompany(companyId, userName, userType, companyClientId);
        }

        public static bool UpdateFriendGroupName(string userName, int friendGroupId, string newName)
        {
            bool flag = false;
            UserInfo usersByUserName = GetUsersByUserName(userName);
            if (usersByUserName.IsNull)
            {
                return flag;
            }
            string[] strArray = usersByUserName.UserFriendGroup.Split(new char[] { '$' });
            strArray[friendGroupId] = newName;
            StringBuilder sb = new StringBuilder();
            foreach (string str in strArray)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    StringHelper.AppendString(sb, str, "$");
                }
            }
            return UpdateUserFriendGroup(userName, sb.ToString());
        }

        public static bool UpdateUserFriendGroup(string userName, string userFriendGroup)
        {
            return dal.UpdateUserFriendGroup(userName, userFriendGroup);
        }

        public static string UserCacheKey(string userName)
        {
            return ("user-" + userName);
        }

        public static string UserNamefilter(string strchar)
        {
            string str2 = "";
            if (string.IsNullOrEmpty(strchar))
            {
                return "";
            }
            string str = strchar;
            string[] strArray = new string[] { 
                "'", "%", "^", "&", "?", "(", ")", "<", ">", "[", "]", "{", "}", "/", "\"", ";", 
                ":", "Chr(34)", "Chr(0)", "*", "|"
             };
            StringBuilder builder = new StringBuilder(str);
            for (int i = 0; i < strArray.Length; i++)
            {
                str2 = builder.Replace(strArray[i], "").ToString();
            }
            return builder.Replace("@@", "@").ToString();
        }

        public static UserStatus ValidateUser(UserInfo userInfo)
        {
            UserInfo usersByUserName = GetUsersByUserName(userInfo.UserName);
            if (usersByUserName.IsNull)
            {
                return (UserStatus)1;
            }
            string str = StringHelper.MD5(userInfo.UserPassword);
            if (!StringHelper.ValidateMD5(usersByUserName.UserPassword, str))
            {
                return (UserStatus)0x65;//新加了as UserStatus
            }
            usersByUserName.UserPassword = str;
            userInfo.UserName = usersByUserName.UserName;
            userInfo.GroupName = usersByUserName.GroupName;
            userInfo.GroupId = usersByUserName.GroupId;
            string str2 = DataSecurity.MakeRandomString(10);
            userInfo.LastPassword = str2;
            usersByUserName.LogOnTimes++;
            usersByUserName.LastLogOnTime = new DateTime?(DateTime.Now);
            usersByUserName.LastLogOnIP = PEContext.Current.UserHostAddress;
            usersByUserName.LastPassword = str2;
            Update(usersByUserName);
            if (usersByUserName.Status > UserStatus.None)
            {
                return usersByUserName.Status;
            }
            return UserStatus.None;
        }

        public static int ValidateUser(string userName, string password)
        {
            return dal.ValidateUser(userName, StringHelper.MD5(password));
        }
    }
}


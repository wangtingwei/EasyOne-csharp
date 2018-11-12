namespace EasyOne.UserManage
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class UserValidLog
    {
        private static readonly IUserValidLog dal = DataAccess.CreateUserValidLog();

        private UserValidLog()
        {
        }

        public static bool Add(UserValidLogInfo userValidLogInfo)
        {
            return dal.Add(userValidLogInfo);
        }

        public static bool Delete(DateTime tempDate)
        {
            return dal.Delete(tempDate);
        }

        public static bool Delete(string userName)
        {
            return dal.Delete(DataSecurity.FilterBadChar(userName));
        }

        public static bool Exists(string userName)
        {
            return dal.Exists(DataSecurity.FilterBadChar(userName));
        }

        public static int GetNumberOfUsersOnline(int scopesType, int field, string keyword)
        {
            return dal.GetCountNumber();
        }

        public static IList<UserValidLogInfo> GetValidList(int startRowIndexId, int maxNumberRows, int scopesType, int field, string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (scopesType)
                {
                    case 10:
                        if (field != 2)
                        {
                            keyword = DataSecurity.FilterBadChar(keyword);
                            break;
                        }
                        keyword = DataConverter.CDate(keyword).ToString("yyyy-MM-dd");
                        break;

                    case 11:
                    {
                        string[] strArray = keyword.Split(new char[] { '|' });
                        if (strArray.Length != 8)
                        {
                            scopesType = 0;
                            break;
                        }
                        string str = DataConverter.CLng(strArray[0]).ToString();
                        string str2 = DataConverter.CLng(strArray[1]).ToString();
                        string str3 = string.IsNullOrEmpty(strArray[2]) ? "" : DataConverter.CDate(strArray[2]).ToString("yyyy-MM-dd");
                        string str4 = string.IsNullOrEmpty(strArray[3]) ? "" : DataConverter.CDate(strArray[3]).ToString("yyyy-MM-dd");
                        string str5 = DataSecurity.FilterBadChar(strArray[4]);
                        string str6 = string.IsNullOrEmpty(strArray[5]) ? "" : DataConverter.CDate(strArray[5]).ToString("yyyy-MM-dd");
                        string str7 = DataSecurity.FilterBadChar(strArray[6]);
                        string str8 = strArray[7];
                        keyword = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", new object[] { str, str2, str3, str4, str5, str6, str7, str8 });
                        break;
                    }
                }
            }
            return dal.GetValidList(startRowIndexId, maxNumberRows, scopesType, field, keyword);
        }

        public static UserValidLogInfo GetValidLogById(int logId)
        {
            return dal.GetValidLogById(logId);
        }

        public static UserValidLogInfo GetValidLogByIdAndUserName(int logId)
        {
            string userName = PEContext.Current.User.UserName;
            return dal.GetValidLogByIdAndUserName(logId, userName);
        }
    }
}


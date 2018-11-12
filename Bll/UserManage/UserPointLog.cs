namespace EasyOne.UserManage
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class UserPointLog
    {
        private static readonly IUserPointLog dal = DataAccess.CreateUserPointLog();

        private UserPointLog()
        {
        }

        public static bool Add(UserPointLogInfo userPointLogInfo)
        {
            return dal.Add(userPointLogInfo);
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

        public static IList<UserPointLogInfo> GetPointList(int startRowIndexId, int maxNumberRows, int scopesType, int field, string keyword)
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
            return dal.GetPointList(startRowIndexId, maxNumberRows, scopesType, field, keyword);
        }

        public static UserPointLogInfo GetPointLogById(int logId)
        {
            return dal.GetPointLogById(logId);
        }

        public static UserPointLogInfo GetPointLogByIdAndUserName(int logId)
        {
            string userName = PEContext.Current.User.UserName;
            return dal.GetPointLogByIdAndUserName(logId, userName);
        }

        public static ArrayList GetTotalInComeAndPayOutAll()
        {
            return dal.GetTotalInComeAndPayOutAll();
        }

        public static ArrayList GetTotalInComeAndPayOutAll(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                ArrayList list = new ArrayList();
                list.Add(0);
                list.Add(0);
                return list;
            }
            return dal.GetTotalInComeAndPayOutAll(userName);
        }

        public static int GetValidPointLogId(string userName, int moduleType, int infoId, int chargeType, int pitchTime, int readTimes)
        {
            return dal.GetValidPointLogId(userName, moduleType, infoId, chargeType, pitchTime, readTimes);
        }

        public static int PointSum(int sumType)
        {
            return dal.PointSum(sumType);
        }

        public static int PointSum(int sumType, string userName)
        {
            return dal.PointSum(sumType, DataSecurity.FilterBadChar(userName));
        }

        public static bool UpdateTimes(int id, string ip)
        {
            return dal.UpdateTimes(id, ip);
        }

        public static void UpdateTimes(string userTrueIP, int logId)
        {
            dal.UpdateTimes(userTrueIP, logId);
        }

        public static int ViewInfosOneDay(string userName)
        {
            return dal.ViewInfosOneDay(DataSecurity.FilterBadChar(userName));
        }

        public static int ViewTotalInfos(string userName)
        {
            return dal.ViewTotalInfos(DataSecurity.FilterBadChar(userName));
        }
    }
}


namespace EasyOne.Contents
{
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class SignInLog
    {
        private static readonly ISignInLog dal = DataAccess.CreateSignInLog();

        private SignInLog()
        {
        }

        public static bool Add(SignInLogInfo signInLogInfo)
        {
            return dal.Add(signInLogInfo);
        }

        public static bool Add(int generalId, string userNameArr)
        {
            foreach (string str in userNameArr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                SignInLogInfo signInLogInfo = new SignInLogInfo();
                signInLogInfo.GeneralId = generalId;
                signInLogInfo.UserName = str;
                signInLogInfo.IsSignIn = false;
                signInLogInfo.IP = "";
                signInLogInfo.SignInTime = DateTime.Now;
                dal.Add(signInLogInfo);
            }
            return true;
        }

        private static List<string> ConvertToList(string stringArr)
        {
            List<string> list = new List<string>();
            foreach (string str in stringArr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                list.Add(str);
            }
            return list;
        }

        public static bool Delete(int generalId)
        {
            return dal.Delete(generalId);
        }

        public static bool Delete(int generalId, string userName)
        {
            return dal.Delete(generalId, userName);
        }

        public static IList<SignInLogInfo> GetList(int generalId)
        {
            return dal.GetList(generalId);
        }

        public static int GetNotSignInContentCountByUserName(string userName)
        {
            return dal.GetNotSignInContentCountByUserName(userName);
        }

        public static SignInLogInfo GetSignInLog(int generalId, string userName)
        {
            return dal.GetSignInLog(generalId, userName);
        }

        public static string GetSignInUsers(int generalId)
        {
            return dal.GetSignInUsers(generalId);
        }

        public static bool SignIn(int generalId, string userName, bool isSignIn, string ip)
        {
            return dal.SignIn(generalId, userName, isSignIn, ip);
        }

        public static bool Update(int generalId, string userNameArr)
        {
            List<string> list = ConvertToList(userNameArr);
            List<string> list2 = ConvertToList(GetSignInUsers(generalId));
            foreach (string str in list)
            {
                if (!list2.Contains(str))
                {
                    SignInLogInfo signInLogInfo = new SignInLogInfo();
                    signInLogInfo.GeneralId = generalId;
                    signInLogInfo.UserName = str;
                    signInLogInfo.IsSignIn = false;
                    signInLogInfo.IP = "";
                    signInLogInfo.SignInTime = DateTime.Now;
                    dal.Add(signInLogInfo);
                }
            }
            foreach (string str2 in list2)
            {
                if (!list.Contains(str2))
                {
                    Delete(generalId, str2);
                }
            }
            return true;
        }
    }
}


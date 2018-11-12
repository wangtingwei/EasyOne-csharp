namespace EasyOne.IDal.Contents
{
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;

    public interface ISignInLog
    {
        bool Add(SignInLogInfo signInLogInfo);
        bool Delete(int generalId);
        bool Delete(int generalId, string userName);
        IList<SignInLogInfo> GetList(int generalId);
        int GetNotSignInContentCountByUserName(string userName);
        SignInLogInfo GetSignInLog(int generalId, string userName);
        string GetSignInUsers(int generalId);
        bool SignIn(int generalId, string userName, bool isSignIn, string ip);
    }
}


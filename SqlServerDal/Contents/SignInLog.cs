namespace EasyOne.SqlServerDal.Contents
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class SignInLog : ISignInLog
    {
        public bool Add(SignInLogInfo signInLogInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, signInLogInfo.GeneralId);
            cmdParams.AddInParameter("@IP", DbType.String, signInLogInfo.IP);
            cmdParams.AddInParameter("@IsSignin", DbType.Boolean, signInLogInfo.IsSignIn);
            cmdParams.AddInParameter("@SigninTime", DbType.DateTime, signInLogInfo.SignInTime);
            cmdParams.AddInParameter("@UserName", DbType.String, signInLogInfo.UserName);
            return DBHelper.ExecuteSql("INSERT INTO PE_SigninLog (GeneralId, UserName, IsSignin, SigninTime, IP) VALUES (@GeneralId, @UserName, @IsSignin, @SigninTime, @IP)", cmdParams);
        }

        public bool Delete(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            return DBHelper.ExecuteSql("DELETE FROM PE_SigninLog WHERE GeneralId = @GeneralId", cmdParams);
        }

        public bool Delete(int generalId, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExecuteSql("DELETE FROM PE_SigninLog WHERE GeneralId = @GeneralId AND UserName = @UserName", cmdParams);
        }

        public IList<SignInLogInfo> GetList(int generalId)
        {
            List<SignInLogInfo> list = new List<SignInLogInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_SigninLog WHERE GeneralId = @GeneralId", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(SigninLogInfoFromDataReader(reader));
                }
            }
            return list;
        }

        public int GetNotSignInContentCountByUserName(string userName)
        {
            string strSql = "SELECT COUNT(*) FROM PE_SigninLog WHERE UserName = @UserName AND IsSignin = 0";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return (int) DBHelper.ExecuteScalarSql(strSql, cmdParams);
        }

        public SignInLogInfo GetSignInLog(int generalId, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralID", DbType.Int32, generalId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            SignInLogInfo info = new SignInLogInfo(true);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_SigninLog WHERE GeneralID = @GeneralID AND UserName = @UserName", cmdParams))
            {
                if (reader.Read())
                {
                    info = SigninLogInfoFromDataReader(reader);
                }
            }
            return info;
        }

        public string GetSignInUsers(int generalId)
        {
            StringBuilder sb = new StringBuilder();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT UserName FROM PE_SigninLog WHERE GeneralId = @GeneralId", cmdParams))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetString("UserName"));
                }
            }
            return sb.ToString();
        }

        public bool SignIn(int generalId, string userName, bool isSignIn, string ip)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@IsSignin", DbType.Boolean, isSignIn);
            cmdParams.AddInParameter("@IP", DbType.String, ip);
            cmdParams.AddInParameter("@SigninTime", DbType.DateTime, DateTime.Now);
            if (!DBHelper.ExecuteSql("UPDATE PE_SigninLog SET IsSignin = @IsSignin, [IP]=@IP, SigninTime = @SigninTime WHERE GeneralId = @GeneralId AND UserName = @UserName", cmdParams))
            {
                return false;
            }
            Parameters parameters2 = new Parameters();
            parameters2.AddInParameter("@GeneralId", DbType.Int32, generalId);
            object obj2 = DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_SigninLog WHERE GeneralId = @GeneralId AND IsSignin = 0", parameters2);
            if ((obj2 != null) && (((int) obj2) > 0))
            {
                parameters2.AddInParameter("@Status", DbType.Int32, SignInStatus.NotSignIn);
                DBHelper.ExecuteNonQuerySql("UPDATE PE_SigninContent SET Status = @Status WHERE GeneralId = @GeneralId", parameters2);
            }
            else
            {
                parameters2.AddInParameter("@Status", DbType.Int32, SignInStatus.SignIned);
                DBHelper.ExecuteNonQuerySql("UPDATE PE_SigninContent SET Status = @Status WHERE GeneralId = @GeneralId", parameters2);
            }
            return true;
        }

        private static SignInLogInfo SigninLogInfoFromDataReader(NullableDataReader dr)
        {
            SignInLogInfo info = new SignInLogInfo();
            info.GeneralId = dr.GetInt32("GeneralId");
            info.IP = dr.GetString("IP");
            info.IsSignIn = dr.GetBoolean("IsSignin");
            info.SignInTime = dr.GetDateTime("SigninTime");
            info.UserName = dr.GetString("UserName");
            return info;
        }
    }
}


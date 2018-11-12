namespace EasyOne.SqlServerDal.Contents
{
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class SignInContent : ISignInContent
    {
        public bool Add(SignInContentInfo signInContentInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, signInContentInfo.GeneralId);
            cmdParams.AddInParameter("@EndTime", DbType.DateTime, signInContentInfo.EndTime);
            cmdParams.AddInParameter("@Priority", DbType.Int32, signInContentInfo.Priority);
            cmdParams.AddInParameter("@SigninType", DbType.Int32, signInContentInfo.SignInType);
            cmdParams.AddInParameter("@Status", DbType.Int32, signInContentInfo.Status);
            cmdParams.AddInParameter("@Title", DbType.String, signInContentInfo.Title);
            return DBHelper.ExecuteSql("INSERT INTO PE_SigninContent (GeneralId, EndTime, Priority, SigninType, Status, Title) VALUES (@GeneralId, @EndTime, @Priority, @SigninType, @Status, @Title)", cmdParams);
        }

        public bool Delete(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            return DBHelper.ExecuteSql("DELETE FROM PE_SigninContent WHERE GeneralId = @GeneralId", cmdParams);
        }

        public SignInContentInfo GetSignInContentByGeneralId(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_SigninContent WHERE GeneralId = @GeneralId", cmdParams))
            {
                if (reader.Read())
                {
                    return SigninContentInfoFromDataReader(reader);
                }
                return new SignInContentInfo(true);
            }
        }

        private static SignInContentInfo SigninContentInfoFromDataReader(NullableDataReader dr)
        {
            SignInContentInfo info = new SignInContentInfo();
            info.GeneralId = dr.GetInt32("GeneralId");
            info.EndTime = dr.GetDateTime("EndTime");
            info.Priority = dr.GetInt32("Priority");
            info.SignInType = (SignInType) dr.GetInt32("SigninType");
            info.Status = (SignInStatus) dr.GetInt32("Status");
            info.Title = dr.GetString("Title");
            return info;
        }

        public bool Update(SignInContentInfo signInContentInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, signInContentInfo.GeneralId);
            cmdParams.AddInParameter("@EndTime", DbType.DateTime, signInContentInfo.EndTime);
            cmdParams.AddInParameter("@Priority", DbType.Int32, signInContentInfo.Priority);
            cmdParams.AddInParameter("@SigninType", DbType.Int32, signInContentInfo.SignInType);
            cmdParams.AddInParameter("@Title", DbType.String, signInContentInfo.Title);
            return DBHelper.ExecuteSql("UPDATE PE_SigninContent SET Title = @Title, SigninType = @SigninType, Priority = @Priority, EndTime = @EndTime WHERE GeneralId = @GeneralId", cmdParams);
        }

        public bool UpdateContentSignInType(int generalId, SignInType signInType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            cmdParams.AddInParameter("@SigninType", DbType.Int32, signInType);
            return DBHelper.ExecuteSql("UPDATE PE_CommonModel SET SigninType = @SigninType WHERE GeneralId = @GeneralId", cmdParams);
        }
    }
}


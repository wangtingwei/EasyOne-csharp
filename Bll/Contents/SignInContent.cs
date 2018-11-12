namespace EasyOne.Contents
{
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using System;
    using EasyOne.DalFactory;

    public sealed class SignInContent
    {
        private static readonly ISignInContent dal = DataAccess.CreateSignInContent();

        private SignInContent()
        {
        }

        public static bool Add(SignInContentInfo signInContentInfo)
        {
            return dal.Add(signInContentInfo);
        }

        public static bool AddSignInContent(SignInContentInfo signInContentInfo)
        {
            if (UpdateContentSignInType(signInContentInfo.GeneralId, signInContentInfo.SignInType))
            {
                Add(signInContentInfo);
                return true;
            }
            return false;
        }

        public static bool Delete(int generalId)
        {
            return dal.Delete(generalId);
        }

        public static SignInContentInfo GetSignInContentByGeneralId(int generalId)
        {
            return dal.GetSignInContentByGeneralId(generalId);
        }

        public static bool Update(SignInContentInfo signInContentInfo)
        {
            return dal.Update(signInContentInfo);
        }

        public static bool UpdateContentSignInType(int generalId, SignInType signInType)
        {
            return dal.UpdateContentSignInType(generalId, signInType);
        }

        public static bool UpdateSignInContent(SignInContentInfo signInContentInfo)
        {
            return (dal.Update(signInContentInfo) || Add(signInContentInfo));
        }
    }
}


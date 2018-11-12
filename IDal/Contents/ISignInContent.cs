namespace EasyOne.IDal.Contents
{
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using System;

    public interface ISignInContent
    {
        bool Add(SignInContentInfo signInContentInfo);
        bool Delete(int generalId);
        SignInContentInfo GetSignInContentByGeneralId(int generalId);
        bool Update(SignInContentInfo signInContentInfo);
        bool UpdateContentSignInType(int generalId, SignInType signInType);
    }
}


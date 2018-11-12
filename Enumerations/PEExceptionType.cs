namespace EasyOne.Enumerations
{
    using System;

    public enum PEExceptionType
    {
        BllError = 10,
        ConnectionFalse = 4,
        ExceedAuthority = 8,
        LockedUser = 3,
        NoSuchUser = 1,
        NotenoughMoney = 6,
        NullHttpContext = 7,
        PasswordNotMatch = 2,
        RefreshedError = 11,
        ResourceError = 9,
        ResourceNotFound = 0,
        SameCard = 5,
        UnknownError = 0x3e7
    }
}


namespace EasyOne.Logging
{
    using System;

    public enum LogCategory
    {
        None,
        LogOnOk,
        LogOnFailure,
        LogOff,
        ExceedAuthority,
        Exception,
        AdminErr,
        ImportantAction,
        SystemAction
    }
}


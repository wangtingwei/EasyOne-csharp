namespace EasyOne.Enumerations
{
    using System;

    public enum PayOnlineState
    {
        AccountPaid = 3,
        Fail = 0x63,
        NoMoney = 4,
        None = 0,
        NoPaymentNumber = 8,
        Ok = 1,
        OrderNotFound = 2,
        PaymentLogNotFound = 9,
        PayPlatFormDisabled = 7,
        PayPlatFormNotFound = 6,
        RemittanceWrong = 10,
        TooLittleMoney = 5
    }
}


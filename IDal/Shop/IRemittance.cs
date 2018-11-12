namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;

    public interface IRemittance
    {
        bool Add(RemittanceInfo remittanceInfo);
        RemittanceInfo GetByOrderId(int orderId);
    }
}


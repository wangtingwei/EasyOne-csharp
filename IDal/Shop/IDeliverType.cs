namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IDeliverType
    {
        bool Add(DeliverTypeInfo deliverTypeInfo);
        bool Delete(int typeId);
        DeliverTypeInfo GetDeliverTypeById(int typeId);
        IList<DeliverTypeInfo> GetDeliverTypeList();
        IList<DeliverTypeInfo> GetEnableDeliverTypeList();
        int GetMaxTypeId();
        bool Update(DeliverTypeInfo deliverTypeInfo);
    }
}


namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public interface IDeliverCharge
    {
        bool Add(IList<DeliverChargeInfo> deliverChargeInfoList);
        bool Add(DeliverChargeInfo deliverChargeInfo);
        bool DeleteById(int id);
        IList<DeliverChargeInfo> GetChargeParmListOfWeight(int areaType, int deliverType);
        DeliverChargeInfo GetChargeParmOfWeight(int areaType, int deliverType);
        DeliverChargeInfo GetDeliverChargeById(int id);
        IList<DeliverChargeInfo> GetDeliverChargeListByAreaType(int typeId, int areaType);
        IList<DeliverChargeInfo> GetDeliverChargeListByTypeId(int typeId);
        ArrayList GetProvinceList();
        StringBuilder GetSelectedProvince(int deliverTypeId);
        bool Update(IList<DeliverChargeInfo> deliverChargeInfoList);
        bool Update(DeliverChargeInfo deliverChargeInfo);
    }
}


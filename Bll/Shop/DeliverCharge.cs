namespace EasyOne.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class DeliverCharge
    {
        private static readonly IDeliverCharge dal = DataAccess.CreateDeliverCharge();

        private DeliverCharge()
        {
        }

        public static bool Add(IList<DeliverChargeInfo> deliverChargeInfoList)
        {
            return dal.Add(deliverChargeInfoList);
        }

        public static bool Add(DeliverChargeInfo deliverChargeInfo)
        {
            return ((deliverChargeInfo != null) && dal.Add(deliverChargeInfo));
        }

        public static bool DeleteById(int id)
        {
            return dal.DeleteById(id);
        }

        private static decimal GetAgreedCharge(decimal charge_Deliver, decimal totalMoney, DeliverTypeInfo deliverTypeInfo)
        {
            decimal num = charge_Deliver;
            if (charge_Deliver <= deliverTypeInfo.ReleaseCharge)
            {
                charge_Deliver = 0M;
            }
            else
            {
                charge_Deliver -= deliverTypeInfo.ReleaseCharge;
            }
            if (totalMoney >= deliverTypeInfo.MinMoney2)
            {
                if (totalMoney >= deliverTypeInfo.MinMoney3)
                {
                    charge_Deliver = 0M;
                    return charge_Deliver;
                }
                if (num <= deliverTypeInfo.MaxCharge)
                {
                    charge_Deliver = 0M;
                }
            }
            return charge_Deliver;
        }

        private static decimal GetChargeByTotalMoney(DeliverTypeInfo deliverTypeInfo, decimal totalMoney)
        {
            decimal chargeMax = 0M;
            chargeMax = ((totalMoney * deliverTypeInfo.ChargePercent) / 100M) + deliverTypeInfo.ChargeMin;
            if (chargeMax > deliverTypeInfo.ChargeMax)
            {
                chargeMax = deliverTypeInfo.ChargeMax;
            }
            return chargeMax;
        }

        private static decimal GetChargeByWeight(string postCode, int deliverTypeId, double totalWeight)
        {
            decimal chargeMax = 0M;
            DeliverChargeInfo chargeParmOfWeight = GetChargeParmOfWeight(postCode, deliverTypeId);
            if (chargeParmOfWeight.IsNull)
            {
                return chargeMax;
            }
            if (totalWeight > 0.0)
            {
                if (totalWeight > chargeParmOfWeight.WeightMin)
                {
                    decimal d = DataConverter.CDecimal((totalWeight - chargeParmOfWeight.WeightMin) / chargeParmOfWeight.WeightPerUnit);
                    if (d > decimal.Floor(d))
                    {
                        d = -(decimal.Floor(d));//将decimal.op_Decrement改为-
                    }
                    chargeMax = chargeParmOfWeight.ChargeMin + (d * chargeParmOfWeight.ChargePerUnit);
                    if (chargeMax > chargeParmOfWeight.ChargeMax)
                    {
                        chargeMax = chargeParmOfWeight.ChargeMax;
                    }
                    return chargeMax;
                }
                return chargeParmOfWeight.ChargeMin;
            }
            return 0M;
        }

        private static DeliverChargeInfo GetChargeParmOfWeight(string postCode, int deliverTypeId)
        {
            string country = SiteConfig.ShopConfig.Country;
            string province = SiteConfig.ShopConfig.Province;
            string city = SiteConfig.ShopConfig.City;
            RegionInfo byPostCodeOfFourNumber = Region.GetByPostCodeOfFourNumber(postCode);
            DeliverChargeInfo info2 = new DeliverChargeInfo(true);
            int areaType = 5;
            if (!byPostCodeOfFourNumber.IsNull)
            {
                if (((byPostCodeOfFourNumber.Country == country) && (byPostCodeOfFourNumber.Province == province)) && (byPostCodeOfFourNumber.City == city))
                {
                    areaType = 1;
                }
                else
                {
                    DeliverChargeInfo chargeParmOfWeight = dal.GetChargeParmOfWeight(2, deliverTypeId);
                    if (!chargeParmOfWeight.IsNull && StringHelper.FoundCharInArr(chargeParmOfWeight.ArrArea, byPostCodeOfFourNumber.Province + "|" + byPostCodeOfFourNumber.City, ","))
                    {
                        areaType = 2;
                        info2 = chargeParmOfWeight;
                    }
                    if (areaType != 2)
                    {
                        if ((byPostCodeOfFourNumber.Country == country) && (byPostCodeOfFourNumber.Province == province))
                        {
                            areaType = 3;
                        }
                        else
                        {
                            areaType = 5;
                            IList<DeliverChargeInfo> list = new List<DeliverChargeInfo>();
                            foreach (DeliverChargeInfo info4 in dal.GetChargeParmListOfWeight(4, deliverTypeId))
                            {
                                if (StringHelper.FoundCharInArr(info4.ArrArea, byPostCodeOfFourNumber.Province, ","))
                                {
                                    areaType = 4;
                                    info2 = info4;
                                    break;
                                }
                            }
                            if ((areaType == 5) && Region.PostCodeExists(postCode))
                            {
                                info2 = dal.GetChargeParmOfWeight(4, deliverTypeId);
                                if (!info2.IsNull)
                                {
                                    areaType = 4;
                                }
                            }
                        }
                    }
                }
            }
            if ((areaType != 2) && (areaType != 4))
            {
                info2 = dal.GetChargeParmOfWeight(areaType, deliverTypeId);
            }
            if (info2.WeightPerUnit <= 0.0)
            {
                info2.WeightPerUnit = 1.0;
            }
            return info2;
        }

        public static decimal GetDeliverCharge(int deliverTypeId, double totalWeight, string postCode, decimal totalMoney, bool needInvoice)
        {
            DeliverTypeInfo deliverTypeById = DeliverType.GetDeliverTypeById(deliverTypeId);
            decimal chargeByTotalMoney = 0M;
            if (!deliverTypeById.IsNull)
            {
                switch (deliverTypeById.ChargeType)
                {
                    case 0:
                        chargeByTotalMoney = 0M;
                        break;

                    case 1:
                        if (totalWeight > 0.0)
                        {
                            chargeByTotalMoney = GetChargeByWeight(postCode, deliverTypeId, totalWeight);
                        }
                        break;

                    case 2:
                        chargeByTotalMoney = GetChargeByTotalMoney(deliverTypeById, totalMoney);
                        break;

                    case 3:
                        return deliverTypeById.MinMoney1;
                }
                if (((chargeByTotalMoney > 0M) && (deliverTypeById.ReleaseType > 0)) && (totalMoney >= deliverTypeById.MinMoney1))
                {
                    chargeByTotalMoney = GetAgreedCharge(chargeByTotalMoney, totalMoney, deliverTypeById);
                }
            }
            if (chargeByTotalMoney > 0M)
            {
                chargeByTotalMoney = TaxRateComputeOfDeliver(deliverTypeById, chargeByTotalMoney, needInvoice);
            }
            if (chargeByTotalMoney < 0M)
            {
                chargeByTotalMoney = 0M;
            }
            return chargeByTotalMoney;
        }

        public static DeliverChargeInfo GetDeliverChargeById(int id)
        {
            return dal.GetDeliverChargeById(id);
        }

        public static IList<DeliverChargeInfo> GetDeliverChargeListByAreaType(int typeId, int areaType)
        {
            return dal.GetDeliverChargeListByAreaType(typeId, areaType);
        }

        public static IList<DeliverChargeInfo> GetDeliverChargeListByTypeId(int typeId)
        {
            return dal.GetDeliverChargeListByTypeId(typeId);
        }

        public static ArrayList GetProvinceList()
        {
            return dal.GetProvinceList();
        }

        public static ArrayList GetProvinceList(int deliverTypeId)
        {
            ArrayList provinceList = GetProvinceList();
            ArrayList list2 = new ArrayList();
            string[] strArray = dal.GetSelectedProvince(deliverTypeId).ToString().Split(new char[] { ',' });
            foreach (string str in provinceList)
            {
                bool flag = false;
                foreach (string str2 in strArray)
                {
                    if (str2 == str)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag && (str != SiteConfig.ShopConfig.Province))
                {
                    list2.Add(str);
                }
            }
            return list2;
        }

        public static decimal TaxRateComputeOfDeliver(DeliverTypeInfo deliverTypeInfo, decimal deliverCharge, bool needInvoice)
        {
            if ((deliverTypeInfo.IncludeTax == TaxRateType.IncludeTaxNoInvoiceFavourable) || (deliverTypeInfo.IncludeTax == TaxRateType.IncludeTaxNoInvoiceNoFavourable))
            {
                if (!needInvoice && (deliverTypeInfo.IncludeTax == TaxRateType.IncludeTaxNoInvoiceFavourable))
                {
                    deliverCharge = (deliverCharge * (100M - DataConverter.CDecimal(deliverTypeInfo.TaxRate))) / 100M;
                }
                return deliverCharge;
            }
            if (needInvoice && (deliverTypeInfo.IncludeTax == TaxRateType.BarringTaxNeedInvoiceAddTax))
            {
                deliverCharge = (deliverCharge * (100M + DataConverter.CDecimal(deliverTypeInfo.TaxRate))) / 100M;
            }
            return deliverCharge;
        }

        public static bool Update(IList<DeliverChargeInfo> deliverChargeInfoList)
        {
            return dal.Update(deliverChargeInfoList);
        }

        public static bool Update(DeliverChargeInfo deliverChargeInfo)
        {
            return ((deliverChargeInfo != null) && dal.Update(deliverChargeInfo));
        }
    }
}


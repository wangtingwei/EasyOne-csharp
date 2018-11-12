namespace EasyOne.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class DeliverType
    {
        private static readonly IDeliverType dal = DataAccess.CreateDeliverType();

        private DeliverType()
        {
        }

        public static bool Add(DeliverTypeInfo deliverTypeInfo)
        {
            if (deliverTypeInfo == null)
            {
                return false;
            }
            if (deliverTypeInfo.ChargeType == 0)
            {
                deliverTypeInfo.IncludeTax = TaxRateType.BarringTaxNeedInvoiceNoTax;
            }
            return dal.Add(deliverTypeInfo);
        }

        public static bool Delete(int typeId)
        {
            return dal.Delete(typeId);
        }

        public static DeliverTypeInfo GetDeliverTypeById(int typeId)
        {
            return dal.GetDeliverTypeById(typeId);
        }

        public static IList<DeliverTypeInfo> GetDeliverTypeList()
        {
            return dal.GetDeliverTypeList();
        }

        public static IList<DeliverTypeInfo> GetEnableDeliverTypeList()
        {
            return dal.GetEnableDeliverTypeList();
        }

        public static int GetMaxTypeId()
        {
            return dal.GetMaxTypeId();
        }

        public static bool Update(DeliverTypeInfo deliverTypeInfo)
        {
            if (deliverTypeInfo == null)
            {
                return false;
            }
            if (deliverTypeInfo.ChargeType == 0)
            {
                deliverTypeInfo.IncludeTax = TaxRateType.BarringTaxNeedInvoiceNoTax;
            }
            return dal.Update(deliverTypeInfo);
        }
    }
}


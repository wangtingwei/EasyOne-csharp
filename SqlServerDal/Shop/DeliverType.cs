namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class DeliverType : IDeliverType
    {
        public bool Add(DeliverTypeInfo deliverTypeInfo)
        {
            return DBHelper.ExecuteProc("PR_Shop_DeliverType_Add", AddInCommonParameter(deliverTypeInfo));
        }

        private static Parameters AddInCommonParameter(DeliverTypeInfo deliverTypeInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@typeId", DbType.Int32, deliverTypeInfo.TypeId);
            parameters.AddInParameter("@typeName", DbType.String, deliverTypeInfo.TypeName);
            parameters.AddInParameter("@intro", DbType.String, deliverTypeInfo.Intro);
            parameters.AddInParameter("@chargeType", DbType.Int32, deliverTypeInfo.ChargeType);
            parameters.AddInParameter("@isDefault", DbType.Boolean, deliverTypeInfo.IsDefault);
            parameters.AddInParameter("@isDisabled", DbType.Boolean, deliverTypeInfo.IsDisabled);
            parameters.AddInParameter("@releaseType", DbType.Int32, deliverTypeInfo.ReleaseType);
            parameters.AddInParameter("@minMoney1", DbType.Currency, deliverTypeInfo.MinMoney1);
            parameters.AddInParameter("@releaseCharge", DbType.Currency, deliverTypeInfo.ReleaseCharge);
            parameters.AddInParameter("@minMoney2", DbType.Currency, deliverTypeInfo.MinMoney2);
            parameters.AddInParameter("@maxCharge", DbType.Currency, deliverTypeInfo.MaxCharge);
            parameters.AddInParameter("@minMoney3", DbType.Currency, deliverTypeInfo.MinMoney3);
            parameters.AddInParameter("@charge_Min", DbType.Currency, deliverTypeInfo.ChargeMin);
            parameters.AddInParameter("@charge_Max", DbType.Currency, deliverTypeInfo.ChargeMax);
            parameters.AddInParameter("@includeTax", DbType.Int32, deliverTypeInfo.IncludeTax);
            parameters.AddInParameter("@taxRate", DbType.Double, deliverTypeInfo.TaxRate);
            parameters.AddInParameter("@charge_Percent", DbType.Int16, deliverTypeInfo.ChargePercent);
            return parameters;
        }

        public bool Delete(int typeId)
        {
            return DBHelper.ExecuteProc("PR_Shop_DeliverType_Delete", new Parameters("@typeId", DbType.Int32, typeId));
        }

        private static DeliverTypeInfo DeliverTypeInfoFromrdr(NullableDataReader rdr)
        {
            DeliverTypeInfo info = new DeliverTypeInfo();
            info.TypeId = rdr.GetInt32("TypeId");
            info.TypeName = rdr.GetString("TypeName");
            info.Intro = rdr.GetString("Intro");
            info.ChargeType = rdr.GetInt32("ChargeType");
            info.IsDefault = rdr.GetBoolean("IsDefault");
            info.IsDisabled = rdr.GetBoolean("IsDisabled");
            info.OrderId = rdr.GetInt32("OrderId");
            info.ReleaseType = rdr.GetInt32("ReleaseType");
            info.MinMoney1 = rdr.GetDecimal("MinMoney1");
            info.ReleaseCharge = rdr.GetDecimal("ReleaseCharge");
            info.MinMoney2 = rdr.GetDecimal("MinMoney2");
            info.MaxCharge = rdr.GetDecimal("MaxCharge");
            info.MinMoney3 = rdr.GetDecimal("MinMoney3");
            info.ChargeMin = rdr.GetDecimal("Charge_Min");
            info.ChargeMax = rdr.GetDecimal("Charge_Max");
            info.IncludeTax = (TaxRateType) rdr.GetInt32("IncludeTax");
            info.TaxRate = rdr.GetDouble("TaxRate");
            info.ChargePercent = rdr.GetInt16("Charge_Percent");
            return info;
        }

        public DeliverTypeInfo GetDeliverTypeById(int typeId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Shop_DeliverType_GetById", new Parameters("@typeId", DbType.Int32, typeId)))
            {
                if (reader.Read())
                {
                    return DeliverTypeInfoFromrdr(reader);
                }
                return new DeliverTypeInfo(true);
            }
        }

        public IList<DeliverTypeInfo> GetDeliverTypeList()
        {
            IList<DeliverTypeInfo> list = new List<DeliverTypeInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Shop_DeliverType_GetList"))
            {
                while (reader.Read())
                {
                    DeliverTypeInfo item = new DeliverTypeInfo();
                    item.TypeId = reader.GetInt32("TypeId");
                    item.TypeName = reader.GetString("TypeName");
                    item.Intro = Convert.IsDBNull(reader["Intro"]) ? "" : reader.GetString("Intro");
                    item.ChargeType = reader.GetInt32("ChargeType");
                    item.IsDefault = reader.GetBoolean("IsDefault");
                    item.IsDisabled = reader.GetBoolean("IsDisabled");
                    item.OrderId = reader.GetInt32("OrderId");
                    item.IncludeTax = (TaxRateType) reader.GetInt32("IncludeTax");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<DeliverTypeInfo> GetEnableDeliverTypeList()
        {
            IList<DeliverTypeInfo> list = new List<DeliverTypeInfo>();
            string strSql = "SELECT * FROM PE_DeliverType WHERE isDisabled = 0";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    list.Add(DeliverTypeInfoFromrdr(reader));
                }
            }
            return list;
        }

        public int GetMaxTypeId()
        {
            return DBHelper.GetMaxId("PR_Shop_DeliverType_GetMaxId");
        }

        public bool Update(DeliverTypeInfo deliverTypeInfo)
        {
            Parameters cmdParams = AddInCommonParameter(deliverTypeInfo);
            cmdParams.AddInParameter("@orderId", DbType.Int32, deliverTypeInfo.OrderId);
            return DBHelper.ExecuteProc("PR_Shop_DeliverType_Update", cmdParams);
        }
    }
}


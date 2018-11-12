namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class DeliverCharge : IDeliverCharge
    {
        public bool Add(DeliverChargeInfo deliverChargeInfo)
        {
            Parameters cmdParams = AddInCommonParameter(deliverChargeInfo);
            cmdParams.AddInParameter("@deliverTypeID", DbType.Int32, deliverChargeInfo.DeliverTypeId);
            return DBHelper.ExecuteProc("PR_Shop_DeliverCharge_Add", cmdParams);
        }

        public bool Add(IList<DeliverChargeInfo> deliverChargeInfoList)
        {
            bool flag = false;
            for (int i = 0; i < deliverChargeInfoList.Count; i++)
            {
                flag = this.Add(deliverChargeInfoList[i]);
                if (!flag)
                {
                    this.DeleteById(deliverChargeInfoList[i].DeliverTypeId);
                    return flag;
                }
            }
            return flag;
        }

        private static Parameters AddInCommonParameter(DeliverChargeInfo deliverChargeInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@areaType", DbType.Int32, deliverChargeInfo.AreaType);
            parameters.AddInParameter("@arrArea", DbType.String, deliverChargeInfo.ArrArea);
            parameters.AddInParameter("@charge_Min", DbType.Currency, deliverChargeInfo.ChargeMin);
            parameters.AddInParameter("@weight_Min", DbType.Double, deliverChargeInfo.WeightMin);
            parameters.AddInParameter("@chargePerUnit", DbType.Currency, deliverChargeInfo.ChargePerUnit);
            parameters.AddInParameter("@weightPerUnit", DbType.Double, deliverChargeInfo.WeightPerUnit);
            parameters.AddInParameter("@charge_Max", DbType.Currency, deliverChargeInfo.ChargeMax);
            return parameters;
        }

        private static DeliverChargeInfo CreateInfo(NullableDataReader rdr)
        {
            DeliverChargeInfo info = new DeliverChargeInfo();
            info.Id = rdr.GetInt32("Id");
            info.DeliverTypeId = rdr.GetInt32("DeliverTypeId");
            info.AreaType = rdr.GetInt32("AreaType");
            info.ArrArea = rdr["arrArea"].ToString();
            info.ChargeMin = rdr.GetDecimal("Charge_Min");
            info.WeightMin = rdr.GetDouble("Weight_Min");
            info.ChargePerUnit = rdr.GetDecimal("ChargePerUnit");
            info.WeightPerUnit = rdr.GetDouble("WeightPerUnit");
            info.ChargeMax = rdr.GetDecimal("Charge_Max");
            return info;
        }

        public bool DeleteById(int id)
        {
            return DBHelper.ExecuteProc("PR_Shop_DeliverCharge_Delete", new Parameters("@id", DbType.Int32, id));
        }

        private static void DeliverChargeInfoFromrdr(DeliverChargeInfo deliverChargeInfo, NullableDataReader rdr)
        {
            deliverChargeInfo.ChargeMin = rdr.GetDecimal("Charge_Min");
            deliverChargeInfo.WeightMin = rdr.GetDouble("Weight_Min");
            deliverChargeInfo.ChargePerUnit = rdr.GetDecimal("ChargePerUnit");
            deliverChargeInfo.WeightPerUnit = rdr.GetDouble("WeightPerUnit");
            deliverChargeInfo.ChargeMax = rdr.GetDecimal("Charge_Max");
            deliverChargeInfo.ArrArea = rdr.GetString("arrArea");
        }

        public IList<DeliverChargeInfo> GetChargeParmListOfWeight(int areaType, int deliverType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@AreaType", DbType.Int32, areaType);
            cmdParams.AddInParameter("@DeliverType", DbType.Int32, deliverType);
            IList<DeliverChargeInfo> list = new List<DeliverChargeInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Shop_DeliverCharge_GetChargeParmOfWeight", cmdParams))
            {
                while (reader.Read())
                {
                    DeliverChargeInfo deliverChargeInfo = new DeliverChargeInfo();
                    DeliverChargeInfoFromrdr(deliverChargeInfo, reader);
                    list.Add(deliverChargeInfo);
                }
            }
            return list;
        }

        public DeliverChargeInfo GetChargeParmOfWeight(int areaType, int deliverType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@AreaType", DbType.Int32, areaType);
            cmdParams.AddInParameter("@DeliverType", DbType.Int32, deliverType);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Shop_DeliverCharge_GetChargeParmOfWeight", cmdParams))
            {
                if (reader.Read())
                {
                    DeliverChargeInfo deliverChargeInfo = new DeliverChargeInfo();
                    DeliverChargeInfoFromrdr(deliverChargeInfo, reader);
                    return deliverChargeInfo;
                }
                return new DeliverChargeInfo(true);
            }
        }

        public DeliverChargeInfo GetDeliverChargeById(int id)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Shop_DeliverCharge_GetById", new Parameters("@id", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    return CreateInfo(reader);
                }
                return new DeliverChargeInfo(true);
            }
        }

        public IList<DeliverChargeInfo> GetDeliverChargeListByAreaType(int typeId, int areaType)
        {
            IList<DeliverChargeInfo> list = new List<DeliverChargeInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@typeId", DbType.Int32, typeId);
            cmdParams.AddInParameter("@areaType", DbType.Int32, areaType);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Shop_DeliverCharge_GetListByAreaType", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(CreateInfo(reader));
                }
            }
            return list;
        }

        public IList<DeliverChargeInfo> GetDeliverChargeListByTypeId(int typeId)
        {
            IList<DeliverChargeInfo> list = new List<DeliverChargeInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Shop_DeliverCharge_GetByTypeId", new Parameters("@typeId", DbType.Int32, typeId)))
            {
                while (reader.Read())
                {
                    list.Add(CreateInfo(reader));
                }
            }
            return list;
        }

        public ArrayList GetProvinceList()
        {
            ArrayList list = new ArrayList();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_Region_GetProvinceList"))
            {
                while (reader.Read())
                {
                    list.Add(reader[0]);
                }
            }
            return list;
        }

        public StringBuilder GetSelectedProvince(int deliverTypeId)
        {
            StringBuilder sb = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT arrArea FROM PE_DeliverCharge WHERE AreaType = 4 AND DeliverTypeID = @DeliverTypeID", new Parameters("@DeliverTypeID", DbType.Int32, deliverTypeId)))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetString("arrArea"));
                }
            }
            return sb;
        }

        public bool Update(DeliverChargeInfo deliverChargeInfo)
        {
            Parameters cmdParams = AddInCommonParameter(deliverChargeInfo);
            cmdParams.AddInParameter("@id", DbType.Int32, deliverChargeInfo.Id);
            return DBHelper.ExecuteProc("PR_Shop_DeliverCharge_Update", cmdParams);
        }

        public bool Update(IList<DeliverChargeInfo> deliverChargeInfoList)
        {
            bool flag = false;
            for (int i = 0; i < deliverChargeInfoList.Count; i++)
            {
                flag = this.Update(deliverChargeInfoList[i]);
                if (!flag)
                {
                    return flag;
                }
            }
            return flag;
        }
    }
}


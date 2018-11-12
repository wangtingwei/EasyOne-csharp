namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public sealed class PaymentType : IPaymentType
    {
        public bool Add(PaymentTypeInfo paymentTypeInfo)
        {
            return DBHelper.ExecuteProc("PR_Shop_PaymentType_Add", GetParameters(paymentTypeInfo));
        }

        public bool Delete(int typeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@typeId", DbType.Int32, typeId);
            return DBHelper.ExecuteProc("PR_Shop_PaymentType_Delete", cmdParams);
        }

        private static Parameters GetParameters(PaymentTypeInfo paymentTypeInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@typeName", DbType.String, paymentTypeInfo.TypeName);
            parameters.AddInParameter("@intro", DbType.String, paymentTypeInfo.Intro);
            parameters.AddInParameter("@discount", DbType.Double, paymentTypeInfo.Discount);
            parameters.AddInParameter("@isDefault", DbType.Boolean, paymentTypeInfo.IsDefault);
            parameters.AddInParameter("@isDisabled", DbType.Boolean, paymentTypeInfo.IsDisabled);
            parameters.AddInParameter("@category", DbType.Int32, paymentTypeInfo.Category);
            return parameters;
        }

        public PaymentTypeInfo GetPaymentTypeById(int typeId)
        {
            Parameters cmdParams = new Parameters("@id", DbType.Int32, typeId);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Shop_PaymentType_GetById", cmdParams)))
            {
                if (reader.Read())
                {
                    return PaymentTypeFromrdr(reader);
                }
                return new PaymentTypeInfo(true);
            }
        }

        public IList<PaymentTypeInfo> GetPaymentTypeList()
        {
            IList<PaymentTypeInfo> list = new List<PaymentTypeInfo>();
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Shop_PaymentType_GetList")))
            {
                while (reader.Read())
                {
                    list.Add(PaymentTypeFromrdr(reader));
                }
            }
            return list;
        }

        public IList<PaymentTypeInfo> GetPaymentTypeList(int category)
        {
            IList<PaymentTypeInfo> list = new List<PaymentTypeInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_PaymentType WHERE category = @category AND isDisabled = 0  ORDER BY orderId", new Parameters("@category", DbType.Int32, category)))
            {
                while (reader.Read())
                {
                    list.Add(PaymentTypeFromrdr(reader));
                }
            }
            return list;
        }

        public IList<PaymentTypeInfo> GetPaymentTypeListByEnabled()
        {
            IList<PaymentTypeInfo> list = new List<PaymentTypeInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_PaymentType WHERE IsDisabled = 0 ORDER BY orderID"))
            {
                while (reader.Read())
                {
                    list.Add(PaymentTypeFromrdr(reader));
                }
            }
            return list;
        }

        private static PaymentTypeInfo PaymentTypeFromrdr(NullableDataReader rdr)
        {
            PaymentTypeInfo info = new PaymentTypeInfo();
            info.TypeId = rdr.GetInt32("TypeId");
            info.TypeName = rdr.GetString("TypeName");
            info.Intro = rdr.GetString("Intro");
            info.Discount = Convert.IsDBNull(rdr["Discount"]) ? 0f : float.Parse(rdr["Discount"].ToString(), (IFormatProvider) null);
            info.IsDefault = rdr.GetBoolean("IsDefault");
            info.IsDisabled = rdr.GetBoolean("IsDisabled");
            info.OrderId = rdr.GetInt32("OrderId");
            info.Category = rdr.GetInt32("Category");
            return info;
        }

        public bool Update(PaymentTypeInfo paymentTypeInfo)
        {
            Parameters cmdParams = GetParameters(paymentTypeInfo);
            cmdParams.AddInParameter("@typeId", DbType.Int32, paymentTypeInfo.TypeId);
            cmdParams.AddInParameter("@orderId", DbType.Int32, paymentTypeInfo.OrderId);
            return DBHelper.ExecuteProc("PR_Shop_PaymentType_Update", cmdParams);
        }
    }
}


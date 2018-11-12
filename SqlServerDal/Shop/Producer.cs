namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public sealed class Producer : IProducer
    {
        private int m_TotalOfProducer;

        public bool Add(ProducerInfo producerInfo)
        {
            if (producerInfo.ProducerId <= 0)
            {
                producerInfo.ProducerId = this.GetMaxId() + 1;
            }
            Parameters cmdParams = GetParameters(producerInfo);
            return DBHelper.ExecuteProc("PR_Shop_Producer_Add", cmdParams);
        }

        public bool Delete(string producerId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProducerID", DbType.String, producerId);
            return DBHelper.ExecuteProc("PR_Shop_Producer_Delete", cmdParams);
        }

        public IList<ProducerInfo> GetList()
        {
            IList<ProducerInfo> list = new List<ProducerInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Producer WHERE Passed = 1 ORDER BY ProducerID DESC"))
            {
                while (reader.Read())
                {
                    list.Add(producerInfoFromrdataReader(reader));
                }
            }
            return list;
        }

        public IList<ProducerInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, int producerType, bool isPassed)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String);
            database.SetParameterValue(storedProcCommand, "@StartRows", startRowIndexId);
            database.SetParameterValue(storedProcCommand, "@PageSize", maxNumberRows);
            database.SetParameterValue(storedProcCommand, "@SortColumn", "ProducerId");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "*");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_Producer");
            string str = "1 = 1 ";
            if (isPassed)
            {
                str = str + "AND Passed = 1 ";
            }
            if (string.IsNullOrEmpty(searchType))
            {
                if (producerType >= 0)
                {
                    str = str + "AND ProducerType = " + producerType;
                }
            }
            else
            {
                str = str + "AND " + DBHelper.FilterBadChar(searchType) + " LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
            }
            database.SetParameterValue(storedProcCommand, "@Filter", str);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            IList<ProducerInfo> list = new List<ProducerInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(producerInfoFromrdataReader(reader));
                }
            }
            this.m_TotalOfProducer = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Producer", "ProducerID");
        }

        private static Parameters GetParameters(ProducerInfo producerInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ProducerID", DbType.Int32, producerInfo.ProducerId);
            parameters.AddInParameter("@ProducerName", DbType.String, producerInfo.ProducerName);
            parameters.AddInParameter("@ProducerShortName", DbType.String, producerInfo.ProducerShortName);
            parameters.AddInParameter("@Birthday", DbType.DateTime, producerInfo.BirthDay);
            parameters.AddInParameter("@Address", DbType.String, producerInfo.Address);
            parameters.AddInParameter("@Phone", DbType.String, producerInfo.Phone);
            parameters.AddInParameter("@Fax", DbType.String, producerInfo.Fax);
            parameters.AddInParameter("@Postcode", DbType.String, producerInfo.Postcode);
            parameters.AddInParameter("@Homepage", DbType.String, producerInfo.Homepage);
            parameters.AddInParameter("@Email", DbType.String, producerInfo.Email);
            parameters.AddInParameter("@ProducerIntro", DbType.String, producerInfo.ProducerIntro);
            parameters.AddInParameter("@ProducerPhoto", DbType.String, producerInfo.ProducerPhoto);
            parameters.AddInParameter("@ProducerType", DbType.Int32, producerInfo.ProducerType);
            return parameters;
        }

        public ProducerInfo GetProducerById(int producerId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProducerID", DbType.Int32, producerId);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Shop_Producer_GetById", cmdParams)))
            {
                if (reader.Read())
                {
                    return producerInfoFromrdataReader(reader);
                }
                return new ProducerInfo(true);
            }
        }

        public int GetTotalOfProducer()
        {
            return this.m_TotalOfProducer;
        }

        private static ProducerInfo producerInfoFromrdataReader(NullableDataReader dataReader)
        {
            ProducerInfo info = new ProducerInfo();
            info.ProducerId = dataReader.GetInt32("ProducerID");
            info.ProducerName = dataReader.GetString("ProducerName");
            info.ProducerShortName = dataReader.GetString("ProducerShortName");
            info.BirthDay = dataReader.GetNullableDateTime("BirthDay");
            info.Address = dataReader.GetString("Address");
            info.Phone = dataReader.GetString("Phone");
            info.Fax = dataReader.GetString("Fax");
            info.Postcode = dataReader.GetString("Postcode");
            info.Homepage = dataReader.GetString("Homepage");
            info.Email = dataReader.GetString("Email");
            info.ProducerIntro = dataReader.GetString("ProducerIntro");
            info.ProducerPhoto = dataReader.GetString("ProducerPhoto");
            info.ProducerType = dataReader.GetInt32("ProducerType");
            info.Passed = dataReader.GetBoolean("Passed");
            info.OnTop = dataReader.GetBoolean("OnTop");
            info.IsElite = dataReader.GetBoolean("IsElite");
            return info;
        }

        public bool ProducerNameExists(string producerName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Producer_ProducerNameExists");
            database.AddInParameter(storedProcCommand, "@ProducerName", DbType.String, producerName);
            database.AddParameter(storedProcCommand, "@RETURN_VALUE", DbType.String, ParameterDirection.ReturnValue, "", DataRowVersion.Current, null);
            database.ExecuteNonQuery(storedProcCommand);
            int num = (int) storedProcCommand.Parameters["@RETURN_VALUE"].Value;
            return (num == 1);
        }

        public bool SetElite(int producerId, bool value)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProducerID", DbType.Int32, producerId);
            cmdParams.AddInParameter("@IsElite", DbType.Boolean, value);
            return DBHelper.ExecuteProc("PR_Shop_Producer_SetElite", cmdParams);
        }

        public bool SetOnTop(int producerId, bool value)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProducerID", DbType.Int32, producerId);
            cmdParams.AddInParameter("@OnTop", DbType.Boolean, value);
            return DBHelper.ExecuteProc("PR_Shop_Producer_SetOnTop", cmdParams);
        }

        public bool SetPassed(int producerId, bool value)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProducerID", DbType.Int32, producerId);
            cmdParams.AddInParameter("@Passed", DbType.Boolean, value);
            return DBHelper.ExecuteProc("PR_Shop_Producer_SetPassed", cmdParams);
        }

        public bool Update(ProducerInfo producerInfo)
        {
            Parameters cmdParams = GetParameters(producerInfo);
            return DBHelper.ExecuteProc("PR_Shop_Producer_Update", cmdParams);
        }
    }
}


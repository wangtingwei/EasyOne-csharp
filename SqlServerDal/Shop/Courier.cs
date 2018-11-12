namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Courier : ICourier
    {
        public bool Add(CourierInfo courier)
        {
            Parameters cmdParams = GetParameters(courier);
            return DBHelper.ExecuteSql("INSERT PE_Courier(ShortName, FullName, Address, Telephone, Contacter, SearchUrl) VALUES (@ShortName, @FullName, @Address, @Telephone,@Contacter, @SearchUrl)", cmdParams);
        }

        public bool CourierIdExists(int courierId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CourierId", DbType.Int32, courierId);
            return DBHelper.ExistsSql("SELECT TOP 1 * FROM PE_DeliverItem WHERE CourierId = @CourierId", cmdParams);
        }

        public bool Delete(int courierId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CourierId", DbType.Int32, courierId);
            return DBHelper.ExecuteSql("DELETE FROM PE_Courier WHERE CourierId =@CourierId", cmdParams);
        }

        public CourierInfo GetCourier(int courierId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CourierId", DbType.Int32, courierId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Courier WHERE CourierId = @CourierId", cmdParams))
            {
                if (reader.Read())
                {
                    return GetItemInfoFromrdataReader(reader);
                }
                return new CourierInfo(true);
            }
        }

        public IList<CourierInfo> GetCourierList()
        {
            IList<CourierInfo> list = new List<CourierInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Courier"))
            {
                while (reader.Read())
                {
                    list.Add(GetItemInfoFromrdataReader(reader));
                }
            }
            return list;
        }

        public static CourierInfo GetItemInfoFromrdataReader(NullableDataReader dataReader)
        {
            CourierInfo info = new CourierInfo();
            info.CourierId = dataReader.GetInt32("CourierId");
            info.ShortName = dataReader.GetString("ShortName");
            info.FullName = dataReader.GetString("FullName");
            info.Address = dataReader.GetString("Address");
            info.Telephone = dataReader.GetString("Telephone");
            info.Contacter = dataReader.GetString("Contacter");
            info.SearchUrl = dataReader.GetString("SearchUrl");
            return info;
        }

        private static Parameters GetParameters(CourierInfo courier)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@CourierId", DbType.Int32, courier.CourierId);
            parameters.AddInParameter("@ShortName", DbType.String, courier.ShortName);
            parameters.AddInParameter("@FullName", DbType.String, courier.FullName);
            parameters.AddInParameter("@Address", DbType.String, courier.Address);
            parameters.AddInParameter("@Telephone", DbType.String, courier.Telephone);
            parameters.AddInParameter("@Contacter", DbType.String, courier.Contacter);
            parameters.AddInParameter("@SearchUrl", DbType.String, courier.SearchUrl);
            return parameters;
        }

        public bool Update(CourierInfo courier)
        {
            Parameters cmdParams = GetParameters(courier);
            return DBHelper.ExecuteSql("UPDATE PE_Courier SET ShortName = @ShortName, FullName = @FullName, Address = @Address, Telephone = @Telephone, Contacter = @Contacter, SearchUrl = @SearchUrl WHERE CourierId = @CourierId", cmdParams);
        }
    }
}


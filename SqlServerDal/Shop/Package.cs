namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Package : IPackage
    {
        public bool Add(PackageInfo packageInfo)
        {
            return DBHelper.ExecuteSql("INSERT INTO PE_Package(PackageName, PackageWeight, GoodsWeightMin, GoodsWeightMax) VALUES (@PackageName, @PackageWeight, @GoodsWeightMin, @GoodsWeightMax)", GetParameters(packageInfo));
        }

        public bool Delete(int id)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Package WHERE PackageId = @PackageId", new Parameters("@PackageId", DbType.Int32, id));
        }

        public bool Delete(string id)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Package WHERE PackageId IN ( " + DBHelper.ToValidId(id) + " )");
        }

        public bool ExistsPackage(string packageName)
        {
            return DBHelper.Exists(CommandType.Text, "SELECT TOP 1 PackageId FROM PE_Package WHERE packageName = @packageName", new Parameters("@packageName", DbType.String, packageName));
        }

        public bool ExistsPackage(double goodsWeightMin, double goodsWeightMax)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Min", DbType.Double, goodsWeightMin);
            cmdParams.AddInParameter("@Max", DbType.Double, goodsWeightMax);
            return DBHelper.Exists(CommandType.Text, "SELECT TOP 1 * FROM PE_Package WHERE \r\n(goodsweightmin <= @Min AND @Min < goodsweightMax) OR (goodsweightmin < @Max AND @Max < goodsweightMax)\r\nor((goodsweightmin <= @Min) AND (@Max < goodsweightMax))", cmdParams);
        }

        public bool ExistsPackage(string packageName, int currentPackageId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@packageName", DbType.String, packageName);
            cmdParams.AddInParameter("@packageId", DbType.Int32, currentPackageId);
            return DBHelper.Exists(CommandType.Text, "SELECT PackageId FROM PE_Package WHERE packageName = @packageName AND PackageId<>@packageId", cmdParams);
        }

        public bool ExistsPackage(double goodsWeightMin, double goodsWeightMax, int currentPackageId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Min", DbType.Double, goodsWeightMin);
            cmdParams.AddInParameter("@Max", DbType.Double, goodsWeightMax);
            cmdParams.AddInParameter("@packageId", DbType.Int32, currentPackageId);
            return DBHelper.Exists(CommandType.Text, "SELECT TOP 1 * FROM PE_Package WHERE PackageID<>@packageId AND\r\n((goodsweightmin <= @Min AND @Min < goodsweightMax) OR (goodsweightmin < @Max AND @Max < goodsweightMax)\r\nor((goodsweightmin <= @Min) AND (@Max < goodsweightMax)))", cmdParams);
        }

        public IList<PackageInfo> GetList()
        {
            IList<PackageInfo> list = new List<PackageInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * From PE_Package ORDER BY GoodsWeightMin, GoodsWeightMax"))
            {
                while (reader.Read())
                {
                    list.Add(PackageFromrdr(reader));
                }
            }
            return list;
        }

        public PackageInfo GetPackageByGoodsWeight(double goodsWeight)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 1 * FROM PE_Package WHERE GoodsWeightMin <= @GoodsWeight AND GoodsWeightMax > @GoodsWeight ORDER BY GoodsWeightMin, GoodsWeightMax", new Parameters("@GoodsWeight", DbType.Double, goodsWeight)))
            {
                if (reader.Read())
                {
                    return PackageFromrdr(reader);
                }
                return new PackageInfo(true);
            }
        }

        public PackageInfo GetPackageById(int id)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Package WHERE PackageId = @PackageId", new Parameters("@PackageId", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    return PackageFromrdr(reader);
                }
                return new PackageInfo(true);
            }
        }

        private static Parameters GetParameters(PackageInfo packageInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@PackageName", DbType.String, packageInfo.PackageName);
            parameters.AddInParameter("@PackageWeight", DbType.Double, packageInfo.PackageWeight);
            parameters.AddInParameter("@GoodsWeightMin", DbType.Double, packageInfo.GoodsWeightMin);
            parameters.AddInParameter("@GoodsWeightMax", DbType.Double, packageInfo.GoodsWeightMax);
            return parameters;
        }

        private static PackageInfo PackageFromrdr(NullableDataReader rdr)
        {
            PackageInfo info = new PackageInfo();
            info.PackageId = rdr.GetInt32("PackageId");
            info.PackageName = rdr.GetString("PackageName");
            info.PackageWeight = rdr.GetDouble("PackageWeight");
            info.GoodsWeightMin = rdr.GetDouble("GoodsWeightMin");
            info.GoodsWeightMax = rdr.GetDouble("GoodsWeightMax");
            return info;
        }

        public bool Update(PackageInfo packageInfo)
        {
            Parameters cmdParams = GetParameters(packageInfo);
            cmdParams.AddInParameter("PackageId", DbType.Int32, packageInfo.PackageId);
            return DBHelper.ExecuteSql("UPDATE PE_Package SET PackageName = @PackageName, PackageWeight = @PackageWeight, GoodsWeightMin = @GoodsWeightMin, GoodsWeightMax = @GoodsWeightMax WHERE PackageId = @PackageId", cmdParams);
        }
    }
}


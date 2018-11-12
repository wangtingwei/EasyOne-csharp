namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Package
    {
        private static readonly IPackage dal = DataAccess.CreatePackage();

        private Package()
        {
        }

        public static bool Add(PackageInfo packageInfo)
        {
            return ((packageInfo != null) && dal.Add(packageInfo));
        }

        public static bool BatchDelete(string id)
        {
            return (DataValidator.IsValidId(id) && dal.Delete(id));
        }

        public static bool Delete(int packageId)
        {
            return dal.Delete(packageId);
        }

        public static bool ExistsPackage(string packageName)
        {
            if (string.IsNullOrEmpty(packageName))
            {
                return false;
            }
            return dal.ExistsPackage(packageName);
        }

        public static bool ExistsPackage(double goodsWeightMin, double goodsWeightMax)
        {
            return dal.ExistsPackage(goodsWeightMin, goodsWeightMax);
        }

        public static bool ExistsPackage(string packageName, int currentPackageId)
        {
            return ((!string.IsNullOrEmpty(packageName) && (currentPackageId > 0)) && dal.ExistsPackage(packageName, currentPackageId));
        }

        public static bool ExistsPackage(double goodsWeightMin, double goodsWeightMax, int currentPackageId)
        {
            if (currentPackageId <= 0)
            {
                return false;
            }
            return dal.ExistsPackage(goodsWeightMin, goodsWeightMax, currentPackageId);
        }

        public static IList<PackageInfo> GetList()
        {
            return dal.GetList();
        }

        public static PackageInfo GetPackageByGoodsWeight(double goodsWeight)
        {
            return dal.GetPackageByGoodsWeight(goodsWeight);
        }

        public static PackageInfo GetPackageById(int id)
        {
            return dal.GetPackageById(id);
        }

        public static bool Update(PackageInfo packageInfo)
        {
            return ((packageInfo != null) && dal.Update(packageInfo));
        }
    }
}


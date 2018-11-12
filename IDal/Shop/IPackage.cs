namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IPackage
    {
        bool Add(PackageInfo packageInfo);
        bool Delete(int id);
        bool Delete(string id);
        bool ExistsPackage(string packageName);
        bool ExistsPackage(double goodsWeightMin, double goodsWeightMax);
        bool ExistsPackage(string packageName, int currentPackageId);
        bool ExistsPackage(double goodsWeightMin, double goodsWeightMax, int currentPackageId);
        IList<PackageInfo> GetList();
        PackageInfo GetPackageByGoodsWeight(double goodsWeight);
        PackageInfo GetPackageById(int id);
        bool Update(PackageInfo packageInfo);
    }
}


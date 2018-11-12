namespace EasyOne.Crm
{
    using EasyOne.Common;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Address
    {
        private static readonly IAddress dal = DataAccess.CreateAddress();

        private Address()
        {
        }

        public static bool Add(AddressInfo addressInfo)
        {
            if (addressInfo == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(addressInfo.UserName))
            {
                return false;
            }
            return dal.Add(addressInfo);
        }

        public static bool Delete(string addressList, string userName)
        {
            return (DataValidator.IsValidId(addressList) && dal.Delete(addressList, userName));
        }

        public static bool DeleteById(int addressId)
        {
            return dal.DeleteById(addressId);
        }

        public static bool DeleteByUserName(string userName)
        {
            return dal.DeleteByUserName(userName);
        }

        public static AddressInfo GetAddressById(int addressId)
        {
            return dal.GetAddressById(addressId);
        }

        public static IList<AddressInfo> GetAddressList(int startRowIndexId, int maxNumberRows, string type, string key)
        {
            return dal.GetAddressList(startRowIndexId, maxNumberRows, type, key);
        }

        public static IList<AddressInfo> GetAddressListByUserName(string userName)
        {
            return dal.GetAddressListByUserName(userName);
        }

        public static AddressInfo GetDefaultAddressByUserName(string userName)
        {
            return dal.GetDefaultAddressByUserName(userName);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static int GetTotalOfAddress(string type, string key)
        {
            return dal.GetTotalOfAddress();
        }

        public static bool SetDefault(int addressId, string userName)
        {
            return dal.SetDefault(addressId, userName);
        }

        public static bool Update(AddressInfo addressInfo)
        {
            if (addressInfo == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(addressInfo.UserName))
            {
                return false;
            }
            return dal.Update(addressInfo);
        }
    }
}


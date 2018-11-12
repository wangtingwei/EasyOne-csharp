namespace EasyOne.IDal.Crm
{
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;

    public interface IAddress
    {
        bool Add(AddressInfo addressInfo);
        bool Delete(string addressList, string userName);
        bool DeleteById(int addressId);
        bool DeleteByUserName(string userName);
        AddressInfo GetAddressById(int addressId);
        IList<AddressInfo> GetAddressList(int startRowIndexId, int maxNumberRows, string type, string key);
        IList<AddressInfo> GetAddressListByUserName(string userName);
        AddressInfo GetDefaultAddressByUserName(string userName);
        int GetMaxId();
        int GetTotalOfAddress();
        bool SetDefault(int addressId, string userName);
        bool Update(AddressInfo addressInfo);
    }
}


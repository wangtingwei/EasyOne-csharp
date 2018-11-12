namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IShoppingCart
    {
        bool Add(ShoppingCartInfo shoppingcartinfo);
        bool Delete(DateTime datePart);
        bool Delete(string cartId);
        IList<ShoppingCartInfo> GetInfoByCart(string cartId, bool isPresent);
        IList<ShoppingCartInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword);
        int GetTotalOfShoppingCart();
        void UpdateInformState(string cartId, int state);
        void UpdateUserName(string cartId, string userName);
    }
}


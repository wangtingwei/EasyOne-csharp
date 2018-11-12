namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Contents;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Coupon
    {
        private static readonly ICoupon dal = DataAccess.CreateCoupon();

        private Coupon()
        {
        }

        public static bool Add(CouponInfo couponInfo)
        {
            return dal.Add(couponInfo);
        }

        private static bool CheckModel(OrderInfo orderInfo, string modelIdList)
        {
            IList<OrderItemInfo> infoListByOrderId = OrderItem.GetInfoListByOrderId(orderInfo.OrderId);
            string[] strArray = modelIdList.Split(new char[] { ',' });
            foreach (OrderItemInfo info in infoListByOrderId)
            {
                if (!string.IsNullOrEmpty(info.TableName))
                {
                    CommonModelInfo commonModelInfo = ContentManage.GetCommonModelInfo(info.ProductId, info.TableName);
                    foreach (string str in strArray)
                    {
                        if (commonModelInfo.ModelId.ToString() == str)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static bool CheckModelForUseCoupon(string cartId, string modelIdList)
        {
            IList<ShoppingCartInfo> infoByCart = ShoppingCart.GetInfoByCart(cartId, false);
            string[] strArray = modelIdList.Split(new char[] { ',' });
            foreach (ShoppingCartInfo info in infoByCart)
            {
                CommonModelInfo commonModelInfo = ContentManage.GetCommonModelInfo(info.ProductId, info.TableName);
                foreach (string str in strArray)
                {
                    if (commonModelInfo.ModelId.ToString() == str)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool CheckProduct(OrderInfo orderInfo, string productList)
        {
            IList<OrderItemInfo> infoListByOrderId = OrderItem.GetInfoListByOrderId(orderInfo.OrderId);
            string[] strArray = productList.Split(new char[] { ',' });
            foreach (OrderItemInfo info in infoListByOrderId)
            {
                if (!string.IsNullOrEmpty(info.TableName))
                {
                    ProductInfo productById = Product.GetProductById(info.ProductId, info.TableName);
                    foreach (string str in strArray)
                    {
                        if (productById.ProductId.ToString() == str)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static bool CheckProductForUseCoupon(string cartId, string productList)
        {
            IList<ShoppingCartInfo> infoByCart = ShoppingCart.GetInfoByCart(cartId, false);
            string[] strArray = productList.Split(new char[] { ',' });
            foreach (ShoppingCartInfo info in infoByCart)
            {
                if (!string.IsNullOrEmpty(info.TableName))
                {
                    ProductInfo productById = Product.GetProductById(info.ProductId, info.TableName);
                    foreach (string str in strArray)
                    {
                        if (productById.ProductId.ToString() == str)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static int CheckUsePurview(CouponInfo couponInfo, CouponItemInfo itemInfo, decimal totalMoney, string cartId)
        {
            if (itemInfo.UseTimes >= couponInfo.LimitNum)
            {
                return 1;
            }
            if (DateTime.Today > couponInfo.UseEndDate)
            {
                return 2;
            }
            if (totalMoney < couponInfo.OrderTotalMoney)
            {
                return 3;
            }
            switch (couponInfo.ProductLimitType)
            {
                case ProductLimitType.NominateModel:
                    if (CheckModelForUseCoupon(cartId, couponInfo.ProductLimitContent))
                    {
                        break;
                    }
                    return 4;

                case ProductLimitType.NominateProduct:
                    if (CheckProductForUseCoupon(cartId, couponInfo.ProductLimitContent))
                    {
                        break;
                    }
                    return 5;
            }
            return 0x63;
        }

        public static void Create(OrderInfo orderInfo)
        {
            if (!string.IsNullOrEmpty(orderInfo.UserName))
            {
                IList<CouponInfo> list = GetList();
                IList<CouponInfo> list2 = new List<CouponInfo>();
                foreach (CouponInfo info in list)
                {
                    bool flag = false;
                    switch (info.CouponCreateType)
                    {
                        case CouponCreateType.AllProduct:
                            flag = true;
                            break;

                        case CouponCreateType.NominateModel:
                            flag = CheckModel(orderInfo, info.CouponCreateContent);
                            break;

                        case CouponCreateType.NominateProduct:
                            flag = CheckProduct(orderInfo, info.CouponCreateContent);
                            break;

                        case CouponCreateType.OrderTotalMoney:
                            if (orderInfo.MoneyTotal >= DataConverter.CDecimal(info.CouponCreateContent))
                            {
                                flag = true;
                            }
                            break;
                    }
                    if (flag)
                    {
                        list2.Add(info);
                    }
                }
                IList<CouponInfo> list3 = new List<CouponInfo>();
                UserInfo usersByUserName = Users.GetUsersByUserName(orderInfo.UserName);
            Label_0137:
                foreach (CouponInfo info3 in list2)
                {
                    foreach (string str in info3.UserGroup.Split(new char[] { ',' }))
                    {
                        if (str == usersByUserName.GroupId.ToString())
                        {
                            list3.Add(info3);
                            goto Label_0137;
                        }
                    }
                }
                foreach (CouponInfo info4 in list3)
                {
                    CreateCoupon(info4, usersByUserName.UserId);
                }
            }
        }

        private static void CreateCoupon(CouponInfo couponInfo, int userId)
        {
            string randStringByPattern = RandomManage.GetRandStringByPattern(couponInfo.CouponNumPattern);
            CouponItemInfo couponItemInfo = new CouponItemInfo();
            couponItemInfo.CouponId = couponInfo.CouponId;
            couponItemInfo.CouponNum = randStringByPattern;
            couponItemInfo.UserId = userId;
            couponItemInfo.OrderId = 0;
            CouponItem.Add(couponItemInfo);
        }

        public static bool Delete(string couponId)
        {
            if (DataValidator.IsValidId(couponId) && dal.Delete(couponId))
            {
                CouponItem.Delete(couponId);
                return true;
            }
            return false;
        }

        public static IList<CouponDetailInfo> GetAllDetailList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            return dal.GetAllDetailList(startRowIndexId, maxNumberRows, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static int GetAllTotalOfCoupon(int searchType, string keyword)
        {
            return dal.GetAllTotalOfCoupon();
        }

        public static CouponInfo GetCouponInfoById(int couponId)
        {
            return dal.GetCouponInfoById(couponId);
        }

        public static IList<CouponDetailInfo> GetDetailList(int startRowIndexId, int maxNumberRows, int userId)
        {
            return dal.GetDetailList(startRowIndexId, maxNumberRows, userId);
        }

        public static IList<CouponInfo> GetList()
        {
            return dal.GetList();
        }

        public static IList<CouponInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetList(startRowIndexId, maxNumberRows);
        }

        public static int GetTotalOfCoupon()
        {
            return dal.GetTotalOfCoupon();
        }

        public static int GetTotalOfCoupon(int userId)
        {
            return GetTotalOfCoupon();
        }

        public static bool SetState(int couponId, int state)
        {
            return dal.SetState(couponId, state);
        }

        public static bool Update(CouponInfo couponInfo)
        {
            return dal.Update(couponInfo);
        }
    }
}


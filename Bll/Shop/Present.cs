namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Present
    {
        private static readonly IPresent dal = DataAccess.CreatePresent();

        private Present()
        {
        }

        public static bool AddPresent(PresentInfo info)
        {
            if (dal.AddPresent(info))
            {
                AddStockInfo(info);
                return true;
            }
            return false;
        }

        private static void AddStockInfo(PresentInfo presentInfo)
        {
            StockInfo stockInfo = new StockInfo();
            stockInfo.StockId = StockManage.GetMaxId() + 1;
            stockInfo.StockNum = StockItem.GetInStockNum();
            stockInfo.InputTime = DateTime.Now;
            stockInfo.StockType = StockType.InStock;
            stockInfo.Inputer = PEContext.Current.Admin.AdminName;
            stockInfo.Remark = "商品库存初始";
            if (StockManage.Add(stockInfo))
            {
                StockItemInfo info = new StockItemInfo();
                info.Amount = presentInfo.Stocks;
                info.Price = presentInfo.Price;
                info.ProductId = presentInfo.PresentId;
                info.TableName = string.Empty;
                info.Property = string.Empty;
                info.ProductNum = presentInfo.PresentNum;
                info.Unit = presentInfo.Unit;
                info.ProductName = presentInfo.PresentName;
                StockItem.Add(info, stockInfo.StockId);
            }
        }

        public static bool AddStocks(int id, int quantity)
        {
            return dal.AddStocks(id, quantity);
        }

        public static bool DeletePresents(string idList)
        {
            return (DataValidator.IsValidId(idList) && dal.DeletePresents(idList));
        }

        public static IList<PresentInfo> GetPresentByCharacter(ProductCharacter productCharacter)
        {
            return dal.GetPresentByCharacter(productCharacter);
        }

        public static PresentInfo GetPresentById(int id)
        {
            return dal.GetPresentById(id);
        }

        public static PresentInfo GetPresentByPresentNum(string presentNum)
        {
            return dal.GetPresentByPresentNum(presentNum);
        }

        public static Dictionary<int, string> GetPresentList(string idList)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            if (DataValidator.IsValidId(idList))
            {
                foreach (string str in idList.Split(new char[] { ',' }))
                {
                    int id = DataConverter.CLng(str);
                    string presentNameById = GetPresentNameById(id);
                    if (!string.IsNullOrEmpty(presentNameById))
                    {
                        dictionary.Add(id, presentNameById);
                    }
                }
            }
            return dictionary;
        }

        public static IList<PresentInfo> GetPresentList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetPresentList(startRowIndexId, maxNumberRows);
        }

        public static IList<PresentInfo> GetPresentList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            return dal.GetPresentList(startRowIndexId, maxNumberRows, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static string GetPresentNameById(int id)
        {
            return dal.GetPresentNameById(id);
        }

        public static int GetPresentTotal()
        {
            return dal.GetPresentTotal();
        }

        public static int GetPresentTotal(int searchType, string keyword)
        {
            return GetPresentTotal();
        }

        public static IList<string> GetUnitList()
        {
            return dal.GetUnitList();
        }

        public static bool UpdatePressent(PresentInfo info)
        {
            return dal.UpdatePressent(info);
        }
    }
}


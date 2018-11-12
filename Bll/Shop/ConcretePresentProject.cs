namespace EasyOne.Shop
{
    using EasyOne.Model.Shop;
    using System;

    public class ConcretePresentProject : AbstractItemInfo
    {
        private int m_PresentId;
        private PresentProjectInfo m_PresentProjectInfo;

        public ConcretePresentProject(int presentId, PresentProjectInfo presentProjectInfo)
        {
            this.m_PresentId = presentId;
            this.m_PresentProjectInfo = presentProjectInfo;
        }

        public override void GetItemInfo()
        {
            PresentInfo presentById = Present.GetPresentById(this.m_PresentId);
            if (!presentById.IsNull)
            {
                base.ProductName = presentById.PresentName;
                base.Unit = presentById.Unit;
                base.Amount = 1;
                base.PriceMarket = presentById.PriceMarket;
                base.Price = this.m_PresentProjectInfo.Price;
                base.ServiceTerm = presentById.ServiceTerm;
                base.ServiceTermUnit = presentById.ServiceTermUnit;
                base.Remark = "超值换购";
                base.BeginDate = DateTime.Today;
                base.PresentExp = 0;
                base.PresentMoney = 0M;
                base.PresentPoint = 0;
                base.TotalWeight = presentById.Weight;
                base.SubTotal = this.m_PresentProjectInfo.Price;
                base.SaleType = 2;
                base.Id = presentById.PresentId;
                base.isPresent = true;
                base.Weight = presentById.Weight;
                base.ProductCharacter = presentById.ProductCharacter;
            }
            else
            {
                base.IsNull = true;
            }
        }
    }
}


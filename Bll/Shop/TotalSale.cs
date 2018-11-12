namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using System;
    using EasyOne.DalFactory;

    public class TotalSale
    {
        private static readonly IStatistics dal = DataAccess.CreateStatistics();
        private DateTime m_BeginDate;
        private DateTime m_EndDate;
        private int m_HaveOrderOfMember;
        private int m_TotalHits;
        private int m_TotalMember;
        private decimal m_TotalMoney;
        private int m_TotalOrder;

        public TotalSale(DateTime beginDate, DateTime endDate)
        {
            this.m_BeginDate = beginDate;
            this.m_EndDate = endDate;
            this.m_TotalMoney = dal.GetTotalMoney(this.m_BeginDate, this.m_EndDate);
            this.m_TotalOrder = dal.GetTotalOrder(this.m_BeginDate, this.m_EndDate);
            this.m_HaveOrderOfMember = dal.GetHaveOrderOfMember();
            this.m_TotalMember = dal.GetTotalMember();
            this.m_TotalHits = dal.GetTotalHits();
        }

        public double AverageHaveOrderByMember
        {
            get
            {
                if (this.m_TotalMember == 0)
                {
                    return 0.0;
                }
                return (Convert.ToDouble(this.m_HaveOrderOfMember) / ((double) this.m_TotalMember));
            }
        }

        public decimal AverageMoneyByTotalHits
        {
            get
            {
                if (this.m_TotalHits == 0)
                {
                    return 0M;
                }
                return (this.m_TotalMoney / this.m_TotalHits);
            }
        }

        public decimal AverageMoneyByTotalOrder
        {
            get
            {
                if (this.m_TotalOrder == 0)
                {
                    return 0M;
                }
                return (this.m_TotalMoney / this.m_TotalOrder);
            }
        }

        public double AverageOrderByHits
        {
            get
            {
                if (this.m_TotalHits == 0)
                {
                    return 0.0;
                }
                return (Convert.ToDouble(this.m_TotalOrder) / ((double) this.m_TotalHits));
            }
        }

        public double AverageOrderOfMember
        {
            get
            {
                if (this.m_TotalMember == 0)
                {
                    return 0.0;
                }
                return (Convert.ToDouble(this.m_TotalOrder) / ((double) this.m_TotalMember));
            }
        }

        public int HaveOrderOfMember
        {
            get
            {
                return this.m_HaveOrderOfMember;
            }
        }

        public int TotalHits
        {
            get
            {
                return this.m_TotalHits;
            }
        }

        public int TotalMember
        {
            get
            {
                return this.m_TotalMember;
            }
        }

        public decimal TotalMoney
        {
            get
            {
                return this.m_TotalMoney;
            }
        }

        public int TotalOrder
        {
            get
            {
                return this.m_TotalOrder;
            }
        }
    }
}


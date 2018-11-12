namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using System;
    using EasyOne.DalFactory;

    public class Saleroom
    {
        private static readonly IStatistics dal = DataAccess.CreateStatistics();
        private int m_Day;
        private int m_Month;
        private decimal m_Sale;
        private int m_Year;

        public Saleroom(int year)
        {
            this.Year = year;
            this.Sale = dal.GetSaleOfYear(DataConverter.CDate(year.ToString() + "-01-01"));
        }

        public Saleroom(int year, int month)
        {
            this.Year = year;
            this.Month = month;
            string str = month.ToString();
            if (str.Length == 1)
            {
                str = "0" + str;
            }
            DateTime saleTime = DataConverter.CDate(year.ToString() + "-" + str + "-01");
            this.Sale = dal.GetSaleOfMonth(saleTime);
        }

        public Saleroom(int year, int month, int day)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
            string str = month.ToString();
            if (str.Length == 1)
            {
                str = "0" + str;
            }
            string str2 = day.ToString();
            if (str2.Length == 1)
            {
                str2 = "0" + str2;
            }
            this.Sale = dal.GetSaleOfDay(DataConverter.CDate(year.ToString() + "-" + str + "-" + str2));
        }

        public int Day
        {
            get
            {
                return this.m_Day;
            }
            set
            {
                this.m_Day = value;
            }
        }

        public int Month
        {
            get
            {
                return this.m_Month;
            }
            set
            {
                this.m_Month = value;
            }
        }

        public decimal Sale
        {
            get
            {
                return this.m_Sale;
            }
            set
            {
                this.m_Sale = value;
            }
        }

        public int Year
        {
            get
            {
                return this.m_Year;
            }
            set
            {
                this.m_Year = value;
            }
        }
    }
}


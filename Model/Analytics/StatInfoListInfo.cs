namespace EasyOne.Model.Analytics
{
    using EasyOne.Model;
    using System;

    public class StatInfoListInfo : EasyOne.Model.Nullable
    {
        private int m_ChinaNum;
        private string m_DayMaxDate;
        private int m_DayMaxNum;
        private int m_DayNum;
        private int m_HourMaxNum;
        private string m_HourMaxTime;
        private int m_HourNum;
        private int m_Interval;
        private int m_IntervalNum;
        private int m_KillRefresh;
        private int m_MasterTimeZone;
        private string m_MonthMaxDate;
        private int m_MonthMaxNum;
        private int m_MonthNum;
        private string m_OldDay;
        private string m_OldHour;
        private string m_OldMonth;
        private int m_OldTotalNum;
        private int m_OldTotalView;
        private int m_OnlineTime;
        private int m_OtherNum;
        private string m_RegFieldsFill;
        private string m_StartDate;
        private int m_TotalNum;
        private int m_TotalView;
        private int m_VisitRecord;

        public StatInfoListInfo()
        {
        }

        public StatInfoListInfo(bool value)
        {
            base.IsNull = value;
        }

        public int ChinaNum
        {
            get
            {
                return this.m_ChinaNum;
            }
            set
            {
                this.m_ChinaNum = value;
            }
        }

        public string DayMaxDate
        {
            get
            {
                return this.m_DayMaxDate;
            }
            set
            {
                this.m_DayMaxDate = value;
            }
        }

        public int DayMaxNum
        {
            get
            {
                return this.m_DayMaxNum;
            }
            set
            {
                this.m_DayMaxNum = value;
            }
        }

        public int DayNum
        {
            get
            {
                return this.m_DayNum;
            }
            set
            {
                this.m_DayNum = value;
            }
        }

        public int HourMaxNum
        {
            get
            {
                return this.m_HourMaxNum;
            }
            set
            {
                this.m_HourMaxNum = value;
            }
        }

        public string HourMaxTime
        {
            get
            {
                return this.m_HourMaxTime;
            }
            set
            {
                this.m_HourMaxTime = value;
            }
        }

        public int HourNum
        {
            get
            {
                return this.m_HourNum;
            }
            set
            {
                this.m_HourNum = value;
            }
        }

        public int Interval
        {
            get
            {
                return this.m_Interval;
            }
            set
            {
                this.m_Interval = value;
            }
        }

        public int IntervalNum
        {
            get
            {
                return this.m_IntervalNum;
            }
            set
            {
                this.m_IntervalNum = value;
            }
        }

        public int KillRefresh
        {
            get
            {
                return this.m_KillRefresh;
            }
            set
            {
                this.m_KillRefresh = value;
            }
        }

        public int MasterTimeZone
        {
            get
            {
                return this.m_MasterTimeZone;
            }
            set
            {
                this.m_MasterTimeZone = value;
            }
        }

        public string MonthMaxDate
        {
            get
            {
                return this.m_MonthMaxDate;
            }
            set
            {
                this.m_MonthMaxDate = value;
            }
        }

        public int MonthMaxNum
        {
            get
            {
                return this.m_MonthMaxNum;
            }
            set
            {
                this.m_MonthMaxNum = value;
            }
        }

        public int MonthNum
        {
            get
            {
                return this.m_MonthNum;
            }
            set
            {
                this.m_MonthNum = value;
            }
        }

        public string OldDay
        {
            get
            {
                return this.m_OldDay;
            }
            set
            {
                this.m_OldDay = value;
            }
        }

        public string OldHour
        {
            get
            {
                return this.m_OldHour;
            }
            set
            {
                this.m_OldHour = value;
            }
        }

        public string OldMonth
        {
            get
            {
                return this.m_OldMonth;
            }
            set
            {
                this.m_OldMonth = value;
            }
        }

        public int OldTotalNum
        {
            get
            {
                return this.m_OldTotalNum;
            }
            set
            {
                this.m_OldTotalNum = value;
            }
        }

        public int OldTotalView
        {
            get
            {
                return this.m_OldTotalView;
            }
            set
            {
                this.m_OldTotalView = value;
            }
        }

        public int OnlineTime
        {
            get
            {
                return this.m_OnlineTime;
            }
            set
            {
                this.m_OnlineTime = value;
            }
        }

        public int OtherNum
        {
            get
            {
                return this.m_OtherNum;
            }
            set
            {
                this.m_OtherNum = value;
            }
        }

        public string RegFieldsFill
        {
            get
            {
                return this.m_RegFieldsFill;
            }
            set
            {
                this.m_RegFieldsFill = value;
            }
        }

        public string StartDate
        {
            get
            {
                return this.m_StartDate;
            }
            set
            {
                this.m_StartDate = value;
            }
        }

        public int TotalNum
        {
            get
            {
                return this.m_TotalNum;
            }
            set
            {
                this.m_TotalNum = value;
            }
        }

        public int TotalView
        {
            get
            {
                return this.m_TotalView;
            }
            set
            {
                this.m_TotalView = value;
            }
        }

        public int VisitRecord
        {
            get
            {
                return this.m_VisitRecord;
            }
            set
            {
                this.m_VisitRecord = value;
            }
        }
    }
}


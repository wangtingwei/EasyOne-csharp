namespace EasyOne.Model.Analytics
{
    using EasyOne.Model;
    using System;

    public class StatUpdateInfo : EasyOne.Model.Nullable
    {
        private string m_Browser;
        private string m_Color;
        private double m_EncodeIP;
        private string m_IP;
        private string m_Keyword;
        private string m_Mozilla;
        private string m_Referer;
        private string m_Screen;
        private string m_System;
        private string m_Timezone;
        private int m_VisitNum;
        private int m_VisitTimezone;
        private string m_Weburl;

        public StatUpdateInfo()
        {
        }

        public StatUpdateInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Browser
        {
            get
            {
                return this.m_Browser;
            }
            set
            {
                this.m_Browser = value;
            }
        }

        public string Color
        {
            get
            {
                return this.m_Color;
            }
            set
            {
                this.m_Color = value;
            }
        }

        public double EncodeIP
        {
            get
            {
                return this.m_EncodeIP;
            }
            set
            {
                this.m_EncodeIP = value;
            }
        }

        public string IP
        {
            get
            {
                return this.m_IP;
            }
            set
            {
                this.m_IP = value;
            }
        }

        public string Keyword
        {
            get
            {
                return this.m_Keyword;
            }
            set
            {
                this.m_Keyword = value;
            }
        }

        public string Mozilla
        {
            get
            {
                return this.m_Mozilla;
            }
            set
            {
                this.m_Mozilla = value;
            }
        }

        public string Referer
        {
            get
            {
                return this.m_Referer;
            }
            set
            {
                this.m_Referer = value;
            }
        }

        public string Screen
        {
            get
            {
                return this.m_Screen;
            }
            set
            {
                this.m_Screen = value;
            }
        }

        public string System
        {
            get
            {
                return this.m_System;
            }
            set
            {
                this.m_System = value;
            }
        }

        public string Timezone
        {
            get
            {
                return this.m_Timezone;
            }
            set
            {
                this.m_Timezone = value;
            }
        }

        public int VisitNum
        {
            get
            {
                return this.m_VisitNum;
            }
            set
            {
                this.m_VisitNum = value;
            }
        }

        public int VisitTimezone
        {
            get
            {
                return this.m_VisitTimezone;
            }
            set
            {
                this.m_VisitTimezone = value;
            }
        }

        public string Weburl
        {
            get
            {
                return this.m_Weburl;
            }
            set
            {
                this.m_Weburl = value;
            }
        }
    }
}


namespace EasyOne.Model.Analytics
{
    using EasyOne.Model;
    using System;

    public class StatVisitorInfo : EasyOne.Model.Nullable
    {
        private string m_Address;
        private string m_Browser;
        private string m_Color;
        private int m_Id;
        private string m_Ip;
        private string m_Referer;
        private string m_Screen;
        private string m_System;
        private int m_Timezone;
        private DateTime m_VTime;

        public StatVisitorInfo()
        {
        }

        public StatVisitorInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Address
        {
            get
            {
                return this.m_Address;
            }
            set
            {
                this.m_Address = value;
            }
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

        public int Id
        {
            get
            {
                return this.m_Id;
            }
            set
            {
                this.m_Id = value;
            }
        }

        public string IP
        {
            get
            {
                return this.m_Ip;
            }
            set
            {
                this.m_Ip = value;
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

        public int Timezone
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

        public DateTime VTime
        {
            get
            {
                return this.m_VTime;
            }
            set
            {
                this.m_VTime = value;
            }
        }
    }
}


namespace EasyOne.Model.AD
{
    using EasyOne.Model;
    using System;

    public class AdvertisementInfo : EasyOne.Model.Nullable
    {
        private int m_ADId;
        private string m_ADIntro;
        private string m_ADName;
        private int m_ADType;
        private int m_Clicks;
        private bool m_CountClick;
        private bool m_CountView;
        private int m_FlashWmode;
        private int m_ImgHeight;
        private string m_ImgUrl;
        private int m_ImgWidth;
        private string m_LinkAlt;
        private int m_LinkTarget;
        private string m_LinkUrl;
        private DateTime m_OverdueDate;
        private bool m_Passed;
        private int m_Priority;
        private string m_Setting;
        private int m_UserId;
        private int m_Views;
        private string m_ZoneId;

        public AdvertisementInfo()
        {
        }

        public AdvertisementInfo(bool value)
        {
            base.IsNull = value;
        }

        public int ADId
        {
            get
            {
                return this.m_ADId;
            }
            set
            {
                this.m_ADId = value;
            }
        }

        public string ADIntro
        {
            get
            {
                return this.m_ADIntro;
            }
            set
            {
                this.m_ADIntro = value;
            }
        }

        public string ADName
        {
            get
            {
                return this.m_ADName;
            }
            set
            {
                this.m_ADName = value;
            }
        }

        public int ADType
        {
            get
            {
                return this.m_ADType;
            }
            set
            {
                this.m_ADType = value;
            }
        }

        public int Clicks
        {
            get
            {
                return this.m_Clicks;
            }
            set
            {
                this.m_Clicks = value;
            }
        }

        public bool CountClick
        {
            get
            {
                return this.m_CountClick;
            }
            set
            {
                this.m_CountClick = value;
            }
        }

        public bool CountView
        {
            get
            {
                return this.m_CountView;
            }
            set
            {
                this.m_CountView = value;
            }
        }

        public int Days
        {
            get
            {
                TimeSpan span = (TimeSpan) (this.m_OverdueDate.Date - DateTime.Today.Date);
                return span.Days;
            }
        }

        public int FlashWmode
        {
            get
            {
                return this.m_FlashWmode;
            }
            set
            {
                this.m_FlashWmode = value;
            }
        }

        public int ImgHeight
        {
            get
            {
                return this.m_ImgHeight;
            }
            set
            {
                this.m_ImgHeight = value;
            }
        }

        public string ImgUrl
        {
            get
            {
                return this.m_ImgUrl;
            }
            set
            {
                this.m_ImgUrl = value;
            }
        }

        public int ImgWidth
        {
            get
            {
                return this.m_ImgWidth;
            }
            set
            {
                this.m_ImgWidth = value;
            }
        }

        public string LinkAlt
        {
            get
            {
                return this.m_LinkAlt;
            }
            set
            {
                this.m_LinkAlt = value;
            }
        }

        public int LinkTarget
        {
            get
            {
                return this.m_LinkTarget;
            }
            set
            {
                this.m_LinkTarget = value;
            }
        }

        public string LinkUrl
        {
            get
            {
                return this.m_LinkUrl;
            }
            set
            {
                this.m_LinkUrl = value;
            }
        }

        public DateTime OverdueDate
        {
            get
            {
                return this.m_OverdueDate;
            }
            set
            {
                this.m_OverdueDate = value;
            }
        }

        public bool Passed
        {
            get
            {
                return this.m_Passed;
            }
            set
            {
                this.m_Passed = value;
            }
        }

        public int Priority
        {
            get
            {
                return this.m_Priority;
            }
            set
            {
                this.m_Priority = value;
            }
        }

        public string Setting
        {
            get
            {
                return this.m_Setting;
            }
            set
            {
                this.m_Setting = value;
            }
        }

        public int UserId
        {
            get
            {
                return this.m_UserId;
            }
            set
            {
                this.m_UserId = value;
            }
        }

        public int Views
        {
            get
            {
                return this.m_Views;
            }
            set
            {
                this.m_Views = value;
            }
        }

        public string ZoneId
        {
            get
            {
                return this.m_ZoneId;
            }
            set
            {
                this.m_ZoneId = value;
            }
        }
    }
}


namespace EasyOne.Model.AD
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class ADZoneInfo : EasyOne.Model.Nullable
    {
        private bool m_Active;
        private bool m_DefaultSetting;
        private string m_Seting;
        private int m_ShowType;
        private DateTime m_UpdateTime;
        private int m_ZoneHeight;
        private int m_ZoneId;
        private string m_ZoneIntro;
        private string m_ZoneJSName;
        private string m_ZoneName;
        private ADZoneType m_ZoneType;
        private int m_ZoneWidth;

        public ADZoneInfo()
        {
        }

        public ADZoneInfo(bool value)
        {
            base.IsNull = value;
        }

        public ADZoneInfo(int zoneId, string zoneName, string zoneJSName, string zoneIntro, ADZoneType zoneType, bool defaultSetting, string setting, int zoneWidth, int zoneHeight, bool active, int showType, DateTime updateTime)
        {
            this.m_ZoneId = zoneId;
            this.m_ZoneName = zoneName;
            this.m_ZoneJSName = zoneJSName;
            this.m_ZoneIntro = zoneIntro;
            this.m_ZoneType = zoneType;
            this.m_DefaultSetting = defaultSetting;
            this.m_Seting = setting;
            this.m_ZoneWidth = zoneWidth;
            this.m_ZoneHeight = zoneHeight;
            this.m_Active = active;
            this.m_ShowType = showType;
            this.m_UpdateTime = updateTime;
        }

        public bool Active
        {
            get
            {
                return this.m_Active;
            }
            set
            {
                this.m_Active = value;
            }
        }

        public bool DefaultSetting
        {
            get
            {
                return this.m_DefaultSetting;
            }
            set
            {
                this.m_DefaultSetting = value;
            }
        }

        public string Setting
        {
            get
            {
                return this.m_Seting;
            }
            set
            {
                this.m_Seting = value;
            }
        }

        public int ShowType
        {
            get
            {
                return this.m_ShowType;
            }
            set
            {
                this.m_ShowType = value;
            }
        }

        public DateTime UpdateTime
        {
            get
            {
                return this.m_UpdateTime;
            }
            set
            {
                this.m_UpdateTime = value;
            }
        }

        public int ZoneHeight
        {
            get
            {
                return this.m_ZoneHeight;
            }
            set
            {
                this.m_ZoneHeight = value;
            }
        }

        public int ZoneId
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

        public string ZoneIntro
        {
            get
            {
                return this.m_ZoneIntro;
            }
            set
            {
                this.m_ZoneIntro = value;
            }
        }

        public string ZoneJSName
        {
            get
            {
                return this.m_ZoneJSName;
            }
            set
            {
                this.m_ZoneJSName = value;
            }
        }

        public string ZoneName
        {
            get
            {
                return this.m_ZoneName;
            }
            set
            {
                this.m_ZoneName = value;
            }
        }

        public ADZoneType ZoneType
        {
            get
            {
                return this.m_ZoneType;
            }
            set
            {
                this.m_ZoneType = value;
            }
        }

        public int ZoneWidth
        {
            get
            {
                return this.m_ZoneWidth;
            }
            set
            {
                this.m_ZoneWidth = value;
            }
        }
    }
}


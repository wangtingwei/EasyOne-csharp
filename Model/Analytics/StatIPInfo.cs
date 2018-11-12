namespace EasyOne.Model.Analytics
{
    using EasyOne.Model;
    using System;

    public class StatIPInfo : EasyOne.Model.Nullable
    {
        private string m_Address;
        private double m_EndIP;
        private double m_StartIP;

        public StatIPInfo()
        {
        }

        public StatIPInfo(bool value)
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

        public double EndIP
        {
            get
            {
                return this.m_EndIP;
            }
            set
            {
                this.m_EndIP = value;
            }
        }

        public double StartIP
        {
            get
            {
                return this.m_StartIP;
            }
            set
            {
                this.m_StartIP = value;
            }
        }
    }
}


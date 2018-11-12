namespace EasyOne.Model.Analytics
{
    using EasyOne.Model;
    using System;

    public class StatOnlineInfo : EasyOne.Model.Nullable
    {
        private int m_Id;
        private DateTime m_LastTime;
        private DateTime m_OnTime;
        private string m_UserAgent;
        private string m_UserIP;
        private string m_UserPage;

        public StatOnlineInfo()
        {
        }

        public StatOnlineInfo(bool value)
        {
            base.IsNull = value;
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

        public DateTime LastTime
        {
            get
            {
                return this.m_LastTime;
            }
            set
            {
                this.m_LastTime = value;
            }
        }

        public DateTime OnTime
        {
            get
            {
                return this.m_OnTime;
            }
            set
            {
                this.m_OnTime = value;
            }
        }

        public string UserAgent
        {
            get
            {
                return this.m_UserAgent;
            }
            set
            {
                this.m_UserAgent = value;
            }
        }

        public string UserIP
        {
            get
            {
                return this.m_UserIP;
            }
            set
            {
                this.m_UserIP = value;
            }
        }

        public string UserPage
        {
            get
            {
                return this.m_UserPage;
            }
            set
            {
                this.m_UserPage = value;
            }
        }
    }
}


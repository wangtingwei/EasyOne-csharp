namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class DownServerInfo : EasyOne.Model.Nullable, IComparable<DownServerInfo>
    {
        private int m_OrderId;
        private int m_ServerId;
        private string m_ServerLogo;
        private string m_ServerName;
        private Uri m_ServerUrl;
        private int m_ShowType;

        public DownServerInfo()
        {
        }

        public DownServerInfo(bool value)
        {
            base.IsNull = value;
            this.m_ServerUrl = new Uri("http://www");
        }

        public int CompareTo(DownServerInfo other)
        {
            return this.m_OrderId.CompareTo(other.m_OrderId);
        }

        public int OrderId
        {
            get
            {
                return this.m_OrderId;
            }
            set
            {
                this.m_OrderId = value;
            }
        }

        public int ServerId
        {
            get
            {
                return this.m_ServerId;
            }
            set
            {
                this.m_ServerId = value;
            }
        }

        public string ServerLogo
        {
            get
            {
                return this.m_ServerLogo;
            }
            set
            {
                this.m_ServerLogo = value;
            }
        }

        public string ServerName
        {
            get
            {
                return this.m_ServerName;
            }
            set
            {
                this.m_ServerName = value;
            }
        }

        public Uri ServerUrl
        {
            get
            {
                return this.m_ServerUrl;
            }
            set
            {
                this.m_ServerUrl = value;
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
    }
}


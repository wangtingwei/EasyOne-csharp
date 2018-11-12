namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class AgentInfo : EasyOne.Model.Nullable
    {
        private string m_AgentName;
        private int m_ClientId;
        private decimal m_Margin;
        private decimal m_Money;
        private int m_OrderId;
        private string m_UserName;

        public AgentInfo()
        {
        }

        public AgentInfo(bool value)
        {
            base.IsNull = value;
        }

        public string AgentName
        {
            get
            {
                return this.m_AgentName;
            }
            set
            {
                this.m_AgentName = value;
            }
        }

        public int ClientId
        {
            get
            {
                return this.m_ClientId;
            }
            set
            {
                this.m_ClientId = value;
            }
        }

        public decimal Margin
        {
            get
            {
                return this.m_Margin;
            }
            set
            {
                this.m_Margin = value;
            }
        }

        public decimal Money
        {
            get
            {
                return this.m_Money;
            }
            set
            {
                this.m_Money = value;
            }
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

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }
    }
}


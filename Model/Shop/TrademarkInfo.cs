namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class TrademarkInfo : EasyOne.Model.Nullable
    {
        private int m_Hits;
        private bool m_IsElite;
        private bool m_onTop;
        private bool m_Passed;
        private int m_ProducerId;
        private int m_TrademarkId;
        private string m_TrademarkIntro;
        private string m_TrademarkName;
        private string m_TrademarkPhoto;
        private int m_TrademarkType;

        public TrademarkInfo()
        {
        }

        public TrademarkInfo(bool value)
        {
            base.IsNull = value;
        }

        public int Hits
        {
            get
            {
                return this.m_Hits;
            }
            set
            {
                this.m_Hits = value;
            }
        }

        public bool IsElite
        {
            get
            {
                return this.m_IsElite;
            }
            set
            {
                this.m_IsElite = value;
            }
        }

        public bool OnTop
        {
            get
            {
                return this.m_onTop;
            }
            set
            {
                this.m_onTop = value;
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

        public int ProducerId
        {
            get
            {
                return this.m_ProducerId;
            }
            set
            {
                this.m_ProducerId = value;
            }
        }

        public int TrademarkId
        {
            get
            {
                return this.m_TrademarkId;
            }
            set
            {
                this.m_TrademarkId = value;
            }
        }

        public string TrademarkIntro
        {
            get
            {
                return this.m_TrademarkIntro;
            }
            set
            {
                this.m_TrademarkIntro = value;
            }
        }

        public string TrademarkName
        {
            get
            {
                return this.m_TrademarkName;
            }
            set
            {
                this.m_TrademarkName = value;
            }
        }

        public string TrademarkPhoto
        {
            get
            {
                return this.m_TrademarkPhoto;
            }
            set
            {
                this.m_TrademarkPhoto = value;
            }
        }

        public int TrademarkType
        {
            get
            {
                return this.m_TrademarkType;
            }
            set
            {
                this.m_TrademarkType = value;
            }
        }
    }
}


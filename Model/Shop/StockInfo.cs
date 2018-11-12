namespace EasyOne.Model.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    [Serializable]
    public class StockInfo : EasyOne.Model.Nullable
    {
        private string m_Inputer;
        private DateTime m_InputTime;
        private string m_Remark;
        private int m_StockId;
        private string m_StockNum;
        private EasyOne.Enumerations.StockType m_StockType;

        public StockInfo()
        {
        }

        public StockInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Inputer
        {
            get
            {
                return this.m_Inputer;
            }
            set
            {
                this.m_Inputer = value;
            }
        }

        public DateTime InputTime
        {
            get
            {
                return this.m_InputTime;
            }
            set
            {
                this.m_InputTime = value;
            }
        }

        public string Remark
        {
            get
            {
                return this.m_Remark;
            }
            set
            {
                this.m_Remark = value;
            }
        }

        public int StockId
        {
            get
            {
                return this.m_StockId;
            }
            set
            {
                this.m_StockId = value;
            }
        }

        public string StockNum
        {
            get
            {
                return this.m_StockNum;
            }
            set
            {
                this.m_StockNum = value;
            }
        }

        public EasyOne.Enumerations.StockType StockType
        {
            get
            {
                return this.m_StockType;
            }
            set
            {
                this.m_StockType = value;
            }
        }
    }
}


namespace EasyOne.Model.Contents
{
    using EasyOne.Model;
    using System;

    public class ContentChargeInfo : EasyOne.Model.Nullable
    {
        private int m_ChargeType;
        private int m_DividePercent;
        private int m_GeneralId;
        private int m_InfoPoint;
        private int m_PitchTime;
        private int m_ReadTimes;

        public ContentChargeInfo()
        {
        }

        public ContentChargeInfo(bool value)
        {
            base.IsNull = value;
        }

        public int ChargeType
        {
            get
            {
                return this.m_ChargeType;
            }
            set
            {
                this.m_ChargeType = value;
            }
        }

        public int DividePercent
        {
            get
            {
                return this.m_DividePercent;
            }
            set
            {
                this.m_DividePercent = value;
            }
        }

        public int GeneralId
        {
            get
            {
                return this.m_GeneralId;
            }
            set
            {
                this.m_GeneralId = value;
            }
        }

        public int InfoPoint
        {
            get
            {
                return this.m_InfoPoint;
            }
            set
            {
                this.m_InfoPoint = value;
            }
        }

        public int PitchTime
        {
            get
            {
                return this.m_PitchTime;
            }
            set
            {
                this.m_PitchTime = value;
            }
        }

        public int ReadTimes
        {
            get
            {
                return this.m_ReadTimes;
            }
            set
            {
                this.m_ReadTimes = value;
            }
        }
    }
}


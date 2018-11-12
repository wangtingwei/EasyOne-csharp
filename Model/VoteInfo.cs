namespace EasyOne.Model
{
    using System;

    public class VoteInfo : EasyOne.Model.Nullable
    {
        private DateTime m_EndTime;
        private int m_GeneralId;
        private bool m_IsAlive;
        private int m_ItemType;
        private DateTime m_StartTime;
        private string m_VoteItem;
        private string m_VoteTitle;
        private int m_VoteTotalNumber;

        public VoteInfo()
        {
        }

        public VoteInfo(bool isNull)
        {
            base.IsNull = isNull;
        }

        public DateTime EndTime
        {
            get
            {
                return this.m_EndTime;
            }
            set
            {
                this.m_EndTime = value;
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

        public bool IsAlive
        {
            get
            {
                return this.m_IsAlive;
            }
            set
            {
                this.m_IsAlive = value;
            }
        }

        public int ItemType
        {
            get
            {
                return this.m_ItemType;
            }
            set
            {
                this.m_ItemType = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.m_StartTime;
            }
            set
            {
                this.m_StartTime = value;
            }
        }

        public string VoteItem
        {
            get
            {
                return this.m_VoteItem;
            }
            set
            {
                this.m_VoteItem = value;
            }
        }

        public string VoteTitle
        {
            get
            {
                return this.m_VoteTitle;
            }
            set
            {
                this.m_VoteTitle = value;
            }
        }

        public int VoteTotalNumber
        {
            get
            {
                return this.m_VoteTotalNumber;
            }
            set
            {
                this.m_VoteTotalNumber = value;
            }
        }
    }
}


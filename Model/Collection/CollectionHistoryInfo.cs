namespace EasyOne.Model.Collection
{
    using EasyOne.Model;
    using System;

    public class CollectionHistoryInfo : EasyOne.Model.Nullable
    {
        private DateTime m_CollectionTime;
        private int m_GeneralId;
        private int m_HistoryId;
        private int m_ItemId;
        private int m_ModelId;
        private string m_NewsUrl;
        private int m_NodeId;
        private bool m_Result;
        private string m_Title;

        public CollectionHistoryInfo()
        {
        }

        public CollectionHistoryInfo(bool value)
        {
            base.IsNull = value;
        }

        public DateTime CollectionTime
        {
            get
            {
                return this.m_CollectionTime;
            }
            set
            {
                this.m_CollectionTime = value;
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

        public int HistoryId
        {
            get
            {
                return this.m_HistoryId;
            }
            set
            {
                this.m_HistoryId = value;
            }
        }

        public int ItemId
        {
            get
            {
                return this.m_ItemId;
            }
            set
            {
                this.m_ItemId = value;
            }
        }

        public int ModelId
        {
            get
            {
                return this.m_ModelId;
            }
            set
            {
                this.m_ModelId = value;
            }
        }

        public string NewsUrl
        {
            get
            {
                return this.m_NewsUrl;
            }
            set
            {
                this.m_NewsUrl = value;
            }
        }

        public int NodeId
        {
            get
            {
                return this.m_NodeId;
            }
            set
            {
                this.m_NodeId = value;
            }
        }

        public bool Result
        {
            get
            {
                return this.m_Result;
            }
            set
            {
                this.m_Result = value;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}


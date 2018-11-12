namespace EasyOne.Model.Collection
{
    using EasyOne.Model;
    using System;

    public class CollectionItemInfo : EasyOne.Model.Nullable
    {
        private bool m_AutoCreateHtml;
        private string m_CodeType;
        private bool m_Detection;
        private string m_InfoNodeId;
        private string m_Intro;
        private int m_ItemId;
        private string m_ItemName;
        private int m_MaxNum;
        private int m_ModelId;
        private DateTime m_NewsCollecDate;
        private int m_NodeId;
        private int m_OrderType;
        private string m_SpecialId;
        private string m_Url;
        private string m_UrlName;

        public CollectionItemInfo()
        {
        }

        public CollectionItemInfo(bool value)
        {
            base.IsNull = value;
        }

        public bool AutoCreateHtml
        {
            get
            {
                return this.m_AutoCreateHtml;
            }
            set
            {
                this.m_AutoCreateHtml = value;
            }
        }

        public string CodeType
        {
            get
            {
                return this.m_CodeType;
            }
            set
            {
                this.m_CodeType = value;
            }
        }

        public bool Detection
        {
            get
            {
                return this.m_Detection;
            }
            set
            {
                this.m_Detection = value;
            }
        }

        public string InfoNodeId
        {
            get
            {
                return this.m_InfoNodeId;
            }
            set
            {
                this.m_InfoNodeId = value;
            }
        }

        public string Intro
        {
            get
            {
                return this.m_Intro;
            }
            set
            {
                this.m_Intro = value;
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

        public string ItemName
        {
            get
            {
                return this.m_ItemName;
            }
            set
            {
                this.m_ItemName = value;
            }
        }

        public int MaxNum
        {
            get
            {
                return this.m_MaxNum;
            }
            set
            {
                this.m_MaxNum = value;
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

        public DateTime NewsCollecDate
        {
            get
            {
                return this.m_NewsCollecDate;
            }
            set
            {
                this.m_NewsCollecDate = value;
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

        public int OrderType
        {
            get
            {
                return this.m_OrderType;
            }
            set
            {
                this.m_OrderType = value;
            }
        }

        public string SpecialId
        {
            get
            {
                return this.m_SpecialId;
            }
            set
            {
                this.m_SpecialId = value;
            }
        }

        public string Url
        {
            get
            {
                return this.m_Url;
            }
            set
            {
                this.m_Url = value;
            }
        }

        public string UrlName
        {
            get
            {
                return this.m_UrlName;
            }
            set
            {
                this.m_UrlName = value;
            }
        }
    }
}


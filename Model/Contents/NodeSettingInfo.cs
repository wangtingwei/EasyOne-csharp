namespace EasyOne.Model.Contents
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot("NodeSettingInfo", Namespace="http://www.EasyOne.net", IsNullable=false)]
    public class NodeSettingInfo
    {
        private int m_AutoUpdatePages;
        private int m_CacheTime;
        private bool m_CommentNeedCheck;
        private int m_DefaultItemChargeType;
        private int m_DefaultItemDividePercent;
        private int m_DefaultItemPitchTime;
        private int m_DefaultItemPoint;
        private int m_DefaultItemReadTimes;
        private bool m_EnableAddWhenHasChild;
        private bool m_EnableComment;
        private bool m_EnableProtect;
        private bool m_EnableTouristsComment;
        private bool m_IsNull;
        private bool m_IsSetCache;
        private int m_PresentExp;

        public NodeSettingInfo()
        {
        }

        public NodeSettingInfo(bool value)
        {
            this.m_IsNull = value;
        }

        public int AutoUpdatePages
        {
            get
            {
                return this.m_AutoUpdatePages;
            }
            set
            {
                this.m_AutoUpdatePages = value;
            }
        }

        public int CacheTime
        {
            get
            {
                return this.m_CacheTime;
            }
            set
            {
                this.m_CacheTime = value;
            }
        }

        public bool CommentNeedCheck
        {
            get
            {
                return this.m_CommentNeedCheck;
            }
            set
            {
                this.m_CommentNeedCheck = value;
            }
        }

        public int DefaultItemChargeType
        {
            get
            {
                return this.m_DefaultItemChargeType;
            }
            set
            {
                this.m_DefaultItemChargeType = value;
            }
        }

        public int DefaultItemDividePercent
        {
            get
            {
                return this.m_DefaultItemDividePercent;
            }
            set
            {
                this.m_DefaultItemDividePercent = value;
            }
        }

        public int DefaultItemPitchTime
        {
            get
            {
                return this.m_DefaultItemPitchTime;
            }
            set
            {
                this.m_DefaultItemPitchTime = value;
            }
        }

        public int DefaultItemPoint
        {
            get
            {
                return this.m_DefaultItemPoint;
            }
            set
            {
                this.m_DefaultItemPoint = value;
            }
        }

        public int DefaultItemReadTimes
        {
            get
            {
                return this.m_DefaultItemReadTimes;
            }
            set
            {
                this.m_DefaultItemReadTimes = value;
            }
        }

        public bool EnableAddWhenHasChild
        {
            get
            {
                return this.m_EnableAddWhenHasChild;
            }
            set
            {
                this.m_EnableAddWhenHasChild = value;
            }
        }

        public bool EnableComment
        {
            get
            {
                return this.m_EnableComment;
            }
            set
            {
                this.m_EnableComment = value;
            }
        }

        public bool EnableProtect
        {
            get
            {
                return this.m_EnableProtect;
            }
            set
            {
                this.m_EnableProtect = value;
            }
        }

        public bool EnableTouristsComment
        {
            get
            {
                return this.m_EnableTouristsComment;
            }
            set
            {
                this.m_EnableTouristsComment = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public bool IsSetCache
        {
            get
            {
                return this.m_IsSetCache;
            }
            set
            {
                this.m_IsSetCache = value;
            }
        }

        public int PresentExp
        {
            get
            {
                return this.m_PresentExp;
            }
            set
            {
                this.m_PresentExp = value;
            }
        }
    }
}


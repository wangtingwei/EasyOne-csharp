namespace EasyOne.Model.Collection
{
    using EasyOne.Model;
    using System;

    public class CollectionPagingRuleInfo : EasyOne.Model.Nullable
    {
        private int m_CorrelationRuleId;
        private string m_DesignatedUrl;
        private int m_ItemId;
        private string m_LinkBeginCode;
        private string m_LinkEndCode;
        private string m_PagingBeginCode;
        private string m_PagingEndCode;
        private int m_PagingRuleId;
        private int m_PagingType;
        private string m_PagingUrlList;
        private int m_RuleType;
        private int m_ScopeBegin;
        private int m_ScopeEnd;

        public CollectionPagingRuleInfo()
        {
        }

        public CollectionPagingRuleInfo(bool value)
        {
            base.IsNull = value;
        }

        public int CorrelationRuleId
        {
            get
            {
                return this.m_CorrelationRuleId;
            }
            set
            {
                this.m_CorrelationRuleId = value;
            }
        }

        public string DesignatedUrl
        {
            get
            {
                return this.m_DesignatedUrl;
            }
            set
            {
                this.m_DesignatedUrl = value;
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

        public string LinkBeginCode
        {
            get
            {
                return this.m_LinkBeginCode;
            }
            set
            {
                this.m_LinkBeginCode = value;
            }
        }

        public string LinkEndCode
        {
            get
            {
                return this.m_LinkEndCode;
            }
            set
            {
                this.m_LinkEndCode = value;
            }
        }

        public string PagingBeginCode
        {
            get
            {
                return this.m_PagingBeginCode;
            }
            set
            {
                this.m_PagingBeginCode = value;
            }
        }

        public string PagingEndCode
        {
            get
            {
                return this.m_PagingEndCode;
            }
            set
            {
                this.m_PagingEndCode = value;
            }
        }

        public int PagingRuleId
        {
            get
            {
                return this.m_PagingRuleId;
            }
            set
            {
                this.m_PagingRuleId = value;
            }
        }

        public int PagingType
        {
            get
            {
                return this.m_PagingType;
            }
            set
            {
                this.m_PagingType = value;
            }
        }

        public string PagingUrlList
        {
            get
            {
                return this.m_PagingUrlList;
            }
            set
            {
                this.m_PagingUrlList = value;
            }
        }

        public int RuleType
        {
            get
            {
                return this.m_RuleType;
            }
            set
            {
                this.m_RuleType = value;
            }
        }

        public int ScopeBegin
        {
            get
            {
                return this.m_ScopeBegin;
            }
            set
            {
                this.m_ScopeBegin = value;
            }
        }

        public int ScopeEnd
        {
            get
            {
                return this.m_ScopeEnd;
            }
            set
            {
                this.m_ScopeEnd = value;
            }
        }
    }
}


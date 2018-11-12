namespace EasyOne.Model.Collection
{
    using EasyOne.Model;
    using System;

    public class CollectionFilterRuleInfo : EasyOne.Model.Nullable
    {
        private string m_BeginCode;
        private string m_EndCode;
        private string m_FilterName;
        private int m_FilterRuleId;
        private int m_FilterType;
        private string m_Replace;

        public CollectionFilterRuleInfo()
        {
        }

        public CollectionFilterRuleInfo(bool value)
        {
            base.IsNull = value;
        }

        public string BeginCode
        {
            get
            {
                return this.m_BeginCode;
            }
            set
            {
                this.m_BeginCode = value;
            }
        }

        public string EndCode
        {
            get
            {
                return this.m_EndCode;
            }
            set
            {
                this.m_EndCode = value;
            }
        }

        public string FilterName
        {
            get
            {
                return this.m_FilterName;
            }
            set
            {
                this.m_FilterName = value;
            }
        }

        public int FilterRuleId
        {
            get
            {
                return this.m_FilterRuleId;
            }
            set
            {
                this.m_FilterRuleId = value;
            }
        }

        public int FilterType
        {
            get
            {
                return this.m_FilterType;
            }
            set
            {
                this.m_FilterType = value;
            }
        }

        public string Replace
        {
            get
            {
                return this.m_Replace;
            }
            set
            {
                this.m_Replace = value;
            }
        }
    }
}


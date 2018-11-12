namespace EasyOne.Model.Collection
{
    using EasyOne.Model;
    using System;

    public class CollectionFieldRuleInfo : EasyOne.Model.Nullable
    {
        private string m_BeginCode;
        private string m_EndCode;
        private int m_ExclosionId;
        private string m_FieldName;
        private int m_FieldRuleId;
        private string m_FieldType;
        private string m_FilterRuleId;
        private int m_ItemId;
        private string m_PrivateFilter;
        private int m_RuleType;
        private string m_SpecialSetting;
        private bool m_UsePaging;

        public CollectionFieldRuleInfo()
        {
        }

        public CollectionFieldRuleInfo(bool value)
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

        public int ExclosionId
        {
            get
            {
                return this.m_ExclosionId;
            }
            set
            {
                this.m_ExclosionId = value;
            }
        }

        public string FieldName
        {
            get
            {
                return this.m_FieldName;
            }
            set
            {
                this.m_FieldName = value;
            }
        }

        public int FieldRuleId
        {
            get
            {
                return this.m_FieldRuleId;
            }
            set
            {
                this.m_FieldRuleId = value;
            }
        }

        public string FieldType
        {
            get
            {
                return this.m_FieldType;
            }
            set
            {
                this.m_FieldType = value;
            }
        }

        public string FilterRuleId
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

        public string PrivateFilter
        {
            get
            {
                return this.m_PrivateFilter;
            }
            set
            {
                this.m_PrivateFilter = value;
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

        public string SpecialSetting
        {
            get
            {
                return this.m_SpecialSetting;
            }
            set
            {
                this.m_SpecialSetting = value;
            }
        }

        public bool UsePaging
        {
            get
            {
                return this.m_UsePaging;
            }
            set
            {
                this.m_UsePaging = value;
            }
        }
    }
}


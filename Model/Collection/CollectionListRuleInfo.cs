namespace EasyOne.Model.Collection
{
    using EasyOne.Model;
    using System;

    public class CollectionListRuleInfo : EasyOne.Model.Nullable
    {
        private bool m_IsLinkSpecialSolution;
        private int m_ItemId;
        private string m_LinkBeginCode;
        private string m_LinkEndCode;
        private string m_ListBeginCode;
        private string m_ListEndCode;
        private string m_RedirectUrl;
        private bool m_UsePaging;

        public CollectionListRuleInfo()
        {
        }

        public CollectionListRuleInfo(bool value)
        {
            base.IsNull = value;
        }

        public bool IsLinkSpecialSolution
        {
            get
            {
                return this.m_IsLinkSpecialSolution;
            }
            set
            {
                this.m_IsLinkSpecialSolution = value;
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

        public string ListBeginCode
        {
            get
            {
                return this.m_ListBeginCode;
            }
            set
            {
                this.m_ListBeginCode = value;
            }
        }

        public string ListEndCode
        {
            get
            {
                return this.m_ListEndCode;
            }
            set
            {
                this.m_ListEndCode = value;
            }
        }

        public string RedirectUrl
        {
            get
            {
                return this.m_RedirectUrl;
            }
            set
            {
                this.m_RedirectUrl = value;
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


namespace EasyOne.Model.Contents
{
    using EasyOne.Model;
    using System;

    public class SpecialCategoryInfo : EasyOne.Model.Nullable, IComparable<SpecialCategoryInfo>
    {
        private string m_Description;
        private bool m_IsCreateHtml;
        private bool m_NeedCreateHtml;
        private bool m_OpenType;
        private int m_OrderId;
        private string m_PagePostfix;
        private string m_SearchTemplatePath;
        private string m_SpecialCategoryDir;
        private int m_SpecialCategoryId;
        private string m_SpecialCategoryName;
        private string m_SpecialTemplatePath;

        public SpecialCategoryInfo()
        {
        }

        public SpecialCategoryInfo(bool value)
        {
            base.IsNull = value;
        }

        public int CompareTo(SpecialCategoryInfo other)
        {
            return this.m_OrderId.CompareTo(other.m_OrderId);
        }

        public string CategoryHtmlPageName
        {
            get
            {
                return (this.m_SpecialCategoryDir + "/Index." + this.m_PagePostfix);
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        public bool IsCreateHtml
        {
            get
            {
                return this.m_IsCreateHtml;
            }
            set
            {
                this.m_IsCreateHtml = value;
            }
        }

        public bool NeedCreateHtml
        {
            get
            {
                return this.m_NeedCreateHtml;
            }
            set
            {
                this.m_NeedCreateHtml = value;
            }
        }

        public bool OpenType
        {
            get
            {
                return this.m_OpenType;
            }
            set
            {
                this.m_OpenType = value;
            }
        }

        public int OrderId
        {
            get
            {
                return this.m_OrderId;
            }
            set
            {
                this.m_OrderId = value;
            }
        }

        public string PagePostfix
        {
            get
            {
                return this.m_PagePostfix;
            }
            set
            {
                this.m_PagePostfix = value;
            }
        }

        public string SearchTemplatePath
        {
            get
            {
                return this.m_SearchTemplatePath;
            }
            set
            {
                this.m_SearchTemplatePath = value;
            }
        }

        public string SpecialCategoryDir
        {
            get
            {
                return this.m_SpecialCategoryDir;
            }
            set
            {
                this.m_SpecialCategoryDir = value;
            }
        }

        public int SpecialCategoryId
        {
            get
            {
                return this.m_SpecialCategoryId;
            }
            set
            {
                this.m_SpecialCategoryId = value;
            }
        }

        public string SpecialCategoryName
        {
            get
            {
                return this.m_SpecialCategoryName;
            }
            set
            {
                this.m_SpecialCategoryName = value;
            }
        }

        public string SpecialTemplatePath
        {
            get
            {
                return this.m_SpecialTemplatePath;
            }
            set
            {
                this.m_SpecialTemplatePath = value;
            }
        }
    }
}


namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class DataBaseVersionInfo : EasyOne.Model.Nullable
    {
        private int m_Build;
        private DateTime m_CreatedDate;
        private int m_Major;
        private int m_Minor;
        private int m_Revision;
        private int m_VersionId;

        public DataBaseVersionInfo()
        {
        }

        public DataBaseVersionInfo(bool isNull)
        {
            base.IsNull = isNull;
        }

        public int Build
        {
            get
            {
                return this.m_Build;
            }
            set
            {
                this.m_Build = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return this.m_CreatedDate;
            }
            set
            {
                this.m_CreatedDate = value;
            }
        }

        public int Major
        {
            get
            {
                return this.m_Major;
            }
            set
            {
                this.m_Major = value;
            }
        }

        public int Minor
        {
            get
            {
                return this.m_Minor;
            }
            set
            {
                this.m_Minor = value;
            }
        }

        public int Revision
        {
            get
            {
                return this.m_Revision;
            }
            set
            {
                this.m_Revision = value;
            }
        }

        public int VersionId
        {
            get
            {
                return this.m_VersionId;
            }
            set
            {
                this.m_VersionId = value;
            }
        }
    }
}


namespace EasyOne.Model.Collection
{
    using EasyOne.Model;
    using System;

    public class CollectionExclosionInfo : EasyOne.Model.Nullable
    {
        private DateTime? m_ExclosionDesignatedDateTime;
        private int m_ExclosionDesignatedNumber;
        private int m_ExclosionId;
        private DateTime? m_ExclosionMaxDateTime;
        private int m_ExclosionMaxNumber;
        private DateTime? m_ExclosionMinDateTime;
        private int m_ExclosionMinNumber;
        private string m_ExclosionName;
        private string m_ExclosionString;
        private int m_ExclosionStringType;
        private int m_ExclosionType;
        private bool m_IsExclosionDesignatedDateTime;
        private bool m_IsExclosionDesignatedNumber;
        private bool m_IsExclosionMaxDateTime;
        private bool m_IsExclosionMaxNumber;
        private bool m_IsExclosionMinDateTime;
        private bool m_IsExclosionMinNumber;

        public CollectionExclosionInfo()
        {
        }

        public CollectionExclosionInfo(bool value)
        {
            base.IsNull = value;
        }

        public DateTime? ExclosionDesignatedDateTime
        {
            get
            {
                return this.m_ExclosionDesignatedDateTime;
            }
            set
            {
                this.m_ExclosionDesignatedDateTime = value;
            }
        }

        public int ExclosionDesignatedNumber
        {
            get
            {
                return this.m_ExclosionDesignatedNumber;
            }
            set
            {
                this.m_ExclosionDesignatedNumber = value;
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

        public DateTime? ExclosionMaxDateTime
        {
            get
            {
                return this.m_ExclosionMaxDateTime;
            }
            set
            {
                this.m_ExclosionMaxDateTime = value;
            }
        }

        public int ExclosionMaxNumber
        {
            get
            {
                return this.m_ExclosionMaxNumber;
            }
            set
            {
                this.m_ExclosionMaxNumber = value;
            }
        }

        public DateTime? ExclosionMinDateTime
        {
            get
            {
                return this.m_ExclosionMinDateTime;
            }
            set
            {
                this.m_ExclosionMinDateTime = value;
            }
        }

        public int ExclosionMinNumber
        {
            get
            {
                return this.m_ExclosionMinNumber;
            }
            set
            {
                this.m_ExclosionMinNumber = value;
            }
        }

        public string ExclosionName
        {
            get
            {
                return this.m_ExclosionName;
            }
            set
            {
                this.m_ExclosionName = value;
            }
        }

        public string ExclosionString
        {
            get
            {
                return this.m_ExclosionString;
            }
            set
            {
                this.m_ExclosionString = value;
            }
        }

        public int ExclosionStringType
        {
            get
            {
                return this.m_ExclosionStringType;
            }
            set
            {
                this.m_ExclosionStringType = value;
            }
        }

        public int ExclosionType
        {
            get
            {
                return this.m_ExclosionType;
            }
            set
            {
                this.m_ExclosionType = value;
            }
        }

        public bool IsExclosionDesignatedDateTime
        {
            get
            {
                return this.m_IsExclosionDesignatedDateTime;
            }
            set
            {
                this.m_IsExclosionDesignatedDateTime = value;
            }
        }

        public bool IsExclosionDesignatedNumber
        {
            get
            {
                return this.m_IsExclosionDesignatedNumber;
            }
            set
            {
                this.m_IsExclosionDesignatedNumber = value;
            }
        }

        public bool IsExclosionMaxDateTime
        {
            get
            {
                return this.m_IsExclosionMaxDateTime;
            }
            set
            {
                this.m_IsExclosionMaxDateTime = value;
            }
        }

        public bool IsExclosionMaxNumber
        {
            get
            {
                return this.m_IsExclosionMaxNumber;
            }
            set
            {
                this.m_IsExclosionMaxNumber = value;
            }
        }

        public bool IsExclosionMinDateTime
        {
            get
            {
                return this.m_IsExclosionMinDateTime;
            }
            set
            {
                this.m_IsExclosionMinDateTime = value;
            }
        }

        public bool IsExclosionMinNumber
        {
            get
            {
                return this.m_IsExclosionMinNumber;
            }
            set
            {
                this.m_IsExclosionMinNumber = value;
            }
        }
    }
}


namespace EasyOne.Model.Shop
{
    using System;

    [Serializable]
    public class ProductDetailInfo : ProductInfo
    {
        private DateTime? m_CreateTime;
        private int m_EliteLevel;
        private int m_GeneralId;
        private int m_LinkType;
        private int m_ModelId;
        private int m_NodeId;
        private string m_NodeName;
        private string m_TemplateFile;
        private DateTime? m_UpdateTime;

        public ProductDetailInfo()
        {
        }

        public ProductDetailInfo(bool value) : base(value)
        {
        }

        public DateTime? CreateTime
        {
            get
            {
                return this.m_CreateTime;
            }
            set
            {
                this.m_CreateTime = value;
            }
        }

        public int EliteLevel
        {
            get
            {
                return this.m_EliteLevel;
            }
            set
            {
                this.m_EliteLevel = value;
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

        public int LinkType
        {
            get
            {
                return this.m_LinkType;
            }
            set
            {
                this.m_LinkType = value;
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

        public string NodeName
        {
            get
            {
                return this.m_NodeName;
            }
            set
            {
                this.m_NodeName = value;
            }
        }

        public string TemplateFile
        {
            get
            {
                return this.m_TemplateFile;
            }
            set
            {
                this.m_TemplateFile = value;
            }
        }

        public DateTime? UpdateTime
        {
            get
            {
                return this.m_UpdateTime;
            }
            set
            {
                this.m_UpdateTime = value;
            }
        }
    }
}


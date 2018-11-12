namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class ProductHitsInfo : EasyOne.Model.Nullable
    {
        private int m_Hits;
        private string m_ProductName;

        public ProductHitsInfo()
        {
        }

        public ProductHitsInfo(bool value)
        {
            base.IsNull = value;
        }

        public int Hits
        {
            get
            {
                return this.m_Hits;
            }
            set
            {
                this.m_Hits = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this.m_ProductName;
            }
            set
            {
                this.m_ProductName = value;
            }
        }
    }
}


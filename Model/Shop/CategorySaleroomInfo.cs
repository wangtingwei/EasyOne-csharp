namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class CategorySaleroomInfo : EasyOne.Model.Nullable
    {
        private string m_NodeName;
        private decimal m_Saleroom;
        private int m_SalesVolumn;

        public CategorySaleroomInfo()
        {
        }

        public CategorySaleroomInfo(bool value)
        {
            base.IsNull = value;
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

        public decimal Saleroom
        {
            get
            {
                return this.m_Saleroom;
            }
            set
            {
                this.m_Saleroom = value;
            }
        }

        public int SalesVolumn
        {
            get
            {
                return this.m_SalesVolumn;
            }
            set
            {
                this.m_SalesVolumn = value;
            }
        }
    }
}


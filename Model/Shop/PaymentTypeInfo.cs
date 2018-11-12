namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class PaymentTypeInfo : EasyOne.Model.Nullable
    {
        private int m_Category;
        private float m_Discount;
        private string m_Intro;
        private bool m_IsDefault;
        private bool m_IsDisabled;
        private int m_OrderId;
        private int m_TypeId;
        private string m_TypeName;

        public PaymentTypeInfo()
        {
        }

        public PaymentTypeInfo(bool value)
        {
            base.IsNull = value;
        }

        public int Category
        {
            get
            {
                return this.m_Category;
            }
            set
            {
                this.m_Category = value;
            }
        }

        public float Discount
        {
            get
            {
                return this.m_Discount;
            }
            set
            {
                this.m_Discount = value;
            }
        }

        public string Intro
        {
            get
            {
                return this.m_Intro;
            }
            set
            {
                this.m_Intro = value;
            }
        }

        public bool IsDefault
        {
            get
            {
                return this.m_IsDefault;
            }
            set
            {
                this.m_IsDefault = value;
            }
        }

        public bool IsDisabled
        {
            get
            {
                return this.m_IsDisabled;
            }
            set
            {
                this.m_IsDisabled = value;
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

        public int TypeId
        {
            get
            {
                return this.m_TypeId;
            }
            set
            {
                this.m_TypeId = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this.m_TypeName;
            }
            set
            {
                this.m_TypeName = value;
            }
        }
    }
}


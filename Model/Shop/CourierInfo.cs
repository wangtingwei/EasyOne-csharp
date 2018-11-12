namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class CourierInfo : EasyOne.Model.Nullable
    {
        private string m_Address;
        private string m_Contacter;
        private int m_CourierId;
        private string m_FullName;
        private string m_SearchUrl;
        private string m_ShortName;
        private string m_Telephone;

        public CourierInfo()
        {
        }

        public CourierInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Address
        {
            get
            {
                return this.m_Address;
            }
            set
            {
                this.m_Address = value;
            }
        }

        public string Contacter
        {
            get
            {
                return this.m_Contacter;
            }
            set
            {
                this.m_Contacter = value;
            }
        }

        public int CourierId
        {
            get
            {
                return this.m_CourierId;
            }
            set
            {
                this.m_CourierId = value;
            }
        }

        public string FullName
        {
            get
            {
                return this.m_FullName;
            }
            set
            {
                this.m_FullName = value;
            }
        }

        public string SearchUrl
        {
            get
            {
                return this.m_SearchUrl;
            }
            set
            {
                this.m_SearchUrl = value;
            }
        }

        public string ShortName
        {
            get
            {
                return this.m_ShortName;
            }
            set
            {
                this.m_ShortName = value;
            }
        }

        public string Telephone
        {
            get
            {
                return this.m_Telephone;
            }
            set
            {
                this.m_Telephone = value;
            }
        }
    }
}


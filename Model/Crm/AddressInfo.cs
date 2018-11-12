namespace EasyOne.Model.Crm
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class AddressInfo : EasyOne.Model.Nullable
    {
        private string m_Address;
        private int m_AddressId;
        private string m_Area;
        private string m_City;
        private string m_ConsigneeName;
        private string m_Country;
        private string m_HomePhone;
        private bool m_IsDefalult;
        private string m_Mobile;
        private string m_Province;
        private string m_UserName;
        private string m_ZipCode;

        public AddressInfo()
        {
        }

        public AddressInfo(bool value)
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

        public int AddressId
        {
            get
            {
                return this.m_AddressId;
            }
            set
            {
                this.m_AddressId = value;
            }
        }

        public string Area
        {
            get
            {
                return this.m_Area;
            }
            set
            {
                this.m_Area = value;
            }
        }

        public string City
        {
            get
            {
                return this.m_City;
            }
            set
            {
                this.m_City = value;
            }
        }

        public string ConsigneeName
        {
            get
            {
                return this.m_ConsigneeName;
            }
            set
            {
                this.m_ConsigneeName = value;
            }
        }

        public string Country
        {
            get
            {
                return this.m_Country;
            }
            set
            {
                this.m_Country = value;
            }
        }

        public string HomePhone
        {
            get
            {
                return this.m_HomePhone;
            }
            set
            {
                this.m_HomePhone = value;
            }
        }

        public bool IsDefault
        {
            get
            {
                return this.m_IsDefalult;
            }
            set
            {
                this.m_IsDefalult = value;
            }
        }

        public string Mobile
        {
            get
            {
                return this.m_Mobile;
            }
            set
            {
                this.m_Mobile = value;
            }
        }

        public string Province
        {
            get
            {
                return this.m_Province;
            }
            set
            {
                this.m_Province = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }

        public string ZipCode
        {
            get
            {
                return this.m_ZipCode;
            }
            set
            {
                this.m_ZipCode = value;
            }
        }
    }
}


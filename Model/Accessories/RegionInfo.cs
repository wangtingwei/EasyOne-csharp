namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class RegionInfo : EasyOne.Model.Nullable
    {
        private string m_Area;
        private string m_AreaCode;
        private string m_City;
        private string m_Country;
        private string m_PostCode;
        private string m_Province;
        private int m_RegionId;

        public RegionInfo()
        {
        }

        public RegionInfo(bool value)
        {
            base.IsNull = value;
        }

        public RegionInfo(int regionId, string country, string province, string city, string area, string postCode, string areaCode)
        {
            this.m_RegionId = regionId;
            this.m_Country = country;
            this.m_Province = province;
            this.m_City = city;
            this.m_Area = area;
            this.m_PostCode = postCode;
            this.m_AreaCode = areaCode;
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

        public string AreaCode
        {
            get
            {
                return this.m_AreaCode;
            }
            set
            {
                this.m_AreaCode = value;
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

        public string PostCode
        {
            get
            {
                return this.m_PostCode;
            }
            set
            {
                this.m_PostCode = value;
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

        public int RegionId
        {
            get
            {
                return this.m_RegionId;
            }
            set
            {
                this.m_RegionId = value;
            }
        }
    }
}


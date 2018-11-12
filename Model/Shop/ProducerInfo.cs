namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class ProducerInfo : EasyOne.Model.Nullable
    {
        private string m_Address;
        private DateTime? m_BirthDay;
        private string m_Email;
        private string m_Fax;
        private int m_Hits;
        private string m_HomePage;
        private bool m_IsElite;
        private DateTime m_LastUseTime;
        private bool m_OnTop;
        private bool m_Passed;
        private string m_Phone;
        private string m_Postcode;
        private int m_ProducerId;
        private string m_ProducerIntro;
        private string m_ProducerName;
        private string m_ProducerPhoto;
        private string m_ProducerShortName;
        private int m_ProducerType;

        public ProducerInfo()
        {
        }

        public ProducerInfo(bool value)
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

        public DateTime? BirthDay
        {
            get
            {
                return this.m_BirthDay;
            }
            set
            {
                this.m_BirthDay = value;
            }
        }

        public string Email
        {
            get
            {
                return this.m_Email;
            }
            set
            {
                this.m_Email = value;
            }
        }

        public string Fax
        {
            get
            {
                return this.m_Fax;
            }
            set
            {
                this.m_Fax = value;
            }
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

        public string Homepage
        {
            get
            {
                return this.m_HomePage;
            }
            set
            {
                this.m_HomePage = value;
            }
        }

        public bool IsElite
        {
            get
            {
                return this.m_IsElite;
            }
            set
            {
                this.m_IsElite = value;
            }
        }

        public DateTime LastUseTime
        {
            get
            {
                return this.m_LastUseTime;
            }
            set
            {
                this.m_LastUseTime = value;
            }
        }

        public bool OnTop
        {
            get
            {
                return this.m_OnTop;
            }
            set
            {
                this.m_OnTop = value;
            }
        }

        public bool Passed
        {
            get
            {
                return this.m_Passed;
            }
            set
            {
                this.m_Passed = value;
            }
        }

        public string Phone
        {
            get
            {
                return this.m_Phone;
            }
            set
            {
                this.m_Phone = value;
            }
        }

        public string Postcode
        {
            get
            {
                return this.m_Postcode;
            }
            set
            {
                this.m_Postcode = value;
            }
        }

        public int ProducerId
        {
            get
            {
                return this.m_ProducerId;
            }
            set
            {
                this.m_ProducerId = value;
            }
        }

        public string ProducerIntro
        {
            get
            {
                return this.m_ProducerIntro;
            }
            set
            {
                this.m_ProducerIntro = value;
            }
        }

        public string ProducerName
        {
            get
            {
                return this.m_ProducerName;
            }
            set
            {
                this.m_ProducerName = value;
            }
        }

        public string ProducerPhoto
        {
            get
            {
                return this.m_ProducerPhoto;
            }
            set
            {
                this.m_ProducerPhoto = value;
            }
        }

        public string ProducerShortName
        {
            get
            {
                return this.m_ProducerShortName;
            }
            set
            {
                this.m_ProducerShortName = value;
            }
        }

        public int ProducerType
        {
            get
            {
                return this.m_ProducerType;
            }
            set
            {
                this.m_ProducerType = value;
            }
        }
    }
}


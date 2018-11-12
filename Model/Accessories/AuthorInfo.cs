namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class AuthorInfo : EasyOne.Model.Nullable
    {
        private string m_Address;
        private DateTime m_BirthDay;
        private string m_Company;
        private string m_Department;
        private bool m_Elite;
        private string m_Email;
        private string m_Fax;
        private int m_Hits;
        private string m_HomePage;
        private int m_Id;
        private string m_Imeeting;
        private string m_Intro;
        private DateTime m_LastUseTime;
        private string m_Mail;
        private string m_Name;
        private bool m_OnTop;
        private bool m_Passed;
        private string m_Photo;
        private int m_Sex;
        private string m_Tel;
        private int m_TemplateId;
        private string m_Type;
        private int m_UserId;
        private int m_ZipCode;

        public AuthorInfo()
        {
        }

        public AuthorInfo(bool value)
        {
            base.IsNull = value;
        }

        public AuthorInfo(string name)
        {
            this.m_Name = name;
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

        public DateTime BirthDay
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

        public string Company
        {
            get
            {
                return this.m_Company;
            }
            set
            {
                this.m_Company = value;
            }
        }

        public string Department
        {
            get
            {
                return this.m_Department;
            }
            set
            {
                this.m_Department = value;
            }
        }

        public bool Elite
        {
            get
            {
                return this.m_Elite;
            }
            set
            {
                this.m_Elite = value;
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

        public string HomePage
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

        public int Id
        {
            get
            {
                return this.m_Id;
            }
            set
            {
                this.m_Id = value;
            }
        }

        public string Imeeting
        {
            get
            {
                return this.m_Imeeting;
            }
            set
            {
                this.m_Imeeting = value;
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

        public string Mail
        {
            get
            {
                return this.m_Mail;
            }
            set
            {
                this.m_Mail = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
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

        public string Photo
        {
            get
            {
                return this.m_Photo;
            }
            set
            {
                this.m_Photo = value;
            }
        }

        public int Sex
        {
            get
            {
                return this.m_Sex;
            }
            set
            {
                this.m_Sex = value;
            }
        }

        public string Tel
        {
            get
            {
                return this.m_Tel;
            }
            set
            {
                this.m_Tel = value;
            }
        }

        public int TemplateId
        {
            get
            {
                return this.m_TemplateId;
            }
            set
            {
                this.m_TemplateId = value;
            }
        }

        public string Type
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
            }
        }

        public int UserId
        {
            get
            {
                return this.m_UserId;
            }
            set
            {
                this.m_UserId = value;
            }
        }

        public int ZipCode
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


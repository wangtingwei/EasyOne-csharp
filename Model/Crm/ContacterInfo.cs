namespace EasyOne.Model.Crm
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    [Serializable]
    public class ContacterInfo : EasyOne.Model.Nullable
    {
        private string m_Address;
        private string m_Aim;
        private DateTime? m_Birthday;
        private string m_City;
        private int m_ClientId;
        private string m_Company;
        private string m_CompanyAddress;
        private int m_ContacterId;
        private ContacterType m_ContacterType;
        private string m_Country;
        private DateTime m_CreateTime;
        private string m_Department;
        private int m_Education;
        private string m_Email;
        private string m_Family;
        private string m_Fax;
        private string m_GraduateFrom;
        private string m_Homepage;
        private string m_HomePhone;
        private string m_Icq;
        private string m_IdCard;
        private int m_Income;
        private string m_InterestsOfAmusement;
        private string m_InterestsOfCulture;
        private string m_InterestsOfLife;
        private string m_InterestsOfOther;
        private string m_InterestsOfSport;
        private UserMarriageType m_Marriage;
        private string m_Mobile;
        private string m_Msn;
        private string m_Nation;
        private string m_NativePlace;
        private string m_OfficePhone;
        private string m_Operation;
        private string m_Owner;
        private int m_ParentId;
        private string m_Phs;
        private string m_Position;
        private string m_Province;
        private string m_QQ;
        private UserSexType m_Sex;
        private string m_ShortedForm;
        private string m_Title;
        private string m_TrueName;
        private string m_UC;
        private DateTime m_UpDateTime;
        private string m_UserName;
        private string m_Yahoo;
        private string m_ZipCode;

        public ContacterInfo()
        {
        }

        public ContacterInfo(bool value)
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

        public string Aim
        {
            get
            {
                return this.m_Aim;
            }
            set
            {
                this.m_Aim = value;
            }
        }

        public DateTime? Birthday
        {
            get
            {
                return this.m_Birthday;
            }
            set
            {
                this.m_Birthday = value;
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

        public int ClientId
        {
            get
            {
                return this.m_ClientId;
            }
            set
            {
                this.m_ClientId = value;
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

        public string CompanyAddress
        {
            get
            {
                return this.m_CompanyAddress;
            }
            set
            {
                this.m_CompanyAddress = value;
            }
        }

        public int ContacterId
        {
            get
            {
                return this.m_ContacterId;
            }
            set
            {
                this.m_ContacterId = value;
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

        public DateTime CreateTime
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

        public int Education
        {
            get
            {
                return this.m_Education;
            }
            set
            {
                this.m_Education = value;
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

        public string Family
        {
            get
            {
                return this.m_Family;
            }
            set
            {
                this.m_Family = value;
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

        public string GraduateFrom
        {
            get
            {
                return this.m_GraduateFrom;
            }
            set
            {
                this.m_GraduateFrom = value;
            }
        }

        public string Homepage
        {
            get
            {
                return this.m_Homepage;
            }
            set
            {
                this.m_Homepage = value;
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

        public string Icq
        {
            get
            {
                return this.m_Icq;
            }
            set
            {
                this.m_Icq = value;
            }
        }

        public string IdCard
        {
            get
            {
                return this.m_IdCard;
            }
            set
            {
                this.m_IdCard = value;
            }
        }

        public int Income
        {
            get
            {
                return this.m_Income;
            }
            set
            {
                this.m_Income = value;
            }
        }

        public string InterestsOfAmusement
        {
            get
            {
                return this.m_InterestsOfAmusement;
            }
            set
            {
                this.m_InterestsOfAmusement = value;
            }
        }

        public string InterestsOfCulture
        {
            get
            {
                return this.m_InterestsOfCulture;
            }
            set
            {
                this.m_InterestsOfCulture = value;
            }
        }

        public string InterestsOfLife
        {
            get
            {
                return this.m_InterestsOfLife;
            }
            set
            {
                this.m_InterestsOfLife = value;
            }
        }

        public string InterestsOfOther
        {
            get
            {
                return this.m_InterestsOfOther;
            }
            set
            {
                this.m_InterestsOfOther = value;
            }
        }

        public string InterestsOfSport
        {
            get
            {
                return this.m_InterestsOfSport;
            }
            set
            {
                this.m_InterestsOfSport = value;
            }
        }

        public UserMarriageType Marriage
        {
            get
            {
                return this.m_Marriage;
            }
            set
            {
                this.m_Marriage = value;
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

        public string Msn
        {
            get
            {
                return this.m_Msn;
            }
            set
            {
                this.m_Msn = value;
            }
        }

        public string Nation
        {
            get
            {
                return this.m_Nation;
            }
            set
            {
                this.m_Nation = value;
            }
        }

        public string NativePlace
        {
            get
            {
                return this.m_NativePlace;
            }
            set
            {
                this.m_NativePlace = value;
            }
        }

        public string OfficePhone
        {
            get
            {
                return this.m_OfficePhone;
            }
            set
            {
                this.m_OfficePhone = value;
            }
        }

        public string Operation
        {
            get
            {
                return this.m_Operation;
            }
            set
            {
                this.m_Operation = value;
            }
        }

        public string Owner
        {
            get
            {
                return this.m_Owner;
            }
            set
            {
                this.m_Owner = value;
            }
        }

        public int ParentId
        {
            get
            {
                return this.m_ParentId;
            }
            set
            {
                this.m_ParentId = value;
            }
        }

        public string Phs
        {
            get
            {
                return this.m_Phs;
            }
            set
            {
                this.m_Phs = value;
            }
        }

        public string Position
        {
            get
            {
                return this.m_Position;
            }
            set
            {
                this.m_Position = value;
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

        public string QQ
        {
            get
            {
                return this.m_QQ;
            }
            set
            {
                this.m_QQ = value;
            }
        }

        public UserSexType Sex
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

        public string ShortedForm
        {
            get
            {
                return this.m_ShortedForm;
            }
            set
            {
                this.m_ShortedForm = value;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }

        public string TrueName
        {
            get
            {
                return this.m_TrueName;
            }
            set
            {
                this.m_TrueName = value;
            }
        }

        public string UC
        {
            get
            {
                return this.m_UC;
            }
            set
            {
                this.m_UC = value;
            }
        }

        public DateTime UpdateTime
        {
            get
            {
                return this.m_UpDateTime;
            }
            set
            {
                this.m_UpDateTime = value;
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

        public ContacterType UserType
        {
            get
            {
                return this.m_ContacterType;
            }
            set
            {
                this.m_ContacterType = value;
            }
        }

        public string Yahoo
        {
            get
            {
                return this.m_Yahoo;
            }
            set
            {
                this.m_Yahoo = value;
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


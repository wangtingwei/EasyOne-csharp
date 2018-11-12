namespace EasyOne.Model.Crm
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class CompanyInfo : EasyOne.Model.Nullable
    {
        private string m_Address;
        private string m_AnnualSales;
        private string m_BankAccount;
        private string m_BankOfDeposit;
        private string m_BusinessScope;
        private string m_City;
        private int m_ClientId;
        private int m_CompanyId;
        private string m_CompanyIntro;
        private string m_CompanyName;
        private string m_CompanyPic;
        private int m_CompanySize;
        private string m_Country;
        private string m_Fax;
        private string m_Homepage;
        private int m_ManagementForms;
        private string m_Phone;
        private string m_Province;
        private string m_RegisteredCapital;
        private int m_StatusInField;
        private string m_TaxNum;
        private string m_ZipCode;

        public CompanyInfo()
        {
        }

        public CompanyInfo(bool value)
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

        public string AnnualSales
        {
            get
            {
                return this.m_AnnualSales;
            }
            set
            {
                this.m_AnnualSales = value;
            }
        }

        public string BankAccount
        {
            get
            {
                return this.m_BankAccount;
            }
            set
            {
                this.m_BankAccount = value;
            }
        }

        public string BankOfDeposit
        {
            get
            {
                return this.m_BankOfDeposit;
            }
            set
            {
                this.m_BankOfDeposit = value;
            }
        }

        public string BusinessScope
        {
            get
            {
                return this.m_BusinessScope;
            }
            set
            {
                this.m_BusinessScope = value;
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

        public int CompanyId
        {
            get
            {
                return this.m_CompanyId;
            }
            set
            {
                this.m_CompanyId = value;
            }
        }

        public string CompanyIntro
        {
            get
            {
                return this.m_CompanyIntro;
            }
            set
            {
                this.m_CompanyIntro = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return this.m_CompanyName;
            }
            set
            {
                this.m_CompanyName = value;
            }
        }

        public string CompanyPic
        {
            get
            {
                return this.m_CompanyPic;
            }
            set
            {
                this.m_CompanyPic = value;
            }
        }

        public int CompanySize
        {
            get
            {
                return this.m_CompanySize;
            }
            set
            {
                this.m_CompanySize = value;
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

        public int ManagementForms
        {
            get
            {
                return this.m_ManagementForms;
            }
            set
            {
                this.m_ManagementForms = value;
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

        public string RegisteredCapital
        {
            get
            {
                return this.m_RegisteredCapital;
            }
            set
            {
                this.m_RegisteredCapital = value;
            }
        }

        public int StatusInField
        {
            get
            {
                return this.m_StatusInField;
            }
            set
            {
                this.m_StatusInField = value;
            }
        }

        public string TaxNum
        {
            get
            {
                return this.m_TaxNum;
            }
            set
            {
                this.m_TaxNum = value;
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


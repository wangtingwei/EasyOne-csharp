namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class BankInfo : EasyOne.Model.Nullable
    {
        private string m_Accounts;
        private int m_BankId;
        private string m_BankIntro;
        private string m_BankName;
        private string m_BankPic;
        private string m_BankShortName;
        private string m_CardNum;
        private string m_HolderName;
        private bool m_IsDefault;
        private bool m_IsDisabled;
        private int m_OrderId;

        public BankInfo()
        {
        }

        public BankInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Accounts
        {
            get
            {
                return this.m_Accounts;
            }
            set
            {
                this.m_Accounts = value;
            }
        }

        public int BankId
        {
            get
            {
                return this.m_BankId;
            }
            set
            {
                this.m_BankId = value;
            }
        }

        public string BankIntro
        {
            get
            {
                return this.m_BankIntro;
            }
            set
            {
                this.m_BankIntro = value;
            }
        }

        public string BankName
        {
            get
            {
                return this.m_BankName;
            }
            set
            {
                this.m_BankName = value;
            }
        }

        public string BankPic
        {
            get
            {
                return this.m_BankPic;
            }
            set
            {
                this.m_BankPic = value;
            }
        }

        public string BankShortName
        {
            get
            {
                return this.m_BankShortName;
            }
            set
            {
                this.m_BankShortName = value;
            }
        }

        public string CardNum
        {
            get
            {
                return this.m_CardNum;
            }
            set
            {
                this.m_CardNum = value;
            }
        }

        public string HolderName
        {
            get
            {
                return this.m_HolderName;
            }
            set
            {
                this.m_HolderName = value;
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
    }
}


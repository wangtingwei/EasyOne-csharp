namespace EasyOne.Model.Contents
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class SignInContentInfo : EasyOne.Model.Nullable
    {
        private DateTime m_EndTime;
        private int m_GeneralId;
        private int m_Priority;
        private EasyOne.Enumerations.SignInType m_SignInType;
        private SignInStatus m_Status;
        private string m_Title;

        public SignInContentInfo()
        {
        }

        public SignInContentInfo(bool value)
        {
            base.IsNull = value;
        }

        public DateTime EndTime
        {
            get
            {
                return this.m_EndTime;
            }
            set
            {
                this.m_EndTime = value;
            }
        }

        public int GeneralId
        {
            get
            {
                return this.m_GeneralId;
            }
            set
            {
                this.m_GeneralId = value;
            }
        }

        public int Priority
        {
            get
            {
                return this.m_Priority;
            }
            set
            {
                this.m_Priority = value;
            }
        }

        public EasyOne.Enumerations.SignInType SignInType
        {
            get
            {
                return this.m_SignInType;
            }
            set
            {
                this.m_SignInType = value;
            }
        }

        public SignInStatus Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
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
    }
}


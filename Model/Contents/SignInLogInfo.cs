namespace EasyOne.Model.Contents
{
    using EasyOne.Model;
    using System;

    public class SignInLogInfo : EasyOne.Model.Nullable
    {
        private int m_GeneralId;
        private string m_IP;
        private bool m_IsSignIn;
        private DateTime m_SignInTime;
        private string m_UserName;

        public SignInLogInfo()
        {
        }

        public SignInLogInfo(bool value)
        {
            base.IsNull = value;
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

        public string IP
        {
            get
            {
                return this.m_IP;
            }
            set
            {
                this.m_IP = value;
            }
        }

        public bool IsSignIn
        {
            get
            {
                return this.m_IsSignIn;
            }
            set
            {
                this.m_IsSignIn = value;
            }
        }

        public DateTime SignInTime
        {
            get
            {
                return this.m_SignInTime;
            }
            set
            {
                this.m_SignInTime = value;
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
    }
}


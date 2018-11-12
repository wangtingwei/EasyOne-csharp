namespace EasyOne.Model.UserManage
{
    using EasyOne.Model;
    using System;

    public class AdministratorInfo : EasyOne.Model.Nullable
    {
        private int m_AdminId;
        private string m_AdminName;
        private string m_AdminPassword;
        private bool m_EnableModifyPassword;
        private bool m_EnableMultiLogOn;
        private bool m_IsLock;
        private DateTime? m_LastLogOffTime;
        private string m_LastLogOnIP;
        private DateTime? m_LastLogOnTime;
        private DateTime? m_LastModifyPasswordTime;
        private int m_LogOnTimes;
        private string m_RndPassword;
        private string m_RoleList;
        private string m_UserName;

        public AdministratorInfo()
        {
        }

        public AdministratorInfo(bool value)
        {
            base.IsNull = value;
        }

        public int AdminId
        {
            get
            {
                return this.m_AdminId;
            }
            set
            {
                this.m_AdminId = value;
            }
        }

        public string AdminName
        {
            get
            {
                return this.m_AdminName;
            }
            set
            {
                this.m_AdminName = value;
            }
        }

        public string AdminPassword
        {
            get
            {
                return this.m_AdminPassword;
            }
            set
            {
                this.m_AdminPassword = value;
            }
        }

        public bool EnableModifyPassword
        {
            get
            {
                return this.m_EnableModifyPassword;
            }
            set
            {
                this.m_EnableModifyPassword = value;
            }
        }

        public bool EnableMultiLogOn
        {
            get
            {
                return this.m_EnableMultiLogOn;
            }
            set
            {
                this.m_EnableMultiLogOn = value;
            }
        }

        public bool IsLock
        {
            get
            {
                return this.m_IsLock;
            }
            set
            {
                this.m_IsLock = value;
            }
        }

        public DateTime? LastLogOffTime
        {
            get
            {
                return this.m_LastLogOffTime;
            }
            set
            {
                this.m_LastLogOffTime = value;
            }
        }

        public string LastLogOnIP
        {
            get
            {
                return this.m_LastLogOnIP;
            }
            set
            {
                this.m_LastLogOnIP = value;
            }
        }

        public DateTime? LastLogOnTime
        {
            get
            {
                return this.m_LastLogOnTime;
            }
            set
            {
                this.m_LastLogOnTime = value;
            }
        }

        public DateTime? LastModifyPasswordTime
        {
            get
            {
                return this.m_LastModifyPasswordTime;
            }
            set
            {
                this.m_LastModifyPasswordTime = value;
            }
        }

        public int LogOnTimes
        {
            get
            {
                return this.m_LogOnTimes;
            }
            set
            {
                this.m_LogOnTimes = value;
            }
        }

        public string RndPassword
        {
            get
            {
                return this.m_RndPassword;
            }
            set
            {
                this.m_RndPassword = value;
            }
        }

        public string RoleList
        {
            get
            {
                return this.m_RoleList;
            }
            set
            {
                this.m_RoleList = value;
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


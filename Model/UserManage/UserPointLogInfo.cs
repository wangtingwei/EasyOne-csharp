namespace EasyOne.Model.UserManage
{
    using EasyOne.Model;
    using System;

    public class UserPointLogInfo : EasyOne.Model.Nullable
    {
        private int m_IncomePayOut;
        private int m_InfoId;
        private string m_Inputer;
        private string m_IP;
        private int m_LogId;
        private DateTime m_LogTime;
        private string m_Memo;
        private int m_ModuleType;
        private int m_Point;
        private string m_Remark;
        private int m_Times;
        private string m_UserName;

        public UserPointLogInfo()
        {
        }

        public UserPointLogInfo(bool value)
        {
            base.IsNull = value;
        }

        public int IncomePayOut
        {
            get
            {
                return this.m_IncomePayOut;
            }
            set
            {
                this.m_IncomePayOut = value;
            }
        }

        public int InfoId
        {
            get
            {
                return this.m_InfoId;
            }
            set
            {
                this.m_InfoId = value;
            }
        }

        public string Inputer
        {
            get
            {
                return this.m_Inputer;
            }
            set
            {
                this.m_Inputer = value;
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

        public int LogId
        {
            get
            {
                return this.m_LogId;
            }
            set
            {
                this.m_LogId = value;
            }
        }

        public DateTime LogTime
        {
            get
            {
                return this.m_LogTime;
            }
            set
            {
                this.m_LogTime = value;
            }
        }

        public string Memo
        {
            get
            {
                return this.m_Memo;
            }
            set
            {
                this.m_Memo = value;
            }
        }

        public int ModuleType
        {
            get
            {
                return this.m_ModuleType;
            }
            set
            {
                this.m_ModuleType = value;
            }
        }

        public int Point
        {
            get
            {
                return this.m_Point;
            }
            set
            {
                this.m_Point = value;
            }
        }

        public string Remark
        {
            get
            {
                return this.m_Remark;
            }
            set
            {
                this.m_Remark = value;
            }
        }

        public int Times
        {
            get
            {
                return this.m_Times;
            }
            set
            {
                this.m_Times = value;
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


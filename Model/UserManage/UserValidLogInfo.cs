namespace EasyOne.Model.UserManage
{
    using EasyOne.Model;
    using System;

    public class UserValidLogInfo : EasyOne.Model.Nullable
    {
        private int m_IncomePayout;
        private string m_Inputer;
        private string m_IP;
        private int m_LogId;
        private DateTime m_LogTime;
        private string m_Memo;
        private string m_Remark;
        private string m_UserName;
        private int m_ValidNum;

        public UserValidLogInfo()
        {
        }

        public UserValidLogInfo(bool value)
        {
            base.IsNull = value;
        }

        public int IncomePayout
        {
            get
            {
                return this.m_IncomePayout;
            }
            set
            {
                this.m_IncomePayout = value;
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

        public int ValidNum
        {
            get
            {
                return this.m_ValidNum;
            }
            set
            {
                this.m_ValidNum = value;
            }
        }
    }
}


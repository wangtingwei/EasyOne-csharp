namespace EasyOne.Model.WorkFlow
{
    using EasyOne.Model;
    using System;

    public class StatusInfo : EasyOne.Model.Nullable
    {
        private int m_StatusCode;
        private int m_StatusId;
        private string m_StatusName;
        private int m_StatusType;

        public StatusInfo()
        {
        }

        public StatusInfo(bool value)
        {
            base.IsNull = value;
        }

        public int StatusCode
        {
            get
            {
                return this.m_StatusCode;
            }
            set
            {
                this.m_StatusCode = value;
            }
        }

        public int StatusId
        {
            get
            {
                return this.m_StatusId;
            }
            set
            {
                this.m_StatusId = value;
            }
        }

        public string StatusName
        {
            get
            {
                return this.m_StatusName;
            }
            set
            {
                this.m_StatusName = value;
            }
        }

        public int StatusType
        {
            get
            {
                return this.m_StatusType;
            }
            set
            {
                this.m_StatusType = value;
            }
        }
    }
}


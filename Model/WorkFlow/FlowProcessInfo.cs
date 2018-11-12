namespace EasyOne.Model.WorkFlow
{
    using EasyOne.Model;
    using System;

    public class FlowProcessInfo : EasyOne.Model.Nullable
    {
        private string m_Description;
        private int m_FlowId;
        private string m_PassActionName;
        private int m_PassActionStatus;
        private int m_ProcessId;
        private string m_ProcessName;
        private string m_RejectActionName;
        private int m_RejectActionStatus;

        public FlowProcessInfo()
        {
        }

        public FlowProcessInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        public int FlowId
        {
            get
            {
                return this.m_FlowId;
            }
            set
            {
                this.m_FlowId = value;
            }
        }

        public string PassActionName
        {
            get
            {
                return this.m_PassActionName;
            }
            set
            {
                this.m_PassActionName = value;
            }
        }

        public int PassActionStatus
        {
            get
            {
                return this.m_PassActionStatus;
            }
            set
            {
                this.m_PassActionStatus = value;
            }
        }

        public int ProcessId
        {
            get
            {
                return this.m_ProcessId;
            }
            set
            {
                this.m_ProcessId = value;
            }
        }

        public string ProcessName
        {
            get
            {
                return this.m_ProcessName;
            }
            set
            {
                this.m_ProcessName = value;
            }
        }

        public string RejectActionName
        {
            get
            {
                return this.m_RejectActionName;
            }
            set
            {
                this.m_RejectActionName = value;
            }
        }

        public int RejectActionStatus
        {
            get
            {
                return this.m_RejectActionStatus;
            }
            set
            {
                this.m_RejectActionStatus = value;
            }
        }
    }
}


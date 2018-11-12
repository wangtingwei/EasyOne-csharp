namespace EasyOne.Model.WorkFlow
{
    using EasyOne.Model;
    using System;

    public class WorkFlowsInfo : EasyOne.Model.Nullable
    {
        private string m_Description;
        private int m_FlowId;
        private string m_FlowName;

        public WorkFlowsInfo()
        {
        }

        public WorkFlowsInfo(bool value)
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

        public string FlowName
        {
            get
            {
                return this.m_FlowName;
            }
            set
            {
                this.m_FlowName = value;
            }
        }
    }
}


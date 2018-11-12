namespace EasyOne.Model.Contents
{
    using EasyOne.Model;
    using System;

    public class NodesModelTemplateRelationShipInfo : EasyOne.Model.Nullable
    {
        private string m_DefaultTemplateFile;
        private int m_ModelId;
        private int m_NodeId;

        public NodesModelTemplateRelationShipInfo()
        {
        }

        public NodesModelTemplateRelationShipInfo(bool value)
        {
            base.IsNull = value;
        }

        public string DefaultTemplateFile
        {
            get
            {
                return this.m_DefaultTemplateFile;
            }
            set
            {
                this.m_DefaultTemplateFile = value;
            }
        }

        public int ModelId
        {
            get
            {
                return this.m_ModelId;
            }
            set
            {
                this.m_ModelId = value;
            }
        }

        public int NodeId
        {
            get
            {
                return this.m_NodeId;
            }
            set
            {
                this.m_NodeId = value;
            }
        }
    }
}


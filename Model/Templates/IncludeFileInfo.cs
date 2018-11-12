namespace EasyOne.Model.Templates
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class IncludeFileInfo : EasyOne.Model.Nullable
    {
        private EasyOne.Enumerations.AssociateType m_AssociateType;
        private string m_Description;
        private string m_FileName;
        private int m_Id;
        private EasyOne.Enumerations.IncludeType m_IncludeType;
        private string m_Name;
        private string m_Template;

        public IncludeFileInfo()
        {
        }

        public IncludeFileInfo(bool isNull)
        {
            base.IsNull = isNull;
        }

        public EasyOne.Enumerations.AssociateType AssociateType
        {
            get
            {
                return this.m_AssociateType;
            }
            set
            {
                this.m_AssociateType = value;
            }
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

        public string FileName
        {
            get
            {
                return this.m_FileName;
            }
            set
            {
                this.m_FileName = value;
            }
        }

        public int Id
        {
            get
            {
                return this.m_Id;
            }
            set
            {
                this.m_Id = value;
            }
        }

        public EasyOne.Enumerations.IncludeType IncludeType
        {
            get
            {
                return this.m_IncludeType;
            }
            set
            {
                this.m_IncludeType = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        public string Template
        {
            get
            {
                return this.m_Template;
            }
            set
            {
                this.m_Template = value;
            }
        }
    }
}


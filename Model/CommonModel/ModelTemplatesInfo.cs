namespace EasyOne.Model.CommonModel
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class ModelTemplatesInfo : EasyOne.Model.Nullable
    {
        private string m_Field;
        private ModelType m_ModelType;
        private string m_TemplateDescription;
        private int m_TemplateId;
        private string m_TemplateName;

        public ModelTemplatesInfo()
        {
        }

        public ModelTemplatesInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Field
        {
            get
            {
                return this.m_Field;
            }
            set
            {
                this.m_Field = value;
            }
        }

        public ModelType IsEshop
        {
            get
            {
                return this.m_ModelType;
            }
            set
            {
                this.m_ModelType = value;
            }
        }

        public string TemplateDescription
        {
            get
            {
                return this.m_TemplateDescription;
            }
            set
            {
                this.m_TemplateDescription = value;
            }
        }

        public int TemplateId
        {
            get
            {
                return this.m_TemplateId;
            }
            set
            {
                this.m_TemplateId = value;
            }
        }

        public string TemplateName
        {
            get
            {
                return this.m_TemplateName;
            }
            set
            {
                this.m_TemplateName = value;
            }
        }
    }
}


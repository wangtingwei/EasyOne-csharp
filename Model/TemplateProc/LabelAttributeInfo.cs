namespace EasyOne.Model.TemplateProc
{
    using System;
    /// <summary>
    /// 标签参数实体
    /// </summary>
    public sealed class LabelAttributeInfo
    {
        private string m_AttributeName;
        private string m_DefaultValue;
        private string m_Intro;

        public LabelAttributeInfo()
        {
        }

        public LabelAttributeInfo(string attributename, string defaultvalue, string intro)
        {
            this.m_AttributeName = attributename;
            this.m_DefaultValue = defaultvalue;
            this.m_Intro = intro;
        }
        /// <summary>
        /// 标签参数名称
        /// </summary>
        public string AttributeName
        {
            get
            {
                return this.m_AttributeName;
            }
            set
            {
                this.m_AttributeName = value;
            }
        }
        /// <summary>
        /// 标签默认值
        /// </summary>
        public string DefaultValue
        {
            get
            {
                return this.m_DefaultValue;
            }
            set
            {
                this.m_DefaultValue = value;
            }
        }
        /// <summary>
        /// 标签简介
        /// </summary>
        public string Intro
        {
            get
            {
                return this.m_Intro;
            }
            set
            {
                this.m_Intro = value;
            }
        }
    }
}


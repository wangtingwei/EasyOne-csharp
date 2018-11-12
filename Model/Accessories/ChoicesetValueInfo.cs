namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class ChoicesetValueInfo : EasyOne.Model.Nullable
    {
        private string m_DataTextField;
        private int m_DataValueField;
        private bool m_IsDefault;
        private bool m_IsEnable;
        private string m_Title;

        public ChoicesetValueInfo()
        {
        }

        public ChoicesetValueInfo(bool value)
        {
            base.IsNull = value;
        }

        public string DataTextField
        {
            get
            {
                return this.m_DataTextField;
            }
            set
            {
                this.m_DataTextField = value;
            }
        }

        public int DataValueField
        {
            get
            {
                return this.m_DataValueField;
            }
            set
            {
                this.m_DataValueField = value;
            }
        }

        public bool IsDefault
        {
            get
            {
                return this.m_IsDefault;
            }
            set
            {
                this.m_IsDefault = value;
            }
        }

        public bool IsEnable
        {
            get
            {
                return this.m_IsEnable;
            }
            set
            {
                this.m_IsEnable = value;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}


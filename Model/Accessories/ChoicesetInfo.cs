namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class ChoicesetInfo : EasyOne.Model.Nullable
    {
        private ChoicesetValueInfoCollection choicesets;
        private int m_FieldId;
        private string m_FieldName;
        private string m_FieldValue;
        private string m_TableName;
        private string m_Title;

        public ChoicesetInfo()
        {
        }

        public ChoicesetInfo(ChoicesetValueInfoCollection choicesets)
        {
            this.choicesets = choicesets;
        }

        public ChoicesetInfo(bool value)
        {
            base.IsNull = value;
        }

        public ChoicesetValueInfoCollection Choicesets
        {
            get
            {
                return this.choicesets;
            }
        }

        public int FieldId
        {
            get
            {
                return this.m_FieldId;
            }
            set
            {
                this.m_FieldId = value;
            }
        }

        public string FieldName
        {
            get
            {
                return this.m_FieldName;
            }
            set
            {
                this.m_FieldName = value;
            }
        }

        public string FieldValue
        {
            get
            {
                return this.m_FieldValue;
            }
            set
            {
                this.m_FieldValue = value;
            }
        }

        public string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
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


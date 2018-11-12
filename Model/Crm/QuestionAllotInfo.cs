namespace EasyOne.Model.Crm
{
    using EasyOne.Model;
    using System;

    public class QuestionAllotInfo : EasyOne.Model.Nullable
    {
        private int m_AdminId;
        private string m_AdminName;
        private int m_TypeId;
        private string m_TypeName;

        public QuestionAllotInfo()
        {
        }

        public QuestionAllotInfo(bool value)
        {
            base.IsNull = value;
        }

        public int AdminId
        {
            get
            {
                return this.m_AdminId;
            }
            set
            {
                this.m_AdminId = value;
            }
        }

        public string AdminName
        {
            get
            {
                return this.m_AdminName;
            }
            set
            {
                this.m_AdminName = value;
            }
        }

        public int TypeId
        {
            get
            {
                return this.m_TypeId;
            }
            set
            {
                this.m_TypeId = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this.m_TypeName;
            }
            set
            {
                this.m_TypeName = value;
            }
        }
    }
}


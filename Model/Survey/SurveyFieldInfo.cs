namespace EasyOne.Model.Survey
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

    [XmlRoot("SurveyFieldInfo")]
    public class SurveyFieldInfo
    {
        private int m_ContentLength;
        private bool m_Disabled;
        private bool m_EnableNull;
        private int m_InputType;
        private bool m_IsNull;
        private string m_QuestionContent;
        private int m_QuestionId;
        private int m_QuestionType;
        private Collection<string> m_Settings;

        public SurveyFieldInfo()
        {
        }

        public SurveyFieldInfo(bool value)
        {
            this.m_IsNull = value;
        }

        public void CopyToSettings(string[] settings)
        {
            this.m_Settings = new Collection<string>();
            foreach (string str in settings)
            {
                this.m_Settings.Add(str);
            }
        }

        public int ContentLength
        {
            get
            {
                return this.m_ContentLength;
            }
            set
            {
                this.m_ContentLength = value;
            }
        }

        [XmlAttribute("Disabled")]
        public bool Disabled
        {
            get
            {
                return this.m_Disabled;
            }
            set
            {
                this.m_Disabled = value;
            }
        }

        public bool EnableNull
        {
            get
            {
                return this.m_EnableNull;
            }
            set
            {
                this.m_EnableNull = value;
            }
        }

        public int InputType
        {
            get
            {
                return this.m_InputType;
            }
            set
            {
                this.m_InputType = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public string QuestionContent
        {
            get
            {
                return this.m_QuestionContent;
            }
            set
            {
                this.m_QuestionContent = value;
            }
        }

        [XmlAttribute("QuestionId")]
        public int QuestionId
        {
            get
            {
                return this.m_QuestionId;
            }
            set
            {
                this.m_QuestionId = value;
            }
        }

        public int QuestionType
        {
            get
            {
                return this.m_QuestionType;
            }
            set
            {
                this.m_QuestionType = value;
            }
        }

        public Collection<string> Settings
        {
            get
            {
                if (this.m_Settings == null)
                {
                    this.m_Settings = new Collection<string>();
                }
                return this.m_Settings;
            }
        }
    }
}


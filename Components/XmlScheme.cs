namespace EasyOne.Components
{
    using System;

    public class XmlScheme
    {
        private int m_Level;
        private string m_Name = string.Empty;
        private string m_Path = string.Empty;
        private int m_Repnum = 1;
        private int m_Station;
        private string m_Text = string.Empty;
        private string m_Type = string.Empty;

        public int Level
        {
            get
            {
                return this.m_Level;
            }
            set
            {
                this.m_Level = value;
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

        public string Path
        {
            get
            {
                return this.m_Path;
            }
            set
            {
                this.m_Path = value;
            }
        }

        public int Repnum
        {
            get
            {
                return this.m_Repnum;
            }
            set
            {
                this.m_Repnum = value;
            }
        }

        public int Station
        {
            get
            {
                return this.m_Station;
            }
            set
            {
                this.m_Station = value;
            }
        }

        public string Text
        {
            get
            {
                return this.m_Text;
            }
            set
            {
                this.m_Text = value;
            }
        }

        public string Type
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
            }
        }
    }
}


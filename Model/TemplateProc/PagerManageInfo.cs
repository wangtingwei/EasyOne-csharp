namespace EasyOne.Model.TemplateProc
{
    using EasyOne.Model;
    using System;
    using System.Text;

    public class PagerManageInfo : EasyOne.Model.Nullable
    {
        private int m_Id;
        private string m_Image;
        private string m_Intro;
        private string m_Name;
        private StringBuilder m_Template;
        private string m_Type;
        private DateTime m_UpDateTime;

        public PagerManageInfo()
        {
            this.m_Name = string.Empty;
            this.m_Type = string.Empty;
            this.m_Template = new StringBuilder();
            this.m_Intro = string.Empty;
            this.m_Image = string.Empty;
        }

        public PagerManageInfo(bool value)
        {
            this.m_Name = string.Empty;
            this.m_Type = string.Empty;
            this.m_Template = new StringBuilder();
            this.m_Intro = string.Empty;
            this.m_Image = string.Empty;
            base.IsNull = value;
        }

        public PagerManageInfo(string name)
        {
            this.m_Name = string.Empty;
            this.m_Type = string.Empty;
            this.m_Template = new StringBuilder();
            this.m_Intro = string.Empty;
            this.m_Image = string.Empty;
            this.m_Name = name;
        }

        public PagerManageInfo(int id, string name, string type, string intro, string image, DateTime updatetime)
        {
            this.m_Name = string.Empty;
            this.m_Type = string.Empty;
            this.m_Template = new StringBuilder();
            this.m_Intro = string.Empty;
            this.m_Image = string.Empty;
            this.m_Id = id;
            this.m_Name = name;
            this.m_Type = type;
            this.m_Intro = intro;
            this.m_Image = image;
            this.m_UpDateTime = updatetime;
        }

        public PagerManageInfo(int id, string name, string type, StringBuilder template, string intro, string image, DateTime updatetime)
        {
            this.m_Name = string.Empty;
            this.m_Type = string.Empty;
            this.m_Template = new StringBuilder();
            this.m_Intro = string.Empty;
            this.m_Image = string.Empty;
            this.m_Id = id;
            this.m_Name = name;
            this.m_Type = type;
            this.m_Template = template;
            this.m_Intro = intro;
            this.m_Image = image;
            this.m_UpDateTime = updatetime;
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

        public string Image
        {
            get
            {
                return this.m_Image;
            }
            set
            {
                this.m_Image = value;
            }
        }

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

        public StringBuilder Template
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

        public DateTime UpDateTime
        {
            get
            {
                return this.m_UpDateTime;
            }
            set
            {
                this.m_UpDateTime = value;
            }
        }
    }
}


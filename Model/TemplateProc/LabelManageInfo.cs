namespace EasyOne.Model.TemplateProc
{
    using EasyOne.Model;
    using System;
    using System.Text;

    public class LabelManageInfo : EasyOne.Model.Nullable
    {
        private StringBuilder m_Define;
        private string m_Ico;
        private int m_Id;
        private string m_Intro;
        private int m_Mid;
        private string m_Name;
        private int m_Nid;
        private StringBuilder m_Template;
        private string m_TemplateType;
        private string m_Type;
        private DateTime m_UpDateTime;

        public LabelManageInfo()
        {
            this.m_TemplateType = string.Empty;
            this.m_Name = string.Empty;
            this.m_Type = string.Empty;
            this.m_Define = new StringBuilder();
            this.m_Template = new StringBuilder();
            this.m_Ico = string.Empty;
            this.m_Intro = string.Empty;
        }

        public LabelManageInfo(bool value)
        {
            this.m_TemplateType = string.Empty;
            this.m_Name = string.Empty;
            this.m_Type = string.Empty;
            this.m_Define = new StringBuilder();
            this.m_Template = new StringBuilder();
            this.m_Ico = string.Empty;
            this.m_Intro = string.Empty;
            base.IsNull = value;
        }

        public LabelManageInfo(string name)
        {
            this.m_TemplateType = string.Empty;
            this.m_Name = string.Empty;
            this.m_Type = string.Empty;
            this.m_Define = new StringBuilder();
            this.m_Template = new StringBuilder();
            this.m_Ico = string.Empty;
            this.m_Intro = string.Empty;
            this.m_Name = name;
        }

        public LabelManageInfo(int id, int mid, int nid, string templatetype, string name, string type, StringBuilder define, StringBuilder template, DateTime updatetime, string ico, string intro)
        {
            this.m_TemplateType = string.Empty;
            this.m_Name = string.Empty;
            this.m_Type = string.Empty;
            this.m_Define = new StringBuilder();
            this.m_Template = new StringBuilder();
            this.m_Ico = string.Empty;
            this.m_Intro = string.Empty;
            this.m_Id = id;
            this.m_Mid = mid;
            this.m_Nid = nid;
            this.m_TemplateType = templatetype;
            this.m_Name = name;
            this.m_Type = type;
            this.m_Define = define;
            this.m_Template = template;
            this.m_UpDateTime = updatetime;
            this.m_Ico = ico;
            this.m_Intro = intro;
        }
        /// <summary>
        /// 标签定义
        /// </summary>
        public StringBuilder Define
        {
            get
            {
                return this.m_Define;
            }
            set
            {
                this.m_Define = value;
            }
        }
        /// <summary>
        /// 标签图标
        /// </summary>
        public string Ico
        {
            get
            {
                return this.m_Ico;
            }
            set
            {
                this.m_Ico = value;
            }
        }
        /// <summary>
        /// 标签ID标识
        /// </summary>
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

        public int Mid
        {
            get
            {
                return this.m_Mid;
            }
            set
            {
                this.m_Mid = value;
            }
        }
        /// <summary>
        /// 标签名称
        /// </summary>
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

        public int Nid
        {
            get
            {
                return this.m_Nid;
            }
            set
            {
                this.m_Nid = value;
            }
        }
        /// <summary>
        /// 标签模板
        /// </summary>
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
        /// <summary>
        /// 标签模板类型
        /// </summary>
        public string TemplateType
        {
            get
            {
                return this.m_TemplateType;
            }
            set
            {
                this.m_TemplateType = value;
            }
        }
        /// <summary>
        /// 标签类型
        /// </summary>
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
        /// <summary>
        /// 标签更新时间
        /// </summary>
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


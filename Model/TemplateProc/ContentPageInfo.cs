namespace EasyOne.Model.TemplateProc
{
    using System;

    public class ContentPageInfo
    {
        private string m_Content;
        private int m_CurrentPage;
        private bool m_IsDynamicPage;
        private string m_PageName;
        private int m_PageNum;
        private string m_Parameter;

        public ContentPageInfo()
        {
            this.m_Parameter = string.Empty;
            this.m_Content = string.Empty;
            this.m_CurrentPage = 1;
        }

        public ContentPageInfo(string parameter, string content, int currentpage, int pagenum)
        {
            this.m_Parameter = string.Empty;
            this.m_Content = string.Empty;
            this.m_CurrentPage = 1;
            this.m_Parameter = parameter;
            this.m_Content = content;
            this.m_CurrentPage = currentpage;
            this.m_PageNum = pagenum;
        }

        public string Content
        {
            get
            {
                return this.m_Content;
            }
            set
            {
                this.m_Content = value;
            }
        }

        public int CurrentPage
        {
            get
            {
                return this.m_CurrentPage;
            }
            set
            {
                this.m_CurrentPage = value;
            }
        }

        public bool IsDynamicPage
        {
            get
            {
                return this.m_IsDynamicPage;
            }
            set
            {
                this.m_IsDynamicPage = value;
            }
        }

        public string PageName
        {
            get
            {
                return this.m_PageName;
            }
            set
            {
                this.m_PageName = value;
            }
        }

        public int PageNum
        {
            get
            {
                return this.m_PageNum;
            }
            set
            {
                this.m_PageNum = value;
            }
        }

        public string Parameter
        {
            get
            {
                return this.m_Parameter;
            }
            set
            {
                this.m_Parameter = value;
            }
        }
    }
}


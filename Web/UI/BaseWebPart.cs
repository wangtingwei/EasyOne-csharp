namespace EasyOne.Web.UI
{
    using System;
    using System.Web.UI.WebControls.WebParts;

    public class BaseWebPart : EasyOne.Web.UI.BaseUserControl, IWebPart
    {
        private string m_CatalogIconImageUrl = string.Empty;
        private string m_Description = string.Empty;
        private string m_SubTitle = string.Empty;
        private string m_Title = string.Empty;
        private string m_TitleIconImageUrl = string.Empty;
        private string m_TitleUrl = string.Empty;

        public string CatalogIconImageUrl
        {
            get
            {
                return this.m_CatalogIconImageUrl;
            }
            set
            {
                this.m_CatalogIconImageUrl = value;
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        public string Subtitle
        {
            get
            {
                return this.m_SubTitle;
            }
            set
            {
                this.m_SubTitle = value;
            }
        }

        [WebDescription("标题"), WebDisplayName("标题"), Personalizable(PersonalizationScope.User), WebBrowsable]
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

        public string TitleIconImageUrl
        {
            get
            {
                return this.m_TitleIconImageUrl;
            }
            set
            {
                this.m_TitleIconImageUrl = value;
            }
        }

        public string TitleUrl
        {
            get
            {
                return this.m_TitleUrl;
            }
            set
            {
                this.m_TitleUrl = value;
            }
        }
    }
}


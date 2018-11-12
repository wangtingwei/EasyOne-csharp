namespace EasyOne.WebSite.Controls.WebPart
{
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls.WebParts;

    public partial class SigninContent : BaseWebPart, IWebPartPermissibility
    {
        //protected ExtendedGridView EgvContent;
        protected string m_OperateCode;
        protected int m_PageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(base.Title))
            {
                base.Title = "我的待签收内容";
            }
            this.EgvContent.DataSource = ContentManage.GetCommonModelInfoListBySignInStatus(0, this.PageSize, PEContext.Current.Admin.UserName, 2);
            this.EgvContent.DataBind();
            base.Subtitle = "共" + ContentManage.GetTotalOfCommonModelInfo(0, ContentSortType.None, 0).ToString() + "条";
        }

        [Personalizable(PersonalizationScope.User)]
        public string OperateCode
        {
            get
            {
                return this.m_OperateCode;
            }
            set
            {
                this.m_OperateCode = value;
            }
        }

        [WebBrowsable, Personalizable(PersonalizationScope.User), WebDisplayName("显示项目数"), WebDescription("显示项目数")]
        public int PageSize
        {
            get
            {
                return this.m_PageSize;
            }
            set
            {
                this.m_PageSize = value;
            }
        }
    }
}


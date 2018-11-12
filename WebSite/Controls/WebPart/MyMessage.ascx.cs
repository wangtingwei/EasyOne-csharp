namespace EasyOne.WebSite.Controls.WebPart
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls.WebParts;

    public partial class MyMessage : BaseWebPart, IWebPartPermissibility
    {
        protected string m_OperateCode;
        protected int m_PageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(base.Title))
            {
                base.Title = "我的短消息";
            }
            this.GdvMessageList.DataSource = Message.GetMessageList(0, this.PageSize, PEContext.Current.Admin.UserName, 0);
            this.GdvMessageList.DataBind();
            base.Subtitle = "共" + Message.Count().ToString() + "条";
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

        [WebDescription("显示项目数"), Personalizable(PersonalizationScope.User), WebBrowsable, WebDisplayName("显示项目数")]
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


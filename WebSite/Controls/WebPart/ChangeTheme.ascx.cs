namespace EasyOne.WebSite.Controls
{
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;

    public partial class ChangeTheme : BaseWebPart, IWebPartPermissibility
    {
        protected string m_OperateCode;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(base.Title))
            {
                base.Title = "主题控制";
            }
        }

        protected void RadlTheme_DataBound(object sender, EventArgs e)
        {
            this.RadlTheme.SelectedValue = this.Page.StyleSheetTheme;
        }

        protected void RadlTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Session.Add("StyleSheetTheme", this.RadlTheme.SelectedValue);
            base.Response.Write("<script type='text/javascript'>parent.location.reload()</script>");
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
    }
}


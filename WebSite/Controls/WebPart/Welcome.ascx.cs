namespace EasyOne.WebSite.Controls
{
    using EasyOne.Components;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;

    public partial class Welcome : BaseWebPart, IWebPartPermissibility
    {
        protected string m_OperateCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(base.Title))
            {
                base.Title = "欢迎登录系统";
            }
            this.LitAdminName.Text = PEContext.Current.Admin.AdminName;
            this.LitDateTime.Text = DateTime.Now.ToString();
            this.LitContent.Text = "0";
            this.LitSignin.Text = "0";
            this.LitComment.Text = "0";
            this.LitMessage.Text = "0";
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


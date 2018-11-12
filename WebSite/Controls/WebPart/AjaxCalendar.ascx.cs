namespace EasyOne.WebSite.Controls.WebPart
{
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;

    public partial class AjaxCalendar : BaseWebPart, IWebPartPermissibility
    {

        protected string m_OperateCode;

        protected void Page_Load(object sender, EventArgs e)
        {
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


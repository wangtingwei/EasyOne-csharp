namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ScoreControl : BaseUserControl
    {
        protected string m_ShowPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_ShowPath = base.BasePath;
            this.m_ShowPath = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + this.m_ShowPath;
        }

        public int Score
        {
            get
            {
                return DataConverter.CLng(this.hdnScore.Value);
            }
            set
            {
                int num = value;
                if (num > 5)
                {
                    num = 0;
                }
                for (int i = 1; i <= num; i++)
                {
                    ((HtmlInputImage) this.FindControl("imageField" + i.ToString())).Src = "../Images/fstar.gif";
                }
                this.hdnScore.Value = num.ToString();
            }
        }
    }
}


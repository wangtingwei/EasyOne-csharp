namespace EasyOne.WebSite
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ShowExceptionMessage : BasePage
    {

        protected void Page_Error(object sender, EventArgs e)
        {
            string s = "<font face=verdana color=red><h4>" + base.Server.HtmlEncode(base.Request.Url.ToString()) + "</h4><pre>" + base.Server.GetLastError().GetBaseException().Source + "</pre><pre>" + base.Server.GetLastError().GetBaseException().Message + "</pre></font>";
            base.Response.Write(s);
            base.Server.ClearError();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int num = DataConverter.CLng(base.Request.QueryString["MessageID"]);
            if (num == 0)
            {
                num = 0x3e7;
            }
            PEExceptionType exceptionType = (PEExceptionType) num;
            ErrMessage message = ResourceManager.GetMessage(exceptionType);
            this.LtrTitle.Text = message.Title;
            this.Literal1.Text = message.Body;
            this.LnkReturnUrl.NavigateUrl = "javascript:history.back();";
        }
    }
}


namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Logging;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class LogDetail : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!base.IsPostBack && (base.PreviousPage != null)) && (base.PreviousPage.Items["LogId"] != null))
            {
                LogInfo logById = new DBLog().GetLogById(DataConverter.CLng(base.PreviousPage.Items["LogId"]));
                this.LblLogId.Text = logById.LogId.ToString();
                this.LblCategory.Text = BasePage.EnumToHtml<LogCategory>(logById.Category);
                this.LblMessage.Text = logById.Message;
                this.LblPostString.Text = logById.PostString;
                this.LblPriority.Text = BasePage.EnumToHtml<LogPriority>(logById.Priority);
                this.LblScriptName.Text = logById.ScriptName;
                this.LblSource.Text = logById.Source;
                this.LblTimestamp.Text = logById.Timestamp.ToString();
                this.LblTitle.Text = logById.Title;
                this.LblUserIP.Text = logById.UserIP;
                this.LblUserName.Text = logById.UserName;
            }
        }
    }
}


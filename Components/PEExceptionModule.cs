namespace EasyOne.Components
{
    using EasyOne.Enumerations;
    using System;
    using System.Web;

    public class PEExceptionModule : IPEModule
    {
        public void Init(PEApplication pea)
        {
            if (pea != null)
            {
                pea.CustomException += new PEExceptionEventHandler(this.pea_CustomException);
            }
        }

        private void pea_CustomException(CustomException ex, EventArgs e)
        {
            RedirectToMessage(ex);
        }

        private static void RedirectToMessage(CustomException exception)
        {
            HttpContext current = HttpContext.Current;
            string path = "/Prompt/ShowError.aspx";
            string url = "/Prompt/ShowExceptionMessage.aspx?MessageID=" + ((int) exception.ExceptionType).ToString();
            string manageDir = SiteConfig.SiteOption.ManageDir;
            if (current.Request.Url.AbsolutePath.ToLower().Contains(manageDir.ToLower()))
            {
                path = "~/" + manageDir + path;
                url = "~/" + manageDir + url;
            }
            else
            {
                path = "~" + path;
                url = "~" + url;
            }
            if (exception.ExceptionType == PEExceptionType.BllError)
            {
                current.Items["ErrorMessage"] = exception.Message;
                current.Server.Transfer(path, true);
            }
            else
            {
                current.Response.Redirect(url, true);
            }
        }
    }
}


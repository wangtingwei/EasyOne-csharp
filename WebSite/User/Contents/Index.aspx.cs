namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Globalization;

    public partial class Index : DynamicPage
    {
        protected string m_RightSrc;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_RightSrc = BasePage.RequestString("Action");
            if (string.Compare(this.m_RightSrc, "Add", true, CultureInfo.CurrentCulture) == 0)
            {
                this.LitRight.Text = "<iframe id=\"contentright\" width=\"745px\" height=\"500px\" onload=\"dyniframesize()\" ondatabinding=\"dyniframesize()\" src=\"NavContent.aspx\" frameborder=\"0\" name=\"main_right\"></iframe>";
            }
            else
            {
                this.LitRight.Text = "<iframe id=\"contentright\" width=\"745px\" height=\"500px\" onload=\"dyniframesize()\" ondatabinding=\"dyniframesize()\" src=\"ContentManage.aspx\" frameborder=\"0\" name=\"main_right\"></iframe>";
            }
        }
    }
}


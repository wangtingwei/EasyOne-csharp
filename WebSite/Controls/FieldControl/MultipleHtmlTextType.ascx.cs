namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Controls.Editor;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Xml;

    public partial class MultipleHtmlTextType : BaseFieldControl
    {

        private static string AllowString(string content)
        {
            string str;
            XmlDocument document = new XmlDocument();
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                str = current.Server.MapPath("~/Common/AllowString.xml");
            }
            else
            {
                str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Common/AllowString.xml");
            }
            try
            {
                document.Load(str);
            }
            catch (XmlException)
            {
                return StringHelper.RemoveXss(content);
            }
            XmlNode node = document.SelectSingleNode("Main");
            if (node == null)
            {
                return StringHelper.RemoveXss(content);
            }
            int num = 0;
            if (node.HasChildNodes)
            {
                foreach (XmlNode node2 in node)
                {
                    num++;
                    content = content.Replace(node2.InnerText, "{$AllowString" + num.ToString() + "}");
                }
                content = StringHelper.RemoveXss(content);
                num = 0;
                foreach (XmlNode node3 in node)
                {
                    num++;
                    content = content.Replace("{$AllowString" + num.ToString() + "}", node3.InnerText);
                }
                return content;
            }
            content = StringHelper.RemoveXss(content);
            return content;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.EditorMultipleHtml.SkinPath = "skins/office2003/";
            this.EditorMultipleHtml.Width = DataConverter.CLng(base.Settings[1]);
            this.EditorMultipleHtml.Height = DataConverter.CLng(base.Settings[2]);
            string str = base.Settings[0];
            if (str != null)
            {
                if (!(str == "1"))
                {
                    if (str == "2")
                    {
                        this.EditorMultipleHtml.ToolbarSet = "";
                    }
                    else if (str == "3")
                    {
                        this.EditorMultipleHtml.ToolbarSet = "Default";
                    }
                }
                else
                {
                    this.EditorMultipleHtml.ToolbarSet = "Basic";
                }
            }
            if (base.EnableNull)
            {
                this.FckEditVal.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.EditorMultipleHtml.Value = this.FieldValue;
            }
            else if (base.IsAdminManage)
            {
                this.FieldValue = this.EditorMultipleHtml.Value;
            }
            else if (PEContext.Current.User.Identity.IsAuthenticated && !PEContext.Current.User.UserInfo.UserPurview.IsXssFilter)
            {
                this.FieldValue = this.EditorMultipleHtml.Value;
            }
            else
            {
                this.FieldValue = AllowString(this.EditorMultipleHtml.Value);
            }
        }
    }
}


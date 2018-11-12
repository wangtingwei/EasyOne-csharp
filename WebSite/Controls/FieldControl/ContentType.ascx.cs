namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Controls.Editor;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;
    using EasyOne.ModelControls;

    public partial class ContentType : BaseFieldControl
    {
        private string m_DefaultPicUrl = "";
        private bool m_IsUpload;
        public string m_UploadDir = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir);

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
            if (!base.IsAdminManage)
            {
                this.LitSaveRemotePic.Visible = false;
                this.ChkSaveRemotePic.Visible = false;
            }
            if (base.EnableNull)
            {
                this.ValrContent.Visible = true;
            }
            this.EditorContent.Width = new Unit(DataConverter.CLng(base.Settings[0]));
            this.EditorContent.Height = new Unit(DataConverter.CLng(base.Settings[1]));
            if (base.Settings.Count > 9)
            {
                if (!string.IsNullOrEmpty(base.Settings[6]))
                {
                    this.EditorContent.ImageUploadAllowedExtensions = base.Settings[6];
                }
                if (!string.IsNullOrEmpty(base.Settings[7]))
                {
                    this.EditorContent.FlashUploadAllowedExtensions = base.Settings[7];
                }
                if (!string.IsNullOrEmpty(base.Settings[8]))
                {
                    this.EditorContent.LinkUploadAllowedExtensions = base.Settings[8];
                }
                if (!string.IsNullOrEmpty(base.Settings[9]))
                {
                    this.EditorContent.IsWatermark = base.Settings[9];
                }
                if (!string.IsNullOrEmpty(base.Settings[10]))
                {
                    this.EditorContent.IsThumb = base.Settings[10];
                }
            }
            else if (base.Settings.Count != 6)
            {
                this.EditorContent.ImageUploadAllowedExtensions = base.Settings[2];
                this.EditorContent.FlashUploadAllowedExtensions = base.Settings[3];
                this.EditorContent.LinkUploadAllowedExtensions = base.Settings[4];
                this.EditorContent.IsWatermark = base.Settings[5];
                this.EditorContent.IsThumb = base.Settings[6];
            }
            if (base.Settings.Count != 6)
            {
                this.EditorContent.ModelId = BaseUserControl.RequestInt32("ModelID").ToString();
                this.EditorContent.FieldName = base.FieldName;
            }
            this.EditorContent.ImgPreview = "true";
            if (this.IsUpload)
            {
                this.EditorContent.IsUpload = "true";
            }
            if ((!base.IsPostBack && !string.IsNullOrEmpty(this.m_DefaultPicUrl)) && !this.Page.ClientScript.IsStartupScriptRegistered(base.GetType(), "initDefaultPicUrl"))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language=\"JavaScript\">\n");
                builder.Append("<!--\n");
                builder.Append("setTimeout(\"try{setpic('" + this.DefaultPicurl + "')}catch(e){}\",1000);\n");
                builder.Append("//-->\n");
                builder.Append("</script>\n");
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "initDefaultPicUrl", builder.ToString());
            }
        }

        private string PathLableReplace(string content)
        {
            content = content.Replace(SiteConfig.SiteOption.UploadDir, "{PE.SiteConfig.uploaddir/}");
            if (string.CompareOrdinal(SiteConfig.SiteInfo.VirtualPath, "/") != 0)
            {
                content = content.Replace(SiteConfig.SiteInfo.VirtualPath, "{PE.SiteConfig.ApplicationPath/}");
            }
            return content;
        }

        private string PathReplaceLable(string content)
        {
            string input = content;
            string pattern = @"{PE\.SiteConfig\.ApplicationPath\/}";
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
            {
                input = input.Replace(match.Value, SiteConfig.SiteInfo.VirtualPath);
            }
            pattern = @"{PE\.SiteConfig\.uploaddir\/}";
            foreach (Match match2 in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
            {
                input = input.Replace(match2.Value, SiteConfig.SiteOption.UploadDir);
            }
            return input;
        }

        public string Content
        {
            get
            {
                string content = this.PathLableReplace(this.EditorContent.Value);
                if (base.IsAdminManage)
                {
                    return content;
                }
                if (PEContext.Current.User.Identity.IsAuthenticated && !PEContext.Current.User.UserInfo.UserPurview.IsXssFilter)
                {
                    return content;
                }
                return AllowString(content);
            }
            set
            {
                this.EditorContent.Value = this.PathReplaceLable(value);
            }
        }

        public string DefaultPicurl
        {
            get
            {
                return StringHelper.RemoveXss(this.m_DefaultPicUrl);
            }
            set
            {
                this.m_DefaultPicUrl = value;
            }
        }

        public PEeditor Editor
        {
            get
            {
                return this.EditorContent;
            }
        }

        public bool IsUpload
        {
            get
            {
                return this.m_IsUpload;
            }
            set
            {
                this.m_IsUpload = value;
            }
        }

        public bool SaveRemotePic
        {
            get
            {
                if (!base.IsAdminManage)
                {
                    return false;
                }
                return this.ChkSaveRemotePic.Checked;
            }
            set
            {
                this.ChkSaveRemotePic.Checked = value;
            }
        }
    }
}


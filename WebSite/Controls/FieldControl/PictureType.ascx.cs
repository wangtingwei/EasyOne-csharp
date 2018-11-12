namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Web;

    public partial class PictureType : BaseFieldControl
    {
        protected string m_FieldName;
        protected string m_JSPrefix;
        protected string m_ShowUploadFilePath;
        public string m_UploadDir = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir);

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TxtImageText.Columns = DataConverter.CLng(base.Settings[0]);
            this.m_ShowUploadFilePath = base.BasePath;
            this.m_ShowUploadFilePath = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + this.m_ShowUploadFilePath;
            if (base.IsAdminManage || PEContext.Current.User.Identity.IsAuthenticated)
            {
                if (base.IsAdminManage)
                {
                    this.m_ShowUploadFilePath = this.m_ShowUploadFilePath + SiteConfig.SiteOption.ManageDir;
                }
                else
                {
                    this.m_ShowUploadFilePath = this.m_ShowUploadFilePath + "User";
                }
                this.m_JSPrefix = this.ClientID.Replace("$", "").Replace("_", "");
                this.m_FieldName = base.FieldName;
                if (DataConverter.CBoolean(base.Settings[3]) && base.IsAdminManage)
                {
                    this.LblIsFromSelected.Text = "<input type=\"button\" class=\"button\" value=\"从已上传文件中选择\" onclick=\"" + this.m_JSPrefix + "SelectFiles();\" /><br />";
                }
                StringBuilder builder = new StringBuilder();
                if ((base.Settings.Count < 7) || (DataConverter.CBoolean(base.Settings[6]) && this.Visible))
                {
                    builder.Append("<tr class='tdbg'><td valign=\"middle\" style=\"width: 12%;\" align=\"right\" >上传图片 ：&nbsp;</td>");
                    builder.Append("<td align=\"left\" style=\"width: 88%;\"><iframe id=\"" + this.m_JSPrefix + "Upload\" src=\"\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"80%\" height=\"30px\" scrolling=\"no\"></iframe></td></tr>");
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("<script type=\"text/javascript\">");
                    builder2.Append(" var nodeId = document.getElementById(nodeIdClientId).value;");
                    builder2.Append(" document.getElementById('" + this.m_JSPrefix + "Upload').src = \"" + this.m_ShowUploadFilePath + "/Accessories/FileUpload.aspx?ReturnJSFunction=" + this.m_JSPrefix + "DealwithUpload&ModuleName=Node&NodeId=\"+nodeId+\"&ModelId=" + base.Request["ModelId"] + "&FieldName=" + this.m_FieldName + "\";");
                    builder2.Append("</script>");
                    this.Page.ClientScript.RegisterStartupScript(base.GetType(), this.m_JSPrefix + "Upload", builder2.ToString());
                }
                if ((base.Settings.Count > 6) && (base.FieldLevel == 0))
                {
                    StringBuilder builder3 = new StringBuilder();
                    builder3.Append("<script type=\"text/javascript\">");
                    builder3.Append(" function homeImageAssignment(path){");
                    builder3.Append(" document.getElementById('" + this.TxtImageText.ClientID + "').value = path;");
                    builder3.Append(this.m_JSPrefix + "AddItem(path);");
                    builder3.Append(" }");
                    builder3.Append("</script>");
                    this.Page.ClientScript.RegisterStartupScript(base.GetType(), this.m_JSPrefix + "Assignment", builder3.ToString());
                }
                if ((base.Settings.Count > 7) && DataConverter.CBoolean(base.Settings[7]))
                {
                    this.ShowUploadFiles.Style.Add("display", "");
                    StringBuilder builder4 = new StringBuilder();
                    builder4.Append("document.getElementById(\"" + this.TxtImageText.ClientID + "\").value =this.value;eImgPreview.src=((this.value == '') ? '../../../Images/nopic.gif' : '");
                    if (string.Compare(base.Request.ApplicationPath, "/", StringComparison.Ordinal) != 0)
                    {
                        builder4.Append(base.Request.ApplicationPath + "/" + this.m_UploadDir);
                    }
                    else
                    {
                        builder4.Append("/" + this.m_UploadDir);
                    }
                    builder4.Append("' + this.value);");
                    this.DropUploadFiles.Attributes.Add("onchange", builder4.ToString());
                    if (!string.IsNullOrEmpty(this.UploadFiles))
                    {
                        foreach (string str in this.UploadFiles.Split(new char[] { '|' }))
                        {
                            ListItem item = new ListItem(str, str);
                            this.DropUploadFiles.Items.Add(item);
                            if (str == this.FieldValue)
                            {
                                this.DropUploadFiles.SelectedValue = item.Value;
                            }
                        }
                    }
                }
                this.LblUpload.Text = builder.ToString();
            }
            if (!base.IsPostBack)
            {
                this.TxtImageText.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.TxtImageText.Text;
            }
        }

        public string UploadFiles
        {
            get
            {
                return StringHelper.RemoveXss(this.HdnUploadFiles.Value);
            }
            set
            {
                this.HdnUploadFiles.Value = value;
            }
        }
    }
}


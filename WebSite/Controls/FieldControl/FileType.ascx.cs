namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Components;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.WebSite.Controls;

    public partial class FileType : BaseFieldControl
    {
        protected string m_JSPrefix;
        protected string m_ShowUploadFilePath;
        private void InitJavaScript()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">\n");
            builder.Append("<!--\n");
            builder.Append("function " + this.m_JSPrefix + "ModifySoftUrl(id){\n");
            builder.Append("  var obj = document.getElementById(id);\n");
            builder.Append("  if(obj==null)return false;\n");
            builder.Append("  if(obj.length==0)return false;\n");
            builder.Append("  var thisurl = obj.value;\n");
            builder.Append("  if(thisurl==''){alert('请先选择一个软件地址，再点修改按钮！');return false;}\n");
            builder.Append("  var url = prompt('请输入软件地址名称和链接，中间用“|”隔开：',thisurl);\n");
            builder.Append("  if(url!=thisurl&&url!=null&&url!=''){obj.options[obj.selectedIndex]=new Option(url,url)};\n");
            builder.Append("  " + this.m_JSPrefix + "ChangeHiddenFieldValue();}\n");
            builder.Append("  function " + this.m_JSPrefix + "AddSoftUrl(id){\n");
            builder.Append("  var obj = document.getElementById(id);\n");
            builder.Append("  var thisurl='软件地址'+(obj.length+1)+'|http://';\n");
            builder.Append("  var url=prompt('请输入软件地址名称和链接，中间用“|”隔开：',thisurl);\n");
            builder.Append("  if(url!=null&&url!=''){obj.options[obj.length]=new Option(url,url);}\n");
            builder.Append("  " + this.m_JSPrefix + "ChangeHiddenFieldValue();}\n");
            builder.Append("function " + this.m_JSPrefix + "DelSoftUrl(id){");
            builder.Append("  var obj = document.getElementById(id);\n");
            builder.Append("  if(obj.length==0) return false;\n");
            builder.Append("  var thisurl=obj.value; \n");
            builder.Append("  if (thisurl=='') {alert('请先选择一个软件地址，再点删除按钮！');return false;}\n");
            builder.Append("  obj.options[obj.selectedIndex]=null;\n");
            builder.Append("  " + this.m_JSPrefix + "ChangeHiddenFieldValue();}\n");
            builder.Append("//-->");
            builder.Append("</script>");
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.Page.GetType(), this.m_JSPrefix + "FileType"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), this.m_JSPrefix + "FileType", builder.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_ShowUploadFilePath = base.BasePath;
            this.m_ShowUploadFilePath = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + this.m_ShowUploadFilePath;
            if (base.IsAdminManage)
            {
                this.m_ShowUploadFilePath = this.m_ShowUploadFilePath + SiteConfig.SiteOption.ManageDir;
            }
            else
            {
                this.m_ShowUploadFilePath = this.m_ShowUploadFilePath + "User";
            }
            this.m_JSPrefix = this.ClientID.Replace("$", "").Replace("_", "");
            if (!base.IsPostBack)
            {
                this.InitJavaScript();
                this.SoftUrl.Attributes.Add("ondblclick", "return " + this.m_JSPrefix + "ModifySoftUrl('" + this.SoftUrl.ClientID + "');");
            }
            else
            {
                this.FieldValue = this.HdnSoftUrls.Value;
            }
        }

        public override string FieldValue
        {
            get
            {
                return this.HdnSoftUrls.Value;
            }
            set
            {
                this.HdnSoftUrls.Value = value;
                foreach (string str in value.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    this.SoftUrl.Items.Add(new ListItem(str, str));
                }
            }
        }

        public string FileSize
        {
            get
            {
                return this.TxtFileSize.Text;
            }
            set
            {
                this.TxtFileSize.Text = value;
            }
        }
    }
}


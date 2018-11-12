namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Components;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class MultiplePhotoType : BaseFieldControl
    {
        protected string m_FieldName;
        protected string m_JSPrefix;
        protected string m_ModelId;
        protected string m_ShowUploadFilePath;

        private void InitJavaScript()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">\n");
            builder.Append("<!--\n");
            builder.Append("function " + this.m_JSPrefix + "ModifyPhotoUrl(id){\n");
            builder.Append("  var obj = document.getElementById(id);\n");
            builder.Append("  if(obj==null)return false;\n");
            builder.Append("  if(obj.length==0)return false;\n");
            builder.Append("  var thisurl = obj.value;\n");
            builder.Append("  if(thisurl==''){alert('请先选择一个图片地址，再点修改按钮！');return false;}\n");
            builder.Append("  var url = prompt('请输入图片地址名称和链接，中间用“|”隔开：',thisurl);\n");
            builder.Append("  if(url!=thisurl&&url!=null&&url!=''){obj.options[obj.selectedIndex]=new Option(url,url)};\n");
            builder.Append("  " + this.m_JSPrefix + "ChangeHiddenFieldValue();}\n");
            builder.Append("  function " + this.m_JSPrefix + "AddPhotoUrl(id){\n");
            builder.Append("  var obj = document.getElementById(id);\n");
            builder.Append("  var thisurl='图片地址'+(obj.length+1)+'|http://';\n");
            builder.Append("  var url=prompt('请输入图片地址名称和链接，中间用“|”隔开：',thisurl);\n");
            builder.Append("  if(url!=null&&url!=''){obj.options[obj.length]=new Option(url,url);}\n");
            builder.Append("  " + this.m_JSPrefix + "ChangeHiddenFieldValue();}\n");
            builder.Append("function " + this.m_JSPrefix + "DelPhotoUrl(id){");
            builder.Append("  var obj = document.getElementById(id);\n");
            builder.Append("  if(obj.length==0) return false;\n");
            builder.Append("  var thisurl=obj.value; \n");
            builder.Append("  if (thisurl=='') {alert('请先选择一个图片地址，再点删除按钮！');return false;}\n");
            builder.Append("  obj.options[obj.selectedIndex]=null;\n");
            builder.Append("  " + this.m_JSPrefix + "ChangeHiddenFieldValue();}\n");
            builder.Append("//-->");
            builder.Append("</script>");
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.Page.GetType(), this.m_JSPrefix + "MultiplePhotoType"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), this.m_JSPrefix + "MultiplePhotoType", builder.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_FieldName = base.FieldName;
            this.m_ModelId = BaseUserControl.RequestInt32("ModelId").ToString();
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
                this.PhotoUrl.Attributes.Add("ondblclick", "return " + this.m_JSPrefix + "ModifyPhotoUrl('" + this.PhotoUrl.ClientID + "');");
            }
        }

        public override string FieldValue
        {
            get
            {
                return this.HdnPhotoUrls.Value;
            }
            set
            {
                this.HdnPhotoUrls.Value = value;
                foreach (string str in value.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    this.PhotoUrl.Items.Add(new ListItem(str, str));
                }
            }
        }

        public string ModelId
        {
            get
            {
                return this.m_ModelId;
            }
            set
            {
                if (string.IsNullOrEmpty(BaseUserControl.RequestString("ModelId")))
                {
                    this.m_ModelId = value;
                }
            }
        }
    }
}


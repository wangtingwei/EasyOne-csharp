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

    public partial class TitleType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string basePath = "";
            basePath = base.BasePath;
            basePath = (this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + basePath) + SiteConfig.SiteOption.ManageDir;
            this.TxtTitle.MaxLength = DataConverter.CLng(base.Settings[0]);
            this.TxtTitle.Columns = DataConverter.CLng(base.Settings[1]);
            if (base.IsAdminManage && DataConverter.CBoolean(base.Settings[2]))
            {
                this.LitCheckTitle.Visible = true;
                this.LitCheckTitle.Text = "<input type=\"button\" onclick=\"ShowCheckTitleMessage()\" value=\"检查是否具有重复\" />";
                StringBuilder builder = new StringBuilder();
                builder.Append("<script type=\"text/javascript\">\n");
                builder.Append("<!--\n");
                builder.Append("  function ShowCheckTitleMessage(){\n");
                builder.Append("      var urlstr= '" + basePath + "/Accessories/ShowCheckTitleMessage.aspx?NodeId='+document.getElementById(nodeIdClientId).value+'&Title='+escape(document.getElementById(\"" + this.TxtTitle.ClientID + "\").value);\n");
                builder.Append("      var isMSIE= (navigator.appName == \"Microsoft Internet Explorer\");\n");
                builder.Append("      if (isMSIE){\n");
                builder.Append("          var arr= window.showModalDialog(urlstr,'self,width=200,height=120,resizable=yes,scrollbars=yes');\n");
                builder.Append("      }else{\n");
                builder.Append("          var arr= window.open(urlstr,'newWin','modal=yes,width=400px,height=320px,resizable=yes,scrollbars=yes'); \n");
                builder.Append("           arr.moveTo(200,200);");
                builder.Append("      }\n");
                builder.Append("  }\n");
                builder.Append(" //-->\n");
                builder.Append("</script>\n");
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("ShowCheckTitleMessage()"))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "ShowCheckTitleMessage()", builder.ToString());
                }
            }
            if ((base.Settings.Count > 3) && DataConverter.CBoolean(base.Settings[3]))
            {
                this.TxtPinyinTitle.Visible = true;
                this.TxtPinyinTitle.Columns = this.TxtTitle.Columns;
                this.TxtTitle.Attributes.Add("onchange", "GetPinyinTitle(this.value)");
            }
            if (base.EnableNull)
            {
                this.ReqTxtTitle.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.TxtTitle.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.TxtTitle.Text;
            }
        }

        public string PinyinTitle
        {
            get
            {
                return this.TxtPinyinTitle.Text;
            }
            set
            {
                this.TxtPinyinTitle.Text = value;
            }
        }
    }
}


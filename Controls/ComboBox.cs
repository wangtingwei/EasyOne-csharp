namespace EasyOne.Controls
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing.Design;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true, "Items"), PersistChildren(false), DefaultProperty("ID"), ToolboxData("<{0}:ComboBox runat=\"server\"></{0}:ComboBox>")]
    public class ComboBox : DropDownList, INamingContainer
    {
        private const string ComboBox_JS = "EasyOne.Controls.ComboBox.ComboBox.js";
        private string m_Attribute;
        private ListItemCollection m_Items = new ListItemCollection();
        private TextBox m_Textbox = new TextBox();

        public ComboBox()
        {
            this.m_Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));
            this.m_Textbox.Text = this.Value;
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            this.m_Textbox.Text = postCollection[postDataKey + "_Text"];
            return base.LoadPostData(postDataKey, postCollection);
        }

        protected override void OnPreRender(EventArgs e)
        {
            ClientScriptManager clientScript = this.Page.ClientScript;
            string webResourceUrl = clientScript.GetWebResourceUrl(base.GetType(), "EasyOne.Controls.ComboBox.ComboBox.js");
            clientScript.RegisterClientScriptInclude(base.GetType(), "EasyOne.Controls.ComboBox.ComboBox.js", webResourceUrl);
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            int num = Convert.ToInt32(base.Width.Value);
            if (num == 0)
            {
                num = 120;
                base.Width = 120;
            }
            int num2 = num - 0x15;
            int num3 = 0x16;
            int num4 = 0x12;
            if (!base.DesignMode)
            {
                HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
                writer.Write("<table  border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                writer.Write("<tr>");
                writer.Write("<td>");
                writer.Write("<div style=\"position:absolute;\">");
                if (browser.Browser == "IE")
                {
                    num4 = 0x10;
                    writer.Write("<iframe frameborder=\"0\" scrolling=\"no\" style=\"z-index:-10;margin-top:1px;margin-left:1px;");
                    writer.Write("width:" + num2.ToString() + "px;height:" + num4.ToString() + "px;position:absolute;\"></iframe>");
                }
                else if (browser.Browser == "Firefox")
                {
                    num3 = 20;
                    this.m_Textbox.Style.Add("z-index", "10");
                }
                this.m_Textbox.Style.Clear();
                this.m_Textbox.Width = num2;
                this.m_Textbox.Height = num4;
                this.m_Textbox.Style.Add(HtmlTextWriterStyle.FontSize, "12px");
                this.m_Textbox.Style.Add(HtmlTextWriterStyle.FontFamily, "宋体");
                this.m_Textbox.Style.Add("margin-top", "1px");
                this.m_Textbox.Style.Add("margin-left", "2px");
                this.m_Textbox.BorderWidth = 0;
                this.m_Textbox.ID = base.UniqueID + "_Text";
                if (!string.IsNullOrEmpty(this.m_Attribute))
                {
                    this.m_Textbox.Attributes.Add("onchange", this.m_Attribute);
                }
                this.m_Textbox.RenderControl(writer);
                writer.Write("</div>");
                string str2 = string.Format("ondropchange" + "(document.getElementById('{0}'),document.getElementById('{1}'))", this.m_Textbox.ClientID, base.ClientID);
                base.Style.Clear();
                base.Attributes.Add("onchange", this.m_Attribute + str2);
                base.Height = num3;
                base.BorderWidth = 2;
                base.BorderStyle = BorderStyle.Inset;
                base.Style.Add(HtmlTextWriterStyle.FontSize, "12px");
                base.Style.Add(HtmlTextWriterStyle.FontFamily, "Times New Roman");
                base.Render(writer);
                writer.Write("</td>");
                writer.Write("</tr>");
                writer.Write("</table>");
            }
        }

        [Category("属性"), Description("自定义属性"), Browsable(true)]
        public string Attribute
        {
            get
            {
                return this.m_Attribute;
            }
            set
            {
                this.m_Attribute = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), MergableProperty(false), Category("杂项"), PersistenceMode(PersistenceMode.InnerProperty), Browsable(true), Description("ComboBox的内容项"), Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        public override ListItemCollection Items
        {
            get
            {
                return this.m_Items;
            }
        }

        [Description("ComboBox的初始文本值"), Browsable(true), Category("行为")]
        public string Value
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_Textbox.Text) && (this.SelectedItem != null))
                {
                    return this.SelectedItem.Text;
                }
                return this.m_Textbox.Text;
            }
            set
            {
                this.m_Textbox.Text = value;
            }
        }
    }
}


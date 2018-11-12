namespace EasyOne.ModelControls
{
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:SpecialControl runat=\"server\"></{0}:SpecialControl>"), Themeable(true)]
    public class SpecialControl : ListBox
    {
        private const string SpecialControljs = "SpecialControljs";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Type type = base.GetType();
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(type, "SpecialControljs"))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language = 'JavaScript'>\n");
                builder.Append("function SelectAll(){\n");
                builder.Append("  for(var i=0;i<document.getElementById('" + this.ClientID + "').options.length;i++){\n");
                builder.Append("    document.getElementById('" + this.ClientID + "').options[i].selected=true;}\n");
                builder.Append("}\n");
                builder.Append("function UnSelectAll(){\n");
                builder.Append("  for(var i=0;i<document.getElementById('" + this.ClientID + "').options.length;i++){\n");
                builder.Append("    document.getElementById('" + this.ClientID + "').options[i].selected=false;}\n");
                builder.Append("}\n");
                builder.Append("</script>\n");
                this.Page.ClientScript.RegisterClientScriptBlock(type, "SpecialControljs", builder.ToString());
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.Write("<br /><input  type='button' name='Submit' value='  选定所有专题  ' onclick='SelectAll()'>");
            writer.Write("<br /><input type='button' name='Submit' value='取消选定所有专题' onclick='UnSelectAll()'>");
        }
    }
}


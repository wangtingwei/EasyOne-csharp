namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:GridViewTemplateField runat=server></{0}:GridViewTemplateField>"), DefaultProperty("Text")]
    public class GridViewTemplateField : System.Web.UI.WebControls.TemplateField
    {
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            base.ItemTemplate = base.ViewState["GridViewTemplateField"] as GridViewTemplate;
        }

        public override ITemplate ItemTemplate
        {
            get
            {
                if (base.ViewState["GridViewTemplateField"] != null)
                {
                    return (base.ViewState["GridViewTemplateField"] as GridViewTemplate);
                }
                return base.ItemTemplate;
            }
            set
            {
                base.ItemTemplate = value;
                base.ViewState["GridViewTemplateField"] = value;
            }
        }
    }
}


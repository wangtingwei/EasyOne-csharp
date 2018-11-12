namespace EasyOne.ModelControls
{
    using EasyOne.Components;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ShowPointName  runat=\"server\"></{0}:ShowPointName>")]
    public class ShowPointName : Literal
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.PointType == EasyOne.ModelControls.PointType.PointUnit)
            {
                base.Text = SiteConfig.UserConfig.PointUnit;
            }
            else
            {
                base.Text = SiteConfig.UserConfig.PointName;
            }
        }

        public EasyOne.ModelControls.PointType PointType
        {
            get
            {
                object obj2 = this.ViewState["PointType"];
                if (obj2 != null)
                {
                    return (EasyOne.ModelControls.PointType) obj2;
                }
                return EasyOne.ModelControls.PointType.PointName;
            }
            set
            {
                this.ViewState["PointType"] = value;
            }
        }
    }
}


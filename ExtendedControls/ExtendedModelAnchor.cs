namespace EasyOne.ExtendedControls
{
    using EasyOne.AccessManage;
    using System;
    using System.ComponentModel;
    using System.Web.UI;

    [ToolboxData("<{0}:ExtendedModelAnchor ID=\"Eah\" runat=\"server\"></{0}:ExtendedModelAnchor>")]
    public class ExtendedModelAnchor : ExtendedAnchor
    {
        private string m_FieldName;
        private int m_ModelId;

        protected override void OnInit(EventArgs e)
        {
            if (base.IsChecked)
            {
                base.Disabled = !RolePermissions.AccessCheckFieldPermission(base.OperateCode, this.m_ModelId, this.m_FieldName);
            }
            if (base.IsVisible)
            {
                this.Visible = !base.Disabled;
            }
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (base.Disabled)
            {
                base.HRef = "";
                base.Attributes.Remove("onclick");
            }
        }

        [Bindable(true), Localizable(true), DefaultValue(""), Description("模型的字段名"), Category("自定义")]
        public string FieldName
        {
            get
            {
                return this.m_FieldName;
            }
            set
            {
                this.m_FieldName = value;
            }
        }

        [Category("自定义"), Bindable(true), DefaultValue(0), Description("模型ID"), Localizable(true)]
        public int ModelId
        {
            get
            {
                return this.m_ModelId;
            }
            set
            {
                this.m_ModelId = value;
            }
        }
    }
}


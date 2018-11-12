namespace EasyOne.ExtendedControls
{
    using EasyOne.AccessManage;
    using System;
    using System.ComponentModel;
    using System.Web.UI;

    [ToolboxData("<{0}:ExtendedModelLinkButton ID=\"ELbtn\" runat=\"server\"></{0}:ExtendedModelLinkButton>")]
    public class ExtendedModelLinkButton : ExtendedLinkButton
    {
        private string m_FieldName;
        private int m_ModelId;

        protected override void OnInit(EventArgs e)
        {
            if (base.IsChecked)
            {
                this.Enabled = RolePermissions.AccessCheckFieldPermission(base.OperateCode, this.m_ModelId, this.m_FieldName);
            }
            if (base.IsVisible)
            {
                this.Visible = this.Enabled;
            }
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!this.Enabled)
            {
                this.OnClientClick = "";
            }
        }

        [DefaultValue(""), Category("自定义"), Bindable(true), Description("模型的字段名"), Localizable(true)]
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

        [Description("模型ID"), Localizable(true), Category("自定义"), Bindable(true), DefaultValue(0)]
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


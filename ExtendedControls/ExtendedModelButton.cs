namespace EasyOne.ExtendedControls
{
    using EasyOne.AccessManage;
    using System;
    using System.ComponentModel;
    using System.Web.UI;

    [ToolboxData("<{0}:ExtendedModelButton ID=\"EBtn\" runat=\"server\"></{0}:ExtendedModelButton>")]
    public class ExtendedModelButton : ExtendedButton
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

        [Bindable(true), Category("自定义"), DefaultValue(""), Description("模型的字段名"), Localizable(true)]
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

        [Description("模型ID"), Category("自定义"), DefaultValue(0), Bindable(true), Localizable(true)]
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


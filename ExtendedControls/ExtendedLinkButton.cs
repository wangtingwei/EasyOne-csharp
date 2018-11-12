namespace EasyOne.ExtendedControls
{
    using EasyOne.AccessManage;
    using EasyOne.Enumerations;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ExtendedLinkButton ID=\"ELbtn\" runat=\"server\"></{0}:ExtendedLinkButton>")]
    public class ExtendedLinkButton : LinkButton
    {
        private bool m_IsChecked;
        private bool m_IsVisible;
        private EasyOne.Enumerations.OperateCode m_Operatecode;

        protected override void OnInit(EventArgs e)
        {
            bool flag = RolePermissions.AccessCheck(this.m_Operatecode);
            if (this.IsChecked && !flag)
            {
                this.Enabled = false;
            }
            if (this.IsVisible && !flag)
            {
                this.Visible = true;
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

        [Category("自定义"), Bindable(true), Localizable(true), DefaultValue(false), Description("是否启用检查")]
        public bool IsChecked
        {
            get
            {
                return this.m_IsChecked;
            }
            set
            {
                this.m_IsChecked = value;
            }
        }

        [DefaultValue(false), Category("自定义"), Localizable(true), Description("没有权限时是否可见"), Bindable(true)]
        public bool IsVisible
        {
            get
            {
                return this.m_IsVisible;
            }
            set
            {
                this.m_IsVisible = value;
            }
        }

        [Localizable(true), Bindable(true), Category("自定义"), DefaultValue(""), Description("操作资源码")]
        public EasyOne.Enumerations.OperateCode OperateCode
        {
            get
            {
                return this.m_Operatecode;
            }
            set
            {
                this.m_Operatecode = value;
            }
        }
    }
}


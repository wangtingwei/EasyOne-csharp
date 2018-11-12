namespace EasyOne.ModelControls
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using System;
    using System.Collections.ObjectModel;
    using System.Web;
    using System.Web.UI;

    [ToolboxData("<{0}:FieldControl ID=\"FldC\" runat=\"server\"></{0}:FieldControl>")]
    public class FieldControl : Control, INamingContainer
    {
        protected IFieldControl m_FieldControl;

        protected override void CreateChildControls()
        {
            Control child = this.Page.LoadControl("~/Controls/FieldControl/" + this.ControlType.ToString() + ".ascx");
            child.ID = "EasyOne2007";
            if (this.IsAdminManage)
            {
                this.Visible = RolePermissions.AccessCheckFieldPermission(OperateCode.ContentFieldInput, DataConverter.CLng(HttpContext.Current.Request.QueryString["ModelId"]), this.FieldName);
            }
            else
            {
                this.Visible = UserPermissions.AccessCheck(OperateCode.ContentFieldInput, DataConverter.CLng(HttpContext.Current.Request.QueryString["ModelId"]), this.FieldName);
            }
            this.m_FieldControl = (IFieldControl) child;
            this.m_FieldControl.Settings = this.Settings;
            this.m_FieldControl.EnableNull = this.EnableNull;
            this.m_FieldControl.FieldAlias = this.FieldAlias;
            this.m_FieldControl.FieldLevel = this.FieldLevel;
            this.m_FieldControl.FieldName = this.FieldName;
            this.m_FieldControl.Tips = this.Tips;
            this.m_FieldControl.Description = this.Description;
            this.m_FieldControl.ControlType = this.ControlType;
            this.m_FieldControl.IsAdminManage = this.IsAdminManage;
            this.m_FieldControl.InitFieldControl();
            this.Controls.Add(child);
        }

        public FieldType ControlType
        {
            get
            {
                object obj2 = this.ViewState["FieldType"];
                if (obj2 != null)
                {
                    return (FieldType) obj2;
                }
                return FieldType.None;
            }
            set
            {
                this.ViewState["FieldType"] = value;
            }
        }

        public string Description
        {
            get
            {
                object obj2 = this.ViewState["Description"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Description"] = value;
            }
        }

        public bool EnableNull
        {
            get
            {
                object obj2 = this.ViewState["EnableNull"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["EnableNull"] = value;
            }
        }

        public string FieldAlias
        {
            get
            {
                object obj2 = this.ViewState["FieldAlias"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["FieldAlias"] = value;
            }
        }

        public int FieldLevel
        {
            get
            {
                object obj2 = this.ViewState["FieldLevel"];
                if (obj2 != null)
                {
                    return (int) obj2;
                }
                return -1;
            }
            set
            {
                this.ViewState["FieldLevel"] = value;
            }
        }

        public string FieldName
        {
            get
            {
                object obj2 = this.ViewState["FieldName"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["FieldName"] = value;
            }
        }

        public bool IsAdminManage
        {
            get
            {
                object obj2 = this.ViewState["IsAdminManage"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["IsAdminManage"] = value;
            }
        }

        public Collection<string> Settings
        {
            get
            {
                object obj2 = this.ViewState["Settings"];
                if (obj2 != null)
                {
                    return (Collection<string>) obj2;
                }
                return null;
            }
            set
            {
                this.ViewState["Settings"] = value;
            }
        }

        public string Tips
        {
            get
            {
                object obj2 = this.ViewState["Tips"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Tips"] = value;
            }
        }

        public string Value
        {
            get
            {
                this.EnsureChildControls();
                return this.m_FieldControl.FieldValue;
            }
            set
            {
                this.EnsureChildControls();
                if (this.ControlType == FieldType.NodeType)
                {
                    if ((value != "0") && !string.IsNullOrEmpty(value))
                    {
                        this.m_FieldControl.FieldValue = value;
                    }
                }
                else
                {
                    this.m_FieldControl.FieldValue = value;
                }
            }
        }
    }
}


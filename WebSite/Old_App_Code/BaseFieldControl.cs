namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Enumerations;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.ObjectModel;

    public class BaseFieldControl : BaseUserControl, IFieldControl
    {
        public virtual void InitFieldControl()
        {
        }

        public FieldType ControlType
        {
            get
            {
                object obj2 = this.ViewState["ControlType"];
                if (obj2 != null)
                {
                    return (FieldType) obj2;
                }
                return FieldType.None;
            }
            set
            {
                this.ViewState["ControlType"] = value;
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
                return 0;
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

        public virtual string FieldValue
        {
            get
            {
                object obj2 = this.ViewState["FieldValue"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["FieldValue"] = value;
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
    }
}


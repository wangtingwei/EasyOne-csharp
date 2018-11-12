namespace EasyOne.ModelControls
{
    using EasyOne.AccessManage;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls.WebParts;

    public class AdminWebPartManager : WebPartManager
    {
        protected override WebPartPersonalization CreatePersonalization()
        {
            return new AdminWebPartPersonalization(this);
        }

        public override GenericWebPart CreateWebPart(Control control)
        {
            GenericWebPart part = base.CreateWebPart(control);
            IWebPartPermissibility permissibility = control as IWebPartPermissibility;
            if (permissibility != null)
            {
                part.AuthorizationFilter = permissibility.OperateCode;
            }
            return part;
        }

        protected override void OnAuthorizeWebPart(WebPartAuthorizationEventArgs e)
        {
            if (PEContext.Current.Admin.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(e.AuthorizationFilter) && Enum.IsDefined(typeof(OperateCode), e.AuthorizationFilter))
                {
                    OperateCode operateCode = (OperateCode) Enum.Parse(typeof(OperateCode), e.AuthorizationFilter);
                    if (operateCode == OperateCode.None)
                    {
                        e.IsAuthorized = true;
                    }
                    else
                    {
                        e.IsAuthorized = RolePermissions.AccessCheck(operateCode);
                    }
                }
            }
            else
            {
                e.IsAuthorized = false;
            }
        }
    }
}


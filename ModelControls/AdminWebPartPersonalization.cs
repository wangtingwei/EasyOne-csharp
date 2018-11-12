namespace EasyOne.ModelControls
{
    using EasyOne.Components;
    using System;
    using System.Collections;
    using System.Web.UI.WebControls.WebParts;

    public class AdminWebPartPersonalization : WebPartPersonalization
    {
        public AdminWebPartPersonalization(WebPartManager webPartManager) : base(webPartManager)
        {
        }

        protected override void ApplyPersonalizationState()
        {
            try
            {
                base.ApplyPersonalizationState();
            }
            catch
            {
                base.ShouldResetPersonalizationState = true;
                this.ResetPersonalizationState();
            }
        }

        protected override void ApplyPersonalizationState(WebPart webPart)
        {
            try
            {
                base.ApplyPersonalizationState(webPart);
            }
            catch
            {
                base.ShouldResetPersonalizationState = true;
                this.ResetPersonalizationState();
            }
        }

        protected override string GetAuthorizationFilter(string webPartID)
        {
            try
            {
                return base.GetAuthorizationFilter(webPartID);
            }
            catch
            {
                return "";
            }
        }

        protected override PersonalizationScope Load()
        {
            base.Load();
            if (PEContext.Current.Admin.Identity.IsAuthenticated)
            {
                return PersonalizationScope.User;
            }
            return PersonalizationScope.Shared;
        }

        protected override IDictionary UserCapabilities
        {
            get
            {
                if (PEContext.Current.Admin.Identity.IsAuthenticated)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable.Add(WebPartPersonalization.ModifyStateUserCapability, WebPartPersonalization.ModifyStateUserCapability);
                    return hashtable;
                }
                return base.UserCapabilities;
            }
        }
    }
}


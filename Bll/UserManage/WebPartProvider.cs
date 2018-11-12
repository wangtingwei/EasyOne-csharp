namespace EasyOne.UserManage
{
    using EasyOne.Components;
    using EasyOne.Model.UserManage;
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Hosting;
    using System.Web.UI.WebControls.WebParts;

    public class WebPartProvider : PersonalizationProvider
    {
        private string m_ApplicationName;

        public override PersonalizationStateInfoCollection FindState(PersonalizationScope scope, PersonalizationStateQuery query, int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 1;
            throw new NotImplementedException();
        }

        public override int GetCountOfState(PersonalizationScope scope, PersonalizationStateQuery query)
        {
            return 1;
        }

        protected override void LoadPersonalizationBlobs(WebPartManager webPartManager, string path, string userName, ref byte[] sharedDataBlob, ref byte[] userDataBlob)
        {
            userName = PEContext.Current.Admin.AdminName;
            AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(userName);
            if (!adminProfile.IsNull && !string.IsNullOrEmpty(adminProfile.WebPartSetting))
            {
                userDataBlob = Convert.FromBase64String(adminProfile.WebPartSetting);
            }
        }

        protected override void ResetPersonalizationBlob(WebPartManager webPartManager, string path, string userName)
        {
            userName = PEContext.Current.Admin.AdminName;
            AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(userName);
            adminProfile.AdminName = userName;
            adminProfile.WebPartSetting = "";
            if (!adminProfile.IsNull)
            {
                AdminProfile.Update(adminProfile);
            }
            else
            {
                AdminProfile.Add(adminProfile);
            }
        }

        public override int ResetState(PersonalizationScope scope, string[] paths, string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int ResetUserState(string path, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        protected override void SavePersonalizationBlob(WebPartManager webPartManager, string path, string userName, byte[] dataBlob)
        {
            userName = PEContext.Current.Admin.AdminName;
            AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(userName);
            adminProfile.AdminName = userName;
            adminProfile.WebPartSetting = Convert.ToBase64String(dataBlob);
            if (!adminProfile.IsNull)
            {
                AdminProfile.Update(adminProfile);
            }
            else
            {
                AdminProfile.Add(adminProfile);
            }
        }

        public override string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_ApplicationName))
                {
                    this.m_ApplicationName = HostingEnvironment.ApplicationVirtualPath;
                }
                return this.m_ApplicationName;
            }
            set
            {
                this.m_ApplicationName = value;
            }
        }
    }
}


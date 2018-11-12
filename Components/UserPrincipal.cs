namespace EasyOne.Components
{
    using EasyOne.Common;
    using EasyOne.Model.UserManage;
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Principal;
    using System.Web.Security;

    [Serializable]
    public class UserPrincipal : IPrincipal
    {
        private IIdentity m_identity;
        private string m_LastPassword;
        [NonSerialized]
        private UserPurviewInfo m_PurviewInfo;
        [NonSerialized]
        private EasyOne.Model.UserManage.UserInfo m_UserInfo;
        private string m_UserName;

        public UserPrincipal()
        {
        }

        public UserPrincipal(IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            this.m_identity = identity;
        }

        public static UserPrincipal CreatePrincipal(FormsAuthenticationTicket ticket)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(ticket.UserData));
                UserPrincipal principal = (UserPrincipal) formatter.Deserialize(serializationStream);
                principal.UserInfo = new EasyOne.Model.UserManage.UserInfo(true);
                serializationStream.Dispose();
                principal.Identity = new FormsIdentity(ticket);
                return principal;
            }
            catch (ArgumentNullException)
            {
                return new UserPrincipal(new NoAuthenticateIdentity());
            }
            catch (FormatException)
            {
                return new UserPrincipal(new NoAuthenticateIdentity());
            }
            catch (SerializationException)
            {
                return new UserPrincipal(new NoAuthenticateIdentity());
            }
        }

        public bool IsInRole(string role)
        {
            int num = DataConverter.CLng(role);
            return ((num != 0) && (num == this.RoleId));
        }

        public string SerializeToString()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream();
            formatter.Serialize(serializationStream, this);
            string str = Convert.ToBase64String(serializationStream.ToArray());
            serializationStream.Dispose();
            return str;
        }

        public int GroupId
        {
            get
            {
                return this.UserInfo.GroupId;
            }
        }

        public IIdentity Identity
        {
            get
            {
                return this.m_identity;
            }
            set
            {
                this.m_identity = value;
            }
        }

        public string LastPassword
        {
            get
            {
                return this.m_LastPassword;
            }
            set
            {
                this.m_LastPassword = value;
            }
        }

        public UserPurviewInfo PurviewInfo
        {
            get
            {
                return this.m_PurviewInfo;
            }
            set
            {
                this.m_PurviewInfo = value;
            }
        }

        public int RoleId
        {
            get
            {
                if (!this.UserInfo.IsInheritGroupRole)
                {
                    return this.UserInfo.UserId;
                }
                return this.UserInfo.GroupId;
            }
        }

        public int UserId
        {
            get
            {
                return this.m_UserInfo.UserId;
            }
        }

        public EasyOne.Model.UserManage.UserInfo UserInfo
        {
            get
            {
                return this.m_UserInfo;
            }
            set
            {
                this.m_UserInfo = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }
    }
}


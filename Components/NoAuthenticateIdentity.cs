namespace EasyOne.Components
{
    using System;
    using System.Security.Principal;

    public class NoAuthenticateIdentity : IIdentity
    {
        public string AuthenticationType
        {
            get
            {
                return "";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return false;
            }
        }

        public string Name
        {
            get
            {
                return "";
            }
        }
    }
}


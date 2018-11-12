namespace EasyOne.Components
{
    using EasyOne.Model.UserManage;
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Principal;
    using System.Web.Security;
    /// <summary>
    /// 定义用户对象的基本功能
    /// </summary>
    [Serializable]
    public class AdminPrincipal : IPrincipal
    {
        [NonSerialized]
        private EasyOne.Model.UserManage.AdministratorInfo m_AdministratorInfo;
        private string m_AdminName;
        private IIdentity m_identity;
        private string m_RndPassword;
        private string[] m_roles;
        [NonSerialized]
        private string m_Roles;
        private string m_UserName;
        private const string SuperAdminRoleId = "0";

        public AdminPrincipal()
        {
        }
        /// <summary>
        /// 构造函数据初始化用户通用功能
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="roles"></param>
        public AdminPrincipal(IIdentity identity, string[] roles)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            this.m_identity = identity;
            if (roles != null)
            {
                this.m_roles = new string[roles.Length];
                for (int i = 0; i < roles.Length; i++)
                {
                    this.m_roles[i] = roles[i];
                }
            }
        }

        public static AdminPrincipal CreatePrincipal(FormsAuthenticationTicket ticket)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(ticket.UserData));
                AdminPrincipal principal = (AdminPrincipal) formatter.Deserialize(serializationStream);
                serializationStream.Dispose();
                principal.Identity = new FormsIdentity(ticket);
                return principal;
            }
            catch (ArgumentNullException)
            {
                return new AdminPrincipal(new NoAuthenticateIdentity(), null);
            }
            catch (FormatException)
            {
                return new AdminPrincipal(new NoAuthenticateIdentity(), null);
            }
            catch (SerializationException)
            {
                return new AdminPrincipal(new NoAuthenticateIdentity(), null);
            }
        }
        /// <summary>
        /// 判断当前用户是否有权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            if ((role != null) && (this.m_roles != null))
            {
                foreach (string str in role.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    for (int i = 0; i < this.m_roles.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(this.m_roles[i]) && (string.Compare(this.m_roles[i], str, StringComparison.OrdinalIgnoreCase) == 0))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 序列化数据
        /// </summary>
        /// <returns></returns>
        public string SerializeToString()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream();
            formatter.Serialize(serializationStream, this);
            string str = Convert.ToBase64String(serializationStream.ToArray());
            serializationStream.Dispose();
            return str;
        }

        public EasyOne.Model.UserManage.AdministratorInfo AdministratorInfo
        {
            get
            {
                return this.m_AdministratorInfo;
            }
            set
            {
                this.m_AdministratorInfo = value;
            }
        }
        /// <summary>
        ///管理员用户名
        /// </summary>
        public string AdminName
        {
            get
            {
                return this.m_AdminName;
            }
            set
            {
                this.m_AdminName = value;
            }
        }
        /// <summary>
        /// 代表特定用户的标识对象，代码当前即以该用户的名义运行
        /// </summary>
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
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSuperAdmin
        {
            get
            {
                return this.IsInRole("0");
            }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string RndPassword
        {
            get
            {
                return this.m_RndPassword;
            }
            set
            {
                this.m_RndPassword = value;
            }
        }
        /// <summary>
        /// 用户角色
        /// </summary>
        public string Roles
        {
            get
            {
                return this.m_Roles;
            }
            set
            {
                this.m_Roles = value;
                if (!string.IsNullOrEmpty(value))
                {
                    this.m_roles = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
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


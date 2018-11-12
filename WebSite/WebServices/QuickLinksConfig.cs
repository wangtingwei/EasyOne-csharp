namespace EasyOne.WebSite.Admin.Profile
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.Script.Services;
    using System.Web.Services;
    using System.Xml;

    [WebService(Namespace="http://tempuri.org/"), WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1), ToolboxItem(false), ScriptService]
    public class QuickLinksConfig : WebService
    {
        [WebMethod]
        public string AddLink(string id)
        {
            string adminName = PEContext.Current.Admin.AdminName;
            AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(adminName);
            adminProfile.AdminName = adminName;
            Collection<string> quickLinksConfigCollection = this.ConvertToCollection(adminProfile.QuickLinksConfig);
            if (quickLinksConfigCollection.Contains(id))
            {
                return "false";
            }
            if (!adminProfile.IsNull)
            {
                quickLinksConfigCollection.Add(id);
                adminProfile.QuickLinksConfig = this.ConvertToString(quickLinksConfigCollection);
                AdminProfile.Update(adminProfile);
            }
            else
            {
                string str2;
                XmlDocument document = new XmlDocument();
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    str2 = current.Server.MapPath("~/Admin/Common/QuickLinks.xml");
                }
                else
                {
                    str2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Admin/Common/QuickLinks.xml");
                }
                document.Load(str2);
                foreach (XmlNode node in document.SelectNodes("/Links//Link[@IsDefalutShow='true']"))
                {
                    if (this.CheckPermission(node.Attributes["operateCode"].Value))
                    {
                        quickLinksConfigCollection.Add(node.Attributes["id"].Value);
                    }
                }
                quickLinksConfigCollection.Add(id);
                adminProfile.QuickLinksConfig = this.ConvertToString(quickLinksConfigCollection);
                AdminProfile.Add(adminProfile);
            }
            return "true";
        }

        private bool CheckPermission(string operateCode)
        {
            if (operateCode == "None")
            {
                return true;
            }
            if (!Enum.IsDefined(typeof(OperateCode), operateCode))
            {
                return false;
            }
            OperateCode code = (OperateCode) Enum.Parse(typeof(OperateCode), operateCode);
            return RolePermissions.AccessCheck(code);
        }

        private Collection<string> ConvertToCollection(string quickLinksConfig)
        {
            Collection<string> collection = new Collection<string>();
            if (!string.IsNullOrEmpty(quickLinksConfig))
            {
                foreach (string str in quickLinksConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    collection.Add(str);
                }
            }
            return collection;
        }

        private string ConvertToString(Collection<string> quickLinksConfigCollection)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in quickLinksConfigCollection)
            {
                StringHelper.AppendString(sb, str);
            }
            return sb.ToString();
        }

        [WebMethod]
        public string DeleteLink(string id)
        {
            AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(PEContext.Current.Admin.AdminName);
            if (adminProfile.IsNull)
            {
                return "false";
            }
            Collection<string> quickLinksConfigCollection = this.ConvertToCollection(adminProfile.QuickLinksConfig);
            if (!quickLinksConfigCollection.Contains(id))
            {
                return "false";
            }
            quickLinksConfigCollection.Remove(id);
            adminProfile.QuickLinksConfig = this.ConvertToString(quickLinksConfigCollection);
            AdminProfile.Update(adminProfile);
            return "true";
        }

        [WebMethod]
        public string HelloWorld()
        {
            Thread.Sleep(0x7d0);
            return "Hello World";
        }

        [WebMethod]
        public string UpdateLinkSort(string sorts)
        {
            string adminName = PEContext.Current.Admin.AdminName;
            AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(adminName);
            adminProfile.AdminName = adminName;
            if (!adminProfile.IsNull)
            {
                adminProfile.QuickLinksConfig = sorts;
                AdminProfile.Update(adminProfile);
                return "ok";
            }
            return "err";
        }
    }
}


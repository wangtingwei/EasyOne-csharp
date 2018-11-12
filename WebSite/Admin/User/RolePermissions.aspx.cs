namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.AccessManage;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class RolePermissionsManage : AdminPage
    {
        private int m_RoleId;
        private string m_RoleName;
        private XmlDocument xmlDoc;
        private string xmlPath = "menu";

        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("RoleManage.aspx");
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.m_RoleId != 0)
            {
                this.GetModelPermission();
            }
        }

        private string Checked(XmlNode child)
        {
            string str = "";
            if (this.GetAttributeValue(child, "IsChoose") == "true")
            {
                str = " Checked ";
            }
            return str;
        }

        private string Description(XmlNode child)
        {
            string attributeValue = "";
            attributeValue = this.GetAttributeValue(child, "Description");
            if (!string.IsNullOrEmpty(attributeValue))
            {
                attributeValue = "<span style='color:green'>" + attributeValue + "</span>";
            }
            return attributeValue;
        }

        private string GetAttributeValue(XmlNode xmlNode, string attributeName)
        {
            string str = "";
            if (xmlNode != null)
            {
                XmlAttribute attribute = xmlNode.Attributes[attributeName];
                if (attribute != null)
                {
                    str = attribute.Value;
                }
            }
            return str;
        }

        private void GetModelPermission()
        {
            string str = base.Request.Form["ModelPurview"];
            RolePermissions.DeletePermissionFromRoles(this.m_RoleId);
            if (!string.IsNullOrEmpty(str))
            {
                RolePermissions.AddModelPermissionToRoles(this.m_RoleId, str.Trim());
            }
            if (BasePage.RequestString("Action") != "Add")
            {
                AdminPage.WriteSuccessMsg("<li>角色" + this.m_RoleName + "权限设置成功！</li>", "RoleManage.aspx");
            }
            else
            {
                BasePage.ResponseRedirect("RoleMember.aspx?RoleID=" + this.m_RoleId.ToString());
            }
        }

        private void InitChannelMenuLi(StringBuilder sb, string channelMenuId)
        {
            string xpath = this.xmlPath + "/channelMenu[@id='" + channelMenuId + "']";
            XmlNode node = this.xmlDoc.SelectSingleNode(xpath);
            if ((node != null) && node.HasChildNodes)
            {
                foreach (XmlNode node2 in node)
                {
                    string attributeValue = this.GetAttributeValue(node2, "operateCode");
                    string subModel = channelMenuId + "_" + this.GetAttributeValue(node2, "id");
                    string str4 = this.Checked(node2);
                    if (!(node2.Name == "mainMenu") || !(this.GetAttributeValue(node2, "ShowOnForm") == "true"))
                    {
                        goto Label_01CB;
                    }
                    sb.Append("<tr>");
                    sb.Append("  <td style='padding-left:30px;'>");
                    sb.Append("     <input type='checkbox' name='ModelPurview' value='" + this.GetAttributeValue(node2, "operateCode") + "' " + str4 + " id='" + subModel + "'  onclick=\"javascript:CheckModel(this);\"  />&nbsp;");
                    sb.Append(this.GetAttributeValue(node2, "title").Replace("点券", SiteConfig.UserConfig.PointName));
                    string str5 = attributeValue;
                    if (str5 != null)
                    {
                        if (!(str5 == "NodesManage"))
                        {
                            if (str5 == "CommentManage")
                            {
                                goto Label_0181;
                            }
                            if (str5 == "ProductManage")
                            {
                                goto Label_018F;
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;&nbsp;&lt;=【<a onclick=\"javascript:ShowWindow(4)\" href='#' ><span style='color:red'>详细设置</span></a>】");
                        }
                    }
                    goto Label_019B;
                Label_0181:
                    sb.Append("&nbsp;&nbsp;&lt;=【<a onclick=\"javascript:ShowWindow(3)\" href='#' ><span style='color:red'>详细设置</span></a>】");
                    goto Label_019B;
                Label_018F:
                    sb.Append("&nbsp;&nbsp;&lt;=【<a onclick=\"javascript:ShowWindow(1)\" href='#' ><span style='color:red'>详细设置</span></a>】");
                Label_019B:
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + this.Description(node2));
                    sb.Append("  </td>");
                    sb.Append("</tr>");
                Label_01CB:
                    this.InitMainMenu(sb, xpath + "/mainMenu[@id='" + this.GetAttributeValue(node2, "id") + "']", subModel);
                }
            }
        }

        private void InitMainMenu(StringBuilder sb, string path, string subModel)
        {
            XmlNode node = this.xmlDoc.SelectSingleNode(path);
            if ((node != null) && node.HasChildNodes)
            {
                foreach (XmlNode node2 in node)
                {
                    string attributeValue = this.GetAttributeValue(node2, "operateCode");
                    if (!(this.GetAttributeValue(node2, "ShowOnForm") == "true"))
                    {
                        continue;
                    }
                    string purviewModel = subModel + "_" + attributeValue;
                    string str3 = this.Checked(node2);
                    sb.Append("<tr>");
                    sb.Append("  <td style='padding-left:60px;'>");
                    sb.Append("     <input type='checkbox' name='ModelPurview' value='" + attributeValue + "' id='" + purviewModel + "' " + str3 + " onclick=\"javascript:CheckModel(this);\" />&nbsp;");
                    sb.Append(this.GetAttributeValue(node2, "title").Replace("点券", SiteConfig.UserConfig.PointName));
                    string str4 = attributeValue;
                    if (str4 != null)
                    {
                        if (!(str4 == "CategoryInfoManage"))
                        {
                            if ((str4 == "SpecialInfoManage") || (str4 == "ProductSpecialInfoManage"))
                            {
                                goto Label_013D;
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;&nbsp;&lt;=【<a onclick=\"javascript:ShowWindow(1)\" href='#' ><span style='color:red'>详细设置</span></a>】");
                        }
                    }
                    goto Label_0149;
                Label_013D:
                    sb.Append("&nbsp;&nbsp;&lt;=【<a onclick=\"javascript:ShowWindow(2)\" href='#' ><span style='color:red'>详细设置</span></a>】");
                Label_0149:
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + this.Description(node2));
                    sb.Append("  </td>");
                    sb.Append("</tr>");
                    this.InitSubMenu(sb, path + "/subMenu[@id='" + this.GetAttributeValue(node2, "id") + "']", purviewModel);
                }
            }
        }

        private void InitPurview()
        {
            string str;
            this.xmlDoc = new XmlDocument();
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                str = current.Server.MapPath("~/Admin/Common/MainMenu.xml");
            }
            else
            {
                str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Admin/Common/MainMenu.xml");
            }
            try
            {
                this.xmlDoc.Load(str);
            }
            catch (XmlException exception)
            {
                AdminPage.WriteErrMsg("MainMenu.xml配置文件不符合XML规范，具体错误信息：" + exception.Message);
            }
            XmlNode node = this.xmlDoc.SelectSingleNode(this.xmlPath);
            StringBuilder sb = new StringBuilder();
            if (node == null)
            {
                AdminPage.WriteErrMsg("MainMenu.xml配置文件不存在menu根元素");
            }
            if (!this.Page.IsPostBack)
            {
                XmlNodeList list = this.xmlDoc.SelectNodes("//*");
                IList<RoleModulePermissionsInfo> otherModelPermissionsById = RolePermissions.GetOtherModelPermissionsById(this.m_RoleId);
                foreach (XmlNode node2 in list)
                {
                    if (string.CompareOrdinal(this.GetAttributeValue(node2, "ShowOnForm"), "true") == 0)
                    {
                        string attributeValue = this.GetAttributeValue(node2, "operateCode");
                        foreach (RoleModulePermissionsInfo info in otherModelPermissionsById)
                        {
                            if (!Enum.IsDefined(typeof(OperateCode), attributeValue) && (RolePermissions.MD5(attributeValue).CompareTo(DataConverter.CLng(info.OperateCode)) == 0))
                            {
                                ((XmlElement) node2).SetAttribute("IsChoose", "true");
                            }
                        }
                        continue;
                    }
                }
                foreach (RoleModulePermissionsInfo info2 in RolePermissions.GetModelPermissionsById(this.m_RoleId))
                {
                    string name = Enum.GetName(typeof(OperateCode), info2.OperateCode);
                    foreach (XmlNode node3 in this.xmlDoc.SelectNodes("//*[@operateCode='" + name + "']"))
                    {
                        if (node3 != null)
                        {
                            ((XmlElement) node3).SetAttribute("IsChoose", "true");
                        }
                    }
                }
            }
            if (node.HasChildNodes)
            {
                foreach (XmlNode node4 in node)
                {
                    string str4 = this.Checked(node4);
                    string channelMenuId = this.GetAttributeValue(node4, "id");
                    if ((channelMenuId != "MenuMyDeskTop") && (this.GetAttributeValue(node4, "ShowOnForm") == "true"))
                    {
                        sb.Append("<tr>");
                        sb.Append("  <td style='width:100%;'>");
                        sb.Append("     <input type='checkbox' name='ModelPurview' value='" + this.GetAttributeValue(node4, "operateCode") + "' " + str4 + " id='" + channelMenuId + "'  onclick=\"javascript:CheckModel(this);\" />&nbsp;");
                        sb.Append(this.GetAttributeValue(node4, "title"));
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + this.Description(node4));
                        sb.Append("  </td>");
                        sb.Append("</tr>");
                        this.InitChannelMenuLi(sb, channelMenuId);
                    }
                }
            }
            this.LblModelPurview.Text = sb.ToString();
        }

        private void InitRole()
        {
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                AdminPage.WriteErrMsg("<li>本模块只有超级管理员身份才能访问！</li>");
            }
            this.m_RoleId = BasePage.RequestInt32("RoleId");
            RoleInfo roleInfoByRoleId = UserRole.GetRoleInfoByRoleId(this.m_RoleId);
            if (roleInfoByRoleId.IsNull)
            {
                AdminPage.WriteErrMsg("没有建立角色，请检查该角色是否存在！");
            }
            this.LblRoleName.Text = roleInfoByRoleId.RoleName;
            this.LblDescription.Text = roleInfoByRoleId.Description;
            this.m_RoleName = roleInfoByRoleId.RoleName;
        }

        private void InitSubMenu(StringBuilder sb, string path, string purviewModel)
        {
            XmlNode node = this.xmlDoc.SelectSingleNode(path);
            if ((node != null) && node.HasChildNodes)
            {
                foreach (XmlNode node2 in node)
                {
                    string attributeValue = this.GetAttributeValue(node2, "operateCode");
                    if (this.GetAttributeValue(node2, "ShowOnForm") == "true")
                    {
                        string str2 = purviewModel + "_" + attributeValue;
                        string str3 = this.Checked(node2);
                        sb.Append("<tr>");
                        sb.Append("  <td style='padding-left:90px;'>");
                        sb.Append("     <input type='checkbox' name='ModelPurview' value='" + attributeValue + "' id='" + str2 + "' " + str3 + " onclick=\"javascript:CheckModel(this);\" />&nbsp;");
                        sb.Append(node2.Attributes["title"].Value.Replace("点券", SiteConfig.UserConfig.PointName));
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + this.Description(node2));
                        sb.Append("  </td>");
                        sb.Append("</tr>");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitRole();
            this.InitPurview();
        }
    }
}


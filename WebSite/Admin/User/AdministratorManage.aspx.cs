namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class AdministratorManage : AdminPage
    {
        protected void Egv_RowCommand(object sender, CommandEventArgs e)
        {
            int adminId = DataConverter.CLng(e.CommandArgument);
            if (string.Compare("ModifyAdmin", e.CommandName, StringComparison.OrdinalIgnoreCase) == 0)
            {
                BasePage.ResponseRedirect("Administrator.aspx?Action=Modify&AdminId=" + adminId);
            }
            if (string.Compare("DeleteAdmin", e.CommandName, StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (PEContext.Current.Admin.AdministratorInfo.AdminId == adminId)
                {
                    AdminPage.WriteErrMsg("不能删除自己！", "AdministratorManage.aspx");
                }
                else
                {
                    Administrators.Delete(adminId);
                    AdminPage.WriteSuccessMsg("删除管理员成功！", "AdministratorManage.aspx");
                }
            }
            if (string.Compare("LockAdmin", e.CommandName, StringComparison.OrdinalIgnoreCase) == 0)
            {
                AdministratorInfo administratorByAdminId = Administrators.GetAdministratorByAdminId(adminId);
                administratorByAdminId.IsLock = !administratorByAdminId.IsLock;
                Administrators.Update(administratorByAdminId);
                this.Egv.DataBind();
            }
        }

        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AdministratorInfo dataItem = (AdministratorInfo) e.Row.DataItem;
                Label label = (Label) e.Row.FindControl("LabEnableMultiLogin");
                Label label2 = (Label) e.Row.FindControl("LabLastLoginTime");
                Label label3 = (Label) e.Row.FindControl("LabIsLock");
                LinkButton button = (LinkButton) e.Row.FindControl("LnkLock");
                HyperLink link = (HyperLink) e.Row.FindControl("HypUserName");
                Label label4 = (Label) e.Row.FindControl("LabRoleList");
                ExtendedHyperLink link2 = (ExtendedHyperLink) e.Row.FindControl("LnkManageName");
                Label label5 = (Label) e.Row.FindControl("LabLastModifyPasswordTime");
                Label label6 = (Label) e.Row.FindControl("LabLastLoginIp");
                Literal literal = (Literal) e.Row.FindControl("LtrRoleList");
                label6.Text = dataItem.LastLogOnIP;
                if (!dataItem.LastModifyPasswordTime.HasValue)
                {
                    label5.Text = "未修改过";
                }
                else
                {
                    label5.Text = dataItem.LastModifyPasswordTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (string.CompareOrdinal("," + RoleMembers.GetRoleIdListByAdminId(dataItem.AdminId) + ",", ",0,") == 0)
                {
                    link2.Text = dataItem.AdminName;
                    link2.NavigateUrl = "Administrator.aspx?Action=Modify&AdminId=" + dataItem.AdminId;
                    link2.BeginTag = "<strong><font color=\"blue\">";
                    link2.EndTag = "</font></strong>";
                }
                else
                {
                    link2.Text = dataItem.AdminName;
                    link2.NavigateUrl = "Administrator.aspx?Action=Modify&AdminId=" + dataItem.AdminId;
                }
                string[] strArray = dataItem.RoleList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length > 1)
                {
                    label4.Text = strArray[0];
                    literal.Text = ",<span style=\"cursor:pointer;\" Title='" + dataItem.RoleList.Replace(strArray[0] + ",", "") + "' >[更多]</span>";
                }
                else
                {
                    label4.Text = dataItem.RoleList;
                }
                link.Text = dataItem.UserName;
                link.NavigateUrl = "UserShow.aspx?UserName=" + base.Server.UrlEncode(dataItem.UserName);
                if (dataItem.EnableMultiLogOn)
                {
                    label.Text = "允许";
                }
                else
                {
                    label.Text = "<font color=\"red\">不允许</font>";
                }
                if (dataItem.IsLock)
                {
                    label3.Text = "<font color=\"red\">已锁定</font>";
                    button.Text = "解锁";
                }
                else
                {
                    label3.Text = "<font color=\"blue\">正常</font>";
                    button.Text = "锁定";
                }
                if (dataItem.LastLogOnTime.HasValue)
                {
                    label2.Text = dataItem.LastLogOnTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (PEContext.Current.Admin.AdministratorInfo.AdminId == dataItem.AdminId)
                {
                    ((LinkButton) e.Row.FindControl("LnkLock")).Enabled = false;
                    ((LinkButton) e.Row.FindControl("LnkDelete")).Enabled = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                AdminPage.WriteErrMsg("<li>本模块只有超级管理员身份才能访问！</li>");
            }
            int roleId = BasePage.RequestInt32("RoleId", -1);
            this.HdnListType.Value = BasePage.RequestInt32("ListType", 0).ToString();
            this.HdnRoleId.Value = roleId.ToString();
            if (roleId > -1)
            {
                if (roleId == 0)
                {
                    this.SmpNavigator.AdditionalNode = "<font color=red>超级管理员</font>";
                }
                else
                {
                    this.SmpNavigator.AdditionalNode = "所属<font color=red>" + UserRole.GetRoleInfoByRoleId(roleId).RoleName + "</font>角色下包含的管理员";
                }
            }
        }
    }
}


namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.AccessManage;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.Templates;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Specials : AdminPage
    {
        private int specialCategoryId;
        private int specialId;

        private void AddSpecial(SpecialInfo specialInfo)
        {
            if (Special.ExistsSpecialName(specialInfo.SpecialName))
            {
                AdminPage.WriteErrMsg("<li>此专题名已经存在，请输入其它的专题名！</li>", "");
            }
            if (Special.ExistsSpecialDir(specialInfo.SpecialDir))
            {
                AdminPage.WriteErrMsg("<li>此专题目录已经存在，请输入其它的目录名！</li>", "");
            }
            if (Special.AddSpecial(specialInfo))
            {
                this.InputPermissions(specialInfo.SpecialId);
                this.InputRolePermission(specialInfo.SpecialId);
                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Special);
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                AdminPage.WriteSuccessMsg("<li>专题信息保存成功！</li>", "Special.aspx?SpecialCategoryID=" + this.specialCategoryId.ToString());
            }
            else
            {
                AdminPage.WriteErrMsg("<li>专题信息保存失败！</li>", "");
            }
        }

        private static void AppendSelectId(bool isChecked, StringBuilder roleIdList, string selectId)
        {
            if (isChecked)
            {
                StringHelper.AppendString(roleIdList, selectId);
            }
        }

        private bool BatchAddSpecial(SpecialInfo specialInfo, StringBuilder information)
        {
            bool flag = false;
            if (IsDir(specialInfo.SpecialDir))
            {
                information.Append("<li>" + specialInfo.SpecialName + "目录名只能是字母、数字、下划线组成，首字符不能是数字！</li>");
                return flag;
            }
            if (Special.ExistsSpecialName(specialInfo.SpecialName))
            {
                information.Append("<li>" + specialInfo.SpecialName + "专题名已经存在，请输入其它的专题名！</li>");
                return flag;
            }
            if (Special.ExistsSpecialDir(specialInfo.SpecialDir))
            {
                information.Append("<li>" + specialInfo.SpecialDir + "专题目录已经存在，请输入其它的目录名！</li>");
                return flag;
            }
            if (Special.AddSpecial(specialInfo))
            {
                this.InputPermissions(specialInfo.SpecialId);
                this.InputRolePermission(specialInfo.SpecialId);
                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Special);
                return true;
            }
            information.Append("<li>" + specialInfo.SpecialName + "专题信息保存失败！</li>");
            return flag;
        }

        private void BindSpecial()
        {
            SpecialInfo specialInfoById = Special.GetSpecialInfoById(this.specialId);
            if (specialInfoById.IsNull)
            {
                AdminPage.WriteErrMsg("发生错误，请传入正确的专题ID！");
            }
            BasePage.SetSelectedIndexByValue(this.DropSpecialCategory, specialInfoById.SpecialCategoryId.ToString());
            this.TxtSpecialName.Text = specialInfoById.SpecialName;
            this.TxtSpecialIdentifier.Text = specialInfoById.SpecialIdentifier;
            this.TxtSpecialDir.Enabled = false;
            string[] strArray = specialInfoById.SpecialDir.Split(new char[] { '/' });
            if (strArray.Length > 1)
            {
                this.TxtSpecialDir.Text = strArray[1];
            }
            else
            {
                this.TxtSpecialDir.Text = strArray[0];
            }
            this.TxtSpecialPic.Text = specialInfoById.SpecialPic;
            this.TxtSpecialTips.Text = specialInfoById.SpecialTips;
            this.TxtDescription.Text = specialInfoById.Description;
            BasePage.SetSelectedIndexByValue(this.RadlIsElite, specialInfoById.IsElite.ToString());
            BasePage.SetSelectedIndexByValue(this.RadOpenType, specialInfoById.OpenType.ToString());
            this.FileCSpecialTemplatePath.Text = specialInfoById.SpecialTemplatePath;
            this.FileCSearchTemplatePath.Text = specialInfoById.SearchTemplatePath;
            BasePage.SetSelectedIndexByValue(this.RadlCreatHtml, specialInfoById.IsCreateListPage.ToString());
            this.PagePostfix.Value = specialInfoById.ListPagePostfix;
            BasePage.SetSelectedIndexByValue(this.RadlListPageHtmlDirType, specialInfoById.ListPageSavePathType.ToString());
            int num = 1;
            if (!string.IsNullOrEmpty(specialInfoById.CustomContent))
            {
                string[] strArray2 = specialInfoById.CustomContent.Split(new string[] { "{#$$$#}" }, StringSplitOptions.None);
                int num2 = 1;
                foreach (string str in strArray2)
                {
                    num = num2;
                    ((TextBox) this.PnlCustomFileds.FindControl("Custom_Content" + num2)).Text = str;
                    ((HtmlTableRow) this.PnlCustomFileds.FindControl("objFiles" + num2)).Attributes.Add("style", "display: ''");
                    num2++;
                }
            }
            ((DropDownList) this.PnlCustomFileds.FindControl("DropCustomNum")).SelectedValue = num.ToString();
            this.HdnSpecialName.Value = specialInfoById.SpecialName;
            this.HdnSpecialDir.Value = specialInfoById.SpecialDir;
            this.HdnOrderId.Value = specialInfoById.OrderId.ToString();
        }

        protected void EBtnAdd_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                SpecialInfo specialInfo = this.GetSpecialInfo();
                if (BasePage.RequestString("AddType").CompareTo("BatchSpecial") != 0)
                {
                    this.AddSpecial(specialInfo);
                }
                else
                {
                    string text = this.TxtSpecialNames.Text;
                    string str2 = this.TxtSpecialIdentifiers.Text;
                    string str3 = this.TxtSpecialDirs.Text;
                    if (text.IndexOf("\r\n") > 0)
                    {
                        string[] strArray = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        if ((str2.IndexOf("\r\n") <= 0) || (str3.IndexOf("\r\n") <= 0))
                        {
                            AdminPage.WriteErrMsg("<li>专题标识符或专题的目录名不能为空！</li>");
                        }
                        else
                        {
                            string[] strArray2 = str2.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            string[] strArray3 = str3.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            if ((strArray2.Length != strArray.Length) || (strArray3.Length != strArray.Length))
                            {
                                AdminPage.WriteErrMsg("<li>专题标识符或专题的目录名与专题名称数目不相同！</li>");
                            }
                            StringBuilder information = new StringBuilder();
                            bool flag = true;
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                specialInfo.SpecialName = strArray[i];
                                specialInfo.SpecialIdentifier = strArray2[i];
                                specialInfo.SpecialDir = strArray3[i];
                                if (!this.BatchAddSpecial(specialInfo, information))
                                {
                                    flag = false;
                                }
                            }
                            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                            if (flag)
                            {
                                AdminPage.WriteSuccessMsg("<li>批量添加专题成功！</li>", "Special.aspx?SpecialCategoryID=" + this.specialCategoryId.ToString());
                            }
                            else
                            {
                                AdminPage.WriteErrMsg(information.ToString());
                            }
                        }
                    }
                    else
                    {
                        specialInfo.SpecialName = text;
                        specialInfo.SpecialIdentifier = str2;
                        specialInfo.SpecialDir = str3;
                        this.AddSpecial(specialInfo);
                    }
                }
            }
        }

        protected void EBtnModify_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                SpecialInfo specialInfo = this.GetSpecialInfo();
                specialInfo.SpecialId = this.specialId;
                specialInfo.OrderId = DataConverter.CLng(this.HdnOrderId.Value);
                specialInfo.NeedCreateHtml = true;
                string specialName = specialInfo.SpecialName;
                if ((specialName != this.HdnSpecialName.Value) && Special.ExistsSpecialName(specialName))
                {
                    AdminPage.WriteErrMsg("<li>此专题名已经存在，请输入其它的专题名！</li>", "Special.aspx");
                }
                if (Special.UpdateSpecial(specialInfo))
                {
                    this.InputPermissions(specialInfo.SpecialId);
                    this.InputRolePermission(specialInfo.SpecialId);
                    IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Special);
                    AdminPage.WriteSuccessMsg("<li>专题信息保存成功！</li>", "SpecialManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>专题信息保存失败！</li>", "SpecialManage.aspx");
                }
            }
        }

        protected void EgvPermissions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserGroupsInfo dataItem = (UserGroupsInfo) e.Row.DataItem;
                CheckBox box = (CheckBox) e.Row.FindControl("ChkSpecialInput");
                foreach (RoleSpecialPermissionsInfo info2 in UserPermissions.GetSpecialPermssionList(dataItem.GroupId, BasePage.RequestInt32("SpecialID"), OperateCode.None, 1))
                {
                    bool flag = info2.SpecialId != -1;
                    if (info2.OperateCode == OperateCode.SpecialContentInput)
                    {
                        box.Checked = true;
                        if (!flag)
                        {
                            box.Enabled = flag;
                        }
                    }
                }
            }
        }

        protected void EgvRoleView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RoleInfo dataItem = (RoleInfo) e.Row.DataItem;
                CheckBox box = (CheckBox) e.Row.FindControl("ChkSpecialInput");
                CheckBox box2 = (CheckBox) e.Row.FindControl("ChkSpecialManage");
                IList<RoleSpecialPermissionsInfo> specialPermssionList = RolePermissions.GetSpecialPermssionList(dataItem.RoleId, BasePage.RequestInt32("SpecialID"));
                bool flag = false;
                foreach (RoleSpecialPermissionsInfo info2 in specialPermssionList)
                {
                    flag = info2.SpecialId != -1;
                    if (info2.OperateCode == OperateCode.SpecialContentInput)
                    {
                        box.Checked = true;
                        if (!flag)
                        {
                            box.Enabled = flag;
                        }
                    }
                    if (info2.OperateCode == OperateCode.SepcialContentManage)
                    {
                        box2.Checked = true;
                        if (!flag)
                        {
                            box2.Enabled = flag;
                        }
                    }
                }
            }
        }

        private SpecialInfo GetSpecialInfo()
        {
            int specialCategoryId = DataConverter.CLng(this.DropSpecialCategory.SelectedValue);
            if (specialCategoryId <= 0)
            {
                AdminPage.WriteErrMsg("<li>请选择专题所属的类别！</li>", "Special.aspx");
            }
            SpecialInfo info = new SpecialInfo();
            SpecialCategoryInfo specialCategoryInfoById = Special.GetSpecialCategoryInfoById(specialCategoryId);
            info.SpecialCategoryId = specialCategoryId;
            if (BasePage.RequestString("AddType").CompareTo("BatchSpecial") != 0)
            {
                info.SpecialName = this.TxtSpecialName.Text;
                info.SpecialDir = specialCategoryInfoById.SpecialCategoryDir + "/" + this.TxtSpecialDir.Text;
                info.SpecialIdentifier = this.TxtSpecialIdentifier.Text;
            }
            info.SpecialPic = this.TxtSpecialPic.Text;
            info.SpecialTips = this.TxtSpecialTips.Text;
            info.SpecialTemplatePath = this.FileCSpecialTemplatePath.Text;
            info.SearchTemplatePath = this.FileCSearchTemplatePath.Text;
            info.IsElite = DataConverter.CBoolean(this.RadlIsElite.SelectedValue);
            info.OpenType = DataConverter.CLng(this.RadOpenType.SelectedValue);
            info.Description = this.TxtDescription.Text;
            info.IsCreateListPage = DataConverter.CBoolean(this.RadlCreatHtml.SelectedValue);
            info.ListPageSavePathType = (ListPagePathType) Enum.Parse(typeof(ListPagePathType), this.RadlListPageHtmlDirType.SelectedValue);
            info.ListPagePostfix = this.PagePostfix.Value;
            int num2 = DataConverter.CLng(this.DropCustomNum.SelectedValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= num2; i++)
            {
                StringHelper.AppendString(sb, ((TextBox) this.PnlCustomFileds.FindControl("Custom_Content" + i)).Text, "{#$$$#}");
            }
            info.CustomContent = sb.ToString();
            info.NeedCreateHtml = false;
            return info;
        }

        private void InitEChklPermission()
        {
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            this.EgvPermissions.DataSource = userGroupList;
            this.EgvPermissions.DataBind();
            this.EgvRoleView.DataSource = UserRole.GetRoleList();
            this.EgvRoleView.DataBind();
        }

        private void InitialText()
        {
            RolePermissions.AccessCheck(OperateCode.SpecialManage);
            string str = BasePage.RequestStringToLower("Action", "add");
            if (this.Page.IsPostBack)
            {
                return;
            }
            if (str == "copy")
            {
                this.specialId = Special.GetSpecialByIdCopySpecial(this.specialId);
                if (this.specialId > 0)
                {
                    this.InputPermissions(this.specialId);
                    this.InputRolePermission(this.specialId);
                    IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Special);
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("<li>专题信息保存成功！</li>", "Special.aspx?Action=Modify&SpecialID=" + this.specialId.ToString());
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>发生错误！</li>", "");
                }
            }
            IList<SpecialCategoryInfo> specialCategoryList = Special.GetSpecialCategoryList();
            this.DropSpecialCategory.SelectedValue = this.specialCategoryId.ToString();
            this.DropSpecialCategory.DataSource = specialCategoryList;
            this.DropSpecialCategory.DataBind();
            ((DropDownList) this.PnlCustomFileds.FindControl("DropCustomNum")).Attributes.Add("onchange", "setFileFileds(this.value)");
            this.InitEChklPermission();
            string str2 = str;
            if (str2 != null)
            {
                if (!(str2 == "add"))
                {
                    if (str2 == "modify")
                    {
                        this.SmpNavigator.CurrentNode = "修改专题";
                        this.EBtnModify.Visible = true;
                        if (!this.Page.IsPostBack)
                        {
                            this.BindSpecial();
                        }
                        goto Label_023A;
                    }
                }
                else
                {
                    this.SmpNavigator.CurrentNode = "添加专题";
                    this.EBtnAdd.Visible = true;
                    BasePage.SetSelectedIndexByValue(this.DropSpecialCategory, this.specialCategoryId.ToString());
                    if (BasePage.RequestString("AddType").CompareTo("BatchSpecial") == 0)
                    {
                        this.SpecialName.Visible = false;
                        this.SpecialIdentifier.Visible = false;
                        this.SpecialDir.Visible = false;
                        this.BatchSpecial.Visible = true;
                        this.TxtSpecialNames.Attributes.Add("onchange", "GetBatchInitial();");
                    }
                    else
                    {
                        this.TxtSpecialName.Attributes.Add("onchange", "GetInitial();");
                    }
                    goto Label_023A;
                }
            }
            this.SmpNavigator.CurrentNode = "添加专题";
            this.EBtnAdd.Visible = true;
        Label_023A:
            if (!RolePermissions.AccessCheck(OperateCode.AdministratorManage))
            {
                this.TabTitle1.Style.Add("display", "none");
            }
        }

        private void InputPermissions(int specialsId)
        {
            StringBuilder roleIdList = new StringBuilder();
            for (int i = 0; i < this.EgvPermissions.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.EgvPermissions.Rows[i].FindControl("ChkSpecialInput");
                string selectId = this.EgvPermissions.DataKeys[i].Value.ToString();
                AppendSelectId(box.Checked, roleIdList, selectId);
            }
            if (!UserPermissions.AddSpecialPermissions(roleIdList.ToString(), OperateCode.SpecialContentInput, specialsId, 1))
            {
                AdminPage.WriteErrMsg("<li>专题信息录入权限添加失败！</li>");
            }
        }

        private void InputRolePermission(int specialsId)
        {
            StringBuilder roleIdList = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            for (int i = 0; i < this.EgvRoleView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.EgvRoleView.Rows[i].FindControl("ChkSpecialInput");
                CheckBox box2 = (CheckBox) this.EgvRoleView.Rows[i].FindControl("ChkSpecialManage");
                string selectId = this.EgvRoleView.DataKeys[i].Value.ToString();
                AppendSelectId(box.Checked, roleIdList, selectId);
                AppendSelectId(box2.Checked, builder2, selectId);
            }
            RolePermissions.DeleteSpecialPermissionFromRoles(-1, specialsId);
            RolePermissions.AddSepcialPermissionToRoles(roleIdList.ToString(), specialsId, OperateCode.SpecialContentInput);
            RolePermissions.AddSepcialPermissionToRoles(builder2.ToString(), specialsId, OperateCode.SepcialContentManage);
        }

        public static bool IsDir(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return !Regex.IsMatch(input, "[_a-zA-Z\r\n][_a-zA-Z0-9\r\n]*");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.SpecialManage);
            this.specialId = BasePage.RequestInt32("SpecialID");
            this.specialCategoryId = BasePage.RequestInt32("SpecialCategoryID");
            this.InitialText();
        }

        protected void ValxSpecialDir_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(this.TxtSpecialDir.Text))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}


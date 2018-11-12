namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.Model.Crm;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.WebControls;

    public partial class CompanyMemberManage : BaseUserControl
    {
        private int m_CompanyId;
        private EasyOne.Enumerations.UserType m_UserType;

        protected void EgvCompanyMember_RowCommand(object sender, CommandEventArgs e)
        {
            string userName = e.CommandArgument.ToString();
            bool flag = false;
            string successMessage = "";
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "RemoveFromCompany"))
                {
                    if (commandName == "AddToAdmin")
                    {
                        flag = Users.AddToAdminCompany(userName);
                        successMessage = "已经将 " + userName + " 升级为管理员！";
                    }
                    else if (commandName == "RemoveFromAdmin")
                    {
                        flag = Users.RemoveFromAdminCompany(userName);
                        successMessage = "已经将 " + userName + " 降为普通成员！";
                    }
                    else if (commandName == "Agree")
                    {
                        int companyClientId = 0;
                       EasyOne.Model.Crm.CompanyInfo compayById = Company.GetCompayById(this.CompanyId);
                        if (!compayById.IsNull)
                        {
                            companyClientId = compayById.ClientId;
                        }
                        flag = Users.AgreeJoinCompany(userName, companyClientId);
                        successMessage = "已经批准 " + userName + " 加入企业中！";
                    }
                    else if (commandName == "Reject")
                    {
                        flag = Users.RemoveFromCompany(userName);
                        successMessage = "已经拒绝 " + userName + " 加入企业中！";
                    }
                }
                else
                {
                    flag = Users.RemoveFromCompany(userName);
                    successMessage = "已经将 " + userName + " 从企业中删除！";
                }
            }
            if (flag)
            {
                BaseUserControl.WriteSuccessMsg(successMessage, this.ReturnAddress);
            }
            else
            {
                BaseUserControl.WriteErrMsg("<li>操作失败！</li>");
            }
        }

        protected void EgvCompanyMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataItem = e.Row.DataItem as DataRowView;
                LinkButton button = e.Row.Cells[5].FindControl("LbtnRemoveFromCompany") as LinkButton;
                LinkButton button2 = e.Row.Cells[5].FindControl("LbtnAddToAdmin") as LinkButton;
                LinkButton button3 = e.Row.Cells[5].FindControl("LbtnRemoveFromAdmin") as LinkButton;
                LinkButton button4 = e.Row.Cells[5].FindControl("LbtnAgree") as LinkButton;
                LinkButton button5 = e.Row.Cells[5].FindControl("LbtnReject") as LinkButton;
                switch (DataConverter.CLng(dataItem["UserType"]))
                {
                    case 1:
                        return;

                    case 2:
                        if (this.m_UserType == EasyOne.Enumerations.UserType.Creator)
                        {
                            button.Visible = true;
                            button3.Visible = true;
                        }
                        return;

                    case 3:
                        switch (this.m_UserType)
                        {
                            case EasyOne.Enumerations.UserType.Creator:
                                button.Visible = true;
                                button2.Visible = true;
                                return;

                            case EasyOne.Enumerations.UserType.Administrators:
                                button.Visible = true;
                                return;
                        }
                        return;

                    case 4:
                        switch (this.m_UserType)
                        {
                            case EasyOne.Enumerations.UserType.Creator:
                            case EasyOne.Enumerations.UserType.Administrators:
                                button4.Visible = true;
                                button5.Visible = true;
                                return;
                        }
                        return;

                    default:
                        return;
                }
            }
        }

        protected string GetUserTypeText(int userType)
        {
            string str = string.Empty;
            switch (((EasyOne.Enumerations.UserType) userType))
            {
                case EasyOne.Enumerations.UserType.Creator:
                    return "创建者";

                case EasyOne.Enumerations.UserType.Administrators:
                    return "管理员";

                case EasyOne.Enumerations.UserType.CommonLeaguer:
                    return "普通成员";

                case EasyOne.Enumerations.UserType.AuditingLeaguer:
                    return "待审核成员";
            }
            return str;
        }

        private DataTable MemberInfo(int companyId)
        {
            IList<UserInfo> listByCompanyId = Users.GetListByCompanyId(companyId);
            DataTable table = new DataTable();
            table.Columns.Add("UserName");
            table.Columns.Add("TrueName");
            table.Columns.Add("ZipCode");
            table.Columns.Add("Address");
            table.Columns.Add("UserType");
            foreach (UserInfo info in listByCompanyId)
            {
                DataRow row = table.NewRow();
                row["UserName"] = info.UserName;
                ContacterInfo contacterByUserName = Contacter.GetContacterByUserName(info.UserName);
                row["TrueName"] = contacterByUserName.TrueName;
                row["ZipCode"] = contacterByUserName.ZipCode;
                row["Address"] = contacterByUserName.Address;
                row["UserType"] = (int) info.UserType;
                table.Rows.Add(row);
            }
            return table;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (this.m_CompanyId > 0))
            {
                this.EgvCompanyMember.DataSource = this.MemberInfo(this.m_CompanyId);
                this.EgvCompanyMember.DataBind();
            }
        }

        public int CompanyId
        {
            get
            {
                return this.m_CompanyId;
            }
            set
            {
                this.m_CompanyId = value;
            }
        }

        public string ReturnAddress
        {
            get
            {
                if (string.IsNullOrEmpty(this.HdnReturnUrl.Value))
                {
                    return ("UserShow.aspx?UserID=" + this.UserId.ToString());
                }
                return this.HdnReturnUrl.Value;
            }
            set
            {
                this.HdnReturnUrl.Value = value;
            }
        }

        public int UserId
        {
            get
            {
                return DataConverter.CLng(this.ViewState["UserId"]);
            }
            set
            {
                this.ViewState["UserId"] = value;
            }
        }

        public EasyOne.Enumerations.UserType UserType
        {
            get
            {
                return this.m_UserType;
            }
            set
            {
                this.m_UserType = value;
            }
        }
    }
}


namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class CompanyManage : AdminPage
    {
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder("");
            selectList = this.EgvCompany.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要删除的企业！</li>", "");
            }
            else
            {
                Company.Delete(selectList.ToString());
                AdminPage.WriteSuccessMsg("删除企业成功", "CompanyManage.aspx");
            }
        }

        protected void EgvCompany_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                Company.Delete(e.CommandArgument.ToString());
            }
            AdminPage.WriteSuccessMsg("删除企业成功", "CompanyManage.aspx");
        }

        protected void EgvCompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CompanyInfo dataItem = e.Row.DataItem as CompanyInfo;
                Label label = e.Row.FindControl("LblStatusInField") as Label;
                Label label2 = e.Row.FindControl("LblCompanySize") as Label;
                Label label3 = e.Row.FindControl("LblManagementForms") as Label;
                label.Text = Choiceset.GetDataText("PE_Company", "StatusInField", dataItem.StatusInField);
                label2.Text = Choiceset.GetDataText("PE_Company", "CompanySize", dataItem.CompanySize);
                label3.Text = Choiceset.GetDataText("PE_Company", "ManagementForms", dataItem.ManagementForms);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


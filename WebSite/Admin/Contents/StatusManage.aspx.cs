namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.WorkFlow;
    using EasyOne.Web.UI;
    using EasyOne.WorkFlows;
    using System;
    using System.Web.UI.WebControls;

    public partial class StatusManage : AdminPage
    {

        protected void EgvStatus_RowCommand(object sender, CommandEventArgs e)
        {
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "DeleteStatus"))
                {
                    if (commandName == "ModifyStatus")
                    {
                        BasePage.ResponseRedirect("Status.aspx?Action=Modify&StatusID=" + DataConverter.CLng(e.CommandArgument.ToString()));
                    }
                }
                else
                {
                    try
                    {
                        Status.Delete(DataConverter.CLng(e.CommandArgument.ToString()));
                        AdminPage.WriteSuccessMsg("<li>稿件状态码删除成功！</li>", "StatusManage.aspx");
                    }
                    catch (CustomException exception)
                    {
                        AdminPage.WriteErrMsg("<li>" + exception.Message + "</li>", "StatusManage.aspx");
                    }
                }
            }
        }

        protected void EgvStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (((StatusInfo) e.Row.DataItem).StatusType == 0))
            {
                ((LinkButton) e.Row.FindControl("ELbtnDelete")).Enabled = false;
                ((LinkButton) e.Row.FindControl("ELbtnModify")).Enabled = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


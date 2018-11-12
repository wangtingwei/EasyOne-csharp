namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Logging;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class LogManager : AdminPage
    {

        protected void BtnClearLog_Click(object sender, EventArgs e)
        {
            DBLog log = new DBLog();
            LogInfo info = new LogInfo();
            info.UserName = PEContext.Current.Admin.AdminName;
            info.UserIP = PEContext.Current.UserHostAddress;
            info.ScriptName = base.Request.RawUrl;
            info.Timestamp = DateTime.Now;
            info.Message = "清空日志";
            info.Title = info.Message;
            log.Add(info);
            log.Delete(DateTime.Today.AddDays(-2.0));
            AdminPage.WriteSuccessMsg("成功清空了日志。注意：两天内的日志会被系统保留。", "LogManager.aspx");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (this.GdvLogManager.SelectList.Length == 0)
            {
                AdminPage.WriteErrMsg("请选择要删除的日志！");
            }
            else
            {
                DBLog log = new DBLog();
                if (log.Delete(this.GdvLogManager.SelectList.ToString()))
                {
                    AdminPage.WriteSuccessMsg("所选日志已成功删除！", "LogManager.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("日志删除失败！");
                }
            }
        }

        protected void GdvLogManager_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                base.Items["LogId"] = e.CommandArgument;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

